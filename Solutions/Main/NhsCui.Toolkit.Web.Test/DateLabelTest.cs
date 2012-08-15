//-----------------------------------------------------------------------
// <copyright file="DateLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for NhsDate label control</summary>
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
    /// Unit tests for NhsDate label control
    /// </summary>
    [TestClass]
    public class DateLabelTest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public DateLabelTest()
        {
        }

        /// <summary>
        /// Test the DateLabel , more specifically test assignment to the DateValue property
        /// </summary>
        [TestMethod]
        public void SetDateValue()
        {
            DateTime baseDateTime = new DateTime(2008, 12, 12);

            DateLabel testLabel = new DateLabel();

            testLabel.DateValue = baseDateTime;

            Assert.AreEqual<DateTime>(baseDateTime, testLabel.DateValue);
        }

        /// <summary>
        /// Test the DateLabel , more specifically test assignment to the Month property
        /// </summary>
        [TestMethod]
        public void SetMonth()
        {
            DateLabel testLabel = new DateLabel();

            testLabel.Month = 3;

            Assert.AreEqual<int>(3, testLabel.Month);
        }

        /// <summary>
        /// Test the DateLabel , more specifically test assignment to the NullIndex property
        /// </summary>
        [TestMethod]
        public void SetNullIndex()
        {
            DateLabel testLabel = new DateLabel();

            testLabel.NullIndex = 2;

            Assert.AreEqual<int>(2, testLabel.NullIndex);
        }   

        /// <summary>
        /// Test the DateLabel , more specifically test assignment to the Year property
        /// </summary>
        [TestMethod]
        public void SetYear()
        {
            DateLabel testLabel = new DateLabel();

            testLabel.Year = 2007;

            Assert.AreEqual<int>(2007, testLabel.Year);
        }

        /// <summary>
        /// Test the DateLabel , more specifically test assignment to the DisplayDayOfWeek property
        /// </summary>
        [TestMethod]
        public void SetDisplayDayOfWeek()
        {
            DateLabel testLabel = new DateLabel();
            Assert.AreEqual<bool>(testLabel.DisplayDayOfWeek, false, "Expecting DateLabel DisplayDayOfWeek to be initially false");

            testLabel.DisplayDayOfWeek = true;

            Assert.AreEqual<bool>(testLabel.DisplayDayOfWeek, true, "DateLabel DisplayDayOfWeek not set as expected");
        }
    }
}
