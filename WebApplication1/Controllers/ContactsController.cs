using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        MyDbContext db;
        public ContactsController(MyDbContext context)
        {
            db = context;
            if (!db.Contacts.Any())
            {
                db.Contacts.Add(new Contact { FirstName = "Tom", LastName = "Brown", Email = "tom@gmail.com" }); //, Accounts = { new Account() { AccountName = "Account4"} } });
                db.Contacts.Add(new Contact { FirstName = "Alice", LastName = "Smith", Email = "alice@gmail.com" }); //, Accounts = { new Account() { AccountName = "Account5" } } });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await db.Contacts.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            Contact user = await db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Contact>> Post(Contact user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Contacts.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Contact>> Put(Contact user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Contacts.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Delete(int id)
        {
            Contact user = db.Contacts.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Contacts.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}

