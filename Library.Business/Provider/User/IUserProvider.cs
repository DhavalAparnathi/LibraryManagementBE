using Library.Business.ViewModel;
using Library.Models.Users;

namespace Library.Business.Provider.User
{
    public interface IUserProvider
    {
        PagedResult<UserViewModel> GetUserList(UserListVM model);

        void DeleteUserById(int userId);

        void UpsertUser(UsersUpsertViewModel model);

        Users? GetUserById(int userId);

    }
}
