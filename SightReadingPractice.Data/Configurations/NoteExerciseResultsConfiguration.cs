using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SightReadingPractice.Domain;
using SightReadingPractice.Domain.Entities;
using System;

namespace SightReadingPractice.Data.Configurations
{
    class NoteExerciseResultsConfiguration : IEntityTypeConfiguration<NoteExerciseResult>
    {
        public void Configure(EntityTypeBuilder<NoteExerciseResult> builder)
        {
            builder.Property(e => e.ClefType).IsRequired().HasConversion(v => v.ToString(), v => (ClefType)Enum.Parse(typeof(ClefType), v));
            builder.Property(e => e.DateTime).IsRequired();
            builder.Property(e => e.ActualTone).IsRequired();
            builder.Property(e => e.SeptimaArea).IsRequired();
            builder.Property(e => e.Success).IsRequired();
        }
    }
}
