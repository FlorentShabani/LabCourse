using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using MessagePack.Formatters;

namespace Travista.Models.Domain
{
    public class Lifti
    {
        [Key]
        public int ID_Lifti { get; set; }

        public string Emertimi { get; set; }

        public int ID_Ndertesa { get; set; }

        [ForeignKey("ID_Ndertesa")]
        public virtual Ndertesa FK_Ndertesa { get; set; }
    }
}
