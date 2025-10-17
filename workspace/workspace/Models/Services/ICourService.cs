namespace workspace.Models.Services
{
    public interface ICourService
    {
        Task<List<Cour>> GetAllCoursAsync();
        Task<Cour> GetCourByIdAsync(string id);
        Task<Cour> GetCourByNameAsync(string nom);
        Task<Cour> AddCourAsync(Cour cour);
        Task UpdateCourAsync(Cour cour);
        Task DeleteCourAsync(string id);
    }
}
