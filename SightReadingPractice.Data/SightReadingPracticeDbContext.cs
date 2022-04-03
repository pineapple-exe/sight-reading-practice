using Microsoft.EntityFrameworkCore;
using SightReadingPractice.Data.Configurations;
using SightReadingPractice.Domain.Entities;

namespace SightReadingPractice.Data
{
    public class SightReadingPracticeDbContext : DbContext
    {
        //private string _connectionString = "Server=localhost;Database=SightReadingPractice;Trusted_Connection=True;";

        public DbSet<NoteExerciseResult> NoteExerciseResults { get; set; }

        //public SightReadingPracticeDbContext()
        //{

        //}

        public SightReadingPracticeDbContext(DbContextOptions<SightReadingPracticeDbContext> contextOptions) : base(contextOptions)
        {

        }

        //public SightReadingPracticeDbContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(optionsBuilder.);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteExerciseResultsConfiguration());
        }
    }
}
