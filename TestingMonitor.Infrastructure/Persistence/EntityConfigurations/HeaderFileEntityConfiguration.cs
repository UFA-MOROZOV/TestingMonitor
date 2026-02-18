using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence.EntityConfigurations;

internal sealed class HeaderFileEntityConfiguration : IEntityTypeConfiguration<HeaderFile>
{
    public void Configure(EntityTypeBuilder<HeaderFile> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
