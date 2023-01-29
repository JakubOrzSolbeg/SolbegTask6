using DataRepository.Entities.Base;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class BankUser : BaseEntity
{
    public string Login { get; set; } = null!;
    public DateTime AccountCreated { get; set; } = DateTime.Now;
    public string Passhash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public UserType UserType { get; set; } = UserType.User;
    public int Account { get; set; }
    public int? SpendingLimit { get; set; }
    
    public virtual ICollection<Subscription> Subscriptions { get; set; }
    public virtual ICollection<Category> CustomCategories { get; set; }
    public virtual ICollection<Transfer> Transfers { get; set; }
}

public class UserConfiguration : BaseEntityTypeConfiguration<BankUser>
{
    public override void Configure(EntityTypeBuilder<BankUser> builder)
    {
        base.Configure(builder);
        builder.HasIndex(user => user.Login).IsUnique();
        builder.HasMany<Category>(u => u.CustomCategories)
            .WithOne(c => c.Creator)
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Subscription>(u => u.Subscriptions)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany<Transfer>(u => u.Transfers)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}