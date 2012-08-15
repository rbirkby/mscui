// <copyright file="PlannedUpdatedControl.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>Planned Updated control</summary>

namespace Microsoft.Cui.SampleWebsite
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Control used to render out the planned or updated notes on the roadmap page.
    /// </summary>
    [ToolboxData("<{0}:PlannedUpdatedControl runat=\"server\"></{0}:PlannedUpdatedControl>")]
    public class PlannedUpdatedControl : CompositeControl
    {
        /// <summary>The date the item was last updated.</summary>
        private DateTime lastUpdatedDate = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the date the guidance item was last updated.
        /// If this is set to DateTime.MinValue, then the functionality is "planned".
        /// </summary>
        public DateTime LastUpdatedDate
        {
            get
            {
                return this.lastUpdatedDate;
            }

            set
            {
                this.lastUpdatedDate = value;
            }
        }

        /// <summary>
        /// Creates the child controls.
        /// </summary>
        protected override void CreateChildControls()
        {
            // Good practice (pg304 Nikhil's book)
            Controls.Clear();
            LiteralControl information = new LiteralControl();

            if (this.lastUpdatedDate == DateTime.MinValue) 
            {
                this.CssClass = "guidancePlanned";
                information.Text = "- planned for future release";
            }
            else 
            {
                this.CssClass = "guidanceUpdated";
                information.Text = "- design guidance updated " + this.lastUpdatedDate.ToString("MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
           
            Controls.Add(information);
        }
    }
}
