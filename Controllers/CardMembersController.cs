using System;
using System.Collections.Generic;
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
    public class CardMembersController : ControllerBase
    {
        private readonly NightClubValidatorContext _context;

        public CardMembersController(NightClubValidatorContext context)
        {
            _context = context;
        }

        // GET: api/CardMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardMember>>> GetCardMember()
        {
            return await _context.CardMembers.ToListAsync();
        }

        // GET: api/CardMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardMember>> GetCardMember(long id)
        {
            var cardMember = await _context.CardMembers.FindAsync(id);

            if (cardMember == null)
            {
                return NotFound();
            }

            return cardMember;
        }

        // PUT: api/CardMembers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardMember(long id, CardMember cardMember)
        {
            if (id != cardMember.CardMemberId)
            {
                return BadRequest();
            }

            // Auto blacklist if not matching the conditions
            cardMember.IsBlacklist = !DateHelper.IsDateExpired(cardMember.BlacklistEndDate) && !cardMember.IsDeactivated ? false : true; 
            _context.Entry(cardMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardMemberExists(id))
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

        // POST: api/CardMembers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CardMember>> PostCardMember(CardMember cardMember)
        {
            // Part 1 : Get all previous card for same member
            List<CardMember> cardMemberListWithSameMember = _context.CardMembers.ToList().Where(
                c => 
                    c.MemberId == cardMember.MemberId 
                    && !c.IsBlacklist 
                    && !c.IsDeactivated
                ).ToList();

            // Part 2 : Set previous one to deactivated
            foreach (CardMember cardMemberToDeactivate in cardMemberListWithSameMember)
            {
                // Deactivate account for old one
                cardMemberToDeactivate.IsDeactivated = true;
                _context.Entry(cardMemberToDeactivate).State = EntityState.Modified;
            }

            // Part 3 : Add newmember card
            _context.CardMembers.Add(cardMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardMember", new { id = cardMember.CardMemberId }, cardMember);
        }

        // DELETE: api/CardMembers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CardMember>> DeleteCardMember(long id)
        {
            var cardMember = await _context.CardMembers.FindAsync(id);
            if (cardMember == null)
            {
                return NotFound();
            }

            _context.CardMembers.Remove(cardMember);
            await _context.SaveChangesAsync();

            return cardMember;
        }

        private bool CardMemberExists(long id)
        {
            return _context.CardMembers.Any(e => e.CardMemberId == id);
        }
    }
}
