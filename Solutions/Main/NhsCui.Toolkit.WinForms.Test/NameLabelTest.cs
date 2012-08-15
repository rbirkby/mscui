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

namespace NhsCui.Toolkit.WinForms.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Globalization;

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
        /// Test FamilyName property
        /// </summary>
        [TestMethod]
        public void FamilyNameTest()
        {
            string testValue = "Evans";

            NameLabel nameLabel = new NameLabel();
            Assert.AreEqual<string>(string.Empty, nameLabel.FamilyName, "Expected FamilyName to initially be string.Empty");

            nameLabel.FamilyName = testValue;
            Assert.AreEqual<string>(testValue, nameLabel.FamilyName, "FamilyName property not set as expected");
        }

        /// <summary>
        /// Test FamilyName length
        /// </summary>
        [TestMethod]
        public void FamilyNameLengthTest()
        {
            string testValueTooLong = "Harcourt-Fotherington-Cholmondley-Chillingsworth-Montgomery";
            string testValueExpected = "HARCOURT-FOTHERINGTON-CHOLMONDLEY-CHI...";

            NameLabel nameLabel = new NameLabel();
            nameLabel.FamilyName = testValueTooLong;
            Assert.AreEqual<string>(testValueExpected, nameLabel.DisplayValue, "FamilyName should have been truncated in DisplayValue");
        }

        /// <summary>
        /// Test GivenName property
        /// </summary>
        [TestMethod]
        public void GivenNameTest()
        {
            string testValue = "Jane";

            NameLabel nameLabel = new NameLabel();
            Assert.AreEqual<string>(string.Empty, nameLabel.GivenName, "Expected GivenName to initially be string.Empty");

            nameLabel.GivenName = testValue;
            Assert.AreEqual<string>(testValue, nameLabel.GivenName, "GivenName property not set as expected");
        }

        /// <summary>
        /// Test GivenName length
        /// </summary>
        [TestMethod]
        public void GivenNameLengthTest()
        {
            string testValueTooLong = "Susan Kathleen Mary Bridgette Concepta Caitlin Margaret Theresa";
            string testValueExpected = "Susan Kathleen Mary Bridgette Concept...";

            NameLabel nameLabel = new NameLabel();
            nameLabel.GivenName = testValueTooLong;
            Assert.AreEqual<string>(testValueExpected, nameLabel.DisplayValue, "GivenName should have been truncated in DisplayValue");
        }

        /// <summary>
        /// Test Title property
        /// </summary>
        [TestMethod]
        public void TitleTest()
        {
            string testValue = "(Mr)";

            NameLabel nameLabel = new NameLabel();
            Assert.AreEqual<string>(string.Empty, nameLabel.Title, "Expected Title to initially be string.Empty");

            nameLabel.Title = testValue;
            Assert.AreEqual<string>(testValue, nameLabel.Title, "Title property not set as expected");
        }

        /// <summary>
        /// Test Title length
        /// </summary>
        [TestMethod]
        public void TitleLengthTest()
        {
            string testValueTooLong = "The Right-Honourable, His Most-High-Excellency";
            string testValueExpected = "SMITH, John (The Right-Honourable, His Most-H...)";

            NameLabel nameLabel = new NameLabel();
            nameLabel.Title = testValueTooLong;
            nameLabel.GivenName = "John";
            nameLabel.FamilyName = "Smith";
            Assert.AreEqual<string>(testValueExpected, nameLabel.DisplayValue, "Title should have been truncated in DisplayValue");
        }

        /// <summary>
        /// Test display value property
        /// </summary>
        [TestMethod]
        public void DisplayValueTest()
        {
            string testFamilyNameInput = "Evans";
            string testGivenName = "Jane";
            string testTitle = "Ms.";

            string testFamilyNameOutput = "EVANS";

            string testLastFirstEntryOutput = testFamilyNameOutput + ", " + testGivenName;
            string testFullEntryOutput = testLastFirstEntryOutput + " (" + testTitle + ")";
            string testFirstTitleEntryOutput = testGivenName + " (" + testTitle + ")";

            NameLabel nameLabel = new NameLabel();

            Assert.AreEqual<string>(string.Empty, nameLabel.DisplayValue, "Expected display value to initially be string.Empty");

            nameLabel.FamilyName = testFamilyNameInput;
            Assert.AreEqual<string>(testFamilyNameOutput, nameLabel.DisplayValue, string.Format(CultureInfo.InvariantCulture, "Expected display value to be '{0}'", testFamilyNameOutput));

            nameLabel.GivenName = testGivenName;
            Assert.AreEqual<string>(testLastFirstEntryOutput, nameLabel.DisplayValue, string.Format(CultureInfo.InvariantCulture, "Expected display value to be '{0}'", testLastFirstEntryOutput));

            nameLabel.Title = testTitle;
            Assert.AreEqual<string>(testFullEntryOutput, nameLabel.DisplayValue, string.Format(CultureInfo.InvariantCulture, "Expected display value to be '{0}'", testFullEntryOutput));

            nameLabel.FamilyName = string.Empty;
            Assert.AreEqual<string>(testFirstTitleEntryOutput, nameLabel.DisplayValue, string.Format(CultureInfo.InvariantCulture, "Expected display value to be '{0}'", testFirstTitleEntryOutput));

            nameLabel.GivenName = string.Empty;
            Assert.AreEqual<string>(string.Empty, nameLabel.DisplayValue, "Expected display value to be string.Empty");

            nameLabel.GivenName = testGivenName;
            nameLabel.Title = string.Empty;
            Assert.AreEqual<string>(testGivenName, nameLabel.DisplayValue, string.Format(CultureInfo.InvariantCulture, "Expected display value to be '{0}'", testGivenName));
        }
    }
}
