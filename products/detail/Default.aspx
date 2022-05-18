<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="products_detail_Default" %>

<%@ Register Src="~/usercontrol/productDetailListItem.ascx" TagPrefix="uc1" TagName="productDetailListItem" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    <%=Product.maintenance_group.SecureText()%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <meta name="description" content='<%=Function.StrCUT(Product.maintenance_group.SecureText(),250)%>' />
    <meta name="keywords" content='<%=Product.brand.SecureText()%>' />
    <meta property="og:title" content="▷ <%=Product.brand.SecureText()%>" />
    <link href="<%=SiteUtility.SiteRoot +"wwwroot/css/inside.css?cash="%>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
    <nav class="breadcrumb">
        <a id="home" href="<%=SiteUtility.SiteRoot %>">NewYorker Gespräch</a>
        <a id="produkte" href='<%=string.Format("{0}Produkte/View",SiteUtility.SiteRoot) %>'>Produkte Seite</a>
        <%=BreadCrumb%>
    </nav>
    <%--/////////////////////////////////////////// Product Specifications /////////////////////////////////////////--%>

    <section class="proCol3">
        <h2 class="hsTitle"><%=Product.maintenance_group.SecureText() %>
            <span><%=Product.brand.ToString() %></span>
        </h2>
        <table class="tblStyle">
            <tbody>
                <tr>
                    <td class="tdItem">Marke</td>
                    <td class="tdInfo"><%=Product.brand %></td>
                </tr>
                <tr>
                    <td class="tdItem">Beschreibungen</td>
                    <td class="tdInfo"><%=Product.descriptions.Where(d=> d.language == _currentLanguage).FirstOrDefault().description.SecureText() %></td>
                </tr>
                <tr>
                    <td class="tdItem">Kundengruppe</td>
                    <td class="tdInfo"><%=Product.customer_group.SecureText() %></td>
                </tr>
                <tr>
                    <td class="tdItem">Kategorie</td>
                    <td class="tdInfo"><%=Product.web_category.SecureText() %></td>
                </tr>
                <tr>
                    <td class="tdItem">Globaler Kodex </td>
                    <td class="tdInfo"><%=Product.global_item_id.SecureText() %></td>
                </tr>
            </tbody>
        </table>
    </section>

    <%=BindProductsList %>

    <div class="clear"></div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="Server">


   
</asp:Content>

