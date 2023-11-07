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
    public class PrivilegeController : ControllerBase
    {
        private readonly DbEntityContext _context;

        public PrivilegeController(DbEntityContext context)
        {
            _context = context;
        }

        // GET: api/Privilege
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Privilege>>> Getprivilege()
        {
          if (_context.privilege == null)
          {
              return NotFound();
          }
            return await _context.privilege.ToListAsync();
        }

        // GET: api/Privilege/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Privilege>> GetPrivilege(int id)
        {
          if (_context.privilege == null)
          {
              return NotFound();
          }
            var privilege = await _context.privilege.FindAsync(id);

            if (privilege == null)
            {
                return NotFound();
            }

            return privilege;
        }

        private bool PrivilegeExists(int id)
        {
            return (_context.privilege?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
