using workspace.Models.Repository;

namespace workspace.Models.Services
{
    public class CourService : ICourService
    {
        private readonly IRepository<Cour> courRepository; // Utilisez un attribut privé

        // Constructeur qui reçoit une instance de IRepository<Enseignant>
        public CourService(IRepository<Cour> courRepository)
        {
            this.courRepository = courRepository ?? throw new ArgumentNullException(nameof(courRepository));
        }

        // Méthode pour obtenir tous les enseignants
        public async Task<List<Cour>> GetAllCoursAsync()
        {
            if (courRepository == null)
            {
                throw new InvalidOperationException("Le repository des cours est nul.");
            }

            return await courRepository.GetAllAsync();
        }

        // Méthode pour obtenir un enseignant par ID
        public async Task<Cour> GetCourByIdAsync(string id)
        {
            return await courRepository.GetByIdAsync(id);
        }
        public async Task<Cour> GetCourByNameAsync(string nom)
        {
            return await courRepository.GetByNameAsync(nom);
        }

        // Méthode pour ajouter un nouvel enseignant
        public async Task<Cour> AddCourAsync(Cour cour)
        {
            return await courRepository.AddAsync(cour);
        }

        // Méthode pour mettre à jour les informations d'un enseignant
        public async Task UpdateCourAsync(Cour cour)
        {
            await courRepository.UpdateAsync(cour);
        }

        // Méthode pour supprimer un enseignant
        public async Task DeleteCourAsync(string id)
        {
            await courRepository.DeleteAsync(id);
        }
    }
}

