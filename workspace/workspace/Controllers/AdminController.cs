using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using workspace.Models;
using workspace.DTO;
using workspace.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;


namespace workspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Endpoint pour créer un enseignant
        //[Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"

        [HttpPost("CreerEnseignant")]
        public async Task<IActionResult> CreerEnseignant(CreerEnseignantDTO enseignantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifie si le rôle "Enseignant" existe, sinon le crée
            var roleExists = await roleManager.RoleExistsAsync("Enseignant");
            if (!roleExists)
            {
                var role = new ApplicationRole
                {
                    Name = "Enseignant",
                    Description = "Role for teachers"
                };
                await roleManager.CreateAsync(role);
            }

            // Vérifie si un utilisateur avec cet email existe déjà
            var existingUser = await userManager.FindByEmailAsync(enseignantDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("Un utilisateur avec cet email existe déjà.");
            }

            // Crée l'utilisateur ApplicationUser
            var user = new ApplicationUser
            {
                UserName = enseignantDTO.UserName,
                Email = enseignantDTO.Email,
                Nom = enseignantDTO.Nom,
                Prenom = enseignantDTO.Prenom,
                Telephone = enseignantDTO.Telephone,
                Adresse = enseignantDTO.Adresse,
                
            };

            var result = await userManager.CreateAsync(user, enseignantDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Assigne le rôle "Enseignant" à l'utilisateur
            await userManager.AddToRoleAsync(user, "Enseignant");


            // Crée l'entité Enseignant
            var enseignant = new Enseignant
            {
                IdEnseignant = Guid.NewGuid().ToString(), // Génération automatique de l'ID
                Specialite = enseignantDTO.Specialite,
                AnneesExperience = enseignantDTO.AnneesExperience,
                Diplome = enseignantDTO.Diplome,
                DateEmbauche = enseignantDTO.DateEmbauche,
                UserId = user.Id // Associe l'utilisateur à l'enseignant
            };

            // Ajoute l'entité Enseignant à la base de données
            var dbContext = HttpContext.RequestServices.GetService<WorkspaceContext>();
            dbContext.Enseignants.Add(enseignant);
            await dbContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Enseignant créé avec succès",
                Enseignant = new
                {
                    enseignant.IdEnseignant,
                    user.UserName,
                    user.Email,
                    user.Nom,
                    user.Prenom
                }
            });
        }

        [HttpPost("CreerEtudiant")]
        public async Task<IActionResult> CreerEtudiant(CreerEtudiantDTO etudiantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifie si un utilisateur avec cet email existe déjà
            var existingUser = await userManager.FindByEmailAsync(etudiantDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("Un utilisateur avec cet email existe déjà.");
            }

            // Crée l'utilisateur ApplicationUser pour l'étudiant
            var user = new ApplicationUser
            {
                UserName = etudiantDTO.UserName,
                Email = etudiantDTO.Email,
                Nom = etudiantDTO.Nom,
                Prenom = etudiantDTO.Prenom,
                Telephone = etudiantDTO.Telephone,
                Adresse = etudiantDTO.Adresse
            };

            var result = await userManager.CreateAsync(user, etudiantDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Crée l'entité Etudiant
            var etudiant = new Etudiant
            {
                IdEtudiant = Guid.NewGuid().ToString(), // Génération automatique de l'ID
                Classe = etudiantDTO.Classe,
                Niveau = etudiantDTO.Niveau,
                DateInscription = etudiantDTO.DateInscription,
                UserId = user.Id // Associe l'utilisateur à l'étudiant
            };

            // Ajoute l'entité Etudiant à la base de données
            var dbContext = HttpContext.RequestServices.GetService<WorkspaceContext>();
            dbContext.Etudiants.Add(etudiant);
            await dbContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Étudiant créé avec succès",
                Etudiant = new
                {
                    etudiant.IdEtudiant,
                    user.UserName,
                    user.Email,
                    user.Nom,
                    user.Prenom
                }
            });
        }










    }
}
