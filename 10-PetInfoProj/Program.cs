using Microsoft.EntityFrameworkCore;
using PetInfo.Database.Data;
using PetInfo.Repositories.AllRepositories;
using PetInfo.Repositories.Contracts.AllContracts;
using PetInfo.Services.AllServices;
using PetInfo.Services.Contracts.AllContracts;

namespace _10_PetInfoProj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString")));
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IUnitService, UnitService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
