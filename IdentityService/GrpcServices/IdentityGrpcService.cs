using Grpc.Core;
using IdentityService.Services;

namespace IdentityService.GrpcServices;

public class IdentityGrpcService(IAuthService authService) : IdentityService.IdentityServiceBase
{
    public override async Task<LoginResponse> Authenticate(LoginRequest request, ServerCallContext context)
    {
        var token = await authService.AuthenticateAsync(request.Email, request.Password);

        if (token == null)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid credentials"));
        }

        return new LoginResponse
        {
            Token = token,
            RefreshToken = Guid.NewGuid().ToString()
        };
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        await authService.RegisterAsync(request.Username, request.Email, request.Password, null);
        return new RegisterResponse
        {
            Success = true,
            Message = "User registered successfully"
        };
    }
}