using Library.Business.Provider;
using Library.Business.ViewModel;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("issue")]
    public class IssueController : BaseController
    {
        private readonly IIssueProvider _issueProvider;


        public IssueController(IIssueProvider issueProvider)
        {
            _issueProvider = issueProvider;
        }

        /// <summary>
        /// Retrieves a list of books currently issued to a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose issued books are to be fetched.</param>
        /// <returns>Standardized response containing a list of issued books.</returns>
        [Authorize]
        [HttpGet("issued-books/{userId}")]
        public BaseResponse GetIssuedBooksByUserId(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new DataValidationException(Messages.User.InValidUserId);

                var issuedBooks = _issueProvider.GetIssuedBooksByUserId(userId);
                return ApiSuccess(APIStatusCode.Ok, Messages.Book.BookFetchSuccess, issuedBooks);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Issues a book to a user based on the provided issue details.
        /// </summary>
        /// <param name="model">Details of the book issue, including user ID and book ID.</param>
        /// <returns>Standardized response indicating the book was issued successfully.</returns>
        [HttpPost("issue-book")]
        public BaseResponse IssueBook([FromBody] IssueBookViewModel model)
        {
            _issueProvider.IssueBook(model);
            return ApiSuccess(APIStatusCode.Ok, Messages.Book.BookIssuedSuccess);
        }

        /// <summary>
        /// Marks a book as returned by a user based on the provided return details.
        /// </summary>
        /// <param name="model">Details of the book return, including user ID and book ID.</param>
        /// <returns>Standardized response indicating the book was returned successfully.</returns>
        [HttpPost("return-book")]
        public BaseResponse ReturnBook([FromBody] ReturnBookViewModel model)
        {
            _issueProvider.ReturnBook(model);
            return ApiSuccess(APIStatusCode.Ok, Messages.Book.BookReturnedSuccess);
        }
    }
}
