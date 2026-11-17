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

app.Run();