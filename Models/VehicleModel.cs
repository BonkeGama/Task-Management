using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskA.Models
{
	public class VehicleModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Model ID")]
		public int Model_ID { get; set; }

		[Required(ErrorMessage = "Vehicle model is required")]
		[DisplayName("Vehilce Model")]
		public string Vehicle_model { get; set; }

		public virtual List<Vehicle> Vehicles{ get; set; }
	}
}