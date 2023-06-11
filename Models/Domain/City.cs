namespace Travista.Models.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class City
{
    [Key]
    public int ID_City { get; set; }

    [Required]
    [StringLength(50)]
    public string name { get; set; }

    [Required]
    public int ID_Country { get; set; }

    [ForeignKey("ID_Country")]
    public virtual Country FK_Country { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; }

    public virtual ICollection<TravelAgency> TravelAgencies { get; set; }


}
