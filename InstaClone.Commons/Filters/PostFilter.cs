using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Filters
{
    public class PostFilter : SortedBaseFilter
    {
        public int? UserId { get; set; }
        public DateTime? FromCreationDate { get; set; }
        public DateTime? ToCreationDate { get; set; }
    }
}
