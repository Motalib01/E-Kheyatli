using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public class MeasurementsGuideService : IMeasurementsGuideService
{
    public Task AddGuideAsync(Guid tailorId, MeasurementsGuideDTO guide) => Task.CompletedTask;
    public Task<IEnumerable<MeasurementsGuideDTO>> GetGuidesByTailorAsync(Guid tailorId) => Task.FromResult<IEnumerable<MeasurementsGuideDTO>>(new List<MeasurementsGuideDTO>());
    public Task<MeasurementsGuideDTO> GetGuideByIdAsync(Guid guideId) => Task.FromResult<MeasurementsGuideDTO>(null);
}