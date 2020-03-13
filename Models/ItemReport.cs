using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TurboInventory.Models
{
    public class ItemReport
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        public int Transactions { get; set; }

        public int Remaining { get; set; }

        public int Taken { get; set; }

        public int Brought { get; set; }

        [ForeignKey("Item")]
        [Required]
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
    }
}
