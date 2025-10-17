using Microsoft.EntityFrameworkCore;

namespace workspace.Models.Repository
{
    public class SalleRepository : IRepository<Salle>
    {
        private readonly WorkspaceContext context;

        public SalleRepository(WorkspaceContext context)
        {
            this.context = context;
        }

        public async Task<List<Salle>> GetAllAsync()
        {
            List<Salle> salles = await context.Salles
                                             .Include(e => e.Matieres) // Inclure la relation User
                                             .ToListAsync();
            return salles;
        }

        public async Task<Salle> GetByIdAsync(string id)
        {
            Salle? salle = await context.Salles.FindAsync(id);
            return salle;
        }

        public async Task<Salle> GetByNameAsync(string name)
        {
            {
                return await context.Salles
                                     .FirstOrDefaultAsync(c => c.NomSalle == name); // Recherche par le champ Chapitre
            }
        }



        public async Task<Salle> AddAsync(Salle salle)
        {
            var result = await context.Salles.AddAsync(salle);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync(Salle salle)
        {
            context.Salles.Update(salle);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var salle = await GetByIdAsync(id);
            if (salle != null)
            {
                context.Salles.Remove(salle);
                await context.SaveChangesAsync();
            }
        }
    }
}
