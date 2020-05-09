using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NightClubValidator.Helper;
using NightClubValidator.Models;

namespace NightClubValidator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberCardsController : ControllerBase
    {
        private readonly NightClubValidatorContext _context;

        public MemberCardsController(NightClubValidatorContext context)
        {
            _context = context;
        }

        // GET: api/MemberCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberCard>>> GetMemberCards()
        {
            return await _context.MemberCards.ToListAsync();
        }

        // GET: api/MemberCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberCard>> GetMemberCards(int id)
        {
            var MemberCards = await _context.MemberCards.FindAsync(id);

            if (MemberCards == null)
            {
                return NotFound();
            }

            return MemberCards;
        }

        // PUT: api/MemberCards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberCards(int id, MemberCard MemberCards)
        {
            if (id != MemberCards.MemberCardId)
            {
                return BadRequest();
            }

            // Auto blacklist if not matching the conditions
            MemberCards.IsDeactivated = DateHelper.IsDateExpired(MemberCards.ExpiryCardDate) ? true : false;
            MemberCards.IsBlacklist = !DateHelper.IsDateExpired(MemberCards.BlacklistEndDate) && !MemberCards.IsDeactivated ? false : true; 
            
            _context.Entry(MemberCards).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                if (!MemberCardsExists(id))
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

        // POST: api/MemberCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MemberCard>> PostMemberCards(MemberCard MemberCards)
        {
            // Part 1 : Get all previous card for same member
            List<MemberCard> MemberCardsListWithSameMember = _context.MemberCards.ToList().Where(
                c => 
                    c.MemberId == MemberCards.MemberId 
                    && !c.IsBlacklist 
                    && !c.IsDeactivated
                ).ToList();

            // Part 2 : Set previous one to deactivated
            foreach (MemberCard MemberCardsToDeactivate in MemberCardsListWithSameMember)
            {
                // Deactivate account for old one
                MemberCardsToDeactivate.IsDeactivated = true;
                _context.Entry(MemberCardsToDeactivate).State = EntityState.Modified;
            }

            // Part 3 : Add new member card
            _context.MemberCards.Add(MemberCards);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberCards", new { id = MemberCards.MemberCardId }, MemberCards);
        }

        // DELETE: api/MemberCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MemberCard>> DeleteMemberCards(int id)
        {
            var MemberCards = await _context.MemberCards.FindAsync(id);
            if (MemberCards == null)
            {
                return NotFound();
            }
            
            _context.MemberCards.Remove(MemberCards);
            await _context.SaveChangesAsync();
            return MemberCards;
        }

        private bool MemberCardsExists(int id)
        {
            return _context.MemberCards.Any(e => e.MemberCardId == id);
        }
    }
}
