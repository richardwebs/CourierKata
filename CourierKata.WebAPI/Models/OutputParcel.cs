
namespace CourierKata.WebAPI.Models
{
    public class OutputParcel : InputParcel
    {
        public ParcelSizeClassEnum ParcelSizeClassId { get; set; }
        public ParcelWeightClassEnum ParcelWeightClassId { get; set; }
        public int SizeCost { get; set; }
        public int WeightCost { get; set; }
        public int TotalCost { get; set; }
    }
}
