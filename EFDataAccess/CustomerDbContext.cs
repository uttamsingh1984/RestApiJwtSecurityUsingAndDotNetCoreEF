using EFDataAccess.IdentityModels;
using EFDataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess
{
    public class CustomerDbContext : IdentityDbContext<AppUser>
    {
        public CustomerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Customer> Customers { get; set; }
    }
}
