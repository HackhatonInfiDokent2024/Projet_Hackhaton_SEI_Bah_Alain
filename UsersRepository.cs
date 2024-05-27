using AppliVacationProject.BusinessLogic.Interfaces.InterfacesRepository;
using AppliVacationProject.DataAccess.Data;
using AppliVacationProject.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AppliVacationProject.BusinessLogic.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppliVacationDbContext _databaseContext;

        public UsersRepository(AppliVacationDbContext databasecontext)
        {
            _databaseContext = databasecontext;
        }

        //Récupère tous les utilisateur de manière asynchrone incluant leur roles distinct
        public async Task<IEnumerable<Users>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.TUsers.ToListAsync(cancellationToken: cancellationToken);
            /*return await _databaseContext.Users.Include(u => u.Roles).ToListAsync();*/
        }

        //Récupère un utilisateur par ID de manière asynchrone
        public async Task<Users> GetUsersByIdAsync(int id, CancellationToken cancellationToken)
        {
            // Récupère un utilisateur en fonction de son ID
            return await _databaseContext.TUsers.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        }

        //Ajoute un nouveau utilisateur de manière asynchrone
        public async Task<Users> CreateUsersAsync(Users user, CancellationToken cancellationToken)
        {
            // Ajoute un utilisateur à la base de données
            _databaseContext.TUsers.Add(user);
            // Enregistre les modifications dans la base de données
            await _databaseContext.SaveChangesAsync(cancellationToken);
            // Renvoie l'utilisateur
            return user;
        }

        // Met à jour un Utilisateur existant de manière asynchrone
        public async Task<bool> UpdateUsersAsync(int id, Users user, CancellationToken cancellationToken)
        {
            // Modifie l'état de l'entité pour indiquer qu'elle est modifiée
            _databaseContext.Entry(user).State = EntityState.Modified;

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

       // Supprime un utilisateur par ID de manière asynchrone
        public async Task<bool> DeleteUsersAsync(int id, CancellationToken cancellationToken)
        {
            // Recherche l'utilisateur en fonction de son ID
            var userToDelete = await _databaseContext.TUsers.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

            if (userToDelete != null)
            {
                // Supprime l'utilisateur de la base de données
                _databaseContext.TUsers.Remove(userToDelete);
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

