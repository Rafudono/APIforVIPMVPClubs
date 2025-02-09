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
    public class TypeOfClubsController : ControllerBase
    {
        private readonly VipclubsContext _context;

        public TypeOfClubsController(VipclubsContext context)
        {
            _context = context;
        }

        // GET: api/TypeOfClubs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfClub>>> GetTypeOfClubs()
        {
            return await _context.TypeOfClubs.ToListAsync();
        }

        // GET: api/TypeOfClubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfClub>> GetTypeOfClub(int id)
        {
            var typeOfClub = await _context.TypeOfClubs.FindAsync(id);

            if (typeOfClub == null)
            {
                return NotFound();
            }

            return typeOfClub;
        }

        // PUT: api/TypeOfClubs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfClub(int id, TypeOfClub typeOfClub)
        {
            if (id != typeOfClub.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeOfClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfClubExists(id))
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

        // POST: api/TypeOfClubs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeOfClub>> PostTypeOfClub(TypeOfClub typeOfClub)
        {
            _context.TypeOfClubs.Add(typeOfClub);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeOfClub", new { id = typeOfClub.Id }, typeOfClub);
        }

        // DELETE: api/TypeOfClubs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfClub(int id)
        {
            var typeOfClub = await _context.TypeOfClubs.FindAsync(id);
            if (typeOfClub == null)
            {
                return NotFound();
            }

            _context.TypeOfClubs.Remove(typeOfClub);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeOfClubExists(int id)
        {
            return _context.TypeOfClubs.Any(e => e.Id == id);
        }
    }
}
