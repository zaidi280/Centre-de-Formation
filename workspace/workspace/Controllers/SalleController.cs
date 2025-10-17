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
    public class SalleController : ControllerBase
    {
        private readonly ISalleService salleService;

        // Injection du service EnseignantService via l'interface IEnseignantService
        public SalleController(ISalleService salleService)
        {
            this.salleService = salleService;
        }



        // API pour récupérer tous les enseignants

        [HttpGet("GetAllSalles")]
        public async Task<ActionResult<IEnumerable<Salle>>> GetAllSalles()
        {
            if (salleService == null)
            {
                return StatusCode(500, "Le service etudiant n'a pas été initialisé.");
            }

            var salles = await salleService.GetAllSallesAsync();
            if (salles == null || salles.Count == 0)
            {
                return NotFound("Aucun salles trouvé.");
            }



            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(salles, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON


        }

        // API pour récupérer un etudiant par son ID
        [HttpGet("GetSalle/{id}")]
        public async Task<IActionResult> GetSalle(string id)
        {
            var salle = await salleService.GetSalleByIdAsync(id);
            if (salle == null)
            {
                return NotFound("Salle non trouvé.");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(salle, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON

        }
        // API pour ajouter une nouvelle salle
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        [HttpPost("AddSalle")]
        public async Task<IActionResult> AddSalle(SalleDTO salleDTO)
        {
            // Vérifier si une salle avec le même nom existe déjà
            var salle = await salleService.GetSalleByNameAsync(salleDTO.NomSalle);
            if (salle != null)
            {
                return BadRequest(new { Message = "Une salle avec ce nom existe déjà." });
            }

            // Créer une nouvelle salle à partir des données du DTO
            var newSalle = new Salle
            {
                NomSalle = salleDTO.NomSalle,
                Capacite = salleDTO.Capacite,
                TypeSalle = salleDTO.TypeSalle,
                Equipement = salleDTO.Equipement
            };

            // Appeler le service pour ajouter la salle dans la base de données
            await salleService.AddSalleAsync(newSalle);

            return Ok(new
            {
                Message = "Salle ajoutée avec succès",
                Salle = new
                {
                    newSalle.IdSalle,
                    newSalle.NomSalle,
                    newSalle.Capacite,
                    newSalle.TypeSalle,
                    newSalle.Equipement
                }
            });
        }

        // API pour mettre à jour une salle existante
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        [HttpPut("UpdateSalle/{id}")]
        public async Task<IActionResult> UpdateSalle(string id, SalleDTO salleDTO)
        {
            // Vérifier si la salle existe déjà
            var salle = await salleService.GetSalleByIdAsync(id);
            if (salle == null)
            {
                return NotFound(new { Message = "La salle spécifiée n'a pas été trouvée." });
            }

            // Mise à jour des propriétés de la salle
            salle.NomSalle = salleDTO.NomSalle;
            salle.Capacite = salleDTO.Capacite;
            salle.TypeSalle = salleDTO.TypeSalle;
            salle.Equipement = salleDTO.Equipement;

            // Appeler le service pour mettre à jour la salle
            await salleService.UpdateSalleAsync(salle);

            return Ok(new
            {
                Message = "Salle mise à jour avec succès",
                Salle = new
                {
                    salle.IdSalle,
                    salle.NomSalle,
                    salle.Capacite,
                    salle.TypeSalle,
                    salle.Equipement
                }
            });
        }
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour supprimer un enseignant par son ID
        [HttpDelete("DeleteSalle/{id}")]
        public async Task<IActionResult> DeleteSalle(string id)
        {
            await salleService.DeleteSalleAsync(id);
            return NoContent();
        }
    }
}

