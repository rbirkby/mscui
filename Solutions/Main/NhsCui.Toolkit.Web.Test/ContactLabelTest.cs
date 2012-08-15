//-----------------------------------------------------------------------
// <copyright file="ContactLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for contact label control</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for contact label control
    /// </summary>
    [TestClass]
    public class ContactLabelTest
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ContactLabelTest()
        {
        }

        /// <summary>
        /// HomePhoneNumber property test
        /// </summary>
        [TestMethod]
        public void HomePhoneNumberTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.HomePhoneNumber, string.Empty, "Expected HomePhoneNumber to initially be empty string");
            contactLabel.HomePhoneNumber = "(01332) 5678900";
            Assert.AreEqual(contactLabel.HomePhoneNumber, "(01332) 5678900", "HomePhoneNumber property not set as expected");
        }

        /// <summary>
        /// WorkPhoneNumber property test
        /// </summary>
        [TestMethod]
        public void WorkPhoneNumberTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.WorkPhoneNumber, string.Empty, "Expected WorkPhoneNumber to initially be empty string");
            contactLabel.WorkPhoneNumber = "(02345) 1232455";
            Assert.AreEqual(contactLabel.WorkPhoneNumber, "(02345) 1232455", "WorkPhoneNumber property not set as expected");
        }

        /// <summary>
        /// MobilePhoneNumber property test
        /// </summary>
        [TestMethod]
        public void MobilePhoneNumberTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.MobilePhoneNumber, string.Empty, "Expected MobilePhoneNumber to initially be empty string");
            contactLabel.MobilePhoneNumber = "(06678) 5787446";
            Assert.AreEqual(contactLabel.MobilePhoneNumber, "(06678) 5787446", "MobilePhoneNumber property not set as expected");
        }

        /// <summary>
        /// EmailAddress property test
        /// </summary>
        [TestMethod]
        public void EmailAddressTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.EmailAddress, string.Empty, "Expected EmailAddress to initially be empty string");
            contactLabel.EmailAddress = "jane.evans@yahoo.com";
            Assert.AreEqual(contactLabel.EmailAddress, "jane.evans@yahoo.com", "EmailAddress property not set as expected");
        }

        /// <summary>
        /// HomePhoneLabelText property test
        /// </summary>
        [TestMethod]
        public void HomePhoneLabelTextTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.HomePhoneLabelText, "Home", "Expected HomePhoneLabelText to initially be 'Home'");
            contactLabel.HomePhoneLabelText = "Home Tel:";
            Assert.AreEqual(contactLabel.HomePhoneLabelText, "Home Tel:", "HomePhoneLabelText property not set as expected");
        }

        /// <summary>
        /// WorkPhoneLabelText property test
        /// </summary>
        [TestMethod]
        public void WorkPhoneLabelTextTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.WorkPhoneLabelText, "Work", "Expected WorkPhoneLabelText to initially be 'Work'");
            contactLabel.WorkPhoneLabelText = "Work Tel:";
            Assert.AreEqual(contactLabel.WorkPhoneLabelText, "Work Tel:", "WorkPhoneLabelText property not set as expected");
        }

        /// <summary>
        /// MobilePhoneLabelText property test
        /// </summary>
        [TestMethod]
        public void MobilePhoneLabelTextTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.MobilePhoneLabelText, "Mobile", "Expected MobilePhoneLabelText to initially be 'Mobile'");
            contactLabel.MobilePhoneLabelText = "Mobile Tel:";
            Assert.AreEqual(contactLabel.MobilePhoneLabelText, "Mobile Tel:", "MobilePhoneLabelText property not set as expected");
        }

        /// <summary>
        /// EmailLabelText property test
        /// </summary>
        [TestMethod]
        public void EmailLabelTextTest()
        {
            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual(contactLabel.EmailLabelText, "Email", "Expected EmailLabelText to initially be 'email'");
            contactLabel.EmailLabelText = "primary email:";
            Assert.AreEqual(contactLabel.EmailLabelText, "primary email:", "EmailLabelText property not set as expected");
        }
    }
}
