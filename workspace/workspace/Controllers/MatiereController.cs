using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using workspace.Models.Services;
using workspace.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using workspace.DTO;

namespace workspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatiereController : ControllerBase
    {
        private readonly IMatiereService matiereService;
        private readonly ISalleService salleService;

        // Injection du service EnseignantService via l'interface IEnseignantService
        public MatiereController(IMatiereService matiereService, ISalleService salleService)
        {
            this.matiereService = matiereService;
            this.salleService = salleService;
        }



        // API pour récupérer tous les enseignants
     
        [HttpGet("GetAllMatieres")]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetAllMatieres()
        {
            if (matiereService == null)
            {
                return StatusCode(500, "Le service matiere n'a pas été initialisé.");
            }

            var matieres = await matiereService.GetAllMatieresAsync();
            if (matieres == null || matieres.Count == 0)
            {
                return NotFound("Aucun salles trouvé.");
            }



            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(matieres, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON


        }

        // API pour récupérer un etudiant par son ID
        [HttpGet("GetMatiere/{id}")]
        public async Task<IActionResult> GetMatiere(string id)
        {
            var matiere = await matiereService.GetMatiereByIdAsync(id);
            if (matiere == null)
            {
                return NotFound("matiere non trouvé.");
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(matiere, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON

        }





        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour ajouter un nouvel enseignant
        
        
        [HttpPost("AddMatiere")]
        public async Task<IActionResult> AddMatiere(MatiereDTO matiereDTO)
        {
            // Recherche de MatiereId par le nom de la matière
            var matiere = await matiereService.GetMatiereByNameAsync(matiereDTO.NomMatiere);

            // Vérification si la matière existe déjà
            if (matiere != null)
            {
                return BadRequest(new { Message = "La matière existe déjà." });
            }
            var Salle = await salleService.GetSalleByNameAsync(matiereDTO.NomSalle);

            if (Salle == null)
            {
                return BadRequest(new { Message = "La salle n'existe pas." });
            }


            // Création de l'entité Matiere
            var newMatiere = new Matiere
            {
                NomMatiere = matiereDTO.NomMatiere,
                Description = matiereDTO.Description,
                VolumeHoraire = matiereDTO.VolumeHoraire,
                SalleId=Salle.IdSalle,
            };

            // Ajout et sauvegarde de la nouvelle matière dans la base de données
            await matiereService.AddMatiereAsync(newMatiere);

            // Retour d'une réponse avec les informations de la nouvelle matière
            return Ok(new
            {
                Message = "Matière créée avec succès",
                Matiere = new
                {
                    newMatiere.IdMatiere,  // Utilisation de newMatiere au lieu de matiere
                    newMatiere.NomMatiere,
                    newMatiere.Description,
                    newMatiere.VolumeHoraire,
                    SalleId = Salle.IdSalle,
                }
            });
        }





        // API pour mettre à jour une matière existante
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Admin"
        [HttpPut("UpdateMatiere/{id}")]
        public async Task<IActionResult> UpdateMatiere(string id, MatiereDTO matiereDTO)
        {
            // Vérification si la matière existe déjà
            var matiere = await matiereService.GetMatiereByIdAsync(id);

            if (matiere == null)
            {
                return NotFound(new { Message = "La matière spécifiée n'a pas été trouvée." });
            }
            var Salle = await salleService.GetSalleByNameAsync(matiereDTO.NomSalle);

            if (Salle == null)
            {
                return BadRequest(new { Message = "La salle n'existe pas." });
            }
            // Mise à jour des propriétés de la matière
            matiere.NomMatiere = matiereDTO.NomMatiere;
            matiere.Description = matiereDTO.Description;
            matiere.VolumeHoraire = matiereDTO.VolumeHoraire;
            matiere.SalleId=Salle.IdSalle;

            // Appel au service pour mettre à jour la matière
            await matiereService.UpdateMatiereAsync(matiere);

            return Ok(new
            {
                Message = "Matière mise à jour avec succès",
                Matiere = new
                {
                    matiere.IdMatiere,
                    matiere.NomMatiere,
                    matiere.Description,
                    matiere.VolumeHoraire,
                    SalleId = Salle.IdSalle,
                }
            });
        }







        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour supprimer un enseignant par son ID
        [HttpDelete("DeleteMatiere/{id}")]
        public async Task<IActionResult> DeleteMatiere(string id)
        {
            await matiereService.DeleteMatiereAsync(id);
            return NoContent();
        }
    }
}
