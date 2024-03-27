using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Models
{
    public class ReactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReactionType type &&
                   Id == type.Id &&
                   Name == type.Name;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            return hash.ToHashCode();
        }
    }
}
