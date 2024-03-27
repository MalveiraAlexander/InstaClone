using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Models
{
    public class PostReaction
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public int ReactionTypeId { get; set; }
        public ReactionType ReactionType { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PostReaction reaction &&
                   Id.Equals(reaction.Id) &&
                   UserId == reaction.UserId &&
                   User.Equals(reaction.User) &&
                   PostId.Equals(reaction.PostId) &&
                   Post.Equals(reaction.Post) &&
                   ReactionTypeId == reaction.ReactionTypeId &&
                   ReactionType.Equals(reaction.ReactionType);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(UserId);
            hash.Add(User);
            hash.Add(PostId);
            hash.Add(Post);
            hash.Add(ReactionTypeId);
            hash.Add(ReactionType);
            return hash.ToHashCode();
        }
    }
}
