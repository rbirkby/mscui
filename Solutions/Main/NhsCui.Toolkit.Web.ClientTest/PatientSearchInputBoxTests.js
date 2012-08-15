//-----------------------------------------------------------------------
// <copyright file="PatientSearchInputBoxTests.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Feb-2007</date>
// <summary>Client-side javascript for PatientSearchInputBox tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

var baseNamingContainer="ctl00_ContentPlaceHolder1_PatientSearchInputBox__";
var controlId=baseNamingContainer + "patientSearchInputBoxExtender";
var textboxId=baseNamingContainer + "TextBox";

/// <summary>
/// Unit tests to test the PatientSearchInputBox control in the NhsCui.Toolkit.Web namespace
/// </summary>
var PatientSearchInputBoxTest = NhsCui.Toolkit.Web.Tests.PatientSearchInputBoxTests = function(){};

NhsCui.Toolkit.Web.Tests.PatientSearchInputBoxTests.prototype = 
{
    IsCommonFamilyNameTrueTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male Mr John Smith 14 Acacia Avenue");
        target.parse();
        
        Assert.AreEqual(true, target.get_isCommonFamilyName(), "IsCommonFamilyName was not set correctly by Parse method.");
    },

    IsCommonFamilyNameFalseTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male Mr John Abercrombie 14 Acacia Avenue");
        target.parse();
        
        Assert.AreEqual(false, target.get_isCommonFamilyName(), "IsCommonFamilyName was not set correctly by Parse method.");
    },

    IsDateOfBirthAgeMismatchTrueTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("John Smith September-1964 20");
        target.parse();

        Assert.AreEqual(true, target.get_isDateOfBirthAgeMismatch(), "IsDateOfBirthAgeMismatch was not set correctly by Parse method.");
    },

    IsDateOfBirthAgeMismatchFalseTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var minus30Year = (new Date().getFullYear() - 30) + '';
        var currentMonth = Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames[new Date().getMonth()];
        target.set_text("John Smith " + currentMonth + "-" + minus30Year + " 30");
        target.parse();

        Assert.AreEqual(false, target.get_isDateOfBirthAgeMismatch(), "IsDateOfBirthAgeMismatch was not set correctly by Parse method.");
    },

    IsGenderTitleMismatchTrueTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male Ms John Smith");
        target.parse();

        Assert.AreEqual(true, target.get_isGenderTitleMismatch(), "IsGenderTitleMismatch was not set correctly by Parse method.");
    },

    IsGenderTitleMismatchFalseTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male Mr John Smith");
        target.parse();

        Assert.AreEqual(false, target.get_isGenderTitleMismatch(), "IsGenderTitleMismatch was not set correctly by Parse method.");
    },

    IsMandatoryInformationEnteredTrueTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_mandatoryInformation([Information.Gender, Information.NhsNumber, Information.Postcode]);
        target.set_text("Female 437 262 3623 RG6 1WG");
        target.parse();

        Assert.AreEqual(true, target.get_isMandatoryInformationEntered(), "IsMandatoryInformationEntered was not set correctly by Parse method.");
    },

    IsMandatoryInformationEnteredFalseTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_mandatoryInformation([Information.Gender, Information.NhsNumber, Information.Postcode, Information.Title]);
        target.set_text("Female 437 262 3623 RG6 1WG");
        target.parse();

        Assert.AreEqual(false, target.get_isMandatoryInformationEntered(), "IsMandatoryInformationEntered was not set correctly by Parse method.");
    },

    ParseCase1Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male Mr John Smith 14 Acacia Avenue");
        target.parse();

        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 1.");
        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly in Parse Case 1.");
        Assert.AreEqual("John", target.get_givenName(), "GivenName was not set correctly in Parse Case 1.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 1.");
        Assert.AreEqual("14 Acacia Avenue", target.get_address(), "Address was not set correctly in Parse Case 1.");
    },

    ParseCase2Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Mr Smith 30 14 Acacia Beacons");
        target.parse();

        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 2.");
        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly in Parse Case 2.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 2.");
        Assert.AreEqual(30, target.get_age(), "Age was not set correctly in Parse Case 2.");
        Assert.AreEqual("14 Acacia Beacons", target.get_address(), "Address was not set correctly in Parse Case 2.");
    },

    ParseCase3Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate(1964, 9);
        target.set_text("John Smith Sept-1964 30");
        target.parse();

        Assert.AreEqual("John", target.get_givenName(), "GivenName was not set correctly in Parse Case 3.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 3.");
        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly in Parse Case 3.");
        Assert.AreEqual(30, target.get_age(), "Age was not set correctly in Parse Case 3.");
    },

    ParseCase4Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate(1954, 6);
        target.set_text("May Williams June-1954");
        target.parse();

        Assert.AreEqual("May", target.get_givenName(), "GivenName was not set correctly in Parse Case 4.");
        Assert.AreEqual("Williams", target.get_familyName(), "FamilyName was not set correctly in Parse Case 4.");
        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly in Parse Case 4.");
    },

    ParseCase5Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("John Smith 14 Acacia Road");
        target.parse();

        Assert.AreEqual("John", target.get_givenName(), "GivenName was not set correctly in Parse Case 5.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 5.");
        Assert.AreEqual("14 Acacia Road", target.get_address(), "Address was not set correctly in Parse Case 5.");
    },

    ParseCase6Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("14 Acacia Road John Smith");
        target.parse();

        Assert.AreEqual("John", target.get_givenName(), "GivenName was not set correctly in Parse Case 6.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 6.");
        Assert.AreEqual("14 Acacia Road", target.get_address(), "Address was not set correctly in Parse Case 6.");
    },

    ParseCase7Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("14 Acacia Beacons John Smith");
        target.parse();

        Assert.AreEqual("14 Acacia Beacons John Smith", target.get_address(), "Address was not set correctly in Parse Case 7.");
    },

    ParseCase8Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("14 Acacia Beacons Mr Smith");
        target.parse();

        Assert.AreEqual("14 Acacia Beacons", target.get_address(), "Address was not set correctly in Parse Case 8.");
        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 8.");
        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly in Parse Case 8.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 8.");
    },

    ParseCase9Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Mr Smith 1964");
        var expectedNhsDate = new NhsDate(1964);
        target.parse();

        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 9.");
        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly in Parse Case 9.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 9.");
        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly in Parse Case 9.");
    },

    ParseCase10Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Mr Smith 30");
        target.parse();

        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 10.");
        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly in Parse Case 10.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 10.");
        Assert.AreEqual(30, target.get_age(), "Age was not set correctly in Parse Case 10.");
    },

    ParseCase11Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("30 Bartholemew Close");
        target.parse();

        Assert.AreEqual("30 Bartholemew Close", target.get_address(), "Address was not set correctly in Parse Case 11.");
    },

    ParseCase12Test : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("30-31 Bartholemew Close");
        target.parse();

        Assert.AreEqual(30, target.get_age(), "Age was not set correctly in Parse Case 12.");
        Assert.AreEqual(31, target.get_ageUpper(), "AgeUpper was not set correctly in Parse Case 12.");
        Assert.AreEqual("Bartholemew", target.get_givenName(), "GivenName was not set correctly in Parse Case 12.");
        Assert.AreEqual("Close", target.get_familyName(), "FamilyName was not set correctly in Parse Case 12.");
    },

    ParseCase13Test : function()
    {
        // Extra case to test for House Name followed by Street Name
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male Mr John Smith Holmeleigh Nursing Home Blackberry Way");
        target.parse();

        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 13.");
        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly in Parse Case 13.");
        Assert.AreEqual("John", target.get_givenName(), "GivenName was not set correctly in Parse Case 13.");
        Assert.AreEqual("Smith", target.get_familyName(), "FamilyName was not set correctly in Parse Case 13.");
        Assert.AreEqual("Holmeleigh Nursing Home Blackberry Way", target.get_address(), "Address was not set correctly in Parse Case 13.");
    },

    ParseCase14Test : function()
    {
        // Extra case to test for the PSIB Sample input string
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate(new Date(1972, 4, 18));

        target.set_text("mr john evans 64 chester str e182ng 34 18/05/1972 1234567890");
        target.parse();

        Assert.AreEqual("evans", target.get_familyName(), "FamilyName was not set correctly in Parse Case 14.");
        Assert.AreEqual("john", target.get_givenName(), "GivenName was not set correctly in Parse Case 14.");
        Assert.AreEqual("1234567890", target.get_nhsNumber(), "NhsNumber was not set correctly in Parse Case 14.");
        Assert.AreEqual(34, target.get_age(), "Age was not set correctly in Parse Case 14.");
        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly in Parse Case 14.");
        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly in Parse Case 14.");
        Assert.AreEqual("mr", target.get_title(), "Title was not set correctly in Parse Case 14.");
        Assert.AreEqual("64 chester str", target.get_address(), "Address was not set correctly in Parse Case 14.");
        Assert.AreEqual("e182ng", target.get_postcode(), "Postcode was not set correctly in Parse Case 14.");
    },

    AddressTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("30 Bartholemew Close");
        target.parse();

        Assert.AreEqual("30 Bartholemew Close", target.get_address(), "Address was not set correctly by Parse method.");
    },

    AgeTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Mr Smith 30");
        target.parse();

        Assert.AreEqual(30, target.get_age(), "Age was not set correctly by Parse method.");
    },

    AgeUpperTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("30-31 Bartholemew Close");
        target.parse();

        Assert.AreEqual(31, target.get_ageUpper(), "AgeUpper was not set correctly by Parse method.");
    },

    CommonFamilyNamesTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var val = ["Flintstone", "Rubble", "Simpson"];

        target.set_commonFamilyNames(val);

        Assert.AreEqual(val, target.get_commonFamilyNames(), "CommonFamilyNames was not set correctly.");
    },

    DateOfBirthDayIntMonthYearTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate(new Date(1960, 3, 15));
        target.set_text("May Williams 15-04-1960");
        target.parse();

        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly by Parse method.");
    },

    DateOfBirthDayTextMonthYearTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate(new Date(1960, 3, 15));
        target.set_text("May Williams 15-April-1960");
        target.parse();

        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly by Parse method.");
    },

    DateOfBirthIntMonthYearTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate("Apr 1960");
        target.set_text("May Williams April-1960");
        target.parse();

        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly by Parse method.");
    },

    DateOfBirthTextMonthYearTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate("April 1960");
        target.set_text("May Williams 04-1960");
        target.parse();

        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly by Parse method.");
    },

    DateOfBirthYearTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate("1960");
        target.set_text("May Williams 1960");
        target.parse();

        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirth().toString(), "DateOfBirth was not set correctly by Parse method.");
    },

    DateOfBirthUpperTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var expectedNhsDate = new NhsDate("July 1954");
        target.set_text("May Williams June-1954-July-1954");
        target.parse();

        Assert.AreEqual(expectedNhsDate.toString(), target.get_dateOfBirthUpper().toString(), "DateOfBirthUpper was not set correctly by Parse method.");
    },

    EndGroupDelimiterTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var defaultVal = '"';

        Assert.AreEqual(defaultVal, target.get_endGroupDelimiter(), "EndGroupDelimiter default value was not initialised correctly.");

        var val = ']';

        target.set_endGroupDelimiter(val);

        Assert.AreEqual(val, target.get_endGroupDelimiter(), "EndGroupDelimiter was not set correctly.");
    },

    FamilyNameTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Williams June-1954-July-1954");
        target.parse();

        Assert.AreEqual("Williams", target.get_familyName(), "FamilyName was not set correctly by Parse method.");
    },

    GenderMaleTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Male John Smith");
        target.parse();

        Assert.AreEqual(Gender.Male, target.get_gender(), "Gender was not set correctly by Parse method.");
    },

    GenderFemaleTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Female Janice Simpson");
        target.parse();

        Assert.AreEqual(Gender.Female, target.get_gender(), "Gender was not set correctly by Parse method.");

        target.set_text("Janice Simpson Female");
        target.parse();

        Assert.AreEqual(Gender.Female, target.get_gender(), "Gender was not set correctly by Parse method.");

        target.set_text("Janice Female Simpson");
        target.parse();

        Assert.AreEqual(Gender.Female, target.get_gender(), "Gender was not set correctly by Parse method.");

        target.set_text("JaniceFemaleSimpson");
        target.parse();

        Assert.AreNotEqual(Gender.Female, target.get_gender(), "Gender was not set correctly by Parse method.");
    },

    GivenNameTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Mr John Smith");
        target.parse();

        Assert.AreEqual("John", target.get_givenName(), "GivenName was not set correctly by Parse method.");
    },

    InformationDelimiterTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var defaultVal = ',';

        Assert.AreEqual(defaultVal, target.get_informationDelimiter(), "EndGroupDelimiter was not initialised correctly.");

        var val = '~';

        target.set_informationDelimiter(val);

        Assert.AreEqual(val, target.get_informationDelimiter(), "InformationDelimiter was not set correctly.");
    },

    InformationDelimiterParseTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_startGroupDelimiter('[');
        target.set_endGroupDelimiter(']');

        target.set_text("Mr [Jean Michel] [Saint Martin de Belleville] CD48 9DB 4372623623 56 [23 Rue de Remarques]");
        target.parse();

        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("Jean Michel", target.get_givenName(), "GivenName was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("Saint Martin de Belleville", target.get_familyName(), "FamilyName was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("CD48 9DB", target.get_postcode(), "Postcode was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("4372623623", target.get_nhsNumber(), "NhsNumber was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual(56, target.get_age(), "Age was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("23 Rue de Remarques", target.get_address(), "Address was not set correctly by Parse method (using InformationFormat).");
    },

    InformationFormatTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var val = [Information.Address, Information.Age, Information.Postcode, Information.Title];

        target.set_informationFormat(val);

        Assert.AreEqual(val, target.get_informationFormat(), "InformationFormat was not set correctly.");
    },

    InformationFormatParseTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var val = [Information.Title, Information.GivenName, Information.FamilyName, Information.Postcode, Information.NhsNumber, Information.Age];

        target.set_informationFormat(val);
        target.set_informationDelimiter('|');

        // Last field (an address) should be ignored as the InformationFormat list does not include it as a field to parse
        target.set_text("Mr|Jean Michel|Saint Martin de Belleville|CD48 9DB|4372623623|56|23 Rue de Remarques");
        target.parse();

        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("Jean Michel", target.get_givenName(), "GivenName was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("Saint Martin de Belleville", target.get_familyName(), "FamilyName was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("CD48 9DB", target.get_postcode(), "Postcode was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual("4372623623", target.get_nhsNumber(), "NhsNumber was not set correctly by Parse method (using InformationFormat).");
        Assert.AreEqual(56, target.get_age(), "Age was not set correctly by Parse method (using InformationFormat).");
        Assert.IsNull(target.get_address(), "Address should not have been set by Parse method (using InformationFormat).");
    },

    MaximumAgeTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var val = 154;

        target.set_maximumAge(val);

        Assert.AreEqual(val, target.get_maximumAge(), "MaximumAge was not set correctly.");
    },

    NhsNumberFormattedTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Female 437 262 3623 RG6 1WG");
        target.parse();

        Assert.AreEqual("437 262 3623", target.get_nhsNumber(), "NhsNumber was not set correctly by Parse method.");
    },

    NhsNumberUnformattedTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Female 4372623623 RG6 1WG");
        target.parse();

        Assert.AreEqual("4372623623", target.get_nhsNumber(), "NhsNumber was not set correctly by Parse method.");
    },

    PostcodeFormattedTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Female 4372623623 RG6 1WG");
        target.parse();

        Assert.AreEqual("RG6 1WG", target.get_postcode(), "Postcode was not set correctly by Parse method.");
    },

    PostcodeUnformattedTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Female 4372623623 RG61WG");
        target.parse();

        Assert.AreEqual("RG61WG", target.get_postcode(), "Postcode was not set correctly by Parse method.");
    },

    StartGroupDelimiterTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var defaultVal = '"';

        Assert.AreEqual(defaultVal, target.get_startGroupDelimiter(), "StartGroupDelimiter default value was not initialised correctly.");

        var val = '[';

        target.set_startGroupDelimiter(val);

        Assert.AreEqual(val, target.get_startGroupDelimiter(), "StartGroupDelimiter was not set correctly.");
    },

    TextTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var val = "Male Mr John Smith 14 Acacia Avenue";

        target.set_text(val);

        Assert.AreEqual(val, target.get_text(), "Text was not set correctly.");
    },

    TitleTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("Mr John Smith");
        target.parse();

        Assert.AreEqual("Mr", target.get_title(), "Title was not set correctly by Parse method.");
    },

    TitlesTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        var val = [new Title("Mdme", Gender.Female), new Title("Herr", Gender.Male), new Title("Dean")];

        target.set_titles(val);

        Assert.AreEqual(val, target.get_titles(), "Titles was not set correctly.");
    },

    UnmatchedTextTest : function()
    {
        var target = testHarness.getObject(controlId);
        var theTextBox = testHarness.getElement(textboxId);
        
        target.set_text("14 Middler Road #####");
        target.parse();

        Assert.AreEqual("#####", target.get_unmatchedText(), "UnmatchedText was not set correctly by Parse method.");
    }
};