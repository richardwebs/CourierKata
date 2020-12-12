using CourierKata.WebAPI.Models;
using System;

namespace CourierKata.WebAPI.Services
{
    public interface IParcelHelper
    {
        ParcelSizeEnum GetParcelSizeFromDimensions(int widthCm, int heightCm, int lengthCm);
        int GetCostFromSize(ParcelSizeEnum size);
        int GetWeightLimitPerSize(ParcelSizeEnum size);
        int GetCostFromWeight(int actualWeightKg, int weightLimitKg, int costPerKgOverweight);
    }

    public class ParcelHelper : IParcelHelper
    {
        public ParcelSizeEnum GetParcelSizeFromDimensions(int widthCm, int heightCm, int lengthCm)
        {
            if (widthCm <= 10 && heightCm <= 10 && lengthCm <=10) return ParcelSizeEnum.Small;
            if (widthCm <= 50 && heightCm <= 50 && lengthCm <= 50) return ParcelSizeEnum.Medium;
            if (widthCm <= 100 && heightCm <= 100 && lengthCm <= 100) return ParcelSizeEnum.Large;
            return ParcelSizeEnum.ExtraLarge;
        }

        public int GetCostFromSize(ParcelSizeEnum size)
        {
            return size switch
            {
                ParcelSizeEnum.Small => 3,
                ParcelSizeEnum.Medium => 8,
                ParcelSizeEnum.Large => 15,
                ParcelSizeEnum.ExtraLarge => 25,
                _ => throw new Exception($"Unhandled size: {size}"),
            };
        }

        public int GetWeightLimitPerSize(ParcelSizeEnum size)
        {
            return size switch
            {
                ParcelSizeEnum.Small => 1,
                ParcelSizeEnum.Medium => 3,
                ParcelSizeEnum.Large => 6,
                ParcelSizeEnum.ExtraLarge => 10,
                _ => throw new Exception($"Unhandled size: {size}"),
            };
        }

        public int GetCostFromWeight(int actualWeightKg, int weightLimitKg, int costPerKgOverweight)
        {
            if (actualWeightKg <= weightLimitKg) return 0;
            return (actualWeightKg - weightLimitKg) * costPerKgOverweight;
        }
    }
}
