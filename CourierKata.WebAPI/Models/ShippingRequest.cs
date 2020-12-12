using System;
using System.ComponentModel.DataAnnotations;

namespace CourierKata.WebAPI.Models
{
    public class ShippingRequest
    {
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public InputParcel[] Parcels { get; set; }
    }
}
