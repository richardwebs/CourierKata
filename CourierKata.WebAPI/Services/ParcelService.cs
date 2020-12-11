using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierKata.WebAPI.Models;

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
            return new ShippingResponse();
        }
    }
}
