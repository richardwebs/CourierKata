using System;

namespace CourierKata.WebAPI.Models
{
    public class ShippingResponse
    {
        public Guid ClientId { get; set; }
        public OutputParcel[] Parcels { get; set; }
        public CourierSpeedClassEnum CourierSpeedClassId { get; set; }
        public int ParcelCost { get; set; }
        public int CourierCost { get; set; }
        public int TotalCost { get; set; }
    }
}
