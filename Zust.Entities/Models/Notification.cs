using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entities.Models
{
    public class Notification
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

    }
}
