using Microsoft.EntityFrameworkCore;

namespace workspace.Models.Repository
{
    public class CourRepository : IRepository<Cour>
    {
        private readonly WorkspaceContext context;

        public CourRepository(WorkspaceContext context)
        {
            this.context = context;
        }

        public async Task<List<Cour>> GetAllAsync()
        {
            List<Cour> cours = await context.Cours
                                             .Include(e => e.Matiere)
                                             
                                             .Include(e => e.Enseignant) // Include Enseignant
                                             .ToListAsync();
            return cours;
        }

        public async Task<Cour> GetByIdAsync(string id)
        {
            Cour? cour = await context.Cours.FindAsync(id);
            return cour;
        }
        public async Task<Cour> GetByNameAsync(string name)
        {
            {
                return await context.Cours
                           .FirstOrDefaultAsync(c => c.Chapitre == name); // Recherche par le champ Chapitre
            }

        }
        public async Task<Cour> AddAsync(Cour cour)
        {
            var result = await context.Cours.AddAsync(cour);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync(Cour cour)
        {
            context.Cours.Update(cour);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var cour = await GetByIdAsync(id);
            if (cour != null)
            {
                context.Cours.Remove(cour);
                await context.SaveChangesAsync();
            }
        }
    }
}
