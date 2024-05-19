using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Mafia.Application;
using Mafia.Domain.Entities;
using Mafia.Application.Paggination;
using Mafia.Domain.Entities.Privilegios;
using Mafia.Domain.Data.Adapters;

namespace Mafia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ActionForUsersController : ControllerBase
    {
        private readonly MafiaDbContext _context;

        public ActionForUsersController(MafiaDbContext context)
        {
            _context = context;
        }

        // GET: api/Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionForUser>>> GetActions([FromQuery] int page, [FromQuery] int size)
        {
            var query = _context.ActionForUsers
                .OrderBy(e => e.Id).ToList();
            try
            {
                var list = await PaginationService.GetPagination(query, page, size);
                return new JsonResult(list);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        // GET: api/Actions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActionForUser>> GetAction(int id)
        {
            var ageGroupAndOrganisation = await _context.ActionForUsers.FindAsync(id);

            if (ageGroupAndOrganisation == null)
            {
                return NotFound();
            }

            return ageGroupAndOrganisation;
        }

        // PUT: api/Actions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAction(int id, ActionForUser ageGroupAndOrganisation)
        {
            if (id != ageGroupAndOrganisation.Id)
            {
                return BadRequest();
            }

            _context.Entry(ageGroupAndOrganisation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionExists(id))
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

        // POST: api/Actions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Action>> PostAction(ActionForUser ageGroupAndOrganisation)
        {
            _context.ActionForUsers.Add(ageGroupAndOrganisation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAction", new { id = ageGroupAndOrganisation.Id }, ageGroupAndOrganisation);
        }

        // DELETE: api/Actions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            var ageGroupAndOrganisation = await _context.ActionForUsers.FindAsync(id);
            if (ageGroupAndOrganisation == null)
            {
                return NotFound();
            }

            _context.ActionForUsers.Remove(ageGroupAndOrganisation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActionExists(int id)
        {
            return _context.ActionForUsers.Any(e => e.Id == id);
        }
    }
}
