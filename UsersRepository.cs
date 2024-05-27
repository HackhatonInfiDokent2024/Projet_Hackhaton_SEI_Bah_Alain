//using AppliWorkFlowProject.DataAccess.Models; // Assuming this is the correct namespace for the 'Users' model
//using AppliWorkFlowProject.DataAccess.Data; // Assuming this is the correct namespace for the database context
//using Microsoft.EntityFrameworkCore; // Assuming you're using Entity Framework Core for data access
//using AppliWorkFlowProject.BusinessLogic.Interfaces.InterfacesRepository;

//namespace AppliWorkFlowProject.BusinessLogic.Services
//{
//    public class UsersRepository : IUsersRepository
//    {
//        private readonly AppliWorkFlowDbContext _databaseContext;
//        private object value;

//        public UsersRepository(AppliWorkFlowDbContext databaseContext)
//        {
//            _databaseContext = databaseContext;
//        }

//        // Retrieves all users asynchronously, including their distinct roles
//        public async Task<IEnumerable<Users>> GetAllUsersAsync(CancellationToken cancellationToken)
//        {
//            // Note: This query assumes that users have a 'Roles' property that holds a collection of role objects
//            var usersWithDistinctRoles = await _databaseContext.Users
//                .Include(u => u.Roles.Select(r => r.RoleName))
//                .Select(u => new Users
//                {
//                    UserId = u.UserId,
//                    FirstName = u.FirstName,
//                    LastName = u.LastName,
//                    Email = u.Email,
//                    Roles = u.Roles.Select(r => r.RoleName).Distinct().ToList()
//                })
//                .ToListAsync(cancellationToken);

//            return usersWithDistinctRoles;
//        }

//        // Retrieves a user by ID asynchronously
//        public async Task<Users> GetUsersByIdAsync(int id, CancellationToken cancellationToken)
//        {
//            // Retrieves a user based on their ID
//            var user = await _databaseContext.Users.FindAsync(id, cancellationToken);

//            if (user != null)
//            {
//                // Eagerly load the user's roles
//                await _databaseContext.Entry(user).Reference(u => u.Roles).LoadAsync(cancellationToken);

//                // Return the user with loaded roles
//                return user;
//            }

//            return null;
//        }

//        // Creates a new user asynchronously
//        public async Task<Users> CreateUsersAsync(Users user, CancellationToken cancellationToken)
//        {
//            // Adds the user to the database
//            await _databaseContext.Users.AddAsync(user, cancellationToken);

//            // Saves the changes to the database
//            await _databaseContext.SaveChangesAsync(cancellationToken);

//            // Return the newly created user
//            return user;
//        }

//        // Updates an existing user asynchronously
//        public async Task<bool> UpdateUsersAsync(Users user, CancellationToken cancellationToken)
//        {
//            // Attach the user to the context for tracking changes
//            value =  _databaseContext.Users.Attach(user);

//            // Set the entity state to 'Modified'
//            _databaseContext.Entry(user).State = EntityState.Modified;

//            try
//            {
//                // Save the changes to the database
//                await _databaseContext.SaveChangesAsync(cancellationToken);

//                // Return true if the update was successful
//                return true;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                // Handle concurrency conflicts if necessary
//                return false;
//            }
//        }

//        // Deletes a user by ID asynchronously
//        public async Task<bool> DeleteUsersAsync(int id, CancellationToken cancellationToken)
//        {
//            // Retrieves the user to be deleted
//            var userToDelete = await _databaseContext.Users.FindAsync(id, cancellationToken);

//            if (userToDelete != null)
//            {
//                // Removes the user from the database
//                _databaseContext.Users.Remove(userToDelete);

//                // Saves the changes to the database
//                await _databaseContext.SaveChangesAsync(cancellationToken);

//                // Return true if the deletion was successful
//                return true;
//            }

//            return false;
//        }
//    }
//}
