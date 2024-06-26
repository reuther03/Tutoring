﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutoring.Domain.Competences;
using Tutoring.Infrastructure.Database.Converters;

namespace Tutoring.Infrastructure.Database.Configurations;

public class CompetencesGroupConfiguration : IEntityTypeConfiguration<CompetenceGroup>
{
    public void Configure(EntityTypeBuilder<CompetenceGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion<NameConverter>()
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion<DescriptionConverter>()
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(x => x.Competences)
            .WithOne()
            .HasForeignKey("CompetenceGroupId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}