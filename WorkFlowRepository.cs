using AppliVacationProject.BusinessLogic.Interfaces.InterfacesRepository;
using AppliVacationProject.DataAccess.Data;
using AppliVacationProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AppliVacationProject.DataAccess.Repository
{
    public class VacationsRepository : IVacationRepository
    {
        private readonly AppliVacationDbContext _databaseContext;

        public VacationsRepository(AppliVacationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

       // Recupère tous les vacations de manière asynchrone incluant l'utilisateur qui a demandé un congé
        public async Task<IEnumerable<Vacation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.Vacations.ToListAsync();
            /*return await _databaseContext.Vacations.Include(u => u.Users).ToListAsync();*/
        }
      

        //Récupère un congé (Vacation) par ID de manière asynchrone
        public async Task<Vacation> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _databaseContext.Vacations.FindAsync(id);
        }
      

        //Ajoute un congé (Vacation) de manière asynchrone
        public async Task<Vacation> AddAsync(Vacation vacation, CancellationToken cancellationToken)
        {
            _databaseContext.Vacations.Add(vacation);
            await _databaseContext.SaveChangesAsync();
            return vacation;
        }
        

        //Met à jour un congé existant de manière asynchrone
        public async Task<bool> UpdateAsync(Vacation vacation, CancellationToken cancellationToken)
        {
            // Modifie l'état de l'entité pour indiquer qu'elle est modifiée
            _databaseContext.Entry(vacation).State = EntityState.Modified;

            try
            {
                // Enregistre les modifications dans la base de données
                await _databaseContext.SaveChangesAsync();
                // Renvoie un booléen indiquant le succès de la mise à jour
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Gestion des conflits de concurrence si nécessaire
                return false;
            }
        }

        // Supprime un conge (Vacation) par ID de manière asynchrone
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            // Recherche l'utilisateur en fonction de son ID
            var vacationsToDelete = await _databaseContext.Vacations.FindAsync(id);

            if (vacationsToDelete != null)
            {
                // Supprime l'utilisateur de la base de données
                _databaseContext.Vacations.Remove(vacationsToDelete);
                // Enregistre les modifications dans la base de données
                await _databaseContext.SaveChangesAsync();
                // Renvoie un booléen indiquant le succès de la suppression
                return true;
            }
            else
            {
                // Renvoie false si l'utilisateur n'est pas trouvé
                return false;
            }
        }
        
    }
}
