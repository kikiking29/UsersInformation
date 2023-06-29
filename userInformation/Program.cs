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

namespace userInformation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddConnections();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication().AddCookie("defult");

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddAuthentication("MyAuthScheme")
            .AddCookie("MyAuthScheme", options => {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
            });

            builder.Services.AddHttpContextAccessor();

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



            //var contact = new OpenApiContact()
            //{
            //    Name = "Admid",
            //    Url = new Uri("https://localhost:44385/swagger/index.html")
            //};
            //var license = new OpenApiLicense()
            //{
            //    Name = "Free License"
            //};
            //var info = new OpenApiInfo()
            //{
            //    Title = "420",
            //    Version = "v1",
            //    Title = "Minimal API - JWT Authentication with Swagger demo",
            //    Description = "Implementing JWT Authentication in Minimal API",
            //    TermsOfService = new Uri("https://localhost:44385/swagger/index.html"),
            //    Contact = contact,
            //    License = license
            //};

            //builder.Services.AddEndpointsApiExplorer();
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

            //builder.Services.AddAuthorization();
            //builder.Services.AddEndpointsApiExplorer();


            var app = builder.Build();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapRazorPages();
            app.MapControllers();
            //UserLoginRequest user = new UserLoginRequest();

            //app.MapPost("/security/getToken", [AllowAnonymous] (UserLoginRequest user) =>
            //{

            //    if (user.Username == "admin@mohamadlawand.com" && user.Password == "1234")
            //    {
            //        var secureKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

            //        var issuer = builder.Configuration["Jwt:Issuer"];
            //        var audience = builder.Configuration["Jwt:Audience"];
            //        var securityKey = new SymmetricSecurityKey(secureKey);
            //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //        // Now its ime to define the jwt token which will be responsible of creating our tokens
            //        var jwtTokenHandler = new JwtSecurityTokenHandler();

            //        // We get our secret from the appsettings
            //        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);


            //        var tokenDescriptor = new SecurityTokenDescriptor
            //        {
            //            Subject = new ClaimsIdentity(new[]
            //            {
            //            new Claim("Id", "1"),
            //            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            //            new Claim(JwtRegisteredClaimNames.Email, user.Username),
            //            // the JTI is used for our refresh token which we will be convering in the next video
            //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //            }),
            //            // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
            //            // but since this is a demo app we can extend it to fit our current need
            //            Expires = DateTime.UtcNow.AddHours(6),
            //            Audience = audience,
            //            Issuer = issuer,
            //            // here we are adding the encryption alogorithim information which will be used to decrypt our token
            //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //        };

            //        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            //        var jwtToken = jwtTokenHandler.WriteToken(token);

            //        return Results.Ok(jwtToken);
            //    }
            //    else
            //    {
            //        return Results.Unauthorized();
            //    }
            //});

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseHttpsRedirection();
            app.UseAuthorization();

           


            app.MapControllers();

            app.Run();
           
        }
    }
}