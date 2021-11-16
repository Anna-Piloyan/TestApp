using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Укажите фамилию пользователя")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Укажите имейл пользователя")]
        public string Email { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public Contact()
        {
            Accounts = new List<Account>();
        }
    }
}
