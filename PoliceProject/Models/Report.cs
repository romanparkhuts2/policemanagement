using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliceProject.Models;

[Table("Report")]
public partial class Report
{
    [Key]
    public int IdReport { get; set; }

    [StringLength(50)]
    public string Type { get; set; } = null!;

    public DateOnly ReportDate { get; set; }

    public int IdObjectType { get; set; }

    [StringLength(300)]
    public string Description { get; set; } = null!;

    public DateOnly IncidentDate { get; set; }

    public int IdPerson { get; set; }

    [StringLength(200)]
    public string ImageFileUrl { get; set; } = null!;

    [StringLength(50)]
    public string AgentName { get; set; } = null!;

    public bool Solved { get; set; }

    [ForeignKey("IdObjectType")]
    [InverseProperty("Reports")]
    public virtual ObjectType IdObjectTypeNavigation { get; set; } = null!;

    [ForeignKey("IdPerson")]
    [InverseProperty("Reports")]
    public virtual Person IdPersonNavigation { get; set; } = null!;

    [InverseProperty("IdReportNavigation")]
    public virtual ICollection<RecoveredItem> RecoveredItems { get; set; } = new List<RecoveredItem>();

    [NotMapped]
    public IFormFile? FileUpload { get; set; }

    [NotMapped]
    public string ReportCompleto
    {
        get { return $"{AgentName} {ReportDate} {IdReport}"; }
    }
}
