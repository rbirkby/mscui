//-----------------------------------------------------------------------
// <copyright file="PatientBannerControlIds.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Well known control ids used by the patient banner</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Well known control ids used by the patient banner
    /// </summary>
    internal static class PatientBannerControlIds
    {
        /// <summary>
        /// id of zone 1 control
        /// </summary>
        public const string ZoneOne = "zoneOne";

        /// <summary>
        /// id of zone 2 control
        /// </summary>
        public const string ZoneTwo = "zoneTwo";

        /// <summary>
        /// id of part of zone 2 that is permanently displayed
        /// </summary>
        public const string ZoneTwoPermanent = "zoneTwoPermanent";

        /// <summary>
        /// id of part of zone 2 that is not permanently displayed
        /// </summary>
        public const string ZoneTwoNonPermanent = "zoneTwoNonPermanent";

        /// <summary>
        /// id of the patient image control
        /// </summary>
        public const string PatientImage = "patientImage";

        /// <summary>
        /// id for the transparent icon shown before deceased patient
        /// </summary>
        public const string DeceasedPatientTransparentIcon = "deceasedPatientTransparentIcon";

        /// <summary>
        /// id of the patient name control
        /// </summary>
        public const string Name = "patientName";

        /// <summary>
        /// id of patient identifier (nhs number) control
        /// </summary>
        public const string Identifier = "identifier";

        /// <summary>
        /// id of patient gender control
        /// </summary>
        public const string Gender = "gender";

        /// <summary>
        /// id of patient date of birth control
        /// </summary>
        public const string DateOfBirth = "dob";

        /// <summary>
        /// id of patient date of death control
        /// </summary>
        public const string DateOfDeath = "dod";

        /// <summary>
        /// id of patient age control
        /// </summary>
        public const string Age = "age";

        /// <summary>
        /// id of image used as a button to expand / collapse detail panel
        /// </summary>
        public const string ExpandImage = "expandImage";

        /// <summary>
        /// id of patient contact details control
        /// </summary>
        public const string ContactDetails = "contactDetails";

        /// <summary>
        /// id of patient address control
        /// </summary>
        public const string Address = "address";

        /// <summary>
        /// id of patient address summary control
        /// </summary>
        public const string AddressSummary = "addressSummary";

        /// <summary>
        /// id of gender label
        /// </summary>
        public const string GenderLabel = "genderLabel";

        /// <summary>
        /// id of date of birth label
        /// </summary>
        public const string DateOfBirthLabel = "dobLabel";

        /// <summary>
        /// id of date of death label
        /// </summary>
        public const string DateOfDeathLabel = "dodLabel";

        /// <summary>
        /// id of identifier label
        /// </summary>
        public const string IdentifierLabel = "identifierLabel";

        /// <summary>
        /// id of subsection one title
        /// </summary>
        public const string SubsectionOneTitle = "subsectionOneTitle";

        /// <summary>
        /// id of subsection two title
        /// </summary>
        public const string SubsectionTwoTitle = "subsectionTwoTitle";

        /// <summary>
        /// id of subsection three title
        /// </summary>
        public const string SubsectionThreeTitle = "subsectionThreeTitle";

        /// <summary>
        /// id of subsection four title
        /// </summary>
        public const string SubsectionFourTitle = "subsectionFourTitle";

        /// <summary>
        /// id of subsection three
        /// </summary>
        public const string SubsectionThree = "subsectionThree";

        /// <summary>
        /// id of subsection four
        /// </summary>
        public const string SubsectionFour = "subsectionFour";

        /// <summary>
        /// id of subsection five
        /// </summary>
        public const string SubsectionFive = "subsectionFive";

        /// <summary>
        /// id of the alllergy details table cell
        /// </summary>
        public const string AllergyDetails = "allergyDetails";

        /// <summary>
        /// id of the alllergy details panel which actually holds the data
        /// </summary>
        public const string AllergyDetailsPanel = "allergyDetailsPanel";
        
        /// <summary>
        /// id of subsection five title
        /// </summary>
        public const string SubsectionFiveTitle = "subsectionFiveTitle";

        /// <summary>
        /// id of the allergy icon
        /// </summary>
        public const string AllergyIcon = "allergyIcon";

        /// <summary>
        /// id of the allergy summary
        /// </summary>
        public const string AllergySummary = "allergySummary";

        /// <summary>
        /// id of preferred name label
        /// </summary>
        public const string PreferredNameLabel = "preferredNameLabel";

        /// <summary>
        /// id of preferred name value
        /// </summary>
        public const string PreferredName = "preferredName";

        /// <summary>
        /// id of preferred name separator (separates patient name from preferred name)
        /// </summary>
        public const string PreferredNameSeparator = "preferredNameSeparator";

        /// <summary>
        /// id of Born died separator (separates death of birth and date of death)
        /// </summary>
        public const string BornDiedSeparator = "bornDiedSeparator";

        /// <summary>
        /// id of View all addresses link
        /// </summary>
        public const string ViewAllAddresses = "viewAllAddresses";

        /// <summary>
        /// id of view all phone numbers link
        /// </summary>
        public const string ViewAllContactDetails = "viewAllContactDetails";

        /// <summary>
        /// id of view allergy record link
        /// </summary>
        public const string ViewAllergyRecord = "viewAllergyRecord";

        /// <summary>
        /// id of patient name table cell
        /// </summary>
        public const string ZoneOnePatientNameCell = "patientNameCell";

        /// <summary>
        /// Patient data cell
        /// </summary>
        public const string ZoneOnePatientDataCell = "patientDataCell";

        /// <summary>
        /// id of DoB and DoD table cell
        /// </summary>
        public const string ZoneOneDOBCell = "dobCell";

        /// <summary>
        /// id of patient gender table cell
        /// </summary>
        public const string ZoneOneGenderCell = "genderCell";

        /// <summary>
        /// id of Identifier table cell
        /// </summary>
        public const string ZoneOneIdentifierCell = "identifierCell";

        /// <summary>
        /// id of Patient image table cell
        /// </summary>
        public const string PatientImageCell = "patientImageCell";

        /// <summary>
        /// id of the links row in Zone Two
        /// </summary>
        public const string ZoneTwoNonPermanentLinksRow = "zoneTwoNonPermanentLinksRow";

        /// <summary>
        /// id of the contact details summary label
        /// </summary>
        public const string ContactDetailsSummary = "contactDetailsSummary";

        /// <summary>
        /// id of Age at death Label
        /// </summary>
        public const string AgeAtDeathLabel = "ageAtDeathLabel";

        /// <summary>
        /// id of Age at death control
        /// </summary>
        public const string AgeAtDeath = "ageAtDeath";

        /// <summary>
        /// id of the client state control for the patient banner
        /// </summary>
        public const string ClientState = "clientState";

        /// <summary>
        /// id of stylesheet link to include default stylesheet
        /// </summary>
        public const string StyleSheetLink = "nhscui_pb_css";
    }
}
