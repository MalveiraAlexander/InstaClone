using InstaClone;
using InstaClone.Commons.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using InstaClone.Commons.Exceptions;
using Microsoft.OpenApi.Models;
using InstaClone.Commons.Helpers;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using InstaClone.Core.Validators;
using System.Xml.Linq;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var certificate = new CertificateGeneratorHelper();

builder.Services.AddDbContext<SystemContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptionsAction => npgsqlOptionsAction.MigrationsAssembly("InstaClone")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CommentRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PostReactionRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clon de Instagram API", Version = "v1" });
    var dir = new DirectoryInfo(AppContext.BaseDirectory);
    foreach (var fi in dir.EnumerateFiles("*.xml"))
    {
        var doc = XDocument.Load(fi.FullName);
        c.IncludeXmlComments(() => new XPathDocument(doc.CreateReader()), true);
    }
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = builder.Configuration["JWT:Issuer"],
                         ValidAudience = builder.Configuration["JWT:Audience"],
                         IssuerSigningKey = certificate.GetPublicSigningKey(),
                     };
                 });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddDependency();

builder.Services.AddHealthChecks().AddDbContextCheck<SystemContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.MapHealthChecks("/status", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status202Accepted,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseExceptionHandlerMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
