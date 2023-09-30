using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillSwap_API.Business.Domain.Repositories;
using SkillSwap_API.Business.Domain.Services;
using SkillSwap_API.Business.Persistence;
using SkillSwap_API.Business.Services;
using SkillSwap_API.Security.Authorization.Handlers.Implementations;
using SkillSwap_API.Security.Authorization.Handlers.Interfaces;
using SkillSwap_API.Security.Authorization.Middleware;
using SkillSwap_API.Security.Authorization.Settings;
using SkillSwap_API.Security.Domain.Repositories;
using SkillSwap_API.Security.Domain.Services;
using SkillSwap_API.Security.Persistence.Repositories;
using SkillSwap_API.Security.Services;
using SkillSwap_API.Shared.Persistence.Contexts;
using SkillSwap_API.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add API Documentation Information
    
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Shared Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Dependency Injection Configuration
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<ILeccionRepository, LeccionRepository>();
builder.Services.AddScoped<ILeccionService, LeccionService>();

// Security Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(SkillSwap_API.Business.Mapping.ModelToResourceProfile),
    typeof(SkillSwap_API.Business.Mapping.ResourceToModelProfile),
    
    typeof(SkillSwap_API.Security.Mapping.ModelToResourceProfile),
    typeof(SkillSwap_API.Security.Mapping.ResourceToModelProfile));


var app = builder.Build();

// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}
// Configure CORS 
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JWT Handling
app.UseMiddleware<JwtMiddleware>();

//Default
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program {}