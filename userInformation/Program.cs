
using userInformation.Services.UserService;
using userInformation.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
        
            builder.Services.AddScoped<IUserService, UserService>();
        

            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<CustomCookieAuthenticationEvents>();
            builder.Services.AddHttpContextAccessor();

            //builder.Services.AddAuthorization(builder =>
            //{
            //    builder.AddPolicy("mypolicy", pb => pb
            //    .RequireAuthenticatedUser()
            //    .RequireClaim("doesntexist", "nonse")
            //    );
            //});


            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen(options =>
            //{
            //    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    {
            //        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            //        In = ParameterLocation.Header,
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey
            //    });

            //    options.OperationFilter<SecurityRequirementsOperationFilter>();
            //});

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                 };
             });
            builder.Services.AddRazorPages();

            //builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
            //policy =>
            //{
            //    policy.WithOrigins("http://localhost:44385").AllowAnyMethod().AllowAnyHeader();
            //}));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
              
            //}
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseCors("NgOrigins");
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseAuthentication();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.Run();
        }
    }
}