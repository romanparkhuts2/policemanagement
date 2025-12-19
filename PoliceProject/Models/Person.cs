using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliceProject.Models;

[Table("Person")]
public partial class Person
{
    [Key]
    public int IdPerson { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Surname { get; set; } = null!;

    [StringLength(100)]
    public string Residence { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    [StringLength(20)]
    public string Tel { get; set; } = null!;

    [StringLength(50)]
    public string DocumentType { get; set; } = null!;

    [InverseProperty("IdPersonNavigation")]
    public virtual ICollection<RecoveredItem> RecoveredItems { get; set; } = new List<RecoveredItem>();

    [InverseProperty("IdPersonNavigation")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [NotMapped]
    public string NomeCompleto
    {
        get { return $"{Name} {Surname}"; }
    }
}
