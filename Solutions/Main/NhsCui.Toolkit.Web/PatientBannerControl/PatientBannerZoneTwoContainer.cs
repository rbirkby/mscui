//-----------------------------------------------------------------------
// <copyright file="PatientBannerZoneTwoContainer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Container for zone 2 of patient banner</summary>
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
    /// Container for zone 2 of patient banner
    /// </summary>
    [ToolboxItem(false)]
    internal class PatientBannerZoneTwoContainer : PatientBannerContainerBase
    {
        #region Constructors
        /// <summary>
        /// construct given owner control
        /// </summary>
        /// <param name="owner">owner patient banner control</param>
        public PatientBannerZoneTwoContainer(PatientBanner owner)
            : base(owner, HtmlTextWriterTag.Table, PatientBannerCssClasses.ZoneTwo)
        {
            this.Attributes["cellspacing"] = "0";                      
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// subsection one title
        /// </summary>
        public ITextControl SubsectionOneTitle
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.SubsectionOneTitle, true);
            }
        }

        /// <summary>
        /// subsection two title
        /// </summary>
        public ITextControl SubsectionTwoTitle
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.SubsectionTwoTitle, true);
            }
        }

        /// <summary>
        /// subsection three title
        /// </summary>
        public ITextControl SubsectionThreeTitle
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.SubsectionThreeTitle, true);
            }
        }

        /// <summary>
        /// subsection four title
        /// </summary>
        public ITextControl SubsectionFourTitle
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.SubsectionFourTitle, true);
            }
        }

        /// <summary>
        /// subsection three
        /// </summary>
        public WebControl SubsectionThree
        {
            get
            {
                return this.FindControl<WebControl>(PatientBannerControlIds.SubsectionThree, true);
            }
        }

        /// <summary>
        /// subsection four
        /// </summary>
        public WebControl SubsectionFour
        {
            get
            {
                return this.FindControl<WebControl>(PatientBannerControlIds.SubsectionFour, true);
            }
        }

        /// <summary>
        /// Image used for expand control
        /// </summary>
        public Image ExpandImage
        {
            get
            {
                return this.FindControl<Image>(PatientBannerControlIds.ExpandImage, true);
            }
        }

        /// <summary>
        /// address control
        /// </summary>
        public AddressLabel AddressControl
        {
            get
            {
                return this.FindControl<AddressLabel>(PatientBannerControlIds.Address, true);
            }
        }

        /// <summary>
        /// address summary control
        /// </summary>
        public ITextControl AddressSummaryControl
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.AddressSummary, true);
            }
        }

        /// <summary>
        /// contact details control
        /// </summary>
        public ContactLabel ContactDetailsControl
        {
            get
            {
                return this.FindControl<ContactLabel>(PatientBannerControlIds.ContactDetails, true);
            }
        }

        /// <summary>
        /// Contact details summary control
        /// </summary>
        public ITextControl ContactDetailsSummaryControl
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.ContactDetailsSummary, true);
            }
        }

        /// <summary>
        /// subsection five
        /// </summary>
        public ITextControl SubsectionFive
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.SubsectionFive, true);
            }
        }

        /// <summary>
        /// subsection five title
        /// </summary>
        public ITextControl SubsectionFiveTitle
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.SubsectionFiveTitle, true);
            }
        }

        /// <summary>
        /// Allergy details
        /// </summary>
        public WebControl AllergyDetails
        {
            get
            {
                return this.FindControl<WebControl>(PatientBannerControlIds.AllergyDetails, true);
            }
        }

        /// <summary>
        /// Allergy summary control
        /// </summary>
        public ITextControl AllergySummaryControl
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.AllergySummary, true);
            }
        }

        /// <summary>
        /// Allergy icon
        /// </summary>
        public Image AllergyIcon
        {
            get
            {
                return this.FindControl<Image>(PatientBannerControlIds.AllergyIcon, true);
            }
        }

        /// <summary>
        /// Permanent part of the zone
        /// </summary>
        public WebControl PermanentControl
        {
            get
            {
                return this.FindControl<WebControl>(PatientBannerControlIds.ZoneTwoPermanent, true);
            }
        }
 
        /// <summary>
        /// Non permanent part of the zone
        /// </summary>
        public WebControl NonPermanentControl
        {
            get
            {
                return this.FindControl<WebControl>(PatientBannerControlIds.ZoneTwoNonPermanent, true);
            }
        }

        /// <summary>
        /// Non permanent part of the zone
        /// </summary>
        public WebControl ZoneTwoNonPermanentLinksRow
        {
            get
            {
                return this.FindControl<WebControl>(PatientBannerControlIds.ZoneTwoNonPermanentLinksRow, true);
            }
        }

        /// <summary>
        /// View all addresses link
        /// </summary>
        public LinkButton ViewAllAddresses
        {
            get
            {
                return this.FindControl<LinkButton>(PatientBannerControlIds.ViewAllAddresses, true);
            }
        }

        /// <summary>
        /// view all phone numbers link
        /// </summary>
        public LinkButton ViewAllContactDetails
        {
            get
            {
                return this.FindControl<LinkButton>(PatientBannerControlIds.ViewAllContactDetails, true);
            }
        }

        /// <summary>
        /// view allergy record link
        /// </summary>
        public LinkButton ViewAllergyRecord
        {
            get
            {
                return this.FindControl<LinkButton>(PatientBannerControlIds.ViewAllergyRecord, true);
            }
        }
        #endregion
    }
}
