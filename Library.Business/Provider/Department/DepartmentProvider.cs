using Library.Business.Provider.WorkContext;
using Library.Business.ViewModel;
using Library.Models.Departments;
using Library.Services.Departments;
using Library.Utilities.Constants;

namespace Library.Business.Provider.Department
{
    public class DepartmentProvider : IDepartmentProvider
    {
        private readonly IDepartmentService _departmentService;
        private readonly IWorkContext _workContext;

        public DepartmentProvider(IDepartmentService departmentService, IWorkContext workContext)
        {
            _departmentService = departmentService;
            _workContext = workContext;
        }

        /// <summary>
        /// Method that retrieves the list of departments.
        /// </summary>
        /// <param name="model">model parameter of type DepartmentListViewModel</param>
        /// <param name="userId">Current userId</param>
        /// <returns>A list of departments</returns>
        public PagedResult<DepartmentViewModel> GetDepartmentList(DepartmentListViewModel model, int userId)
        {
            if (userId <= 0)
                throw new UnauthorizedAccessException();

            var requestModel = new DepartmentList
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                SortColumn = model.SortColumn,
                SortDirection = model.SortDirection,
                DepartmentName = model.Filters.DepartmentName?.Trim(),
                HodUserName = model.Filters.HodUserName?.Trim(),
            };

            var (departments, totalCount) = _departmentService.GetDepartmentList(requestModel, userId);

            var departmentViewModels = departments.Select(d => new DepartmentViewModel
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
                CreatedDate = d.CreatedDate,
                HodUserId = d.HodUserId,
                HodUserName = d.HodUserName,
            }).ToList();

            return new PagedResult<DepartmentViewModel>
            {
                Items = departmentViewModels,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = totalCount,
                SortColumn = model.SortColumn,
                SortDirection = model.SortDirection
            };
        }

        /// <summary>
        /// Method that deletes the department by departmentId
        /// </summary>
        /// <param name="departmentId">Entered departmentId to delete</param>
        public void DeleteDepartmentById(int departmentId)
        {
            if (departmentId <= 0)
                throw new ArgumentException(Messages.Department.InvalidDepartmentId);

            _departmentService.DeleteDepartmentById(departmentId);
        }

        /// <summary>
        /// Mehtod that upserts the Department data into Department table.
        /// </summary>
        /// <param name="model">Model of DepartmentUpsertViewModel</param>
        public void UpsertDepartment(DepartmentUpsertViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            int currentUserId = _workContext.CurrentUserId;

            var upsertModel = new DepartmentUpsertViewModel
            {
                DepartmentId = model.DepartmentId,
                DepartmentName = model.DepartmentName,
                Description = model.Description,
                HodUserId = model.HodUserId
            };

            _departmentService.UpsertDepartment(upsertModel, currentUserId);
        }

        /// <summary>
        /// Retrieves all the departments.
        /// </summary>
        /// <returns>List of all the departments.</returns>
        public List<DepartmentViewModel> GetAllDepartments()
        {
            return _departmentService.GetAllDepartments();
        }
    }
}
