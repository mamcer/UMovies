using System.Data.Entity;

namespace UMovies.Data
{
    public class UMoviesEntities : DbContext
    {
        public UMoviesEntities() : base("name=UMovies")
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Movie>()
                .HasMany(mc => mc.MovieFiles)
                .WithRequired()
                .WillCascadeOnDelete(true);
        }
    }
}