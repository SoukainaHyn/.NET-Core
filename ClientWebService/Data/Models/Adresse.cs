using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Data.Models
{
    public class Adresse: IEntity<string>
    {
        [Key]
        public string Id{ get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime DateCreation { get; set; }

        [DataType(DataType.Text)]
        public string Rue { get; set; }

        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; }

        [DataType(DataType.Text)]
        public string Ville { get; set; }

        [DataType(DataType.Text)]
        public string Pays { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Portable { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
