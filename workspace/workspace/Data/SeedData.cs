using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using workspace.Models;

namespace workspace.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();  // Utilisez ApplicationRole ici
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(); // ApplicationUser ici

            // Initialisation des rôles
            string[] roles = { "Admin", "Enseignant", "Etudiant" };
            foreach (var role in roles)
            {
                // Créer un rôle de type ApplicationRole, pas IdentityRole
                var existingRole = await roleManager.FindByNameAsync(role);
                if (existingRole == null)
                {
                    var newRole = new ApplicationRole
                    {
                        Name = role,
                        Description = $"Role for {role.ToLower()}s"
                    };
                    var result = await roleManager.CreateAsync(newRole);
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Rôle {role} créé avec succès.");
                    }
                    else
                    {
                        Console.WriteLine($"Erreur lors de la création du rôle {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            // Création de l'admin par défaut
            var adminEmail = configuration["DefaultAdmin:Email"] ?? "admin@site.com";
            var username = configuration["DefaultAdmin:Username"] ?? "admin";
            var adminPassword = configuration["DefaultAdmin:Password"] ?? "Admin@123";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser  // Utilisez ApplicationUser ici
                {
                    UserName = username,
                    Email = adminEmail,
                    Nom = "Admin",
                    Prenom = "Super",
                };
                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createResult.Succeeded)
                {

                    var claims = new List<Claim>
                {
                    new Claim("IsAdmin", "true")
                };
                    await userManager.AddClaimsAsync(adminUser, claims);



                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Administrateur par défaut créé avec succès.");
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la création de l'administrateur : {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Administrateur par défaut existe déjà.");
            }
        }
    }
}
