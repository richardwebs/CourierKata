using CourierKata.WebAPI.Models;
using System;

namespace CourierKata.WebAPI.Services
{
    public interface IParcelHelper
    {
        ParcelSizeEnum GetParcelSizeFromDimensions(int widthCm, int heightCm, int lengthCm);
        int CalculateCostFromSize(ParcelSizeEnum size);
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

        public int CalculateCostFromSize(ParcelSizeEnum size)
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
    }
}
