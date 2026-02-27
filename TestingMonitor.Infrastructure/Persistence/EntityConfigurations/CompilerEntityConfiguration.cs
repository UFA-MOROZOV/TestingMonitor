using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class CompilerEntityConfiguration : IEntityTypeConfiguration<Compiler>
{
    public void Configure(EntityTypeBuilder<Compiler> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.Name, x.Version })
            .IsUnique();
    }
}
