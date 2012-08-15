//-----------------------------------------------------------------------
// <copyright file="DateTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests to test the Date class in the NhsCui.Toolkit.DateAndTime namespace</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Unit tests to test the Date class in the NhsCui.Toolkit.DateAndTime namespace
    /// </summary>
    [TestClass]
    public class DateTest
    {
        /// <summary>
        /// Default ctor 
        /// </summary>
        public DateTest()
        {
        }

        /// <summary>
        /// Test ToString on DateType Exact
        /// </summary>
        [TestMethod]
        public void ConstructNhsDateDateTimeAndBool()
        {
            DateTime baseDateTime = DateTime.Now;

            NhsDate date = new NhsDate(baseDateTime, false);

            Assert.IsTrue(date.DateType == DateType.Exact);
            Assert.IsTrue(date.DateValue == baseDateTime);

            NhsDate date2 = new NhsDate(baseDateTime, true);

            Assert.IsTrue(date2.DateType == DateType.Approximate);
            Assert.IsTrue(date2.DateValue == baseDateTime);
        }

        #region Tests for Add(NhsDate sourceDate, string instruction) function
        
        /// <summary>
        /// Test the ArgumentNullException for instruction parameter
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
        public void NullInstructionAddTest()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now;
            NhsDate date = new NhsDate(nowAsItWasAtTheStartofTheTest);
            NhsDate.Add(date, null);            
        }

        /// <summary>
        /// Test the ArgumentNullException for sourcedate parameter
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
        public void NullSourceDateAddTest()
        {
            NhsDate.Add(null, string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.DaysUnit));
        }
        
        /// <summary>
        /// Test the ArgumentException for instruction parameter
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException), "Should throw ArgumentOutOfRangeException from Add method")]
        public void InvalidInstructionAddTest()
        {
            DateTime date = DateTime.Now;
            NhsDate sourcedate = new NhsDate(date);
            NhsDate.Add(sourcedate, string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.resourceCulture));
        }     

        /// <summary>
        /// Test the ArgumentException for sourcedate parameter being a year
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException), "Should throw ArgumentOutOfRangeException from Add method")]
        public void SourceDateYearAddTest()
        {
            int year = 2007;
            NhsDate date = new NhsDate(year);
            NhsDate.Add(date, string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.DaysUnit));
        }

        /// <summary>
        /// Test the ability to do simple arithemtic updates to date by asking Date to add 1 day
        /// </summary>
        [TestMethod]
        public void DataAddDay()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; 

            NhsDate date = new NhsDate(nowAsItWasAtTheStartofTheTest);

            date.Add(string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.DaysUnit));

            Assert.AreEqual(date.DateValue, nowAsItWasAtTheStartofTheTest.AddDays(1));
        }

        /// <summary>
        /// Test the ability to do complex arithemtic updates to date by asking Date to add 1 month and 1 day
        /// </summary>
        [TestMethod]
        public void DataAddMonthAndDay()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; 

            NhsDate date = new NhsDate(nowAsItWasAtTheStartofTheTest);

            date.Add(string.Format(CultureInfo.CurrentCulture, "+1{0}+1{1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.MonthsUnit, NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.DaysUnit));

            Assert.AreEqual(date.DateValue, nowAsItWasAtTheStartofTheTest.AddMonths(1).AddDays(1));
        }

        /// <summary>
        /// Test the ability to do simple arithemtic updates to date by asking Date to Add 7 years
        /// </summary>
        [TestMethod]
        public void DataAdd7Years()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; 

            NhsDate date = new NhsDate(nowAsItWasAtTheStartofTheTest);

            date.Add(string.Format(CultureInfo.CurrentCulture, "+7{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.YearsUnit));
            
            Assert.AreEqual(nowAsItWasAtTheStartofTheTest.AddYears(7), date.DateValue);
        }

        /// <summary>
        /// Test the ability to do simple arithemtic updates to date by asking Date to Subtract 7 years
        /// </summary>
        [TestMethod]
        public void DataSubtract7Years()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; 

            NhsDate date = new NhsDate(nowAsItWasAtTheStartofTheTest);

            date.Add(string.Format(CultureInfo.CurrentCulture, "-7{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.YearsUnit));

            Assert.AreEqual(nowAsItWasAtTheStartofTheTest.AddYears(-7), date.DateValue);
        }

        /// <summary>
        /// Test the ability to do complex arithemtic updates to date by asking Date to Subtract 2 years AND add 5 days
        /// </summary>
        [TestMethod]
        public void DataSubtract2YearsAdd5Days()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; 

            NhsDate date = new NhsDate(nowAsItWasAtTheStartofTheTest);

            date.Add(string.Format(CultureInfo.CurrentCulture, "-2{0}+5{1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.YearsUnit, NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.DaysUnit));

            Assert.AreEqual(nowAsItWasAtTheStartofTheTest.AddYears(-2).AddDays(5), date.DateValue);
        }

        #endregion    

        /// <summary>
        /// Test ToString on DateType Exact
        /// </summary>
        [TestMethod]
        public void ToStringExact()
        {
            NhsDate date = new NhsDate(new System.DateTime(1974, 3, 26));

            Assert.AreEqual("26-Mar-1974", date.ToString());

            Assert.AreEqual("26-Mar-1974", date.ToString(false), "Check that default for 'include day of week' flag is correct");

            // try with includeDayOfWeek flag
            Assert.AreEqual("Tue 26-Mar-1974", date.ToString(true));
        }

        /// <summary>
        /// Test ToString on DateType Exact
        /// </summary>
        [TestMethod]
        public void ToStringExactGetRelativeText()
        {
            NhsDate date = new NhsDate(System.DateTime.Today);

            GlobalizationService gc = new GlobalizationService();

            // Today works
            Assert.AreEqual(NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.Today, date.ToString(false, false, true, CultureInfo.CurrentCulture), "RelativeText of Today is not working");

            date.DateValue = DateTime.Today.AddDays(1);

            Assert.AreEqual(NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.Tomorrow, date.ToString(false, false, true, CultureInfo.CurrentCulture), "RelativeText of Tomorrow is not working");

            date.DateValue = DateTime.Today.AddDays(-1);

            Assert.AreEqual(NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.Yesterday, date.ToString(false, false, true, CultureInfo.CurrentCulture), "RelativeText of Yesterday is not working");
            
            // Check the "Who Wins condition when "Show Day of Week" and "Show Relative Text" are both true
            Assert.AreEqual(NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsDateResourcesAccessor.Yesterday, date.ToString(true, false, true, CultureInfo.CurrentCulture), "When includeDayOfWeek, showRelativeText are both true, showRelativeText should win. It is not");

            DateTime testDate = new DateTime(1974, 3, 26);

            date.DateValue = testDate;

            Assert.AreEqual(testDate.ToString(gc.ShortDatePattern, CultureInfo.CurrentCulture), date.ToString(), "Check that default for 'include day of week' flag is correct");

            // try with includeDayOfWeek flag

            Assert.AreEqual(testDate.ToString(gc.ShortDatePatternWithDayOfWeek, CultureInfo.CurrentCulture), date.ToString(true), "Check that default for 'include day of week' flag is correct");
        }

        /// <summary>
        /// Test ToString on DateType Year
        /// </summary>
        [TestMethod]
        public void ToStringYear()
        {
            System.DateTime baseDateTime = System.DateTime.Now;

            NhsDate date = new NhsDate(baseDateTime);

            date.DateType = DateType.Year;

            Assert.AreEqual("0000", date.ToString(), "Check the default of Year when an explicit date is passed to ctor");

            date = new NhsDate(baseDateTime.Year);

            date.DateType = DateType.Year;

            Assert.AreEqual(baseDateTime.Year.ToString(CultureInfo.CurrentCulture), date.ToString());
        }

        /// <summary>
        /// Test ToString on DateType YearMonth
        /// </summary>
        [TestMethod]
        public void ToStringYearMonth()
        {
            System.DateTime baseDateTime = System.DateTime.Now;

            NhsDate date = new NhsDate(baseDateTime);

            date.DateType = DateType.YearMonth;

            Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, "{0}-{1}", CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[1 - 1], "0000"), date.ToString(), "Check the default of Month and Year when an explicit date is passed to ctor");
            
            // Now set the date to a year and a month which is what DateType = YearMonth prefers
            date = new NhsDate(1974, 3);

            Assert.AreEqual(
                string.Format(CultureInfo.InvariantCulture, "{0}-{1}", CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[3 - 1], "1974"), date.ToString());
        }

        /// <summary>
        /// Test ToString on DateType Null
        /// </summary>
        [TestMethod]
        public void ToStringNull()
        {
            NhsDate date = new NhsDate();
            date.DateType = DateType.Null;
            Assert.IsTrue(date.ToString().Length == 0);
        }    

        /// <summary>
        /// Test Parse for a Month and Year
        /// </summary>
        [TestMethod]
        public void ParseMonthAndYear()
        {
            System.DateTime baseDateTime = System.DateTime.Now;

            NhsDate date = new NhsDate(baseDateTime);

            date.DateType = DateType.YearMonth;
            date.Year = 1974;
            date.Month = 3;

            NhsDate parsedDate;

            Assert.IsTrue(NhsDate.TryParseExact("jan-2008", out parsedDate, CultureInfo.InvariantCulture), "Try parse failed");
            Assert.AreEqual(parsedDate.Month, 1, "Month not being parsed");
            Assert.AreEqual(parsedDate.Year, 2008, "Month not being parsed");

            Assert.IsTrue(NhsDate.TryParseExact("january-2008", out parsedDate, CultureInfo.InvariantCulture), "Try parse failed");
            Assert.AreEqual(parsedDate.Month, 1, "Month not being parsed");
            Assert.AreEqual(parsedDate.Year, 2008, "Month not being parsed");

            Assert.IsTrue(NhsDate.TryParseExact("1-2008", out parsedDate, CultureInfo.InvariantCulture), "Try parse failed");
            Assert.AreEqual(parsedDate.Month, 1, "Month not being parsed");
            Assert.AreEqual(parsedDate.Year, 2008, "Month not being parsed");

            Assert.IsTrue(NhsDate.TryParseExact("10-2008", out parsedDate, CultureInfo.InvariantCulture), "Try parse failed");
            Assert.AreEqual(parsedDate.Month, 10, "Month not being parsed");
            Assert.AreEqual(parsedDate.Year, 2008, "Month not being parsed");

            Assert.IsTrue(NhsDate.TryParseExact(date.ToString(), out parsedDate, CultureInfo.InvariantCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed with date, {0}", date.ToString()));

            Assert.AreEqual(date.Month, parsedDate.Month, "Parse did not error but Month does not match");
            Assert.AreEqual(date.DateType, parsedDate.DateType, "Parse did not error but DateType does not match");
            Assert.AreEqual(date.Year, parsedDate.Year, "Parse did not error but Year does not match");
        }

        /// <summary>
        /// Test Parse for a Null
        /// </summary>
        [TestMethod]
        public void ParseNullIndex()
        {
            NhsDate date = new NhsDate();

            date.DateType = DateType.NullIndex;
            date.NullIndex = 14;
            
            // Muddy the water by assigning Month and year
            date.Year = 2007;
            date.Month = 3;

            NhsDate parsedDate;

            Assert.IsTrue(NhsDate.TryParseExact(date.ToString(), out parsedDate, CultureInfo.InvariantCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed with date, {0}", date.ToString()));

            Assert.AreEqual(date.DateType, parsedDate.DateType, "Parse did not error but DateType does not match");
            Assert.AreEqual(date.NullIndex, parsedDate.NullIndex, "Parse did not error but NullIndex does not match");
        }

        /// <summary>
        /// Test Parse for an Invalid NullIndex
        /// </summary>
        [TestMethod]
        public void ParseInvalidNullIndex()
        {
            NhsDate parsedDate;

            string invalidNullIndexString = "Null:-256";

            Assert.IsFalse(NhsDate.TryParseExact(invalidNullIndexString, out parsedDate, CultureInfo.InvariantCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed to return false with string of, {0}", invalidNullIndexString));

            invalidNullIndexString = "Null:124";

            Assert.IsFalse(NhsDate.TryParseExact(invalidNullIndexString, out parsedDate, CultureInfo.InvariantCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed to return false with string of, {0}", invalidNullIndexString));
        }

        /// <summary>
        /// Test Parse for a free text
        /// </summary>
        [TestMethod]
        public void ParseDateText()
        {
            NhsDate date = new NhsDate("26-March-1974");

            NhsDate parsedDate;

            Assert.IsTrue(NhsDate.TryParseExact(date.ToString(), out parsedDate, CultureInfo.InvariantCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed with date, {0}", date.ToString()));

            Assert.AreEqual(date.DateType, parsedDate.DateType, "Parse did not error but DateType does not match");
            Assert.AreEqual(date.DateValue, parsedDate.DateValue, "Parse did not error but DateValue does not match");
        }

        /// <summary>
        /// Test Parse null
        ///</summary>
        [TestMethod()]
        public void ParseNull()
        {
            NhsDate date = NhsDate.ParseExact(null, CultureInfo.CurrentCulture);
            Assert.AreEqual<DateType>(date.DateType, DateType.Null, "Parse did not error but DateType does not match");
        }

        /// <summary>
        /// Test Parse empty string
        ///</summary>
        [TestMethod()]
        public void ParseEmptyString()
        {
            NhsDate date = NhsDate.ParseExact(string.Empty, CultureInfo.CurrentCulture);
            Assert.AreEqual<DateType>(date.DateType, DateType.Null, "Parse did not error but DateType does not match");
        }

        /// <summary>
        /// Test tryParse to see thatit gets a False when it hsould
        ///</summary>
        [TestMethod()]
        public void TryParseFailures()
        {
            NhsDate result;

            Assert.IsFalse(NhsDate.TryParseExact("31-12", out result, CultureInfo.CurrentCulture));

            Assert.IsFalse(NhsDate.TryParseExact("01-2008", out result, CultureInfo.CurrentCulture));

            Assert.IsFalse(NhsDate.TryParseExact("-12", out result, CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Test IsNull Property
        ///</summary>
        [TestMethod()]
        public void IsNullProperty()
        {
            NhsDate date = new NhsDate();

            // default datetype is exact
            Assert.IsFalse(date.IsNull);
            date.DateType = DateType.Null;
            Assert.IsTrue(date.IsNull);
        }

        /// <summary>
        /// Test the ArgumentNullException for IsAddInstruction method
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from IsAddInstruction method")]
        public void NullIsAddInstructionTest()
        {
            NhsDate.IsAddValid(null);
        }
    }
}
