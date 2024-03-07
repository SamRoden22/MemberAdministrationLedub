using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberAdministrationLedûbCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberAdministrationLedûbDAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }

        //public DbSet<Team> Teams { get; set; }

        public DbSet<TeamMembers> TeamMembers { get; set; }
    }
}
