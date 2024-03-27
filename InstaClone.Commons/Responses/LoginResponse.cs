using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
