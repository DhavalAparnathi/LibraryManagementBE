using Library.Business.ViewModel;
using Library.Models.Subjects;

namespace Library.Services.Subject
{
    public interface ISubjectService
    {
        List<SubjectViewModel> GetAllSubjects();

        void UpsertSubject(SubjectUpsertViewModel model);

        void DeleteSubjectById(int subjectId);

        Subjects GetSubjectById(int subjectId);

        List<Subjects> GetSubjectsByDepartmentId(int departmentId);

    }
}
