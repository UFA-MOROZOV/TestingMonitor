using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class ExecutionTaskEntityConfiguration : IEntityTypeConfiguration<ExecutionTask>
{
    public void Configure(EntityTypeBuilder<ExecutionTask> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.TestGroup)
            .WithMany()
            .HasForeignKey(x => x.TestGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Test)
            .WithMany()
            .HasForeignKey(x => x.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Compiler)
            .WithMany()
            .HasForeignKey(x => x.CompilerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
