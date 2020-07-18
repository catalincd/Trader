using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace Trader
{
    public class Price
    {

        public static string GetPrice(string weaponName)
        {
            string url = @"http://steamcommunity.com/market/priceoverview/?appid=730&currency=3&market_hash_name=";
            string response = GetResponse(url + weaponName);
            ReqResponse req = JsonSerializer.Deserialize<ReqResponse>(response);

            return req.lowest_price.Substring(0, req.lowest_price.Length - 1);
        }

        public static float GetPriceFloat(string weaponName)
        {
            string price = GetPrice(weaponName);
            price = price.Replace(",", ".");
            return float.Parse(price);
        }
        public static string GetPrice(Skin skin, SkinArgs args)
        {
            string url = @"http://steamcommunity.com/market/priceoverview/?appid=730&currency=3&market_hash_name=";
            string weaponName = skin.ParseName(args);
            string response = GetResponse(url + weaponName);
            ReqResponse req = JsonSerializer.Deserialize<ReqResponse>(response);

            return req.median_price;
        }
        public static string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            return reader.ReadToEnd();
        }
    }

    [Serializable]
    public class ReqResponse
    {
        public bool success { get; set; }
        public string lowest_price { get; set; }
        public string volume { get; set; }
        public string median_price { get; set; }

        public ReqResponse()
        {
            success = false;
            lowest_price = "-1";
            median_price = "-1";
            volume = "0";
        }
    }
}
