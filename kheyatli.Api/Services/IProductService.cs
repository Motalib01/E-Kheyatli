using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public interface IProductService
{
    Task UpdateProductMeasurementsAsync(Guid productId, IEnumerable<ProductMeasurementDTO> measurements);
}