using CourierKata.WebAPI.Models;
using System.Linq;

namespace CourierKata.WebAPI.Services
{
    public interface IShippingService
    {
        ShippingResponse GetShippingCost(ShippingRequest request);
    }

    public class ShippingService : IShippingService
    {
        private readonly IParcelService _service;

        public ShippingService(IParcelService service)
        {
            _service = service;
        }
        public ShippingResponse GetShippingCost(ShippingRequest request)
        {
            var parcels = request.Parcels.Select(_service.GetParcelCost).ToArray();
            var parcelCost = parcels.Sum(x => x.TotalCost);
            var courierCost = request.CourierSpeedClassId == CourierSpeedClassEnum.Fast ? parcelCost : 0;
            var totalCost = courierCost + parcelCost;
            return new ShippingResponse
            {
                ClientId = request.ClientId,
                CourierSpeedClassId = request.CourierSpeedClassId,
                Parcels = parcels,
                ParcelCost = parcelCost,
                CourierCost = courierCost,
                TotalCost = totalCost
            };
        }
    }
}
