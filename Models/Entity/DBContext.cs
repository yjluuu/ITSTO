using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<InterfaceUser> InterfaceUser { get; set; }
        public virtual DbSet<InterfaceMapping> InterfaceMapping { get; set; }


        //public DBContext() { }
        //public static string conStr { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseMySql(conStr);
        //    }
        //}
    }
}
