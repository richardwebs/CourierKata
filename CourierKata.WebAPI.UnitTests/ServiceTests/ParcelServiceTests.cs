using CourierKata.WebAPI.Models;
using CourierKata.WebAPI.Services;
using Moq;
using NUnit.Framework;
using System;

namespace CourierKata.WebAPI.UnitTests.ServiceTests
{
    [TestFixture]
    public class ParcelServiceTests
    {
        private IParcelService _service;
        private Mock<IParcelHelper> _helper;

        [SetUp]
        public void Setup()
        {
            _helper = new Mock<IParcelHelper>();
            _service = new ParcelService(_helper.Object);
        }

        [TestCase(ParcelSizeClassEnum.Small, ParcelWeightClassEnum.NoCharge, 10, 0)]
        [TestCase(ParcelSizeClassEnum.Small, ParcelWeightClassEnum.StandardCharge, 10, 10)]
        [TestCase(ParcelSizeClassEnum.Small, ParcelWeightClassEnum.ExtraHeavyCharge, 10, 10)]
        public void GetParcelCost_Returns_Result(ParcelSizeClassEnum sizeClassId, ParcelWeightClassEnum weightClassId, int sizeCost, int weightCost)
        {
            var input = new InputParcel
            {
                HeightCm = 1, 
                LengthCm = 2, 
                WidthCm = 3, 
                WeightKg = 2, 
                ParcelId = Guid.NewGuid()
            };

            _helper.Setup(x => x.GetSizeClassId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(sizeClassId);
            _helper.Setup(x => x.GetCostFromSize(It.IsAny<ParcelSizeClassEnum>())).Returns(sizeCost);
            _helper.Setup(x => x.GetWeightClassId(It.IsAny<int>(), It.IsAny<ParcelSizeClassEnum>())).Returns(weightClassId);
            _helper.Setup(x => x.GetCostFromWeight(It.IsAny<int>(), It.IsAny<ParcelSizeClassEnum>())).Returns(weightCost);
            var result = _service.GetParcelCost(input);
            _helper.Verify(x => x.GetSizeClassId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
            _helper.Verify(x => x.GetCostFromSize(It.IsAny<ParcelSizeClassEnum>()), Times.Exactly(1));
            _helper.Verify(x => x.GetWeightClassId(It.IsAny<int>(), It.IsAny<ParcelSizeClassEnum>()), Times.Exactly(1));
            _helper.Verify(x => x.GetCostFromWeight(It.IsAny<int>(), It.IsAny<ParcelSizeClassEnum>()), Times.Exactly(1));
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OutputParcel>(result);
            CompareResults(input, result);
            Assert.AreEqual(sizeCost + weightCost, result.TotalCost);
            Assert.AreEqual(sizeCost, result.SizeCost);
            Assert.AreEqual(weightCost, result.WeightCost);
            Assert.AreEqual(sizeClassId, result.ParcelSizeClassId);
            Assert.AreEqual(weightClassId, result.ParcelWeightClassId);
        }

        private static void CompareResults(InputParcel input, InputParcel output)
        {
            Assert.AreEqual(input.WidthCm, output.WidthCm);
            Assert.AreEqual(input.HeightCm, output.HeightCm);
            Assert.AreEqual(input.LengthCm, output.LengthCm);
            Assert.AreEqual(input.ParcelId, output.ParcelId);
            Assert.AreEqual(input.WeightKg, output.WeightKg);
        }
    }
}
