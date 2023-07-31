﻿using Zust.Core.Abstraction;

namespace Zust.Entities.Models
{
    public class Comment : IEntity
    {
        public string? Id { get; set; }

        public string? PostId { get; set; }

        public virtual Post? Post { get; set; }

        public string? UserId { get; set; }

        public virtual User? User { get; set; }

        public string? Text { get; set; }
    }
}
