using System;
using System.ComponentModel.DataAnnotations;

namespace CourierKata.WebAPI.Models
{
    public class InputParcel
    {
        [Required]
        public Guid ParcelId { get; set; }
        [Required]
        public int WidthCm { get; set; }
        [Required]
        public int HeightCm { get; set; }
        [Required]
        public int LengthCm { get; set; }
    }
}
