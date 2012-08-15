//-----------------------------------------------------------------------
// <copyright file="IdentifierLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for Identifier Label control</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.WinForms.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NhsCui.Toolkit;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// IdentifierLabelTest class
    /// </summary>
    [TestClass]
    public class IdentifierLabelTest
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public IdentifierLabelTest()
        {
        }

        /// <summary>
        ///A test for IdentifierType
        ///</summary>
        [TestMethod()]
        public void IdentifierTypeTest()
        {
            IdentifierLabel target = new IdentifierLabel();

            IdentifierType val = IdentifierType.NhsNumber;

            target.Text = "4372623623";
            target.IdentifierType = val;
            Assert.AreEqual<IdentifierType>(val, target.IdentifierType, "NhsCui.Toolkit.WinForms.IdentifierLabel.IdentifierType was not set correctly.");

            val = IdentifierType.Other;

            target.IdentifierType = val;
            Assert.AreEqual<IdentifierType>(val, target.IdentifierType, "NhsCui.Toolkit.WinForms.IdentifierLabel.IdentifierType was not set correctly.");
        }

        #region Construction from string tests

        /// <summary>
        /// TestValidNhsNumberFormattedInputString
        /// </summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void ValidNhsNumberFormattedInputStringTest()
        {
            string testValue = "437 262 3623";
            IdentifierLabel target = new IdentifierLabel();
            target.Text = testValue;

            Assert.AreEqual<string>(testValue, target.Text, string.Format("Actual value [{1}] does not match expected [{0}]", testValue, target.Text));
        }

        /// <summary>
        /// TestValidNhsNumberUnformattedInputString
        /// </summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void ValidNhsNumberUnformattedInputStringTest()
        {
            string expectedFormattedValue = "437 262 3623";
            string testValue = "4372623623";

            IdentifierLabel target = new IdentifierLabel();
            target.Text = testValue;
            target.IdentifierType = IdentifierType.NhsNumber;

            Assert.AreNotEqual<string>(testValue, target.Text, string.Format("Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, target.Text));
            Assert.AreEqual<string>(expectedFormattedValue, target.Text, string.Format("Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, target.Text));
        }

        #endregion

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            IdentifierLabel target = new IdentifierLabel();

            NhsNumber val = new NhsNumber("4372623623");

            target.Value = val;

            Assert.AreEqual<NhsNumber>(val, target.Value, "NhsCui.Toolkit.WinForms.IdentifierLabel.Value was not set correctly.");
        }
    }
}
