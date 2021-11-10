using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
	public class ServiceCat
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Category ID")]
		public int ServiceCat_ID { get; set; }

		[Required(ErrorMessage = "Category is  required")]
		[DisplayName("Category")]
		public string Category { get; set; }

		[Required(ErrorMessage = "Service cost is  required")]
		[DataType(DataType.Currency)]
		[DisplayName("Service cost")]
		public decimal Cost_Per_Hour { get; set; }
		public virtual List<Service> Services { get; set; }
	}
}