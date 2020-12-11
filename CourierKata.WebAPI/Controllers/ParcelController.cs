using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CourierKata.WebAPI.Controllers
{
    public interface IParcelController
    {
        Task<DateTime> Test();
    }
    
    [ApiController]
    [Route("[controller]")]
    public class ParcelController : ControllerBase, IParcelController
    {
        [HttpGet, Route("Test")]
        public async Task<DateTime> Test()
        {
            return await Task.Run(() => DateTime.UtcNow);
        }
    }
}
