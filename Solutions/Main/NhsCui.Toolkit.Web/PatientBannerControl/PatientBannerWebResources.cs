//-----------------------------------------------------------------------
// <copyright file="PatientBannerWebResources.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation.
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
// <summary>File to register patient banner web resources</summary>
//-----------------------------------------------------------------------

// register javascript associated with this control as a web resource
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.Javascript, "text/javascript")]

// register javascript associated with this control as a web resource
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.CommonJavascript, "text/javascript")]

// default patient image
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.DefaultPatientImage, "image/gif")]

// default expand image
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.DefaultExpandImage, "image/gif")]

// default collapse image
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.DefaultCollapseImage, "image/gif")]

// stylesheet
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.StyleSheet, "text/css", PerformSubstitution = true)]

// zone 2 title background image resource
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.Zone2TitleBackgroundImage, "image/png")]

// zone 1 background for deceased patients image resource
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.Zone1DeadBackgroundImage, "image/bmp")]

// Icon that will be used when allergies are not recorded
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.AllergiesNotPresentImage, "image/gif")]

// Icon that will be used when allergies are none known/present
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.AllergiesPresentImage, "image/gif")]

// Icon that will be used when allergies information is not available
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.AllergiesUnavailableImage, "image/gif")]

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientBannerWebResources.DeceasedPatientTransparentIcon, "image/gif")]
namespace NhsCui.Toolkit.Web
{
    using System;

    /// <summary>
    /// Patient banner web resources
    /// </summary>
    internal static class PatientBannerWebResources 
    {
        /// <summary>
        /// Name of the patient banner web resource
        /// </summary>
        public const string Javascript = "NhsCui.Toolkit.Web.PatientBannerControl.PatientBanner.js";

        /// <summary>
        /// Name of the Common.js web resource
        /// </summary>
        public const string CommonJavascript = "NhsCui.Toolkit.Web.Common.Common.js";

        /// <summary>
        /// Name of the default patient image web resource
        /// </summary>
        public const string DefaultPatientImage = "NhsCui.Toolkit.Web.PatientBannerControl.DefaultPatient.gif";

        /// <summary>
        /// Name of the default expand image web resource
        /// </summary>
        public const string DefaultExpandImage = "NhsCui.Toolkit.Web.PatientBannerControl.DefaultExpand.gif";

        /// <summary>
        /// Name of the default collapse image web resource
        /// </summary>
        public const string DefaultCollapseImage = "NhsCui.Toolkit.Web.PatientBannerControl.DefaultCollapse.gif";

        /// <summary>
        /// Name of style sheet web resource
        /// </summary>
        public const string StyleSheet = "NhsCui.Toolkit.Web.PatientBannerControl.PatientBanner.css";

        /// <summary>
        /// Name of Zone 2 label background image resource
        /// </summary>
        public const string Zone2TitleBackgroundImage = "NhsCui.Toolkit.Web.PatientBannerControl.Zone2TitleBackground.png";

        /// <summary>
        /// Name of Zone 1 background for deceased patients image resource
        /// </summary>
        public const string Zone1DeadBackgroundImage = "NhsCui.Toolkit.Web.PatientBannerControl.Zone1DeadBackground.bmp";

        /// <summary>
        /// Icon that will be used when allergies are none known/present
        /// </summary>
        public const string AllergiesPresentImage = "NhsCui.Toolkit.Web.PatientBannerControl.AllergiesPresentIcon.gif";

        /// <summary>
        /// Icon that will be used when allergies are not recorded
        /// </summary>
        public const string AllergiesNotPresentImage = "NhsCui.Toolkit.Web.PatientBannerControl.AllergiesNotPresentIcon.gif";

        /// <summary>
        /// Icon that will be used when allergies information is not available
        /// </summary>
        public const string AllergiesUnavailableImage = "NhsCui.Toolkit.Web.PatientBannerControl.AllergiesUnavailable.gif";

        /// <summary>
        /// Icon that will be shown before the patient name for a deceased patient. This icon is transparent
        /// </summary>
        public const string DeceasedPatientTransparentIcon = "NhsCui.Toolkit.Web.PatientBannerControl.DeceasedPatientTransparentIcon.gif";
    }
}
