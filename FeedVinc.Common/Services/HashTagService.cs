using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public static class HashTagService
    {
        public static List<string> GetHashTags(this string value)
        {
            var hashTags = value.Split(' ').ToList();

            hashTags.RemoveAll(item => !item.Contains("#"));

            return hashTags;
        }

        public static string ModifyHashTagInput(this string value)
        {
            var input = value.Split(' ').ToList();

            var hashTags = input.FindAll(item => item.Contains("#"));

            for (int i = 0; i < hashTags.Count; i++)
            {
                hashTags[i] = "<a style='color:#db9e36;' href='/hashtag?filter=" + hashTags[i] + "'>" + hashTags[i] + "</a>";
            }

            int counter = 0;

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Contains("#"))
                {
                    input[i] = hashTags[counter];
                    counter++;
                }
            }

            return String.Join(" ",input);
        }
    }
}
