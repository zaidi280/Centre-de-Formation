using Microsoft.EntityFrameworkCore;

namespace workspace.Models.Repository
{
    public class EtudiantRepository : IRepository<Etudiant>
    {
        private readonly WorkspaceContext context;

        public EtudiantRepository(WorkspaceContext context)
        {
            this.context = context;
        }

        public async Task<List<Etudiant>> GetAllAsync()
        {
            List<Etudiant> etudiants = await context.Etudiants
                                             .Include(e => e.User) // Inclure la relation User
                                             .ToListAsync();
            return etudiants;
        }

        public async Task<Etudiant> GetByIdAsync(string id)
        {
            Etudiant? etudiant = await context.Etudiants.FindAsync(id);
            return etudiant;
        }
        public async Task<Etudiant> GetByNameAsync(string name)
        {
            return await context.Etudiants
                       .FirstOrDefaultAsync(c => c.User.UserName == name); // Recherche par le champ Chapitre
        }

        public async Task<Etudiant> AddAsync(Etudiant etudiant)
        {
            var result = await context.Etudiants.AddAsync(etudiant);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync(Etudiant etudiant)
        {
            context.Etudiants.Update(etudiant);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var etudiant = await GetByIdAsync(id);
            if (etudiant != null)
            {
                context.Etudiants.Remove(etudiant);
                await context.SaveChangesAsync();
            }
        }
    }
}
