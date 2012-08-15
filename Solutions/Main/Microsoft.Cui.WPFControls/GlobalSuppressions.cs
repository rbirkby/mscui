//-----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-Jun-2008</date>
// <summary>Global suppressions file for code analysis. </summary>
//-----------------------------------------------------------------------

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiDate.#TryParseExact(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiDate&,System.Globalization.CultureInfo)", Justification = "TryParse methods needs out parameters")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.#TryParse(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan&,System.Boolean)", Justification = "TryParse methods needs out parameters")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.NhsNumber.#TryParseNhsNumber(System.String,System.String&)", Justification = "TryParse methods needs out parameters")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.#TryParseExact(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTime&,System.IFormatProvider)", Justification = "TryParse methods needs out parameters")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Microsoft.Cui.Controls.GenderLabel.#Value", Justification = "As per spec")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cultureInfo", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.#Parse(System.String,System.Globalization.CultureInfo,System.Boolean)", Justification = "As per spec")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "Microsoft.Cui.Controls.Common.DateAndTime.InvalidArithmeticSetException", Justification = "Implemented as per Spec")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Microsoft.Cui.Controls.WrapDataGrid.#WrapDataGridColumns", Justification = "As per spec")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.Font.#TextDecorations", Justification = "Converters return a collection and keeping the setter to avoild looping again.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "XamlGeneratedNamespace", Justification = "Auto generated namespace.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Microsoft.Cui.Controls.DataTemplateHelper.#LoadContent(System.Windows.DataTemplate)", Justification = "Silverlight doesnt support FrameworkTemplates, supressing this message as we are sharing the same codebase")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Microsoft.Cui.Controls.FilterControl.#System.Windows.Markup.IComponentConnector.Connect(System.Int32,System.Object)", Justification = "Autogenerated code")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Microsoft.Cui.Controls.GroupingControl.#System.Windows.Markup.IComponentConnector.Connect(System.Int32,System.Object)", Justification = "Autogenerated code")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Microsoft.Cui.Controls.LevelOfDetailControl.#System.Windows.Markup.IComponentConnector.Connect(System.Int32,System.Object)", Justification = "Autogenerated code")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Microsoft.Cui.Controls.WrapDataGrid.#System.Windows.Markup.IComponentConnector.Connect(System.Int32,System.Object)", Justification = "Autogenerated code")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.CuiContentControl.#TextDecorations", Justification = "Dependency property is of type Collection and hence retaining the setter.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Microsoft.Cui.Controls.WaitAnimation.#System.Windows.Markup.IComponentConnector.Connect(System.Int32,System.Object)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Microsoft.Cui.Controls.LabelAutomationPeer.#System.Windows.Automation.Provider.IValueProvider.SetValue(System.String)", Justification = "Method not implemented.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Scope = "member", Target = "Microsoft.Cui.Controls.WrapDataGrid.#HandleParentKeyEvent(System.Object,System.Windows.Input.KeyEventArgs)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Scope = "member", Target = "Microsoft.Cui.Controls.GraphPoint.#X")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Scope = "member", Target = "Microsoft.Cui.Controls.GraphPoint.#Y")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Microsoft.Cui.Controls.TimeFrequency+TimeUnit")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.DateLabel.#NullStrings", Justification = "Need a setter to be able to do DataBinding.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Microsoft.Cui.Controls.DateLabel.#Value", Justification = "Property named as per specifications.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Microsoft.Cui.Controls.TimeLabel.#Value", Justification = "Property named as per specifications.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.TimeLabel.#NullStrings", Justification = "Need a setter to be able to do DataBinding.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.PatientBanner.#Allergies", Justification = "Need a setter to be able to do DataBinding.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Microsoft.Cui.Controls.SplitListBox.#RaiseItemSelected()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Microsoft.Cui.Controls.SplitComboBox.#RaiseItemSelected()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.TimeActivityGraphHost.#Graphs", Justification = "Need a setter to be able to do DataBinding.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Scope = "member", Target = "Microsoft.Cui.Controls.TimeActivityGraph.#Activities", Justification = "Need a setter to be able to do DataBinding.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Scope = "type", Target = "Microsoft.Cui.Controls.BrushHighContrastSetter")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Scope = "type", Target = "Microsoft.Cui.Controls.LinearGradientBrushHighContrastSetter")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Scope = "type", Target = "Microsoft.Cui.Controls.SolidColorBrushHighContrastSetter")]
