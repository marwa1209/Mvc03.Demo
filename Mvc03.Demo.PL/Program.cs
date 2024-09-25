using Microsoft.EntityFrameworkCore;
using Mvc.Demo.DAL.Data.Contexts;
using Mvc03.Demo.BLL.Interfaces;
using Mvc03.Demo.BLL;
using Mvc03.Demo.BLL.Repositories;
using Mvc03.Demo.PL.Controllers;
using Mvc03.Demo.PL.Mapping.Employees;
using Mvc03.Demo.PL.Services;
using Microsoft.AspNetCore.Identity;
using Mvc.Demo.DAL.Models;

namespace Mvc03.Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //  builder.Services.AddScoped<AppDBContext>(); //allow DI for AooDBContext
            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
            }); //scoped
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //allow DI for Department Repository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//allow DI for Employee Repository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddSingleton<ISingeltonService, SingeltonService>();


           // builder.Services.AddScoped<UserManager<ApplicationUser>>();

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddIdentity<ApplicationUser , IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
