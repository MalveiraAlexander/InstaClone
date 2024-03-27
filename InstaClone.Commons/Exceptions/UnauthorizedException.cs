using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        private static readonly string message = "Unauthorized";

        public UnauthorizedException() : base(HttpStatusCode.Unauthorized, message)
        {
        }

        public UnauthorizedException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
