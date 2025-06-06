using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface IMeasurementsGuideService
{
    Task<IEnumerable<MeasurementsGuideDTO>> GetGuidesByTailorAsync(Guid tailorId);
}