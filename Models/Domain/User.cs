namespace Travista.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


public class User
{
    [Key]
    public int ID_Users { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public string Password { get; set; }
    public string role { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; }
}
