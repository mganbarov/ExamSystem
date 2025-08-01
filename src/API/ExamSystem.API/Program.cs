using ExamSystem.API.Middlewares;
using ExamSystem.Application;
using ExamSystem.Infrastructure;
using ExamSystem.Infrastructure.Configurations;
using ExamSystem.Persistence;
using ExamSystem.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("az");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ExamSystem.API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddCors(p => p.AddPolicy("corspolicy", policy =>
{
    policy.WithOrigins("http://localhost:4200", "http://localhost:64976")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
}));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.RegisterApplicationDependencies();
builder.Services.AddInfrastructureServices(builder.Configuration);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope())
{
    var intializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextIntializer>();
    intializer.IntializeDbContext().Wait();
}

app.UseCors("corspolicy");

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
