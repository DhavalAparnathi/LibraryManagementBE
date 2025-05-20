using Library.Business.Provider.Book;
using Library.Business.ViewModel;
using Library.Utilities.Constants;
using Library.Utilities.ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    [Route("books")]
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly IBookProvider _bookProvider;

        public BooksController(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        /// <summary>
        /// Retrieves a paginated list of books based on filter criteria.
        /// </summary>
        /// <param name="model">Filter and pagination criteria for retrieving books.</param>
        /// <returns>Standardized response containing the paginated book list.</returns>
        [Authorize(Roles = "Admin, User")]
        [HttpPost("list")]
        public BaseResponse GetBookList([FromBody] BookListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                    var pagedResult = _bookProvider.GetBookList(model, userId);
                   
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
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>Standardized response indicating success or failure.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBookById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new DataValidationException(Messages.Book.InValidBookId);

                _bookProvider.DeleteBookById(id);
                return Ok(new BaseResponse
                {
                    StatusCode = (APIStatusCode)(int)APIStatusCode.Ok,
                    Message = Messages.Book.BookDeleteSuccess
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
        /// Adds a new book or updates an existing one.
        /// </summary>
        /// <param name="model">Book data to be added or updated.</param>
        /// <returns>Standardized response indicating the operation result.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("upsert")]
        public BaseResponse UpsertBook([FromBody] BooksUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bookProvider.UpsertBook(model);
                    return ApiSuccess(APIStatusCode.Ok, model.Id > 0 ? Messages.Book.BookUpdateSuccess : Messages.Book.BookAddedSuccess);
                }
                throw new DataValidationException(ModelState);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the list of available genres.
        /// </summary>
        /// <returns>List of genres as strings.</returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("get-all-genres")]
        public BaseResponse GetGenreList()
        {
            try
            {
                var genres = _bookProvider.GetGenreList();
                return ApiSuccess(APIStatusCode.Ok, Messages.Book.GenreListSuccess, genres);
            }
            catch
            {
                throw;
            }
        }

    }
}
