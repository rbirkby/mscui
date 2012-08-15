//-----------------------------------------------------------------------
// <copyright file="NhsTimeSpanScripts.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Class to include NhsTimeSpan.js script</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.NhsTimeSpanScripts.ScriptFile, "application/x-javascript")]
[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.NhsTimeSpanScripts.ScriptFile, NhsCui.Toolkit.Web.NhsTimeSpanScripts.ResourceFile, NhsCui.Toolkit.Web.NhsTimeSpanScripts.ResourceClass)]

namespace NhsCui.Toolkit.Web
{
    using System;
    using AjaxControlToolkit;

    /// <summary>
    /// Class to include NhsTimeSpan.js script
    /// </summary>
    [RequiredScript(typeof(DateTimeScripts))]
    [ClientScriptResource(null, NhsCui.Toolkit.Web.NhsTimeSpanScripts.ScriptFile)]
    internal class NhsTimeSpanScripts
    {
        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string ResourceFile = "NhsCui.Toolkit.Web.Common.NhsTimeSpanResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string ScriptFile = "NhsCui.Toolkit.Web.Common.NhsTimeSpan.js";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string ResourceClass = "NhsTimeSpanResources";
    }
}
