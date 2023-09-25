using System.ComponentModel.DataAnnotations;
using Travista.Models.Domain;

namespace Travista.Models
{
    public class HomePageModel
    {
        public ContactUs? ContactUsData { get; set; }

        public List<Destination>? DestinationData { get; set; }

        public List<TravelAgency>? TravelAgency { get; set;}

        public List<Promo>? PromoData { get; set; }

        public ErrorViewModel? ErrorViewModel { get; set; }
    }
}
