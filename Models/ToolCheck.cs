using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class ToolCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Signature ID")]
        public int ToolCheck_ID { get; set; }
        public int Tool_ID { get; set; }
        public virtual ToolCheck Check { get; set; }
        public string Tasker_ID { get; set; }
        public virtual Tasker Tasker { get; set; }
    }
}