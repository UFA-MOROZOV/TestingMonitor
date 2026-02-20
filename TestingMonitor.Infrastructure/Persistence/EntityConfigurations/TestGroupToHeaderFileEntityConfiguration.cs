using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class TestGroupToHeaderFileEntityConfiguration : IEntityTypeConfiguration<TestGroupToHeaderFile>
{
    public void Configure(EntityTypeBuilder<TestGroupToHeaderFile> builder)
    {
        builder.HasKey(x => new
        {
            x.HeaderId,
            x.TestGroupId
        });

        builder.HasOne(x => x.HeaderFile)
            .WithMany(x => x.TestGroups)
            .HasForeignKey(x => x.HeaderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.TestGroup)
            .WithMany(x => x.HeaderFiles)
            .HasForeignKey(x => x.TestGroupId)   
            .OnDelete(DeleteBehavior.Cascade);
    }
}
