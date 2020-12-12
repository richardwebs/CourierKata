using CourierKata.WebAPI.Models;
using System.Linq;

namespace CourierKata.WebAPI.Services
{
    public interface IParcelService
    {
        ShippingResponse GetShippingCost(ShippingRequest request);
    }

    public class ParcelService : IParcelService
    {
        private readonly IParcelHelper _helper;

        public ParcelService(IParcelHelper helper)
        {
            _helper = helper;
        }

        public ShippingResponse GetShippingCost(ShippingRequest request)
        {
            var parcels = request.Parcels.Select(GetOutputParcel).ToArray();
            var parcelCost = parcels.Sum(x => x.Cost);
            var speedyShippingCost = request.IncludeSpeedyShipping ? parcelCost : 0;
            var totalCost = speedyShippingCost + parcelCost;
            return new ShippingResponse{ 
                ClientId = request.ClientId, 
                IncludeSpeedyShipping = request.IncludeSpeedyShipping,
                Parcels = parcels, 
                TotalCost = totalCost,
                SpeedyShippingCost = speedyShippingCost
            };
        }

        private OutputParcel GetOutputParcel(InputParcel input)
        {
            const int costPerKgOverweight = 2;
            var size = _helper.GetParcelSizeFromDimensions(input.WidthCm, input.HeightCm, input.LengthCm);
            var costFromSize = _helper.GetCostFromSize(size);
            var weightLimitKg = _helper.GetWeightLimitPerSize(size);
            var costFromWeight = _helper.GetCostFromWeight(input.WeightKg, weightLimitKg, costPerKgOverweight);
            var totalCost = costFromSize + costFromWeight;
            return new OutputParcel
            {
                ParcelId = input.ParcelId, 
                WidthCm = input.WidthCm, 
                HeightCm = input.HeightCm, 
                LengthCm = input.LengthCm,
                WeightKg = input.WeightKg,
                ParcelSizeId = size,
                Cost = totalCost
            };
        }
    }
}
