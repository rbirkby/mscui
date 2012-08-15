//-----------------------------------------------------------------------
// <copyright file="PatientNameTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Patient Name class test</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Test
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Patient Name class test
    /// </summary>
    [TestClass]
    public class PatientNameTest
    {
        /// <summary>
        /// constructor
        /// </summary>
        public PatientNameTest()
        {
        }

        /// <summary>
        /// Test Format method
        /// </summary>
        [TestMethod]
        public void FormatNameTest()
        {
            string formattedName = PatientName.Format(string.Empty, string.Empty, string.Empty);
            Assert.AreEqual(formattedName, string.Empty, "Expected formatted name to initially be empty string");

            formattedName = PatientName.Format("Evans", string.Empty, string.Empty);
            Assert.AreEqual(formattedName, "EVANS", "Expected formatted name to be 'EVANS'");

            formattedName = PatientName.Format("Evans", "John", string.Empty);
            Assert.AreEqual(formattedName, "EVANS, John", "Expected formatted name to be 'EVANS John'");

            formattedName = PatientName.Format("Evans", "John", "Mr");
            Assert.AreEqual(formattedName, "EVANS, John (Mr)", "Expected formatted name to be 'EVANS John (Mr)'");

            formattedName = PatientName.Format(string.Empty, "John", "Mr");
            Assert.AreEqual(formattedName, "John (Mr)", "Expected formatted name to be 'John (Mr)'");

            formattedName = PatientName.Format(string.Empty, string.Empty, "Mr");
            Assert.AreEqual(formattedName, string.Empty, "Expected formatted name to be ''");

            formattedName = PatientName.Format(string.Empty, "John", string.Empty);
            Assert.AreEqual(formattedName, "John", "Expected formatted name to be 'John'");
        }

        /// <summary>
        /// Test formatted name property at maximum lengths
        /// </summary>
        [TestMethod]
        public void FormatNameMaximumLengthTest()
        {
            string familyName = "VeryLongFamilyName9012345678901234567890";

            string givenName = "VeryLongGivenName89012345678901234567890";
            string title = "VeryLongTitle8901234567890123456789";
            string formattedName = PatientName.Format(familyName, givenName, title);
            Assert.AreEqual(
                        formattedName,
                        familyName.ToUpper(CultureInfo.CurrentCulture) + ", " + givenName + " (" + title + ")",
                        "Max length formatted name not as expected");
        }

        /// <summary>
        /// Test formatted name property truncates names as expected
        /// </summary>
        [TestMethod]
        public void FormatNameTruncationTest()
        {
            string familyName = "VeryLongFamilyName9012345678901234567890ThisShouldBeTruncated";

            string givenName = "VeryLongGivenName89012345678901234567890ThisShouldBeTruncated";
            string title = "VeryLongTitle8901234567890123456789ThisShouldBeTruncated";
            string formattedName = PatientName.Format(familyName, givenName, title);
            Assert.AreEqual(
                        formattedName,
                        "VERYLONGFAMILYNAME9012345678901234567..., VeryLongGivenName89012345678901234567... (VeryLongTitle8901234567890123456...)",
                        "Formatted name not truncated as expected");
        }

        /// <summary>
        /// Test the FormatNameArray method
        ///</summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.AreEqual(System.Collections.ICollection,System.Collections.ICollection,System.String)")]
        public void PatientNameFormatNameArrayTest()
        {
            string familyName = "Evans";

            string givenName = "Jane";

            string title = "Ms.";

            string[] expected = new string[3] { "EVANS, ", "Jane", " (Ms.)" };
            string[] actual;

            actual = NhsCui.Toolkit.PatientName.FormatNameArray(familyName, givenName, title);

            CollectionAssert.AreEqual(expected, actual, "NhsCui.Toolkit.PatientName.FormatNameArray did not return the expected value.");
        }

        /// <summary>
        ///A test for FormatGivenName (string)
        ///</summary>
        [TestMethod]
        public void PatientNameFormatGivenNameTest()
        {
            string familyName = "Jane";
            string expected = "Jane";
            string actual;

            actual = NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientNameAccessor.FormatGivenName(familyName);

            Assert.AreEqual(expected, actual, "NhsCui.Toolkit.PatientName.FormatGivenName did not return the expected value.");
        }

        /// <summary>
        ///A test for Null GivenName
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw null argument exception from FormatGivenName method")]
        public void PatientNameNullGivenNameTest()
        {
            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientNameAccessor.FormatGivenName(null);
        }

        /// <summary>
        ///A test for FormatFamilyName (string)
        ///</summary>
        [TestMethod]
        public void PatientNameFormatFamilyNameTest()
        {
            string familyName = "Evans";
            string expected = "EVANS";
            string actual;

            actual = NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientNameAccessor.FormatFamilyName(familyName);

            Assert.AreEqual(expected, actual, "NhsCui.Toolkit.PatientName.FormatFamilyName did not return the expected value.");
        }

        /// <summary>
        ///A test for Null FamilyName
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw null argument exception from FormatFamilyName method")]
        public void PatientNameNullFamilyNameTest()
        {
            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientNameAccessor.FormatFamilyName(null);
        }

        /// <summary>
        ///A test for FormatTitle (string)
        ///</summary>
        [TestMethod]
        public void PatientNameFormatTitleTest()
        {
            string title = "Ms.";
            string expected = " (Ms.)";
            string actual;

            actual = NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientNameAccessor.FormatTitle(title);

            Assert.AreEqual(expected, actual, "NhsCui.Toolkit.PatientName.FormatTitle did not return the expected value.");
        }
        
        /// <summary>
        ///A test for Null Title
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw null argument exception from FormatTitle method")]
        public void PatientNameNullTitleTest()
        {
            NhsCui.Toolkit.Test.NhsCui_Toolkit_PatientNameAccessor.FormatTitle(null);
        }
    }
}
