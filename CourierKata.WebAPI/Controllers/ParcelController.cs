using CourierKata.WebAPI.Models;
using CourierKata.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CourierKata.WebAPI.Controllers
{
    public interface IParcelController
    {
        DateTime Test();
        ShippingResponse CalculateShippingCost(ShippingRequest request);
    }
    
    [ApiController]
    [Route("[controller]")]
    public class ParcelController : ControllerBase, IParcelController
    {
        private readonly IParcelService _service;

        public ParcelController(IParcelService service)
        {
            _service = service;
        }
        
        [HttpGet, Route("Test")]
        public DateTime Test()
        {
            return DateTime.UtcNow;
        }

        [HttpPost, Route("GetShippingCost")]
        public ShippingResponse CalculateShippingCost(ShippingRequest request)
        {
            return _service.CalculateShippingCost(request);
        }
    }
}
