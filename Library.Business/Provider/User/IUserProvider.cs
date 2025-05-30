using Library.Business.ViewModel;
using Library.Models.Roles;
using Library.Models.Users;

namespace Library.Business.Provider.User
{
    public interface IUserProvider
    {
        PagedResult<UserViewModel> GetUserList(UserListVM model);

        void DeleteUserById(int userId, int currentUserId, string currentUserRole);

        void UpsertUser(UsersUpsertViewModel model);

        Users? GetUserById(int userId);

        List<Roles> GetAllRoles();

        List<UserListViewModel> GetUsersByRole(int roleId);

    }
}
