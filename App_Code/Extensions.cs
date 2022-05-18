using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for Extensions
/// </summary>
public static class Extensions
{
    public static T GetRandomItem<T>(this IEnumerable<T> list)
    {
        return list.ElementAt(new Random(DateTime.Now.Millisecond).Next(list.Count()));
    }

    public static IEnumerable<Enum> GetValues(Enum enumeration)
    {
        List<Enum> enumerations = new List<Enum>();
        foreach (System.Reflection.FieldInfo fieldInfo in enumeration.GetType().GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
        {
            enumerations.Add((Enum)fieldInfo.GetValue(enumeration));
        }
        return enumerations;
    }
    public static string ToStringNullSafe<T>(this T value)
    {
        string retVal = null;
        if (value != null)
        {
            retVal = value.ToString();
        }
        return retVal;
    }

   
    public static int? ToNullableInt<T>(this T value)
    {
        int i;
        if (int.TryParse(value.ToString(), out i)) return i;
        return null;
    }
    public static T ParseEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
    public static string ValidUrl(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        return Regex.Replace(str, @"[^\w\d-\/]", "_");
    }

    public static string ValidProductUrl(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        return Regex.Replace(str, @"_", ".");
    }
    public static string UrlExtraction(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        return Regex.Replace(str, "_", " ");
    }
    public static byte[] AddByteToArray(this byte[] bArray, byte newByte)
    {
        byte lenght = bArray != null ? Convert.ToByte(bArray.Length) : Convert.ToByte(0);
        byte[] newArray = new byte[lenght + 1];
        bArray.CopyTo(newArray, 1);
        newArray[0] = newByte;
        return newArray;
    }
    public static T FromJSon<T>(this string _json)
    {
        var retval = (T)Activator.CreateInstance(typeof(T));
        if (_json != null)
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(_json)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
                retval = (T)Convert.ChangeType(deserializer.ReadObject(ms), typeof(T));
            }
        return retval;
    }
    public static Dictionary<K, V> FromJSon<K, V>(this string _json)
    {
        Dictionary<K, V> retval = new Dictionary<K, V>();
        if (_json != null)
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(_json)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Dictionary<K, V>));
                retval = (Dictionary<K, V>)deserializer.ReadObject(ms);
            }
        return retval;
    }
    public static string ToJson<T>(this T _object)
    {
        string retVal = "{}";
        if (_object != null)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, _object);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            retVal = sr.ReadToEnd();
            sr.Close();
            msObj.Close();
        }
        return retVal;
    }
    public static string ToJson<K, V>(this Dictionary<K, V> _objectDictionary)
    {
        string retVal = "[]";
        if (_objectDictionary != null && _objectDictionary.Count > 0)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Dictionary<K, V>));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, _objectDictionary);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            retVal = sr.ReadToEnd();
            sr.Close();
            msObj.Close();
        }
        return retVal;
    }
    public static string ToPersian(this DateTime datetime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return persianCalendar.GetYear(datetime).ToString() + "/" +
            persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
            persianCalendar.GetDayOfMonth(datetime).ToString("0#") + "-" + datetime.Hour.ToString("0#") + ":" + datetime.Minute.ToString("0#");
    }
    public static string ToShortPersian(this DateTime datetime)
    {
        try
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#");
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string ToPersianDateTime(this DateTime datetime)
    {
        try
        {
            datetime = datetime.ToLocalTime();
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetHour(datetime).ToString("0#") + ":" + persianCalendar.GetMinute(datetime).ToString("0#") + ":" + persianCalendar.GetSecond(datetime).ToString("0#") + "  <sub>&#128337;</sub>  " +
                persianCalendar.GetYear(datetime).ToString() + "/" +
                persianCalendar.GetMonth(datetime).ToString("0#") + "/" +
                persianCalendar.GetDayOfMonth(datetime).ToString("0#");

        }
        catch
        {
            return string.Empty;
        }
    }
    public static string ToPersianWithNamedMonth(this DateTime datetime)
    {
        string retVal = string.Empty;
        try
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetDayOfMonth(datetime).ToString() + " " +
                PersianMonthName((byte)persianCalendar.GetMonth(datetime)) + " " +
                persianCalendar.GetYear(datetime).ToString();
        }
        catch
        {
            retVal = string.Empty;
        }
        return retVal;
    }
    public static string ToPersianWithNamedMonth2(this DateTime datetime)
    {
        string retVal = string.Empty;
        try
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetDayOfMonth(datetime).ToString() + " " +
                PersianMonthName((byte)persianCalendar.GetMonth(datetime)) + " " + "ماه" + " " +
                persianCalendar.GetYear(datetime).ToString();
        }
        catch
        {
            retVal = string.Empty;
        }
        return retVal;
    }
    public static string PersianMonthName(byte month)
    {
        string retVal = string.Empty;
        switch (month)
        {
            case 1:
                retVal = "فروردین";
                break;
            case 2:
                retVal = "اردیبهشت";
                break;
            case 3:
                retVal = "خرداد";
                break;
            case 4:
                retVal = "تیر";
                break;
            case 5:
                retVal = "مرداد";
                break;
            case 6:
                retVal = "شهریور";
                break;
            case 7:
                retVal = "مهر";
                break;
            case 8:
                retVal = "آبان";
                break;
            case 9:
                retVal = "آذر";
                break;
            case 10:
                retVal = "دی";
                break;
            case 11:
                retVal = "بهمن";
                break;
            case 12:
                retVal = "اسفند";
                break;
        }
        return retVal;
    }
    public static string PersianDayOfWeek(this DateTime datetime)
    {
        string retVal = string.Empty;

        switch (datetime.DayOfWeek)
        {
            case DayOfWeek.Friday:
                retVal = "جمعه";
                break;
            case DayOfWeek.Monday:
                retVal = "دوشنبه";
                break;
            case DayOfWeek.Saturday:
                retVal = "شنبه";
                break;
            case DayOfWeek.Sunday:
                retVal = "یکشنبه";
                break;
            case DayOfWeek.Tuesday:
                retVal = "سه شنبه";
                break;
            case DayOfWeek.Thursday:
                retVal = "پنجشنبه";
                break;
            case DayOfWeek.Wednesday:
                retVal = "چهارشنبه";
                break;
            default:
                break;
        }

        return retVal;
    }
    public static int PersianYear(this DateTime datetime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return persianCalendar.GetYear(datetime);
    }
    public static string PersianMonth2(this DateTime datetime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return PersianMonthName((byte)persianCalendar.GetMonth(datetime)) + " " +
            persianCalendar.GetYear(datetime).ToString();
    }
    public static int PersianMonth(this DateTime datetime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return persianCalendar.GetMonth(datetime);
    }
    public static int PersianDay(this DateTime datetime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        return persianCalendar.GetDayOfMonth(datetime);
    }
    public static string PersianTime(this DateTime datetime)
    {
        return datetime.Hour.ToString() + ":" + datetime.Minute.ToString();
    }

    public static bool IsInDate(this DateTime datetime1, DateTime datetime2)
    {
        return datetime1.Date == datetime2.Date;
    }

    public static string ValidPersian(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        else
        {
            str = Regex.Replace(Regex.Replace(Regex.Replace(str, "ك", "ک", RegexOptions.IgnoreCase), "ي", "ی"), "\\s{2,}", "\\s", RegexOptions.IgnoreCase);
            return str;

        }
    }

    public static string CorrectNumbers(this string number)
    {
        if (number != null && number != string.Empty)
        {
            number = number.ToString().Replace("0", "۰");
            number = number.ToString().Replace("1", "۱");
            number = number.ToString().Replace("2", "۲");
            number = number.ToString().Replace("3", "۳");
            number = number.ToString().Replace("4", "۴");
            number = number.ToString().Replace("5", "۵");
            number = number.ToString().Replace("6", "۶");
            number = number.ToString().Replace("7", "۷");
            number = number.ToString().Replace("8", "۸");
            number = number.ToString().Replace("9", "۹");
            return number.ToString();
        }
        return 0.ToString();
    }

    public static string SecureText(this string str)
    {
        return HttpContext.Current.Server.HtmlEncode(str);
    }

    public static string SecureText(this object str)
    {
        return HttpContext.Current.Server.HtmlEncode(Convert.ToString(str));
    }

    public static List<T> GetSelectedId<T>(this System.Web.UI.WebControls.ListView listview, string selectControlId)
    {
        List<T> retVal = new List<T>();
        foreach (System.Web.UI.WebControls.ListViewItem item in listview.Items)
        {
            if (item.ItemType == System.Web.UI.WebControls.ListViewItemType.DataItem)
            {
                var selectControl = item.FindControl(selectControlId);
                if ((selectControl is System.Web.UI.WebControls.CheckBox) && ((selectControl as System.Web.UI.WebControls.CheckBox).Checked))
                {
                    retVal.Add((T)listview.DataKeys[item.DisplayIndex].Value);
                }
                if ((selectControl is System.Web.UI.WebControls.RadioButton) && ((selectControl as System.Web.UI.WebControls.RadioButton).Checked))
                {
                    retVal.Add((T)listview.DataKeys[item.DisplayIndex].Value);
                }
            }
        }
        return retVal;
    }

    public static int GetIdIndex<T>(this System.Web.UI.WebControls.ListView listview, T id)
    {
        int retVal = -1;
        for (int i = 0; i < listview.DataKeys.Count; i++)
        {
            if (((T)listview.DataKeys[i].Value).Equals(id))
            {
                retVal = i;
                break;
            }
        }
        return retVal;
    }

    public static string ToPersianCurrency(this object val)
    {
        return string.Format("{0:C0}", val);
    }
}