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
                DepartmentName = model.Filters.DepartmentName?.Trim()
            };

            var (departments, totalCount) = _departmentService.GetDepartmentList(requestModel, userId);

            var departmentViewModels = departments.Select(d => new DepartmentViewModel
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                Description = d.Description
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

        public void DeleteDepartmentById(int departmentId)
        {
            if (departmentId <= 0)
                throw new ArgumentException(Messages.Department.InvalidDepartmentId);

            _departmentService.DeleteDepartmentById(departmentId);
        }

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


        public List<DepartmentViewModel> GetAllDepartments()
        {
            return _departmentService.GetAllDepartments();
        }
    }
}
