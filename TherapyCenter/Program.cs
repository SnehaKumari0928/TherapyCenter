
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TherapyCenter.Data;
using TherapyCenter.Middleware;
using TherapyCenter.Repositories.Implementation;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Implementation;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var jwtKey = builder.Configuration["Jwt:Key"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

            builder.Services.AddAuthorization();

            //services

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<ITherapyService, TherapyService>();
            builder.Services.AddScoped<ISlotService, SlotService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IDoctorFindingService, DoctorFindingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();


            builder.Services.AddScoped<IJwtService, JwtService>();



            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<ITherapyRepository, TherapyRepository>();
            builder.Services.AddScoped<ISlotRepository, SlotRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IDoctorFindingRepository, DoctorFindingRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseMySql(
         builder.Configuration.GetConnectionString("DefaultConnection"),
         ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
     ));


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();
            app.MapControllers();

            app.Run();
        }
    }
}
