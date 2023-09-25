namespace Travista.Models.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Travista.Areas.Identity.Data;

public class ContactUs
{
    [Key]
    public int ID_ContactUs { get; set; }

    [StringLength(40)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Subject { get; set; }

    [StringLength(8000)]
    public string Message { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    public string ID_Users { get; set; }

    [ForeignKey("ID_Users")]
    public virtual TravistaUser FK_Users { get; set; }
}
