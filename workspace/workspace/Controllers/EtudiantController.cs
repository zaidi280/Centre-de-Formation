using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using workspace.Models.Services;
using workspace.Models;
using workspace.DTO;

namespace workspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtudiantController : ControllerBase
    {
        private readonly IEtudiantService etudiantService;

        // Injection du service EnseignantService via l'interface IEnseignantService
        public EtudiantController(IEtudiantService etudiantService)
        {
            this.etudiantService = etudiantService;
        }



        // API pour récupérer tous les enseignants
        //[Authorize(Roles = "Enseignant")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        //[Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        [HttpGet("GetAllEtudiants")]
        public async Task<ActionResult<IEnumerable<Etudiant>>> GetAllEtudiants()
        {
            if (etudiantService == null)
            {
                return StatusCode(500, "Le service etudiant n'a pas été initialisé.");
            }

            var etudiants = await etudiantService.GetAllEtudiantsAsync();
            if (etudiants == null || etudiants.Count == 0)
            {
                return NotFound("Aucun etudiant trouvé.");
            }



            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(etudiants, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON


        }

        // API pour récupérer un etudiant par son ID
        [HttpGet("GetEtudiant/{id}")]
        public async Task<IActionResult> GetEtudiant(string id)
        {
            var etudiant = await etudiantService.GetEtudiantByIdAsync(id);
            if (etudiant == null)
            {
                return NotFound("Etudiant non trouvé.");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(etudiant, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON

        }
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
    
        [Authorize(Roles = "Etudiant")] // Vérifie que l'utilisateur a le rôle "Admin"
        // API pour mettre à jour un étudiant existant
        [HttpPut("UpdateEtudiant/{id}")]
        public async Task<IActionResult> UpdateEtudiant(string id, [FromBody] CreerEtudiantDTO etudiantDTO)
        {
          
            // Récupérer l'étudiant existant à partir de l'ID
            var etudiant = await etudiantService.GetEtudiantByIdAsync(id);
            if (etudiant == null)
            {
                return NotFound("Étudiant non trouvé.");
            }

            // Mapper les données du DTO à l'étudiant existant
            etudiant.User.Email = etudiantDTO.Email;
            etudiant.User.Nom = etudiantDTO.Nom;
            etudiant.User.Prenom = etudiantDTO.Prenom;
            etudiant.User.Telephone = etudiantDTO.Telephone;
            etudiant.User.Adresse = etudiantDTO.Adresse;
            etudiant.Classe = etudiantDTO.Classe;
            etudiant.Niveau = etudiantDTO.Niveau;
            etudiant.DateInscription = etudiantDTO.DateInscription;

            // Mettre à jour l'étudiant dans le service
            await etudiantService.UpdateEtudiantAsync(etudiant);

            return NoContent(); // Retourne une réponse vide (204 No Content) en cas de succès
        }


        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour supprimer un enseignant par son ID
        [HttpDelete("DeleteEtudiant/{id}")]
        public async Task<IActionResult> DeleteEtudiant(string id)
        {
            await etudiantService.DeleteEtudiantAsync(id);
            return NoContent();
        }
    }
}
