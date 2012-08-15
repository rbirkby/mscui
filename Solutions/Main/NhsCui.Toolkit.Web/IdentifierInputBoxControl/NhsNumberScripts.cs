//-----------------------------------------------------------------------
// <copyright file="NhsNumberScripts.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// Class to include NhsNumber.js class.
// <date>10-Sep-2007</date>
// <summary>Class to include NhsNumber.js script</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.NhsNumberScripts.ScriptFile, "application/x-javascript")]
[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.NhsNumberScripts.ScriptFile, NhsCui.Toolkit.Web.NhsNumberScripts.ResourceFile, NhsCui.Toolkit.Web.NhsNumberScripts.ResourceClass)]

namespace NhsCui.Toolkit.Web
{
    using System;
    using AjaxControlToolkit;

    /// <summary>
    /// Class to include NhsNumber.js script
    /// </summary>
    /// <exclude />
    [ClientScriptResource(null, NhsCui.Toolkit.Web.NhsNumberScripts.ScriptFile)]
    public class NhsNumberScripts
    {
        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string ResourceFile = "NhsCui.Toolkit.Web.IdentifierInputBoxControl.NhsNumberExceptionResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string ScriptFile = "NhsCui.Toolkit.Web.IdentifierInputBoxControl.NhsNumber.js";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string ResourceClass = "NhsNumberExceptionResources";
    }
}
