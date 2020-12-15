using CourierKata.WebAPI.Models;
using CourierKata.WebAPI.Services;
using NUnit.Framework;

namespace CourierKata.WebAPI.UnitTests.ServiceTests
{
    [TestFixture]
    public class ParcelHelperTests
    {
        private readonly IParcelHelper _helper;

        public ParcelHelperTests()
        {
            _helper = new ParcelHelper();
        }

        [TestCase(1, 1, 1)]
        [TestCase(10, 10, 10)]
        public void GetSizeClassId_Returns_Small(int widthCm, int heightCm, int lengthCm)
        {
            var result = _helper.GetSizeClassId(widthCm, heightCm, lengthCm);
            Assert.AreEqual(ParcelSizeClassEnum.Small, result);
        }

        [TestCase(1, 11, 50)]
        [TestCase(11, 1, 50)]
        [TestCase(50, 1, 11)]
        [TestCase(25, 25, 50)]
        [TestCase(25, 50, 50)]
        public void GetSizeClassId_Returns_Medium(int widthCm, int heightCm, int lengthCm)
        {
            var result = _helper.GetSizeClassId(widthCm, heightCm, lengthCm);
            Assert.AreEqual(ParcelSizeClassEnum.Medium, result);
        }

        [TestCase(1, 50, 100)]
        [TestCase(100, 50, 1)]
        [TestCase(50, 1, 100)]
        [TestCase(25, 75, 75)]
        [TestCase(75, 50, 50)]
        [TestCase(80, 90, 60)]
        public void GetSizeClassId_Returns_Large(int widthCm, int heightCm, int lengthCm)
        {
            var result = _helper.GetSizeClassId(widthCm, heightCm, lengthCm);
            Assert.AreEqual(ParcelSizeClassEnum.Large, result);
        }

        [TestCase(1, 50, 101)]
        [TestCase(101, 50, 1)]
        [TestCase(50, 1, 101)]
        [TestCase(25, 75, 200)]
        [TestCase(770, 50, 50)]
        [TestCase(110, 120, 130)]
        public void GetSizeClassId_Returns_ExtraLarge(int widthCm, int heightCm, int lengthCm)
        {
            var result = _helper.GetSizeClassId(widthCm, heightCm, lengthCm);
            Assert.AreEqual(ParcelSizeClassEnum.ExtraLarge, result);
        }

        [TestCase(ParcelSizeClassEnum.Small, 3)]
        [TestCase(ParcelSizeClassEnum.Medium, 8)]
        [TestCase(ParcelSizeClassEnum.Large, 15)]
        [TestCase(ParcelSizeClassEnum.ExtraLarge, 25)]
        public void GetCostFromSize_Returns_Result(ParcelSizeClassEnum size, int expectedResult)
        {
            var result = _helper.GetCostFromSize(size);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, ParcelSizeClassEnum.Small)]
        [TestCase(1, ParcelSizeClassEnum.Medium)]
        [TestCase(3, ParcelSizeClassEnum.Medium)]
        [TestCase(1, ParcelSizeClassEnum.Large)]
        [TestCase(3, ParcelSizeClassEnum.Large)]
        [TestCase(6, ParcelSizeClassEnum.Large)]
        [TestCase(1, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(3, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(6, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(10, ParcelSizeClassEnum.ExtraLarge)]
        public void GetWeightClassId_Returns_NoCharge(int weightKg, ParcelSizeClassEnum sizeClassId)
        {
            var result = _helper.GetWeightClassId(weightKg, sizeClassId);
            Assert.AreEqual(ParcelWeightClassEnum.NoCharge, result);
        }

        [TestCase(2, ParcelSizeClassEnum.Small)]
        [TestCase(49, ParcelSizeClassEnum.Small)]
        [TestCase(4, ParcelSizeClassEnum.Medium)]
        [TestCase(49, ParcelSizeClassEnum.Medium)]
        [TestCase(7, ParcelSizeClassEnum.Large)]
        [TestCase(49, ParcelSizeClassEnum.Large)]
        [TestCase(11, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(49, ParcelSizeClassEnum.ExtraLarge)]
        public void GetWeightClassId_Returns_StandardCharge(int weightKg, ParcelSizeClassEnum sizeClassId)
        {
            var result = _helper.GetWeightClassId(weightKg, sizeClassId);
            Assert.AreEqual(ParcelWeightClassEnum.StandardCharge, result);
        }
        
        [TestCase(50, ParcelSizeClassEnum.Small)]
        [TestCase(50, ParcelSizeClassEnum.Medium)]
        [TestCase(50, ParcelSizeClassEnum.Large)]
        [TestCase(50, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(60, ParcelSizeClassEnum.Small)]
        [TestCase(60, ParcelSizeClassEnum.Medium)]
        [TestCase(60, ParcelSizeClassEnum.Large)]
        [TestCase(60, ParcelSizeClassEnum.ExtraLarge)]
        public void GetWeightClassId_Returns_ExtraHeavyCharge(int weightKg, ParcelSizeClassEnum sizeClassId)
        {
            var result = _helper.GetWeightClassId(weightKg, sizeClassId);
            Assert.AreEqual(ParcelWeightClassEnum.ExtraHeavyCharge, result);
        }

        [TestCase(1, ParcelSizeClassEnum.Small)]
        [TestCase(1, ParcelSizeClassEnum.Medium)]
        [TestCase(3, ParcelSizeClassEnum.Medium)]
        [TestCase(1, ParcelSizeClassEnum.Large)]
        [TestCase(3, ParcelSizeClassEnum.Large)]
        [TestCase(6, ParcelSizeClassEnum.Large)]
        [TestCase(1, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(3, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(6, ParcelSizeClassEnum.ExtraLarge)]
        [TestCase(10, ParcelSizeClassEnum.ExtraLarge)]
        public void GetCostFromWeight_Returns_Zero(int weightKg, ParcelSizeClassEnum sizeClassId)
        {
            var result = _helper.GetCostFromWeight(weightKg, sizeClassId);
            Assert.AreEqual(0, result);
        }

        [TestCase(2, ParcelSizeClassEnum.Small, 2)]
        [TestCase(49, ParcelSizeClassEnum.Small, 96)]
        [TestCase(4, ParcelSizeClassEnum.Medium, 2)]
        [TestCase(49, ParcelSizeClassEnum.Medium, 92)]
        [TestCase(7, ParcelSizeClassEnum.Large, 2)]
        [TestCase(49, ParcelSizeClassEnum.Large, 86)]
        [TestCase(11, ParcelSizeClassEnum.ExtraLarge, 2)]
        [TestCase(49, ParcelSizeClassEnum.ExtraLarge, 78)]
        [TestCase(50, ParcelSizeClassEnum.ExtraLarge, 50)]
        [TestCase(60, ParcelSizeClassEnum.ExtraLarge, 60)]
        public void GetCostFromWeight_Returns_Result(int weightKg, ParcelSizeClassEnum sizeClassId, int expectedResult)
        {
            var result = _helper.GetCostFromWeight(weightKg, sizeClassId);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
