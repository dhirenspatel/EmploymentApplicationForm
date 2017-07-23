using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EmployInformationSystem.Models
{
    public class EmployeeDbContext : DbContext
    {
        #region Constructor
        public EmployeeDbContext() : base("EmployeeConnection") { }
        #endregion

        #region Property
        public DbSet<Employment> EmploymentDB { get; set; }
        public DbSet<Education> EducationDB { get; set; }
        public DbSet<ApplicationInfo> ApplicationInfoDB { get; set; }
        public DbSet<Contact> ContactDB { get; set; }


        #endregion

        #region Method
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Changing Database table name to Metadata
            //Database.SetInitializer<EmployeeDbContext>(null);
            //base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}