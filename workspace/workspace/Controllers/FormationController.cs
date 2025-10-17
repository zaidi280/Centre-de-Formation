using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workspace.DTO;
using workspace.Models;
using workspace.Models.Services;
using System.Threading.Tasks;
using System.Text.Json;

namespace workspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormationController : ControllerBase
    {
        private readonly IFormationService formationService;
        private readonly IEnseignantService enseignantService;
        private readonly IEtudiantService etudiantService;
        private readonly IMatiereService matiereService;

        public FormationController(IFormationService formationService,
            IEnseignantService enseignantService, IEtudiantService etudiantService, IMatiereService matiereService)
        {
            this.formationService = formationService;
            this.enseignantService = enseignantService;
            this.etudiantService = etudiantService;
            this.matiereService = matiereService;
        }
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour ajouter une nouvelle formation
        [HttpPost("AddFormation")]
        public async Task<IActionResult> AddFormation(FormationDTO formationDTO)
        {
            // Créer une nouvelle formation
            var formation = new Formation
            {
                Titre = formationDTO.Titre,
                Description = formationDTO.Description,
                Duree = formationDTO.Duree,
                Prix = formationDTO.Prix,
                DateDebut = formationDTO.DateDebut,
                DateFin = formationDTO.DateFin,
            };

            // Ajouter la formation
            var addedFormation = await formationService.AddFormationAsync(formation);

            // Récupérer et associer les enseignants
            var enseignants = new List<object>();
            foreach (var enseignantNom in formationDTO.Enseignants)
            {
                var enseignant = await enseignantService.GetEnseignantByNameAsync(enseignantNom);
                if (enseignant != null)
                {
                    await formationService.AddEnseignantFormation(addedFormation.IdFormation, enseignant.IdEnseignant);
                    enseignants.Add(new { enseignantNom });
                }
            }

            // Récupérer et associer les étudiants
            var etudiants = new List<object>();
            foreach (var etudiantNom in formationDTO.Etudiants)
            {
                var etudiant = await etudiantService.GetEtudiantByNameAsync(etudiantNom);
                if (etudiant != null)
                {
                    await formationService.AddEtudiantFormation(addedFormation.IdFormation, etudiant.IdEtudiant);
                    etudiants.Add(new { etudiantNom });
                }
            }

            // Récupérer et associer les matières
            var matieres = new List<object>();
            foreach (var matiereNom in formationDTO.Matieres)
            {
                var matiere = await matiereService.GetMatiereByNameAsync(matiereNom);
                if (matiere != null)
                {
                    await formationService.AddMatiereFormation(addedFormation.IdFormation, matiere.IdMatiere);
                    matieres.Add(new { matiereNom });
                }
            }

            // Réponse finale
            return Ok(new
            {
                Message = "Formation créée et associations effectuées avec succès.",
                Formation = new
                {
                    addedFormation.IdFormation,
                    addedFormation.Titre,
                    addedFormation.Description,
                    Enseignants = enseignants,
                    Etudiants = etudiants,
                    Matieres = matieres
                }
            });
        }
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour mettre à jour une formation
        [HttpPut("UpdateFormation/{idFormation}")]
        public async Task<IActionResult> UpdateFormation(string idFormation, FormationDTO formationDTO)
        {
            // Vérifier si la formation existe
            var existingFormation = await formationService.GetFormationByIdAsync(idFormation);
            if (existingFormation == null)
            {
                return NotFound(new { Message = "Formation non trouvée." });
            }

            // Mettre à jour les détails de la formation
            existingFormation.Titre = formationDTO.Titre;
            existingFormation.Description = formationDTO.Description;
            existingFormation.Duree = formationDTO.Duree;
            existingFormation.Prix = formationDTO.Prix;
            existingFormation.DateDebut = formationDTO.DateDebut;
            existingFormation.DateFin = formationDTO.DateFin;

            await formationService.UpdateFormationAsync(existingFormation);

            // Mettre à jour les enseignants associés
            var enseignants = new List<object>();
            await formationService.ClearEnseignantsFromFormationAsync(idFormation);

            foreach (var enseignantNom in formationDTO.Enseignants)
            {
                var enseignant = await enseignantService.GetEnseignantByNameAsync(enseignantNom);
                if (enseignant != null)
                {
                    await formationService.AddEnseignantFormation(idFormation, enseignant.IdEnseignant);
                    enseignants.Add(new { enseignantNom });
                }
            }

            // Mettre à jour les étudiants associés
            var etudiants = new List<object>();
            await formationService.ClearEtudiantsFromFormationAsync(idFormation);

            foreach (var etudiantNom in formationDTO.Etudiants)
            {
                var etudiant = await etudiantService.GetEtudiantByNameAsync(etudiantNom);
                if (etudiant != null)
                {
                    await formationService.AddEtudiantFormation(idFormation, etudiant.IdEtudiant);
                    etudiants.Add(new { etudiantNom });
                }
            }

            // Mettre à jour les matières associées
            var matieres = new List<object>();
            await formationService.ClearMatieresFromFormationAsync(idFormation);

            foreach (var matiereNom in formationDTO.Matieres)
            {
                var matiere = await matiereService.GetMatiereByNameAsync(matiereNom);
                if (matiere != null)
                {
                    await formationService.AddMatiereFormation(idFormation, matiere.IdMatiere);
                    matieres.Add(new { matiereNom });
                }
            }

            // Réponse finale
            return Ok(new
            {
                Message = "Formation mise à jour avec succès.",
                Formation = new
                {
                    existingFormation.IdFormation,
                    existingFormation.Titre,
                    existingFormation.Description,
                    Enseignants = enseignants,
                    Etudiants = etudiants,
                    Matieres = matieres
                }
            });
        }
        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
        // API pour mettre à jour une formation
        [HttpPut("Inscriptionformetud/{idFormation}")]
        public async Task<IActionResult> Inscriptionformetud(string idFormation, InscriptionformetudDTO inscriptionformetud)
        {
            // Vérifier si la formation existe
            var existingFormation = await formationService.GetFormationByIdAsync(idFormation);
            if (existingFormation == null)
            {
                return NotFound(new { Message = "Formation non trouvée." });
            }

            // Mettre à jour les détails de la formation
            existingFormation.Titre = formationDTO.Titre;
            existingFormation.Description = formationDTO.Description;
            existingFormation.Duree = formationDTO.Duree;
            existingFormation.Prix = formationDTO.Prix;
            existingFormation.DateDebut = formationDTO.DateDebut;
            existingFormation.DateFin = formationDTO.DateFin;

            await formationService.UpdateFormationAsync(existingFormation);

            // Mettre à jour les enseignants associés
            var enseignants = new List<object>();
            await formationService.ClearEnseignantsFromFormationAsync(idFormation);

            foreach (var enseignantNom in formationDTO.Enseignants)
            {
                var enseignant = await enseignantService.GetEnseignantByNameAsync(enseignantNom);
                if (enseignant != null)
                {
                    await formationService.AddEnseignantFormation(idFormation, enseignant.IdEnseignant);
                    enseignants.Add(new { enseignantNom });
                }
            }

            // Mettre à jour les étudiants associés
            var etudiants = new List<object>();
            await formationService.ClearEtudiantsFromFormationAsync(idFormation);

            foreach (var etudiantNom in formationDTO.Etudiants)
            {
                var etudiant = await etudiantService.GetEtudiantByNameAsync(etudiantNom);
                if (etudiant != null)
                {
                    await formationService.AddEtudiantFormation(idFormation, etudiant.IdEtudiant);
                    etudiants.Add(new { etudiantNom });
                }
            }

            // Mettre à jour les matières associées
            var matieres = new List<object>();
            await formationService.ClearMatieresFromFormationAsync(idFormation);

            foreach (var matiereNom in formationDTO.Matieres)
            {
                var matiere = await matiereService.GetMatiereByNameAsync(matiereNom);
                if (matiere != null)
                {
                    await formationService.AddMatiereFormation(idFormation, matiere.IdMatiere);
                    matieres.Add(new { matiereNom });
                }
            }

            // Réponse finale
            return Ok(new
            {
                Message = "Formation mise à jour avec succès.",
                Formation = new
                {
                    existingFormation.IdFormation,
                    existingFormation.Titre,
                    existingFormation.Description,
                    Enseignants = enseignants,
                    Etudiants = etudiants,
                    Matieres = matieres
                }
            });
        }

        // API pour récupérer toutes les formations
        [HttpGet("GetAllFormations")]
        public async Task<ActionResult<IEnumerable<Formation>>> GetAllFormations()
        {
            if (formationService == null)
            {
                return StatusCode(500, "Le service formation n'a pas été initialisé.");
            }

            var formations = await formationService.GetAllFormationsAsync();
            if (formations == null || formations.Count == 0)
            {
                return NotFound("Aucune formation trouvée.");
            }


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(formations, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON
        }






        
        // API pour récupérer une formation par son ID
        [HttpGet("GetFormation/{id}")]
        public async Task<IActionResult> GetFormation(string id)
        {
            var formation = await formationService.GetFormationByIdAsync(id);
            if (formation == null)
            {
                return NotFound("Formation non trouvée.");
            }


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            string jsonString = JsonSerializer.Serialize(formation, options);  // Sérialise les données en JSON

            return Ok(jsonString);  // Retourne la chaîne JSON
        }



        [Authorize(Roles = "Admin")] // Vérifie que l'utilisateur a le rôle "Etudiant"
                                     // API pour supprimer une formation par son ID
        [HttpDelete("DeleteFormation/{id}")]
        public async Task<IActionResult> DeleteFormation(string id)
        {
            await formationService.DeleteFormationAsync(id);
            return NoContent();
        }
    }
}
