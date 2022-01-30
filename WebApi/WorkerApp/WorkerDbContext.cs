using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WorkerApp
{
    public class WorkerDbContext: DbContext
    {
        public WorkerDbContext(DbContextOptions<WorkerDbContext> options) : base(options)
        {

        }
        public DbSet<ItemDbEntity> ItemDbEntities { get; set; }
    }
}
