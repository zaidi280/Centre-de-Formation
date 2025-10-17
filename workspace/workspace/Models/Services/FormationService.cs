using Microsoft.EntityFrameworkCore;
using workspace.Models.Repository;

namespace workspace.Models.Services
{
    public class FormationService : IFormationService
    {
        private readonly IRepository<Formation> formationRepository;
        private readonly WorkspaceContext context; // Ajout du DbContext

        // Constructeur qui reçoit une instance de IRepository<Formation> et ApplicationDbContext
        public FormationService(IRepository<Formation> formationRepository, WorkspaceContext context)
        {
            this.formationRepository = formationRepository ?? throw new ArgumentNullException(nameof(formationRepository));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Méthode pour obtenir toutes les formations
        public async Task<List<Formation>> GetAllFormationsAsync()
        {
            return await formationRepository.GetAllAsync();
        }

        // Méthode pour obtenir une Formation par ID
        public async Task<Formation> GetFormationByIdAsync(string id)
        {
            return await formationRepository.GetByIdAsync(id);
        }

        // Méthode pour obtenir une Formation par nom
        public async Task<Formation> GetFormationByNameAsync(string nom)
        {
            return await formationRepository.GetByNameAsync(nom);
        }

        // Méthode pour ajouter une nouvelle formation
        public async Task<Formation> AddFormationAsync(Formation formation)
        {
            return await formationRepository.AddAsync(formation);
        }

        // Méthode pour mettre à jour les informations d'une formation
        public async Task UpdateFormationAsync(Formation formation)
        {
            await formationRepository.UpdateAsync(formation);
        }

        // Méthode pour supprimer une formation
        public async Task DeleteFormationAsync(string id)
        {
            await formationRepository.DeleteAsync(id);
        }

        // Méthode pour ajouter un enseignant à la formation
        public async Task AddEnseignantFormation(string formationId, string enseignantId)
        {
            // Récupérer la formation par son ID
            var formation = await context.Formations.Include(f => f.ListeEnseignants)
                                                      .FirstOrDefaultAsync(f => f.IdFormation == formationId);
            if (formation == null)
            {
                throw new Exception("Formation non trouvée.");
            }

            // Récupérer l'enseignant par son ID
            var enseignant = await context.Enseignants.FirstOrDefaultAsync(e => e.IdEnseignant == enseignantId);
            if (enseignant == null)
            {
                throw new Exception("Enseignant non trouvé.");
            }

            // Ajouter l'enseignant à la liste des enseignants
            formation.ListeEnseignants.Add(enseignant);

            // Sauvegarder les modifications
            await context.SaveChangesAsync();
        }

        // Méthode pour ajouter un étudiant à la formation
        public async Task AddEtudiantFormation(string formationId, string etudiantId)
        {
            // Récupérer la formation par son ID
            var formation = await context.Formations.Include(f => f.ListeEtudiants)
                                                      .FirstOrDefaultAsync(f => f.IdFormation == formationId);
            if (formation == null)
            {
                throw new Exception("Formation non trouvée.");
            }

            // Récupérer l'étudiant par son ID
            var etudiant = await context.Etudiants.FirstOrDefaultAsync(e => e.IdEtudiant == etudiantId);
            if (etudiant == null)
            {
                throw new Exception("Étudiant non trouvé.");
            }

            // Ajouter l'étudiant à la liste des étudiants
            formation.ListeEtudiants.Add(etudiant);

            // Sauvegarder les modifications
            await context.SaveChangesAsync();
        }

        // Méthode pour ajouter une matière à la formation
        public async Task AddMatiereFormation(string formationId, string matiereId)
        {
            // Récupérer la formation par son ID
            var formation = await context.Formations.Include(f => f.ListeMatieres)
                                                      .FirstOrDefaultAsync(f => f.IdFormation == formationId);
            if (formation == null)
            {
                throw new Exception("Formation non trouvée.");
            }

            // Récupérer la matière par son ID
            var matiere = await context.Matieres.FirstOrDefaultAsync(m => m.IdMatiere == matiereId);
            if (matiere == null)
            {
                throw new Exception("Matière non trouvée.");
            }
            // Ajouter la matière à la liste des matières
            formation.ListeMatieres.Add(matiere);

            // Sauvegarder les modifications
            await context.SaveChangesAsync();
        }


        public async Task ClearEnseignantsFromFormationAsync(string formationId)
        {
            // Récupérer la formation par son ID
            var formation = await context.Formations.Include(f => f.ListeEnseignants)
                                                      .FirstOrDefaultAsync(f => f.IdFormation == formationId);
            if (formation == null)
            {
                throw new Exception("Formation non trouvée.");
            }

            // Vider la liste des enseignants de la formation
            formation.ListeEnseignants.Clear();

            // Sauvegarder les modifications
            await context.SaveChangesAsync();
        }

        public async Task ClearEtudiantsFromFormationAsync(string formationId)
        {
            // Récupérer la formation par son ID
            var formation = await context.Formations.Include(f => f.ListeEtudiants)
                                                      .FirstOrDefaultAsync(f => f.IdFormation == formationId);
            if (formation == null)
            {
                throw new Exception("Formation non trouvée.");
            }

            // Vider la liste des étudiants de la formation
            formation.ListeEtudiants.Clear();

            // Sauvegarder les modifications
            await context.SaveChangesAsync();
        }


        public async Task ClearMatieresFromFormationAsync(string formationId)
        {
            // Récupérer la formation par son ID
            var formation = await context.Formations.Include(f => f.ListeMatieres)
                                                      .FirstOrDefaultAsync(f => f.IdFormation == formationId);
            if (formation == null)
            {
                throw new Exception("Formation non trouvée.");
            }

            // Vider la liste des matières de la formation
            formation.ListeMatieres.Clear();

            // Sauvegarder les modifications
            await context.SaveChangesAsync();
        }


    }
}