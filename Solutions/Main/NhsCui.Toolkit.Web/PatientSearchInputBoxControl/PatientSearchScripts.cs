//-----------------------------------------------------------------------
// <copyright file="PatientSearchScripts.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09-Feb-2007</date>
// <summary>Class to include Parser.js script</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientSearchScripts.CommonFamilyNamesScriptFile, "application/x-javascript")]
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientSearchScripts.CommonThoroughfaresScriptFile, "application/x-javascript")]
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientSearchScripts.ParserScriptFile, "application/x-javascript")]
[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.PatientSearchScripts.TitlesScriptFile, "application/x-javascript")]

[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.PatientSearchScripts.CommonFamilyNamesScriptFile, NhsCui.Toolkit.Web.PatientSearchScripts.CommonFamilyNamesResourceFile, NhsCui.Toolkit.Web.PatientSearchScripts.CommonFamilyNamesResourceClass)]
[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.PatientSearchScripts.CommonThoroughfaresScriptFile, NhsCui.Toolkit.Web.PatientSearchScripts.CommonThoroughfaresResourceFile, NhsCui.Toolkit.Web.PatientSearchScripts.CommonThoroughfaresResourceClass)]
[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.PatientSearchScripts.ParserScriptFile, NhsCui.Toolkit.Web.PatientSearchScripts.ParserResourceFile, NhsCui.Toolkit.Web.PatientSearchScripts.ParserResourceClass)]
[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.PatientSearchScripts.TitlesScriptFile, NhsCui.Toolkit.Web.PatientSearchScripts.TitlesResourceFile, NhsCui.Toolkit.Web.PatientSearchScripts.TitlesResourceClass)]

namespace NhsCui.Toolkit.Web
{
    using System;
    using AjaxControlToolkit;

    /// <summary>
    /// Class to include Parser.js script
    /// </summary>
    [ClientScriptResource(null, NhsCui.Toolkit.Web.PatientSearchScripts.CommonFamilyNamesScriptFile)]
    [ClientScriptResource(null, NhsCui.Toolkit.Web.PatientSearchScripts.CommonThoroughfaresScriptFile)]
    [ClientScriptResource(null, NhsCui.Toolkit.Web.PatientSearchScripts.ParserScriptFile)]
    [ClientScriptResource(null, NhsCui.Toolkit.Web.PatientSearchScripts.TitlesScriptFile)]
    internal class PatientSearchScripts
    {
        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string CommonFamilyNamesResourceFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.CommonFamilyNamesResources";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string CommonFamilyNamesResourceClass = "CommonFamilyNamesResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string CommonFamilyNamesScriptFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.CommonFamilyNames.js";

        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string CommonThoroughfaresResourceFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.CommonThoroughfaresResources";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string CommonThoroughfaresResourceClass = "CommonThoroughfaresResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string CommonThoroughfaresScriptFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.CommonThoroughfares.js";

        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string ParserResourceFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.ParserResources";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string ParserResourceClass = "ParserResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string ParserScriptFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.Parser.js";

        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string TitlesResourceFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.TitlesResources";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string TitlesResourceClass = "TitlesResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string TitlesScriptFile = "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.TitlesResources.js";
    }
}
