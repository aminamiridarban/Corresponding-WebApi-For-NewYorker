using System;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for Function
/// </summary>
public static class Function
{
    public static bool IsEmpty(string Str)
    {
        if (Str == null)
            if (String.IsNullOrEmpty(Str))
                if (Str == string.Empty)
                    if (Str == String.Empty)
                        if (Str == "-")
                            return true;
        return false;
    }
    public static string RandomNumber(int Count)
    {
        Random r = new Random();
        string retVal = string.Empty;
        for (int i = 1; i <= Count; i++)
            retVal += r.Next(9).ToString();
        return retVal;
    }
    public static string GetTime()
    {
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        DateTime dt = DateTime.Now;
        if (int.Parse(pc.GetDayOfMonth(DateTime.Now).ToString()) > 6)
            dt = dt.AddMinutes(double.Parse("210"));
        else
            dt = dt.AddMinutes(double.Parse("150"));
        string tdate = pc.GetYear(dt).ToString("0000") + "/" + pc.GetMonth(dt).ToString("00") + "/" + pc.GetDayOfMonth(dt).ToString("00");
        string clock = pc.GetHour(dt).ToString("00") + ":" + pc.GetMinute(dt).ToString("00") + ":" + pc.GetSecond(dt).ToString("00");

        return clock;
    }

    public static DateTime GetDays(DateTime Date, int MonthCount)
    {
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        DateTime EndDate = pc.AddMonths(Date, 2);
        return EndDate;
    }
    public static string DateNOW()
    {
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
        DateTime dt = DateTime.Now;
        if (int.Parse(pc.GetDayOfMonth(DateTime.Now).ToString()) > 6)
            dt = dt.AddMinutes(double.Parse("210"));
        else
            dt = dt.AddMinutes(double.Parse("150"));
        return pc.GetYear(dt).ToString("0000") + " , " + pc.GetMonth(dt).ToString("00") + " , " + pc.GetDayOfMonth(dt).ToString("00") + " , " +
            pc.GetHour(dt).ToString("00") + " , " + pc.GetMinute(dt).ToString("00") + " , " + pc.GetSecond(dt).ToString("00") + " , " + pc.GetMilliseconds(dt).ToString("00");
    }
    public static string StrCUT(object inpt, int Len)
    {
        string Str = Convert.ToString(inpt);
        if (Str.Length > Len)
            Str = Str.Remove(Len) + "...";
        return Str;
    }
    public static Tuple<string, string> StrDivisionTwo(object inpt, int Len)
    {
        string StrWhole = Convert.ToString(inpt);
        string Str1 = string.Empty;
        string Str2 = string.Empty;
        if (StrWhole.Length > Len)
        {
            Str1 = StrWhole.Remove(Len);
            Str2 = StrWhole.Replace(Str1, string.Empty);
        }
        return new Tuple<string, string>(Str1, Str2);
    }
    public static string InsNoPic(object Pic, string NoPicPath)
    {
        string RetVal = Convert.ToString(Pic);
        if (RetVal == string.Empty)
            RetVal = NoPicPath;
        return RetVal;
    }

    public static string makelink(string url, object inpid, object inptitle)
    {
        string id = inpid.ToString();
        string title = string.Empty;
        if (inptitle != null)
            title = inptitle.ToString().ValidUrl();
        string retVal = string.Empty;
        retVal = String.Format("{0}{1}/{2}/{3}/view/", SiteUtility.SiteRoot, url.ValidUrl(), title.ValidUrl(), id.ValidUrl());
        return retVal;
    }

    public static string makeNewYorkerlink(string url, object inptitle)
    {
        string title = string.Empty;
        if (inptitle != null)
            title = inptitle.ToString().ValidUrl();
        string retVal = string.Empty;
        retVal = String.Format("{0}{1}/{2}/view/", SiteUtility.SiteRoot, url.ValidUrl(), title.ValidUrl());
        return retVal;
    }

    public static string makeNewYorkerlink(string url, object inpGrouptitle, object inptitle, object inpid)
    {
        string retVal = string.Empty;

        string id = (inpid != null) ? inpid.ToString() : string.Empty;
        string title = (inptitle != null) ? title = inptitle.ToString().ValidUrl() : string.Empty;
        string titleGroup = (inpGrouptitle != null) ? inpGrouptitle.ToString().ValidUrl() : string.Empty;

        retVal = String.Format("{0}{1}/{2}/{3}/{4}/view/", SiteUtility.SiteRoot, url.ValidUrl(), titleGroup.ValidUrl(), title.ValidUrl(), id.ValidUrl());

        return retVal;
    }

