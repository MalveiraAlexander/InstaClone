using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstaClone.Commons.Requests
{
    public class CommentRequest
    {
        public string Text { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
