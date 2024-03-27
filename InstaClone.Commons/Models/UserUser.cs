using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Models
{
    public class UserUser
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int FollowerId { get; set; }
        public User Follower { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UserUser user &&
                   UserId == user.UserId &&
                   User.Equals(user.User) &&
                   FollowerId == user.FollowerId &&
                   User.Equals(user.Follower);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(UserId);
            hash.Add(User);
            hash.Add(FollowerId);
            hash.Add(Follower);
            return hash.ToHashCode();
        }
    }
}
