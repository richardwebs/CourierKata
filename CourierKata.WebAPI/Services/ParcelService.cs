using CourierKata.WebAPI.Models;
using System.Linq;

namespace CourierKata.WebAPI.Services
{
    public interface IParcelService
    {
        ShippingResponse CalculateShippingCost(ShippingRequest request);
    }

    public class ParcelService : IParcelService
    {
        private readonly IParcelHelper _helper;

        public ParcelService(IParcelHelper helper)
        {
            _helper = helper;
        }

        public ShippingResponse CalculateShippingCost(ShippingRequest request)
        {
            var parcels = request.Parcels.Select(GetOutputParcel).ToArray();
            var totalCost = parcels.Sum(x => x.Cost);
            return new ShippingResponse{ 
                ClientId = request.ClientId, 
                Parcels = parcels, 
                TotalCost = totalCost
            };
        }

        private OutputParcel GetOutputParcel(InputParcel input)
        {
            var size = _helper.GetParcelSizeFromDimensions(input.WidthCm, input.HeightCm, input.LengthCm);
            var cost = _helper.CalculateCostFromSize(size);
            return new OutputParcel
            {
                ParcelId = input.ParcelId, 
                WidthCm = input.WidthCm, 
                HeightCm = input.HeightCm, 
                LengthCm = input.LengthCm, 
                ParcelSizeId = size,
                Cost = cost
            };
        }
    }
}
