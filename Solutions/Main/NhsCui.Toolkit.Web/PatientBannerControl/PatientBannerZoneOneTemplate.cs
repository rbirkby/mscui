//-----------------------------------------------------------------------
// <copyright file="PatientBannerZoneOneTemplate.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation.
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
// <summary>default template used to generate zone 1 area of patient banner</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// default template used to generate zone 1 area of patient
    /// banner
    /// </summary>
    [ToolboxItem(false)]
    internal class PatientBannerZoneOneTemplate : PatientBannerTemplateBase, ITemplate
    {
        #region Const Values
        /// <summary>
        /// non-breaking space
        /// </summary>
        private const string NonBreakingSpace = "&nbsp;";
        #endregion

        #region ITemplate Members
        /// <summary>
        /// When implemented by a class, defines the Control object that child controls 
        /// and templates belong to. These child controls are in turn defined within an 
        /// inline template. 
        /// </summary>
        /// <param name="container">The Control object to contain the instances of controls from the inline template. </param>
        void ITemplate.InstantiateIn(Control container)
        {
            // add extra container with specific width to trigger IE to fire
            // mouse events correctly
            Panel parent = new Panel();
            parent.Style[HtmlTextWriterStyle.Width] = "100%";
            container.Controls.Add(parent);
            InstantiatePatientImage(parent);
            InstantiateItems(parent);
            InstantiateClearingPanel(parent);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// instantiate patient image
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiatePatientImage(Control parent)
        {
            Panel imagePanel = new Panel();
            imagePanel.ID = PatientBannerControlIds.PatientImageCell;

            // setting overflow hidden PS # 6561
            imagePanel.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");
            imagePanel.Style.Add("float", "left");

            Image patientImage = new Image();
            patientImage.Width = Unit.Pixel(48);
            patientImage.Height = Unit.Pixel(48);
            patientImage.ID = PatientBannerControlIds.PatientImage;

            imagePanel.Controls.Add(patientImage);
            parent.Controls.Add(imagePanel);
        }

        /// <summary>
        /// instantiate items
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateItems(Control parent)
        {
            Panel patientNamePanel = new Panel();
            patientNamePanel.Style.Add("float", "left");
            parent.Controls.Add(patientNamePanel);

            InstantiatePatientName(patientNamePanel);

            Panel patientDataPanel = new Panel();
            patientDataPanel.Style.Add("float", "right");
            parent.Controls.Add(patientDataPanel);

            InstantiatePatientDetails(patientDataPanel);            
        }

        /// <summary>
        /// instantiate name item
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiatePatientName(Control parent)
        {
            parent.ID = PatientBannerControlIds.ZoneOnePatientNameCell;

            Image img = new Image();
            img.Width = Unit.Pixel(1);
            img.Height = Unit.Pixel(1);
            img.ID = PatientBannerControlIds.DeceasedPatientTransparentIcon;
            parent.Controls.Add(img);

            NameLabel name = new NameLabel();
            name.ID = PatientBannerControlIds.Name;
            name.CssClass = PatientBannerCssClasses.PatientName;
            parent.Controls.Add(name);

            LiteralControl separator = new LiteralControl(PatientBannerControl.Resources.SeparatorControlHtmlText);
            separator.ID = PatientBannerControlIds.PreferredNameSeparator;
            parent.Controls.Add(separator);

            InstantiateLabelItemPair(
                                parent, 
                                PatientBannerControlIds.PreferredNameLabel, 
                                PatientBannerControlIds.PreferredName,
                                new Label());
        }

        /// <summary>
        /// Instatiates the patient DoB, DoD, Gender and NhsNumber details
        /// </summary>
        /// <param name="parent">Control to instantiate in</param>
        private static void InstantiatePatientDetails(Control parent)
        {
            parent.ID = PatientBannerControlIds.ZoneOnePatientDataCell;

            // Instatiate DoB
            InstantiateLabelItemPair(
                               parent,
                               PatientBannerControlIds.DateOfBirthLabel,
                               PatientBannerControlIds.DateOfBirth,
                               new DateLabel());
            Label age = new Label();
            age.CssClass = PatientBannerCssClasses.ZoneOneData;
            age.ID = PatientBannerControlIds.Age;
            parent.Controls.Add(age);
            parent.Controls.Add(new LiteralControl(PatientBannerControl.Resources.NonBreakingSpace));

            // Instantiate gender
            InstantiateLabelItemPair(
                                parent,
                                PatientBannerControlIds.GenderLabel,
                                PatientBannerControlIds.Gender,
                                new GenderLabel());
            parent.Controls.Add(new LiteralControl(PatientBannerControl.Resources.NonBreakingSpace));

            // Instantiate Identifier
            InstantiateLabelItemPair(
                               parent,
                               PatientBannerControlIds.IdentifierLabel,
                               PatientBannerControlIds.Identifier,
                               new IdentifierLabel());

            // Add a separator Control to show DoD and age at death
            LiteralControl separator = new LiteralControl(PatientBannerControl.Resources.SeparatorControlHtmlText);
            separator.ID = PatientBannerControlIds.BornDiedSeparator;
            parent.Controls.Add(separator);

            // Instantiate DoD
            InstantiateLabelItemPair(
                            parent,
                            PatientBannerControlIds.DateOfDeathLabel,
                            PatientBannerControlIds.DateOfDeath,
                            new DateLabel());
            parent.Controls.Add(new LiteralControl(PatientBannerControl.Resources.NonBreakingSpace));

            // Instantiate Age at death
            InstantiateLabelItemPair(
                            parent,
                            PatientBannerControlIds.AgeAtDeathLabel,
                            PatientBannerControlIds.AgeAtDeath,
                            new TimeSpanLabel());  
        }

        /// <summary>
        /// Create label item pairs
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        /// <param name="labelId">label id</param>
        /// <param name="itemId">item id</param>
        /// <param name="item">item</param>
        private static void InstantiateLabelItemPair(Control parent, string labelId, string itemId, WebControl item)
        {
            Label label = new Label();
            label.CssClass = PatientBannerCssClasses.ZoneOneLabel;
            label.ID = labelId;
            item.ID = itemId;
            item.CssClass = PatientBannerCssClasses.ZoneOneData;

            parent.Controls.Add(label);
            parent.Controls.Add(item);
        }

        #endregion
    }
}
