using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface ITailorService
{
    Task<IEnumerable<OrderDTO>> ViewOrdersAsync(Guid tailorId);
    Task<bool> MarkOrderAsDeliveredAsync(Guid orderId);
    Task AddMeasurementGuideAsync(Guid tailorId, MeasurementsGuideDTO guide);
    Task<IEnumerable<MeasurementsGuideDTO>> GetMyMeasurementGuidesAsync(Guid tailorId);
}