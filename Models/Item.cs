﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TurboInventory.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<ItemReport> ItemReports { get; set; }
        public override string ToString()
        {
            return "Item: " + Name + " Description: " + Description + " Stock" + Stock;
        }

    }
}
