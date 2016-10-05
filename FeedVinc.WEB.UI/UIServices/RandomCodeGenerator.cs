using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public class RandomCodeGenerator
    {
        public static string Generate()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 10000;
            return String.Format("{0:D5}", random);
        }
    }
}