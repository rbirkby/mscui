//-----------------------------------------------------------------------
// <copyright file="TimeSpanLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for NhsTimeSpan label control</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Web.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    using NhsCui.Toolkit.Web;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Unit tests for NhsTimeSpan label control
    /// </summary>
    [TestClass]
    public class TimeSpanLabelTest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public TimeSpanLabelTest()
        {
        }

        /// <summary>
        /// Test the TimeSpanLabel , more specifically test assignment to the From property
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void Write_ToFrom()
        {
            DateTime baseDateTime = new DateTime(2008, 12, 12);

            TimeSpanLabel testLabel = new TimeSpanLabel();

            testLabel.From = baseDateTime;

            Assert.AreEqual<DateTime>(baseDateTime, testLabel.From);
        }

        /// <summary>
        /// Test the TimeSpanLabel , more specifically test assignment to the To property
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void Write_ToTo()
        {
            DateTime baseDateTime = new DateTime(2066, 12, 12);

            TimeSpanLabel testLabel = new TimeSpanLabel();

            testLabel.To = baseDateTime;

            Assert.AreEqual<DateTime>(baseDateTime, testLabel.To);
        }

        /// <summary>
        /// Test the TimeSpanLabel, more specifically test assignment to the IsAge property
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void Write_ToIsAge()
        {
            TimeSpanLabel testLabel = new TimeSpanLabel();

            testLabel.IsAge = true;

            Assert.AreEqual<bool>(true, testLabel.IsAge);
        }

        /// <summary>
        /// Test the TimeSpanLabel, more specifically test assignment to the Granularity property
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void Write_ToGranularity()
        {
            TimeSpanLabel testLabel = new TimeSpanLabel();

            testLabel.Granularity = TimeSpanUnit.Weeks;

            Assert.AreEqual<TimeSpanUnit>(TimeSpanUnit.Weeks, testLabel.Granularity);
        }

        /// <summary>
        /// Test the TimeSpanLabel, more specifically test assignment to the Threshold property
        /// </summary>
        [TestMethod,
       SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void Write_ToThreshold()
        {
            TimeSpanLabel testLabel = new TimeSpanLabel();

            testLabel.Threshold = TimeSpanUnit.Days;

            Assert.AreEqual<TimeSpanUnit>(TimeSpanUnit.Days, testLabel.Threshold);
        }

        /// <summary>
        /// Test UnitLength property
        /// </summary>
        [TestMethod]
        public void UnitLengthProperty()
        {
            TimeSpanLabel testLabel = new TimeSpanLabel();
            Assert.AreEqual<TimeSpanUnitLength>(testLabel.UnitLength, TimeSpanUnitLength.Short, "Expected default value of UnitLength property to be short");

            testLabel.UnitLength = TimeSpanUnitLength.Long;
            Assert.AreEqual<TimeSpanUnitLength>(testLabel.UnitLength, TimeSpanUnitLength.Long, "UnitLength property could not be set");
        }
    }
}
