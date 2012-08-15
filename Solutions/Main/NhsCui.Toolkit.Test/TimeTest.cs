//-----------------------------------------------------------------------
// <copyright file="TimeTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests to test the Time class in the NhsCui.Toolkit.DateAndTime namespace</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Test
{
    using System;
    using System.Text;
    using System.Diagnostics.CodeAnalysis;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Unit tests to test the Time class in the NhsCui.Toolkit.DateAndTime namespace
    /// </summary>
    [TestClass]
    public class TimeTest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public TimeTest()
        {
        }

        /// <summary>
        /// Test the ArgumentNullException for instruction parameter
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
        public void NullInstructionTimeTwoParametersAddTest()
        {
            DateTime baseDateTime = DateTime.Now;
            NhsTime time = new NhsTime(baseDateTime);
            NhsTime.Add(time, null);
        }

        /// <summary>
        /// Test the ArgumentNullException for sourcetime parameter
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
        public void NullSourceTimeAddTest()
        {
            NhsTime.Add(null, string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit));
        }

        /// <summary>
        /// Test the ArgumentOutOfRangeException for instruction parameter
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException), "Should throw ArgumentOutOfRangeException from Add method")]
        public void InvalidInstructionTimeAddTest()
        {
            DateTime baseDateTime = DateTime.Now;
            NhsTime time = new NhsTime(baseDateTime);
            NhsTime.Add(time, string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.resourceCulture));
        }

        /// <summary>
        /// Test the ArgumentNullException for instruction parameter for Add(string instruction) method
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
        public void NullInstructionTimeOneParameterAddTest()
        {
            DateTime baseDateTime = DateTime.Now;
            NhsTime time = new NhsTime(baseDateTime);
            time.Add(null);
        }

        /// <summary>
        /// Test the ArgumentNullException for instruction parameter for IsAddInstruction() method
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
        public void NullInstructionTimeIsAddInstructionTest()
        {
            NhsTime.IsAddValid(null);
        }

        /// <summary>
        /// Test the ability to do simple arithemtic updates to date by asking Time to add 1 hour
        /// </summary>
        [TestMethod]
        public void AddAddHour()
        {
            DateTime baseDateTime = DateTime.Now; // :-)

            NhsTime time = new NhsTime(baseDateTime);

            Assert.IsTrue(NhsTime.IsAddValid(string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit)));

            time.Add(string.Format(CultureInfo.CurrentCulture, "+1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit));

            Assert.AreEqual(time.TimeValue, baseDateTime.AddHours(1));
                        
            // Now try it without the "+", this will check that + is the default
            // Reset
            baseDateTime = DateTime.Now;
            time = new NhsTime(baseDateTime);

            Assert.IsTrue(NhsTime.IsAddValid(string.Format(CultureInfo.CurrentCulture, "1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit)), "1h was not recognised as a Time Add Instruction");

            time.Add(string.Format(CultureInfo.CurrentCulture, "1{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit));

            Assert.AreEqual(time.TimeValue, baseDateTime.AddHours(1));
        }

        /// <summary>
        /// Test the ability to do complex arithemtic updates to date by asking Time to add 1 hour and 5 minutes
        /// </summary>
        [TestMethod]
        public void AddAddHoursAndMinutes()
        {
            DateTime baseDateTime = DateTime.Now; // :-)

            NhsTime time = new NhsTime(baseDateTime);

            string addInstruction = string.Format(CultureInfo.CurrentCulture, "+1{0}+5{1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit, NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.MinutesUnit);

            Assert.IsTrue(NhsTime.IsAddValid(addInstruction), string.Format(CultureInfo.CurrentCulture, "{0} was not recognised as an Time Add Instruction", addInstruction));

            time.Add(addInstruction);

            Assert.AreEqual(time.TimeValue, baseDateTime.AddHours(1).AddMinutes(5), string.Format(CultureInfo.CurrentCulture, "Failed using {0}", addInstruction));

            // Now try without +

            // Reset
            baseDateTime = DateTime.Now;
            time = new NhsTime(baseDateTime);

            // reinitialise add instructionstring as instruction without operands

            addInstruction = string.Format(CultureInfo.CurrentCulture, "1{0}5{1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit, NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.MinutesUnit);

            Assert.IsTrue(NhsTime.IsAddValid(addInstruction), string.Format(CultureInfo.CurrentCulture, "{0} was not recognised as an Time Add Instruction", addInstruction));

            time.Add(addInstruction);

            Assert.AreEqual(time.TimeValue, baseDateTime.AddHours(1).AddMinutes(5), string.Format(CultureInfo.CurrentCulture, "Failed using {0}", addInstruction));
        }
        
        /// <summary>
        /// Test the ability to do simple arithemtic updates to time by asking Time to Subtract 5 hours
        /// </summary>
        [TestMethod]
        public void AddSubtract5Hours()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; // :-)

            NhsTime time = new NhsTime(nowAsItWasAtTheStartofTheTest);

            string addInstruction = string.Format(CultureInfo.CurrentCulture, "-5{0}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit);

            Assert.IsTrue(NhsTime.IsAddValid(addInstruction), string.Format(CultureInfo.CurrentCulture, "{0} was not recognised as a Time Add Instruction", addInstruction));

            time.Add(addInstruction);

            Assert.AreEqual(nowAsItWasAtTheStartofTheTest.AddHours(-5), time.TimeValue);
        }

        /// <summary>
        /// Test the ability to do complex arithemtic updates to time by asking Time to Subtract 2 hours AND add 25 minutes
        /// </summary>
        [TestMethod]
        public void AddSubtract2HoursAdd25Minutes()
        {
            DateTime nowAsItWasAtTheStartofTheTest = DateTime.Now; // :-)

            NhsTime time = new NhsTime(nowAsItWasAtTheStartofTheTest);

            string addInstruction = string.Format(CultureInfo.CurrentCulture, "-2{0}+25{1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.HoursUnit, NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.MinutesUnit);

            Assert.IsTrue(NhsTime.IsAddValid(addInstruction), string.Format(CultureInfo.CurrentCulture, "{0} was not recognised as a Time Add Instruction", addInstruction));

            time.Add(addInstruction);

            Assert.AreEqual(nowAsItWasAtTheStartofTheTest.AddHours(-2).AddMinutes(25), time.TimeValue);
        }       

        /// <summary>
        /// Test ToString for TimeType Approx
        /// </summary>
        [TestMethod]
        public void ToStringApproximate()
        {
            DateTime baseDateTime = System.DateTime.Now;
            GlobalizationService gc = new GlobalizationService();

            NhsTime time = new NhsTime(baseDateTime, true);

            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, "{0} {1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.Approximate, baseDateTime.ToString(gc.ShortTimePattern, CultureInfo.CurrentCulture)), time.ToString());

            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, "{0} {1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.Approximate, baseDateTime.ToString(gc.ShortTimePattern, CultureInfo.CurrentCulture)), time.ToString(true));

            time = new NhsTime(baseDateTime);

            time.TimeType = TimeType.Approximate;

            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, "{0} {1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.Approximate, baseDateTime.ToString(gc.ShortTimePattern, CultureInfo.CurrentCulture)), time.ToString());

            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, "{0} {1}", NhsCui.Toolkit.Test.NhsCui_Toolkit_DateAndTime_Resources_NhsTimeResourcesAccessor.Approximate, baseDateTime.ToString(gc.ShortTimePattern, CultureInfo.CurrentCulture)), time.ToString(true));                                                
        }

        /// <summary>
        /// Test ToString can output seconds
        /// </summary>
        [TestMethod]
        public void ToStringDisplaySeconds()
        {
            NhsTime time = new NhsTime();

            string formattedTime = time.ToString(false, CultureInfo.CurrentCulture, true, false, false);
            TimeSpan parsedTimeSpan = TimeSpan.Parse(formattedTime);

            Assert.AreEqual<int>(parsedTimeSpan.Seconds, time.TimeValue.Second, "NhsTime DisplaySeconds incorrect");
        }

        /// <summary>
        /// Test ToString display in 12 / 24 hour clock
        /// </summary>
        [TestMethod]
        public void ToStringDisplay12Hour()
        {
            int hours = 14;
            DateTime dateTime = new DateTime(2006, 1, 1, hours, 20, 11);
            NhsTime time = new NhsTime(dateTime);

            string formattedTime = time.ToString(false, CultureInfo.CurrentCulture, false, true, false);
            DateTime parsedDateTime = DateTime.Parse(formattedTime, CultureInfo.CurrentCulture);

            Assert.AreEqual<int>(parsedDateTime.Hour, hours - 12, "NhsTime Display12Hour incorrect");
        }

        /// <summary>
        /// Test ToString display in AMPM indicator
        /// </summary>
        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public void ToStringDisplayAMPM()
        {
            NhsTime morningTime = NhsTime.ParseExact("10:00:00", CultureInfo.CurrentCulture);
            NhsTime afternoonTime = NhsTime.ParseExact("15:00:00", CultureInfo.CurrentCulture);

            string formattedMorningTime = morningTime.ToString(false, CultureInfo.CurrentCulture, false, true, true);
            string formattedAfternoonTime = afternoonTime.ToString(false, CultureInfo.CurrentCulture, false, true, true);
            string morningDesignator = CultureInfo.CurrentCulture.DateTimeFormat.AMDesignator.ToLower(CultureInfo.CurrentCulture);
            string afternoonDesignator = CultureInfo.CurrentCulture.DateTimeFormat.PMDesignator.ToLower(CultureInfo.CurrentCulture);
            Assert.IsTrue(formattedMorningTime.Contains(morningDesignator));
            Assert.IsTrue(formattedAfternoonTime.Contains(afternoonDesignator));
        }       

        /// <summary>
        /// Test Parse for an NhsTime set to a time (not approx)
        /// </summary>
        [TestMethod]
        public void ParseNotApproximate()
        {
            System.DateTime baseDateTime = System.DateTime.Now;

            NhsTime time = new NhsTime(baseDateTime);

            Assert.AreEqual<TimeType>(time.TimeType, TimeType.Exact, "TimeType should be Exact when set to a date");

            NhsTime parsedTime;

            Assert.IsTrue(NhsTime.TryParseExact(time.ToString(), out parsedTime, CultureInfo.CurrentCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed with time, {0}", time.ToString()));

            Assert.AreEqual(time.TimeType, parsedTime.TimeType, "Parse did not error but TimeType does not match");

            Assert.AreEqual<string>(time.TimeValue.ToString("HH:mm", CultureInfo.InvariantCulture), parsedTime.TimeValue.ToString("HH:mm", CultureInfo.InvariantCulture), "Parse did not error but TimeValue does not match");
        }

        /// <summary>
        /// Test Parse for an NhsTime set to a time (not approx, but approx = true passed to ToString)
        /// </summary>
        [TestMethod, 
        ExpectedException(typeof(System.ArgumentOutOfRangeException), "Should throw ArgumentOutOfRangeException because oassing True is only valid when TimeType is Approximate")]
        public void ParseNotApproximateTruePassedToToString()
        {
            System.DateTime baseDateTime = System.DateTime.Now;
            
            NhsTime time = new NhsTime(baseDateTime);

            Assert.AreEqual<TimeType>(time.TimeType, TimeType.Exact, "TimeType should be Exact when set to a date");

            NhsTime.ParseExact(time.ToString(true), CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Test Parse for an NhsTime set to a time (IS approx)
        /// </summary>
        [TestMethod]
        public void ParseApproximate()
        {
            System.DateTime baseDateTime = System.DateTime.Now;

            NhsTime time = new NhsTime(baseDateTime, true);

            Assert.AreEqual<TimeType>(time.TimeType, TimeType.Approximate, "TimeType should be Approxinmate when set to a date");

            NhsTime parsedTime;

            Assert.IsTrue(NhsTime.TryParseExact(time.ToString(), out parsedTime, CultureInfo.CurrentCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed with time, {0}", time.ToString()));

            Assert.AreEqual(time.TimeType, parsedTime.TimeType, "Parse did not error but TimeType does not match");
            Assert.AreEqual<string>(time.TimeValue.ToString("HH:mm", CultureInfo.InvariantCulture), parsedTime.TimeValue.ToString("HH:mm", CultureInfo.InvariantCulture), "Parse did not error but TimeValue does not match");
        }

        /// <summary>
        /// Test Parse for a Null
        /// </summary>
        [TestMethod]
        public void ParseNullIndex()
        {
            NhsTime time = new NhsTime();

            time.TimeType = TimeType.NullIndex;
            time.NullIndex = 4;           
            
            NhsTime parsedTime;

            Assert.IsTrue(NhsTime.TryParseExact(time.ToString(), out parsedTime, CultureInfo.CurrentCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed with time, {0}", time.ToString()));

            Assert.AreEqual<TimeType>(time.TimeType, parsedTime.TimeType, "Parse did not error but TimeType does not match");
            Assert.AreEqual(time.NullIndex, parsedTime.NullIndex, "Parse did not error but NullIndex does not match");
        }

        /// <summary>
        /// Test Parse for an Invalid NullIndex
        /// </summary>
        [TestMethod]
        public void ParseInvalidNullIndex()
        {
            NhsTime parsedTime;

            string invalidNullIndexString = "Null:-256";

            Assert.IsFalse(NhsTime.TryParseExact(invalidNullIndexString, out parsedTime, CultureInfo.CurrentCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed to return false with string of, {0}", invalidNullIndexString));

            invalidNullIndexString = "Null:124";

            Assert.IsFalse(NhsTime.TryParseExact(invalidNullIndexString, out parsedTime, CultureInfo.CurrentCulture), string.Format(CultureInfo.InvariantCulture, "TryParse failed to return false with string of, {0}", invalidNullIndexString));
        }

        /// <summary>
        /// Test Parse for an NhsTime set to Afternoon
        /// </summary>
        [TestMethod,
        ExpectedException(typeof(System.FormatException), "Should throw format exception because of badly formatted text")]
        public void ParseShouldFail()
        {
            NhsTime.ParseExact("barf", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Test Parse for an NhsTime set to a time (not approx) with seconds
        /// </summary>
        [TestMethod]
        public void ParseWithSeconds()
        {
            NhsTime time = NhsTime.ParseExact("10:30:23", CultureInfo.CurrentCulture);

            Assert.AreEqual<int>(time.TimeValue.Second, 23, "Parse failed to set seconds");
        }

        /// <summary>
        /// Test Parse with AM designator
        /// </summary>
        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public void ParseWithAMPMDesignator()
        {
            GlobalizationService gs = new GlobalizationService();
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString(gs.ShortTimePatternWithSecondsAMPM, CultureInfo.CurrentCulture);
            NhsTime time = NhsTime.ParseExact(formattedTime, CultureInfo.CurrentCulture);

            Assert.AreEqual<int>(now.Hour, time.TimeValue.Hour, "Parse failed to set hours with am/pm designator");
            Assert.AreEqual<int>(now.Minute, time.TimeValue.Minute, "Parse failed to set minutes with am/pm designator");
            Assert.AreEqual<int>(now.Second, time.TimeValue.Second, "Parse failed to set seconds with am/pm designator");
        }

        /// <summary>
        /// Test Parse null
        ///</summary>
        [TestMethod()]
        public void ParseNull()
        {
            NhsTime time = NhsTime.ParseExact(null, CultureInfo.CurrentCulture);
            Assert.AreEqual<TimeType>(time.TimeType, TimeType.Null, "Parse did not error but TimeType does not match");
        }

        /// <summary>
        /// Test Parse empty string
        ///</summary>
        [TestMethod()]
        public void ParseEmptyString()
        {
            NhsTime time = NhsTime.ParseExact(string.Empty, CultureInfo.CurrentCulture);
            Assert.AreEqual<TimeType>(time.TimeType, TimeType.Null, "Parse did not error but TimeType does not match");
        }

        /// <summary>
        /// Test IsNull Property
        ///</summary>
        [TestMethod()]
        public void IsNullProperty()
        {
            NhsTime time = new NhsTime();

            // default datetype is exact
            Assert.IsFalse(time.IsNull);
            time.TimeType = TimeType.Null;
            Assert.IsTrue(time.IsNull);
        }
    }
}
