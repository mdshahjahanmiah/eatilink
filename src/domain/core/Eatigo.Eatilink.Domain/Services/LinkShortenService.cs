﻿using Eatigo.Eatilink.Domain.Interfaces;
using System;
using System.Linq;

namespace Eatigo.Eatilink.Domain.Services
{
    public class LinkShortenService : ILinkShortenService
    {
        private string alphanumeric = "0123456789" +
                                      "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                      "abcdefghijklmnopqrstuvwxyz";
        public string GenerateShortUrl()
        {
            Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => alphanumeric += Convert.ToChar(i));

            string token = alphanumeric.Substring(new Random().Next(0, alphanumeric.Length), new Random().Next(2, 6));
            return token;

        }
        public int ShortUrlToId(string shortUrl)
        {
            int id = 0;
            for (int i = 0; i < shortUrl.Length; i++)
            {
                if ('a' <= shortUrl[i] && shortUrl[i] <= 'z')
                    id = id * 62 + shortUrl[i] - 'a';
                if ('A' <= shortUrl[i] && shortUrl[i] <= 'Z')
                    id = id * 62 + shortUrl[i] - 'A' + 26;
                if ('0' <= shortUrl[i] && shortUrl[i] <= '9')
                    id = id * 62 + shortUrl[i] - '0' + 52;
            }
            return id;
        }
        public string IdToBase62(int number)
        {
            var n = number;
            int basis = 62;
            var ret = "";
            while (n > 0)
            {
                int temp = n % basis;
                ret = alphanumeric[(int)temp] + ret;
                n = (n / basis);
            }
            return ret;
        }
    }
}
