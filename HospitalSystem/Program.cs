using HospitalSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة الـ DbContext مع قراءة الـ Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
     

            builder.Services.AddScoped(typeof(IRepository<>), typeof(IRepository<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var app = builder.Build();

// 2. إضافة الخدمات الأساسية
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// إعدادات الـ Pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();