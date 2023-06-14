using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Travista.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TravistaUser class
public class TravistaUser : IdentityUser
{
    public string Fullname { get; set; }
}

