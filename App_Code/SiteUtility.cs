using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

/// <summary>
/// Summary description for SiteUtility
/// </summary>
public class SiteUtility
{
    public enum SiteRoles { Developer = 0, Admin = 1, AdminLevelTwo = 2, SiteUser = 3 };

    public SiteUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string CurrentCulture
    {
        get
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["currentCulture"])))
                return "de-DE";
            return Convert.ToString(HttpContext.Current.Session["currentCulture"]);
        }
        set
        {
            HttpContext.Current.Session["currentCulture"] = value;
        }
    }












    public static void SendMail(string destinationEmail, string mailSubject, string mailBody)
    {
        MailMessage mailMessage = new MailMessage();
        MailAddress sender = new MailAddress(SiteUtility.SiteMailAddress);
        mailMessage.From = sender;
        mailMessage.To.Add(destinationEmail);
        mailMessage.Body = mailBody;
        mailMessage.IsBodyHtml = true;
        mailMessage.Subject = mailSubject;
        mailMessage.BodyEncoding = Encoding.UTF8;
        SmtpClient smtpClient = new SmtpClient("localhost");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Send(mailMessage);
    }

    public static void SendMailBatch(string[] destinationEmails, string mailSubject, string mailBody)
    {
        MailMessage mailMessage = new MailMessage();
        MailAddress sender = new MailAddress(SiteUtility.SiteMailAddress);
        mailMessage.From = sender;
        foreach (string mail in destinationEmails)
        {
            if (string.IsNullOrEmpty(mail) || !Regex.IsMatch(mail, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", RegexOptions.IgnoreCase))
            {
                continue;
            }
            mailMessage.To.Add(mail);
        }
        if (mailMessage.To.Count == 0)
            return;
        mailMessage.Body = mailBody;
        mailMessage.IsBodyHtml = true;
        mailMessage.Subject = mailSubject;
        mailMessage.BodyEncoding = Encoding.UTF8;
        SmtpClient smtpClient = new SmtpClient("localhost");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Send(mailMessage);
    }

    public static string SiteRoot
    {
        get
        {
            if (HttpContext.Current.Request.Url.Port != 80 || HttpContext.Current.Request.Url.Port != 8080)
                return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host).Trim('/') + ":" + HttpContext.Current.Request.Url.Port + "/";
            else
                return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host).Trim('/') + "/";
        }

    }
    public static string SiteRootPictures
    {
        get
        {
            if (HttpContext.Current.Request.Url.Port != 80 || HttpContext.Current.Request.Url.Port != 8080)
                return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host).Trim('/') + ":" + HttpContext.Current.Request.Url.Port + "/";
            else
                return (HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host).Trim('/');
        }

    }
    public static string SiteRootWebconfig
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["siteRoot"];
        }
    }

    public static string SearchKey
    {
        get
        {
            return Convert.ToString(HttpContext.Current.Session["searchKey"]);
        }

        set
        {
            HttpContext.Current.Session["searchKey"] = value;
        }
    }

    public static string SiteMailAddress
    {
        get
        {
            return "info@newyorker.aminamiridarban.ir";
        }
    }
    public static string ClientIp()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
                return addresses[0];
        }
        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public static int? InPayOrderId
    {
        get
        {
            int retVal = 0;
            if (!int.TryParse(Convert.ToString(HttpContext.Current.Session["InPayOrderId"]), out retVal))
            {
                return null;
            }
            return retVal;
        }
        set
        {
            if (value.HasValue)
            {
                HttpContext.Current.Session["InPayOrderId"] = value.Value;
            }
            else
            {
                HttpContext.Current.Session.Remove("InPayOrderId");
            }
        }
    }

}