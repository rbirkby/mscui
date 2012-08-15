//-----------------------------------------------------------------------
// <copyright file="NameLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for name label control</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for name label
    /// </summary>
    [TestClass]
    public class NameLabelTest
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public NameLabelTest()
        {
        }

        /// <summary>
        /// Test LastName property
        /// </summary>
        [TestMethod]
        public void LastNameTest()
        {
            NameLabel nameLabel = new NameLabel();
            Assert.AreEqual(nameLabel.FamilyName, string.Empty, "Expected family name to initially be empty string");
            nameLabel.FamilyName = "Evans";
            Assert.AreEqual(nameLabel.FamilyName, "Evans", "Family name property not set as expected");
        }

        /// <summary>
        /// Test GivenName property
        /// </summary>
        [TestMethod]
        public void GivenNameTest()
        {
            NameLabel nameLabel = new NameLabel();
            Assert.AreEqual(nameLabel.GivenName, string.Empty, "Expected GivenName to initially be empty string");
            nameLabel.GivenName = "John";
            Assert.AreEqual(nameLabel.GivenName, "John", "GivenName property not set as expected");
        }

        /// <summary>
        /// Test Title property
        /// </summary>
        [TestMethod]
        public void TitleTest()
        {
            NameLabel nameLabel = new NameLabel();
            Assert.AreEqual(nameLabel.Title, string.Empty, "Expected Title to initially be empty string");
            nameLabel.Title = "Mr";
            Assert.AreEqual(nameLabel.Title, "Mr", "Title property not set as expected");
        }

        /// <summary>
        /// Test display value property
        /// </summary>
        [TestMethod]
        public void DisplayValueTest()
        {
            NameLabel nameLabel = new NameLabel();

            Assert.AreEqual(nameLabel.DisplayValue, string.Empty, "Expected display value to initially be empty string");

            nameLabel.FamilyName = "Evans";
            Assert.AreEqual(nameLabel.DisplayValue, "EVANS", "Expected display value to be 'EVANS'");

            nameLabel.GivenName = "John";
            Assert.AreEqual(nameLabel.DisplayValue, "EVANS, John", "Expected display value to be 'EVANS John'");

            nameLabel.Title = "Mr";
            Assert.AreEqual(nameLabel.DisplayValue, "EVANS, John (Mr)", "Expected display value to be 'EVANS John (Mr)'");

            nameLabel.FamilyName = string.Empty;
            Assert.AreEqual(nameLabel.DisplayValue, "John (Mr)", "Expected display value to be 'John (Mr)'");

            nameLabel.GivenName = string.Empty;
            Assert.AreEqual(nameLabel.DisplayValue, string.Empty, "Expected display value to be ''");

            nameLabel.GivenName = "John";
            nameLabel.Title = string.Empty;
            Assert.AreEqual(nameLabel.DisplayValue, "John", "Expected display value to be 'John'");
        }

        /// <summary>
        /// Test display value property at maximum lengths
        /// </summary>
        [TestMethod]
        public void DisplayValueMaximumLengthTest()
        {
            NameLabel nameLabel = new NameLabel();
            nameLabel.FamilyName = "VeryLongFamilyName9012345678901234567890";

            nameLabel.GivenName = "VeryLongGivenName89012345678901234567890";
            nameLabel.Title = "VeryLongTitle8901234567890123456789";
            Assert.AreEqual(
                        nameLabel.DisplayValue,
                        "VERYLONGFAMILYNAME9012345678901234567890, VeryLongGivenName89012345678901234567890 (VeryLongTitle8901234567890123456789)",
                        "Max length DisplayValue not as expected");
        }

        /// <summary>
        /// Test display value property truncates names as expected
        /// </summary>
        [TestMethod]
        public void DisplayValueTruncationTest()
        {
            NameLabel nameLabel = new NameLabel();
            nameLabel.FamilyName = "VeryLongFamilyName9012345678901234567890ThisShouldBeTruncated";

            nameLabel.GivenName = "VeryLongGivenName89012345678901234567890ThisShouldBeTruncated";
            nameLabel.Title = "VeryLongTitle8901234567890123456789ThisShouldBeTruncated";
            Assert.AreEqual(
                        nameLabel.DisplayValue, 
                        "VERYLONGFAMILYNAME9012345678901234567..., VeryLongGivenName89012345678901234567... (VeryLongTitle8901234567890123456...)", 
                        "DisplayValue not truncated as expected");
        }
   }
}
