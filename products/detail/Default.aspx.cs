using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class products_detail_Default : BasePage
{
    protected string _currentLanguage;
    AllEnums.UserInfo.Gender _gender = AllEnums.UserInfo.Gender.UnKnown;
    public override void Dispose()
    {
        base.Dispose();

    }

    protected string SelectedId
    {
        get
        {
            string retVal = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                return Request.QueryString["id"].ToString();
            }
            return retVal;
        }
    }
    protected string GroupTitle
    {
        get
        {
            string retVal = null;
            if (Request.QueryString["groupTitle"] != null)
            {
                return Request.QueryString["groupTitle"];
            }
            return retVal;
        }
    }
    protected string Title
    {
        get
        {
            string retVal = null;
            if (Request.QueryString["title"] != null)
            {
                return Request.QueryString["title"];
            }
            return retVal;
        }
    }
    protected string BreadCrumb
    {
        get
        {
            System.Text.StringBuilder retVal = new System.Text.StringBuilder();
            var availableGenders = AllEnums.EnumConverter.ToNameArray<AllEnums.UserInfo.Gender>().ToList();
            

            //BreadCrumb Creates For Gender Base On Product Customer Group
            if (!Enum.TryParse<AllEnums.UserInfo.Gender>(Product.customer_group, out _gender))
                foreach (string genderDeutchString in availableGenders)
                    if (string.Equals(Product.customer_group, genderDeutchString, StringComparison.OrdinalIgnoreCase))
                        _gender = AllEnums.UserInfo.GenderDetuchName(Product.customer_group);
            retVal.Append(string.Format("<a id='{0}' href='{1}'>{0}</a>",
                _gender
                ,
                Function.makeNewYorkerlink("Produkte", _gender)
                ));


            if (!string.IsNullOrEmpty(GroupTitle) && !string.IsNullOrEmpty(Title))
            {
                retVal.Append(
                    string.Format("<a id='{1}' href='{0}'>{1}</a><span class='cur'>{2}</span>",
                    Function.makeNewYorkerlink("Produkte",
                    GroupTitle.Replace("_", " "), Title.Replace("_", " "), Product.id), GroupTitle.Replace("_", " "), Product.brand));
            }
            if (retVal.Length <= 0)
                retVal.Append("NewYorker Prudukten");

            return retVal.ToString();
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        _currentLanguage = Session["lang"] != null ? Session["lang"].ToString() : "DE";
        if (!IsPostBack)
            if (string.IsNullOrEmpty(SelectedId))
                Response.Redirect("/error/default.aspx");//404 notfound

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected Models.Item _Product = null;
    protected Models.Item Product
    {
        get
        {
            Services service = new Services(_currentLanguage, SelectedId.ValidProductUrl());
            if (_Product == null)
                if (SelectedId.Length > 0)
                    _Product = service.Product ?? new Models.Item();

            return _Product;
        }
    }



    protected string BindProductsList
    {
        get
        {
            StringBuilder retVal = new StringBuilder();
            foreach (Models.Variant variant in Product.variants)
            {
                StringBuilder b = new StringBuilder();
                HtmlTextWriter h = new HtmlTextWriter(new System.IO.StringWriter(b));
                Control control = Page.LoadControl("~/usercontrol/productDetailListItem.ascx");
                Views_productDetailListItem productDetailListItem = (Views_productDetailListItem)control;
                productDetailListItem.Variant = variant;
                productDetailListItem.Brand = Product.brand;
                productDetailListItem.ProductId = Product.id;
                productDetailListItem.Group = Product.maintenance_group;

                control.RenderControl(h);

                retVal.Append(b.ToString());

            }
            return retVal.ToString();
        }

    }


}