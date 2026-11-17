
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using Ecommerce533.Reposetories;
using HospitalSystem.Reposetories;

namespace HospitalSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository Pattern
            builder.Services.AddScoped(
                typeof(IRepository<>), typeof(Repository<>));

            // Unit Of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Default Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Appointments}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
