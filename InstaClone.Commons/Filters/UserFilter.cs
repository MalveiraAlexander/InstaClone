using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Filters
{
    public class UserFilter : SortedBaseFilter
    {
        public DateTime? FromCreationDate { get; set; }
        public DateTime? ToCreationDate { get; set; }
        public string? NickName { get; set; }
    }
}
