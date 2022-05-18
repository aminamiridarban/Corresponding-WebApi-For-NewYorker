using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using System.ComponentModel;
using System.Web.UI.Design;
using System.Drawing.Design;
using System.Web.UI;
using System.Security.Permissions;

namespace AADPager.WebControls
{
    public class UrlRewriteNumericPagerField : NumericPagerField
    {
        private int _maximumRows;
        private int _startRowIndex;
        private int _totalRowCount;

        protected new string GetQueryStringNavigateUrl(int pageNumber)
        {
            string queryStringField = this.DataPager.QueryStringField;
            StringBuilder builder = new StringBuilder();
            HttpRequest request = this.DataPager.Page.Request;

            string url = GetURL(request.RawUrl.ToLower().ToString());
            if (request.RawUrl.ToLower().Contains("/view/"))
            {
                url = url + "/" + pageNumber.ToString() + "/view/";
            }
            else
            {
                url = url.Replace("/page/","") + "/page/" + pageNumber.ToString();
            }

            return url;
        }

        protected string GetURL(string URL)
        {
            string retVal = string.Empty;
            retVal = URL.ToString().ToLower().Replace(SiteUtility.SiteRoot, "").Replace("default.aspx", "");
            if (retVal.Contains("?"))
            {
                retVal = retVal.Remove(retVal.IndexOf("?"));
            }
            if (retVal.Contains("/view/"))
            {
                retVal = retVal.Remove(retVal.IndexOf("/view/"));
            }
            if (retVal.Contains("/page/"))
            {
                retVal = retVal.Remove(retVal.IndexOf("page/"));
            }

            retVal = retVal.Substring(0, retVal.LastIndexOf("/"));

            return retVal;
        }

        public override void CreateDataPagers(DataPagerFieldItem container, int startRowIndex, int maximumRows, int totalRowCount, int fieldIndex)
        {
            if (string.IsNullOrEmpty(base.DataPager.QueryStringField))
            {
                base.CreateDataPagers(container, startRowIndex, maximumRows, totalRowCount, fieldIndex);
            }
            else
            {
                this._startRowIndex = startRowIndex;
                this._maximumRows = maximumRows;
                this._totalRowCount = totalRowCount;
                this.CreateDataPagersForQueryString(container, fieldIndex);
            }
        }

        private void CreateDataPagersForQueryString(DataPagerFieldItem container, int fieldIndex)
        {
            int num = this._startRowIndex / this._maximumRows;
            bool flag = false;
            if (!base.QueryStringHandled)
            {
                int num2;
                base.QueryStringHandled = true;
                if (int.TryParse(base.QueryStringValue, out num2))
                {
                    num2--;
                    int num3 = (this._totalRowCount - 1) / this._maximumRows;
                    if ((num2 >= 0) && (num2 <= num3))
                    {
                        num = num2;
                        this._startRowIndex = num * this._maximumRows;
                        flag = true;
                    }
                }
            }
            int num4 = (this._startRowIndex / (this.ButtonCount * this._maximumRows)) * this.ButtonCount;
            int num5 = (num4 + this.ButtonCount) - 1;
            int num6 = ((num5 + 1) * this._maximumRows) - 1;
            if (num4 != 0)
            {
                container.Controls.Add(this.CreateNextPrevLink(this.PreviousPageText, num4 - 1, this.PreviousPageImageUrl));
                this.AddNonBreakingSpace(container);
            }
            for (int i = 0; (i < this.ButtonCount) && (this._totalRowCount > ((i + num4) * this._maximumRows)); i++)
            {
                if ((i + num4) == num)
                {
                    Label child = new Label();
                    child.Text = ((i + num4) + 1).ToString(System.Globalization.CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(this.CurrentPageLabelCssClass))
                    {
                        child.CssClass = this.CurrentPageLabelCssClass;
                    }
                    container.Controls.Add(child);
                }
                else
                {
                    container.Controls.Add(this.CreateNumericLink(i + num4));
                }
                this.AddNonBreakingSpace(container);
            }
            if (num6 < (this._totalRowCount - 1))
            {
                this.AddNonBreakingSpace(container);
                container.Controls.Add(this.CreateNextPrevLink(this.NextPageText, num4 + this.ButtonCount, this.NextPageImageUrl));
                this.AddNonBreakingSpace(container);
            }
            if (flag)
            {
                base.DataPager.SetPageProperties(this._startRowIndex, this._maximumRows, true);
            }
        }

