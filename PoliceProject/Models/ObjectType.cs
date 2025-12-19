using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliceProject.Models;

[Table("ObjectType")]
public partial class ObjectType
{
    [Key]
    public int IdObjectType { get; set; }

    [Column("ObjectType")]
    [StringLength(50)]
    public string ObjectType1 { get; set; } = null!;

    [InverseProperty("IdObjectTypeNavigation")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
