using AutoMapper;
using FKA.Krivosinnyy.DAL;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.DAL.Repositories;
using FKA.Krivosinnyy.Services;
using FKA.Krivosinnyy.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FKA.Krivosinnyy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("HP_Connection");

            var mapperConfig = new MapperConfiguration(v =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            
            // Add services to the container.
            builder.Services.AddSingleton(mapper);
            builder.Services.AddDbContext<MyFamilyContext>(options => 
                options.UseSqlServer(connection, b => b.MigrationsAssembly("FKA.Krivosinnyy"))
                        //.ConfigureWarnings(warnings => 
                        //                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
                        );
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddTransient<IPersonService, PersonService>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
            builder.Services.AddTransient<IRelationshipService, RelationshipService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddDefaultTokenProviders()
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
            builder.Services.AddControllersWithViews();

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
