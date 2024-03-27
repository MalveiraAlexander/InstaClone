using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstaClone.Commons.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public List<UserFollower> Followers { get; set; }
        public int FollowerCount { get; set; }
        public int PostCount { get; set; }
    }

    public class UserFollower
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
