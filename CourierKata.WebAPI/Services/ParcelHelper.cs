using CourierKata.WebAPI.Models;
using System;

namespace CourierKata.WebAPI.Services
{
    public interface IParcelHelper
    {
        ParcelSizeClassEnum GetSizeClassId(int widthCm, int heightCm, int lengthCm);
        int GetCostFromSize(ParcelSizeClassEnum sizeClassId);
        int GetCostFromWeight(int weightKg, ParcelSizeClassEnum sizeClassId);
        ParcelWeightClassEnum GetWeightClassId(int weightKg, ParcelSizeClassEnum sizeClassId);
    }

    public class ParcelHelper : IParcelHelper
    {
        public ParcelSizeClassEnum GetSizeClassId(int widthCm, int heightCm, int lengthCm)
        {
            if (widthCm <= 10 && heightCm <= 10 && lengthCm <=10) return ParcelSizeClassEnum.Small;
            if (widthCm <= 50 && heightCm <= 50 && lengthCm <= 50) return ParcelSizeClassEnum.Medium;
            if (widthCm <= 100 && heightCm <= 100 && lengthCm <= 100) return ParcelSizeClassEnum.Large;
            return ParcelSizeClassEnum.ExtraLarge;
        }

        public int GetCostFromSize(ParcelSizeClassEnum size)
        {
            return size switch
            {
                ParcelSizeClassEnum.Small => 3,
                ParcelSizeClassEnum.Medium => 8,
                ParcelSizeClassEnum.Large => 15,
                ParcelSizeClassEnum.ExtraLarge => 25,
                _ => throw new Exception($"Unhandled size: {size}"),
            };
        }

        public int GetCostFromWeight(int weightKg, ParcelSizeClassEnum sizeClassId)
        {
            if (weightKg >= 50) return GetCostFromWeight(weightKg, 50, 1, 50);
            return sizeClassId switch
                {
                ParcelSizeClassEnum.Small => weightKg <= 1 ? 0 : GetCostFromWeight(weightKg, 1, 2, 0),
                ParcelSizeClassEnum.Medium => weightKg <= 3 ? 0 : GetCostFromWeight(weightKg, 3, 2, 0),
                ParcelSizeClassEnum.Large => weightKg <= 6 ? 0 : GetCostFromWeight(weightKg, 6, 2, 0),
                ParcelSizeClassEnum.ExtraLarge => weightKg <= 10 ? 0 : GetCostFromWeight(weightKg, 10, 2, 0),
                _ => throw new Exception($"Unhandled size: {sizeClassId}"),
                };
        }

        public ParcelWeightClassEnum GetWeightClassId(int weightKg, ParcelSizeClassEnum sizeClassId)
        {
            if (weightKg >= 50) return ParcelWeightClassEnum.ExtraHeavyCharge;
            return sizeClassId switch
            {
                ParcelSizeClassEnum.Small => weightKg <= 1 ? ParcelWeightClassEnum.NoCharge : ParcelWeightClassEnum.StandardCharge,
                ParcelSizeClassEnum.Medium => weightKg <= 3 ? ParcelWeightClassEnum.NoCharge : ParcelWeightClassEnum.StandardCharge,
                ParcelSizeClassEnum.Large => weightKg <= 6 ? ParcelWeightClassEnum.NoCharge : ParcelWeightClassEnum.StandardCharge,
                ParcelSizeClassEnum.ExtraLarge => weightKg <= 10 ? ParcelWeightClassEnum.NoCharge : ParcelWeightClassEnum.StandardCharge,
                _ => throw new Exception($"Unhandled size: {sizeClassId}"),
            };
        }

        private static int GetCostFromWeight(int actualWeightKg, int weightLimitKg, int costPerKgOverweight, int minCost)
        {
            var excessWeight = actualWeightKg - weightLimitKg;
            if (excessWeight < 0) return 0;
            return (excessWeight * costPerKgOverweight) + minCost;
        }
    }
}
