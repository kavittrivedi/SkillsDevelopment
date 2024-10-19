using EmployeeSkillsDevelopment.Api.Configurations;
using EmployeeSkillsDevelopment.Api.Filters;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Mappers;
using EmployeeSkillsDevelopment.Core.Services;
using EmployeeSkillsDevelopment.Infrastructure.Boot;
using EmployeeSkillsDevelopment.Infrastructure.Interfaces;
using EmployeeSkillsDevelopment.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for logging
var logFilePath = @"..\..\logs\log.txt";
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Ensure Serilog is used for logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Configure CORS for Angular app
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAngularApplication", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .WithOrigins("https://editor.swagger.io")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add services to the container

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://login.microsoftonline.com/e74b78cf-bd8b-497f-b7ca-0ab771eb93a6";
        options.Audience = "api://601a5a34-48da-492a-9a16-d5063d2a6ee4"; // Your WebApi Application ID URI
    });

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.Requirements.Add(new RoleRequirement("Admin")));
    options.AddPolicy("TesterPolicy", policy =>
          policy.Requirements.Add(new RoleRequirement("Admin", "Tester")));
    options.AddPolicy("DeveloperPolicy", policy =>
              policy.Requirements.Add(new RoleRequirement("Admin", "Developer")));
});



// AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAutoMapper(typeof(DtoMapperProfile));

// Register services and filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>(); // Register the exception filter
});
builder.Services.AddScoped<IAuthorizationHandler, RoleHandler>();
builder.Services.Configure<StorageSettings>(builder.Configuration.GetSection("StorageSettings"));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ExceptionFilter>();

// Database connection
BootStrapper.ConnectionStringBootStrapper(builder);

// Dependency injection
BootStrapper.DataBootStrapper(builder.Services);

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c=>
    {
        c.SwaggerDoc("Employee", new OpenApiInfo { Title = "Swagger Azure AD Demo", Version = "v1", Description = "APIs related to employee" });
        c.SwaggerDoc("File", new OpenApiInfo { Title = "Swagger Azure AD Demo", Version = "v1", Description = "APIs related to files"});
        c.SwaggerDoc("User", new OpenApiInfo { Title = "Swagger Azure AD Demo", Version = "v1", Description = "APIs related to user"});

        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Outh2.0 which uses AuthenticationCode flow",
            Name = "oauth2.0",
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAD:AuthorizationUrl"]),
                    TokenUrl = new Uri(builder.Configuration["SwaggerAzureAD:TokenUrl"]),
                    Scopes = new Dictionary<string,string>
                    {
                        {builder.Configuration["SwaggerAzureAd:Scope"],"Access API as User" }
                    }
                }
            }
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="oauth2"}
                },
                new []{builder.Configuration["SwaggerAzureAd:Scope"]}
            }
        });
        //for XML comments
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        c.AddServer(new OpenApiServer()
        {
            Url = "http://localhost:5006"
        });

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        
        c.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
        //c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/Employee/swagger.yaml", "Employee v1");
        c.SwaggerEndpoint("/swagger/File/swagger.yaml", "File v1");
        c.SwaggerEndpoint("/swagger/Employee/swagger.yaml", "User v1");

    });
}

app.UseAuthentication();
app.UseCors("AllowAngularApplication");
app.UseAuthorization();
app.MapControllers();

app.Run();
