using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstaClone.Commons.Requests
{
    public class PostReactionRequest
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public Guid PostId { get; set; }
        public int ReactionTypeId { get; set; }
    }
}
