using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Database;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class {{Name}}Controller : ControllerBase
    {
        private readonly {{Name}}DbContext _context;

        public {{Name}}Controller({{Name}}DbContext context)
        {
            _context = context;
        }

        // GET: api/{{Name}}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<{{Name}}>>> Get{{Name}}()
        {
            return await _context.{{Name}}.ToListAsync();
        }

        // GET: api/{{Name}}/5
        [HttpGet("{id}")]
        public async Task<ActionResult<{{Name}}>> Get{{Name}}(Guid id)
        {
            var {{Name}} = await _context.{{Name}}.FindAsync(id);

            if ({{Name}} == null)
            {
                return NotFound();
            }

            return {{Name}};
        }

        // PUT: api/{{Name}}/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put{{Name}}(Guid id, {{Name}} {{Name}})
        {
            if (id != {{Name}}.Id)
            {
                return BadRequest();
            }

            _context.Entry({{Name}}).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!{{Name}}Exists(id))
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

        // POST: api/{{Name}}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<{{Name}}>> Post{{Name}}({{Name}} {{Name}})
        {
            _context.{{Name}}.Add({{Name}});
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get{{Name}}", new { id = {{Name}}.Id }, {{Name}});
        }

        // DELETE: api/{{Name}}/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<{{Name}}>> Delete{{Name}}(Guid id)
        {
            var {{Name}} = await _context.{{Name}}.FindAsync(id);
            if ({{Name}} == null)
            {
                return NotFound();
            }

            _context.{{Name}}.Remove({{Name}});
            await _context.SaveChangesAsync();

            return {{Name}};
        }

        private bool {{Name}}Exists(Guid id)
        {
            return _context.{{Name}}.Any(e => e.Id == id);
        }
    }
}
