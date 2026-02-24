using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class TestExecutionEntityConfiguration : IEntityTypeConfiguration<TestExecution>
{
    public void Configure(EntityTypeBuilder<TestExecution> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Test)
            .WithMany()
            .HasForeignKey(x => x.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CompilerTask)
            .WithMany(x => x.TestsExecuted)
            .HasForeignKey(x => x.CompilerTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
