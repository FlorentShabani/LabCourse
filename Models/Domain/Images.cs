namespace Travista.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;
    using Travista.Areas.Identity.Data;

    public class Images
    {
        [Key]
        public int ID_Image { get; set; }

        [StringLength(1000)]
        public string ImagePath { get; set; }


        public int? ID_Destination { get; set; }

        public int? ID_TravelAgency { get; set; }

        [ForeignKey("ID_Destination")]
        public virtual Destination? Destinations { get; set; }

        [ForeignKey("ID_TravelAgency")]
        public virtual TravelAgency? TravelAgencys { get; set;}

    }
}
