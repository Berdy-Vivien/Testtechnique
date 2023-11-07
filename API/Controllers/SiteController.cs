using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly DbEntityContext _context;

        public SiteController(DbEntityContext context)
        {
            _context = context;
        }

        // GET: api/Site
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> Getsite()
        {
          if (_context.site == null)
          {
              return NotFound();
          }
            return await _context.site.ToListAsync();
        }

        // GET: api/Site/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Site>> GetSite(int id)
        {
          if (_context.site == null)
          {
              return NotFound();
          }
            var site = await _context.site.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // PUT: api/Site/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutSite(int id, Site site)
        {
            if (id != site.Id)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Site
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Site>> PostSite(Site site)
        {
          if (_context.site == null)
          {
              return Problem("Entity set 'DbEntityContext.site'  is null.");
          }
            _context.site.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSite", new { id = site.Id }, site);
        }

        // DELETE: api/Site/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            if (_context.site == null)
            {
                return NotFound();
            }
            var site = await _context.site.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.site.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteExists(int id)
        {
            return (_context.site?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
