namespace workspace.Models.Services
{
    public interface IFormationService
    {
        Task<List<Formation>> GetAllFormationsAsync();
        Task<Formation> GetFormationByIdAsync(string id);
        Task<Formation> AddFormationAsync(Formation formation);
        Task<Formation> GetFormationByNameAsync(string nom);
        Task UpdateFormationAsync(Formation formation);
        Task DeleteFormationAsync(string id);
        // Ajouter un enseignant à une formation
        Task AddEnseignantFormation(string formationId, string enseignantId);

        // Ajouter un étudiant à une formation
        Task AddEtudiantFormation(string formationId, string etudiantId);

        // Ajouter une matière à une formation
        Task AddMatiereFormation(string formationId, string matiereId);
        // New method signatures
        Task ClearEnseignantsFromFormationAsync(string formationId);
        Task ClearEtudiantsFromFormationAsync(string formationId);
        Task ClearMatieresFromFormationAsync(string formationId);
    }
}
