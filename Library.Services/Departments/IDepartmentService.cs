using Library.Models.Departments;

namespace Library.Services.Departments
{
    public interface IDepartmentService
    {
        (List<Library.Models.Departments.Departments>, int) GetDepartmentList(DepartmentList requestModel, int userId);

        void DeleteDepartmentById(int departmentId);

        void UpsertDepartment(DepartmentUpsertViewModel model, int currentUserId);

        List<DepartmentViewModel> GetAllDepartments();

        int? GetHodUserIdByDepartment(int departmentId);

        //Task<(List<StudentDto> Students, List<SubjectDto> Subjects)> GetStudentsAndSubjectsByDepartmentAsync(int departmentId);
        (List<StudentDto>, SubjectDto) GetStudentsAndSubjectsByDepartment(int departmentId);

    }
}
