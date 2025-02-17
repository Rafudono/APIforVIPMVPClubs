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
    public class ClubsController : ControllerBase
    {
        private readonly VipclubsContext _context;

        public ClubsController(VipclubsContext context)
        {
            _context = context;
        }

        // GET: api/Clubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            return await _context.Clubs.Include(s=>s.IdBossNavigation).Include(s=>s.IdTypeNavigation).AsNoTracking().ToListAsync();
        }
        [HttpPost("GetFiltredClubs")]
        public async Task<ActionResult<List<Club>>> FilterClubs(TypeOfClub type) 
        {
            return await _context.Clubs.Include(s => s.IdTypeNavigation).Include(s => s.IdBossNavigation).Where(s => s.IdType == type.Id).ToListAsync();
        }
        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(int id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            return club;
        }
        [HttpPost("NewMember")]
        public async Task<ActionResult> PostClub(Appls appls)
        {
            var original = _context.Application1s.
                           FirstOrDefault(s => s.Id == appls.Id);
            Application1 apples = original;
            apples.IdStatus = appls.IdStatus;
            _context.Entry(original).CurrentValues.SetValues(apples);

            if (appls.IdStatus == 2)
            {
                var user = _context.Users.FirstOrDefault(s => s.Id == appls.IdApplicant);
                var originalClub = _context.Clubs.FirstOrDefault(s => s.Id == appls.IdClub);
                Club club = originalClub;
                club.Users.Add(user);
                _context.Entry(originalClub).CurrentValues.SetValues(club);

            }
            await _context.SaveChangesAsync();
            return Ok();
        }
        // PUT: api/Clubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(int id, Club club)
        {
            if (id != club.Id)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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

        // POST: api/Clubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Club>> PostClub(Club club)
        {
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.Id }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }
    }
}
