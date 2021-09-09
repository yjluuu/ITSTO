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
        public virtual DbSet<DishCategory> DishCategory { get; set; }
        public virtual DbSet<Dish> Dish { get; set; }
        public virtual DbSet<InterfaceLogs> InterfaceLogs { get; set; }
        public virtual DbSet<AppSetting> AppSetting { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }

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
