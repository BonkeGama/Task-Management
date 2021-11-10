using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class ToolSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Tool ID")]
        public int Tool_ID { get; set; }

        [DisplayName("Tool Set Name")]
        public string Tool_SetName { get; set; }

        [Display(Name = "Tool Image")]
        public byte[] ToolImage { get; set; }
        public string QRCodeText { get; set; }
        [Display(Name = "QRCode Image")]
        public string QRCodeImagePath { get; set; }

        [Display(Name = "Availability")]
        public bool availability { get; set; }

        [Display(Name = "Tool Category")]
        public int ServiceCat_ID { get; set; }
        public virtual ServiceCat ServiceCat { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();
        public void updateStatus(int id)
        {
            ToolSet toolSet = (from i in db.ToolSets
                               where i.Tool_ID == id
                               select i).FirstOrDefault();
            toolSet.availability = false;
            db.SaveChanges();
        }

    }
}