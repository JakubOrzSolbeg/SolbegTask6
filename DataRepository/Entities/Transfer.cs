using DataRepository.Entities.Base;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Transfer : BaseEntity
{
    public DateTime TransferTime { get; set; } = DateTime.Now;
    public TransferType TransferType { get; set; } = TransferType.Single;
    public int SubscriptionId { get; set; }
    public int UserId { get; set; }
    

    public virtual User User { get; set; } = null!;
    public virtual Subscription Subscription { get; set; } = null!;

}

public class TransferTypeConfiguration : BaseEntityTypeConfiguration<Transfer>
{
    public override void Configure(EntityTypeBuilder<Transfer> builder)
    {
        base.Configure(builder);
        builder.HasOne<Subscription>(t => t.Subscription)
            .WithMany(s => s.Transfers)
            .HasForeignKey(t => t.SubscriptionId);
    }
}