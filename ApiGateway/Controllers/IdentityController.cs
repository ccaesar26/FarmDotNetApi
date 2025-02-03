// using ApiGateway.Contracts.Identity;
// using IdentityService;
// using Grpc.Net.Client;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ApiGateway.Controllers;
//
// [ApiController]
// [Route("/api/[controller]")]
// public class IdentityController(IdentityService.IdentityService.IdentityServiceClient identityClient) : ControllerBase
// {
//     [HttpPost("login")]
//     public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
//     {
//         var grpcRequest = new LoginRequest
//         {
//             Email = request.Email,
//             Password = request.Password
//         };
//         
//         var grpcResponse = await identityClient.AuthenticateAsync(grpcRequest);
//         
//         return Ok(grpcResponse);
//     }
//     
//     [HttpPost("register")]
//     public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
//     {
//         var grpcRequest = new RegisterRequest
//         {
//             Username = request.Username,
//             Email = request.Email,
//             Password = request.Password,
//             FarmId = request.FarmId
//         };
//         
//         var grpcResponse = await identityClient.RegisterAsync(grpcRequest);
//         
//         return Ok(grpcResponse);
//     }
// }