using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }

    [NotMapped]
    public override DateTime? UpdatedAt { get => base.UpdatedAt; set => base.UpdatedAt = value; }
}



