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

        // POST: api/CardMembers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CardMember>> PostCardMember(CardMember cardMember)
        {
             
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
