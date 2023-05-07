namespace Travista.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;

public class TravelAgency
{
    public int ID_TravelAgency { get; set; }

    [Required]
    [StringLength(30)]
    public string emri { get; set; }

    [StringLength(1000)]
    public string description { get; set; }

    [Required]
    [Range(0, 999.99)]
    public string price { get; set; }

    [StringLength(1000)]
    public string image { get; set; }

    [Required]
    public int ID_Country { get; set; }

    [Required]
    public int ID_City { get; set; }

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
}
