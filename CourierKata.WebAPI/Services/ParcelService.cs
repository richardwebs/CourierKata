using CourierKata.WebAPI.Models;

namespace CourierKata.WebAPI.Services
{
    public interface IParcelService
    {
        OutputParcel GetParcelCost(InputParcel input);
    }

    public class ParcelService : IParcelService
    {
        private readonly IParcelHelper _helper;

        public ParcelService(IParcelHelper helper)
        {
            _helper = helper;
        }

        public OutputParcel GetParcelCost(InputParcel input)
        {
            var sizeClassId = _helper.GetSizeClassId(input.WidthCm, input.HeightCm, input.LengthCm);
            var weightClassId = _helper.GetWeightClassId(input.WeightKg, sizeClassId);
            var sizeCost = _helper.GetCostFromSize(sizeClassId);
            var weightCost = _helper.GetCostFromWeight(input.WeightKg, sizeClassId);
            var totalCost = sizeCost + weightCost;
            return new OutputParcel
            {
                ParcelId = input.ParcelId,
                WidthCm = input.WidthCm,
                HeightCm = input.HeightCm,
                LengthCm = input.LengthCm,
                WeightKg = input.WeightKg,
                ParcelSizeClassId = sizeClassId,
                ParcelWeightClassId = weightClassId, 
                SizeCost = sizeCost,
                WeightCost = weightCost,
                TotalCost = totalCost
            };
        }
    }
}
