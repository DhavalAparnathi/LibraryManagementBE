using Library.Business.Provider.Department;
using Library.Business.Provider.WorkContext;
using Library.Business.ViewModel;
using Library.Models.Departments;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Route("departments")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentProvider _departmentProvider;
        private readonly IWorkContext _workContext;

        public DepartmentController(IDepartmentProvider departmentProvider, IWorkContext workContext)
        {
            _departmentProvider = departmentProvider;
            _workContext = workContext;
        }

        /// <summary>
        /// Retrieves a paginated list of departments.
        /// </summary>
        /// <param name="model">Filter and pagination criteria.</param>
        /// <returns>Standardized response containing the paginated department list.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("list")]
        public BaseResponse GetDepartmentList([FromBody] DepartmentListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int currentUserId = _workContext.CurrentUserId;

                    var pagedResult = _departmentProvider.GetDepartmentList(model, currentUserId);
                    return ApiSuccess(APIStatusCode.Ok, string.Empty, pagedResult);
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new department or updates an existing one.
        /// </summary>
        /// <param name="model">Department data to be added or updated.</param>
        /// <returns>Standardized response indicating success or failure.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("upsert")]
        public BaseResponse UpsertDepartment([FromBody] DepartmentUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentProvider.UpsertDepartment(model);
                    return ApiSuccess(APIStatusCode.Ok,
                        model.DepartmentId > 0 ? Messages.Department.DepartmentUpdateSuccess : Messages.Department.DepartmentAddSuccess);
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>Standardized response indicating success or failure.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartmentById(int departmentId)
        {
            try
            {
                if (departmentId <= 0)
                    throw new DataValidationException(Messages.Department.InvalidDepartmentId);

                _departmentProvider.DeleteDepartmentById(departmentId);
                return Ok(new BaseResponse
                {
                    StatusCode = (APIStatusCode)(int)APIStatusCode.Ok,
                    Message = Messages.Department.DepartmentDeleteSuccess
                });
            }
            catch (DataValidationException ex)
            {
                return BadRequest(new BaseResponse
                {
                    StatusCode = (APIStatusCode)(int)APIStatusCode.BadRequest,
                    Message = ex.Message
                });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the list of all departments.
        /// </summary>
        /// <returns>List of departments.</returns>
        [Authorize(Roles = "Admin, HOD, Teacher, AssistantTeacher, Student")]
        [HttpGet("get-all")]
        public BaseResponse GetAllDepartments()
        {
            try
            {
                var departments = _departmentProvider.GetAllDepartments();
                return ApiSuccess(APIStatusCode.Ok, Messages.Department.DepartmentListFetchSuccess, departments);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{departmentId}/students-subjects")]
        public async Task<IActionResult> GetStudentsAndSubjectsByDepartment(int departmentId)
        {
            var (students, subject) = await _departmentProvider.GetStudentsAndSubjectsByDepartmentAsync(departmentId);

            // Return object with both properties
            var response = new
            {
                Students = students,
                Subjects = new List<SubjectDto> { subject }  // Wrapping single SubjectDto into a list for consistency with frontend expectations
            };

            return Ok(response);
        }

    }
}
