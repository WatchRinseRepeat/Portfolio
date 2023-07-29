using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreCMS3.Areas.Rent.Models;

namespace TheatreCMS3.Areas.Rent.ViewModels
{
    public class RentalVM
    {
        public IEnumerable<Rental> Rental { get; set; }
        public IEnumerable<RentalEquipment> Equipment { get; set; }
        public IEnumerable<RentalRoom> Room { get; set; }
    }
}