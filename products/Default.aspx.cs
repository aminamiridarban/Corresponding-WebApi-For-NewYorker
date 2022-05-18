using System.Linq;

public partial class Sample_Default : BasePage
{
    Services services = null;
    public override void Dispose()
    {
        base.Dispose();
    }

    protected void Page_Init(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
            services = new Services();


    }

    protected string SelectedLanguage
    {
        get
        {
            return Session["lang"] != null ? Session["lang"].ToString() : "DE";
        }
    }

    protected AllEnums.UserInfo.Gender SelectedGender
    {
        get
        {
            AllEnums.UserInfo.Gender retVal = AllEnums.UserInfo.Gender.UnKnown;
            if (Request.QueryString["gender"] != null)
            {
                retVal = (AllEnums.UserInfo.Gender)System.Enum.Parse(typeof(AllEnums.UserInfo.Gender), Request.QueryString["gender"], true);
            }
            return retVal;
        }
    }

    protected string SelectedFilters
    {
        get
        {
            string retVal = null;
            if (Request.QueryString["category-filters"] != null)
            {
                return Request.QueryString["category-filters"];
            }
            return retVal;
        }
    }

    public string GetProductsCategorizedByGender()
    {
        System.Text.StringBuilder retVal = new System.Text.StringBuilder();

        if (SelectedGender != AllEnums.UserInfo.Gender.UnKnown)
            retVal.Append(getCategories(SelectedGender));
        else
        {
            retVal.Append(string.Format("<li><a href='{0}'><span class='cCat-Close'>Herren Produkte</span>{1}</a></li>",
                Function.makeNewYorkerlink("Produkte", AllEnums.UserInfo.Gender.Herren.ToString()),
                getCategories(AllEnums.UserInfo.Gender.Herren)));

            retVal.Append(string.Format("<li><a href='{0}'><span class='cCat-Close'>Damen Produkte</span>{1}</a></li>",
                Function.makeNewYorkerlink("Produkte", AllEnums.UserInfo.Gender.Damen.ToString()),
                getCategories(AllEnums.UserInfo.Gender.Damen)));
        }

        return retVal.ToString();
    }
    private string getCategories(AllEnums.UserInfo.Gender gender)
    {
        System.Text.StringBuilder retVal = new System.Text.StringBuilder();
        SerializableDictionary<string, string> staticCategories = AllEnums.UserInfo.Gender.Damen == gender ? MainStaticCategories.DamenCategoriesDictionary : MainStaticCategories.HerrenCategoriesDictionary;

        foreach (System.Collections.Generic.KeyValuePair<string, string> category in staticCategories)
            retVal.Append(string.Format("<li><a href='{0}' data-filter='{1}'><span class='cCat-Open'>{2}</span></a><ul>{3}</ul></li>",
                Function.makeNewYorkerlink(
                    string.Format("Produkte/{0}",
                    gender),
                    category.Key,
                    category.Value),
                category.Value,
                category.Key,
                getSubCategories(category, gender)));


        return retVal.ToString();
    }
    private string getSubCategories(System.Collections.Generic.KeyValuePair<string, string> parentCategory, AllEnums.UserInfo.Gender gender)
    {
        System.Text.StringBuilder retVal = new System.Text.StringBuilder(string.Empty);

        foreach (var category in parentCategory.Value.Split(','))
        {
            SerializableDictionary<string, string> filters = new SerializableDictionary<string, string>();
            filters.Add("web_category", category);

            
            Models.ResponsedModel productsModel = new Models.ResponsedModel();
            productsModel = services.GetProductsByCategoryName(category);
            //Due to slow connection to fetch all categories and cause the was no method to get categories in guide i fetch them from sample for categories,
            //productsModel = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(System.IO.File.ReadAllText(Server.MapPath("~/uploads/Top100_QuerySampleResult.json")), productsModel);

            //Group by result base on maintenance_group(group title) and destract appropriate description base on language
            var Groups = productsModel.items.GroupBy(p => p.web_category).Distinct()
                .Select(g => g.First())
                .Select(p =>
                new
                {
                    id = p.web_category_id,
                    group = p.web_category,
                    summary = p.descriptions.FirstOrDefault(v => v.language.ToLower() == SelectedLanguage.ToLower()).description
                })
                .Where(p => p.group == category)
                .ToList();

            foreach (var productItem in Groups.ToList())
                //Producing html and anchor link to the product
                retVal.Append(
                    string.Format("<li><a href='{0}'><span class='cCat-Sub'>{1}</span></a></li>",
                    Function.makeNewYorkerlink(
                        string.Format("Produkte/{0}", gender.ToString()),
                        productItem.summary,
                        productItem.group),
                    productItem.summary));

        }

        return retVal.ToString();
    }

    protected string GetPageTitle()
    {
        System.Text.StringBuilder retVal = new System.Text.StringBuilder();
        if (SelectedGender != AllEnums.UserInfo.Gender.UnKnown)
            retVal.Append(string.Format("{0} ", SelectedGender.ToString()));
        if (!string.IsNullOrEmpty(SelectedFilters))
            retVal.Append(string.Format("» {0}", SelectedFilters.UrlExtraction().Split('/').FirstOrDefault(s => !s.Contains("WCA"))));
        if (retVal.Length <= 0)
            retVal.Append("NewYorker Prudukten");
        return retVal.ToString();
    }
    protected string GetBreadCrumb()
    {
        System.Text.StringBuilder retVal = new System.Text.StringBuilder();
        if (SelectedGender != AllEnums.UserInfo.Gender.UnKnown && !string.IsNullOrEmpty(SelectedFilters))
            retVal.Append(string.Format("<a id='{0}' href='{1}'>{0}</a>", SelectedGender.ToString(), Function.makeNewYorkerlink("Produkte", SelectedGender.ToString())));
        else
            retVal.Append(string.Format("<span class='cur'>{0}</span>", SelectedGender != AllEnums.UserInfo.Gender.UnKnown ? SelectedGender.ToString() : string.Empty));

        if (!string.IsNullOrEmpty(SelectedFilters))
        {
            var _selectedFilters = SelectedFilters.UrlExtraction().Split('/').Where(s => !s.Contains("WCA")).ToList();
            retVal.Append(
                (_selectedFilters.FirstOrDefault() == _selectedFilters.LastOrDefault())
                ?
                string.Format(
                    "<span class='cur'>{0}</span>",
                    _selectedFilters.FirstOrDefault())
                :
                string.Format("<a id='{1}' href='{0}'>{1}</a><span class='cur'>{2}</span>",
                Function.makeNewYorkerlink("Produkte",
                SelectedGender.ToString(),
                _selectedFilters.FirstOrDefault(),
                SelectedFilters.UrlExtraction().Split('/').FirstOrDefault(s => s.Contains("WCA"))),
               _selectedFilters.FirstOrDefault(),
               _selectedFilters.LastOrDefault()
                ));
        }
        if (retVal.Length <= 0)
            retVal.Append("NewYorker Prudukten");

        return retVal.ToString();
    }
}