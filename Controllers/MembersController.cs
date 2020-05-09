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
                    join cardMember in _context.CardMembers on member.MemberId equals cardMember.MemberId
                    select new Member
                    {
                        MemberId = member.MemberId,
                        Mail = member.Mail,
                        Phone = member.Phone,
                        Birthdate = member.Birthdate,
                        IdCard = member.IdCard,
                        CardMembers = member.CardMembers
                    }
                ).ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members
                .Include(i => i.IdCard)
                .Include(c => c.CardMembers)
                .Where(c => c.MemberId == id)
                .FirstOrDefaultAsync(i => i.MemberId == id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
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
                //int idCardStatus = member.IdCard.IsValidIdCard();
                if (memberStatus == 200)
                {
                    

                    // Members
                    _context.Members.Add(member);
                    await _context.SaveChangesAsync();

                    /*
                     // We need to ensure that eveyrthing is ok before save -> transactional
                   // Members card
                    foreach (CardMembers cardMember in member.CardMembers)
                    {
                        await cardMembersController.PostCardMember();
                    }

                    // ID card
                    await idCardsController.PostIdCard(member.IdCards);
                     */

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

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
