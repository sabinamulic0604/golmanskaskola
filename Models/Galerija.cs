using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GolSkola.Models;

[Table("galerija")]
public partial class Galerija
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("ime")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Ime { get; set; }

    [Column("fajl")]
    [StringLength(500)]
    [Unicode(false)]
    public string Fajl { get; set; } = null!;

    [Column("vidljivost")]
    public int Vidljivost { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Pomocna")]
    [NotMapped]
    public IFormFile? Pomocna { get; set; }
}
