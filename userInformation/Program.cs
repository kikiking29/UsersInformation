using userInformation.Interfaces;
using userInformation.Services;
using userInformation.Models;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using VDS.RDF.Nodes;
using VDS.RDF.Query.Algebra;
using Microsoft.AspNetCore.Authentication;

namespace userInformation
{
    public class Program
    {
        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();
            builder.Services.AddConnections();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication().AddCookie("defult");

            var dateTimeNow = DateTime.UtcNow;
            

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<ITokenService, TokenService>();


            builder.Services.AddAuthentication("MyAuthScheme")
            .AddCookie("MyAuthScheme", options => {

                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
                
            }).AddJwtBearer(options => { options.Events.OnMessageReceived = context => {
                if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                {
                    context.Token = context.Request.Cookies["X-Access-Token"];
                }
            return Task.CompletedTask;
                };
            });


            builder.Services.AddTransient<CustomCookieAuthenticationEvents>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization(builder =>
            {
                builder.AddPolicy("mypolicy",pb => pb
                .RequireAuthenticatedUser()
                .RequireClaim("doesntexist","nonse")
                );
            });
           

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();


            var app = builder.Build();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

           
            app.MapControllers();

           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.MapGet("/Logout", async (HttpContext context) =>
            {
                await context.SignOutAsync("MyAuthScheme",
                    new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                return "OK";
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();

           


            app.MapControllers();

            app.Run();
           
        }

    }

}



//var securityScheme = new OpenApiSecurityScheme()
//{
//    Name = "Admin",
//    Type = SecuritySchemeType.Http,
//    Scheme = "bearer",
//    BearerFormat = "JWT",
//    In = ParameterLocation.Header,
//    Description = "JSON Web Token based security",
//    Reference = new OpenApiReference
//    {
//        Type = ReferenceType.SecurityScheme,
//        Id = JwtBearerDefaults.AuthenticationScheme,
//    }
//};

////builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(o =>
//{
//    //o.SwaggerDoc("v1", info);
//    o.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
//    o.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {securityScheme, new string[] { }}
//    });
//});

//builder.Services.AddAuthentication(o =>
//{
//    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true
//    };
//});