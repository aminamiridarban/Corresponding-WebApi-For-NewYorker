/// <summary>
/// Summary description for NewYorkerApiController
/// </summary>
public class BaseServices
{
    protected string _language { get; set; }
    protected AllEnums.UserInfo.Gender _gender { get; set; }
    protected int? _pageNo { get; set; }
    protected int? _perPageItems { get; set; }


    protected SerializableDictionary<string, string> _filtersString = new SerializableDictionary<string, string>();

    protected Models.ResponsedModel _productGroup;



    protected BaseServices()
    {
        _productGroup = null;
    }
    protected BaseServices(string language) : base()
    {
        _language = language;
    }
    protected BaseServices(string language, AllEnums.UserInfo.Gender gender) : base()
    {
        _gender = gender;
        _language = language;
    }
    protected BaseServices(string lang, AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems) : base()
    {
        _pageNo = pageNo;
        _perPageItems = perPageItems;
        _gender = gender;
        _language = lang;
    }
    protected BaseServices(string lang, AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, SerializableDictionary<string, string> filtersString) : base()
    {
        _language = lang;
        _gender = gender;
        _pageNo = pageNo;
        _perPageItems = perPageItems;
        _filtersString = filtersString;
    }

    protected BaseServices(AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, string categories) : base()
    {
        _gender = gender;
        _pageNo = pageNo;
        _perPageItems = perPageItems;
        _filtersString.Remove("web_category");
        _filtersString.Add("web_category", categories);
    }

    protected BaseServices(string lang, AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, string categories) : base()
    {
        _language = lang;
        _gender = gender;
        _pageNo = pageNo;
        _perPageItems = perPageItems;
        _filtersString.Remove("web_category");
        _filtersString.Add("web_category", categories);
    }

    protected BaseServices(AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, SerializableDictionary<string, string> filtersString) : base()
    {
        _gender = gender;
        _pageNo = pageNo;
        _perPageItems = perPageItems;
        _filtersString = filtersString;
    }







}