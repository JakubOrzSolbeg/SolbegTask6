using DataRepository.Entities.Base;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Transfer : BaseEntity
{
    public DateTime TransferStart { get; set; }
    public DateTime? TransferEnd { get; set; }
    public TransferType TransferType { get; set; } = TransferType.Single;
    public int Amount { get; set; }
    public string? Comment { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

public class TransferConfiguration : BaseEntityTypeConfiguration<Transfer>
{
    public override void Configure(EntityTypeBuilder<Transfer> builder)
    {
        base.Configure(builder);
        builder.HasOne<Category>(t => t.Category)
            .WithMany(c => c.Transfers)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}