
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TherapyCenter.Helpers;
using TherapyCenter2.Data;
using TherapyCenter2.Helper;
using TherapyCenter2.Middleware;
using TherapyCenter2.Repositories.Implementations;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;
using TherapyCenter2.Services.Interfaces;
using TherapyCenter2.Services.Payment;

namespace TherapyCenter2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseMySql(
         builder.Configuration.GetConnectionString("DefaultConnection"),
         ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
     ));
            builder.Services.Configure<StripeSettings>(
            builder.Configuration.GetSection("StripeSettings"));

            builder.Services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<StripeSettings>>().Value);

            builder.Services.AddScoped<IPaymentService,PaymentService>();


            var jwtSettings = builder.Configuration
               .GetSection("Jwt")
               .Get<JwtSettings>() ?? throw new Exception("JWT settings not found");

            builder.Services.AddSingleton(jwtSettings);


            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtHelper, JwtHelper>();

            builder.Services.AddScoped<ITherapyRepository, TherapyRepository>();
            builder.Services.AddScoped<ITherapyService, TherapyService>();

            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();

            builder.Services.AddScoped<ISlotRepository, SlotRepository>();
            builder.Services.AddScoped<ISlotService, SlotService>();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // generates swagger.json
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; // makes Swagger UI at root /
                });
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
