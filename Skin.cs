using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader
{
    public class Skin
    {
        public int id;
        public string name;
        public int collectionId;
        public string collectionName;
        public float minFloat = 0.0f;
        public float maxFloat = 1.0f;
        public string prices;
        public int rarity;

        public Skin() { }

        public Skin(string _name, int _collection = -1)
        {
            name = _name;
            collectionId = _collection;
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
