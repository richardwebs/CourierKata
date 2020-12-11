using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierKata.WebAPI.Models;

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
            return ParcelSizeEnum.ExtraLarge;
        }

        public int CalculateCostFromSize(ParcelSizeEnum size)
        {
            return 0;
        }
    }
}
