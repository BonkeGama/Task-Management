using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskA.Models
{
    public class DriverWallets
    {
        [Key]
        public int DWallet_ID { get; set; }
        
        public string Driver_ID { get; set; }
        public virtual Driver Driver { get; set; }
        [Display(Name ="Commission Amount")]
        public decimal DAmount { get; set; }


        public decimal? CalcCommission()
        {
            DriverWallets driverWallets = new DriverWallets();
            Order order = new Order();
            decimal amnt= order.Order_ID;
            if (amnt>5)
            {
                driverWallets.DAmount += 100;
            }
            return DAmount;
        }




    } 
    
}