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

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NhsCui.Toolkit.Web.Test")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("505adc73-29b7-47da-8bf7-c7cae6c7787a")]

// Global excludes
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NhsCui.Toolkit")]

// For auto-gen VSCodeGenAccessor.cs
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_IdentifierLabelAccessor.RenderContents(System.Web.UI.HtmlTextWriter):System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject.Invoke(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_NameLabelAccessor.RenderContentsInternal(System.Web.UI.HtmlTextWriter,System.Boolean):System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject.Invoke(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_NameLabelAccessor.RenderNameItem(System.Web.UI.HtmlTextWriter,System.String,System.String,System.Boolean):System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject.Invoke(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_NameLabelAccessor.RenderText(System.Web.UI.HtmlTextWriter,System.String,System.Boolean):System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType.InvokeStatic(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_NameLabelAccessor.RenderContents(System.Web.UI.HtmlTextWriter):System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject.Invoke(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_NameLabelAccessor.TrackViewState():System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject.Invoke(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelAccessor.RenderContents(System.Web.UI.HtmlTextWriter):System.Void", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject.Invoke(System.String,System.Type[],System.Object[])")]
[module: SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Scope = "member", Target = "NhsCui.Toolkit.Web.Test.BaseAccessor.Equals(System.Object):System.Boolean")]