
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
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Corresponding WebApi conversation method")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    protected virtual Models.ResponsedModel NewYorkerQueryList_Post(AllEnums.UserInfo.Gender gender, SerializableDictionary<string, string> requestedFiltersCollection, int? pageNo, int? perPageItems)
    {
        Models.ResponsedModel responseModel = new Models.ResponsedModel();
        string offset = pageNo > 0 && perPageItems > 0
            ?
            ((pageNo.HasValue ? (System.Convert.ToInt32(pageNo) - pageNo) : 1) * ((perPageItems.HasValue ? System.Convert.ToInt32(perPageItems) + 1 : perPageItems))).ToString()
            :
            string.Empty;


        System.Text.StringBuilder requestUrl = new System.Text.StringBuilder("https://api.newyorker.de/csp/products/public/query?");
        try
        {
            System.Net.WebClient myWebClient = new System.Net.WebClient();


            if (!string.IsNullOrEmpty(AllEnums.UserInfo.GenderEnglishName(gender)))
                requestedFiltersCollection.Add("gender", AllEnums.UserInfo.GenderEnglishName(gender));
            requestedFiltersCollection.Add("country", "de");
            foreach (System.Collections.Generic.KeyValuePair<string, string> requestedFilter in requestedFiltersCollection)
                requestUrl.Append(string.Format("filters[{0}]={1}&", requestedFilter.Key, requestedFilter.Value));
            if (!string.IsNullOrEmpty(offset))
            {
                requestedFiltersCollection.Add("offset", offset);//offset & limit must have diffrent expression from other filters, without 'filters'
                requestedFiltersCollection.Add("limit", perPageItems.ToString());//offset & limit must have diffrent expression from other filters, without 'filters'
            }
            string t = requestUrl.ToString();
            string responseFromServer = System.Text.Encoding.ASCII.GetString(myWebClient.DownloadData(requestUrl.ToString()));
            responseModel = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(responseFromServer, responseModel);
        }
        catch (System.Exception ex)
        {
            SaveLog("WebApiCorrspondingSpeech",
                "NewYorkerQueryList_Post",
                new System.TimeSpan(0),
                "username",
                AllEnums.LogOperation.Events.View,
                string.Format("Message : {0}; StackTrace : {1}; InnerException : {2}; ResponsedModel : {3}",
                ex.Message,
                ex.StackTrace,
                ex.InnerException,
                responseModel));

            //To ensure that, there is always a result back, if in test or running local may be a problem in connection to the new yorker api 
            //>This is a real experiance when developer tried to connect your website or Api from Iran, look like you banned Iran ip.
            responseModel = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(System.IO.File.ReadAllText(Server.MapPath("~/uploads/Top100_QuerySampleResult.json")), responseModel);
        }

        return responseModel;
    }

}
