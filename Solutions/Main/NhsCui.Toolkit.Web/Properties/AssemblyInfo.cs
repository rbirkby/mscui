//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>assembly attributes</summary>
//-----------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NhsCui.Toolkit.Web")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4a4f1a41-b000-45ec-bdf9-418abeace497")]

// use NhsCui tag prefix when the designer creates controls
[assembly: TagPrefix("NhsCui.Toolkit.Web", "NhsCui")]

// Enable script combining for Toolkit scripts 
// PS#6393 Fix - Excluded DateInputBox.js - There is a problem in IE if too many 
// scripts are combined, as the Script tag emitted to the page that references the 
// combined script URL, breaks the the max URL length for IE (2084 chars) as it 
// contains a fully-qualified name, version, public-key token etc., for each script file
// Best to leave it that all the smaller scripts are combined into a single call and 
// move one large one out so it will it only mean one more large payload in a 
// separate HTTP get. Problem occured on the End-toEnd sample page and in the 
// Date CSS Styling in the MSCUI site and likely to occur elsewhere if multiple 
// different types of control result in large numbers of scripts being combined...
[assembly: AjaxControlToolkit.ScriptCombine(ExcludeScripts = "NhsCui.Toolkit.Web.DateInputBoxControl.DateInputBox.js")]

// Global FxCop excludes
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NhsCui.Toolkit.Web")]