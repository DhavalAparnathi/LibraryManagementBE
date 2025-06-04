namespace Library.Business.Provider.WorkContext
{
    public interface IWorkContext
    {
        int CurrentUserId { get; }
        string CurrentUserRole { get; }
        int? CurrentDepartmentId { get; }

    }
}
