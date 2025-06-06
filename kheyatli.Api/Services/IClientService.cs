using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface IClientService
{
    Task<PortfolioDTO> ViewTailorPortfolioAsync(Guid tailorId);
    Task AddMeasurementsAsync(Guid productId, IEnumerable<ProductMeasurementDTO> measurements);
    Task<IEnumerable<OrderDTO>> TrackOrdersAsync(Guid clientId);
    Task<IEnumerable<MeasurementsGuideDTO>> ViewMeasurementGuidesAsync(Guid tailorId);

}