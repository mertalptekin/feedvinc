using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public static class NameExtention
    {

        /// <summary>
        /// Extention sınıfların sadece static olan sınıflardan farkı extention sınıfları uygulama içinde bir değerin kendi methodumuş gibi kullanmamı sağlar
        /// SiteManager.GetList(); 
        /// yukardaki tanım ise sadece SiteManagerdan instance almadan GetList methodunu çağırmaya yarar
        /// sadece name isimli değişkene etki eder name.Capitalize();
        /// SiteManager.GetList(value);  value.GetList();
        /// Eğer c# da extention genişletilebilir bir sınıf yazmak istiyorsa sınıfı static yazma zorunluluğumuz vardır
        /// ve hangi değer için bir extention tanımlıyorsak o tipe özgü yamamız gerekir this string => this Math this int
        /// </summary>
        /// <param name="value">bu extentionın uygulandığı değişkenin değeri</param>
        /// <returns></returns>
        public static NameModel SetRuleBasedName(this string value)
        {
            value = value.Trim();
            string[] Names = value.Split(' ').ToArray();
            string name = "";
            string surname = "";

            for (int i = 0; i < Names.Length; i++)
            {
                if (i == (Names.Length - 1))
                {
                    surname = Names[i].ToUpper();
                }
                else
                {
                    name += Names[i].Substring(0, 1).ToUpper() + Names[i].Substring(1, Names[i].Length - 1) + " ";
                }
            }

            name = name.Trim();
            surname = surname.Trim();


            return new NameModel { Name = name, Surname = surname };

        }
    }

}