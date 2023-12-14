using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Travista.Models.Domain
{
    public class Ndertesa
    {
        [Key]
        public int ID_Ndertesa { get; set; }

        public string Emertimi { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataPT { get; set; }

        public virtual ICollection<Lifti> Lifti { get; set; }

    }
}
