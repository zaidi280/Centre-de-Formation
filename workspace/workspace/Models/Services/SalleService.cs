using workspace.Models.Repository;

namespace workspace.Models.Services
{
    public class SalleService : ISalleService
    {
        private readonly IRepository<Salle> salleRepository;

        // Constructeur qui reçoit une instance de IRepository<Etudiant>
        public SalleService(IRepository<Salle> salleRepository)
        {
            this.salleRepository = salleRepository ?? throw new ArgumentNullException(nameof(salleRepository));
        }

        // Méthode pour obtenir tous les etudiants
        public async Task<List<Salle>> GetAllSallesAsync()
        {
            if (salleRepository == null)
            {
                throw new InvalidOperationException("Le repository des salles est nul.");
            }

            return await salleRepository.GetAllAsync();
        }

        // Méthode pour obtenir un Etudiant par ID
        public async Task<Salle> GetSalleByIdAsync(string id)
        {
            return await salleRepository.GetByIdAsync(id);
        }

        public async Task<Salle> GetSalleByNameAsync(string nom)
        {
            return await salleRepository.GetByNameAsync(nom);
        }

        // Méthode pour ajouter un nouvel etudiant
        public async Task<Salle> AddSalleAsync(Salle salle)
        {
            return await salleRepository.AddAsync(salle);
        }

        // Méthode pour mettre à jour les informations d'un etudiant
        public async Task UpdateSalleAsync(Salle salle)
        {
            await salleRepository.UpdateAsync(salle);
        }

        // Méthode pour supprimer un etudiant
        public async Task DeleteSalleAsync(string id)
        {
            await salleRepository.DeleteAsync(id);
        }
    }
}