    public static string makeNewYorkerlink(string url, object inpGrouptitle, object inptitle)
    {
        string retVal = string.Empty;

        string title = (inptitle != null) ? title = inptitle.ToString().ValidUrl() : string.Empty;
        string titleGroup = (inpGrouptitle != null) ? inpGrouptitle.ToString().ValidUrl() : string.Empty;

        retVal = String.Format("{0}{1}/{2}/{3}/view/", SiteUtility.SiteRoot, url.ValidUrl(), titleGroup.ValidUrl(), title.ValidUrl());

        return retVal;
    }



    public static string makeProductsAjaxlink(object inpUrl, object inptitle, AllEnums.UserInfo.Gender inpGender)
    {
        string retVal = string.Empty;

        string title = string.Empty;
        if (inptitle != null)
            title = inptitle.ToString().ValidUrl();

        string url = string.Empty;
        if (inpUrl != null)
            url = inpUrl.ToString().ValidUrl();

        string gender = string.Empty;

        title = inpGender.ToString().ValidUrl();

        retVal = String.Format("{0}{1}/{2}/{3}/view/", SiteUtility.SiteRoot, url, gender, title);

        return retVal;
    }


    public static string makeprice(object inp)
    {
        try
        {
            if (inp.ToString() != string.Empty)
            {
                string num = string.Format("{0:0,0}", Double.Parse(inp.ToString()));
                return CorrectNumbersEn(num);
            }
            else
            {
                return "0";
            }
        }
        catch { return "0"; }
    }

    public static string makenumber(object inp)
    {
        try
        {
            if (inp.ToString() != string.Empty)
            {
                string num = string.Format("{0:0,0}", Double.Parse(inp.ToString()));
                num = num.TrimStart('0');
                if (string.IsNullOrEmpty(num))
                    num = "0";
                return CorrectNumbersEn(num);
            }
            else
            {
                return "0";
            }
        }
        catch { return "0"; }
    }

    public static string CorrectNumbers(object number)
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
    public static string CorrectNumbersEn(object number)
    {
        number = number.ToString().Replace("۰", "0");
        number = number.ToString().Replace("۱", "1");
        number = number.ToString().Replace("۲", "2");
        number = number.ToString().Replace("۳", "3");
        number = number.ToString().Replace("۴", "4");
        number = number.ToString().Replace("۵", "5");
        number = number.ToString().Replace("۶", "6");
        number = number.ToString().Replace("۷", "7");
        number = number.ToString().Replace("۸", "8");
        number = number.ToString().Replace("۹", "9");
        return number.ToString();
    }




    public static string RandomPassGenerator(int Count)
    {
        Random r = new Random();
        string pass = string.Empty;
        for (int i = 1; i <= Count; i++)
            pass += r.Next(9).ToString();
        return pass;
    }

    #region Check Exist Page
    /// <summary>
    /// TableName as string
    /// Id = querystring["id"].tostring()
    /// TitleURL= querystring["title"].tostring()
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="Id"></param>
    /// <param name="TitleUrl"></param>
    /// <returns></returns>
    public static bool ExistPage(string TableName, string Id, string TitleUrl)
    {
        bool retVal = false;
        ParameterCollection Param = new ParameterCollection();
        SqlDataAdapter SDA = new SqlDataAdapter("SELECT Title, TitleURL FROM " + TableName + " WHERE Id=@id", ConfigurationManager.ConnectionStrings["CSConnectionString"].ToString());
        SDA.SelectCommand.Parameters.AddWithValue("@id", Id);
        DataSet DS = new DataSet();
        DS.Reset();
        SDA.Fill(DS);
        DataTable dtTitle = DS.Tables[0];
        if (dtTitle.Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(dtTitle.Rows[0]["TitleURL"].ToString().Trim()) && dtTitle.Rows[0]["Title"].ToString().SecureText().ValidPersian().ValidUrl().CompareTo(TitleUrl) == 0)
            {
                retVal = true;
            }
            else if (!string.IsNullOrEmpty(dtTitle.Rows[0]["TitleURL"].ToString().Trim()) && dtTitle.Rows[0]["TitleURL"].ToString().SecureText().ValidPersian().ValidUrl().CompareTo(TitleUrl) == 0)
            {
                retVal = true;
            }
            else { retVal = false; }
        }
        return retVal;
    }
    #endregion

    #region SQLCommandExecute
    public static void SQLCommandExecute(string SQLStr)
    {
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["CSConnectionString"].ConnectionString);
        sqlCon.Open();
        SqlCommand sqlCom = new SqlCommand(SQLStr, sqlCon);
        sqlCom.ExecuteNonQuery();
        sqlCon.Close();
    }
    #endregion

    public static string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

}