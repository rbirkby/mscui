// <copyright file="Error.aspx.cs" company="Microsoft Corporation copyright 2008.">
// (c) 2008 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx.
//
// This document and/or software (�this Content�) has been created in partnership
// with the National Health Service (NHS) in England.  Intellectual Property Rights
// to this Content are jointly owned by Microsoft and the NHS in England, although 
// both Microsoft and the NHS are entitled to independently exercise their rights
// of ownership.  Microsoft acknowledges the contribution of the NHS in England
// through their Common User Interface programme to this Content.  Readers are 
// referred to www.cui.nhs.uk for further information on the NHS CUI Programme.
// </copyright>
// <date>10-Jan-2008</date>
// <summary>error control</summary>

namespace Microsoft.Cui.SampleWebsite
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Net;
    using System.Globalization;

    /// <summary>
    /// Error web page code behind
    /// </summary>
    public partial class Error : System.Web.UI.Page
    {
        /// <summary>
        /// Load time procesing
        /// </summary>
        /// <param name="e">Event Argument.</param>
        protected override void OnLoad(EventArgs e)
        {
            int errorCode = 200;
            HttpStatusCode statusCode = HttpStatusCode.OK;

            if (ConfigurationManager.AppSettings["skipBlog"] != null)
            {
                bool skipBlog = Convert.ToBoolean(ConfigurationManager.AppSettings["skipBlog"], CultureInfo.CurrentCulture);
                this.blogLink.Visible = !skipBlog;
            }

            // Validate user-supplied data
            if (Int32.TryParse(Request.QueryString.ToString(), out errorCode))
            {
                if (Enum.IsDefined(typeof(HttpStatusCode), errorCode))
                {
                    statusCode = (HttpStatusCode)errorCode;

                    this.lblErrorInfo.Text = "(" + errorCode + " - " + Enum.Format(typeof(HttpStatusCode), statusCode, "g") + ")";
                }
            }

            Response.StatusCode = (int)statusCode;

            base.OnLoad(e);
        }
    }
}
