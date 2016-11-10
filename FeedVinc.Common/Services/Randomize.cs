using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public static class Randomize
    {
        

        public static List<long> GenerateRandomOrder(this int MaxSize,int Quantity)
        {
            List<long> _assingnedRandomNumbers = new List<long>();

            if (MaxSize<Quantity)
                Quantity = MaxSize;

            for (int i = 0; i < Quantity; i++)
            {
                Random rdm = new Random();
                int random = rdm.Next(1, MaxSize + 1);

                 while (_assingnedRandomNumbers.Contains(random)){
                   random = rdm.Next(1, MaxSize +1);
                }

                _assingnedRandomNumbers.Add(random);
            }

            return _assingnedRandomNumbers;

        }
    }
}
