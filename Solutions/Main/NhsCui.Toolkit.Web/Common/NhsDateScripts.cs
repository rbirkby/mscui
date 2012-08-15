//-----------------------------------------------------------------------
// <copyright file="NhsDateScripts.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// Class to include NhsDate.js class.
// <date>09-Feb-2007</date>
// <summary>Class to include NhsDate.js script</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.NhsDateScripts.ScriptFile, "application/x-javascript")]
[assembly: System.Web.UI.ScriptResource(NhsCui.Toolkit.Web.NhsDateScripts.ScriptFile, NhsCui.Toolkit.Web.NhsDateScripts.ResourceFile, NhsCui.Toolkit.Web.NhsDateScripts.ResourceClass)]

namespace NhsCui.Toolkit.Web
{
    using System;
    using AjaxControlToolkit;

    /// <summary>
    /// Class to include NhsDate.js script
    /// </summary>
    /// <exclude />
    [RequiredScript(typeof(DateTimeScripts))]
    [ClientScriptResource(null, NhsCui.Toolkit.Web.NhsDateScripts.ScriptFile)]
    public class NhsDateScripts
    {
        /// <summary>
        /// Name of resource file
        /// </summary>
        public const string ResourceFile = "NhsCui.Toolkit.Web.Common.NhsDateResources";

        /// <summary>
        /// Name of script file
        /// </summary>
        public const string ScriptFile = "NhsCui.Toolkit.Web.Common.NhsDate.js";

        /// <summary>
        /// Name of script resource class
        /// </summary>
        public const string ResourceClass = "NhsDateResources";
    }
}
