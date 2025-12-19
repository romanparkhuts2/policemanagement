using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliceProject.Models;

[Table("RecoveredItem")]
public partial class RecoveredItem
{
    [Key]
    public int IdRecoveredItem { get; set; }

    public DateOnly FoundDate { get; set; }

    public int IdPerson { get; set; }

    [StringLength(100)]
    public string Description { get; set; } = null!;

    public int IdReport { get; set; }

    [ForeignKey("IdPerson")]
    [InverseProperty("RecoveredItems")]
    public virtual Person IdPersonNavigation { get; set; } = null!;

    [ForeignKey("IdReport")]
    [InverseProperty("RecoveredItems")]
    public virtual Report IdReportNavigation { get; set; } = null!;
}
