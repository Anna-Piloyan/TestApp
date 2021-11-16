using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int? ContactId { get; set; }
        public Contact Contacts { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
        public Account()
        {
            Incidents = new List<Incident>();
        }
    }
}
