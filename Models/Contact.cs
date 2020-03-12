using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TurboInventory.Models
{
    public class Contact
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }


        public override string ToString()
        {
            return "Contact: " + Name + " " + Phone + " " + Id;
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [InverseProperty("Issuer")]
        public ICollection<Transaction> Issued { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<Transaction> Received{ get; set; }
    }
}
