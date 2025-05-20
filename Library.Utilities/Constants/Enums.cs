using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Utilities.Constants
{
    public static class Enums
    {
        public enum UserStatus // User status codes
        {
            Active = 1,
            Inactive = 2,
        }

        public enum APIStatusCode // API status codes
        {
            ModelStateError = -1,
            Ok = 200,
            BadRequest = 400,
            NotFound = 404,
            ServerError = 500,
            UnAuthorized = 401,
            AccessDenied = 403,
            NotAllowed = 405,
            Conflict = 409
        }

        public static readonly List<string> Genres = new List<string>
        {
            "Fiction",
            "Non-Fiction",
            "Science Fiction",
            "Fantasy",
            "Mystery",
            "Thriller",
            "Romance",
            "Horror",
            "Biography",
            "History",
            "Children",
            "Young Adult",
            "Self-Help",
            "Poetry"
        };
    }
}
