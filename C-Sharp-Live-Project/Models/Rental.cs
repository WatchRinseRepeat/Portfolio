using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Rent.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public string RentalName { get; set; }
        public int RentalCost { get; set; }
        public string FlawsAndDamages { get; set; }
        public virtual List<RentalHistory> RentalHistory { get; set; }

    }

    public class RentalEquipment : Rental
    {
        [Required(ErrorMessage = "You must indicate if Choking Hazard.")]
        public bool ChockingHazard { get; set; }
        public bool SuffocationHazard { get; set; }
        public int PurchasePrice { get; set; }
    }

    public class RentalRoom : Rental
    {
        [Required(ErrorMessage ="You must indicate the Room Number.")]
        public int RoomNumber { get; set; }
        public int SquareFootage { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
