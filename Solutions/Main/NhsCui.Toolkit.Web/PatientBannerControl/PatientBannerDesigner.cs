//-----------------------------------------------------------------------
// <copyright file="PatientBannerDesigner.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Feb-2007</date>
// <summary>Designer for Patient Banner</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Resources;
    using System.Web.UI.Design.WebControls;

    /// <summary>
    /// Designer for Patient Banner
    /// </summary>
    internal class PatientBannerDesigner : CompositeControlDesigner
    {
        /// <summary>
        /// Retrieves the HTML markup that is used to represent 
        /// the control at design time. 
        /// </summary>
        /// <returns>The HTML markup used to represent the control at design time.</returns>
        public override string GetDesignTimeHtml()
        {
            PatientBanner patientBanner = (PatientBanner)this.Component;
            string html = base.GetDesignTimeHtml();
            if (patientBanner.IsUsingDefaultStyleSheet)
            {
                // write our default stylesheet into to page, as runtime mechanism
                // used by the control this doesn't work at design time.
                using (Stream s = typeof(PatientBannerWebResources).Assembly.GetManifestResourceStream(PatientBannerWebResources.StyleSheet))
                {
                    using (StreamReader r = new StreamReader(s))
                    {
                        html += "<style>" + r.ReadToEnd() + "</style>";
                    }
                }
            }

            return html;
        }
    }
}
