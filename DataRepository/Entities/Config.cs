using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Config : SingletonEntity
{
    public DateTime LastTimeTransferCount { get; set; } = DateTime.Now.AddDays(-1);
}

public class ConfigConfig : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> builder)
    {
        builder.HasKey(o => o.Index);
        builder.Property(o => o.Index).HasDefaultValue(1);
    }
}