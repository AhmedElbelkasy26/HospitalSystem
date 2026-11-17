using Ecommerce533.Reposetories;
using HospitalManagementSystem.Data; // تأكد إن ده نفس الـ Namespace بتاع مشروعك
using HospitalSystem.Reposetories;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            // 2. إضافة الخدمات الأساسية
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();

            var app = builder.Build();

            // إعدادات الـ Pipeline
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapControllerRoute(
             name: "default",
            pattern: "{controller=Patient}/{action=Index}/{id?}");

            app.UseStaticFiles();
            app.Run();
        }
    }
}