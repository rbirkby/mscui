//-----------------------------------------------------------------------
// <copyright file="TelephoneInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved.
//
// CERTAIN PARTS OF THIS WORK CONTAIN SOFTWARE CODE THAT IS LICENSED 
// FOR USE UNDER THE MICROSOFT PUBLIC LICENSE. DISTRIBUTION, IN SOURCE CODE 
// OR OBJECT CODE FORM, OF THOSE PARTS MUST COMPLY WITH THE TERMS OF THE 
// PUBLIC LICENSE. SEE http://www.microsoft.com/opensource/licenses.mspx 
// FOR DETAILS.  
// IF YOU BRING A PATENT CLAIM AGAINST ANY CONTRIBUTOR OVER PATENTS THAT 
// YOU CLAIM ARE INFRINGED BY THE PUBLIC LICENSE SOFTWARE, YOUR PATENT 
// LICENSE FROM SUCH CONTRIBUTOR TO THE SOFTWARE ENDS AUTOMATICALLY.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>14-Aug-2007</date>
// <summary>TelephoneInputBox Extender, class to provide server-side configuration 
// of the TelephoneInputBox's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.TelephoneInputBoxControl.TelephoneInputBox.js", "text/javascript")]

#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Web.UI;
    using AjaxControlToolkit;
    using System.Web.UI.WebControls;
    using System.Web.Script.Serialization;

    /// <summary>
    /// TelephoneInputBox Extender, class to provide server-side configuration of 
    /// the TelephoneInputBox's AJAX functionality
    /// </summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [ClientScriptResource("NhsCui.Toolkit.Web.TelephoneInputBox", "NhsCui.Toolkit.Web.TelephoneInputBoxControl.TelephoneInputBox.js")]
    internal class TelephoneInputBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// object to hold our state
        /// </summary>
        private TelephoneInputClientState state;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TelephoneInputBoxExtender()
        {
            this.EnableClientState = true;
            this.state = new TelephoneInputClientState();

            this.ClientStateValuesLoaded += new EventHandler(this.TelephoneInputBoxExtender_ClientStateValuesLoaded);
        }

        /// <summary>
        /// Handle loading of client state
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void TelephoneInputBoxExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();

                // May need to create one...
                // jss.RegisterConverters(new JavaScriptConverter[] { new TelephoneJavascriptConverter() });

                this.state = jss.Deserialize<TelephoneInputClientState>(ClientState);

                // this.value = this.state.Value;
            }
        }
    }
}
