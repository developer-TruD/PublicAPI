using Microsoft.EntityFrameworkCore;
using PublicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAPI.Data
{
    public class ViewCyclesContext : DbContext
    {
        public ViewCyclesContext(DbContextOptions<ViewCyclesContext> options) : base(options)
        {

        }
        public DbSet<ViewCycles> View_Cycles { get; set; }
    }
}
