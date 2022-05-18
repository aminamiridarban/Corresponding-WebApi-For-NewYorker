<%@ Page Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    NewYorker Interview
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
    <meta name="description" content="This is a newyorker interview landing page." />
    <meta name="keywords" content="Newyorker, Interview" />
    <link href="wwwroot/css/landing.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
    <div class="hCol2">
        <div class="landing-page">
            <asp:LinkButton CssClass="cg-select" ID="lbtnDamen" runat="server" PostBackUrl="~/Produkte/Damen/View" Text="Damen" OnClick="lbtnDamen_Click">
                  <div class="cg-select-img" style="background-image: url('https://api.newyorker.de/blob/cms-startpage/d1d51128b99dcfba0af2fbee4cd6fb71.jpg')"></div>
                <div class="cg-select-img is-desktop" style="background-image: url('https://api.newyorker.de/blob/cms-startpage/15cc3c655e87e6a1be43c84edc6d33fe.jpg')"></div>
                <div class="label">Damen</div>
            </asp:LinkButton>
            <asp:LinkButton CssClass="cg-select men" ID="lbtnHerren" runat="server" PostBackUrl="~/Produkte/Herren/View" Text="Herren" OnClick="lbtnHerren_Click">
                  <div class="cg-select-img" style="background-image: url('https://api.newyorker.de/blob/cms-startpage/f81c711aa55483107ff8211489740177.jpg')"></div>
                <div class="cg-select-img is-desktop" style="background-image: url('https://api.newyorker.de/blob/cms-startpage/bcc41547a70c0f42f01ac0a37383f138.jpg')"></div>
                <div class="label">Herren</div>
            </asp:LinkButton>
        </div>
    </div>
    <div class="clear"></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="Server">
    <script src="/js/owl.carousel.js" type="text/javascript"></script>

</asp:Content>

