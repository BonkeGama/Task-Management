﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class Spins
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Spins ID")]
        public int Spins_ID { get; set; }

        public int No_Spins { get; set; }

        public string Profile_ID { get; set; }
        public virtual Client Client { get; set; }
  

    }
}