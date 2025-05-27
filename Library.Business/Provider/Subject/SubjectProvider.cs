using Library.Business.ViewModel;
using Library.Models.Departments;
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

        public List<SubjectViewModel> GetAllSubjects()
        {
            return _subjectService.GetAllSubjects();
        }

        public void DeleteSubjectById(int subjectId)
        {
            if (subjectId <= 0)
                throw new ArgumentException(Messages.Subject.InvalidSubjectId);

            _subjectService.DeleteSubjectById(subjectId);
        }

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

        public Subjects GetSubjectById(int subjectId)
        {
            return _subjectService.GetSubjectById(subjectId);
        }

        public List<Subjects> GetSubjectsByDepartmentId(int departmentId)
        {
            return _subjectService.GetSubjectsByDepartmentId(departmentId);
        }
    }
}
