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
    public class Application1Controller : ControllerBase
    {
        private readonly VipclubsContext _context;

        public Application1Controller(VipclubsContext context)
        {
            _context = context;
        }

        // GET: api/Application1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application1>>> GetApplication1s()
        {
            return await _context.Application1s.ToListAsync();
        }
        [HttpPost("GetMyAppls")]
        public async Task<ActionResult<List<Application1>>> GetMyAppls(User applicator)
        {
            return await _context.Application1s.Include(s=>s.IdTypeNavigation).Include(s=>s.IdClubNavigation).Include(s=>s.IdStatusNavigation).Where(s=>s.IdApplicant==applicator.Id).ToListAsync();
        }
        // GET: api/Application1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application1>> GetApplication1(int id)
        {
            var application1 = await _context.Application1s.FindAsync(id);

            if (application1 == null)
            {
                return NotFound();
            }

            return application1;
        }

        // PUT: api/Application1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication1(int id, Application1 application1)
        {
            if (id != application1.Id)
            {
                return BadRequest();
            }

            _context.Entry(application1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Application1Exists(id))
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

        // POST: api/Application1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostApplication1(Appls appls)
        {
            Application1 application = new Application1() { IdStatus = appls.IdStatus, IdType = appls.IdType, Image = appls.Image, Title = appls.Title, Description = appls.Description, DateOfFiling = appls.DateOfFiling, IdApplicant = appls.IdApplicant };
            _context.Application1s.Add(application);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        // DELETE: api/Application1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication1(int id)
        {
            var application1 = await _context.Application1s.FindAsync(id);
            if (application1 == null)
            {
                return NotFound();
            }

            _context.Application1s.Remove(application1);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Application1Exists(int id)
        {
            return _context.Application1s.Any(e => e.Id == id);
        }
    }
}
