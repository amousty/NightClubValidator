using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        // PUT: api/IdCards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIdCard(int id, IdCard idCard)
        {

            if (IdCardVerificator(idCard))
            {
                // Need to define if it's the ID or nationalID
                // Card ID
                if (id != idCard.IdCardId)
                {
                    return BadRequest();
                }

                _context.Entry(idCard).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdCardExists(idCard.IdCardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetIdCard", new { id = idCard.IdCardId }, idCard);
            }
            else
            {
                return StatusCode(406, "Data not valid.");
            }
        }

        // POST: api/IdCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IdCard>> PostIdCard(IdCard idCard)
        {
            if (IdCardVerificator(idCard))
            {
                _context.IdCards.Add(idCard);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetIdCard", new { id = idCard.NationalId }, idCard);
            }
            else
            {
                return StatusCode(406, "Data not valid.");
            }
        }

        private bool IdCardVerificator(IdCard idCard)
        {
            // National ID is unique and cannot be the same as another one within the DB. 
            if (!_context.IdCards.Any(i => i.NationalId == idCard.NationalId && i.MemberId != idCard.MemberId))
            {
                if (!DateHelper.IsDateExpired(idCard.ExpiryDate) && DateHelper.IsDateExpired(idCard.CreatedOn))
                {
                    if (!IdCardExists(idCard.NationalId) && idCard.CardIsValid())
                    {
                        return true;
                    }
                }
            }
            return false;
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
        public bool IdCardExists(int id)
        {
            return _context.IdCards.Any(e => e.IdCardId == id);
        }
    }
}
