// NotificationService.Tests/Mocks/TestFarmAuthorizationService.cs
using System;
using Shared.FarmAuthorizationService; // Assuming this is where IFarmAuthorizationService is

namespace NotificationService.Tests.Mocks
{
    public class TestFarmAuthorizationService : IFarmAuthorizationService
    {
        public Guid? FarmId { get; set; }
        public Guid? UserId { get; set; }
        public required string Role { get; set; } // Or however your roles are managed

        public Guid? GetFarmId() => FarmId;
        public Guid? GetUserId() => UserId;
        public string GetUserRole() => Role; // Adapt if your interface is different
        public bool IsManager() => Role == "Manager";
        public bool IsWorker() => Role == "Worker";
    }
}