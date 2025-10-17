using Microsoft.EntityFrameworkCore; // Nécessaire pour ToListAsync, FindAsync, etc.
using workspace.Models; // Nécessaire pour accéder au modèle Enseignant
using workspace.Models.Repository; // Si IRepository se trouve dans ce namespace


namespace workspace.Repository
{
    public class EnseignantRepository : IRepository<Enseignant>
    {
        private readonly WorkspaceContext context;

        public EnseignantRepository(WorkspaceContext context)
        {
            this.context = context;
        }

        public async Task<List<Enseignant>> GetAllAsync()
        {
            List<Enseignant> enseignants = await context.Enseignants
                                             .Include(e => e.User) // Inclure la relation User
                                             .ToListAsync();
            return enseignants;
        }

        public async Task<Enseignant> GetByIdAsync(string id)
        {
            Enseignant? enseignant = await context.Enseignants.FindAsync(id);
            return enseignant;
        }
        public async Task<Enseignant> GetByNameAsync(string name)
        {

            return await context.Enseignants
                       .FirstOrDefaultAsync(c => c.User.UserName == name); // Recherche par le champ Chapitre

        }








        public async Task<Enseignant> AddAsync(Enseignant enseignant)
        {
            var result = await context.Enseignants.AddAsync(enseignant);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync(Enseignant enseignant)
        {
            context.Enseignants.Update(enseignant);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var enseignant = await GetByIdAsync(id);
            if (enseignant != null)
            {
                context.Enseignants.Remove(enseignant);
                await context.SaveChangesAsync();
            }
        }
    }
}
