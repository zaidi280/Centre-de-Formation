using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using workspace.Controllers;
using workspace.Data;
using workspace.Models;
using workspace.Models.Repository;
using workspace.Models.Services;
using workspace.Repository;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Remplacez par l'URL de votre React App
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});
// Add services to the container.
builder.Services.AddControllers();

// Swagger setup for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection setup
var connectionString = builder.Configuration.GetConnectionString("dbconnexion");
builder.Services.AddDbContext<WorkspaceContext>(options => options.UseSqlServer(connectionString));





// Identity setup
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<WorkspaceContext>()
    .AddDefaultTokenProviders();

// Authentication and JWT Bearer token setup
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWT:Issuer"], 
        ValidAudience = builder.Configuration["JWT:Audience"]
    };
});

// Ajouter les services nécessaires
builder.Services.AddScoped<IEnseignantService, EnseignantService>();

// Ajout de l'interface IRepository<Enseignant> et de son implémentation
builder.Services.AddScoped<IRepository<Enseignant>, EnseignantRepository>();

builder.Services.AddScoped<IEtudiantService, EtudiantService>();
builder.Services.AddScoped<IRepository<Etudiant>, EtudiantRepository>();
builder.Services.AddScoped<ICourService, CourService>();
builder.Services.AddScoped<IRepository<Cour>, CourRepository>();
builder.Services.AddScoped<IMatiereService, MatiereService>();
builder.Services.AddScoped<IRepository<Matiere>, MatiereRepository>();
builder.Services.AddScoped<ISalleService, SalleService>();
builder.Services.AddScoped<IRepository<Salle>, SalleRepository>();
builder.Services.AddScoped<IFormationService, FormationService>();
builder.Services.AddScoped<IRepository<Formation>, FormationRepository>();
// Ajouter une politique personnalisée



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireClaim("IsAdmin", "true")  // Exige que la revendication "IsAdmin" soit présente avec la valeur "true"
              .RequireRole("Admin");           // Exige que l'utilisateur soit dans le rôle "Admin"
    });
});


var app = builder.Build();

// Run SeedRolesAndAdminAsync at application startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Call the SeedData method to initialize roles and the admin user
        await SeedData.SeedRolesAndAdminAsync(services, builder.Configuration);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<TokenValidationMiddleware>();
app.MapControllers();

app.Run();
