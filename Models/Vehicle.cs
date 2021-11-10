using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
	public class Vehicle
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Vehicle ID")]
		public int Vehicle_ID { get; set; }

		//[Required(ErrorMessage = "Vehicle year is required")]
		//[DisplayName("Year")]
		//public string Vehicle_year { get; set; }

		[Required(ErrorMessage = "Vehicle make is required")]
		[DisplayName("Make")]
		public string Vehicle_make { get; set; }

		[Required(ErrorMessage = "colour of vehicle is required")]
		[DisplayName("Colour")]
		public string Vehicle_color { get; set; }

		[Required(ErrorMessage = "VIN number is required")]
		[DisplayName("VIN number")]
		public string VinNumber { get; set; }

		[Required(ErrorMessage = " Fuel Consumption liter/km is required")]
		[DisplayName("Consumption in liter/km")]
		public int liter { get; set; }
		[DisplayName("Model")]
		public int Model_ID { get; set; }
		public virtual VehicleModel VehicleModel { get; set; }

		public virtual List<Driver> Drivers { get; set; }


	}
}