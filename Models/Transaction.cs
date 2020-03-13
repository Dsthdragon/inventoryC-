using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TurboInventory.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Issuer")]
        [Required]
        public int IssuerId { get; set; }
        public virtual Contact Issuer { get; set; }

        [ForeignKey("Receiver")]
        [Required]
        public int ReceiverId { get; set; }
        public virtual Contact Receiver { get; set; }

        [ForeignKey("Item")]
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public bool Credit { get; set; }

        public int Amount { get; set; }


        public override string ToString()
        {
            return "Transaction: ID - " + Id + " IssuerId - " + IssuerId + " " + Issuer.ToString() + " ReceiverId - " + ReceiverId + " " + Receiver.ToString() + " Credit - " + Credit + " Amount - " + Amount;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }


    }
}
