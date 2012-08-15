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
// <date>19-Jun-2008</date>
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

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Microsoft.Cui.Controls.GenderLabel.#Value", Justification = "As per the spec.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.NhsNumber.#TryParseNhsNumber(System.String,System.String&)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.#TryParse(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan&,System.Globalization.CultureInfo,System.Boolean)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.#TryParse(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan&,System.Globalization.CultureInfo)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.#TryParseExact(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTime&,System.Globalization.CultureInfo)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiDate.#TryParseExact(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiDate&,System.Globalization.CultureInfo)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Scope = "member", Target = "Microsoft.Cui.Controls.WrapDataGrid.#HandleSelect(System.Int32,Microsoft.Cui.Controls.SelectionSource,System.Windows.RoutedEventArgs)", Justification = "Routed event is referred in wpf version.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.#TryParse(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan&,System.Boolean)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "Microsoft.Cui.Controls.Common.DateAndTime.InvalidArithmeticSetException", Justification = "Not valid for silverlight.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Microsoft.Cui.Controls.WrapDataGrid.#WrapDataGridColumns", Justification = "By design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.#TryParseExact(System.String,Microsoft.Cui.Controls.Common.DateAndTime.CuiTime&,System.IFormatProvider)", Justification = "Parse methods have out params by design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "cultureInfo", Scope = "member", Target = "Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.#Parse(System.String,System.Globalization.CultureInfo,System.Boolean)", Justification = "By design.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Scope = "member", Target = "Microsoft.Cui.Controls.LabelAutomationPeer.#Value", Justification = "Need to implement value to implement IvalueProvider implementation pattern.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Scope = "member", Target = "Microsoft.Cui.Controls.GraphPoint.#X")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Scope = "member", Target = "Microsoft.Cui.Controls.GraphPoint.#Y")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Scope = "type", Target = "Microsoft.Cui.Controls.TimeFrequency+TimeUnit")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Scope = "member", Target = "Microsoft.Cui.Controls.TimeGraphBase.#PlotTimeTextBlock(System.DateTime,System.Double,Microsoft.Cui.Controls.TimeFrequency)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Scope = "member", Target = "Microsoft.Cui.Controls.TimePoint.#Y")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Microsoft.Cui.Controls.TimeGraphBase.#GetAxisDesign()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Scope = "member", Target = "Microsoft.Cui.Controls.WrapDataGrid.#HandleParentKeyEvent(System.Object,System.Windows.Input.KeyEventArgs)")]
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
