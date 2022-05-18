public partial class main : System.Web.UI.MasterPage
{

    public override void Dispose()
    {
        base.Dispose();
    }


    protected void lbtnHerren_Click(object sender, System.EventArgs e)
    {
        Session["gender"] = AllEnums.UserInfo.GenderEnglishName(AllEnums.UserInfo.Gender.Herren);
    }

    protected void lbtnDamen_Click(object sender, System.EventArgs e)
    {
        Session["gender"] = AllEnums.UserInfo.GenderEnglishName(AllEnums.UserInfo.Gender.Damen);
    }
}
