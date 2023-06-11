namespace Travista.Models.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Country
{
    [Key]
    public int ID_Country { get; set; }

    [Required]
    [StringLength(50)]
    public string name { get; set; }

    [Required]
    [StringLength(50)]
    public string language { get; set; }

    public virtual ICollection<TravelAgency> TravelAgencies { get; set; }

}
