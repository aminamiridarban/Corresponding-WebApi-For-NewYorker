using System.Collections.Generic;
using System.Runtime.Serialization;

public static class MainStaticCategories
{
    private static SerializableDictionary<string, string> _damenMainCategories = new SerializableDictionary<string, string>();
    private static SerializableDictionary<string, string> _herrenMainCategories = new SerializableDictionary<string, string>();

    public static SerializableDictionary<string, string> HerrenCategoriesDictionary
    {
        get
        {
            if (_herrenMainCategories.Keys.Count == 0)
            {
                _herrenMainCategories.Add("Accessories", "WCA02305,WCA02306,WCA02304,WCA02303,WCA02308,WCA02309,WCA02307,WCA02301,WCA02302");

                _herrenMainCategories.Add("Shirts", "WCA02211");

                _herrenMainCategories.Add("T-Shirts", "WCA00221,WCA00223,WCA00222,WCA00220");

                _herrenMainCategories.Add("Sweatshirts & Pullover", "WCA02222,WCA02223,WCA02224,WCA02221");

                _herrenMainCategories.Add("Pants", "WCA02252, WCA02251, WCA02253");

                _herrenMainCategories.Add("Denim", "WCA02246, WCA02242, WCA02241, WCA02243, WCA02245, WCA02244");

            }
            return _herrenMainCategories;
        }



    }
    public static SerializableDictionary<string, string> DamenCategoriesDictionary
    {
        get
        {
            if (_damenMainCategories.Keys.Count == 0)
            {
                _damenMainCategories.Add("Accessories", "WCA01156,WCA01159,WCA01155,WCA01152,WCA01158,WCA01153,WCA01157,WCA01154");

                _damenMainCategories.Add("Blouses", "WCA00122,WCA00121");

                _damenMainCategories.Add("Tops & T-Shirts", "WCA00111,WCA00112,WCA00110");

                _damenMainCategories.Add("Sweatshirts", "WCA00132,WCA00131");

                _damenMainCategories.Add("Pants", "WCA00172,WCA00173,WCA00171");

                _damenMainCategories.Add("Skirts", "WCA00161,WCA00162,WCA00163");
            }
            return _damenMainCategories;
        }

    }

}