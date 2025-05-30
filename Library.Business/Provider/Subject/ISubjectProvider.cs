using Library.Business.ViewModel;
using Library.Models.Subjects;

namespace Library.Business.Provider.Subject
{
    public interface ISubjectProvider
    {
        List<SubjectViewModel> GetAllSubjects();

        void UpsertSubject(SubjectUpsertViewModel model);

        void DeleteSubjectById(int subjectId);

        Subjects GetSubjectById(int subjectId);

        List<Subjects> GetSubjectsByDepartmentId(int departmentId);

        PagedResult<SubjectViewModel> GetSubjectList(SubjectListViewModel model, int userId);

    }
}
