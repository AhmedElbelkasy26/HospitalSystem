using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data; // تأكد إن ده نفس الـ Namespace بتاع مشروعك

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة الـ DbContext مع قراءة الـ Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. إضافة الخدمات الأساسية
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// إعدادات الـ Pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();