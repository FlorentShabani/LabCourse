using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Travista.Areas.Identity.Data;

namespace Travista.Models.Domain
{
    public class Review
    {
        [Key]
        public int ID_Reviews { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime Review_Date { get; set; }

        public int ID_Destination { get; set; }

        [ForeignKey("ID_Destination")]
        public virtual Destination FK_Destination { get; set; }

        [StringLength(450)]
        public string ID_Users { get; set; }

        [ForeignKey("ID_Users")]
        public virtual TravistaUser FK_Users { get; set; }
    }
}
