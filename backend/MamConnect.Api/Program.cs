using MamConnect.Api.Services;
using MamConnect.Application.Assistants.Commands;
using MamConnect.Application.Assistants.Queries;
using MamConnect.Application.Auth.Commands;
using MamConnect.Application.Auth.Queries;
using MamConnect.Application.Children.Commands;
using MamConnect.Application.Children.Queries;
using MamConnect.Application.Common.Interfaces;
using MamConnect.Application.DailyReports.Commands;
using MamConnect.Application.DailyReports.Queries;
using MamConnect.Application.Parents.Commands;
using MamConnect.Application.Parents.Queries;
using MamConnect.Domain.Entities;
using MamConnect.Domain.Services;
using MamConnect.Infrastructure.Data;
using MamConnect.Infrastructure.Repositories;
using MamConnect.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Brancher EF Core + SQL Server (ici LocalDB)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sql => sql.EnableRetryOnFailure()
    ));

var jwtKey = builder.Configuration["Jwt:Key"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IVaccinationService, VaccinationService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IChildrenRepository, ChildrenRepository>();
builder.Services.AddScoped<IDailyReportRepository, DailyReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<GetChildrenQuery>();
builder.Services.AddScoped<GetChildrenWithRelationsQuery>();
builder.Services.AddScoped<CreateChildCommand>();
builder.Services.AddScoped<UpdateChildCommand>();
builder.Services.AddScoped<DeleteChildCommand>();
builder.Services.AddScoped<GetAuthorizedChildIdsQuery>();
builder.Services.AddScoped<GetDailyReportsQuery>();
builder.Services.AddScoped<GetChildDailyReportsQuery>();
builder.Services.AddScoped<CreateDailyReportCommand>();
builder.Services.AddScoped<RegisterUserCommand>();
builder.Services.AddScoped<LoginUserQuery>();
builder.Services.AddScoped<SetPasswordCommand>();
builder.Services.AddScoped<GetAssistantsQuery>();
builder.Services.AddScoped<CreateAssistantCommand>();
builder.Services.AddScoped<UpdateAssistantCommand>();
builder.Services.AddScoped<DeleteAssistantCommand>();
builder.Services.AddScoped<GetParentsQuery>();
builder.Services.AddScoped<CreateParentCommand>();
builder.Services.AddScoped<UpdateParentCommand>();
builder.Services.AddScoped<DeleteParentCommand>();
builder.Services.AddScoped<SetParentChildrenCommand>();

// Swagger pour tester facilement
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Ajoute la politique CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVite", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // origine Vite
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
// Active la politique CORS
app.UseCors("AllowVite");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();