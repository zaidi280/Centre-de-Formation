using workspace.Models.Repository;

namespace workspace.Models.Services
{
    public class EtudiantService : IEtudiantService
    {
        private readonly IRepository<Etudiant> etudiantRepository;

        // Constructeur qui reçoit une instance de IRepository<Etudiant>
        public EtudiantService(IRepository<Etudiant> etudiantRepository)
        {
            this.etudiantRepository = etudiantRepository ?? throw new ArgumentNullException(nameof(etudiantRepository));
        }

        // Méthode pour obtenir tous les etudiants
        public async Task<List<Etudiant>> GetAllEtudiantsAsync()
        {
            if (etudiantRepository == null)
            {
                throw new InvalidOperationException("Le repository des enseignants est nul.");
            }

            return await etudiantRepository.GetAllAsync();
        }

        // Méthode pour obtenir un Etudiant par ID
        public async Task<Etudiant> GetEtudiantByIdAsync(string id)
        {
            return await etudiantRepository.GetByIdAsync(id);
        }
        public async Task<Etudiant> GetEtudiantByNameAsync(string nom)
        {
            return await etudiantRepository.GetByNameAsync(nom);
        }

        // Méthode pour ajouter un nouvel etudiant
        public async Task<Etudiant> AddEtudiantAsync(Etudiant etudiant)
        {
            return await etudiantRepository.AddAsync(etudiant);
        }

        // Méthode pour mettre à jour les informations d'un etudiant
        public async Task UpdateEtudiantAsync(Etudiant etudiant)
        {
            await etudiantRepository.UpdateAsync(etudiant);
        }

        // Méthode pour supprimer un etudiant
        public async Task DeleteEtudiantAsync(string id)
        {
            await etudiantRepository.DeleteAsync(id);
        }
    }
}
