//-----------------------------------------------------------------------
// <copyright file="AddressLabelTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests for address label control</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for address label control
    /// </summary>
    [TestClass]
    public class AddressLabelTest
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AddressLabelTest()
        {
        }

        /// <summary>
        /// Test for Address1 property
        /// </summary>
        [TestMethod]
        public void Address1Test()
        {
            string testValue = "65 Willow Road";
        
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.Address1, "Expected Address1 to initially be string.Empty");
            
            addressLabel.Address1 = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.Address1, "Address1 property not set as expected");
        }

        /// <summary>
        /// Test for Address2 property
        /// </summary>
        [TestMethod]
        public void Address2Test()
        {
            string testValue = "Hedle End";

            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.Address2, "Expected Address2 to initially be string.Empty");
            
            addressLabel.Address2 = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.Address2, "Address2 property not set as expected");
        }

        /// <summary>
        /// Test for Address3 property
        /// </summary>
        [TestMethod]
        public void Address3Test()
        {
            string testValue = "Off London Road";

            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.Address3, "Expected Address3 to initially be string.Empty");
            
            addressLabel.Address3 = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.Address3, "Address3 property not set as expected");
        }

        /// <summary>
        /// Test for Town property
        /// </summary>
        [TestMethod]
        public void TownTest()
        {
            string testValue = "Coventry";

            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.Town, "Expected Town to initially be string.Empty");
            
            addressLabel.Town = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.Town, "Town property not set as expected");
        }

        /// <summary>
        /// Test for County property
        /// </summary>
        [TestMethod]
        public void CountyTest()
        {
            string testValue = "Warwickshire";

            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.County, "Expected County to initially be string.Empty");
            addressLabel.County = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.County, "County property not set as expected");
        }

        /// <summary>
        /// Test for PostCode property
        /// </summary>
        [TestMethod]
        public void PostcodeTest()
        {
            string testValue = "C48 9DB";

            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.Postcode, "Expected PostCode to initially be string.Empty");
            
            addressLabel.Postcode = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.Postcode, "PostCode property not set as expected");
        }

        /// <summary>
        /// Test for Country property
        /// </summary>
        [TestMethod]
        public void CountryTest()
        {
            string testValue = "United Kingdom";

            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual<string>(string.Empty, addressLabel.Country, "Expected Country to initially be string.Empty");
            
            addressLabel.Country = testValue;
            Assert.AreEqual<string>(testValue, addressLabel.Country, "PostCode property not set as expected");
        }
    }
}
