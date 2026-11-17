using Ecommerce533.Reposetories;
using HospitalSystem.Data;
using HospitalSystem.Reposetories;
using Microsoft.EntityFrameworkCore;

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


            // 2. إضافة الخدمات الأساسية
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
