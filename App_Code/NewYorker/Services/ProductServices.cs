using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


/// <summary>
/// Summary description for NewYorkerApiController
/// </summary>
public partial class Services : BaseServices
{
    private string _selectedId { get; set; }

    protected Models.Item _product;
    public Services(string lang, string selectedId) : base(language: lang)
    {
        _product = null;
        _selectedId = selectedId;
    }
    public Models.Item Product
    {
        get
        {
            if (_product == null)
                if (_selectedId.Length > 0)
                {
                    CorrespondingWebApi webapi = new CorrespondingWebApi();
                    var response = webapi.ProductDetails(_selectedId.ToString(), _language);
                    if (response != null)
                        _product = response;
                }
            return _product;
        }
    }

}