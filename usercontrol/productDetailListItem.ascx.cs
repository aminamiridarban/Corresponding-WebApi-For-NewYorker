using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_productDetailListItem : System.Web.UI.UserControl
{
    protected Models.Variant _variant;
    protected string _group;
    protected string _brand;
    protected string _productId;
    protected AllEnums.UserInfo.Gender _gender;

    [Bindable(true, BindingDirection.TwoWay)]
    [PersistenceMode(PersistenceMode.Attribute)]
    public Models.Variant Variant
    {
        get
        {
            return _variant;
        }
        set
        {
            _variant = value;
        }
    }
    [Bindable(true, BindingDirection.TwoWay)]
    [PersistenceMode(PersistenceMode.Attribute)]
    public string Group
    {
        get
        {
            return _group;
        }
        set
        {
            _group = value;
        }
    }

    [Bindable(true, BindingDirection.TwoWay)]
    [PersistenceMode(PersistenceMode.Attribute)]
    public string Brand
    {
        get
        {
            return _brand;
        }
        set
        {
            _brand = value;
        }
    }
    [Bindable(true, BindingDirection.TwoWay)]
    [PersistenceMode(PersistenceMode.Attribute)]
    public string ProductId
    {
        get
        {
            return _productId;
        }
        set
        {
            _productId = value;
        }
    }
    [Bindable(true, BindingDirection.TwoWay)]
    [PersistenceMode(PersistenceMode.Attribute)]
    public AllEnums.UserInfo.Gender Gender
    {
        get
        {
            return _gender;
        }
        set
        {
            _gender = value;
        }
    }


    public override void Dispose()
    {
        base.Dispose();
    }


    protected string Color
    {
        get
        {
            StringBuilder retVal = new StringBuilder();

            retVal.Append(string.Format("<li><span class='color'><div style='float:left;margin:0px 10px 0px 10px;border-radius:50%;width:20px;height:20px;background-color:rgb({0},{1},{2})'></div>{3} - {4}</span></li>", _variant.red,_variant.green,_variant.blue, _variant.color_group,_variant.pantone_color_name));

            return retVal.ToString();
        }

    }

    protected string Sizes
    {
        get
        {
            StringBuilder retVal = new StringBuilder();

            foreach (Models.Size sizes in _variant.sizes.ToList())
            {
                retVal.Append("<li>" + sizes.size_name + " <span class='size'>" + sizes.size_value + "</span></li>");
            }

            return retVal.ToString();
        }
    }

}