using System;
using CourierKata.WebAPI.Controllers;
using CourierKata.WebAPI.Models;
using CourierKata.WebAPI.Services;
using Moq;
using NUnit.Framework;

namespace CourierKata.WebAPI.UnitTests.ControllerTests
{
    [TestFixture]
    public class ParcelControllerTests
    {
        private IParcelController _controller;
        private Mock<IShippingService> _service;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IShippingService>();
            _controller = new ParcelController(_service.Object);
        }

        [Test]
        public void Test_Returns_Result()
        {
            var timeNow = DateTime.UtcNow;
            var result = _controller.Test();
            Assert.IsInstanceOf<DateTime>(result);
            var diff = new TimeSpan(timeNow.Ticks - result.Ticks).TotalSeconds;
            Assert.IsTrue(diff < 1);
        }

        [Test]
        public void GetShippingCost_Returns_Result()
        {
            _service.Setup(x => x.GetShippingCost(It.IsAny<ShippingRequest>())).Returns(new ShippingResponse());
            var result = _controller.GetShippingCost(new ShippingRequest());
            _service.Verify(x => x.GetShippingCost(It.IsAny<ShippingRequest>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ShippingResponse>(result);
        }
    }
}
