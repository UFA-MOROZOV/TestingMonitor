using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class TestToHeaderFileEntityConfiguration : IEntityTypeConfiguration<TestToHeaderFile>
{
    public void Configure(EntityTypeBuilder<TestToHeaderFile> builder)
    {
        builder.HasKey(x => new
        {
            x.HeaderId,
            x.TestId
        });

        builder.HasOne(x => x.HeaderFile)
            .WithMany()
            .HasForeignKey(x => x.HeaderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Test)
            .WithMany(x => x.HeaderFiles)
            .HasForeignKey(x => x.TestId)   
            .OnDelete(DeleteBehavior.Cascade);
    }
}
