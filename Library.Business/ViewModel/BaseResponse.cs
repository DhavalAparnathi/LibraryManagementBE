using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Utilities.Constants.Enums;

namespace Library.Business.ViewModel
{
    public class BaseResponse
    {
        // request is successfull or not
        public bool IsSuccessfull { get; set; } = true;

        // request api status code
        public APIStatusCode StatusCode { get; set; }

        // add any message that regarding request api
        public string Message { get; set; } = string.Empty;

        // data that need to provide on responce of request api
        public object? Data { get; set; } = null;
    }
}
