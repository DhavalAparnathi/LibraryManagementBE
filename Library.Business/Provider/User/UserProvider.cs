using Library.Business.Mapper;
using Library.Business.Provider.WorkContext;
using Library.Business.ViewModel;
using Library.Models.Users;
using Library.Services.User;

namespace Library.Business.Provider.User
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;

        public UserProvider(IUserService userService, IWorkContext workContext)
        {
            _userService = userService;
            _workContext = workContext;
        }

        /// <summary>
        /// Retrieves a paginated and sorted list of users based on the provided filter criteria.
        /// </summary>
        /// <param name="model">Filtering, sorting, and paging criteria.</param>
        /// <returns>A paged result containing a list of user view models.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when current user context is not valid.</exception>
        public PagedResult<UserViewModel> GetUserList(UserListVM model)
        {
            try
            {
                var currentUserId = _workContext.CurrentUserId;
                var requestModel = new UserList
                {
                    PageNumber = model.PageNumber,
                    PageSize = model.PageSize,
                    SortColumn = model.SortColumn,
                    SortDirection = model.SortDirection,
                    UserName = model.Filters.UserName?.Trim(),
                    Email = model.Filters.Email?.Trim()
                };

                var (users, totalCount) = _userService.GetUserList(requestModel);

                var userViewModels = users.ToModelList<UserViewModel, UserWithStats>();

                var pageCount = (int)Math.Ceiling((double)totalCount / requestModel.PageSize);

                return new PagedResult<UserViewModel>
                {
                    Items = userViewModels,
                    PageNumber = model.PageNumber,
                    PageSize = model.PageSize,
                    TotalCount = totalCount,
                    SortColumn = model.SortColumn,
                    SortDirection = model.SortDirection,
                };
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <exception cref="ArgumentException">Thrown if the userId is invalid (less than or equal to zero).</exception>
        public void DeleteUserById(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid book ID.");

            _userService.DeleteUserById(userId);
        }

        /// <summary>
        /// Creates a new user or updates an existing user.
        /// </summary>
        /// <param name="model">The user data to upsert.</param>
        /// <exception cref="ArgumentNullException">Thrown if the user model is null.</exception>
        public void UpsertUser(UsersUpsertViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _userService.UpsertUser(model);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        public Users? GetUserById(int userId)
        {
            return _userService.GetUserById(userId);
        }

    }
}
