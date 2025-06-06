using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface IAdminService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task ToggleUserStatusAsync(Guid userId, bool isActive);

    Task<IEnumerable<ReportDTO>> GetPlatformAnalyticsAsync();
}