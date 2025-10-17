using Microsoft.EntityFrameworkCore;

namespace workspace.Models.Repository
{
    public class FormationRepository : IRepository<Formation>
    {
        private readonly WorkspaceContext context;

        public FormationRepository(WorkspaceContext context)
        {
            this.context = context;
        }

        public async Task<List<Formation>> GetAllAsync()
        {
            List<Formation> formations = await context.Formations
                                                 .Include(e => e.ListeEnseignants)
                                                 .Include(e => e.ListeEtudiants)
                                                 .Include(e => e.ListeMatieres)
                                                
                                                 .ToListAsync();
            return formations;
        }

        public async Task<Formation> GetByIdAsync(string id)
        {
            Formation? formation = await context.Formations.FindAsync(id);
            return formation;
        }

        public async Task<Formation> GetByNameAsync(string name)
        {
            return await context.Formations
                           .FirstOrDefaultAsync(f => f.Titre == name); // Recherche par le champ Titre
        }

        public async Task<Formation> AddAsync(Formation formation)
        {
            var result = await context.Formations.AddAsync(formation);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync(Formation formation)
        {
            context.Formations.Update(formation);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var formation = await GetByIdAsync(id);
            if (formation != null)
            {
                context.Formations.Remove(formation);
                await context.SaveChangesAsync();
            }
        }
    }
}
