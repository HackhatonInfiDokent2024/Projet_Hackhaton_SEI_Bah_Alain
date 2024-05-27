using AppliVacationProject.BusinessLogic.Interfaces.InterfacesRepository;
using AppliVacationProject.DataAccess.Data;
using AppliVacationProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AppliVacationProject.BusinessLogic.Services
{
    public class RolesRepository : IUserRoleRepository
    {
        private readonly AppliVacationDbContext _databaseContext;

        public RolesRepository(AppliVacationDbContext databasecontext)
        {
            _databaseContext = databasecontext;
        }

        public async Task<IEnumerable<UserRole>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.Roles.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<UserRole> GetRolesByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _databaseContext.Roles.FindAsync(id, cancellationToken);
        }

        public async Task<UserRole> CreateUserRoleAsync(UserRole role, CancellationToken cancellationToken)
        {
            // Ajoute un rôle à la base de données
            _databaseContext.Roles.Add(role);
            // Enregistre les modifications dans la base de données
            await _databaseContext.SaveChangesAsync(cancellationToken);
            // Renvoie le rôle
            return role;
        }

        public async Task<bool> UpdateRolesAsync(int id, UserRole role, CancellationToken cancellationToken)
        {
            // Modifie l'état de l'entité pour indiquer qu'elle est modifiée
            _databaseContext.Entry(role).State = EntityState.Modified;

            try
            {
                // Enregistre les modifications dans la base de données
                await _databaseContext.SaveChangesAsync(cancellationToken);
                // Renvoie un booléen indiquant le succès de la mise à jour
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Gestion des conflits de concurrence si nécessaire
                return false;
            }
        }

        public async Task<bool> DeleteRolesAsync(int id, CancellationToken cancellationToken)
        {
            // Recherche le rôle en fonction de son ID
            var roleToDelete = await _databaseContext.Roles.FindAsync(id);

            if (roleToDelete != null)
            {
                // Supprime le rôle de la base de données
                _databaseContext.Roles.Remove(roleToDelete);
                // Enregistre les modifications dans la base de données
                await _databaseContext.SaveChangesAsync(cancellationToken);
                // Renvoie un booléen indiquant le succès de la suppression
                return true;
            }
            else
            {
                // Renvoie false si le rôle n'est pas trouvé
                return false;
            }

        }
    }
}
