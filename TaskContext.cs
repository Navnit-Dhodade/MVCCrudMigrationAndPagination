using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NimapTask.Models
{
    public class TaskContext:DbContext
    {
        public DbSet<CategoryMaster> CategoryMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
    }
}