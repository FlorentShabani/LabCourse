using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Travista.Models.Domain;

namespace Travista.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TravistaUser class
public class TravistaUser : IdentityUser
{
    public string Fullname { get; set; }

    public virtual ICollection<Destination> Destinations { get; set; }

    public virtual ICollection<TravelTips> TravelTips { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }

    public virtual ICollection<Promo> Promos { get; set; }

    public virtual ICollection<ContactUs> ContactUss { get; set; }
}

