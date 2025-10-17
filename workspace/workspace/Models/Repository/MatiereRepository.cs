using Microsoft.EntityFrameworkCore;

namespace workspace.Models.Repository
{
    public class MatiereRepository : IRepository<Matiere>
    {
        private readonly WorkspaceContext context;

        public MatiereRepository(WorkspaceContext context)
        {
            this.context = context;
        }

        public async Task<List<Matiere>> GetAllAsync()
        {
            List<Matiere> matieres = await context.Matieres
                                             .Include(e => e.Cours) // Inclure la relation User
                                             .ToListAsync();
            return matieres;
        }

        public async Task<Matiere> GetByIdAsync(string id)
        {
            Matiere? matiere = await context.Matieres.FindAsync(id);
            return matiere;
        }
        public async Task<Matiere> GetByNameAsync(string name)
        {
            {
                return await context.Matieres
                                     .FirstOrDefaultAsync(c => c.NomMatiere == name); // Recherche par le champ Chapitre
            }
        }



        public async Task<Matiere> AddAsync(Matiere matiere)
        {
            var result = await context.Matieres.AddAsync(matiere);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync(Matiere matiere)
        {
            context.Matieres.Update(matiere);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var matiere = await GetByIdAsync(id);
            if (matiere != null)
            {
                context.Matieres.Remove(matiere);
                await context.SaveChangesAsync();
            }
        }
    }
}
