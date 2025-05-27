using Dapper;
using Library.Data.Repository;
using Library.Models.Departments;
using Library.Services.Departments;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.Data.SqlClient;

namespace Library.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDapperService _dapperService;

        public DepartmentService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public (List<Library.Models.Departments.Departments>, int) GetDepartmentList(DepartmentList model, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageIndex", model.PageNumber);
            parameters.Add("PageSize", model.PageSize);
            parameters.Add("ColumnName", model.SortColumn);
            parameters.Add("SortDirection", model.SortDirection);
            parameters.Add("DepartmentName", model.DepartmentName);
            parameters.Add("UserId", userId);

            var (departments, totalCount) = _dapperService.QueryMultiple<Library.Models.Departments.Departments, int>(
                StoredProcedures.GetAllDepartments,parameters
            );

            return (departments, totalCount);
        }

        public void DeleteDepartmentById(int departmentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", departmentId);

            try
            {
                _dapperService.Execute(StoredProcedures.DeleteDepartmentById, parameters);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains(Messages.Department.DepartmentDeleteSqlError))
                {
                    throw new DataValidationException(Messages.Department.DepartmentDeleteError);
                }
                throw;
            }
        }

        public void UpsertDepartment(DepartmentUpsertViewModel model, int currentUserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", model.DepartmentId);
            parameters.Add("DepartmentName", model.DepartmentName);
            parameters.Add("Description", model.Description);
            parameters.Add("HodUserId", model.HodUserId);

            _dapperService.Execute(StoredProcedures.UpsertDepartment, parameters);
        }

        public List<DepartmentViewModel> GetAllDepartments()
        {
            var departments = _dapperService.Query<DepartmentViewModel>(
                StoredProcedures.GetAllDepartmentsList
            );
            return departments.ToList();
        }
    }
}
