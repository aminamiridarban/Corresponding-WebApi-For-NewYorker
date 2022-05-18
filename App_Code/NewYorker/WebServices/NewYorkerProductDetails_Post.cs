//<%@ WebHandler Language = "C#" Class="aadAjaxLoader" %>

public partial class CorrespondingWebApi : BaseWebServices
{
    /// <summary>
    /// This is the Corresponding WebApi conversation method
    /// </summary>
    /// <param name="requestedFiltersCollection">Recive a collection of query string parameters</param>
    /// <param name="pageNo">Get page number to calculate the offset in query parameter</param>
    /// <param name="perPageItems">Get items count per each page to calculate the offset and set limit in query parameter</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Corresponding WebApi conversation method to get product details")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.Item))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    protected virtual Models.Item NewYorkerProductDetails_Post(string productId, string lang)
    {
        Models.Item responseItem = new Models.Item();
        string requestUrl = string.Format("https://api.newyorker.de/csp/products/public/product/{0}?country={1}", productId, lang);
        try
        {
            System.Net.WebClient myWebClient = new System.Net.WebClient();


            string responseFromServer = System.Text.Encoding.ASCII.GetString(myWebClient.DownloadData(requestUrl));
            responseItem = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responseFromServer, responseItem);
        }
        catch (System.Exception ex)
        {
            SaveLog("WebApiCorrspondingSpeech",
                "NewYorkerProductDetails_Post",
                new System.TimeSpan(0),
                "username",
                AllEnums.LogOperation.Events.View,
                string.Format("Message : {0}; StackTrace : {1}; InnerException : {2}; ResponsedModel : {3}",
                ex.Message, ex.StackTrace, ex.InnerException, responseItem));

            //To ensure that, there is always a result back, if in test or running local may be a problem in connection to the new yorker api 
            //>This is a real experiance when developer tried to connect your website or Api from Iran, look like you banned Iran ip.
            responseItem = (Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(System.IO.File.ReadAllText(Server.MapPath("~/uploads/03.01.106.0178.json")), responseItem));
        }

        return responseItem;
    }

}
