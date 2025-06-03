using Library.Business.ViewModel;
using Library.Models.Roles;
using Library.Models.Users;

namespace Library.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a paginated, sorted list of users with additional statistics.
        /// </summary>
        (List<UserWithStats> Users, int TotalCount) GetUserList(UserList model);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        void DeleteUserById(int userId, int currentUserId);

        /// <summary>
        /// Inserts a new user or updates an existing user.
        /// </summary>
        void UpsertUser(UsersUpsertViewModel model, int currentUserId);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        Users? GetUserById(int userId);

        List<Roles> GetAllRoles();

        bool IsCreatedBy(int userId, int creatorId);

        List<UserListViewModel> GetUsersByRole(int roleId);

    }
}
