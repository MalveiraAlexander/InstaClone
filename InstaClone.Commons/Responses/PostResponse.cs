using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string? FileUrl { get; set; }
        public UserResponse User { get; set; }
        public int LikeCount { get; set; }
    }
}
