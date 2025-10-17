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
    public class CourController : ControllerBase
    {
        private readonly ICourService courService;
      
        private readonly IMatiereService matiereService;
        private readonly IEnseignantService enseignantService;
        // Injection du service EnseignantService via l'interface IEnseignantService
        public CourController(ICourService courService, IMatiereService matiereService, IEnseignantService enseignantService)
        {
            this.courService = courService;

            this.matiereService = matiereService;
            this.enseignantService = enseignantService;
        }
        [Authorize(Roles = "Enseignant")]
        [HttpPost("AddCour")]
        public async Task<IActionResult> AddCour(CourDTO courDTO)
        {
            // Recherche de MatiereId
            // Recherche de MatiereId
            var matiere = await matiereService.GetMatiereByNameAsync(courDTO.NomMatiere);
            if (matiere == null)
            {
                return BadRequest(new { Message = "La matière spécifiée n'existe pas." });
            }

            // Recherche de EnseignantId
            var enseignant = await enseignantService.GetEnseignantByNameAsync(courDTO.EnseignantNom);
            if (enseignant == null)
            {
                return BadRequest(new { Message = "L'enseignant spécifié n'existe pas." });
            }

          


            // Création de l'entité Cour
            var cour = new Cour
            {
                IdCour = Guid.NewGuid().ToString(), // Génération automatique de l'ID
                MatiereId = matiere.IdMatiere, // Associe l'ID de la matière trouvée
                EnseignantId = enseignant.IdEnseignant, // Associe l'ID de l'enseignant trouvé
                Chapitre = courDTO.Chapitre,
                Description = courDTO.Description,
                DateHeure = courDTO.DateHeure,
     
            };

            // Ajout et sauvegarde dans la base de données
            // Appel au service pour ajouter le cours
            await courService.AddCourAsync(cour);
            return Ok(new
            {
                Message = "Cour créé avec succès",
                Cour = new
                {
                    cour.IdCour,
                    cour.Chapitre,
                    cour.Description,
                    cour.DateHeure
                }
            });
        }

        // API pour récupérer tous les enseignants
        [Authorize(Roles = "Enseignant")] // Vérifie que l'utilisateur a le rôle "Enseignant"
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "admin"
        [HttpGet("GetAllCours")]
        public async Task<ActionResult<IEnumerable<Cour>>> GetAllCours()
        {
            if (courService == null)
            {
                return StatusCode(500, "Le service cour n'a pas été initialisé.");
            }

            var cours = await courService.GetAllCoursAsync();
            if (cours == null || cours.Count == 0)
            {
                return NotFound("Aucun cour trouvé.");
            }



            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(cours, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON


        }

        // API pour récupérer un enseignant par son ID
        [HttpGet("GetCour/{id}")]
        public async Task<IActionResult> GetCour(string id)
        {
            var cour = await courService.GetCourByIdAsync(id);
            if (cour == null)
            {
                return NotFound("cour non trouvé.");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(cour, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON

        }



        [Authorize(Roles = "Enseignant")] // Vérifie que l'utilisateur a le rôle "Enseignant"
        [HttpPut("UpdateCour/{id}")]
        public async Task<IActionResult> UpdateCour(string id, [FromBody] CourDTO courDTO)
        {

            // Recherche de la matière par nom
            var matiere = await matiereService.GetMatiereByNameAsync(courDTO.NomMatiere);
            if (matiere == null)
            {
                return BadRequest(new { Message = "La matière spécifiée n'existe pas." });
            }

            // Recherche de l'enseignant par nom
            var enseignant = await enseignantService.GetEnseignantByNameAsync(courDTO.EnseignantNom);
            if (enseignant == null)
            {
                return BadRequest(new { Message = "L'enseignant spécifié n'existe pas." });
            }

      

            // Récupération du cours existant à partir de l'ID

            var cour = await courService.GetCourByNameAsync(courDTO.Chapitre);
            if (cour == null)
            {
                return NotFound("Le cours n'a pas été trouvé.");
            }

            // Mise à jour des propriétés du cours avec les nouvelles valeurs
            cour.Chapitre = courDTO.Chapitre;
            cour.Description = courDTO.Description;
            cour.DateHeure = courDTO.DateHeure;
            cour.MatiereId = matiere.IdMatiere;
            cour.EnseignantId = enseignant.IdEnseignant;


            // Appel au service pour mettre à jour le cours
            await courService.UpdateCourAsync(cour);

            return NoContent();
        }









        [Authorize(Roles = "Enseignant")] // Vérifie que l'utilisateur a le rôle "Admin"
        // API pour supprimer un enseignant par son ID
        [HttpDelete("DeleteCour/{id}")]
        public async Task<IActionResult> DeleteCour(string id)
        {
            await courService.DeleteCourAsync(id);
            return NoContent();
        }
    }
}
