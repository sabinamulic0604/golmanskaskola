using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GolSkola.Models;

[Table("golmani")]
public partial class Golmani
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("godiste")]
    [StringLength(20)]
    [Unicode(false)]
    public string Godiste { get; set; } = null!;

    [Column("klub")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Klub { get; set; }

    [Column("slika")]
    [StringLength(500)]
    [Unicode(false)]
    public string? Slika { get; set; }

    [Column("vidljivost")]
    public int Vidljivost { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }


    [Display(Name = "Profilna")]
    [NotMapped]
    public IFormFile? Profilna { get; set; }

    public static implicit operator Golmani(List<Golmani> v)
    {
        throw new NotImplementedException();
    }
}
