using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstaClone.Commons.Requests
{
    public class PostRequest
    {
        public string? Description { get; set; }
        public string? FileUrl { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
