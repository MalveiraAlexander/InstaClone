using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<PostReaction> Reactions { get; set; } = new List<PostReaction>();
        public string? FileUrl { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Post post &&
                   Id.Equals(post.Id) &&
                   Description == post.Description &&
                   CreationDate == post.CreationDate &&
                   UserId == post.UserId &&
                   User.Equals(post.User) &&
                   EqualComments(post.Comments) &&
                   EqualReactions(post.Reactions) &&
                   FileUrl == post.FileUrl;
        }

        private bool EqualComments(List<Comment> objComments)
        {
            return Comments.SequenceEqual(objComments);
        }

        private bool EqualReactions(List<PostReaction> objReactions)
        {
            return Reactions.SequenceEqual(objReactions);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Description);
            hash.Add(CreationDate);
            hash.Add(UserId);
            hash.Add(User);
            hash.Add(Comments);
            hash.Add(Reactions);
            hash.Add(FileUrl);
            return hash.ToHashCode();
        }
    }
}
