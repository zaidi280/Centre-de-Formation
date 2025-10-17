using workspace.Models.Repository;

namespace workspace.Models.Services
{
    public class MatiereService : IMatiereService
    {
        private readonly IRepository<Matiere> matiereRepository;

        // Constructeur qui reçoit une instance de IRepository<Etudiant>
        public MatiereService(IRepository<Matiere> matiereRepository)
        {
            this.matiereRepository = matiereRepository ?? throw new ArgumentNullException(nameof(matiereRepository));
        }

        // Méthode pour obtenir tous les etudiants
        public async Task<List<Matiere>> GetAllMatieresAsync()
        {
            if (matiereRepository == null)
            {
                throw new InvalidOperationException("Le repository des enseignants est nul.");
            }

            return await matiereRepository.GetAllAsync();
        }

        // Méthode pour obtenir un Etudiant par ID
        public async Task<Matiere> GetMatiereByIdAsync(string id)
        {
            return await matiereRepository.GetByIdAsync(id);
        }

        public async Task<Matiere> GetMatiereByNameAsync(string nom)
        {
            return await matiereRepository.GetByNameAsync(nom);
        }


        // Méthode pour ajouter un nouvel etudiant
        public async Task<Matiere> AddMatiereAsync(Matiere matiere)
        {
            return await matiereRepository.AddAsync(matiere);
        }

        // Méthode pour mettre à jour les informations d'un etudiant
        public async Task UpdateMatiereAsync(Matiere matiere)
        {
            await matiereRepository.UpdateAsync(matiere);
        }

        // Méthode pour supprimer un etudiant
        public async Task DeleteMatiereAsync(string id)
        {
            await matiereRepository.DeleteAsync(id);
        }
    }
}
