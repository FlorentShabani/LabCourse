 namespace Travista.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TravelAgency
{
    [Key]
    public int ID_TravelAgency { get; set; }

    [Required]
    [StringLength(30)]
    public string emri { get; set; }

    [StringLength(1000)]
    public string description { get; set; }

    [Required]
    [Range(0, 999.99)]
    public string price { get; set; }

    [Required]
    public int ID_Country { get; set; }

    [Required]
    public int ID_City { get; set; }

    [ForeignKey("ID_Country")]
    public virtual Country FK_Country { get; set; }

    [ForeignKey("ID_City")]
    public virtual City FK_City { get; set; }

    [Required]
    [StringLength(100)]
    public string streetAddress { get; set; }

    [StringLength(100)]
    public string additionalAddressInfo { get; set; }

    [Required]
    [StringLength(20)]
    public string postalCode { get; set; }

    [Required]
    [StringLength(20)]
    [Phone]
    public string phoneNumber { get; set; }

    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string email { get; set; }

    public virtual ICollection<Images> Images { get; set; }
}
