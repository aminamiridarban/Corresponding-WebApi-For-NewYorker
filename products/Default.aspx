<%@ Page Language="C#" MasterPageFile="~/product.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Sample_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Produkten
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <meta name="description" content="This is the all products page in Newyorker Interview" />
    <meta name="keywords" content="NewYorker, InterView, Products Page" />
    <link href="<%=SiteUtility.SiteRoot + "wwwroot/css/aadAjaxLoader.css" %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Content" runat="Server">
    <nav class="breadcrumb">
        <a id="home" href="<%=SiteUtility.SiteRoot %>">NewYorker Gespräch</a>
        <a id="produkte" href='<%=string.Format("{0}Produkte/View",SiteUtility.SiteRoot) %>'>Produkte Seite</a>
        <%=GetBreadCrumb()%>
    </nav>
    <aside class="pCol1">
        <div class="filterBox">
            <h3 class="fbTit"><%=GetPageTitle()%>
            </h3>
        </div>
        <div class="filterBox">
            <h3 class="fbTit">Suche in Produkten 
            </h3>
            <div class="smSearch">
                <input type="text" placeholder=" Schreiben Sie den Namen des Produkts oder der Marke…" class="txtSerach" id="txtSearchProductAjax" autocomplete="off">
                <input type="button" value="&#xf002;" class="smBtn" id="inpSearchProductsAjax">
                <ul class="smSearchResults" id="lstSearchResultList">
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlProductFilters" runat="server">
            <div class="filterBox">
                <h3 class="fbTit toggleTit">Produkte Filter</h3>
                <div class="overflowList">
                    <div class="t-center">Price Range</div>
                    <div class="pricePick">
                        <div id="prSlider">
                            <div id="amount">
                                <span class="maxprice"></span><span class="minprice"></span>
                            </div>
                        </div>
                    </div>
                    <div class="filterBox">
                        <ul class="aadAjaxLoaderFilter aadAjaxLoader1Filter TMComingSoonFilter">
                            <li>
                                <a class="swipInp">
                                    <span class="chkName">Coming Soon</span>
                                    <input id="ChkComingSoon" type="checkbox" value="comingsoon-1">
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="filterBox">
                        <ul class="aadAjaxLoaderFilter aadAjaxLoader1Filter TMIsSaleFilter">
                            <li>
                                <a class="swipInp">
                                    <span class="chkName">Sale</span>
                                    <input id="ChkSale" type="checkbox" value="sale-1">
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="filterBox">
                        <ul class="aadAjaxLoaderFilter aadAjaxLoader1Filter TMIsNewFilter">
                            <li>
                                <a class="swipInp">
                                    <span class="chkName">New Collections</span>
                                    <input id="ChkIsNew" type="checkbox" value="isnew-1">
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="filterBox">
                        <ul class="aadAjaxLoaderFilter aadAjaxLoader1Filter TMDisscountFilter">
                            <li>
                                <a class="swipInp">
                                    <span class="chkName">Disscount</span>
                                    <input id="ChkDisscount" type="checkbox" value="disscount-1">
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="filterBox">
            <h4 class="fbTit"><a href="<%=SiteUtility.SiteRoot + "Produkte/View/#!/allgroups/" %>">
                <%=SelectedGender != AllEnums.UserInfo.Gender.UnKnown ? SelectedGender.ToString() : "Produkte"%>&nbsp;Kategorien</a>
            </h4>
            <ul class="pCategory">
                <%=GetProductsCategorizedByGender() %>
            </ul>
            <div class="ShowAll"><span>Alle ansehen..!</span></div>
        </div>
    </aside>
    <div class="pCol2">
        <div class="aadAjaxLoaderPane">
            <div class="aadAjaxLoader">
            </div>
            <div class="leftWihte">
                <asp:Panel ID="pnlSortProducts" runat="server" CssClass="controlBox">
                    <div class="sortPane">
                        <ul class="pDdl aadAjaxLoaderSortFilter">
                            <li>
                                <a class="pagesize">
                                    <span class="sortTit">
                                        <input id="hdpageSize" type="hidden" value="pagesize-12">
                                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="ddl tinytxt acBtn t-center page-size" Width="50" Height="31" AutoPostBack="false" ClientIDMode="Static">
                                            <asp:ListItem Value="3"></asp:ListItem>
                                            <asp:ListItem Value="6"></asp:ListItem>
                                            <asp:ListItem Value="9" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="12"></asp:ListItem>
                                            <asp:ListItem Value="15"></asp:ListItem>
                                            <asp:ListItem Value="30"></asp:ListItem>
                                            <asp:ListItem Value="60"></asp:ListItem>
                                            <asp:ListItem Value="99"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </a>
                            </li>

                            <li>
                                <a class="mostpopular">
                                    <input id="rdbSortPublishDate" type="checkbox" value="sortby-0">
                                    <span class="chkName">Angekündigtes Datum</span>
                                </a>
                            </li>
                            <li>
                                <a class="newest">
                                    <input id="rdbSortNewest" type="checkbox" value="sortby-1">
                                    <span class="chkName">Neueste</span>
                                </a>
                            </li>
                            <li>
                                <a class="cheapest">
                                    <input id="rdbSortCheapest" type="checkbox" value="sortby-3">
                                    <span class="chkName">Geringster Preis</span>
                                </a>
                            </li>
                            <li>
                                <a class="mostexpensive">
                                    <input id="rdbSortMostexpensive" type="checkbox" value="sortby-4">
                                    <span class="chkName">Höchster Preis</span>
                                </a>
                            </li>
                        </ul>
                        <ul class="pDdl TMSortConditionFilter">
                            <li>
                                <a class="Herren deascending" href='<%=string.Format("{0}Produkte/Herren/View",SiteUtility.SiteRoot) %>'>
                                    <span class="chkName">Herren</span>
                                </a>
                            </li>
                            <li>
                                <a class="damen ascending" href='<%=string.Format("{0}Produkte/Damen/View",SiteUtility.SiteRoot) %>'>
                                    <span class="chkName">Damen</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </asp:Panel>
                <ul class='productlist'>
                </ul>
                <div id="aadAjaxLoaderPaging">
                </div>

            </div>
        </div>
        <div class="clear margin-b15"></div>
    </div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Script" runat="Server">
    <script>
        $(document).ready(function () {
            $('.overflowList').hide();

            $('.toggleTit').click(function () {
                if ($(this).hasClass('OpenTit')) {
                    $(this).removeClass('OpenTit');
                    $(this).next('.overflowList').slideUp();
                } else {
                    $(this).addClass('OpenTit');
                    $(this).next('.overflowList').slideDown();
                }

            })

            $('.ShowAll').click(function () {
                if ($(this).hasClass('closeAll')) {
                    $(this).removeClass('closeAll').children().text('Alle Kategorien anzeigen..!');
                    $(this).prev('.pCategory').addClass('MaxHeight');
                } else {
                    $(this).addClass('closeAll').children().text('Close');
                    $(this).prev('.pCategory').removeClass('MaxHeight');
                }
            })

           
            $('.AdvSearch').click(function () {
                $(this).toggleClass('AdvSearch1').next().next('.pCol1').slideToggle();
            });

            $('#txtSearchProductAjax').keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13')
                    $("input[id^='inpSearchProductsAjax']").click();

            });
            $('#txtSearchProductAjax').on('input', function (event) {
                if ($(this).val().length > 0) {
                    $("#lstSearchResultList").show();
                    ProductsLookUp($(this).val(), $("#lstSearchResultList").height());
                }
                else {
                    $('#lstSearchResultList').empty();
                    $("#lstSearchResultList").hide();
                    $("#lstSearchResultList").css("height", "35px");
                }
            });
            $('body').on('click', '.searchResultItem', function () {
                $("#txtSearchProductAjax").val($(this).text());
                $("#lstSearchResultList").hide();
            });

            $("#txtSearchProductAjax").blur(function () {
                $("#lstSearchResultList").delay(1000).fadeOut()
            });
            $("#txtSearchProductAjax").focusin(function () {
                $("#lstSearchResultList").show();
            });
            $("input[id^='inpSearchProductsAjax']").click(function () {
                $("#lstSearchResultList").hide();
            });
        });

    </script>
    <script src='<%=SiteUtility.SiteRoot +  "wwwroot/js/jquery-ui.js"%>'></script>
    <script src='<%=SiteUtility.SiteRoot +  "wwwroot/js/jquery.cookie.min.js"%>'></script>
    <script src='<%=SiteUtility.SiteRoot +  "wwwroot/js/aadAjaxLoader.js?cash="%>'></script>
</asp:Content>

