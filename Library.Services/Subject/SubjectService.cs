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

        public List<SubjectViewModel> GetAllSubjects()
        {
            var subjects = _dapperService.Query<SubjectViewModel>(
                StoredProcedures.GetAllSubjects
            );
            return subjects.ToList();
        }

        public void UpsertSubject(SubjectUpsertViewModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SubjectId", model.SubjectId);
            parameters.Add("SubjectName", model.SubjectName);
            parameters.Add("DepartmentId", model.DepartmentId);
            parameters.Add("Year", model.Year);

            _dapperService.Execute(StoredProcedures.UpsertSubject, parameters);
        }

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

        public Subjects GetSubjectById(int subjectId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SubjectId", subjectId);

            return _dapperService.QueryFirstOrDefault<Subjects>(StoredProcedures.GetSubjectById, parameters);
        }
        
        public List<Subjects> GetSubjectsByDepartmentId(int departmentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DepartmentId", departmentId);

            return _dapperService.Query<Subjects>(StoredProcedures.GetSubjectsByDepartment, parameters).ToList();
        }
    }
}
