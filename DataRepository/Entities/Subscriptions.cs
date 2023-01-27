using DataRepository.Entities.Base;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Subscription : BaseEntity
{
    public DateTime SubscriptionStart { get; set; }
    public DateTime? SubscriptionEnd { get; set; }
    public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Monthly;
    public int Amount { get; set; }
    public string? Comment { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Transfer> Transfers { get; set; }
}

public class TransferConfiguration : BaseEntityTypeConfiguration<Subscription>
{
    public override void Configure(EntityTypeBuilder<Subscription> builder)
    {
        base.Configure(builder);
        builder.HasOne<Category>(t => t.Category)
            .WithMany(c => c.Transfers)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}