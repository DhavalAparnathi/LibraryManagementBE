using Library.Business.Mapper;
using Library.Business.Provider.WorkContext;
using Library.Business.ViewModel;
using Library.Models.Roles;
using Library.Models.Users;
using Library.Services.User;
using Library.Utilities.Constants;
using static Library.Utilities.Constants.Messages;

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
        public void DeleteUserById(int userId, int currentUserId, string currentUserRole)
        {
            if (userId <= 0)
                throw new ArgumentException(Messages.User.InValidUserId);

            bool isAdmin = currentUserRole == Messages.Role.ADMIN;
            bool isCreatedBy = _userService.IsCreatedBy(userId, currentUserId);
            if (!isAdmin && !isCreatedBy)
                throw new UnauthorizedAccessException(Messages.User.NoAuthorizedToDelete);

            _userService.DeleteUserById(userId, currentUserId);
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

            var currentUserId = _workContext.CurrentUserId;
            var currentUserRole = _workContext.CurrentUserRole;

            if (currentUserRole != Role.ADMIN)
            {
                if (currentUserRole == Role.HOD)
                {
                    if (!AllowedForHOD(model.RoleId))
                        throw new UnauthorizedAccessException(Messages.User.HODPermissions);

                    if (model.Id > 0 && !_userService.IsCreatedBy(model.Id, currentUserId))
                        throw new UnauthorizedAccessException(Messages.User.HODNoPermission);
                }
                else if (currentUserRole == Role.TEACHER)
                {
                    if (!AllowedForTeacher(model.RoleId))
                        throw new UnauthorizedAccessException(Messages.User.TeacherPermissions);

                    if (model.Id > 0 && !_userService.IsCreatedBy(model.Id, currentUserId))
                        throw new UnauthorizedAccessException(Messages.User.TeacherNoPermission);
                }
                else
                {
                    throw new UnauthorizedAccessException(Messages.User.GeneralNoPermission);
                }
            }

            _userService.UpsertUser(model, currentUserId);
        }

        /// <summary>
        /// Check for HOD's role access
        /// </summary>
        private bool AllowedForHOD(int roleId)
        {
            return roleId == RoleIds.Teacher|| roleId == RoleIds.AssistantTeacher || roleId == RoleIds.Student;
        }

        /// <summary>
        /// Check for teacher's role access
        /// </summary>
        private bool AllowedForTeacher(int roleId)
        {
            return roleId == RoleIds.AssistantTeacher || roleId == RoleIds.Student;
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

        /// <summary>
        /// Retrieves all the user roles.
        /// </summary>
        /// <returns>List of all the user roles.</returns>
        public List<Roles> GetAllRoles()
        {
            return _userService.GetAllRoles();
        }

        /// <summary>
        /// Gets the user details by given roleId
        /// </summary>
        /// <param name="roleId">RoleId of the user</param>
        /// <returns>User details of given role</returns>
        public List<UserListViewModel> GetUsersByRole(int roleId)
        {
            return _userService.GetUsersByRole(roleId);
        }

    }
}
