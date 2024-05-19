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
    public class ActionCategoryForUsersController : ControllerBase
    {
        private readonly MafiaDbContext _context;

        public ActionCategoryForUsersController(MafiaDbContext context)
        {
            _context = context;
        }

        // GET: api/Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionCategoryForUser>>> GetActions([FromQuery] int page, [FromQuery] int size)
        {
            var query = _context.ActionCategoryForUsers
                .Include(e=>e.ActionForUsers)
                .OrderBy(e => e.OrderByField)
                .ToList();
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
        public async Task<ActionResult<ActionCategoryForUser>> GetAction(int id)
        {
            var actionCategoryForUser = await _context.ActionCategoryForUsers.FindAsync(id);

            if (actionCategoryForUser == null)
            {
                return NotFound();
            }

            return actionCategoryForUser;
        }

        // PUT: api/Actions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAction(int id, ActionCategoryForUser actionCategoryForUser)
        {
            if (id != actionCategoryForUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(actionCategoryForUser).State = EntityState.Modified;

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
        public async Task<ActionResult<Action>> PostAction(ActionCategoryForUser actionCategoryForUser)
        {
            _context.ActionCategoryForUsers.Add(actionCategoryForUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAction", new { id = actionCategoryForUser.Id }, actionCategoryForUser);
        }

        // DELETE: api/Actions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAction(int id)
        {
            var actionCategoryForUser = await _context.ActionCategoryForUsers.FindAsync(id);
            if (actionCategoryForUser == null)
            {
                return NotFound();
            }

            _context.ActionCategoryForUsers.Remove(actionCategoryForUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActionExists(int id)
        {
            return _context.ActionCategoryForUsers.Any(e => e.Id == id);
        }
    }
}
