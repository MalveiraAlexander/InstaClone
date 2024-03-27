using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Responses
{
    public class Response<T>
    {
        public long Count { get; set; }
        public List<T>? Data { get; set; }
    }
}
