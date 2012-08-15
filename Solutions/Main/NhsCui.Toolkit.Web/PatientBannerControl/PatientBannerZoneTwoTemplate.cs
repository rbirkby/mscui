//-----------------------------------------------------------------------
// <copyright file="PatientBannerZoneTwoTemplate.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>default template used to generate zone 2 area of patient banner</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// default template used to generate zone 2 area of patient banner
    /// </summary>
    [ToolboxItem(false)]
    internal class PatientBannerZoneTwoTemplate : PatientBannerTemplateBase, ITemplate
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
            InstantiateTitles(container);
            InstantiateItems(container);
            InstantiateLinks(container);
        }
        #endregion

        #region Private methods

        /// <summary>
        /// instantiate labels and expand button
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateTitles(Control parent)
        {
            TableHeaderRow labelsRow = new TableHeaderRow();
            labelsRow.ID = PatientBannerControlIds.ZoneTwoPermanent;

            WebControl subsectionOne = InstantiateTitle(labelsRow, PatientBannerControlIds.SubsectionOneTitle);
            WebControl subsectionTwo = InstantiateTitle(labelsRow, PatientBannerControlIds.SubsectionTwoTitle);
            InstantiateTitle(labelsRow, PatientBannerControlIds.SubsectionThreeTitle);
            InstantiateTitle(labelsRow, PatientBannerControlIds.SubsectionFourTitle);
            WebControl subsectionFive = InstantiateImageTitle(labelsRow, PatientBannerControlIds.SubsectionFive, PatientBannerControlIds.SubsectionFiveTitle, PatientBannerControlIds.AllergyIcon);
            InstantiateExpandImage(labelsRow);
            InstantiateAddressSummary(subsectionOne);
            InstantiateContactDetailsSummary(subsectionTwo);
            InstantiateAllergySummary(subsectionFive);

            parent.Controls.Add(labelsRow);
        }

        /// <summary>
        /// Instantiate expand/collapse image
        /// </summary>
        /// <param name="parent">Control to which to instantiate in</param>
        private static void InstantiateExpandImage(Control parent)
        {
            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.CssClass = PatientBannerCssClasses.ZoneTwoTitle;
            headerCell.Width = Unit.Pixel(20);
            headerCell.HorizontalAlign = HorizontalAlign.Right;

            Image expandImage = new Image();
            expandImage.ID = PatientBannerControlIds.ExpandImage;
            expandImage.GenerateEmptyAlternateText = true;

            headerCell.Controls.Add(expandImage);
            parent.Controls.Add(headerCell);
        }

        /// <summary>
        /// instantiate address summary
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateAddressSummary(Control parent)
        {
            Label addressSummary = new Label();

            addressSummary.ID = PatientBannerControlIds.AddressSummary;
            addressSummary.CssClass = PatientBannerCssClasses.ZoneTwoData;

            parent.Controls.Add(addressSummary);
        }

        /// <summary>
        /// instantiate contact details summary
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateContactDetailsSummary(Control parent)
        {
            Label contactDetailsSummary = new Label();

            contactDetailsSummary.ID = PatientBannerControlIds.ContactDetailsSummary;
            contactDetailsSummary.CssClass = PatientBannerCssClasses.ZoneTwoData;

            parent.Controls.Add(contactDetailsSummary);
        }

        /// <summary>
        /// Instantiate allergy summary
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateAllergySummary(Control parent)
        {           
            Label allergySummary = new Label();
            allergySummary.ID = PatientBannerControlIds.AllergySummary;
            allergySummary.CssClass = PatientBannerCssClasses.ZoneTwoData;
                        
            parent.Controls.Add(allergySummary);
        }

        /// <summary>
        /// Instantiate label
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        /// <param name="labelId">label id</param>
        /// <returns>Container control</returns>
        private static WebControl InstantiateTitle(Control parent, string labelId)
        {
            TextTableHeaderCell labelCell = new TextTableHeaderCell();

            labelCell.ID = labelId;
            labelCell.CssClass = PatientBannerCssClasses.ZoneTwoTitle;

            parent.Controls.Add(labelCell);

            return labelCell;
        }

        /// <summary>
        /// Instantiate label with Icon
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        /// <param name="cellId">Cell Id</param>
        /// <param name="labelId">label id</param>
        /// <param name="imageId">Image id</param>
        /// <returns>Container control</returns>
        private static WebControl InstantiateImageTitle(Control parent, string cellId, string labelId, string imageId)
        {
            Image icon = new Image();
            icon.ID = imageId;
            icon.Width = Unit.Pixel(16);
            icon.Height = Unit.Pixel(16);

            icon.Style.Add("float", "left");
            
            TextTableHeaderCell titleCell = new TextTableHeaderCell();
            titleCell.CssClass = PatientBannerCssClasses.ZoneTwoTitle;
            titleCell.ID = cellId;

            Label labelCell = new Label();
            labelCell.ID = labelId;            

            titleCell.Controls.Add(icon);
            titleCell.Controls.Add(labelCell);

            parent.Controls.Add(titleCell);

            return titleCell;
        }

        /// <summary>
        /// instantiate data items
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateItems(Control parent)
        {
            TableHeaderRow itemsRow = new TableHeaderRow();
            itemsRow.ID = PatientBannerControlIds.ZoneTwoNonPermanent;

            InstantiateSubsectionOne(itemsRow);
            InstantiateSubsectionTwo(itemsRow);
            InstantiateSubsectionThree(itemsRow);
            InstantiateSubsectionFour(itemsRow);
            InstantiateSubsectionFive(itemsRow);

            parent.Controls.Add(itemsRow);
        }

        /// <summary>
        /// instantiate sub section one 
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateSubsectionOne(Control parent)
        {
            TableCell itemCell = new TextTableCell();
            itemCell.VerticalAlign = VerticalAlign.Top;
            AddressLabel addressControl = new AddressLabel();
            itemCell.CssClass = PatientBannerCssClasses.ZoneTwoData;

            addressControl.ID = PatientBannerControlIds.Address;
            addressControl.AddressDisplayFormat = AddressDisplayFormat.InForm;
            itemCell.Controls.Add(addressControl);
                        
            parent.Controls.Add(itemCell);
        }

        /// <summary>
        /// instantiate subsection two
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateSubsectionTwo(Control parent)
        {
            TableCell itemCell = new TextTableCell();
            itemCell.VerticalAlign = VerticalAlign.Top;
            itemCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            ContactLabel contactLabel = new ContactLabel();

            contactLabel.LabelStyle = PatientBannerCssClasses.ZoneTwoLabel;
            contactLabel.ID = PatientBannerControlIds.ContactDetails;
            itemCell.Controls.Add(contactLabel);

            parent.Controls.Add(itemCell);
        }

        /// <summary>
        /// instantiate subsection three
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateSubsectionThree(Control parent)
        {
            TableCell container = new TextTableCell();
            container.CssClass = PatientBannerCssClasses.ZoneTwoData;
            container.ID = PatientBannerControlIds.SubsectionThree;
            container.VerticalAlign = VerticalAlign.Top;
            parent.Controls.Add(container);
        }

        /// <summary>
        /// instantiate subsection four
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateSubsectionFour(Control parent)
        {
            TableCell container = new TextTableCell();
            container.CssClass = PatientBannerCssClasses.ZoneTwoData;
            container.ID = PatientBannerControlIds.SubsectionFour;
            container.VerticalAlign = VerticalAlign.Top;
            parent.Controls.Add(container);
        }
        
        /// <summary>
        /// instantiate subsection five
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateSubsectionFive(Control parent)
        {
            TableCell itemCell = new TextTableCell();
            itemCell.ColumnSpan = 2;
            itemCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            itemCell.ID = PatientBannerControlIds.AllergyDetails;
            itemCell.VerticalAlign = VerticalAlign.Top;
            parent.Controls.Add(itemCell);
        }

        /// <summary>
        /// Instantiate the Links in Zone 2 subsections
        /// </summary>
        /// <param name="parent">control to instantiate in</param>
        private static void InstantiateLinks(Control parent)
        {
            TableRow linksRow = new TableRow();
            linksRow.ID = PatientBannerControlIds.ZoneTwoNonPermanentLinksRow;
            linksRow.Height = Unit.Pixel(1);
            linksRow.Style.Add(HtmlTextWriterStyle.Display, "none");
            linksRow.HorizontalAlign = HorizontalAlign.Right;

            TableCell subsectionOneLinkCell = new TableCell();
            subsectionOneLinkCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            LinkButton subsectionOneLink = new LinkButton();
            subsectionOneLink.Text = PatientBannerControl.Resources.ViewAllAddressLinkText;
            subsectionOneLink.ToolTip = PatientBannerControl.Resources.ViewAllAddressLinkText;
            subsectionOneLink.ID = PatientBannerControlIds.ViewAllAddresses;
            subsectionOneLinkCell.Controls.Add(subsectionOneLink);

            TableCell subsectionTwoLinkCell = new TableCell();
            subsectionTwoLinkCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            LinkButton subsectionTwoLink = new LinkButton();
            subsectionTwoLink.Text = PatientBannerControl.Resources.ViewAllContactDetailsLinkText;
            subsectionTwoLink.ToolTip = PatientBannerControl.Resources.ViewAllContactDetailsLinkText;
            subsectionTwoLink.ID = PatientBannerControlIds.ViewAllContactDetails;
            subsectionTwoLinkCell.Controls.Add(subsectionTwoLink);

            TableCell subsectionThreeLinkCell = new TableCell();
            subsectionThreeLinkCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            subsectionThreeLinkCell.Controls.Add(new LiteralControl(PatientBannerControl.Resources.NonBreakingSpace));

            TableCell subsectionFourLinkCell = new TableCell();
            subsectionFourLinkCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            subsectionFourLinkCell.Controls.Add(new LiteralControl(PatientBannerControl.Resources.NonBreakingSpace));

            TableCell subsectionFiveLinkCell = new TableCell();
            subsectionFiveLinkCell.CssClass = PatientBannerCssClasses.ZoneTwoData;
            subsectionFiveLinkCell.ColumnSpan = 2;
            LinkButton subsectionFiveLink = new LinkButton();
            subsectionFiveLink.Text = PatientBannerControl.Resources.ViewAllergyRecordLinkText;
            subsectionFiveLink.ToolTip = PatientBannerControl.Resources.ViewAllergyRecordLinkText;
            subsectionFiveLink.ID = PatientBannerControlIds.ViewAllergyRecord;
            subsectionFiveLinkCell.Controls.Add(subsectionFiveLink);

            linksRow.Cells.Add(subsectionOneLinkCell);
            linksRow.Cells.Add(subsectionTwoLinkCell);
            linksRow.Cells.Add(subsectionThreeLinkCell);
            linksRow.Cells.Add(subsectionFourLinkCell);
            linksRow.Cells.Add(subsectionFiveLinkCell);

            parent.Controls.Add(linksRow);
        }
        #endregion

        #region TextTableCell
        /// <summary>
        /// Table cell that implements ITextControl
        /// </summary>
        private class TextTableCell : TableCell, ITextControl
        {
            /// <summary>
            /// Renders the contents of the control to the specified writer
            /// </summary>
            /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
            protected override void RenderContents(HtmlTextWriter writer)
            {
                if (this.HasControls() || this.Text.Length > 0)
                {
                    base.RenderContents(writer);
                }
                else
                {
                    writer.Write(NonBreakingSpace);
                }
            }
        }
        #endregion

        #region TextTableHeaderCell
        /// <summary>
        /// Table header cell that implements ITextControl
        /// </summary>
        private class TextTableHeaderCell : TableHeaderCell, ITextControl
        {
            /// <summary>
            /// literal text held in the control
            /// </summary>
            public override string Text
            {
                get
                {
                    return (string)this.ViewState["Text"] ?? string.Empty;
                }

                set
                {
                    this.ViewState["Text"] = value;
                }
            }

            /// <summary>
            /// Renders the contents of the control to the specified writer
            /// </summary>
            /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
            protected override void RenderContents(HtmlTextWriter writer)
            {
                if (this.HasControls() || this.Text.Length > 0)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.WriteEncodedText(this.Text);
                    writer.RenderEndTag();
                    this.RenderChildren(writer);
                }
                else
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(NonBreakingSpace);
                    writer.RenderEndTag();
                }
            }
        }
        #endregion
    }
}
