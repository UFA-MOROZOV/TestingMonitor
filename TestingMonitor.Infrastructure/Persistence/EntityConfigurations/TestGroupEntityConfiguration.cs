using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class TestGroupEntityConfiguration : IEntityTypeConfiguration<TestGroup>
{
    public void Configure(EntityTypeBuilder<TestGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.SubGroups)
            .WithOne(x => x.ParentGroup)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tests)
            .WithOne(x => x.TestGroup)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
