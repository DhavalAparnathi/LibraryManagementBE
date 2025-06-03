using Dapper;
using Library.Business.ViewModel;
using Library.Data.Repository;
using Library.Models.Roles;
using Library.Models.Users;
using Library.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Library.Services.User
{
    public class UserService : IUserService
    {
        private readonly IDapperService _dapperService;

        public UserService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Retrieves a paginated, sorted list of users with additional statistics.
        /// </summary>
        /// <param name="model">Filtering, paging, and sorting criteria.</param>
        /// <returns>A tuple containing the list of users and the total record count.</returns>
        public (List<UserWithStats> Users, int TotalCount) GetUserList(UserList model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageIndex", model.PageNumber);
            parameters.Add("PageSize", model.PageSize);
            parameters.Add("ColumnName", model.SortColumn);
            parameters.Add("SortDirection", model.SortDirection);
            parameters.Add("UserName", model.UserName);
            parameters.Add("Email", model.Email);

            var (users, totalCount) = _dapperService.QueryMultiple<UserWithStats, int>(StoredProcedures.GetAllUsers, parameters);

            return (users, totalCount);
        }

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public void DeleteUserById(int userId, int currentUserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", userId);

            _dapperService.Execute(StoredProcedures.DeleteUserById, parameters);
        }

        /// <summary>
        /// Inserts a new user or updates an existing user.
        /// </summary>
        /// <param name="model">The user data for insertion or update.</param>
        public void UpsertUser(UsersUpsertViewModel model, int currentUserId)
        {
            var parameters = new DynamicParameters();

            string newUsername = (model.Username ?? string.Empty).Replace(" ", string.Empty);

            parameters.Add("Id", model.Id);
            parameters.Add("Username", newUsername);
            parameters.Add("Email", model.Email);
            parameters.Add("PhoneNumber", model.PhoneNumber);
            parameters.Add("IsActive", model.IsActive);

            string hashedPassword = new PasswordHasher<string>().HashPassword(null, model.PasswordHash);
            parameters.Add("PasswordHash", hashedPassword);
            parameters.Add("RoleId", model.RoleId);
            parameters.Add("DepartmentId", model.DepartmentId);
            if (model.Id == 0)
                parameters.Add("CreatedBy", currentUserId);


            _dapperService.Execute(StoredProcedures.UpsertUser, parameters);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The user ID to look up.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        public Users? GetUserById(int userId)
        {
            var param = new { Id = userId };
            return _dapperService.QueryFirstOrDefault<Users>(StoredProcedures.GetUserById, param);
        }

        /// <summary>
        /// Retrieves the user roles.
        /// </summary>
        /// <returns>List of all user roles.</returns>
        public List<Roles> GetAllRoles()
        {
            var roles = _dapperService.Query<Roles>(
                StoredProcedures.GetAllRoles
            );
            return roles.ToList();
        }

        /// <summary>
        /// Method that returns the CreatedBy user's Id.
        /// </summary>
        /// <param name="userId">Current user's Id</param>
        /// <param name="creatorId">Record creator's Id</param>
        /// <returns></returns>
        public bool IsCreatedBy(int userId, int creatorId)
        {
            var parameters = new { UserId = userId, CreatorId = creatorId };
            return _dapperService.ExecuteScalar<bool>(
                StoredProcedures.CheckUserCreatedBy, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Method that returns the Users by given roleId.
        /// </summary>
        /// <param name="roleId">Role Id based on user data is fetched</param>
        /// <returns>User data by RoleId</returns>
        public List<UserListViewModel> GetUsersByRole(int roleId)
        {
            var users = _dapperService.Query<UserListViewModel>(
                StoredProcedures.GetUsersByRole, new { RoleId = roleId });
            return users.ToList();
        }

    }
}
