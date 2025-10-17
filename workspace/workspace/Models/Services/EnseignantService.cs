using workspace.Models.Repository;
using workspace.Models.Services;
using workspace.Models;

public class EnseignantService : IEnseignantService
{
    private readonly IRepository<Enseignant> enseignantRepository; // Utilisez un attribut privé

    // Constructeur qui reçoit une instance de IRepository<Enseignant>
    public EnseignantService(IRepository<Enseignant> enseignantRepository)
    {
        this.enseignantRepository = enseignantRepository ?? throw new ArgumentNullException(nameof(enseignantRepository));
    }

    // Méthode pour obtenir tous les enseignants
    public async Task<List<Enseignant>> GetAllEnseignantsAsync()
    {
        if (enseignantRepository == null)
        {
            throw new InvalidOperationException("Le repository des enseignants est nul.");
        }

        return await enseignantRepository.GetAllAsync();
    }

    // Méthode pour obtenir un enseignant par ID
    public async Task<Enseignant> GetEnseignantByIdAsync(string id)
    {
        return await enseignantRepository.GetByIdAsync(id);
    }
    public async Task<Enseignant> GetEnseignantByNameAsync(string nom)
    {
        return await enseignantRepository.GetByNameAsync(nom);
    }

    // Méthode pour ajouter un nouvel enseignant
    public async Task<Enseignant> AddEnseignantAsync(Enseignant enseignant)
    {
        return await enseignantRepository.AddAsync(enseignant);
    }

    // Méthode pour mettre à jour les informations d'un enseignant
    public async Task UpdateEnseignantAsync(Enseignant enseignant)
    {
        await enseignantRepository.UpdateAsync(enseignant);
    }

    // Méthode pour supprimer un enseignant
    public async Task DeleteEnseignantAsync(string id)
    {
        await enseignantRepository.DeleteAsync(id);
    }
}
