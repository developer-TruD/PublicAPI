using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicAPI.Data;
using PublicAPI.Models;

namespace PublicAPI.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ViewCyclesController : ControllerBase
    {
        private readonly ViewCyclesContext _context;

        public ViewCyclesController(ViewCyclesContext context)
        {
            _context = context;
        }

        //// GET: api/ViewCycles
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ViewCycles>>> GetView_Cycles()
        //{
        //    return await _context.View_Cycles.ToListAsync();
        //}

        //// GET: api/ViewCycles/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ViewCycles>> GetViewCycles(int id)
        //{
        //    var viewCycles = await _context.View_Cycles.FindAsync(id);

        //    if (viewCycles == null)
        //    {
        //        return NotFound();
        //    }

        //    return viewCycles;
        //}
        // GET: api/ViewCycles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewCycles>>> GetView_Cycles()
        {

            var authencationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(authencationHeaderValue.Parameter);
            string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");

            string s_password = credentials[1];
            int i_password = int.Parse(s_password);
            var viewcycle = await _context.View_Cycles.Where(viewcycle => viewcycle.ClientId == i_password).ToListAsync();
                return viewcycle;


           // return await _context.View_Cycles.ToListAsync();
        }

        private bool ViewCyclesExists(int id)
        {
            return _context.View_Cycles.Any(e => e.Id == id);
        }
    }
}
