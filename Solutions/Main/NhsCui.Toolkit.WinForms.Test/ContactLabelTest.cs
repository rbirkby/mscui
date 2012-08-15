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

namespace NhsCui.Toolkit.WinForms.Test
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
            string testValue = "01253 2960523";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>(string.Empty, contactLabel.HomePhoneNumber, "Expected HomePhoneNumber to initially be string.Empty");

            contactLabel.HomePhoneNumber = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.HomePhoneNumber, "HomePhoneNumber property not set as expected");
        }

        /// <summary>
        /// WorkPhoneNumber property test
        /// </summary>
        [TestMethod]
        public void WorkPhoneNumberTest()
        {
            string testValue = "0207 946 0901";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>(string.Empty, contactLabel.WorkPhoneNumber, "Expected WorkPhoneNumber to initially be string.Empty");

            contactLabel.WorkPhoneNumber = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.WorkPhoneNumber, "WorkPhoneNumber property not set as expected");
        }

        /// <summary>
        /// MobilePhoneNumber property test
        /// </summary>
        [TestMethod]
        public void MobilePhoneNumberTest()
        {
            string testValue = "077 009 00949";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>(string.Empty, contactLabel.MobilePhoneNumber, "Expected MobilePhoneNumber to initially be string.Empty");

            contactLabel.MobilePhoneNumber = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.MobilePhoneNumber, "MobilePhoneNumber property not set as expected");
        }

        /// <summary>
        /// EmailAddress property test
        /// </summary>
        [TestMethod]
        public void EmailAddressTest()
        {
            string testValue = "jane.evans@hotmail.com";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>(string.Empty, contactLabel.EmailAddress, "Expected EmailAddress to initially be string.Empty");

            contactLabel.EmailAddress = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.EmailAddress, "EmailAddress property not set as expected");
        }

        /// <summary>
        /// HomePhoneLabelText property test
        /// </summary>
        [TestMethod]
        public void HomePhoneLabelTextTest()
        {
            string testValue = "maison:";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>("Home", contactLabel.HomePhoneLabelText, "Expected HomePhoneLabelText to initially be 'Home'");

            contactLabel.HomePhoneLabelText = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.HomePhoneLabelText, "HomePhoneLabelText property not set as expected");
        }

        /// <summary>
        /// WorkPhoneLabelText property test
        /// </summary>
        [TestMethod]
        public void WorkPhoneLabelTextTest()
        {
            string testValue = "travail:";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>("Work", contactLabel.WorkPhoneLabelText, "Expected WorkPhoneLabelText to initially be 'Work'");

            contactLabel.WorkPhoneLabelText = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.WorkPhoneLabelText, "WorkPhoneLabelText property not set as expected");
        }

        /// <summary>
        /// MobilePhoneLabelText property test
        /// </summary>
        [TestMethod]
        public void MobilePhoneLabelTextTest()
        {
            string testValue = "mobile:";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>("Mobile", contactLabel.MobilePhoneLabelText, "Expected MobilePhoneLabelText to initially be 'Mobile'");

            contactLabel.MobilePhoneLabelText = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.MobilePhoneLabelText, "MobilePhoneLabelText property not set as expected");
        }

        /// <summary>
        /// EmailLabelText property test
        /// </summary>
        [TestMethod]
        public void EmailLabelTextTest()
        {
            string testValue = "e:";

            ContactLabel contactLabel = new ContactLabel();
            Assert.AreEqual<string>("Email", contactLabel.EmailLabelText, "Expected EmailLabelText to initially be 'Email'");

            contactLabel.EmailLabelText = testValue;
            Assert.AreEqual<string>(testValue, contactLabel.EmailLabelText, "EmailLabelText property not set as expected");
        }
    }
}
