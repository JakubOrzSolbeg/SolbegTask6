using DataRepository.Entities.Base;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;
    public DateTime AccountCreated { get; set; } = DateTime.Now;
    public string Passhash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public UserType UserType { get; set; } = UserType.User;
    public int Account { get; set; }
    
    public virtual ICollection<Transfer> Transfers { get; set; }
    public virtual ICollection<Category> CustomCategories { get; set; }
}

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.HasIndex(user => user.Login).IsUnique();
        builder.HasMany<Category>(u => u.CustomCategories)
            .WithOne(c => c.Creator)
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Transfer>(u => u.Transfers)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}