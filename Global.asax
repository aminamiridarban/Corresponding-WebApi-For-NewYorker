<%@ Application Language="C#" %>
<script RunAt="server">

    private const string _WebApiPrefix = "api";
    private static string _WebApiExecutionPath = String.Format("~/{0}", _WebApiPrefix);
    protected void Application_PostAuthorizeRequest()
    {
        if (IsWebApiRequest())
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
    }
    private static bool IsWebApiRequest()
    {
        return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(_WebApiExecutionPath);
    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["UserSessionCount"] = 0;
        Application["AdminSessionCount"] = 0;

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception exc = Server.GetLastError();
        string applicationErrorDetail = string.Format("#Message : {0} <br> #InnerException : {1} <br> #StackTrace : {2} <hr>", exc.Message.ToString(), exc.InnerException.ToStringNullSafe(), exc.StackTrace.ToString());
        if (exc.InnerException != null)
        {
            SaveLog("Application_Error", "Error in Application Level", new TimeSpan(0), "Developer", AllEnums.LogOperation.Events.Error, applicationErrorDetail);
            // Response.Redirect(SiteUtility.SiteRoot + @"user/logout/default.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        Session.Timeout = 1440;
        Response.AppendHeader("Access-Control-Allow-Origin", "*");

        //---------------------------online user----------------------


        try
        {
            if (HttpContext.Current != null && (HttpContext.Current.Request.UrlReferrer != null && !HttpContext.Current.Request.UrlReferrer.AbsolutePath.Contains("admin")))
            {
                Application.Lock();
                if (Application["UserSessionCount"] != null)
                    Application["UserSessionCount"] = Convert.ToInt32(Application["UserSessionCount"]) + 1;
                else
                    Application["UserSessionCount"] = 1;
                Application.UnLock();

            }
            else
            {
                Application.Lock();
                if (Application["AdminSessionCount"] != null)
                    Application["AdminSessionCount"] = Convert.ToInt32(Application["AdminSessionCount"]) + 1;
                else
                    Application["AdminSessionCount"] = 1;
                Application.UnLock();
            }

        }
        catch (Exception ex)
        {
            SaveLog("Global", "Session_Start", new TimeSpan(0), null, AllEnums.LogOperation.Events.Error,
                string.Format("Message:{0}*****InnerException:{1}******StackTrace:{2}******IP ADDRESS :", ex.Message, ex.InnerException, ex.StackTrace, SiteUtility.ClientIp()));
        }

    }

    void Session_End(object sender, EventArgs e)
    {


    }
    public void SaveLog(string pageUrl, string tableNames, TimeSpan eventDuration, string userId, AllEnums.LogOperation.Events operation, string description)
    {


        StringBuilder log = new StringBuilder();

        log.Append(string.Format("DateX : ", DateTime.Now));
        log.Append(string.Format("Detail : ", description));
        log.Append(string.Format("Event : ", (byte)operation));
        log.Append(string.Format("PageUrl : ", pageUrl));
        log.Append(string.Format("RelatedTables : ", tableNames));

        // Console Log
        Console.WriteLine(log.ToString());
    }
</script>
