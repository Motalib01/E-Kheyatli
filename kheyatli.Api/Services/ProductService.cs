using kheyatli.Api.Dtos;

namespace kheyatli.Api.Services;

public class ProductService : IProductService
{
    public Task UpdateProductMeasurementsAsync(Guid productId, IEnumerable<ProductMeasurementDTO> measurements) => Task.CompletedTask;
}