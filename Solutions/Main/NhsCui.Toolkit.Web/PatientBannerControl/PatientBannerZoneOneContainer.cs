//-----------------------------------------------------------------------
// <copyright file="PatientBannerZoneOneContainer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Container for zone1 of patient banner</summary>
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
    /// Container for zone1 of patient banner
    /// </summary>
    [ToolboxItem(false)]
    internal class PatientBannerZoneOneContainer : PatientBannerContainerBase
    {
        #region Constructors
        /// <summary>
        /// construct given owner control
        /// </summary>
        /// <param name="owner">owner patient banner control</param>
        public PatientBannerZoneOneContainer(PatientBanner owner)
            : base(owner, HtmlTextWriterTag.Div, PatientBannerCssClasses.ZoneOne)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// patient image control
        /// </summary>
        public Image PatientImage
        {
            get
            {
                return this.FindControl<Image>(PatientBannerControlIds.PatientImage, true);
            }
        }

        /// <summary>
        /// name control
        /// </summary>
        public NameLabel NameControl
        {
            get
            {
                return this.FindControl<NameLabel>(PatientBannerControlIds.Name, true);
            }
        }

        /// <summary>
        /// Date of birth label
        /// </summary>
        public Label DateOfBirthLabel
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.DateOfBirthLabel, true);
            }
        }

        /// <summary>
        /// Date of death label
        /// </summary>
        public Label DateOfDeathLabel
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.DateOfDeathLabel, true);
            }
        }

        /// <summary>
        /// date of birth control
        /// </summary>
        public DateLabel DateOfBirthControl
        {
            get
            {
                return this.FindControl<DateLabel>(PatientBannerControlIds.DateOfBirth, true);
            }
        }

        /// <summary>
        /// date of death control
        /// </summary>
        public DateLabel DateOfDeathControl
        {
            get
            {
                return this.FindControl<DateLabel>(PatientBannerControlIds.DateOfDeath, true);
            }
        }

        /// <summary>
        /// age control
        /// </summary>
        public TimeSpanLabel AgeAtDeathControl
        {
            get
            {
                return this.FindControl<TimeSpanLabel>(PatientBannerControlIds.AgeAtDeath, true);
            }
        }

        /// <summary>
        /// age control
        /// </summary>
        public Label AgeAtDeathLabel
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.AgeAtDeathLabel, true);
            }
        }

        /// <summary>
        /// age control
        /// </summary>
        public ITextControl Age
        {
            get
            {
                return this.FindControl<ITextControl>(PatientBannerControlIds.Age, true);
            }
        }

        /// <summary>
        /// Gender label
        /// </summary>
        public Label GenderLabel
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.GenderLabel, true);
            }
        }

        /// <summary>
        /// Gender control
        /// </summary>
        public GenderLabel GenderControl
        {
            get
            {
                return this.FindControl<GenderLabel>(PatientBannerControlIds.Gender, true);
            }
        }

        /// <summary>
        /// Identifier label
        /// </summary>
        public Label IdentifierLabel
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.IdentifierLabel, true);
            }
        }

        /// <summary>
        /// identifier control
        /// </summary>
        public IdentifierLabel IdentifierControl
        {
            get
            {
                return this.FindControl<IdentifierLabel>(PatientBannerControlIds.Identifier, true);
            }
        }

        /// <summary>
        /// Preferred name label
        /// </summary>
        public Label PreferredNameLabel
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.PreferredNameLabel, true);
            }
        }

        /// <summary>
        /// preferred name
        /// </summary>
        public Label PreferredNameControl
        {
            get
            {
                return this.FindControl<Label>(PatientBannerControlIds.PreferredName, true);
            }
        }

        /// <summary>
        /// Preferred name separator
        /// </summary>
        public LiteralControl PreferredNameSeparator
        {
            get
            {
                return this.FindControl<LiteralControl>(PatientBannerControlIds.PreferredNameSeparator, true);
            }
        }

        /// <summary>
        /// date of birth and date of death separator
        /// </summary>
        public LiteralControl BornDiedSeparator
        {
            get
            {
                return this.FindControl<LiteralControl>(PatientBannerControlIds.BornDiedSeparator, true);
            }
        }        

        /// <summary>
        /// Transparent icon shown before patient name of a deceased patient
        /// </summary>
        public Image DeceasedPatientTransparentIcon
        {
            get
            {
                return this.FindControl<Image>(PatientBannerControlIds.DeceasedPatientTransparentIcon, true);
            }
        }
       #endregion
    }
}
