using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class Signature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Signature ID")]
        public int Signature_ID { get; set; }
        [UIHint("SignaturePad")]
        [DisplayName("      ")]
        public byte[] MySignature { get; set; }
        public int Tool_ID { get; set; }
        public virtual ToolSet ToolSet { get; set; }

    }
}