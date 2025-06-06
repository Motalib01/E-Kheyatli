using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public class AdminService : IAdminService
{
    public Task<IEnumerable<UserDTO>> GetAllUsersAsync() => Task.FromResult<IEnumerable<UserDTO>>(new List<UserDTO>());
    public Task ToggleUserStatusAsync(Guid userId, bool isActive) => Task.CompletedTask;
    public Task<IEnumerable<ReportDTO>> GetPlatformAnalyticsAsync() => Task.FromResult<IEnumerable<ReportDTO>>(new List<ReportDTO>());
}