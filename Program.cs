using AdvancedDb_Order_Inventory.Repository;
using Microsoft.EntityFrameworkCore;

namespace AdvancedDb_Order_Inventory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            var app = builder.Build();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
