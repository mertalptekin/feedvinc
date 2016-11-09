using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public static class CryptoProvider
    {
        public static string Decrpte(this string chipperText)
        {
            const string AES_IV = @"!&+QWSDF!123126+";
            string aes_anahtar = @"QQsaw!257()%%ert";

            AesCryptoServiceProvider aes_saglayici = new AesCryptoServiceProvider();
            aes_saglayici.BlockSize = 128;
            aes_saglayici.KeySize = 128;
            aes_saglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
            aes_saglayici.Key = Encoding.UTF8.GetBytes(aes_anahtar);
            aes_saglayici.Mode = CipherMode.CBC;
            aes_saglayici.Padding = PaddingMode.PKCS7;

            byte[] kaynak = System.Convert.FromBase64String(chipperText);

            using (ICryptoTransform decrypt = aes_saglayici.CreateDecryptor())
            {
                byte[] hedef = decrypt.TransformFinalBlock(kaynak, 0, kaynak.Length);
                return Encoding.Unicode.GetString(hedef);
            }
        }

        public static string Encrypte(this string plainText)
        {
            const string AES_IV = @"!&+QWSDF!123126+"; // 16 tane karakterden oluşması lazım
            string aes_anahtar = @"QQsaw!257()%%ert"; //256 bit elde etmek için 16X16


            AesCryptoServiceProvider aes_saglayici = new AesCryptoServiceProvider();
            /*
            Şifreleme yöntemi olarak AES şifreleme yöntemini seçiyoruz.
              */

            aes_saglayici.BlockSize = 128;
            /*
            AES bloklar halinde şifreleme yapar. 
            Biz de bloklama yöntemini belirliyoruz.
              */

            aes_saglayici.KeySize = 128;
            /*
            AES şifreleme metodunda anahtar ile şifreleme yapılıyor.
            Anahtar boyutları 128, 192 ve 256 olabilir.
              */

            aes_saglayici.IV = Encoding.UTF8.GetBytes(AES_IV);
            //IV = Initial Vector
            aes_saglayici.Key = Encoding.UTF8.GetBytes(aes_anahtar);
            aes_saglayici.Mode = CipherMode.CBC;
            aes_saglayici.Padding = PaddingMode.PKCS7;

            byte[] kaynak = Encoding.Unicode.GetBytes(plainText);
            /*
              Metni byte dizisine çeviriyoruz.
              */
            using (ICryptoTransform sifrele = aes_saglayici.CreateEncryptor())
            {
                byte[] hedef = sifrele.TransformFinalBlock(kaynak, 0, kaynak.Length);
                return Convert.ToBase64String(hedef);

            }
        }


        public static string PasswordHash(this string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plainText));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();

        }
    }
}
