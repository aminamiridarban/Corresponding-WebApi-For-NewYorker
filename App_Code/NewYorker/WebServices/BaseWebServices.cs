using System.Linq;
/// <summary>
/// This Class as WebApi is used to control the request/responses for the external devices and offline synchronization
/// </summary>
[System.Web.Services.WebService(Namespace = "NewYorkerInterView", Name = "Interview Case Study", Description = "NewYorker InterView Corresponding WebApi")]
[System.Web.Services.WebServiceBinding(ConformsTo = System.Web.Services.WsiProfiles.BasicProfile1_1)]

[System.Web.Script.Services.ScriptService]
public abstract class BaseWebServices : System.Web.Services.WebService
{
    public abstract Models.ResponsedModel GetProductsByCategoryName(string categoryName);
    public abstract Models.ResponsedModel GetProductsForCategoryNames(string categoryNames, AllEnums.UserInfo.Gender gender);
    public abstract Models.ResponsedModel GetProductsByGender(AllEnums.UserInfo.Gender gender);
    public abstract Models.ResponsedModel GetProductsByCategoryId(string categoryId);
    public abstract Models.ResponsedModel GetProductsByBrand(string brand);
    public abstract Models.ResponsedModel GetProductsByColor(string color);
    public abstract Models.ResponsedModel GetProductsBySize(string size);
    public abstract Models.ResponsedModel GetProductsByPriceRange(double minPrice, double maxPrice);
    public abstract Models.ResponsedModel GetProductsByPrice(double price);
    protected void SaveLog(string pageUrl, string tableNames, System.TimeSpan eventDuration, string userId, AllEnums.LogOperation.Events operation, string description)
    {

        System.Text.StringBuilder log = new System.Text.StringBuilder();

        log.Append(string.Format("DateX : ", System.DateTime.Now));
        log.Append(string.Format("Detail : ", description));
        log.Append(string.Format("Event : ", (byte)operation));
        log.Append(string.Format("PageUrl : ", pageUrl));
        log.Append(string.Format("RelatedTables : ", tableNames));


        System.Console.WriteLine(log.ToString());

    }


}
