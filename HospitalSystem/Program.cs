using Microsoft.EntityFrameworkCore;
using HospitalSystem.Data; // تأكد من الـ Namespace بتاعك

var builder = WebApplication.CreateBuilder(args);

// 1. ربط الداتا بيز باسم الكلاس الصح
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. تفعيل الـ MVC Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. إعدادات الـ Pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 4. المسار الافتراضي يفتح على إضافة دكتور
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Doctors}/{action=Create}/{id?}");

            // 2. إضافة الخدمات الأساسية
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // إعدادات الـ Pipeline
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
