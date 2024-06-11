using Microsoft.EntityFrameworkCore;
using StuentWebAPI.Model;
using System.Collections.Generic;

namespace StuentWebAPI.DataContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options)
        {
        }
      
        public  DbSet<Student> Student { get; set; }
    }
}