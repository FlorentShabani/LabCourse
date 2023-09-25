namespace Travista.Models.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Travista.Areas.Identity.Data;

public class Promo
{
    [Key]
    public int ID_Promo { get; set; }

    [StringLength(20)]
    public string Title { get; set; }

    [StringLength(70)]
    public string Subtitle { get; set; }

    [StringLength(400)]
    public string Description { get; set; }

    [StringLength(400)]
    public string Picture { get; set; }

    [StringLength(400)]
    public string DestLink { get; set; }

    public string ID_Users { get; set; }

    [ForeignKey("ID_Users")]
    public virtual TravistaUser FK_Users { get; set; }
}
