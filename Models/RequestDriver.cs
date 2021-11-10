using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class RequestDriver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RDriver_ID { get; set; }

        public string Driver_ID { get; set; }
        public virtual Driver Driver { get; set; }
        [Display(Name = "Tasker Fullname")]
        public string TaskerFullName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        //[Required(ErrorMessage = "Contact number is required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                 ErrorMessage = "Entered Contact number format is not valid.")]
        public string TaskerCellNo { get; set; }
        [Display(Name = "Depature Location")]
        public string FromLocation { get; set; }
        [Display(Name = "Destination Location")]
        public string ToLocation { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
    }
}