        private HyperLink CreateNextPrevLink(string buttonText, int pageIndex, string imageUrl)
        {
            int pageNumber = pageIndex + 1;
            HyperLink link = new HyperLink();
            link.Text = buttonText;
            link.NavigateUrl = this/* AAAARGGHHHH!!! */.GetQueryStringNavigateUrl(pageNumber);
            link.ImageUrl = imageUrl;
            if (!string.IsNullOrEmpty(this.NextPreviousButtonCssClass))
            {
                link.CssClass = this.NextPreviousButtonCssClass;
            }
            return link;
        }

        private HyperLink CreateNumericLink(int pageIndex)
        {
            int pageNumber = pageIndex + 1;
            HyperLink link = new HyperLink();
            link.Text = pageNumber.ToString(System.Globalization.CultureInfo.InvariantCulture);
            link.NavigateUrl = this/* AAAARGGHHHH!!! */.GetQueryStringNavigateUrl(pageNumber);
            if (!string.IsNullOrEmpty(this.NumericButtonCssClass))
            {
                link.CssClass = this.NumericButtonCssClass;
            }
            return link;
        }

        private void AddNonBreakingSpace(DataPagerFieldItem container)
        {
            if (this.RenderNonBreakingSpacesBetweenControls)
            {
                container.Controls.Add(new System.Web.UI.LiteralControl("&nbsp;"));
            }
        }
    }

