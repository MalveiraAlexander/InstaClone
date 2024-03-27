using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<PostReaction> PostReactions { get; set; } = new List<PostReaction>();
        public DateTime CreationDate { get; set; }
        public List<UserUser> Followers { get; set; } = new List<UserUser>();

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   NickName == user.NickName &&
                   Password == user.Password &&
                   EqualPosts(user.Posts) &&
                   EqualComments(user.Comments) &&
                   EqualPostReactions(user.PostReactions) &&
                   CreationDate == user.CreationDate &&
                   EqualFollowers(user.Followers);
        }

        private bool EqualPosts(List<Post> objPosts)
        {
            return Posts.SequenceEqual(objPosts);
        }

        private bool EqualComments(List<Comment> objComments)
        {
            return Comments.SequenceEqual(objComments);
        }

        private bool EqualPostReactions(List<PostReaction> objPostReactions)
        {
            return PostReactions.SequenceEqual(objPostReactions);
        }

        private bool EqualFollowers(List<UserUser> objFollowers)
        {
            return Followers.SequenceEqual(objFollowers);
        }



        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(NickName);
            hash.Add(Password);
            hash.Add(Posts);
            hash.Add(Comments);
            hash.Add(PostReactions);
            hash.Add(CreationDate);
            hash.Add(Followers);
            return hash.ToHashCode();
        }
    }
}
