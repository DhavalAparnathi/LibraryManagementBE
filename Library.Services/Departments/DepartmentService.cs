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

        /// <summary>
        /// Method that returns the Paginated list of All the departments with filter & sorting.
        /// </summary>
        /// <param name="model">Model type of Department list</param>
        /// <param name="userId">Current user Id</param>
        /// <returns>Paginated list of All the departments with filter & sorting.</returns>
        public (List<Library.Models.Departments.Departments>, int) GetDepartmentList(DepartmentList model, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageIndex", model.PageNumber);
            parameters.Add("PageSize", model.PageSize);
            parameters.Add("ColumnName", model.SortColumn);
            parameters.Add("SortDirection", model.SortDirection);
            parameters.Add("DepartmentName", model.DepartmentName);
            parameters.Add("HodUserName", model.HodUserName);
            parameters.Add("UserId", userId);

            var (departments, totalCount) = _dapperService.QueryMultiple<Library.Models.Departments.Departments, int>(
                StoredProcedures.GetAllDepartments,parameters
            );

            return (departments, totalCount);
        }

        /// <summary>
        /// Deletes the department by Id.
        /// </summary>
        /// <param name="departmentId">Department Id which needed to be deleted</param>
        /// <exception cref="DataValidationException">Custom exception.</exception>
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

        /// <summary>
        /// Upsert the department with the given input payload.
        /// </summary>
        /// <param name="model">Model class containing the payload keys</param>
        /// <param name="currentUserId">Current userId</param>
        public void UpsertDepartment(DepartmentUpsertViewModel model, int currentUserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", model.DepartmentId);
            parameters.Add("DepartmentName", model.DepartmentName);
            parameters.Add("Description", model.Description);
            parameters.Add("HodUserId", model.HodUserId);

            _dapperService.Execute(StoredProcedures.UpsertDepartment, parameters);
        }

        /// <summary>
        /// Gets the list of all the departments.
        /// </summary>
        /// <returns>List of all the departments.</returns>
        public List<DepartmentViewModel> GetAllDepartments()
        {
            var departments = _dapperService.Query<DepartmentViewModel>(
                StoredProcedures.GetAllDepartmentsList
            );
            return departments.ToList();
        }

        public int? GetHodUserIdByDepartment(int departmentId)
        {
            var param = new { DepartmentId = departmentId };
            var department = _dapperService.QueryFirstOrDefault<Library.Models.Departments.Departments>(
                StoredProcedures.GetDepartmentById, param);

            return department?.HodUserId;
        }

    }
}
