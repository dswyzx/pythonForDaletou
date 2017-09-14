using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DotNetCoreTest.Models;

namespace DotNetCoreTest.Data
{
    public class EFDbContext : DbContext
    {

        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
            
        }
        public DbSet<DLTInfo> DLTList { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }

    }
}
