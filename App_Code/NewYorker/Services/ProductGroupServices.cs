using System;
using System.Linq;



/// <summary>
/// This is a partial class associated ProductGroup & Product Controller in two sepretade files
/// </summary>
public partial class Services : BaseServices
{
    public Models.ResponsedModel ProductGroup
    {
        get
        {
            if (_productGroup == null)
            {
                var response = new CorrespondingWebApi().QueryList(_gender, _filtersString, _pageNo, _perPageItems);
                if (response != null)
                    _productGroup = response;
            }
            return _productGroup;
        }
    }
    public Services()
    {
    }
    public Services(string lang) : base(language: lang)
    {
        _productGroup = ProductGroup;
    }
    public Services(AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, SerializableDictionary<string, string> filtersString)
        : base(gender, pageNo, perPageItems, filtersString)
    {
        _productGroup = ProductGroup;
    }
    public Services(AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, string categories)
       : base(gender, pageNo, perPageItems, categories)
    {
        _productGroup = ProductGroup;
    }
    public Services(string lang, AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, string categories)
    : base(lang, gender, pageNo, perPageItems, categories)
    {
        _productGroup = ProductGroup;
    }
    public Services(string lan, AllEnums.UserInfo.Gender gender) : base(language: lan, gender: gender)
    {
    }
    public Services(string lang, AllEnums.UserInfo.Gender gender, int pageNo, int perPageItems, SerializableDictionary<string, string> filtersString)
        : base(lang, gender, pageNo, perPageItems, filtersString)
    {
        _productGroup = ProductGroup;
    }
    public Models.ResponsedModel GetProductsByCategoryName(string searchPhrase)
    {
        _filtersString.Remove("web_category");
        _filtersString.Add("web_category", searchPhrase);
        _productGroup = ProductGroup;
        _productGroup.items = _productGroup.items.Where((p) =>
        string.Equals(p.web_category, searchPhrase,
        StringComparison.OrdinalIgnoreCase) || string.Equals(p.maintenance_group, searchPhrase,
        StringComparison.OrdinalIgnoreCase)).Distinct().ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsForCategoryNames(string categoryNames, AllEnums.UserInfo.Gender gender)
    {
        _gender = gender;
        _filtersString.Remove("web_category");
        _filtersString.Add("web_category", string.Join(",", categoryNames));
        _productGroup = ProductGroup;
        _productGroup.items = _productGroup.items.Where((p) =>
        categoryNames.Contains(p.web_category)).Distinct().ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsByGender(AllEnums.UserInfo.Gender gender)
    {
        string genderString = AllEnums.UserInfo.GenderEnglishName(gender);
        _gender = gender;
        _productGroup = ProductGroup;
        _productGroup.items = _productGroup.items.Where((p) =>
        string.Equals(p.customer_group, genderString,
        StringComparison.OrdinalIgnoreCase)).Distinct().ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsByCategoryId(string categoryId)
    {
        _filtersString.Remove("web_category_id");
        _filtersString.Add("web_category_id", categoryId);
        _productGroup = ProductGroup;
        _productGroup.items = _productGroup.items.Where((p) =>
        string.Equals(p.web_category_id, categoryId,
        StringComparison.OrdinalIgnoreCase)).Distinct().ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsByBrand(string brand)
    {
        _filtersString.Remove("brand");
        _filtersString.Add("brand", brand);
        _productGroup = ProductGroup;
        _productGroup.items = _productGroup.items.Where((p) =>
        string.Equals(p.brand, brand,
        StringComparison.OrdinalIgnoreCase)).ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsByColor(string color)
    {
        _productGroup.items = ProductGroup.items.Where((p) =>
        p.variants.Any(v => v.color_group == color || v.color_name == color)).ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsBySize(string size)
    {
        _productGroup.items = ProductGroup.items.Where((p) =>
        p.variants.Any(v => v.sizes.Any(s => s.size_name == size || s.size_value == size))).ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsByPriceRange(double minPrice, double maxPrice)
    {
        _productGroup.items = ProductGroup.items.Where((p) =>
            p.variants.Any(v =>
            (v.original_price >= minPrice && v.original_price <= maxPrice)
            ||
            (v.current_price >= minPrice && v.current_price <= maxPrice))
            ).ToList();
        return _productGroup;
    }
    public Models.ResponsedModel GetProductsByPrice(double price)
    {
        _productGroup.items = ProductGroup.items.Where((p) =>
        p.variants.Any(v => v.original_price == price || v.current_price == price)).ToList();
        return _productGroup;
    }

    public Models.CorrespondingResponsedModel GetProductsByFilterString(string filtersString)
    {
        System.Text.StringBuilder retVal = new System.Text.StringBuilder();

        string ajaxFilterCommands = filtersString.Contains("#!/")
            ?
            filtersString.Substring(filtersString.IndexOf("#!/") + 3)
            :
            filtersString.Contains("#")
            ?
            filtersString.Substring(filtersString.IndexOf("#") + 1)
            :
            string.Empty;


        Uri uriAddress = new Uri(filtersString.Contains("#!/")
            ?
            filtersString.Substring(0, filtersString.IndexOf("#!/"))
            :
            filtersString.Contains("#")
            ?
            filtersString.Substring(0, filtersString.IndexOf("#"))
            :
            filtersString);

        string SelectedFilters = uriAddress.ToString();
        System.Text.StringBuilder query = new System.Text.StringBuilder();

        string filterQueryStringCategory = !string.IsNullOrEmpty(SelectedFilters) ? SelectedFilters.Replace("_", " ,") : string.Empty;
        var matchesFilters = System.Text.RegularExpressions.Regex.Matches(filterQueryStringCategory, @"\b[WCA]\w+");
        if (!string.IsNullOrEmpty(filterQueryStringCategory) || _gender != AllEnums.UserInfo.Gender.UnKnown)
        {
            if (matchesFilters.Count > 0)
                foreach (System.Text.RegularExpressions.Match match in matchesFilters)
                    query.Append(string.Format("{0},", match));

            else if (matchesFilters.Count == 0)
            {
                if (_gender == AllEnums.UserInfo.Gender.Herren)
                    if (MainStaticCategories.HerrenCategoriesDictionary.Any(p => p.Key.Equals(filterQueryStringCategory, System.StringComparison.OrdinalIgnoreCase)))
                        query.Append(string.Format("{0}, {1}",
                            MainStaticCategories.HerrenCategoriesDictionary.FirstOrDefault(p => p.Key.Equals(filterQueryStringCategory, System.StringComparison.OrdinalIgnoreCase)).Key,
                            MainStaticCategories.HerrenCategoriesDictionary.FirstOrDefault(p => p.Key.Equals(filterQueryStringCategory, System.StringComparison.OrdinalIgnoreCase)).Value
                            ));

                if (_gender == AllEnums.UserInfo.Gender.Damen)
                    if (MainStaticCategories.DamenCategoriesDictionary.Any(p => p.Key.Equals(filterQueryStringCategory, System.StringComparison.OrdinalIgnoreCase)))
                        query.Append(string.Format("{0}, {1}",
                            MainStaticCategories.DamenCategoriesDictionary.FirstOrDefault(p => p.Key.Equals(filterQueryStringCategory, System.StringComparison.OrdinalIgnoreCase)).Key,
                            MainStaticCategories.DamenCategoriesDictionary.FirstOrDefault(p => p.Key.Equals(filterQueryStringCategory, System.StringComparison.OrdinalIgnoreCase)).Value
                         ));

            }
            Services service = new Services(_language, _gender, 0, 0, query.ToString());

        }


        int pageIndex = int.TryParse(_pageNo.ToString(), out pageIndex) ? int.Parse(_pageNo.ToString()) : 1;
        int pageSize = int.TryParse(_perPageItems.ToString(), out pageIndex) ? int.Parse(_perPageItems.ToString()) : 9;

        string productGroupId = string.Empty;

        int sortBy = 0;
        int sortDirection = 0;

        double minPrice = 0;
        double maxPrice = 0;

        int commingsoon = 0;
        int sale = 0;
        int isnew = 0;
        int disscount = 0;

        System.Text.StringBuilder pager = new System.Text.StringBuilder();

        string keyword = string.Empty;

        string strFilters = "";









        if (!string.IsNullOrEmpty(ajaxFilterCommands))
        {
            //Get Page Index
            if (ajaxFilterCommands.Contains("page-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("page-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                pageIndex = int.Parse(subStr.Replace("page-", ""));
            }

            #region All(Products/ParentGroups)
            //Get ParentGroup

            if (ajaxFilterCommands.Contains("allproducts"))
            {
                productGroupId = string.Empty;
            }

            #endregion
            #region StringFilters
            //Get Sort By
            if (ajaxFilterCommands.Contains("sortby-"))
            {
                string subStr = string.Empty;
                subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("sortby-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                sortBy = int.Parse(subStr.Replace("sortby-", ""));
            }
            //Get Sort Direction
            if (ajaxFilterCommands.Contains("sortdirection-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("sortdirection-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                sortDirection = int.Parse(subStr.Replace("sortdirection-", ""));
            }
            //Get Min Price
            if (ajaxFilterCommands.Contains("minprice-"))
            {
                string subStr = string.Empty;
                subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("minprice-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                minPrice = double.Parse(subStr.Replace("minprice-", ""));
            }
            //Get Max Price
            if (ajaxFilterCommands.Contains("maxprice-"))
            {
                string subStr = string.Empty;
                subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("maxprice-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                maxPrice = double.Parse(subStr.Replace("maxprice-", ""));
            }
            if (ajaxFilterCommands.Contains("keyword-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("keyword-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                keyword = subStr.Replace("keyword-", "");
            }
            if (ajaxFilterCommands.Contains("comingsoon-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("comingsoon-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                commingsoon = int.Parse(subStr.Replace("comingsoon-", ""));
            }
            if (ajaxFilterCommands.Contains("disscount-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("disscount-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                disscount = int.Parse(subStr.Replace("disscount-", ""));
            }
            if (ajaxFilterCommands.Contains("isnew-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("isnew-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                isnew = int.Parse(subStr.Replace("isnew-", ""));
            }
            if (ajaxFilterCommands.Contains("sale-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("sale-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                sale = int.Parse(subStr.Replace("sale-", ""));
            }
            if (ajaxFilterCommands.Contains("pagesize-"))
            {
                string subStr = ajaxFilterCommands.Substring(ajaxFilterCommands.IndexOf("pagesize-"));
                subStr = subStr.Substring(0, subStr.IndexOf('/'));
                ajaxFilterCommands = ajaxFilterCommands.Replace(subStr + "/", "");
                pageSize = int.Parse(!string.IsNullOrEmpty(subStr.Replace("pagesize-", "")) ? subStr.Replace("pagesize-", "") : pageSize.ToString());
            }
            #endregion





        }

        //Fetching Products from Api by base query string filters from NewYorker
        _gender = uriAddress.Segments.Any(a => a.Contains(AllEnums.UserInfo.Gender.Herren.ToString())) ? AllEnums.UserInfo.Gender.Herren : uriAddress.Segments.Any(b => b.Contains(AllEnums.UserInfo.Gender.Damen.ToString())) ? AllEnums.UserInfo.Gender.Damen : AllEnums.UserInfo.Gender.UnKnown;
        _pageNo = pageIndex;
        _perPageItems = pageSize;

        System.Collections.Generic.List<Models.Item> productsFilteringQuery = ProductGroup.items.ToList();


        #region Applying filters on products base on StringFilters
        //Product Filter by Group Id
        if (!string.IsNullOrEmpty(productGroupId))
            _productGroup.items = ProductGroup.items.Where(p => p.web_category_id == productGroupId).ToList();
        //Search Groups maintenance_group & web_category By Keyword
        if (keyword != "")
            productsFilteringQuery = productsFilteringQuery.Where(pg => pg.maintenance_group.Contains(keyword) | pg.web_category.Contains(keyword) | pg.descriptions[0].description.Contains(keyword)).ToList();
        //IsSale
        if (sale > 0)
            productsFilteringQuery = productsFilteringQuery.Where(p => p.variants[0].sale).ToList();
        //Is Coming soon
        if (commingsoon > 0)
            productsFilteringQuery = productsFilteringQuery.Where(p => p.variants[0].coming_soon).ToList();

        //Min&Max Price Filter
        productsFilteringQuery = productsFilteringQuery.Select(p => p).ToList();
        if (maxPrice > 0)
            productsFilteringQuery = productsFilteringQuery.Where(p => p.variants[0].original_price <= maxPrice && p.variants[0].original_price >= minPrice).ToList();
        #endregion


        //Product Sort List
        #region Product Sort List
        switch (sortBy)
        {
            case 0: //PublishDate
                if (sortDirection == 0)//Ascending
                {
                    productsFilteringQuery = productsFilteringQuery.OrderBy(p => p.variants[0].publish_date).ToList();
                }
                else if (sortDirection == 1)//Descending
                {
                    productsFilteringQuery = productsFilteringQuery.OrderByDescending(p => p.variants[0].publish_date).ToList();
                }
                break;
            case 1: //Is New
                if (sortDirection == 0)//Ascending
                {
                    productsFilteringQuery = productsFilteringQuery.OrderBy(p => p.variants[0].new_in).ToList();
                }
                else if (sortDirection == 1)//Descending
                {
                    productsFilteringQuery = productsFilteringQuery.OrderByDescending(p => p.variants[0].new_in).ToList();
                }
                break;

            case 4: //Price
                if (sortDirection == 0)//Ascending
                {
                    productsFilteringQuery = productsFilteringQuery.OrderByDescending(p => p.variants[0].current_price).ToList();
                }
                else if (sortDirection == 1)//Descending
                {
                    productsFilteringQuery = productsFilteringQuery.OrderBy(p => p.variants[0].current_price).ToList();
                }
                break;
            default:
                break;
        }
        #endregion




        #region ProductList


        int totalProductsCount = productsFilteringQuery.Count();
        int productStartIndex = Math.Abs(pageSize * (pageIndex - 1));
        productsFilteringQuery = productsFilteringQuery.Skip(productStartIndex).Take(pageSize).ToList();


        //Create ParentGroup Pager
        #region Create ParentGroup Pager

        if (filtersString.Contains("page-"))
        {
            string subStr = filtersString.Substring(filtersString.IndexOf("page-"));
            filtersString = filtersString.Replace(subStr.Trim('/') + "/", "");
        }

        int totalProductPageCount = ((totalProductsCount % pageSize) > 0 ? (System.Convert.ToInt32(decimal.Round(totalProductsCount / pageSize)) + 1) : System.Convert.ToInt32(decimal.Round(totalProductsCount / pageSize)));
        if (pageIndex <= totalProductPageCount)
        {
            pager.Append("<div class='aadAjaxLoaderPaging'>");
            pager.Append("<div class='aadAjaxLoaderPanel'>");

            if (totalProductPageCount <= 5)
            {
                for (int i = 0; i < totalProductPageCount; i++)
                {
                    pager.Append("<a class='page-" + (i + 1).ToString() + (pageIndex == (i + 1) ? " current" : "") + "' title='Page " + (i + 1).ToString() + "' href='" + (pageIndex == (i + 1) ? "javascript:void(0);" : "/" + filtersString + "page-" + (i + 1).ToString()) + "' " + (pageIndex == (i + 1) ? "" : "onclick='javascript:return JumpToPage(" + (i + 1).ToString() + ")'") + ">" + (i + 1).ToString() + "</a>");
                }
            }
            else if (totalProductPageCount > 5)
            {
                if (pageIndex <= 5)
                {
                    int i = 0;
                    for (; i < 5; i++)
                        pager.Append("<a class='page-" + (i + 1).ToString() + (pageIndex == (i + 1) ? " current" : "") + "' href='" + (pageIndex == (i + 1) ? "javascript:void(0);" : filtersString + "page-" + (i + 1).ToString()) + "' " + (pageIndex == (i + 1) ? "" : "onclick='javascript:return JumpToPage(" + (i + 1).ToString() + ")'") + ">" + (i + 1).ToString() + "</a>");

                    pager.Append("<a class='next' href='" + filtersString + "page-" + (pageIndex + 1).ToString() + "' title='Next Page' onclick='javascript:return JumpToPage(" + (pageIndex + 1).ToString() + ")'> ›</a>");
                    pager.Append("<a class='last' href='" + filtersString + "page-" + totalProductPageCount.ToString() + "' title='Last Page' onclick='javascript:return JumpToPage(" + totalProductPageCount.ToString() + ")'> »</a>");
                }
                else if (pageIndex > 4 && pageIndex <= (totalProductPageCount - 4))
                {
                    pager.Append("<a class='first' href='" + filtersString + "page-1' title='First Page' onclick='javascript:return JumpToPage(1)'> «</a>");
                    pager.Append("<a class='prev' href='" + filtersString + "page-" + (pageIndex - 1).ToString() + "' title='Prev Page' onclick='javascript:return JumpToPage(" + (pageIndex - 1).ToString() + ")'> ‹</a>");

                    pager.Append("<a title='Page " + (pageIndex - 2).ToString() + "' href='" + filtersString + "page-" + (pageIndex - 2).ToString() + "' onclick='javascript:return JumpToPage(" + (pageIndex - 2).ToString() + ")'>" + (pageIndex - 2).ToString() + "</a>");
                    pager.Append("<a title='Page " + (pageIndex - 1).ToString() + "' href='" + filtersString + "page-" + (pageIndex - 1).ToString() + "' onclick='javascript:return JumpToPage(" + (pageIndex - 1).ToString() + ")'>" + (pageIndex - 1).ToString() + "</a>");
                    pager.Append("<a class='current' title='Page " + (pageIndex).ToString() + "' href='javascript:void(0);'>" + pageIndex.ToString() + "</a>");
                    pager.Append("<a title='Page " + (pageIndex + 1).ToString() + "' href='" + filtersString + "page-" + (pageIndex + 1).ToString() + "' onclick='javascript:return JumpToPage(" + (pageIndex + 1).ToString() + ")'>" + (pageIndex + 1).ToString() + "</a>");
                    pager.Append("<a title='Page " + (pageIndex + 2).ToString() + "' href='" + filtersString + "page-" + (pageIndex + 2).ToString() + "' onclick='javascript:return JumpToPage(" + (pageIndex + 2).ToString() + ")'>" + (pageIndex + 2).ToString() + "</a>");

                    pager.Append("<a class='next' href='" + filtersString + "page-" + (pageIndex + 1).ToString() + "' title='Next Page' onclick='javascript:return JumpToPage(" + (pageIndex + 1).ToString() + ")'> ›</a>");
                    pager.Append("<a class='last' href='" + filtersString + "page-" + totalProductPageCount.ToString() + "' title='Last Page' onclick='javascript:return JumpToPage(" + totalProductPageCount.ToString() + ")'> »</a>");
                }
                else if (pageIndex > (totalProductPageCount - 4))
                {
                    pager.Append("<a class='first' href='" + filtersString + "page-1' title='First Page' onclick='javascript:return JumpToPage(1)'> «</a>");
                    pager.Append("<a class='prev' href='" + filtersString + "page-" + (pageIndex - 1).ToString() + "' title='Prev Page' onclick='javascript:return JumpToPage(" + (pageIndex - 1).ToString() + ")'> ‹</a>");

                    for (int i = totalProductPageCount - 4; i <= totalProductPageCount; i++)
                        pager.Append("<a class='" + (pageIndex == i ? "current" : "") + "' title='Page " + (i).ToString() + "' href='" + (pageIndex == i ? "javascript:void(0);" : filtersString + "page-" + i.ToString()) + "' " + (pageIndex == i ? "" : "onclick='javascript:return JumpToPage(" + (i).ToString() + ")'") + ">" + i.ToString() + "</a>");

                }
            }
            pager.Append("</div>");
            if (totalProductPageCount == pageIndex)
            {
                pager.Append("<div class='aadAjaxLoaderInfo'>Produkte zeigen <span class='aadAjaxLoaderColors'>" + (productStartIndex + 1).ToString() + "</span> zu <span class='aadAjaxLoaderColors'>" + totalProductsCount.ToString() + "</span> von <span class='aadAjaxLoaderColors'>" + totalProductsCount.ToString() + "</span> Das Produkten  </div></div>");
            }
            else
            {
                pager.Append("<div class='aadAjaxLoaderInfo'>Produkte zeigen <span class='aadAjaxLoaderColors'>" + (productStartIndex + 1).ToString() + "</span> zu <span class='aadAjaxLoaderColors'>" + (totalProductsCount + pageSize).ToString() + "</span> von <span class='aadAjaxLoaderColors'>" + totalProductsCount.ToString() + "</span> Das Produkten  </div></div>");
            }
        }
        #endregion


        //Create GroupItems In Page
        #region Create GroupItems In Page
        int zIndex = 100;
        var productsFilteringQueryList = productsFilteringQuery.ToList();
        foreach (var item in productsFilteringQueryList)
        {
            retVal.Append(productsFilteringQueryList.GetRandomItem() == item ? "<li class='randomElement'>" : "<li>");
            retVal.Append("<div class='proItem'>");
            retVal.Append("<div class='giftPane'>");
            if (item.variants[0].sale)
                retVal.Append("<span class='giftLBL blink'>Sale</span>");
            retVal.Append("</div>");//Div class 'giftPane' close
            retVal.Append("<a class='prpLink' href='" + Function.makeNewYorkerlink("produkte", item.maintenance_group.SecureText(), item.web_category, item.id) + "'>");
            retVal.Append("<figure class='prPic'>");
            retVal.Append("<img src='" + string.Format("https://api.newyorker.de/csp/images/image/public/{0}?res=mid&frame=2_3", item.variants[0].images[0].key) + "' alt='" + item.maintenance_group + "'/>");
            retVal.Append("<figcaption class='prName'>");
            retVal.Append(item.maintenance_group);
            retVal.Append("</figcaption>");
            retVal.Append("</figure>");
            retVal.Append("</a>");
            retVal.Append("<br/>");
            retVal.Append("<a class='prpLink' href='" + Function.makeNewYorkerlink("produkte", item.maintenance_group.SecureText().ValidPersian(), item.web_category, item.id) + "'>");
            retVal.Append(Function.StrCUT(item.maintenance_group, 85));
            retVal.Append("</a>");
            retVal.Append("<div class='prPrice'>");
            retVal.Append("<span class='price-amount'>" + Function.CorrectNumbersEn(item.variants[0].current_price).Replace("/", ".") + "&nbspEur</span>");

            retVal.Append("<div class='cAdd' data-id='" + item.id + "' data-url='" + Function.makelink("product", item.id, item.web_category.SecureText().ValidPersian()) + "' data-title='" + item.web_category_id + "' data-image='" + SiteUtility.SiteRootPictures + item.variants[0].images[0].key + "'>");
            retVal.Append("<div class='cAddItem' style='display:none'>");
            retVal.Append("<span class='selectedItem'>1</span>");
            retVal.Append("<ul>");
            retVal.Append("<li class='dellItem'>×</li>");
            retVal.Append("<li class='highlighted'>1</li>");
            retVal.Append("<li>2</li>");
            retVal.Append("<li>3</li>");
            retVal.Append("<li>4</li>");
            retVal.Append("<li>5</li>");
            retVal.Append("<li>6</li>");
            retVal.Append("<li>7</li>");
            retVal.Append("<li>8</li>");
            retVal.Append("</ul>");
            retVal.Append("</div>");//Div class 'cAddItem' close
            retVal.Append("<a class='addBasket' href='#'>+</a>");
            retVal.Append("</div>");
            retVal.Append("</div>");
            retVal.Append("<div class='sellLabel'></div>");
            retVal.Append("</div>");//Div class 'proItem' close
            retVal.Append("</li>");
            #endregion
            zIndex = zIndex - 1;
        }

        #endregion ProductList
        return new Models.CorrespondingResponsedModel()
        {
            filteredProductsHtmlString = retVal.ToString(),
            pager = pager.ToString(),
            filterString = strFilters,
            totalCount = productsFilteringQueryList.Count,
            //I'm sending a random item from server select but it's possible to get it random from 'filteredProductsHtmlString' it in my JavaScript by class attribute 'randomElement'
            //not needed, just to determine the ways job could be done
            specialRandomOfferItem = productsFilteringQueryList.Count > 0 ? productsFilteringQueryList.GetRandomItem() : new Models.Item()

        };
    }





}