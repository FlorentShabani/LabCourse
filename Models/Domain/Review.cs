using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Travista.Models.Domain
{
    public class Review
    {
        [Key]
        public int ID_Reviews { get; set; }

        public string Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime Review_Date { get; set; }

        public int ID_Destination { get; set; }

        [ForeignKey("ID_Destination")]
        public virtual Destination FK_Destination { get; set; }

        public int ID_Users { get; set; }

        [ForeignKey("ID_Users")]
        public virtual User FK_Users { get; set; }
    }
}
