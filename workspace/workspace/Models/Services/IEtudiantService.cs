namespace workspace.Models.Services
{
    public interface IEtudiantService
    {
        Task<List<Etudiant>> GetAllEtudiantsAsync();
        Task<Etudiant> GetEtudiantByIdAsync(string id);
        Task<Etudiant> GetEtudiantByNameAsync(string nom);
        Task<Etudiant> AddEtudiantAsync(Etudiant etudiant);
        Task UpdateEtudiantAsync(Etudiant etudiant);
        Task DeleteEtudiantAsync(string id);
    }
}
