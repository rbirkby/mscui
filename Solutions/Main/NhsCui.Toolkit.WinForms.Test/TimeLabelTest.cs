//-----------------------------------------------------------------------
// <copyright file="TimeLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-Jun-2007</date>
// <summary>Unit tests for TimeLabel control</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.WinForms.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;
    using NhsCui.Toolkit.WinForms;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Unit tests for TimeLabel control
    /// </summary>
    [TestClass]
    public class TimeLabelTest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public TimeLabelTest()
        {
        }

        /// <summary>
        /// Test assignment to the TimeValue property
        /// </summary>
        [TestMethod]
        public void TimeValueProperty()
        {
            DateTime baseDateTime = new DateTime(2008, 12, 12);

            TimeLabel testLabel = new TimeLabel();

            testLabel.TimeValue = baseDateTime;

            Assert.AreEqual<DateTime>(baseDateTime, testLabel.TimeValue);
        }

        /// <summary>
        /// Test assignment to the TimeType property
        /// </summary>
        [TestMethod]
        public void TimeTypeProperty()
        {
            TimeLabel testLabel = new TimeLabel();

            testLabel.TimeType = TimeType.Approximate;

            Assert.AreEqual<TimeType>(testLabel.TimeType, TimeType.Approximate);
        }        

        /// <summary>
        /// Test assignment to the TimePeriod property
        /// </summary>
        [TestMethod]
        public void ValueProperty()
        {
            NhsTime time = new NhsTime();
            TimeLabel testLabel = new TimeLabel();

            testLabel.Value = time;

            Assert.AreEqual<NhsTime>(testLabel.Value, time);
        }

        /// <summary>
        /// Test assignment to the DisplaySeconds property
        /// </summary>
        [TestMethod]
        public void DisplaySecondsProperty()
        {
            TimeLabel testLabel = new TimeLabel();

            testLabel.DisplaySeconds = true;

            Assert.AreEqual<bool>(testLabel.DisplaySeconds, true);
        }

        /// <summary>
        /// Test assignment to the Display12Hour property
        /// </summary>
        [TestMethod]
        public void Display12HourProperty()
        {
            TimeLabel testLabel = new TimeLabel();

            testLabel.Display12Hour = true;

            Assert.AreEqual<bool>(testLabel.Display12Hour, true);
        }

        /// <summary>
        /// Test assignment to the DisplayAMPM property
        /// </summary>
        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public void DisplayAMPMProperty()
        {
            TimeLabel testLabel = new TimeLabel();

            testLabel.DisplayAMPM = true;

            Assert.AreEqual<bool>(testLabel.DisplayAMPM, true);
        }

        /// <summary>
        /// Test assignment to the NullIndex property
        /// </summary>
        [TestMethod]
        public void NullIndexProperty()
        {
            TimeLabel testLabel = new TimeLabel();

            testLabel.NullIndex = 2;

            Assert.AreEqual<int>(2, testLabel.NullIndex);
        }
    }
}
