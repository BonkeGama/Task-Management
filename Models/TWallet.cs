using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class TWallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TWallet_ID { get; set; }

        public string Tasker_ID { get; set; }
        public virtual Tasker Tasker { get; set; }
        [Display(Name = "Commission Amount")]
        public decimal TAmount { get; set; }
    }
}