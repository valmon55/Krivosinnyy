using AutoMapper;
using FKA.Krivosinnyy.DAL;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FKA.Krivosinnyy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mapperConfig = new MapperConfiguration(v =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddDbContext<MyFamilyContext>(options => options.UseSqlServer("Data Source=DESKTOP-ACP6245\\SQLEXPRESS;Database=Krivosinnyy;Integrated Security = true;Trust Server Certificate=True;Trusted_Connection=True;"));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<MyFamilyContext>();

            builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/Home/Error";
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
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
