using CourierKata.WebAPI.Models;
using CourierKata.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CourierKata.WebAPI.Controllers
{
    public interface IParcelController
    {
        DateTime Test();
        ShippingResponse GetShippingCost(ShippingRequest request);
    }
    
    [ApiController]
    [Route("[controller]")]
    public class ParcelController : ControllerBase, IParcelController
    {
        private readonly IShippingService _service;

        public ParcelController(IShippingService service)
        {
            _service = service;
        }
        
        [HttpGet, Route("Test")]
        public DateTime Test()
        {
            return DateTime.UtcNow;
        }

        [HttpPost, Route("GetShippingCost")]
        public ShippingResponse GetShippingCost(ShippingRequest request)
        {
            return _service.GetShippingCost(request);
        }
    }
}
