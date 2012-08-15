//-----------------------------------------------------------------------
// <copyright file="MedicationsListViewExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>18-Jan-2007</date>
// <summary>MedicationListView Extender </summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.MedicationGridControl.MedicationListView.js", "text/javascript")]

#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Web.UI.WebControls;
    using System.Web;
    using System.Web.UI;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections.Generic;    
    using System.Web.Script.Serialization;

    using AjaxControlToolkit;
    using NhsCui.Toolkit.DateAndTime;    

    /// <summary>
    /// DateInputBox Extender
    /// </summary>
    [ToolboxItem(false)]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]    
    [RequiredScript(typeof(PopupExtender), 2)]
    [RequiredScript(typeof(TimerScript), 3)]
    [TargetControlType(typeof(MedicationListView))]
    [ClientScriptResource("NhsCui.Toolkit.Web.MedicationListView", "NhsCui.Toolkit.Web.MedicationGridControl.MedicationListView.js")]
    [DesignTimeVisible(false)]
    internal class MedicationListViewExtender : ExtenderControlBase
    {
    }
}
