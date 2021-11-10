using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskA.Models
{
    public class Tasker
    {
        [Key]
        [DisplayName("Client ID")]
        public string Tasker_ID { get; set; }

        //[Required(ErrorMessage = "First name is required")]
        [DisplayName("First Name")]
        public string Tasker_Name { get; set; }

        //[Required(ErrorMessage = "ID number is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "This field must be 13 characters")]
        [DisplayName("ID number")]
        public string Tasker_IDNo { get; set; }


        //[Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last Name")]
        public string Tasker_Surname { get; set; }

     
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        //[Required(ErrorMessage = "Contact Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
          ErrorMessage = "Entered Contact number format is not valid.")]
        public string Tasker_Cellnumber { get; set; }

        //[Required(ErrorMessage = "Residence address is required")]
        [DisplayName("Residence address")]
        public string Tasker_Address { get; set; }

        //[Required(ErrorMessage = "Email address is required")]
        [DisplayName("Email address")]
        [EmailAddress]
        public string Tasker_Email { get; set; }
  

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telephone")]
       // [Required(ErrorMessage = "Telephone number is Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
          ErrorMessage = "Entered tellephone number format is not valid.")]
        public string Tasker_Tellnum { get; set; }

        [DisplayName("Service Name")]
        public int Service_ID { get; set; }
        public virtual Service Service { get; set; }
        public virtual List<Document> Documents{ get; set; }
        public virtual List<TWallet> TWallets { get; set; }



    }
}