    [AspNetHostingPermissionAttribute(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermissionAttribute(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class UrlRewriteNextPreviousPagerField : NextPreviousPagerField
    {
        int _startRowIndex;
        int _maximumRows;
        int _totalRowCount;
        int _fieldIndex;

        protected new string GetQueryStringNavigateUrl(int pageNumber)
        {
            string queryStringField = this.DataPager.QueryStringField;
            StringBuilder builder = new StringBuilder();
            HttpRequest request = this.DataPager.Page.Request;
            string url = GetURL(request.RawUrl.ToLower().ToString());
            if (request.RawUrl.ToLower().Contains("/view/"))
            {
                url = url + "/" + pageNumber.ToString() + "/view/";
            }
            else
            {
                url = url + "/default.aspx?page=" + pageNumber.ToString();
            }
            return url;
        }

        protected string GetURL(string URL)
        {
            string retVal = string.Empty;
            retVal = URL.ToString().ToLower().Replace(SiteUtility.SiteRoot, "").Replace("default.aspx", "");
            if (retVal.Contains("?"))
            {
                retVal = retVal.Remove(retVal.IndexOf("?"));
            }
            if (retVal.Contains("/view/"))
            {
                retVal = retVal.Remove(retVal.IndexOf("/view/"));
            }

            retVal = retVal.Substring(0, retVal.LastIndexOf("/"));

            return retVal;
        }

        protected override void CopyProperties(DataPagerField newField)
        {
            base.CopyProperties(newField);

            NextPreviousPagerField field = newField as NextPreviousPagerField;
            if (field == null)
                return;

            field.ButtonCssClass = ButtonCssClass;
            field.ButtonType = ButtonType;
            field.FirstPageImageUrl = FirstPageImageUrl;
            field.FirstPageText = FirstPageText;
            field.LastPageImageUrl = LastPageImageUrl;
            field.LastPageText = LastPageText;
            field.NextPageImageUrl = NextPageImageUrl;
            field.NextPageText = NextPageText;
            field.PreviousPageImageUrl = PreviousPageImageUrl;
            field.PreviousPageText = PreviousPageText;
            field.ShowFirstPageButton = ShowFirstPageButton;
            field.ShowLastPageButton = ShowLastPageButton;
            field.ShowNextPageButton = ShowNextPageButton;
            field.ShowPreviousPageButton = ShowPreviousPageButton;
        }

        public override void CreateDataPagers(DataPagerFieldItem container, int startRowIndex, int maximumRows, int totalRowCount, int fieldIndex)
        {
            _startRowIndex = startRowIndex;
            _maximumRows = maximumRows;
            _totalRowCount = totalRowCount;
            _fieldIndex = fieldIndex;

            bool setPagePropertiesNeeded = false;
            bool queryMode = true;
            bool enablePrevFirst = _startRowIndex >= _maximumRows;
            bool enableNextLast = (_startRowIndex + _maximumRows) < _totalRowCount;
            bool addNonBreakingSpace = RenderNonBreakingSpacesBetweenControls;

            if (ShowFirstPageButton)
                CreateButton(container, DataControlCommands.FirstPageCommandArgument, FirstPageText, FirstPageImageUrl, 0,
                          queryMode, enablePrevFirst, addNonBreakingSpace);

            int newPageNum = -1;
            if (ShowPreviousPageButton)
            {
                if (queryMode)
                    newPageNum = (_startRowIndex / _maximumRows) - 1;

                CreateButton(container, DataControlCommands.PreviousPageCommandArgument, PreviousPageText, PreviousPageImageUrl, newPageNum,
                          queryMode, enablePrevFirst, addNonBreakingSpace);
            }

            if (ShowNextPageButton)
            {
                if (queryMode)
                    newPageNum = (_startRowIndex + _maximumRows) / _maximumRows;

                CreateButton(container, DataControlCommands.NextPageCommandArgument, NextPageText, NextPageImageUrl, newPageNum,
                          queryMode, enableNextLast, addNonBreakingSpace);
            }

            if (ShowLastPageButton)
            {
                if (queryMode)
                {
                    newPageNum = _totalRowCount / _maximumRows;
                    if ((_totalRowCount % _maximumRows) == 0)
                        newPageNum--;
                }

                CreateButton(container, DataControlCommands.LastPageCommandArgument, LastPageText, LastPageImageUrl, newPageNum,
                          queryMode, enableNextLast, addNonBreakingSpace);
            }

            if (setPagePropertiesNeeded)
                DataPager.SetPageProperties(_startRowIndex, _maximumRows, true);
        }

        void CreateButton(DataPagerFieldItem container, string commandName, string text, string imageUrl, int pageNum, bool queryMode, bool enabled, bool addNonBreakingSpace)
        {
            WebControl ctl = null;

            if (queryMode)
            {
                pageNum++;
                HyperLink h = new HyperLink();
                h.Text = text;
                h.ImageUrl = imageUrl;
                h.Enabled = enabled;
                h.NavigateUrl = GetQueryStringNavigateUrl(pageNum);
                h.CssClass = ButtonCssClass;
                ctl = h;
            }
            else
            {
                if (!enabled && RenderDisabledButtonsAsLabels)
                {
                    Label l = new Label();
                    l.Text = text;
                    ctl = l;
                }
                else
                {
                    switch (ButtonType)
                    {
                        case ButtonType.Button:
                            Button btn = new Button();
                            btn.CommandName = commandName;
                            btn.Text = text;
                            ctl = btn;
                            break;

                        case ButtonType.Link:
                            LinkButton lbtn = new LinkButton();
                            lbtn.CommandName = commandName;
                            lbtn.Text = text;
                            ctl = lbtn;
                            break;

                        case ButtonType.Image:
                            ImageButton ibtn = new ImageButton();
                            ibtn.CommandName = commandName;
                            ibtn.ImageUrl = imageUrl;
                            ibtn.AlternateText = text;
                            ctl = ibtn;
                            break;
                    }

                    if (ctl != null)
                    {
                        ctl.Enabled = enabled;
                        ctl.CssClass = ButtonCssClass;
                    }
                }
            }

            if (ctl != null)
            {
                container.Controls.Add(ctl);
                if (addNonBreakingSpace)
                    container.Controls.Add(new LiteralControl("&nbsp;"));
            }
        }

        protected override DataPagerField CreateField()
        {
            return new NextPreviousPagerField();
        }

        public override bool Equals(object o)
        {
            NextPreviousPagerField field = o as NextPreviousPagerField;
            if (field == null)
                return false;

            // Compare using the properties that are copied in CopyProperties
            if (field.ButtonCssClass != ButtonCssClass)
                return false;

            if (field.ButtonType != ButtonType)
                return false;

            if (field.FirstPageImageUrl != FirstPageImageUrl)
                return false;

            if (field.FirstPageText != FirstPageText)
                return false;

            if (field.LastPageImageUrl != LastPageImageUrl)
                return false;

            if (field.LastPageText != LastPageText)
                return false;

            if (field.NextPageImageUrl != NextPageImageUrl)
                return false;

            if (field.NextPageText != NextPageText)
                return false;

            if (field.PreviousPageImageUrl != PreviousPageImageUrl)
                return false;

            if (field.PreviousPageText != PreviousPageText)
                return false;

            if (field.ShowFirstPageButton != ShowFirstPageButton)
                return false;

            if (field.ShowLastPageButton != ShowLastPageButton)
                return false;

            if (field.ShowNextPageButton != ShowNextPageButton)
                return false;

            if (field.ShowPreviousPageButton != ShowPreviousPageButton)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int ret = 0;

            // Base the calculation on the properties that are copied in CopyProperties
            ret |= ButtonCssClass.GetHashCode();
            ret |= ButtonType.GetHashCode();
            ret |= FirstPageImageUrl.GetHashCode();
            ret |= FirstPageText.GetHashCode();
            ret |= LastPageImageUrl.GetHashCode();
            ret |= LastPageText.GetHashCode();
            ret |= NextPageImageUrl.GetHashCode();
            ret |= NextPageText.GetHashCode();
            ret |= PreviousPageImageUrl.GetHashCode();
            ret |= PreviousPageText.GetHashCode();
            ret |= ShowFirstPageButton.GetHashCode();
            ret |= ShowLastPageButton.GetHashCode();
            ret |= ShowNextPageButton.GetHashCode();
            ret |= ShowPreviousPageButton.GetHashCode();

            return ret;
        }

        public override void HandleEvent(CommandEventArgs e)
        {
            string commandName = e.CommandName;
            int newStartIndex = -1;
            int pageSize = DataPager.PageSize;

            if (String.Compare(commandName, DataControlCommands.FirstPageCommandArgument, StringComparison.OrdinalIgnoreCase) == 0)
                newStartIndex = 0;
            else if (String.Compare(commandName, DataControlCommands.LastPageCommandArgument, StringComparison.OrdinalIgnoreCase) == 0)
            {
                int lastPageMod = _totalRowCount % pageSize;
                if (lastPageMod == 0)
                    newStartIndex = _totalRowCount - pageSize;
                else
                    newStartIndex = _totalRowCount - lastPageMod;
            }
            else if (String.Compare(commandName, DataControlCommands.NextPageCommandArgument, StringComparison.OrdinalIgnoreCase) == 0)
            {
                newStartIndex = _startRowIndex + pageSize;
                if (_totalRowCount >= 0 && newStartIndex > _totalRowCount)
                    newStartIndex = _totalRowCount - pageSize;
            }
            else if (String.Compare(commandName, DataControlCommands.PreviousPageCommandArgument, StringComparison.OrdinalIgnoreCase) == 0)
            {
                newStartIndex = _startRowIndex - pageSize;
                if (newStartIndex < 0)
                    newStartIndex = 0;
            }

            if (newStartIndex >= 0)
                DataPager.SetPageProperties(newStartIndex, pageSize, true);
        }

        public string ButtonCssClass
        {
            get
            {
                string s = ViewState["ButtonCssClass"] as string;
                if (s != null)
                    return s;

                return String.Empty;
            }

            set { ViewState["ButtonCssClass"] = value; }
        }

        public ButtonType ButtonType
        {
            get
            {
                object o = ViewState["ButtonType"];
                if (o != null)
                    return (ButtonType)o;

                return ButtonType.Link;
            }

            set { ViewState["ButtonType"] = value; }
        }

        public string FirstPageImageUrl
        {
            get
            {
                string s = ViewState["FirstPageImageUrl"] as string;
                if (s != null)
                    return s;

                return String.Empty;
            }

            set { ViewState["FirstPageImageUrl"] = value; }
        }

        public string FirstPageText
        {
            get
            {
                string s = ViewState["FirstPageText"] as string;
                if (s != null)
                    return s;

                return "First";
            }

            set { ViewState["FirstPageText"] = value; }
        }

        public string LastPageImageUrl
        {
            get
            {
                string s = ViewState["LastPageImageUrl"] as string;
                if (s != null)
                    return s;

                return String.Empty;
            }

            set { ViewState["LastPageImageUrl"] = value; }
        }

        public string LastPageText
        {
            get
            {
                string s = ViewState["LastPageText"] as string;
                if (s != null)
                    return s;

                return "Last";
            }

            set { ViewState["LastPageText"] = value; }
        }

        public string NextPageImageUrl
        {
            get
            {
                string s = ViewState["NextPageImageUrl"] as string;
                if (s != null)
                    return s;

                return String.Empty;
            }

            set { ViewState["NextPageImageUrl"] = value; }
        }

        public string NextPageText
        {
            get
            {
                string s = ViewState["NextPageText"] as string;
                if (s != null)
                    return s;

                return "Next";
            }

            set { ViewState["NextPageText"] = value; }
        }

        public string PreviousPageImageUrl
        {
            get
            {
                string s = ViewState["PreviousPageImageUrl"] as string;
                if (s != null)
                    return s;

                return String.Empty;
            }

            set { ViewState["PreviousPageImageUrl"] = value; }
        }

        public string PreviousPageText
        {
            get
            {
                string s = ViewState["PreviousPageText"] as string;
                if (s != null)
                    return s;

                return "Previous";
            }

            set { ViewState["PreviousPageText"] = value; }
        }

        public bool RenderDisabledButtonsAsLabels
        {
            get
            {
                object o = ViewState["RenderDisabledButtonsAsLabels"];
                if (o != null)
                    return (bool)o;

                return false;
            }

            set { ViewState["RenderDisabledButtonsAsLabels"] = value; }
        }

        public bool RenderNonBreakingSpacesBetweenControls
        {
            get
            {
                object o = ViewState["RenderNonBreakingSpacesBetweenControls"];
                if (o != null)
                    return (bool)o;

                return true;
            }

            set { ViewState["RenderNonBreakingSpacesBetweenControls"] = value; }
        }

        public bool ShowFirstPageButton
        {
            get
            {
                object o = ViewState["ShowFirstPageButton"];
                if (o != null)
                    return (bool)o;

                return false;
            }

            set { ViewState["ShowFirstPageButton"] = value; }
        }

        public bool ShowLastPageButton
        {
            get
            {
                object o = ViewState["ShowLastPageButton"];
                if (o != null)
                    return (bool)o;

                return false;
            }

            set { ViewState["ShowLastPageButton"] = value; }
        }

        public bool ShowNextPageButton
        {
            get
            {
                object o = ViewState["ShowNextPageButton"];
                if (o != null)
                    return (bool)o;

                return true;
            }

            set { ViewState["ShowNextPageButton"] = value; }
        }

        public bool ShowPreviousPageButton
        {
            get
            {
                object o = ViewState["ShowPreviousPageButton"];
                if (o != null)
                    return (bool)o;

                return true;
            }

            set { ViewState["ShowPreviousPageButton"] = value; }
        }
    }
}
