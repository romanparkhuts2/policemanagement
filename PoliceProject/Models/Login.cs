using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliceProject.Models;

[Table("Login")]
public partial class Login
{
    [Key]
    public int IdLogin { get; set; }

    [StringLength(100)]
    public string Username { get; set; } = null!;

    [StringLength(300)]
    public string Password { get; set; } = null!;

    [NotMapped]
    public bool IsPersistent { get; set; }
}
