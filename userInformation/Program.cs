using Google.Protobuf.WellKnownTypes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using userInformation.Services.UserService;
using userInformation.Services;

namespace userInformation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<CustomCookieAuthenticationEvents>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization(builder =>
            {
                builder.AddPolicy("mypolicy", pb => pb
                .RequireAuthenticatedUser()
                .RequireClaim("doesntexist", "nonse")
                );
            });


            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddAuthentication("MyAuthScheme")
            //.AddCookie("MyAuthScheme", options =>
            //{

            //    options.Cookie.SameSite = SameSiteMode.Lax;
            //    options.Cookie.Name = ".AspNet.SharedCookie";
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Cookie.MaxAge = options.ExpireTimeSpan;
            //    options.SlidingExpiration = true;
            //    options.EventsType = typeof(CustomCookieAuthenticationEvents);
            //});

            builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
            policy =>
            {
                policy.WithOrigins("http://localhost:44385").AllowAnyMethod().AllowAnyHeader();
            }));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("NgOrigins");


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}