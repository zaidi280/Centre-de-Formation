namespace workspace.Models.Services
{
    public interface ISalleService
    {
        Task<List<Salle>> GetAllSallesAsync();
        Task<Salle> GetSalleByIdAsync(string id);
        Task<Salle> GetSalleByNameAsync(string nom);
        Task<Salle> AddSalleAsync(Salle salle);
        Task UpdateSalleAsync(Salle salle);
        Task DeleteSalleAsync(string id);
    }
}
