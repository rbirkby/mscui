//-----------------------------------------------------------------------
// <copyright file="PatientBannerLayoutEngine.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>The PatientBanner Custom LayoutEngine.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms.Layout;
    using System.Windows.Forms;
    using System.Drawing;

    /// <exclude />
    /// <summary>
    /// The PatientBanner custom layout engine.
    /// </summary>
    public class PatientBannerLayoutEngine : LayoutEngine
    {
        /// <summary>
        /// The PatientBanner layout. 
        /// </summary>
        /// <param name="container">The layout control.</param>
        /// <param name="layoutEventArgs">The event args.</param>
        /// <returns>True if layout should be performed again by the parent of the container; otherwise, false.</returns>
        public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
        {
            return false;
        }
    }
}
