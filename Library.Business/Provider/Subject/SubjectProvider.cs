using Library.Business.ViewModel;
using Library.Models.Subjects;
using Library.Services.Subject;
using Library.Utilities.Constants;

namespace Library.Business.Provider.Subject
{
    public class SubjectProvider : ISubjectProvider
    {
        private readonly ISubjectService _subjectService;

        public SubjectProvider(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>
        /// Gets list of all the subjects.
        /// </summary>
        /// <returns>List of all the Subjects.</returns>
        public List<SubjectViewModel> GetAllSubjects()
        {
            return _subjectService.GetAllSubjects();
        }

        /// <summary>
        /// Method that handles the add/edit of the Subject performed by Admin.
        /// </summary>
        /// <param name="model">model type of SubjectUpsertViewModel</param>
        public void UpsertSubject(SubjectUpsertViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));


            var upsertModel = new SubjectUpsertViewModel
            {
                SubjectId = model.SubjectId,
                SubjectName = model.SubjectName,
                DepartmentId = model.DepartmentId,
                Year = model.Year
            };

            _subjectService.UpsertSubject(upsertModel);
        }

        /// <summary>
        /// Method that deletes the subject by Id.
        /// </summary>
        /// <param name="subjectId">subject Id which is being passed to delete the specific Subject.</param>
        public void DeleteSubjectById(int subjectId)
        {
            if (subjectId <= 0)
                throw new ArgumentException(Messages.Subject.InvalidSubjectId);

            _subjectService.DeleteSubjectById(subjectId);
        }


        /// <summary>
        /// Method that retrieves the subject by Id.
        /// </summary>
        /// <param name="subjectId">Subject Id of which data is fetching.</param>
        /// <returns>Subject data with appropriate response message.</returns>
        public Subjects GetSubjectById(int subjectId)
        {
            return _subjectService.GetSubjectById(subjectId);
        }

        /// <summary>
        /// Method that retrieves subject by departmentId.
        /// </summary>
        /// <param name="departmentId">Expected department's subject departmentId.</param>
        /// <returns>List of subjects for the Specific departmentId.</returns>
        public List<Subjects> GetSubjectsByDepartmentId(int departmentId)
        {
            return _subjectService.GetSubjectsByDepartmentId(departmentId);
        }

        public PagedResult<SubjectViewModel> GetSubjectList(SubjectListViewModel model, int userId)
        {
            if (userId <= 0)
                throw new UnauthorizedAccessException();

            var requestModel = new SubjectList
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                SortColumn = model.SortColumn,
                SortDirection = model.SortDirection,
                DepartmentId = model.Filters.DepartmentId,
                SubjectName = model.Filters.SubjectName?.Trim(),
                Year = model.Filters.Year
            };

            var (subjects, totalCount) = _subjectService.GetSubjectList(requestModel);

            var subjectViewModels = subjects.Select(s => new SubjectViewModel
            {
                SubjectId = s.SubjectId,
                SubjectName = s.SubjectName,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.DepartmentName,
                Year = s.Year
            }).ToList();

            return new PagedResult<SubjectViewModel>
            {
                Items = subjectViewModels,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                TotalCount = totalCount,
                SortColumn = model.SortColumn,
                SortDirection = model.SortDirection
            };
        }

    }
}
