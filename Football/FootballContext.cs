using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;



namespace Football
{
    class FootballContext : DbContext
    {
        public DbSet<Team > Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"");
        }

    }
}
