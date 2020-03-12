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
        [Required]
        public int IssuerId { get; set; }
        public Contact Issuer { get; set; }

        [Required]
        public int ReceiverId { get; set; }
        public Contact Receiver { get; set; }

        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public bool Credit { get; set; }

        public int Amount { get; set; }


        public override string ToString()
        {
            return "Transaction: ID - " + Id + " IssuerId - " + IssuerId + " " + IssuerId.ToString() + " ReceiverId - " + ReceiverId + " " + IssuerId.ToString() + " Credit - " + Credit + " Amount - " + Amount;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }


    }
}
