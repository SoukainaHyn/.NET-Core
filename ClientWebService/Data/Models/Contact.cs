using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Data.Models
{
    public class Contact: IEntity<string>
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime DateCreation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Prenom { get; set; }

        [DataType(DataType.Text)]
        public string Service { get; set; }

        [DataType(DataType.Text)]
        public string Fonction { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }

        public virtual Client Client { get; set; }


    }
}
