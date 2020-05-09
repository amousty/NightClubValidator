using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NightClubValidator.Models;

namespace NightClubValidator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly NightClubValidatorContext _context;

        public MembersController(NightClubValidatorContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await
                (
                    from member in _context.Members
                    join idCard in _context.IdCards on member.MemberId equals idCard.MemberId
                    join cardMember in _context.MemberCards on member.MemberId equals cardMember.MemberId
                    select new Member
                    {
                        MemberId = member.MemberId,
                        EmailAddress = member.EmailAddress,
                        PhoneNumber = member.PhoneNumber,
                        Birthdate = member.Birthdate,
                        IdCard = member.IdCard,
                        MemberCards = member.MemberCards
                    }
                ).ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members
                .Include(i => i.IdCard)
                .Include(c => c.MemberCards)
                .Where(c => c.MemberId == id)
                .FirstOrDefaultAsync(i => i.MemberId == id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(long id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            try
            {
                int memberStatus = member.IsValidUser();
                IdCard idCard = member.IdCard;
                if (memberStatus == 200 &&  idCard.CardIsValid())
                {
                    IdCardsController idCardsController = new IdCardsController(_context);
                    if (!idCardsController.IdCardExists(member.IdCard.NationalId))
                    {
                        _context.Members.Add(member);
                        await _context.SaveChangesAsync();
                    }

                    return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }


        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Member>> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return member;
        }

        private bool MemberExists(long id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
