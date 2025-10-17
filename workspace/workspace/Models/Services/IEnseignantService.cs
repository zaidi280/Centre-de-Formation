using workspace.Models;

namespace workspace.Models.Services
{
    public interface IEnseignantService
    {
        Task<List<Enseignant>> GetAllEnseignantsAsync();
        Task<Enseignant> GetEnseignantByNameAsync(string nom);
        Task<Enseignant> GetEnseignantByIdAsync(string id);
        Task<Enseignant> AddEnseignantAsync(Enseignant enseignant);
        Task UpdateEnseignantAsync(Enseignant enseignant);
        Task DeleteEnseignantAsync(string id);
    }
}

