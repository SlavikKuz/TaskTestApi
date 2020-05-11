using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApi2c
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) 
            : base (options)
        { 
        }
                
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
