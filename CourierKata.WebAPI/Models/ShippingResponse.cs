using System;

namespace CourierKata.WebAPI.Models
{
    public class ShippingResponse
    {
        public Guid ClientId { get; set; }
        public OutputParcel[] Parcels { get; set; }
        public bool IncludeSpeedyShipping { get; set; }
        public int SpeedyShippingCost { get; set; }
        public double TotalCost { get; set; }
    }
}
