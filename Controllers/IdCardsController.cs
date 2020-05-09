using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NightClubValidator.Models;

namespace NightClubValidator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdCardsController : ControllerBase
    {
        private readonly NightClubValidatorContext _context;

        public IdCardsController(NightClubValidatorContext context)
        {
            _context = context;
        }

        // GET: api/IdCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdCard>>> GetIdCard()
        {
            return await _context.IdCards.ToListAsync();
        }

        // GET: api/IdCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IdCard>> GetIdCard(int id)
        {
            var idCard = await _context.IdCards.FindAsync(id);

            if (idCard == null)
            {
                return NotFound();
            }

            return idCard;
        }

        // POST: api/IdCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IdCard>> PostIdCard(IdCard idCard)
        {
            if (!IdCardExists(idCard.NationalId) && idCard.CardIsValid())
            {
                _context.IdCards.Add(idCard);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetIdCard", new { id = idCard.NationalId }, idCard);
            }
            else
            {
                return NotFound();
            }
            
        }

        // DELETE: api/IdCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IdCard>> DeleteIdCard(int id)
        {
            var idCard = await _context.IdCards.FindAsync(id);
            if (idCard == null)
            {
                return NotFound();
            }

            _context.IdCards.Remove(idCard);
            await _context.SaveChangesAsync();

            return idCard;
        }

        public bool IdCardExists(string nationalId)
        {
            return _context.IdCards.Any(e => e.NationalId == nationalId);
        }
    }
}
