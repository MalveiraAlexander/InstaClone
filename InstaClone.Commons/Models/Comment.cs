using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Comment comment &&
                   Id.Equals(comment.Id) &&
                   Text == comment.Text &&
                   CreationDate == comment.CreationDate &&
                   UserId == comment.UserId &&
                   User.Equals(comment.User) &&
                   PostId.Equals(comment.PostId) &&
                   Post.Equals(comment.Post);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Text);
            hash.Add(CreationDate);
            hash.Add(UserId);
            hash.Add(User);
            hash.Add(PostId);
            hash.Add(Post);
            return hash.ToHashCode();
        }
    }
}
