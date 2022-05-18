public partial class _Default : System.Web.UI.Page
{

    public override void Dispose()
    {
        base.Dispose();
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
             
    }


    protected void lbtnDamen_Click(object sender, System.EventArgs e)
    {
        Session["gender"] = "MALE";
    }

    protected void lbtnHerren_Click(object sender, System.EventArgs e)
    {
        Session["gender"] = "FEMALE";
    }
}