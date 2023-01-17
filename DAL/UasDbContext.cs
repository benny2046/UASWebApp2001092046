using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UASWebApp2001092046.Models;

namespace UASWebApp2001092046.DAL
{
    public class UasDbContext : IdentityDbContext
    {
        public UasDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> Products {get; set;}
    }
    
}