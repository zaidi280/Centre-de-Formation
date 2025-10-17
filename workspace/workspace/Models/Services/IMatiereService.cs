namespace workspace.Models.Services
{
    public interface IMatiereService
    {
        Task<List<Matiere>> GetAllMatieresAsync();
        Task<Matiere> GetMatiereByIdAsync(string id);
        Task<Matiere> GetMatiereByNameAsync(string nom);
        Task<Matiere> AddMatiereAsync(Matiere matiere);
        Task UpdateMatiereAsync(Matiere matiere);
        Task DeleteMatiereAsync(string id);
    }
}
