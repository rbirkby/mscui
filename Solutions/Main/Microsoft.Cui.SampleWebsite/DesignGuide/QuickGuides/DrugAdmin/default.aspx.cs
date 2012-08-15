namespace Nhs.Cui.QIGs.Web.quickguides.drugadmin
{
    #region Using...

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;

    #endregion

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Attributes.Add("PageHref", "quickguides/drugadmin/default.aspx");
            Master.Attributes.Add("SubHeader1", "Overview"); 
        }
    }
}