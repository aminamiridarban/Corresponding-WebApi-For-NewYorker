[System.Web.Services.WebService(Namespace = "NewYorkerInterView", Name = "Interview Case Study", Description = "NewYorker InterView Corresponding WebApi")]
[System.Web.Services.WebServiceBinding(ConformsTo = System.Web.Services.WsiProfiles.BasicProfile1_1)]

[System.Web.Script.Services.ScriptService]

public partial class CorrespondingWebApi : BaseWebServices
{ /// <summary>
  /// This is the Corresponding WebApi conversation method
  /// </summary>
  /// <param name="requestedFiltersCollection">Recive a collection of query string parameters</param>
  /// <param name="pageNo">Get page number to calculate the offset in query parameter</param>
  /// <param name="perPageItems">Get items count per each page to calculate the offset and set limit in query parameter</param>
  /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products By Filter")]
    [System.Xml.Serialization.XmlInclude(typeof(string))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public Models.CorrespondingResponsedModel GetProductsByFilterString(string urlString)
    {
        Services service = new Services();
        return service.GetProductsByFilterString(urlString);
    }
    /// <summary>
    /// This is the Corresponding WebApi conversation method
    /// </summary>
    /// <param name="requestedFiltersCollection">Recive a collection of query string parameters</param>
    /// <param name="pageNo">Get page number to calculate the offset in query parameter</param>
    /// <param name="perPageItems">Get items count per each page to calculate the offset and set limit in query parameter</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get List of products base on entry parameters")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    internal virtual Models.ResponsedModel QueryList(AllEnums.UserInfo.Gender gender, SerializableDictionary<string, string> requestedFiltersCollection, int? pageNo = 1, int? perPageItems = 12)
    {
        return NewYorkerQueryList_Post(gender, requestedFiltersCollection, pageNo, perPageItems);
    }
    /// <summary>
    /// This is the method to get specified product detail
    /// </summary>
    /// <param name="productId">Id of the product in query</param>
    /// <param name="lang">country parameter in query</param>
    /// <returns>A binded model of "Models.Item" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get specified product Details")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.Item))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    internal virtual Models.Item ProductDetails(string productId, string lang)
    {
        return NewYorkerProductDetails_Post(productId, lang);
    }
    /// <summary>
    /// This method will get Products by category name
    /// </summary>
    /// <param name="categoryName">"web_category" field in newyorker data response</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products By Category Name")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByCategoryName(string categoryName)
    {
        Services service = new Services();
        return service.GetProductsByCategoryName(categoryName);
    }

    /// <summary>
    /// This method will get products by list of category name
    /// </summary>
    /// <param name="categoryName">List of "web_category" fields in newyorker data response</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products By List of Category Name")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsForCategoryNames(string categoryNames, AllEnums.UserInfo.Gender gender)
    {
        Services service = new Services(gender,0,0, categoryNames);
        return service.GetProductsForCategoryNames(categoryNames, gender);
    }

    /// <summary>
    /// This method will get products filtered by gender
    /// </summary>
    /// <param name="gender">"customer_group" fields in newyorker data response or just filter by gender</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by gender")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByGender(AllEnums.UserInfo.Gender gender)
    {
        Services service = new Services();
        return service.GetProductsByGender(gender);
    }
    /// <summary>
    /// This method will get products filtered by category id
    /// </summary>
    /// <param name="categoryId">"web_category_id" fields in newyorker data response</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by category id")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByCategoryId(string categoryId)
    {
        Services service = new Services();
        return service.GetProductsByCategoryId(categoryId);
    }
    /// <summary>
    /// This method will get products filtered by brand
    /// </summary>
    /// <param name="brand">"brand" fields in newyorker data response</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by brand")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByBrand(string brand)
    {
        Services service = new Services();
        return service.GetProductsByBrand(brand);
    }
    /// <summary>
    /// This method will get products filtered by color
    /// </summary>
    /// <param name="color">"variants -> color" fields in newyorker data response</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by color")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByColor(string color)
    {
        Services service = new Services();
        return service.GetProductsByColor(color);
    }
    /// <summary>
    /// This method will get products filtered by size
    /// </summary>
    /// <param name="size">"variants -> size" fields in newyorker data response</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by size")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsBySize(string size)
    {
        Services service = new Services();
        return service.GetProductsBySize(size);
    }
    /// <summary>
    /// This method will get products filtered by price range
    /// </summary>
    /// <param name="minPrice">Recieve as minimum price to filter</param>
    /// <param name="maxPrice">Recieve as maximum price to filter</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by price range")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByPriceRange(double minPrice, double maxPrice)
    {
        Services service = new Services();
        return service.GetProductsByPriceRange(minPrice, maxPrice);
    }
    /// <summary>
    /// This method will get products filtered by exact price with two digit percises
    /// </summary>
    /// <param name="price">Recieve as minimum price to filter</param>
    /// <returns>A binded model of "Models.ResponsedModel" which recives from the newyorker api</returns>
    [System.ServiceModel.DataContractFormat]
    [System.Web.Services.WebMethod(EnableSession = true, CacheDuration = 0, Description = "Get Products filtered by exact price with two digit percises")]
    [System.Xml.Serialization.XmlInclude(typeof(Models.ResponsedModel))]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, XmlSerializeString = false, UseHttpGet = false)]
    public override Models.ResponsedModel GetProductsByPrice(double price)
    {
        Services service = new Services();
        return service.GetProductsByPrice(price);
    }
}
