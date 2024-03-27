using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Exceptions
{
    public class NotFoundException : CustomException
    {
        private static readonly string message = "The record not found";

        public NotFoundException() : base(HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
