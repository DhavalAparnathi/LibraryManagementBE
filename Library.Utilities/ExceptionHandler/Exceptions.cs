using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Library.Utilities.ExceptionHandler
{
    public class DataValidationException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public IList<ValidationDetailsModel> Error { get; private set; }
        public new string Message { get; private set; }

        /// <summary>
        /// Constructor that creates a validation exception from a ModelStateDictionary. Extracts all validation errors and sets the HTTP status code to BadRequest.
        /// </summary>
        /// <param name="excpetion">ModelStateDictionary containing validation errors.</param>
        /// <param name="message">Optional custom message (default empty).</param>
        public DataValidationException(ModelStateDictionary excpetion, string message = "") : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Error = excpetion.Select(ex => new ValidationDetailsModel { InputName = ex.Key, ValidationMessage = ex.Value?.Errors.FirstOrDefault()?.ErrorMessage }).ToList();
            Message = excpetion.Select(ex => ex.Value?.Errors.FirstOrDefault()?.ErrorMessage).FirstOrDefault();
        }

        /// <summary>
        /// Constructor that creates a validation exception with a simple message. Useful for manual validation failures outside ModelState.
        /// </summary>
        /// <param name="message">Error message to include in the exception.</param>
        public DataValidationException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
        }
    }
}
