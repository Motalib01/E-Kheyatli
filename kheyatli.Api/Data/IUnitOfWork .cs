using kheyatli.Api.Models;
using kheyatli.Api.Repositories;

namespace kheyatli.Api.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Admin> Admins { get; }
        IRepository<Client> Clients { get; }
        IRepository<Tailor> Tailors { get; }
        IRepository<Order> Orders { get; }
        IRepository<Chat> Chats { get; }
        IRepository<ChatMessage> ChatMessages { get; }
        IRepository<Portfolio> Portfolios { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductMeasurement> ProductMeasurements { get; }
        IRepository<MeasurementsGuide> MeasurementGuides { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Notification> Notifications { get; }
        IRepository<Category> Categories { get; }


        Task<int> CompleteAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<User> Users { get; }
        public IRepository<Admin> Admins { get; }
        public IRepository<Client> Clients { get; }
        public IRepository<Tailor> Tailors { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<Chat> Chats { get; }
        public IRepository<ChatMessage> ChatMessages { get; }
        public IRepository<Portfolio> Portfolios { get; }
        public IRepository<Product> Products { get; }
        public IRepository<ProductMeasurement> ProductMeasurements { get; }
        public IRepository<MeasurementsGuide> MeasurementGuides { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<Notification> Notifications { get; }
        public IRepository<Category> Categories { get; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Users = new Repository<User>(_context);
            Admins = new Repository<Admin>(_context);
            Clients = new Repository<Client>(_context);
            Tailors = new Repository<Tailor>(_context);
            Orders = new Repository<Order>(_context);
            Chats = new Repository<Chat>(_context);
            ChatMessages = new Repository<ChatMessage>(_context);
            Portfolios = new Repository<Portfolio>(_context);
            Products = new Repository<Product>(_context);
            ProductMeasurements = new Repository<ProductMeasurement>(_context);
            MeasurementGuides = new Repository<MeasurementsGuide>(_context);
            Reviews = new Repository<Review>(_context);
            Notifications = new Repository<Notification>(_context);
            Categories = new Repository<Category>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}