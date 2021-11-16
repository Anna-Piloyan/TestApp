using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? AccountId { get; set; }
        public Account Account { get; set; }
    }
}
