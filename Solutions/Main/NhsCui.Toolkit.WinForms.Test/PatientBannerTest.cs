//-----------------------------------------------------------------------
// <copyright file="PatientBannerTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for patient banner control</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms.Test
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using NhsCui.Toolkit.DateAndTime;
    using System.Drawing;

    /// <summary>
    /// Unit tests for patient banner control
    /// </summary>
    [TestClass]
    public class PatientBannerTest
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientBannerTest()
        {
        }
        #endregion

        #region Patient Image Tests
        
        /// <summary>
        /// Test image displayed property
        /// </summary>
        [TestMethod]
        public void ImageDisplayedTest()
        {
            PatientBanner banner = new PatientBanner();
            const bool TestValue = false;

            banner.ImageDisplayed = TestValue;
            Assert.AreEqual(banner.ImageDisplayed, TestValue, "ImageDisplayed property no set as expected");
        }
        #endregion

        #region Patient Name Property Tests

        /// <summary>
        /// Test LastName property
        /// </summary>
        [TestMethod]
        public void LastNameTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.FamilyName, string.Empty, "Expected surname to initially be empty string");
            banner.FamilyName = "Evans";
            Assert.AreEqual(banner.FamilyName, "Evans", "Surname property not set as expected");
        }

        /// <summary>
        /// Test FirstName property
        /// </summary>
        [TestMethod]
        public void FirstNameTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.GivenName, string.Empty, "Expected FirstName to initially be empty string");
            banner.GivenName = "John";
            Assert.AreEqual(banner.GivenName, "John", "FirstName property not set as expected");
        }

        /// <summary>
        /// Test Title property
        /// </summary>
        [TestMethod]
        public void TitleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Title, string.Empty, "Expected Title to initially be empty string");
            banner.Title = "Mr";
            Assert.AreEqual(banner.Title, "Mr", "Title property not set as expected");
        }

        /// <summary>
        /// Test display value property
        /// </summary>
        [TestMethod]
        public void NameDisplayValueTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.NameDisplayValue, string.Empty, "Expected display value to initially be empty string");

            banner.FamilyName = "Evans";
            Assert.AreEqual(banner.NameDisplayValue, "EVANS", "Expected display value to be 'EVANS'");

            banner.GivenName = "John";
            Assert.AreEqual(banner.NameDisplayValue, "EVANS, John", "Expected display value to be 'EVANS John'");

            banner.Title = "Mr";
            Assert.AreEqual(banner.NameDisplayValue, "EVANS, John (Mr)", "Expected display value to be 'EVANS John (Mr)'");

            banner.FamilyName = string.Empty;
            Assert.AreEqual(banner.NameDisplayValue, "John (Mr)", "Expected display value to be 'John (Mr)'");

            banner.GivenName = string.Empty;
            Assert.AreEqual(banner.NameDisplayValue, string.Empty, "Expected display value to be ''");

            banner.GivenName = "John";
            banner.Title = string.Empty;
            Assert.AreEqual(banner.NameDisplayValue, "John", "Expected display value to be 'John'");
        }

        /// <summary>
        /// Test Preferred Name
        /// </summary>
        [TestMethod]
        public void PreferredNameTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.PreferredName, string.Empty, "Preferred name is expected to be empty initially");

            banner.PreferredName = "John";
            Assert.AreEqual(banner.PreferredName, "John", "Preferred name not set as expected");
        }

        /// <summary>
        /// Test preferred name label
        /// </summary>
        [TestMethod]
        public void PreferredNameLabelTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.PreferredNameLabelText, "Preferred name", "Preferred name label is expected to be empty initially");

            string preferredNameLabel = "Known as";
            banner.PreferredNameLabelText = preferredNameLabel;
            Assert.AreEqual(banner.PreferredNameLabelText, preferredNameLabel, "Preferred name label not set as expected");
        }
        #endregion

        #region Patient Identifier Tests
        /// <summary>
        ///A test for IdentifierType
        ///</summary>
        [TestMethod()]
        public void IdentifierTypeTest()
        {
            PatientBanner target = new PatientBanner();

            IdentifierType val = IdentifierType.NhsNumber;

            target.IdentifierType = val;
            Assert.AreEqual<IdentifierType>(val, target.IdentifierType, "IdentifierType was not set correctly.");

            val = IdentifierType.Other;

            target.IdentifierType = val;
            Assert.AreEqual<IdentifierType>(val, target.IdentifierType, "IdentifierType was not set correctly.");
        }

        /// <summary>
        /// Test that value throws an exception when an invalid identifier type is specified
        ///</summary>
        [TestMethod(), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdentifierTypeBadParameterTest()
        {
            PatientBanner target = new PatientBanner();

            target.IdentifierType = (IdentifierType)99;
        }

        /// <summary>
        /// TestValidNhsNumberFormattedInputString
        /// </summary>
        [TestMethod]
        public void ValidNhsNumberFormattedInputStringTest()
        {
            string testValue = "437 262 3623";
            PatientBanner target = new PatientBanner();
            target.IdentifierType = IdentifierType.NhsNumber;
            target.Identifier = testValue;

            Assert.AreEqual<string>(testValue, target.Identifier, string.Format(CultureInfo.InvariantCulture, "Actual value [{1}] does not match expected [{0}]", testValue, target.Identifier));
        }

        /// <summary>
        /// TestValidNhsNumberUnformattedInputString
        /// </summary>
        [TestMethod]
        public void ValidNhsNumberUnformattedInputStringTest()
        {
            string expectedFormattedValue = "437 262 3623";
            string testValue = "4372623623";

            PatientBanner target = new PatientBanner();
            target.IdentifierType = IdentifierType.NhsNumber;
            target.Identifier = testValue;

            Assert.AreNotEqual<string>(testValue, target.Identifier, string.Format(CultureInfo.InvariantCulture, "Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, target.Identifier));
            Assert.AreEqual<string>(expectedFormattedValue, target.Identifier, string.Format(CultureInfo.InvariantCulture, "Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, target.Identifier));
        }

        #endregion

        #region Date Of Birth / Death Tests
         /// <summary>
        /// Test DateOfBirth property
        /// </summary>
        [TestMethod]
        public void DateOfBirthTest()
        {
            PatientBanner banner = new PatientBanner();
            DateTime testValue = DateTime.Parse("05-Feb-1951", CultureInfo.InvariantCulture);
            Assert.AreEqual(banner.DateOfBirth, DateTime.MinValue, "Expected DateOfBirth to initially be DateTime.MinValue");
            banner.DateOfBirth = testValue;
            Assert.AreEqual<DateTime>(banner.DateOfBirth, testValue, "DateOfBirth property not set as expected");
        }

        /// <summary>
        /// Test DateOfDeath property
        /// </summary>
        [TestMethod]
        public void DateOfDeathTest()
        {
            PatientBanner banner = new PatientBanner();
            DateTime testValue = DateTime.Parse("05-Feb-2005", CultureInfo.InvariantCulture);
            Assert.AreEqual(banner.DateOfDeath, DateTime.MinValue, "Expected DateOfDeath to initially be DateTime.MinValue");
            banner.DateOfDeath = testValue;
            Assert.AreEqual<DateTime>(banner.DateOfDeath, testValue, "DateOfDeath property not set as expected");
        }
        #endregion

        #region Patient Gender Tests
        /// <summary>
        /// Test Gender property
        /// </summary>
        [TestMethod]
        public void GenderTest()
        {
            PatientBanner banner = new PatientBanner();
            PatientGender testValue = PatientGender.Female;
            Assert.AreEqual(banner.Gender, PatientGender.NotKnown, "Expected Gender to initially be NotSpecified");
            banner.Gender = testValue;
            Assert.AreEqual<PatientGender>(banner.Gender, testValue, "Gender property not set as expected");
        }
        #endregion

        #region Contact Details Property Tests

        /// <summary>
        /// HomePhoneNumber property test
        /// </summary>
        [TestMethod]
        public void HomePhoneNumberTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.HomePhoneNumber, string.Empty, "Expected HomePhoneNumber to initially be empty string");
            banner.HomePhoneNumber = "(01332) 5678900";
            Assert.AreEqual(banner.HomePhoneNumber, "(01332) 5678900", "HomePhoneNumber property not set as expected");
        }

        /// <summary>
        /// WorkPhoneNumber property test
        /// </summary>
        [TestMethod]
        public void WorkPhoneNumberTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.WorkPhoneNumber, string.Empty, "Expected WorkPhoneNumber to initially be empty string");
            banner.WorkPhoneNumber = "(02345) 1232455";
            Assert.AreEqual(banner.WorkPhoneNumber, "(02345) 1232455", "WorkPhoneNumber property not set as expected");
        }

        /// <summary>
        /// MobilePhoneNumber property test
        /// </summary>
        [TestMethod]
        public void MobilePhoneNumberTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.MobilePhoneNumber, string.Empty, "Expected MobilePhoneNumber to initially be empty string");
            banner.MobilePhoneNumber = "(06678) 5787446";
            Assert.AreEqual(banner.MobilePhoneNumber, "(06678) 5787446", "MobilePhoneNumber property not set as expected");
        }

        /// <summary>
        /// EmailAddress property test
        /// </summary>
        [TestMethod]
        public void EmailAddressTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.EmailAddress, string.Empty, "Expected EmailAddress to initially be empty string");
            banner.EmailAddress = "jane.evans@yahoo.com";
            Assert.AreEqual(banner.EmailAddress, "jane.evans@yahoo.com", "EmailAddress property not set as expected");
        }

        #endregion

        #region Address Property Tests

        /// <summary>
        /// Test for Address1 property
        /// </summary>
        [TestMethod]
        public void Address1Test()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Address1, string.Empty, "Expected Address1 to initially be empty string");
            banner.Address1 = "1 Acacia Avenue";
            Assert.AreEqual(banner.Address1, "1 Acacia Avenue", "Address1 property not set as expected");
        }

        /// <summary>
        /// Test for Address2 property
        /// </summary>
        [TestMethod]
        public void Address2Test()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Address2, string.Empty, "Expected Address2 to initially be empty string");
            banner.Address2 = "Little Amwell";
            Assert.AreEqual(banner.Address2, "Little Amwell", "Address2 property not set as expected");
        }

        /// <summary>
        /// Test for Address3 property
        /// </summary>
        [TestMethod]
        public void Address3Test()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Address3, string.Empty, "Expected Address3 to initially be empty string");
            banner.Address3 = "Great Amwell";
            Assert.AreEqual(banner.Address3, "Great Amwell", "Address3 property not set as expected");
        }

        /// <summary>
        /// Test for Town property
        /// </summary>
        [TestMethod]
        public void TownTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Town, string.Empty, "Expected Town to initially be empty string");
            banner.Town = "Ware";
            Assert.AreEqual(banner.Town, "Ware", "Town property not set as expected");
        }

        /// <summary>
        /// Test for County property
        /// </summary>
        [TestMethod]
        public void CountyTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.County, string.Empty, "Expected County to initially be empty string");
            banner.County = "Herfordshire";
            Assert.AreEqual(banner.County, "Herfordshire", "County property not set as expected");
        }

        /// <summary>
        /// Test for PostCode property
        /// </summary>
        [TestMethod]
        public void PostCodeTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Postcode, string.Empty, "Expected PostCode to initially be empty string");
            banner.Postcode = "WR1 1RG";
            Assert.AreEqual(banner.Postcode, "WR1 1RG", "PostCode property not set as expected");
        }

        /// <summary>
        /// Test for Country property
        /// </summary>
        [TestMethod]
        public void CountryTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.Country, string.Empty, "Expected Country to initially be empty string");
            banner.Country = "UK";
            Assert.AreEqual(banner.Country, "UK", "Country property not set as expected");
        }

        #endregion

        #region Label Text Tests
        /// <summary>
        /// Test IdentifierLabelText property
        /// </summary>
        [TestMethod]
        public void IdentifierLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.IdentifierLabelText = testValue;
            Assert.AreEqual<string>(banner.IdentifierLabelText, testValue, "IdentifierLabelText property not set as expected");
        }

        /// <summary>
        /// Test GenderLabelText property
        /// </summary>
        [TestMethod]
        public void GenderLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.GenderLabelText = testValue;
            Assert.AreEqual<string>(banner.GenderLabelText, testValue, "GenderLabelText property not set as expected");
        }

        /// <summary>
        /// Test BornLabelText property
        /// </summary>
        [TestMethod]
        public void BornLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.DateOfBirthLabelText = testValue;
            Assert.AreEqual<string>(banner.DateOfBirthLabelText, testValue, "BornLabelText property not set as expected");
        }

        /// <summary>
        /// Test ContactDetailsLabelText property
        /// </summary>
        [TestMethod]
        public void ContactDetailsLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.SubsectionTwoTitle = testValue;
            Assert.AreEqual<string>(banner.SubsectionTwoTitle, testValue, "ContactDetailsLabelText property not set as expected");
        }

        /// <summary>
        /// Test AddressLabelText property
        /// </summary>
        [TestMethod]
        public void AddressLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.SubsectionOneTitle = testValue;
            Assert.AreEqual<string>(banner.SubsectionOneTitle, testValue, "AddressLabelText property not set as expected");
        }

        /// <summary>
        /// Test HomePhoneLabelText property
        /// </summary>
        [TestMethod]
        public void HomePhoneLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.HomePhoneLabelText = testValue;
            Assert.AreEqual<string>(banner.HomePhoneLabelText, testValue, "HomePhoneLabelText property not set as expected");
        }

        /// <summary>
        /// Test WorkPhoneLabelText property
        /// </summary>
        [TestMethod]
        public void WorkPhoneLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.WorkPhoneLabelText = testValue;
            Assert.AreEqual<string>(banner.WorkPhoneLabelText, testValue, "WorkPhoneLabelText property not set as expected");
        }

        /// <summary>
        /// Test MobilePhoneLabelText property
        /// </summary>
        [TestMethod]
        public void MobilePhoneLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.MobilePhoneLabelText = testValue;
            Assert.AreEqual<string>(banner.MobilePhoneLabelText, testValue, "MobilePhoneLabelText property not set as expected");
        }

        /// <summary>
        /// Test EmailLabelText property
        /// </summary>
        [TestMethod]
        public void EmailLabelTextTest()
        {
            PatientBanner banner = new PatientBanner();
            string testValue = "&&*&&*&*";
            banner.EmailLabelText = testValue;
            Assert.AreEqual<string>(banner.EmailLabelText, testValue, "EmailLabelText property not set as expected");
        }

        #endregion

        #region Style Tests
        /// <summary>
        /// Test for ZoneOneBackColor property
        /// </summary>
        [TestMethod]
        public void ZoneOneBackColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneOneBackColor, SystemColors.Control, "Expected ZoneOneBackColor to initially be empty string");
            banner.ZoneOneBackColor = Color.Beige;
            Assert.AreEqual<Color>(banner.ZoneOneBackColor, Color.Beige, "ZoneOneBackColor property not set as expected");
        }

        /// <summary>
        /// Test for BorderColor property
        /// </summary>
        [TestMethod]
        public void BorderColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.BorderColor, SystemColors.ControlDarkDark, "Expected BorderColor to initially be 'SystemColors.ControlDarkDark'");
            banner.BorderColor = Color.Beige;
            Assert.AreEqual<Color>(banner.BorderColor, Color.Beige, "BorderColor property not set as expected");
        }

        /// <summary>
        /// Test for ZoneOneDataFont property
        /// </summary>
        [TestMethod]
        public void ZoneOneDataFontTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneOneDataFont, null, "Expected ZoneOneDataFont to initially be empty string");

            Font font = new Font("Verdana", 8, FontStyle.Regular);
            banner.ZoneOneDataFont = font;
            Assert.AreEqual<Font>(banner.ZoneOneDataFont, font, "ZoneOneDataFont property not set as expected");
        }

        /// <summary>
        /// Test for ZoneOneLabelFont
        /// </summary>
        [TestMethod]
        public void ZoneOneLabelFontTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneOneLabelFont, null, "Expected ZoneOneLabelFont to initially be empty string");

            Font font = new Font("Verdana", 8, FontStyle.Regular);
            banner.ZoneOneLabelFont = font;
            Assert.AreEqual<Font>(banner.ZoneOneLabelFont, font, "ZoneOneLabelFont property not set as expected");
        }
                
        /// <summary>
        /// Test for ZoneTwoBackColor
        /// </summary>
        [TestMethod]
        public void ZoneTwoBackColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoBackColor, SystemColors.Control, "Expected ZoneTwoBackColor to initially be empty");

            banner.ZoneTwoBackColor = Color.Gray;
            Assert.AreEqual(banner.ZoneTwoBackColor, Color.Gray, "ZoneTwoBackColor not set as expected");
        }        

        /// <summary>
        /// Test for ZoneTwoDataFont
        /// </summary>
        [TestMethod]
        public void ZoneTwoDataFontTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoDataFont, null, "Expected ZoneTwoDataFont to initially be empty string");

            Font font = new Font("Verdana", 8, FontStyle.Regular);
            banner.ZoneTwoDataFont = font;
            Assert.AreEqual<Font>(banner.ZoneTwoDataFont, font, "ZoneTwoDataFont property not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoLabelFont
        /// </summary>
        [TestMethod]
        public void ZoneTwoLabelFontTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoLabelFont, null, "Expected ZoneTwoLabelFont to initially be empty string");

            Font font = new Font("Verdana", 8, FontStyle.Regular);
            banner.ZoneTwoLabelFont = font;
            Assert.AreEqual<Font>(banner.ZoneTwoLabelFont, font, "ZoneTwoLabelFont property not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoHoverBorderColor
        /// </summary>
        [TestMethod]
        public void ZoneTwoHoverBorderColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoHoverBorderColor, SystemColors.MenuHighlight, "Expected ZoneTwoHoverBorderColor to initially be empty");

            banner.ZoneTwoHoverBorderColor = Color.Gray;
            Assert.AreEqual(banner.ZoneTwoHoverBorderColor, Color.Gray, "ZoneTwoHoverBorderColor not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoTitleBackColor
        /// </summary>
        [TestMethod]
        public void ZoneTwoTitleBackColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoTitleBackColor, SystemColors.Control, "Expected ZoneTwoTitleBackColor to initially be empty");

            banner.ZoneTwoTitleBackColor = Color.Gray;
            Assert.AreEqual(banner.ZoneTwoTitleBackColor, Color.Gray, "ZoneTwoTitleBackColor not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoTitleFont
        /// </summary>
        [TestMethod]
        public void ZoneTwoTitleFontTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoLabelFont, null, "Expected ZoneTwoLabelFont to initially be empty string");

            Font font = new Font("Verdana", 8, FontStyle.Regular);
            banner.ZoneTwoLabelFont = font;
            Assert.AreEqual<Font>(banner.ZoneTwoLabelFont, font, "ZoneTwoLabelFont property not set as expected");
        }

        /// <summary>
        /// Test for ActivePatientBackColor
        /// </summary>
        [TestMethod]
        public void ActivePatientBackColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ActivePatientBackColor, SystemColors.Control, "Expected ActivePatientBackColor to initially be empty");

            banner.ActivePatientBackColor = Color.Gray;
            Assert.AreEqual(banner.ActivePatientBackColor, Color.Gray, "ActivePatientBackColor not set as expected");
        }

        /// <summary>
        /// Test for DeadPatientBackColor
        /// </summary>
        [TestMethod]
        public void DeadPatientBackColorTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.DeadPatientBackColor, SystemColors.ButtonShadow, "Expected DeadPatientBackColor to initially be empty");

            banner.DeadPatientBackColor = Color.Gray;
            Assert.AreEqual(banner.DeadPatientBackColor, Color.Gray, "DeadPatientBackColor not set as expected");
        }
        #endregion

        #region Tooltip Tests
        
        /// <summary>
        /// Test for ZoneTwoTooltip property
        /// </summary>
        [TestMethod]
        public void ZoneTwoTooltipTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoTooltip, "Zone Two Tooltip", "Expected ZoneTwoTooltip to initially be empty string");
            string testValue = "tooltip";
            banner.ZoneTwoTooltip = testValue;
            Assert.AreEqual<string>(banner.ZoneTwoTooltip, testValue, "ZoneTwoTooltip property not set as expected");
        }

        /// <summary>
        /// Gender label tooltip test
        /// </summary>
        [TestMethod]
        public void GenderLabelTooltipTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.GenderLabelTooltip, "Gender: a person's current Gender. This may be different from a person's Sex which is a person's Gender defined at the point of birth registration.", "Gender label tooltip us expected to be empty initially");

            string genderLabelTooltip = "Patient gender";
            banner.GenderLabelTooltip = genderLabelTooltip;
            Assert.AreEqual(banner.GenderLabelTooltip, genderLabelTooltip, "Gender label tooltip not set as expected");
        }

        /// <summary>
        /// Gender value tooltip test
        /// </summary>
        [TestMethod]
        public void GenderValueTooltipTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.GenderValueTooltip, "Click here to open this section of the record", "Gender value tooltip us expected to be empty initially");

            string genderValueTooltip = "Patient gender";
            banner.GenderValueTooltip = genderValueTooltip;
            Assert.AreEqual(banner.GenderValueTooltip, genderValueTooltip, "Gender value tooltip not set as expected");
        }

        /// <summary>
        /// Identifier label tooltip test
        /// </summary>
        [TestMethod]
        public void IdentifierLabelTooltipTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.IdentifierLabelTooltip, "A ten-digit number used to identify a person uniquely within the NHS in England and Wales", "Identifier label tooltip expected to be empty initially");

            string identifierLabelTooltip = "NHS Number";
            banner.IdentifierLabelTooltip = identifierLabelTooltip;
            Assert.AreEqual(banner.IdentifierLabelTooltip, identifierLabelTooltip, "Identifier label tooltip not set as expected");
        }

        /// <summary>
        /// Identifier tooltip
        /// </summary>
        [TestMethod]
        public void IdentifierTooltipTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.IdentifierTooltip, "Click here to open this section of the record", "Identifier tooltip expected to be empty initially");

            string identifierTooltip = "NHS Number";
            banner.IdentifierTooltip = identifierTooltip;
            Assert.AreEqual(banner.IdentifierTooltip, identifierTooltip, "Identifier tooltip not set as expected");
        }
        #endregion

        #region Zone Two Subsection

        /// <summary>
        /// Subsection one width test
        /// </summary>
        [TestMethod]
        public void SubsectionOneWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionOneWidth, 206, "Subsection one initial width not set as expected");

            banner.SubsectionOneWidth = 200;
            Assert.AreEqual(banner.SubsectionOneWidth, 200, "Subsection one width not set as expected");
        }

        /// <summary>
        /// Subsection two width test
        /// </summary>
        [TestMethod]
        public void SubsectionTwoWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionTwoWidth, 215, "Subsection two initial width not set as expected");

            banner.SubsectionTwoWidth = 111;
            Assert.AreEqual(banner.SubsectionTwoWidth, 111, "Subsection two width not set as expected");
        }

        /// <summary>
        /// Subsection three width test
        /// </summary>
        [TestMethod]
        public void SubsectionThreeWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionThreeWidth, 99, "Subsection two initial width not set as expected");

            banner.SubsectionThreeWidth = 111;
            Assert.AreEqual(banner.SubsectionThreeWidth, 111, "Subsection three width not set as expected");
        }

        /// <summary>
        /// Subsection four width test
        /// </summary>
        [TestMethod]
        public void SubsectionFourWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionFourWidth, 99, "Subsection four initial width not set as expected");

            banner.SubsectionFourWidth = 111;
            Assert.AreEqual(banner.SubsectionFourWidth, 111, "Subsection four width not set as expected");
        }

        /// <summary>
        /// Subsection five width test
        /// </summary>
        [TestMethod]
        public void SubsectionFiveWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionFiveWidth, 187, "Subsection five initial width not set as expected");

            banner.SubsectionFiveWidth = 111;
            Assert.AreEqual(banner.SubsectionFiveWidth, 111, "Subsection five width not set as expected");
        }
                
        /// <summary>
        /// Allergy information test
        /// </summary>
        [TestMethod]
        public void AllergyInformationTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.AllergyInformation, AllergyInformation.Unavailable, "Allergy Information initial value not set as expected");

            banner.AllergyInformation = AllergyInformation.Present;
            Assert.AreEqual(banner.AllergyInformation, AllergyInformation.Present, "Allergy Information not set as expected");
        }
        #endregion
    }
}
