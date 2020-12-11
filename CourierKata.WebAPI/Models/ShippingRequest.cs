using System;

namespace CourierKata.WebAPI.Models
{
    public class ShippingRequest
    {
        public Guid ClientId { get; set; }
        public InputParcel[] Parcels { get; set; }
    }
}
