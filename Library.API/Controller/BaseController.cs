using Library.Business.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static Library.Utilities.Constants.Enums;

namespace Library.API.Controller
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Returns a standardized successful API response.
        /// </summary>
        /// <param name="statusCode">The custom status code representing the outcome.</param>
        /// <param name="message">A message describing the success.</param>
        /// <param name="data">Optional data to return in the response.</param>
        /// <returns>A standardized success response object.</returns>
        protected BaseResponse ApiSuccess(APIStatusCode statusCode, string message, object? data = null)
        {
            var response = new BaseResponse()
            {
                IsSuccessfull = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };

            return response;
        }

        /// <summary>
        /// Returns a standardized error API response.
        /// </summary>
        /// <param name="statusCode">The custom status code representing the error.</param>
        /// <param name="message">A message describing the error.</param>
        /// <param name="data">Optional data to provide additional context.</param>
        /// <returns>A standardized error response object.</returns>
        protected BaseResponse ApiError(APIStatusCode statusCode, string message, object? data = null)
        {
            return new BaseResponse()
            {
                IsSuccessfull = false,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }
    }
}
