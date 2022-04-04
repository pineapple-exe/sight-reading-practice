using Microsoft.EntityFrameworkCore;
using SightReadingPractice.Data.Configurations;
using SightReadingPractice.Domain.Entities;

namespace SightReadingPractice.Data
{
    public class SightReadingPracticeDbContext : DbContext
    {
        public DbSet<NoteExerciseResult> NoteExerciseResults { get; set; }

        public SightReadingPracticeDbContext()
        {

        }

        public SightReadingPracticeDbContext(DbContextOptions<SightReadingPracticeDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteExerciseResultsConfiguration());
        }
    }
}
