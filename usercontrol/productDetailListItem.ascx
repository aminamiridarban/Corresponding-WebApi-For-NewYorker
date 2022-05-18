<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productDetailListItem.ascx.cs" Inherits="Views_productDetailListItem" %>


<article class="proCol2 ">
    <ul class="pinfoLst">
        <li>
            <ul class="detList">
                <li>
                    <span>Brand : </span>
                    <a href="<%=Function.makeProductsAjaxlink("produkte",_brand,_gender) %>"><%=_brand.SecureText() %></a>
                </li>
                <li>
                    <span>Category : </span>
                    <a href="<%=Function.makeProductsAjaxlink("produkte",_group,_gender) %>"><%=_group.SecureText() %></a>
                </li>
            </ul>
        </li>
        <%if (!string.IsNullOrEmpty(Sizes))%>
        <%{  %>
        <li>
            <ul class="detList" id="sizes">
                <%=Sizes %>
            </ul>
        </li>
        <%} %>
        <%if (!string.IsNullOrEmpty(Color))%>
        <%{  %>
        <li>
            <ul class="detList" id="colors">
                <%=Color %>
            </ul>
        </li>
        <%} %>
        <li>
            <ul class="detList">
                <li><span class="okPrice"><span class="okPrice1">Preis : <%=Function.makeprice(_variant.current_price)%></span>  €</span>
                
                    <ul class="bigFont">
                        <li data-id="<%=ProductId %>" data-title="<%=_group.ValidPersian() %>" data-url="<%=Function.makelink("product",_productId,_group) %>"
                            data-image="<%=Function.InsNoPic(string.Format("https://api.newyorker.de/csp/images/image/public/{0}?res=high&frame=1_1", _variant.images[0].key) ,"/images/no-pic.png") %>">
                            <div class="basketBtnPane">
                                <a class="ic icAddBasket" href="#">Fügen Sie diesen Artikel meinem Warenkorb hinzu</a>
                                <div class="sellLabel" style="display: none">
                                    <span style="float:left">Selected Successfully..:)</span>
                                    <div class="cAdd" data-id="<%=_productId %>" data-title="<%=_group %>" data-url="<%=Function.makelink("product",_productId,_group) %>"
                                        data-image="<%=Function.InsNoPic(string.Format("https://api.newyorker.de/csp/images/image/public/{0}?res=high&frame=1_1", _variant.images[0].key) ,"/images/no-pic.png") %>" style="float: right">
                                        <div class="cAddItem">
                                            <span class="selectedItem">1</span>
                                            <ul style="background: rgba(126, 255, 116, 0.85); z-index: 1000; color: #111; position: relative">
                                                <li class="dellItem">×</li>
                                                <li class='highlighted'>1</li>
                                                <li class='highlighted'>2</li>
                                                <li class='highlighted'>3</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
    </ul>

</article>
<aside class="proCol1 spSell">

    <div class="bPicPane">
        <div class="pBigPic">
            <figure>
                <img id="zoom"
                    src='<%= Function.InsNoPic(string.Format("https://api.newyorker.de/csp/images/image/public/{0}?res=high&frame=1_1", _variant.images[0].key) ,"/images/no-pic.png")%>'
                    alt="<%=_group.SecureText()%>"
                    title="<%=_group.SecureText()%>"
                    data-zoom-image="<%= Function.InsNoPic(string.Format("https://api.newyorker.de/csp/images/image/public/{0}?res=high&frame=1_1", _variant.images[0].key) ,"/images/no-pic.png")%>" />
            </figure>
        </div>
        <ul class="ulIcon">
            <li>
                <asp:LinkButton ID="lbtnFavorites" runat="server" CssClass="tooltip addFav" ToolTip="Zu den Favoriten hinzufügen" CausesValidation="false" ClientIDMode="Static" OnClientClick="return false">
                    <span class="tooltiptext"><%=lbtnFavorites.ToolTip %></span>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
</aside>

