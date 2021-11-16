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
    public class AccountsController : ControllerBase
    {
        MyDbContext db;
        public AccountsController(MyDbContext context)
        {
            db = context;
            if (!db.Accounts.Any())
            {
                db.Accounts.Add(new Account { AccountName = "Account1", ContactId = 1, Incidents = { new Incident() { Description = "Description4" } } });
                db.Accounts.Add(new Account { AccountName = "Account2", ContactId = 2, Incidents = { new Incident() { Description = "Description3" } } });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            return await db.Accounts.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(string accountName)
        {
            Account account = await db.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);
            if (account == null)
                return NotFound();
            return new ObjectResult(account);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Account>> Post(Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }

            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            return Ok(account);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Account>> Put(Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }
            if (!db.Accounts.Any(x => x.Id == account.Id))
            {
                return NotFound();
            }

            db.Update(account);
            await db.SaveChangesAsync();
            return Ok(account);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Account>> Delete(int id)
        {
            Account account = db.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            db.Accounts.Remove(account);
            await db.SaveChangesAsync();
            return Ok(account);
        }
    }
}

