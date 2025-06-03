using Dapper;
using Library.Business.ViewModel;
using Library.Data.Repository;
using Library.Models.Subjects;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.Data.SqlClient;

namespace Library.Services.Subject
{
    public class SubjectService : ISubjectService
    {
        private readonly IDapperService _dapperService;
        public SubjectService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        /// <summary>
        /// Gets list of all the subjects.
        /// </summary>
        /// <returns>List of all the Subjects.</returns>
        public List<SubjectViewModel> GetAllSubjects()
        {
            var subjects = _dapperService.Query<SubjectViewModel>(
                StoredProcedures.GetAllSubjects
            );
            return subjects.ToList();
        }

        /// <summary>
        /// Method that handles the add/edit of the Subject performed by Admin.
        /// </summary>
        /// <param name="model">model type of SubjectUpsertViewModel</param>
        public void UpsertSubject(SubjectUpsertViewModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SubjectId", model.SubjectId);
            parameters.Add("SubjectName", model.SubjectName);
            parameters.Add("DepartmentId", model.DepartmentId);
            parameters.Add("Year", model.Year);

            _dapperService.Execute(StoredProcedures.UpsertSubject, parameters);
        }

        /// <summary>
        /// Method that deletes the subject by Id.
        /// </summary>
        /// <param name="subjectId">subject Id which is being passed to delete the specific Subject.</param>
        /// <exception cref="DataValidationException">Exception for subject delete error.</exception>
        public void DeleteSubjectById(int subjectId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SubjectId", subjectId);

            try
            {
                _dapperService.Execute(StoredProcedures.DeleteSubjectById, parameters);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains(Messages.Subject.SubjectDeleteSqlError))
                {
                    throw new DataValidationException(Messages.Subject.SubjectDeleteError);
                }
                throw;
            }
        }

        /// <summary>
        /// Method that retrieves the subject by Id.
        /// </summary>
        /// <param name="subjectId">Subject Id of which data is fetching.</param>
        /// <returns>Subject data with appropriate response message.</returns>
        public Subjects GetSubjectById(int subjectId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SubjectId", subjectId);

            return _dapperService.QueryFirstOrDefault<Subjects>(StoredProcedures.GetSubjectById, parameters);
        }

        /// <summary>
        /// Method that retrieves subject by departmentId.
        /// </summary>
        /// <param name="departmentId">Expected department's subject departmentId.</param>
        /// <returns>List of subjects for the Specific departmentId.</returns>
        public List<Subjects> GetSubjectsByDepartmentId(int departmentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", departmentId);

            return _dapperService.Query<Subjects>(StoredProcedures.GetSubjectsByDepartment, parameters).ToList();
        }

        /// <summary>
        /// Method that retrieves the list of subjects.
        /// </summary>
        /// <param name="model">model parameter of type SubjectListViewModel</param>
        /// <returns>A list of subjects</returns>
        public (List<Subjects>, int) GetSubjectList(SubjectList model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageIndex", model.PageNumber);
            parameters.Add("PageSize", model.PageSize);
            parameters.Add("ColumnName", string.IsNullOrWhiteSpace(model.SortColumn) ? "SubjectName" : model.SortColumn);
            parameters.Add("SortDirection", string.IsNullOrWhiteSpace(model.SortDirection) ? "ASC" : model.SortDirection);
            parameters.Add("SubjectName", string.IsNullOrWhiteSpace(model.SubjectName) ? null : model.SubjectName);
            parameters.Add("DepartmentId", model.DepartmentId);

            int? yearInt = null;
            if (!string.IsNullOrWhiteSpace(model.Year) && int.TryParse(model.Year, out int parsedYear))
            {
                yearInt = parsedYear;
            }
            parameters.Add("Year", yearInt);

            var (subjects, totalCount) = _dapperService.QueryMultiple<Subjects, int>(StoredProcedures.GetAllSubjectList, parameters);
            return (subjects, totalCount);
        }

    }
}
