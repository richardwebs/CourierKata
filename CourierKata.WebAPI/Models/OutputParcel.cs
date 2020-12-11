
namespace CourierKata.WebAPI.Models
{
    public class OutputParcel : InputParcel
    {
        public ParcelSizeEnum ParcelSizeId { get; set; }
        public int Cost { get; set; }
    }
}
