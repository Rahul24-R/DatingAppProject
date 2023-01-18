using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext                 //Inherting EntityFramework class
    {
        public DataContext(DbContextOptions options):base(options) //ctor with DbContextOptions and passing them to base class
        {
            
        }
        public DbSet<AppUser> Users { get; set; }   // Users will be the table name and the columns will be the props of AppUser
    }
}