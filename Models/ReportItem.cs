using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TurboInventory.Models
{
    public class ReportItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [ForeignKey("Item")]
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey("Report")]
        [Required]
        public int ReportId { get; set; }
        public Report Report { get; set; }

        public int Left { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
    }
}
