using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GolSkola.Models;

[Table("vijesti")]
[Index("Podnaslov", Name = "UQ__vijesti__B8B514928CD07B2B")]
public partial class Vijesti
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("naslov")]
    [StringLength(100)]
    [Unicode(false)]
    public string Naslov { get; set; } = null!;


    [Column("podnaslov")]
    [StringLength(150)]
    [Unicode(false)]
    public string Podnaslov { get; set; } = null!;

    [Column("tekst")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Tekst { get; set; }

    [Column("baner")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Baner { get; set; }

    [Column("vidljivost")]
    public int Vidljivost { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Okvir")]
    [NotMapped]
    public IFormFile? Okvir { get; set; }
}
