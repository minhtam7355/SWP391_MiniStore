using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;

namespace SWP391_MiniStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Connect to the database
            builder.Services.AddDbContext<MiniStoreDbContext>(option =>
                option.UseSqlServer(builder.Configuration
                .GetConnectionString("MiniStoreConnectionString")));

            // Add Authentication
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Access/Login";
                    option.AccessDeniedPath = "/Access/Error";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    option.SlidingExpiration = true;
                });

            // Add Authorization
            // Omit the builder.Services.AddAuthorization() call, and the default authorization policy with role-based support will be added automatically by the framework
            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy("ManagerOnly", policy => policy.RequireClaim("Role", "Manager"));
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