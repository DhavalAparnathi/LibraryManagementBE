using Library.Business.ViewModel;
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
        void DeleteUserById(int userId);

        /// <summary>
        /// Inserts a new user or updates an existing user.
        /// </summary>
        void UpsertUser(UsersUpsertViewModel model);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        Users? GetUserById(int userId);
    }
}
