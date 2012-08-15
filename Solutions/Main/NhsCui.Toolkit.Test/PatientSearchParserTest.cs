//-----------------------------------------------------------------------
// <copyright file="PatientSearchParserTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-Jan-2007</date>
// <summary>Parser class test</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.Web;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Collections.ObjectModel;
    using NhsCui.Toolkit.PatientSearch;

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.Parser and is intended
    ///to contain all NhsCui.Toolkit.Parser Unit Tests
    ///</summary>
    [TestClass()]
    public class PatientSearchParserTest
    {
        /// <summary>
        ///A test for AllMandatoryInformationEntered ()
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.dll")]
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void AllMandatoryInformationEnteredTrueTest()
        {
            Parser target = new Parser();

            target.MandatoryInformation = new List<Information>(new Information[] { Information.Gender, Information.NhsNumber, Information.Postcode });
            target.Text = "Female 437 262 3623 RG6 1WG";
            target.Parse();

            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor accessor = new NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor(target);

            bool expected = true;
            bool actual;

            actual = accessor.AllMandatoryInformationEntered();

            Assert.AreEqual<bool>(expected, actual, "NhsCui.Toolkit.Parser.AllMandatoryInformationEntered did not return the expected value.");
        }

        /// <summary>
        ///A test for AllMandatoryInformationEntered ()
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.dll")]
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void AllMandatoryInformationEnteredFalseTest()
        {
            Parser target = new Parser();

            target.MandatoryInformation = new List<Information>(new Information[] { Information.Gender, Information.NhsNumber, Information.Postcode, Information.Title });
            target.Text = "Female 437 262 3623 RG6 1WG";
            target.Parse();

            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor accessor = new NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor(target);

            bool expected = false;
            bool actual;

            actual = accessor.AllMandatoryInformationEntered();

            Assert.AreEqual<bool>(expected, actual, "NhsCui.Toolkit.Parser.AllMandatoryInformationEntered did not return the expected value.");
        }

        /// <summary>
        ///A test for IsCommonFamilyName
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsCommonFamilyNameTrueTest()
        {
            Parser target = new Parser();

            target.Text = "Male Mr John Smith 14 Acacia Avenue";
            target.Parse();

            Assert.AreEqual<bool>(true, target.IsCommonFamilyName, "NhsCui.Toolkit.Parser.IsCommonFamilyName was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsCommonFamilyName
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsCommonFamilyNameFalseTest()
        {
            Parser target = new Parser();

            target.Text = "Male Mr John Abercrombie 14 Acacia Avenue";
            target.Parse();

            Assert.AreEqual<bool>(false, target.IsCommonFamilyName, "NhsCui.Toolkit.Parser.IsCommonFamilyName was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsDateOfBirthAgeMismatch
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsDateOfBirthAgeMismatchTrueTest()
        {
            Parser target = new Parser();

            target.Text = "John Smith September-1964 20";
            target.Parse();

            Assert.AreEqual<bool>(true, target.IsDateOfBirthAgeMismatch, "NhsCui.Toolkit.Parser.IsDateOfBirthAgeMismatch was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsDateOfBirthAgeMismatch
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsDateOfBirthAgeMismatchFalseTest()
        {
            Parser target = new Parser();

            string minus30Year = (DateTime.Now.Year - 30).ToString(CultureInfo.InvariantCulture);
            string currentMonth = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month];
            target.Text = string.Format(CultureInfo.InvariantCulture, "John Smith {0}-{1} 30", currentMonth, minus30Year);
            target.Parse();

            Assert.AreEqual<bool>(false, target.IsDateOfBirthAgeMismatch, "NhsCui.Toolkit.Parser.IsDateOfBirthAgeMismatch was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsGenderTitleMismatch
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsGenderTitleMismatchTrueTest()
        {
            Parser target = new Parser();

            target.Text = "Male Ms John Smith";
            target.Parse();

            Assert.AreEqual<bool>(true, target.IsGenderTitleMismatch, "NhsCui.Toolkit.Parser.IsGenderTitleMismatch was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsGenderTitleMismatch
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsGenderTitleMismatchFalseTest()
        {
            Parser target = new Parser();

            target.Text = "Male Mr John Smith";
            target.Parse();

            Assert.AreEqual<bool>(false, target.IsGenderTitleMismatch, "NhsCui.Toolkit.Parser.IsGenderTitleMismatch was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsMandatoryInformationEntered
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsMandatoryInformationEnteredTrueTest()
        {
            Parser target = new Parser();

            target.MandatoryInformation = new List<Information>(new Information[] { Information.Gender, Information.NhsNumber, Information.Postcode });
            target.Text = "Female 437 262 3623 RG6 1WG";
            target.Parse();

            Assert.AreEqual<bool>(true, target.IsMandatoryInformationEntered, "NhsCui.Toolkit.Parser.IsMandatoryInformationEntered was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for IsMandatoryInformationEntered
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void IsMandatoryInformationEnteredFalseTest()
        {
            Parser target = new Parser();

            target.MandatoryInformation = new List<Information>(new Information[] { Information.Gender, Information.NhsNumber, Information.Postcode, Information.Title });
            target.Text = "Female 437 262 3623 RG6 1WG";
            target.Parse();

            Assert.AreEqual<bool>(false, target.IsMandatoryInformationEntered, "NhsCui.Toolkit.Parser.IsMandatoryInformationEntered was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for MandatoryInformation
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void MandatoryInformationTest()
        {
            Parser target = new Parser();

            List<Information> val = new List<Information>(new Information[] { Information.Gender, Information.NhsNumber, Information.Postcode, Information.Title });

            target.MandatoryInformation = val;

            CollectionAssert.AreEqual(val, target.MandatoryInformation, "NhsCui.Toolkit.Parser.MandatoryInformation was not set correctly.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase1Test()
        {
            Parser target = new Parser();

            target.Text = "Male Mr John Smith 14 Acacia Avenue";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 1.");
            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 1.");
            Assert.AreEqual<string>("John", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 1.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 1.");
            Assert.AreEqual<string>("14 Acacia Avenue", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 1.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase2Test()
        {
            Parser target = new Parser();

            target.Text = "Mr Smith 30 14 Acacia Beacons";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 2.");
            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 2.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 2.");
            Assert.AreEqual<int>(30, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly in Parse Case 2.");
            Assert.AreEqual<string>("14 Acacia Beacons", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 2.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase3Test()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate(1964, 9);
            target.Text = "John Smith Sept-1964 30";
            target.Parse();

            Assert.AreEqual<string>("John", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 3.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 3.");

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 3.");
            Assert.AreEqual<int>(30, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly in Parse Case 3.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase4Test()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate(1954, 6);
            target.Text = "May Williams June-1954";
            target.Parse();

            Assert.AreEqual<string>("May", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 4.");
            Assert.AreEqual<string>("Williams", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 4.");

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 4.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase5Test()
        {
            Parser target = new Parser();

            target.Text = "John Smith 14 Acacia Road";
            target.Parse();

            Assert.AreEqual<string>("John", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 5.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 5.");
            Assert.AreEqual<string>("14 Acacia Road", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 5.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase6Test()
        {
            Parser target = new Parser();

            target.Text = "14 Acacia Road John Smith";
            target.Parse();

            Assert.AreEqual<string>("John", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 6.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 6.");
            Assert.AreEqual<string>("14 Acacia Road", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 6.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase7Test()
        {
            Parser target = new Parser();

            target.Text = "14 Acacia Beacons John Smith";
            target.Parse();

            Assert.AreEqual<string>("14 Acacia Beacons John Smith", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 7.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase8Test()
        {
            Parser target = new Parser();

            target.Text = "14 Acacia Beacons Mr Smith";
            target.Parse();

            Assert.AreEqual<string>("14 Acacia Beacons", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 8.");
            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 8.");
            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 8.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 8.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase9Test()
        {
            Parser target = new Parser();

            target.Text = "Mr Smith 1964";
            NhsDate expectedNhsDate = new NhsDate(1964);
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 9.");
            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 9.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 9.");

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly in Parse Case 9.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase10Test()
        {
            Parser target = new Parser();

            target.Text = "Mr Smith 30";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 10.");
            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 10.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 10.");
            Assert.AreEqual<int>(30, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly in Parse Case 10.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase11Test()
        {
            Parser target = new Parser();

            target.Text = "30 Bartholemew Close";
            target.Parse();

            Assert.AreEqual<string>("30 Bartholemew Close", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 11.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase12Test()
        {
            Parser target = new Parser();

            target.Text = "30-31 Bartholemew Close";
            target.Parse();

            Assert.AreEqual<int>(30, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly in Parse Case 12.");
            Assert.AreEqual<int>(31, target.AgeUpper, "NhsCui.Toolkit.Parser.AgeUpper was not set correctly in Parse Case 12.");
            Assert.AreEqual<string>("Bartholemew", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 12.");
            Assert.AreEqual<string>("Close", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 12.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase13Test()
        {
            // Extra case to test for House Name followed by Street Name
            Parser target = new Parser();

            target.Text = "Male Mr John Smith Holmeleigh Nursing Home Blackberry Way";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 13.");
            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 13.");
            Assert.AreEqual<string>("John", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 13.");
            Assert.AreEqual<string>("Smith", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 13.");
            Assert.AreEqual<string>("Holmeleigh Nursing Home Blackberry Way", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 13.");
        }

        /// <summary>
        ///A test for Parse ()
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void ParseCase14Test()
        {
            // Extra case to test for the PSIB Sample input string
            Parser target = new Parser();
            NhsDate expectedNhsDate = new NhsDate("18-May-1972");

            target.Text = "mr john evans 64 chester str e182ng 34 18/05/1972 1234567890";
            target.Parse();

            Assert.AreEqual<string>("evans", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly in Parse Case 14.");
            Assert.AreEqual<string>("john", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly in Parse Case 14.");
            Assert.AreEqual<string>("1234567890", target.NhsNumber, "NhsCui.Toolkit.Parser.NhsNumber was not set correctly in Parse Case 14.");
            Assert.AreEqual<int>(34, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly in Parse Case 14.");
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly in Parse Case 14.");

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly in Parse Case 14.");
            Assert.AreEqual<string>("mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly in Parse Case 14.");
            Assert.AreEqual<string>("64 chester str", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly in Parse Case 14.");
            Assert.AreEqual<string>("e182ng", target.Postcode, "NhsCui.Toolkit.Parser.Postcode was not set correctly in Parse Case 14.");
        }

        /// <summary>
        ///A test for PropertyHasBeenSet (Information)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.dll")]
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void PropertyHasBeenSetTrueTest()
        {
            Parser target = new Parser();

            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor accessor = new NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor(target);

            Information patientSearchInfo = Information.Gender;

            target.Text = "Female";
            target.Parse();

            bool actual;

            actual = accessor.PropertyHasBeenSet(patientSearchInfo);

            Assert.AreEqual<bool>(true, actual, "NhsCui.Toolkit.Parser.PropertyHasBeenSet did not return the expected value.");
        }

        /// <summary>
        ///A test for PropertyHasBeenSet (Information)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.dll")]
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void PropertyHasBeenSetFalseTest()
        {
            Parser target = new Parser();

            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor accessor = new NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientSearchParserAccessor(target);

            Information patientSearchInfo = Information.DateOfBirth;

            target.Text = "Mr Smith 30";
            target.Parse();

            bool actual;

            actual = accessor.PropertyHasBeenSet(patientSearchInfo);

            Assert.AreEqual<bool>(false, actual, "NhsCui.Toolkit.Parser.PropertyHasBeenSet did not return the expected value.");
        }

        /// <summary>
        ///A test for Address
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void AddressTest()
        {
            Parser target = new Parser();

            target.Text = "30 Bartholemew Close";
            target.Parse();

            Assert.AreEqual<string>("30 Bartholemew Close", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for Age
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void AgeTest()
        {
            Parser target = new Parser();

            target.Text = "Mr Smith 30";
            target.Parse();

            Assert.AreEqual<int>(30, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for AgeUpper
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void AgeUpperTest()
        {
            Parser target = new Parser();

            target.Text = "30-31 Bartholemew Close";
            target.Parse();

            Assert.AreEqual<int>(31, target.AgeUpper, "NhsCui.Toolkit.Parser.AgeUpper was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for CommonFamilyNames
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void CommonFamilyNamesTest()
        {
            Parser target = new Parser();

            System.Collections.Generic.List<string> val = new List<string>();
            val.Add("Flintstone");
            val.Add("Rubble");
            val.Add("Simpson");

            target.CommonFamilyNames = val;

            Assert.AreEqual<List<string>>(val, target.CommonFamilyNames, "NhsCui.Toolkit.Parser.CommonFamilyNames was not set correctly.");
        }

        /// <summary>
        ///A test for DateOfBirth
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void DateOfBirthDayIntMonthYearTest()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate(new DateTime(1960, 4, 15));
            target.Text = "May Williams 15-04-1960";
            target.Parse();

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for DateOfBirth
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void DateOfBirthDayTextMonthYearTest()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate(new DateTime(1960, 4, 15));
            target.Text = "May Williams 15-April-1960";
            target.Parse();

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for DateOfBirth
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void DateOfBirthIntMonthYearTest()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate("April 1960");
            target.Text = "May Williams April-1960";
            target.Parse();

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for DateOfBirth
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void DateOfBirthTextMonthYearTest()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate("April 1960");
            target.Text = "May Williams 04-1960";
            target.Parse();

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for DateOfBirth
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void DateOfBirthYearTest()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate("1960");
            target.Text = "May Williams 1960";
            target.Parse();

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirth.ToString(), "NhsCui.Toolkit.Parser.DateOfBirth was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for DateOfBirthUpper
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void DateOfBirthUpperTest()
        {
            Parser target = new Parser();

            NhsDate expectedNhsDate = new NhsDate(1954, 7);
            target.Text = "May Williams June-1954-July-1954";
            target.Parse();

            // Can't check for equivalence using Assert.AreEqual<NhsDate>(expectedNhsDate, target.DateOfBirth)
            // because NhsDate doesn't override the equality operator so check using ToString() calls...
            Assert.AreEqual<string>(expectedNhsDate.ToString(), target.DateOfBirthUpper.ToString(), "NhsCui.Toolkit.Parser.DateOfBirthUpper was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for EndGroupDelimiter
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void EndGroupDelimiterTest()
        {
            Parser target = new Parser();

            char defaultVal = '"';

            Assert.AreEqual<char>(defaultVal, target.EndGroupDelimiter, "NhsCui.Toolkit.Parser.EndGroupDelimiter default value was not initialised correctly.");

            char val = ']';

            target.EndGroupDelimiter = val;

            Assert.AreEqual<char>(val, target.EndGroupDelimiter, "NhsCui.Toolkit.Parser.EndGroupDelimiter was not set correctly.");
        }

        /// <summary>
        ///A test for FamilyName
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void FamilyNameTest()
        {
            Parser target = new Parser();

            target.Text = "Williams June-1954-July-1954";
            target.Parse();

            Assert.AreEqual<string>("Williams", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for Gender - Male
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void GenderMaleTest()
        {
            Parser target = new Parser();

            target.Text = "Male John Smith";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Male, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for Gender - Female
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void GenderFemaleTest()
        {
            Parser target = new Parser();

            target.Text = "Female Janice Simpson";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Female, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly by Parse method.");

            target.Text = "Janice Simpson Female";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Female, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly by Parse method.");

            target.Text = "Janice Female Simpson";
            target.Parse();

            Assert.AreEqual<Gender>(Gender.Female, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly by Parse method.");

            target.Text = "JaniceFemaleSimpson";
            target.Parse();

            Assert.AreNotEqual<Gender>(Gender.Female, target.Gender, "NhsCui.Toolkit.Parser.Gender was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for GivenName
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void GivenNameTest()
        {
            Parser target = new Parser();

            target.Text = "Mr John Smith";
            target.Parse();

            Assert.AreEqual<string>("John", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for InformationDelimiter
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void InformationDelimiterTest()
        {
            Parser target = new Parser();

            char defaultVal = ',';

            Assert.AreEqual<char>(defaultVal, target.InformationDelimiter, "NhsCui.Toolkit.Parser.EndGroupDelimiter was not initialised correctly.");

            char val = '~';

            target.InformationDelimiter = val;

            Assert.AreEqual<char>(val, target.InformationDelimiter, "NhsCui.Toolkit.Parser.InformationDelimiter was not set correctly.");
        }

        /// <summary>
        ///A test for parsing with specific InformationDelimiter
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void InformationDelimiterParseTest()
        {
            Parser target = new Parser();

            target.StartGroupDelimiter = '[';
            target.EndGroupDelimiter = ']';

            target.Text = "Mr [Jean Michel] [Saint Martin de Belleville] CD48 9DB 4372623623 56 [23 Rue de Remarques]";
            target.Parse();

            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("Jean Michel", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("Saint Martin de Belleville", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("CD48 9DB", target.Postcode, "NhsCui.Toolkit.Parser.Postcode was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("4372623623", target.NhsNumber, "NhsCui.Toolkit.Parser.NhsNumber was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<int>(56, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("23 Rue de Remarques", target.Address, "NhsCui.Toolkit.Parser.Address was not set correctly by Parse method (using InformationFormat).");
        }

        /// <summary>
        ///A test for InformationFormat
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void InformationFormatTest()
        {
            Parser target = new Parser();

            List<Information> val = new List<Information>(new Information[] { Information.Address, Information.Age, Information.Postcode, Information.Title });

            target.InformationFormat = val;

            CollectionAssert.AreEqual(val, target.InformationFormat, "NhsCui.Toolkit.Parser.InformationFormat was not set correctly.");
        }

        /// <summary>
        ///A test for parsing using InformationFormat
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void InformationFormatParseTest()
        {
            Parser target = new Parser();

            List<Information> val = new List<Information>(new Information[] { Information.Title, Information.GivenName, Information.FamilyName, Information.Postcode, Information.NhsNumber, Information.Age });

            target.InformationFormat = val;
            target.InformationDelimiter = '|';

            // Last field (an address) should be ignored as the InformationFormat list does not include it as a field to parse
            target.Text = "Mr|Jean Michel|Saint Martin de Belleville|CD48 9DB|4372623623|56|23 Rue de Remarques";
            target.Parse();

            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("Jean Michel", target.GivenName, "NhsCui.Toolkit.Parser.GivenName was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("Saint Martin de Belleville", target.FamilyName, "NhsCui.Toolkit.Parser.FamilyName was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("CD48 9DB", target.Postcode, "NhsCui.Toolkit.Parser.Postcode was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<string>("4372623623", target.NhsNumber, "NhsCui.Toolkit.Parser.NhsNumber was not set correctly by Parse method (using InformationFormat).");
            Assert.AreEqual<int>(56, target.Age, "NhsCui.Toolkit.Parser.Age was not set correctly by Parse method (using InformationFormat).");
            Assert.IsNull(target.Address, "NhsCui.Toolkit.Parser.Address should not have been set by Parse method (using InformationFormat).");
        }

        /// <summary>
        ///A test for MaximumAge
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void MaximumAgeTest()
        {
            Parser target = new Parser();

            int val = 154;

            target.MaximumAge = val;

            Assert.AreEqual<int>(val, target.MaximumAge, "NhsCui.Toolkit.Parser.MaximumAge was not set correctly.");
        }

        /// <summary>
        ///A test for formatted NhsNumber
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void NhsNumberFormattedTest()
        {
            Parser target = new Parser();

            target.Text = "Female 437 262 3623 RG6 1WG";
            target.Parse();

            Assert.AreEqual("437 262 3623", target.NhsNumber, "NhsCui.Toolkit.Parser.NhsNumber was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for unformatted NhsNumber
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void NhsNumberUnformattedTest()
        {
            Parser target = new Parser();

            target.Text = "Female 4372623623 RG6 1WG";
            target.Parse();

            Assert.AreEqual<string>("4372623623", target.NhsNumber, "NhsCui.Toolkit.Parser.NhsNumber was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for Postcode
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void PostcodeFormattedTest()
        {
            Parser target = new Parser();

            target.Text = "Female 4372623623 RG6 1WG";
            target.Parse();

            Assert.AreEqual<string>("RG6 1WG", target.Postcode, "NhsCui.Toolkit.Parser.Postcode was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for Postcode
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void PostcodeUnformattedTest()
        {
            Parser target = new Parser();

            target.Text = "Female 4372623623 RG61WG";
            target.Parse();

            Assert.AreEqual<string>("RG61WG", target.Postcode, "NhsCui.Toolkit.Parser.Postcode was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for StartGroupDelimiter
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void StartGroupDelimiterTest()
        {
            Parser target = new Parser();

            char defaultVal = '"';

            Assert.AreEqual<char>(defaultVal, target.StartGroupDelimiter, "NhsCui.Toolkit.Parser.StartGroupDelimiter default value was not initialised correctly.");

            char val = '[';

            target.StartGroupDelimiter = val;

            Assert.AreEqual<char>(val, target.StartGroupDelimiter, "NhsCui.Toolkit.Parser.StartGroupDelimiter was not set correctly.");
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void TextTest()
        {
            Parser target = new Parser();

            string val = "Male Mr John Smith 14 Acacia Avenue";

            target.Text = val;

            Assert.AreEqual(val, target.Text, "NhsCui.Toolkit.Parser.Text was not set correctly.");
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void TitleTest()
        {
            Parser target = new Parser();

            target.Text = "Mr John Smith";
            target.Parse();

            Assert.AreEqual<string>("Mr", target.Title, "NhsCui.Toolkit.Parser.Title was not set correctly by Parse method.");
        }

        /// <summary>
        ///A test for Titles
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void TitlesTest()
        {
            Parser target = new Parser();

            List<Title> val = new List<Title>();
            val.Add(new Title("Mdme", Gender.Female));
            val.Add(new Title("Herr", Gender.Male));
            val.Add(new Title("Dean"));

            target.Titles = val;

            CollectionAssert.AreEqual(val, target.Titles, "NhsCui.Toolkit.Parser.Titles was not set correctly.");
        }

        /// <summary>
        ///A test for UnmatchedText
        ///</summary>
        [TestMethod(), Description("PatientSearch.Parser tests")]
        public void UnmatchedTextTest()
        {
            Parser target = new Parser();

            target.Text = "14 Middler Road #####";
            target.Parse();

            Assert.AreEqual<String>("#####", target.UnmatchedText, "NhsCui.Toolkit.Parser.UnmatchedText was not set correctly by Parse method.");
        }
    }
}
