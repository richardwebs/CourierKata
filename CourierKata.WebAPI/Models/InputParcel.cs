using System;

namespace CourierKata.WebAPI.Models
{
    public class InputParcel
    {
        public Guid ParcelId { get; set; }
        public int WidthCm { get; set; }
        public int HeightCm { get; set; }
        public int LengthCm { get; set; }
    }
}
