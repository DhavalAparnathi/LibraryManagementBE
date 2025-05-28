using Library.Business.Provider.Subject;
using Library.Business.ViewModel;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Route("subjects")]
    [ApiController]
    public class SubjectsController : BaseController
    {
        private readonly ISubjectProvider _subjectProvider;

        public SubjectsController(ISubjectProvider subjectProvider)
        {
            _subjectProvider = subjectProvider;
        }

        /// <summary>
        /// Gets list of all the subjects.
        /// </summary>
        /// <returns>List of all the Subjects.</returns>
        [Authorize(Roles = "Admin, HOD, Teacher, AssistantTeacher, Student")]
        [HttpGet("get-all")]
        public BaseResponse GetAllSubjects()
        {
            try
            {
                var departments = _subjectProvider.GetAllSubjects();
                return ApiSuccess(APIStatusCode.Ok, Messages.Subject.SubjectListFetchSuccess, departments);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method that handles the add/edit of the Subject performed by Admin.
        /// </summary>
        /// <param name="model">model type of SubjectUpsertViewModel</param>
        /// <returns>Success response with the newly generated subjectId.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("upsert")]
        public BaseResponse UpsertSubject([FromBody] SubjectUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _subjectProvider.UpsertSubject(model);
                    return ApiSuccess(APIStatusCode.Ok,
                        model.SubjectId > 0 ? Messages.Subject.SubjectUpdateSuccess : Messages.Subject.SubjectAddSuccess);
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method that deletes the subject by Id.
        /// </summary>
        /// <param name="subjectId">SubjectId which needed to be deleted.</param>
        /// <returns>Success or exception message.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{subjectId}")]
        public IActionResult DeleteSubjectById(int subjectId)
        {
            try
            {
                if (subjectId <= 0)
                    throw new DataValidationException(Messages.Subject.InvalidSubjectId);

                _subjectProvider.DeleteSubjectById(subjectId);
                return Ok(new BaseResponse
                {
                    StatusCode = (APIStatusCode)(int)APIStatusCode.Ok,
                    Message = Messages.Subject.SubjectDeleteSuccess
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
        /// Method that retrieves the subject by Id.
        /// </summary>
        /// <param name="subjectId">Subject Id of which data is fetching.</param>
        /// <returns>Subject data with appropriate response message.</returns>
        [Authorize]
        [HttpGet("subject-by-Id/{subjectId}")]
        public BaseResponse GetSubjectById(int subjectId)
        {
            try
            {
                if (subjectId <= 0)
                    throw new DataValidationException(Messages.Subject.InvalidSubjectId);

                var subject = _subjectProvider.GetSubjectById(subjectId);
                return ApiSuccess(APIStatusCode.Ok, Messages.Subject.SubjectFetchSuccess, subject);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method that retrieves subject by departmentId.
        /// </summary>
        /// <param name="departmentId">Expected department's subject departmentId.</param>
        /// <returns>List of subjects for the Specific departmentId.</returns>
        [Authorize]
        [HttpGet("by-departmentId/{departmentId}")]
        public BaseResponse GetSubjectsByDepartmentId(int departmentId)
        {
            try
            {
                if (departmentId <= 0)
                    throw new DataValidationException(Messages.Department.InvalidDepartmentId);

                var subjects = _subjectProvider.GetSubjectsByDepartmentId(departmentId);
                return ApiSuccess(APIStatusCode.Ok, Messages.Subject.SubjectFetchSuccess, subjects);
            }
            catch
            {
                throw;
            }
        }
    }
}
