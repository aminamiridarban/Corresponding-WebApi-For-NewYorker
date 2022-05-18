using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    private void setCalture(string calture)
    {
        System.Globalization.CultureInfo Culture = new System.Globalization.CultureInfo(calture);
        System.Threading.Thread.CurrentThread.CurrentCulture = Culture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = Culture;
    }

    protected override void InitializeCulture()
    {
        base.InitializeCulture();
        setCalture(Convert.ToString(SiteUtility.CurrentCulture));
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    protected void SetPageCss(string CssUrl)
    {
        HtmlLink css = new HtmlLink();
        css.Href = ResolveUrl(CssUrl);
        css.Attributes["rel"] = "stylesheet";
        css.Attributes["type"] = "text/css";
        css.Attributes["media"] = "all";
        Page.Header.Controls.Add(css);
    }

    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void RegisterScriptAfterLoadAllScripts(string script)
    {
        Page.ClientScript.RegisterStartupScript(typeof(Page), DateTime.Now.Ticks.ToString(), script, true);
    }

    public void RegisterScriptDocumentReady(string script)
    {
        Page.ClientScript.RegisterStartupScript(typeof(Page), DateTime.Now.Ticks.ToString(), "$(document).ready(function(){" + script + "});", true);
    }

    public void Alert(string message)
    {
        RegisterScriptDocumentReady("alert('" + message + "');");
    }

    protected string GetPagerLink(int pageIndex, string queryName)
    {
        if (Request.RawUrl.ToLower().IndexOf(queryName.ToLower()) == -1)
        {
            string contacter = Request.RawUrl.IndexOf("?") > -1 ? "&" : "?";
            return Request.RawUrl + contacter + queryName + "=" + pageIndex;
        }
        else
        {
            return System.Text.RegularExpressions.Regex.Replace(Request.RawUrl, queryName + "\\s*=\\s*\\d*", queryName + "=" + pageIndex);
        }
    }

    protected string AddQueryLink(string link, string[] querys)
    {
        string retVal = link;
        var queryKeys = Request.QueryString.AllKeys;
        for (int i = 0; i < querys.Length; i += 2)
        {
            string queryName = querys[i];
            string queryValue = querys[i + 1];
            if (retVal.ToLower().IndexOf(queryName.ToLower()) == -1)
            {
                string contacter = "&";
                retVal = retVal + contacter + queryName + "=" + queryValue;
            }
            else
            {
                retVal = System.Text.RegularExpressions.Regex.Replace(retVal, queryName + "\\s*=.*&", "&" + queryName + "=" + queryValue);
            }
        }
        return retVal;
    }

    protected string GetPagerFields(System.Web.UI.WebControls.DataPager pager, string[] exteraQueryValue)
    {
        int pageCount = (int)(pager.TotalRowCount / pager.PageSize);
        string retVal = string.Empty;
        string contacter = Request.RawUrl.IndexOf("?") > -1 ? "&" : "?";
        int curpageindex = 1;
        if (!int.TryParse(Request[pager.QueryStringField], out curpageindex))
        {
            curpageindex = 1;
        }

        int buttonCount = 5;

        int domain = (curpageindex - 1) / buttonCount;

        if (domain * buttonCount - 1 >= 0)
        {
            retVal += "<a class=\"arrow\" href=\"" + AddQueryLink(GetPagerLink(domain * buttonCount, pager.QueryStringField), exteraQueryValue) + "\">&laquo;</a>";
        }
        else
        {
            retVal += "<a class=\"arrow\">&lsaquo;</a>";
        }

        for (int i = (domain * buttonCount); i < (domain + 1) * buttonCount && i * pager.PageSize < pager.TotalRowCount; i++)
        {
            if (i == curpageindex - 1)
            {
                retVal += "<span class=\"active\">" + (i + 1) + "</span>";
            }
            else
            {
                retVal += "<a href=\"" + AddQueryLink(GetPagerLink(i + 1, pager.QueryStringField), exteraQueryValue) + "\">" + (i + 1) + "</a>";
            }
        }

        if (((domain + 1) * buttonCount) * pager.PageSize + 1 <= pager.TotalRowCount)
        {
            retVal += "<a class=\"arrow\" href=\"" + AddQueryLink(GetPagerLink(domain * buttonCount, pager.QueryStringField), exteraQueryValue) + "\">&raquo;</a>";
        }
        else
        {
            retVal += "<a class=\"arrow\">&rsaquo;</a>";
        }

        return retVal;
    }

    protected string GetPagerFields(System.Web.UI.WebControls.DataPager pager)
    {
        int pageCount = (int)(pager.TotalRowCount / pager.PageSize);
        string retVal = string.Empty;
        string contacter = Request.RawUrl.IndexOf("?") > -1 ? "&" : "?";
        int curpageindex = 1;
        if (!int.TryParse(Request[pager.QueryStringField], out curpageindex))
        {
            curpageindex = 1;
        }

        int buttonCount = 5;

        int domain = (curpageindex - 1) / buttonCount;

        if (domain * buttonCount - 1 >= 0)
        {
            retVal += "<a class=\"arrow\" href=\"" + GetPagerLink(domain * buttonCount, pager.QueryStringField) + "\">&laquo;</a>";
        }
        else
        {
            retVal += "<a class=\"arrow\">&lsaquo;</a>";
        }

        for (int i = (domain * buttonCount); i < (domain + 1) * buttonCount && i * pager.PageSize < pager.TotalRowCount; i++)
        {
            if (i == curpageindex - 1)
            {
                retVal += "<span class=\"active\">" + (i + 1) + "</span>";
            }
            else
            {
                retVal += "<a href=\"" + GetPagerLink(i + 1, pager.QueryStringField) + "\">" + (i + 1) + "</a>";
            }
        }

        if (((domain + 1) * buttonCount) * pager.PageSize + 1 <= pager.TotalRowCount)
        {
            retVal += "<a class=\"arrow\" href=\"" + GetPagerLink(domain * buttonCount, pager.QueryStringField) + "\">&raquo;</a>";
        }
        else
        {
            retVal += "<a class=\"arrow\">&rsaquo;</a>";
        }

        return retVal;
    }
}