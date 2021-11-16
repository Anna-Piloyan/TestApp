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
    public class IncidentsController : ControllerBase
    {
        MyDbContext db;
        public IncidentsController(MyDbContext context)
        {
            db = context;
            if (!db.Incidents.Any())
            {
                db.Incidents.Add(new Incident { Description = "Description1", AccountId = 1 });
                db.Incidents.Add(new Incident { Description = "Description2", AccountId = 2 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> Get()
        {
            return await db.Incidents.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            Incident incident = await db.Incidents.FirstOrDefaultAsync(x => x.Id == id);
            if (incident == null)
                return NotFound();
            return new ObjectResult(incident);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Incident>> Post(Incident incident)
        {
            if (incident == null)
            {
                return BadRequest();
            }

            db.Incidents.Add(incident);
            await db.SaveChangesAsync();
            return Ok(incident);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Incident>> Put(Incident incident)
        {
            if (incident == null)
            {
                return BadRequest();
            }
            if (!db.Incidents.Any(x => x.Id == incident.Id))
            {
                return NotFound();
            }

            db.Update(incident);
            await db.SaveChangesAsync();
            return Ok(incident);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Incident>> Delete(int id)
        {
            Incident incident = db.Incidents.FirstOrDefault(x => x.Id == id);
            if (incident == null)
            {
                return NotFound();
            }
            db.Incidents.Remove(incident);
            await db.SaveChangesAsync();
            return Ok(incident);
        }
    }
}

