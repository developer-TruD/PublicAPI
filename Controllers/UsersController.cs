using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicAPI.Data;
using PublicAPI.Models;

namespace PublicAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

       
        [HttpGet("GetUser")]
        public async Task<ActionResult<Users>> GetUser()
        {
            string s_username = HttpContext.User.Identity.Name;
            var user = await _context.api_users.Where(user => user.username == s_username).FirstOrDefaultAsync();

            user.username = null;
            user.password = null;
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        

        private bool UsersExists(int id)
        {
            return _context.api_users.Any(e => e.Id == id);
        }
    }
}
