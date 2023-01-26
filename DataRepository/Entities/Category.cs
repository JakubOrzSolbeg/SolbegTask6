using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsCustom { get; set; } = false;
    public bool IsIncome { get; set; } = false;
    public int? CreatorId { get; set; }
    public virtual User? Creator { get; set; }
    public virtual ICollection<Transfer> Transfers { get; set; }
}

public class CategoryConfiguration : BaseEntityTypeConfiguration<Category>
{
    
}