using AngularJSDemo.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AngularJSDemo.MvcWebApi.Models
{
    public class MovieContext : DbContext
    {

        public MovieContext()
            : base("MovieContext")
        {
            Database.SetInitializer(new MovieInitializer());
            this.Configuration.LazyLoadingEnabled = false;

            // because EntityFramework creates a 'proxy' of our class.
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // fucking annoying..
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // one-to-many
            modelBuilder.Entity<Director>().HasMany<Movie>(d => d.Movies)
                .WithRequired(m => m.Director).HasForeignKey(m => m.DirectorId);

            //modelBuilder.Entity<Movie>().HasRequired(m => m.Director)
            //    .WithMany(d => d.Movies);

            // .WillCascadeOnDelete(true);
        }
    }
}