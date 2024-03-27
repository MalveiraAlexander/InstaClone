using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Filters
{
    public class SortedBaseFilter : BaseFilter
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? Order { get; set; }
    }
}
