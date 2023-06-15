using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Travista.Areas.Identity.Data;

namespace Travista.Models.Domain;

    public class TravelTips
    {
        [Key]
        public int ID_TravelTips { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(8000)]
        public string Description { get; set; }

        [Required]
        public DateTime TravelTips_Date { get; set; }

        [StringLength(450)]
        public string ID_Users { get; set; }

        [ForeignKey("ID_Users")]
        public virtual TravistaUser FK_Users { get; set; }

        public int ID_Country { get; set; }

        [ForeignKey("ID_Country")]
        public virtual Country FK_Country { get; set; }
    }
