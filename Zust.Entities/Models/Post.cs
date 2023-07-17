using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entities.Models
{
    public class Post
    {
        public string? Id { get; set; }

        public string? Description { get; set; }

        public string? ContentUrl { get; set; }

        public bool IsVideo { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
