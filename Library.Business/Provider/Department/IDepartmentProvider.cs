using Library.Business.ViewModel;
using Library.Models.Departments;

namespace Library.Business.Provider.Department
{
    public interface IDepartmentProvider
    {
        PagedResult<DepartmentViewModel> GetDepartmentList(DepartmentListViewModel model, int userId);
        void DeleteDepartmentById(int departmentId);
        void UpsertDepartment(DepartmentUpsertViewModel model);
        List<DepartmentViewModel> GetAllDepartments();
        Task<(List<StudentDto> Students, SubjectDto Subject)> GetStudentsAndSubjectsByDepartmentAsync(int departmentId);
    }
}
