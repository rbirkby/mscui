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
// <summary>Unit tests for timespan label control</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Unit tests for timespan label control
    /// </summary>
    [TestClass]
    public class TimeSpanLabelTest
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeSpanLabelTest()
        {
        }

        /// <summary>
        /// Test UnitLength property
        /// </summary>
        [TestMethod]
        public void UnitLengthTest()
        {
            TimeSpanUnitLength testValue = TimeSpanUnitLength.Long;

            TimeSpanLabel label = new TimeSpanLabel();
            Assert.AreEqual<TimeSpanUnitLength>(TimeSpanUnitLength.Short, label.UnitLength, "Expected UnitLength to initially be Short");

            label.UnitLength = testValue;
            Assert.AreEqual<TimeSpanUnitLength>(testValue, label.UnitLength, "UnitLength property not set as expected");
        }
    }
}
