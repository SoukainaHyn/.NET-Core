using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Data.Models
{
    public class Client: IEntity<string>
    {
        public Client()
        {
            Adresses = new HashSet<Adresse>();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime DateCreation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nom { get; set; }

        [Required]
        [StringLength(18,MinimumLength =14,ErrorMessage ="Siret doit avoir minimum 14 caractères !")]
        public string Siret { get; set; }
        
        public virtual Contact Contact { get; set; }

        public virtual ICollection<Adresse> Adresses { get; set; }
    }
}
