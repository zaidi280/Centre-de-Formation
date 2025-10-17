using Microsoft.AspNetCore.Mvc;
using workspace.Models;

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using workspace.Models.Services;
using System.Text.Json;
using workspace.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using workspace.DTO;
using Microsoft.EntityFrameworkCore;

namespace workspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnseignantController : ControllerBase
    {
        private readonly IEnseignantService enseignantService;

        // Injection du service EnseignantService via l'interface IEnseignantService
        public EnseignantController(IEnseignantService enseignantService)
        {
            this.enseignantService = enseignantService;
        }



        // API pour récupérer tous les enseignants
        //[Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        [HttpGet("GetAllEnseignants")]
        public async Task<ActionResult<IEnumerable<Enseignant>>> GetAllEnseignants()
        {
            if (enseignantService == null)
            {
                return StatusCode(500, "Le service enseignant n'a pas été initialisé.");
            }

            var enseignants = await enseignantService.GetAllEnseignantsAsync();
            if (enseignants == null || enseignants.Count == 0)
            {
                return NotFound("Aucun enseignant trouvé.");
            }



            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(enseignants, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON


        }
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        // API pour récupérer un enseignant par son ID
        [HttpGet("GetEnseignant/{id}")]
        public async Task<IActionResult> GetEnseignant(string id)
        {
            var enseignant = await enseignantService.GetEnseignantByIdAsync(id);
            if (enseignant == null)
            {
                return NotFound("Enseignant non trouvé.");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(enseignant, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON

        }

       
        // API pour mettre à jour un enseignant existant
        [HttpPut("UpdateEnseignant/{id}")]
        public async Task<IActionResult> UpdateEnseignant(string id, [FromBody] CreerEnseignantDTO enseignantDTO)
        {
            

            // Récupérer l'enseignant existant à partir de l'ID
            var enseignant = await enseignantService.GetEnseignantByIdAsync(id);
            if (enseignant == null)
            {
                return NotFound("Enseignant non trouvé.");
            }

            // Mapper les données du DTO à l'enseignant existant
            enseignant.User.Email = enseignantDTO.Email;
            enseignant.User.Nom = enseignantDTO.Nom;
            enseignant.User.Prenom = enseignantDTO.Prenom;
            enseignant.User.Telephone = enseignantDTO.Telephone;
            enseignant.User.Adresse = enseignantDTO.Adresse;
            enseignant.Specialite = enseignantDTO.Specialite;
            enseignant.AnneesExperience = enseignantDTO.AnneesExperience;
            enseignant.Diplome = enseignantDTO.Diplome;
            enseignant.DateEmbauche = enseignantDTO.DateEmbauche;

            // Mettre à jour l'enseignant dans le service
            await enseignantService.UpdateEnseignantAsync(enseignant);

            return NoContent(); // Retourne une réponse vide (204 No Content) en cas de succès
        }




        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        // API pour supprimer un enseignant par son ID
        [HttpDelete("DeleteEnseignant/{id}")]
        public async Task<IActionResult> DeleteEnseignant(string id)
        {
            await enseignantService.DeleteEnseignantAsync(id);
            return NoContent();
        }
    }
}
