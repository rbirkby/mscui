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

namespace NhsCui.Toolkit.Web.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NhsCui.Toolkit.Web;
    using NhsCui.Toolkit;
    using System.Web.UI.WebControls;
    using System.Web.UI;
    using System.IO;
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

            target.IdentifierType = val;
            Assert.AreEqual<IdentifierType>(val, target.IdentifierType, "NhsCui.Toolkit.Web.IdentifierLabel.IdentifierType was not set correctly.");

            val = IdentifierType.Other;

            target.IdentifierType = val;
            Assert.AreEqual<IdentifierType>(val, target.IdentifierType, "NhsCui.Toolkit.Web.IdentifierLabel.IdentifierType was not set correctly.");
        }

        /// <summary>
        /// Test that value throws an exception when an invalid identifier type is specified
        ///</summary>
        [TestMethod(), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IdentifierTypeBadParameterTest()
        {
            IdentifierLabel target = new IdentifierLabel();

            target.IdentifierType = (IdentifierType)99;
        }

        /// <summary>
        ///A test for RenderContents (HtmlTextWriter)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor(System.Text.StringBuilder)")]
        public void RenderContentsTest()
        {
            IdentifierLabel target = new IdentifierLabel();

            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_IdentifierLabelAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_IdentifierLabelAccessor(target);

            StringBuilder testString = new StringBuilder();
            HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(testString));

            accessor.RenderContents(writer);

            // rendered contents of the identifier label without an identifier
            // being set should be an empty string
            Assert.AreEqual<string>(string.Empty, testString.ToString());
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
            target.IdentifierType = IdentifierType.NhsNumber;
            target.Text = testValue;

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

            Assert.AreEqual<NhsNumber>(val, target.Value, "NhsCui.Toolkit.Web.IdentifierLabel.Value was not set correctly.");
        }
    }
}
