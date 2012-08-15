//-----------------------------------------------------------------------
// <copyright file="GlobalizationService.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NhsCui.Toolkit.Web globalization</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

var GlobalizationService = NhsCui.Toolkit.Web.GlobalizationService = function() {
    NhsCui.Toolkit.Web.GlobalizationService.initializeBase(this);
    
    this.name = "en-GB";
    this.shortTimePattern = "HH:mm";
    this.shortDatePattern = "dd-MMM-yyyy";
    this.shortDatePatternWithDayOfWeek = "ddd dd-MMM-yyyy";
    this.shortTimePatternWithSeconds = "HH:mm:ss";
    this.shortTimePatternWithSeconds12Hour = "hh:mm:ss";
    this.shortTimePatternWithSeconds12HourAMPM = "hh:mm:ss (tt)";
    this.shortTimePatternWithSecondsAMPM = "HH:mm:ss (tt)";
    this.shortTimePatternAMPM = "HH:mm (tt)";
    this.shortTimePattern12Hour = "hh:mm";
    this.shortTimePattern12HourAMPM = "hh:mm (tt)";
}
