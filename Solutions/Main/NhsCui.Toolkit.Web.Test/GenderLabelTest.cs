//-----------------------------------------------------------------------
// <copyright file="GenderLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>GenderLabelTest file</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using NhsCui.Toolkit.Web;
    using System.Web.UI;
    using NhsCui.Toolkit;
    using System.IO;
    using System.Globalization;

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.Web.GenderLabel and is intended
    ///to contain all NhsCui.Toolkit.Web.GenderLabel Unit Tests
    ///</summary>
    [TestClass()]
    public class GenderLabelTest
    {
        /// <summary>
        /// testContextInstance
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }
        #region Additional test attributes
        // You can use the following additional attributes as you write your tests:
        // 
        // Use ClassInitialize to run code before running the first test in the class
        // 
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext)
        // {
        // }
        // 
        // Use ClassCleanup to run code after all tests in a class have run
        // 
        // [ClassCleanup()]
        // public static void MyClassCleanup()
        // {
        // }
        // 
        // Use TestInitialize to run code before running each test
        // 
        // [TestInitialize()]
        // public void MyTestInitialize()
        // {
        // }
        // 
        // Use TestCleanup to run code after each test has run
        // 
        // [TestCleanup()]
        // public void MyTestCleanup()
        // {
        // }
        #endregion

        /// <summary>
        ///A test for RenderContents (HtmlTextWriter)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void RenderContentsTest()
        {
            GenderLabel target = new GenderLabel();

            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelAccessor(target);

            StringBuilder testString = new StringBuilder();
            HtmlTextWriter writer = new HtmlTextWriter(new StringWriter(testString, CultureInfo.InvariantCulture));

            accessor.RenderContents(writer);
            Assert.AreNotEqual<string>(string.Empty, testString.ToString());
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            GenderLabel target = new GenderLabel();

            PatientGender val = PatientGender.Male;
            target.Value = val;
            Assert.AreEqual<PatientGender>(val, target.Value, "NhsCui.Toolkit.Web.GenderLabel.Value was not set correctly.");

            val = PatientGender.Female;
            target.Value = val;
            Assert.AreEqual<PatientGender>(val, target.Value, "NhsCui.Toolkit.Web.GenderLabel.Value was not set correctly.");

            val = PatientGender.NotKnown;
            target.Value = val;
            Assert.AreEqual<PatientGender>(val, target.Value, "NhsCui.Toolkit.Web.GenderLabel.Value was not set correctly.");
        }

        /// <summary>
        /// Test that value throws an exception when in invalid enum value is set
        ///</summary>
        [TestMethod(), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValueBadParameterTest()
        {
            GenderLabel target = new GenderLabel();

            target.Value = (PatientGender)99;
        }
    }
}
