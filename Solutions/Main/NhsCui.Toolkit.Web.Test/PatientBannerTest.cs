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

namespace NhsCui.Toolkit.Web.Test
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using NhsCui.Toolkit.DateAndTime;
    using System.Web.UI.WebControls;

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
        /// Test display value property
        /// </summary>
        [TestMethod]
        public void PatientImageTest()
        {
            PatientBanner banner = new PatientBanner();
            const string TestImageUrl = "images/logo.gif";

            banner.PatientImage = TestImageUrl;
            Assert.AreEqual(banner.PatientImage, TestImageUrl, "PatientImage property no set as expected");
        }

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
            Assert.AreEqual(banner.FamilyName, "FamilyName", "Expected surname to initially be FamilyName");
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
            Assert.AreEqual(banner.GivenName, "GivenName", "Expected FirstName to initially be GivenName");
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
            Assert.AreEqual(banner.Title, "Title", "Expected Title to initially be Title");
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

            Assert.AreEqual(banner.NameDisplayValue, "FAMILYNAME, GivenName (Title)", "Expected display value to initially be FAMILYNAME, GivenName (Title)");

            banner.FamilyName = "Evans";
            Assert.AreEqual(banner.NameDisplayValue, "EVANS, GivenName (Title)", "Expected display value to be 'EVANS'");

            banner.GivenName = "John";
            Assert.AreEqual(banner.NameDisplayValue, "EVANS, John (Title)", "Expected display value to be 'EVANS John'");

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

            Assert.AreEqual<string>(testValue, target.Identifier, string.Format(CultureInfo.CurrentCulture, "Actual value [{1}] does not match expected [{0}]", testValue, target.Identifier));
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

            Assert.AreNotEqual<string>(testValue, target.Identifier, string.Format(CultureInfo.CurrentCulture, "Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, target.Identifier));
            Assert.AreEqual<string>(expectedFormattedValue, target.Identifier, string.Format(CultureInfo.CurrentCulture, "Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, target.Identifier));
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
            banner.HomePhoneNumber = "01332 5678900";
            Assert.AreEqual(banner.HomePhoneNumber, "01332 5678900", "HomePhoneNumber property not set as expected");
        }

        /// <summary>
        /// WorkPhoneNumber property test
        /// </summary>
        [TestMethod]
        public void WorkPhoneNumberTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.WorkPhoneNumber, string.Empty, "Expected WorkPhoneNumber to initially be empty string");
            banner.WorkPhoneNumber = "02345 1232455";
            Assert.AreEqual(banner.WorkPhoneNumber, "02345 1232455", "WorkPhoneNumber property not set as expected");
        }

        /// <summary>
        /// MobilePhoneNumber property test
        /// </summary>
        [TestMethod]
        public void MobilePhoneNumberTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.MobilePhoneNumber, string.Empty, "Expected MobilePhoneNumber to initially be empty string");
            banner.MobilePhoneNumber = "06678 5787446";
            Assert.AreEqual(banner.MobilePhoneNumber, "06678 5787446", "MobilePhoneNumber property not set as expected");
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

        #region DropDownImage Tests
        /// <summary>
        /// Test for DropDownImage property
        /// </summary>
        [TestMethod]
        public void DropDownImageTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.DropDownImage, string.Empty, "Expected DropDownImage to initially be empty string");
            string testValue = "logo.gif";
            banner.DropDownImage = testValue;
            Assert.AreEqual<string>(banner.DropDownImage, testValue, "DropDownImage property not set as expected");
        }

        /// <summary>
        /// Test for CollapseImage property
        /// </summary>
        [TestMethod]
        public void CollapseImageTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.CollapseImage, string.Empty, "Expected CollapseImage to initially be empty string");
            string testValue = "logo.gif";
            banner.CollapseImage = testValue;
            Assert.AreEqual<string>(banner.CollapseImage, testValue, "CollapseImage property not set as expected");
        }
        #endregion

        #region Style Tests
        /// <summary>
        /// Test for ZoneOneStyle property
        /// </summary>
        [TestMethod]
        public void ZoneOneStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneOneStyle, string.Empty, "Expected ZoneOneStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneOneStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneOneStyle, testValue, "ZoneOneStyle property not set as expected");
        }

        /// <summary>
        /// Test for ZoneOneLabelStyle property
        /// </summary>
        [TestMethod]
        public void ZoneOneLabelStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneOneLabelStyle, string.Empty, "Expected ZoneOneLabelStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneOneLabelStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneOneLabelStyle, testValue, "ZoneOneLabelStyle property not set as expected");
        }

        /// <summary>
        /// Test for ZoneOneDataStyle property
        /// </summary>
        [TestMethod]
        public void ZoneOneDataStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneOneDataStyle, string.Empty, "Expected ZoneOneDataStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneOneDataStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneOneDataStyle, testValue, "ZoneOneDataStyle property not set as expected");
        }

        /// <summary>
        /// Test for PatientNameStyle property
        /// </summary>
        [TestMethod]
        public void PatientNameStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.PatientNameStyle, string.Empty, "Expected PatientNameStyle to initially be empty string");
            string testValue = "css_class";
            banner.PatientNameStyle = testValue;
            Assert.AreEqual<string>(banner.PatientNameStyle, testValue, "PatientNameStyle property not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoTitleStyle property
        /// </summary>
        [TestMethod]
        public void ZoneTwoTitleStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoTitleStyle, string.Empty, "Expected ZoneTwoTitleStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneTwoTitleStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneTwoTitleStyle, testValue, "ZoneTwoTitleStyle property not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoStyle property
        /// </summary>
        [TestMethod]
        public void ZoneTwoStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoStyle, string.Empty, "Expected ZoneTwoStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneTwoStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneTwoStyle, testValue, "ZoneTwoStyle property not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoDataStyle property
        /// </summary>
        [TestMethod]
        public void ZoneTwoDataStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoDataStyle, string.Empty, "Expected ZoneTwoDataStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneTwoDataStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneTwoDataStyle, testValue, "ZoneTwoDataStyle property not set as expected");
        }

        /// <summary>
        /// Test for ZoneTwoLabelStyle property
        /// </summary>
        [TestMethod]
        public void ZoneTwoLabelStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoLabelStyle, string.Empty, "Expected ZoneTwoLabelStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneTwoLabelStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneTwoLabelStyle, testValue, "ZoneTwoLabelStyle property not set as expected");
        }

        /// <summary>
        /// Test for ActivePatientStyle property
        /// </summary>
        [TestMethod]
        public void ActivePatientStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ActivePatientStyle, string.Empty, "Expected ActivePatientStyle to initially be empty string");
            string testValue = "css_class";
            banner.ActivePatientStyle = testValue;
            Assert.AreEqual<string>(banner.ActivePatientStyle, testValue, "ActivePatientStyle property not set as expected");
        }

        /// <summary>
        /// Test for DeadPatientStyle property
        /// </summary>
        [TestMethod]
        public void DeadPatientStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.DeadPatientStyle, string.Empty, "Expected DeadPatientStyle to initially be empty string");
            string testValue = "css_class";
            banner.DeadPatientStyle = testValue;
            Assert.AreEqual<string>(banner.DeadPatientStyle, testValue, "DeadPatientStyle property not set as expected");
        }
                
        /// <summary>
        /// Test for ZoneTwoHoverStyle property
        /// </summary>
        [TestMethod]
        public void ZoneTwoHoverStyleTest()
        {
            PatientBanner banner = new PatientBanner();
            Assert.AreEqual(banner.ZoneTwoHoverStyle, string.Empty, "Expected ZoneTwoHoverStyle to initially be empty string");
            string testValue = "css_class";
            banner.ZoneTwoHoverStyle = testValue;
            Assert.AreEqual<string>(banner.ZoneTwoHoverStyle, testValue, "ZoneTwoHoverStyle property not set as expected");
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
            Assert.AreEqual(banner.ZoneTwoTooltip, "Click to expand or collapse", "Expected ZoneTwoTooltip to initially be empty string");
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

            Assert.AreEqual(banner.SubsectionOneWidth, Unit.Percentage(25), "Subsection one initial width not set as expected");

            banner.SubsectionOneWidth = Unit.Percentage(20);
            Assert.AreEqual(banner.SubsectionOneWidth, Unit.Percentage(20), "Subsection one width not set as expected");
        }

        /// <summary>
        /// Subsection two width test
        /// </summary>
        [TestMethod]
        public void SubsectionTwoWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionTwoWidth, Unit.Percentage(26), "Subsection two initial width not set as expected");

            banner.SubsectionTwoWidth = Unit.Percentage(20);
            Assert.AreEqual(banner.SubsectionTwoWidth, Unit.Percentage(20), "Subsection two width not set as expected");
        }

        /// <summary>
        /// Subsection three width test
        /// </summary>
        [TestMethod]
        public void SubsectionThreeWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionThreeWidth, Unit.Percentage(12), "Subsection two initial width not set as expected");

            banner.SubsectionThreeWidth = Unit.Percentage(20);
            Assert.AreEqual(banner.SubsectionThreeWidth, Unit.Percentage(20), "Subsection three width not set as expected");
        }

        /// <summary>
        /// Subsection four width test
        /// </summary>
        [TestMethod]
        public void SubsectionFourWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionFourWidth, Unit.Percentage(12), "Subsection four initial width not set as expected");

            banner.SubsectionFourWidth = Unit.Percentage(20);
            Assert.AreEqual(banner.SubsectionFourWidth, Unit.Percentage(20), "Subsection four width not set as expected");
        }

        /// <summary>
        /// Subsection five width test
        /// </summary>
        [TestMethod]
        public void SubsectionFiveWidthTest()
        {
            PatientBanner banner = new PatientBanner();

            Assert.AreEqual(banner.SubsectionFiveWidth, Unit.Percentage(25), "Subsection five initial width not set as expected");

            banner.SubsectionFiveWidth = Unit.Percentage(20);
            Assert.AreEqual(banner.SubsectionFiveWidth, Unit.Percentage(20), "Subsection five width not set as expected");
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
