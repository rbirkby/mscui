//-----------------------------------------------------------------------
// <copyright file="Resources.DesignerTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>This is a test class for NhsCui.Toolkit.Web.GenderLabelControl.Resources and is intended
// to contain all NhsCui.Toolkit.Web.GenderLabelControl.Resources Unit Tests</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Resources;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.Web.GenderLabelControl.Resources and is intended
    ///to contain all NhsCui.Toolkit.Web.GenderLabelControl.Resources Unit Tests
    ///</summary>
    [TestClass()]
    public class ResourcesTest
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
        ///A test for Culture
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void CultureTest()
        {
            CultureInfo val = new CultureInfo("en-gb");

            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.Culture = val;

            Assert.AreEqual(val, NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.Culture, "NhsCui.Toolkit.Web.GenderLabelControl.Resources.Culture was not set correctly.");
        }

        /// <summary>
        ///A test for Female
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void FemaleTest()
        {
            string val = "Female"; 

            Assert.AreEqual(val, NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.Female, "NhsCui.Toolkit.Web.GenderLabelControl.Resources.Female was not set correctly.");
        }

        /// <summary>
        ///A test for Male
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void MaleTest()
        {
            string val = "Male"; 

            Assert.AreEqual(val, NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.Male, "NhsCui.Toolkit.Web.GenderLabelControl.Resources.Male was not set correctly.");
        }

        /// <summary>
        ///A test for NotKnown
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void NotKnownTest()
        {
            string val = "Not Known";

            Assert.AreEqual<string>(val, NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.NotKnown, "NhsCui.Toolkit.Web.GenderLabelControl.Resources.NotKnown was not set correctly.");
        }

        /// <summary>
        ///A test for ResourceManager
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(System.Object,System.String)")]
        public void ResourceManagerTest()
        {
            Assert.IsNotNull(NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.ResourceManager, "NhsCui.Toolkit.Web.GenderLabelControl.Resources.ResourceManager was not set correctly.");
        }

        /// <summary>
        ///A test for Resources ()
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void ConstructorTest()
        {
            object target = NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_GenderLabelControl_ResourcesAccessor.CreatePrivate();

            Assert.IsNotNull(target);
        }
    }
}
