using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trader
{
    public class Skin
    {
        public int id;
        public string name;
        public string collection;
        public float minFloat = 0.0f;
        public float maxFloat = 1.0f;
        public SkinPrices prices;
        public int rarity;

        public Skin()
        {
            prices = new SkinPrices();
        }

        public Skin(string _name, int _rarity, float _min, float _max, string _collection)
        {
            name = _name;
            rarity = _rarity;
            minFloat = _min;
            maxFloat = _max;
            collection = _collection;
            prices = new SkinPrices();
        }

        public Skin(string q)
        {
            string[] args = q.Split(',');
            name = args[0];
            rarity = Int32.Parse(args[1]);
            minFloat = float.Parse(args[2]);
            maxFloat = float.Parse(args[3]);
            collection = args[4];
            prices = new SkinPrices();
        }

        public bool allowsCondition(string codition)
        {
            //MessageBox.Show(name + " " + codition + " Min: " + minFloat + " Max: " + maxFloat);
            if (codition == "Factory New") return (minFloat < 0.07f);
            if (codition == "Minimal Wear") return (minFloat < 0.15f && maxFloat > 0.07);
            if (codition == "Field-Tested") return (minFloat < 0.38f && maxFloat > 0.15);
            if (codition == "Well-Worn") return (minFloat < 0.45f && maxFloat > 0.38);
            if (codition == "Battle-Scarred") return (maxFloat > 0.45);
            return false;
        }

        public string getFullName(string stat, string condition)
        {
            return stat + name + " (" + condition + ")";
        }

        public string ParseName(SkinArgs args)
        {
            string result = "";
            if (args.statTrak) result += "StatTrak™ ";
            if (args.souvenir) result += "Souvenir ";

            result += name + " ";

            if (args.condition == 0) result += "(Factory New)";
            if (args.condition == 1) result += "(Minimal Wear)";
            if (args.condition == 2) result += "(Field Tested)";
            if (args.condition == 3) result += "(Well Worn)";
            if (args.condition == 4) result += "(Battle Scarred)";

            return result;
        }
    }

    public class SkinPrices
    {
        public float[] prices;
        public SkinPrices()
        {
            prices = new float[5];
            for(int i=0;i<5;i++)
                prices[i] = -1;
        }
    }

    public class SkinArgs
    {
        public bool statTrak = false;
        public bool souvenir = false;
        public int condition;

        public static int floatToCondition(float condition)
        {
            if (condition < 0.07f) return 0;
            if (condition < 0.15f) return 1;
            if (condition < 0.38f) return 2;
            if (condition < 0.45f) return 3;
            return 4;
        }
    }
}
