using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIforVIPMVPClubs;

namespace APIforVIPMVPClubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusApplicationsController : ControllerBase
    {
        private readonly VipclubsContext _context;

        public StatusApplicationsController(VipclubsContext context)
        {
            _context = context;
        }

        // GET: api/StatusApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusApplication>>> GetStatusApplications()
        {
            return await _context.StatusApplications.ToListAsync();
        }

        // GET: api/StatusApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusApplication>> GetStatusApplication(int id)
        {
            var statusApplication = await _context.StatusApplications.FindAsync(id);

            if (statusApplication == null)
            {
                return NotFound();
            }

            return statusApplication;
        }

        // PUT: api/StatusApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusApplication(int id, StatusApplication statusApplication)
        {
            if (id != statusApplication.Id)
            {
                return BadRequest();
            }

            _context.Entry(statusApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StatusApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusApplication>> PostStatusApplication(StatusApplication statusApplication)
        {
            _context.StatusApplications.Add(statusApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusApplication", new { id = statusApplication.Id }, statusApplication);
        }

        // DELETE: api/StatusApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusApplication(int id)
        {
            var statusApplication = await _context.StatusApplications.FindAsync(id);
            if (statusApplication == null)
            {
                return NotFound();
            }

            _context.StatusApplications.Remove(statusApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusApplicationExists(int id)
        {
            return _context.StatusApplications.Any(e => e.Id == id);
        }
    }
}
