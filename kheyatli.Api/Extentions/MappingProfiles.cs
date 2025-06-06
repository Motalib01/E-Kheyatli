using AutoMapper;
using kheyatli.Api.Dtos;
using kheyatli.Api.Models;

namespace kheyatli.Api.Extentions;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Tailor, TailorDTO>().ReverseMap();
        CreateMap<Client, ClientDTO>().ReverseMap();
        CreateMap<Admin, AdminDTO>().ReverseMap();
        CreateMap<Review, ReviewDTO>().ReverseMap();
        CreateMap<Report, ReportDTO>().ReverseMap();
        CreateMap<Product,ProductDTO>().ReverseMap();
        CreateMap<ProductMeasurement, ProductMeasurementDTO>().ReverseMap();
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<Portfolio, PortfolioDTO>().ReverseMap();
        CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();
        CreateMap<Notification, NotificationDTO>().ReverseMap();
        CreateMap<MeasurementsGuide, MeasurementsGuideDTO>().ReverseMap();

    }
    
}