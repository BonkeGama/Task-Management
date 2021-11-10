using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskA.Models
{
	public class Service
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Service ID")]
		public int Service_ID { get; set; }

		[DisplayName("Service Name")]
		public string Service_Name { get; set; }

		[StringLength(100000, MinimumLength = 20, ErrorMessage = "The service description should be between 2 - 250 Characters")]
		[DisplayName("Service description")]
		[DataType(DataType.MultilineText)]
		public string Service_Description { get; set; }

		public int ServiceCat_ID { get; set; }
		public virtual ServiceCat ServiceCat { get; set; }
		public virtual List<Tasker> Taskers{ get; set; }


	}
}