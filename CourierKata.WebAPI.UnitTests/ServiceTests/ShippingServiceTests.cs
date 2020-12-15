using System;
using CourierKata.WebAPI.Models;
using CourierKata.WebAPI.Services;
using Moq;
using NUnit.Framework;

namespace CourierKata.WebAPI.UnitTests.ServiceTests
{
    [TestFixture]
    public class ShippingServiceTests
    {
        private IShippingService _shippingService;
        private Mock<IParcelService> _parcelService;

        [SetUp]
        public void Setup()
        {
            _parcelService = new Mock<IParcelService>();
            _shippingService = new ShippingService(_parcelService.Object);
        }

        [TestCase(CourierSpeedClassEnum.Fast)]
        [TestCase(CourierSpeedClassEnum.Standard)]
        public void GetShippingCost_Returns_Result(CourierSpeedClassEnum speedClassId)
        {
            var inputParcels = new[]
            {
                new InputParcel{ HeightCm = 1, LengthCm = 2, WidthCm = 3, WeightKg = 2, ParcelId = Guid.NewGuid() },
                new InputParcel{ HeightCm = 4, LengthCm = 5, WidthCm = 6, WeightKg = 3, ParcelId = Guid.NewGuid() }
            };
            var request = new ShippingRequest
            {
                ClientId = Guid.NewGuid(),
                Parcels = inputParcels, CourierSpeedClassId = speedClassId

            };
            const int totalParcelCost = 10;
            _parcelService.Setup(x => x.GetParcelCost(It.IsAny<InputParcel>())).Returns(new OutputParcel{ TotalCost = totalParcelCost });
            var result = _shippingService.GetShippingCost(request);
            _parcelService.Verify(x => x.GetParcelCost(It.IsAny<InputParcel>()), Times.Exactly(inputParcels.Length));
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ShippingResponse>(result);
            Assert.AreEqual(inputParcels.Length, result.Parcels.Length);
            Assert.AreEqual(request.ClientId, result.ClientId);
            Assert.AreEqual(request.CourierSpeedClassId, result.CourierSpeedClassId);
            var parcelCost = (totalParcelCost * inputParcels.Length);
            Assert.AreEqual(parcelCost, result.ParcelCost);
            var courierCost = request.CourierSpeedClassId == CourierSpeedClassEnum.Fast ? parcelCost : 0;
            Assert.AreEqual(courierCost, result.CourierCost);
            var totalCost = parcelCost + courierCost;
            Assert.AreEqual(totalCost, result.TotalCost);
        }
    }
}
