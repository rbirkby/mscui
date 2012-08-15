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

namespace NhsCui.Toolkit.Web.Test
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
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.Address1, string.Empty, "Expected Address1 to initially be empty string");
            addressLabel.Address1 = "1 Acacia Avenue";
            Assert.AreEqual(addressLabel.Address1, "1 Acacia Avenue", "Address1 property not set as expected");
        }

        /// <summary>
        /// Test for Address2 property
        /// </summary>
        [TestMethod]
        public void Address2Test()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.Address2, string.Empty, "Expected Address2 to initially be empty string");
            addressLabel.Address2 = "Little Amwell";
            Assert.AreEqual(addressLabel.Address2, "Little Amwell", "Address2 property not set as expected");
        }

        /// <summary>
        /// Test for Address3 property
        /// </summary>
        [TestMethod]
        public void Address3Test()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.Address3, string.Empty, "Expected Address3 to initially be empty string");
            addressLabel.Address3 = "Great Amwell";
            Assert.AreEqual(addressLabel.Address3, "Great Amwell", "Address3 property not set as expected");
        }

        /// <summary>
        /// Test for Town property
        /// </summary>
        [TestMethod]
        public void TownTest()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.Town, string.Empty, "Expected Town to initially be empty string");
            addressLabel.Town = "Ware";
            Assert.AreEqual(addressLabel.Town, "Ware", "Town property not set as expected");
        }

        /// <summary>
        /// Test for County property
        /// </summary>
        [TestMethod]
        public void CountyTest()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.County, string.Empty, "Expected County to initially be empty string");
            addressLabel.County = "Herfordshire";
            Assert.AreEqual(addressLabel.County, "Herfordshire", "County property not set as expected");
        }

        /// <summary>
        /// Test for PostCode property
        /// </summary>
        [TestMethod]
        public void PostCodeTest()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.Postcode, string.Empty, "Expected PostCode to initially be empty string");
            addressLabel.Postcode = "WR1 1RG";
            Assert.AreEqual(addressLabel.Postcode, "WR1 1RG", "PostCode property not set as expected");
        }

        /// <summary>
        /// Test for Country property
        /// </summary>
        [TestMethod]
        public void CountryTest()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.Country, string.Empty, "Expected Country to initially be empty string");
            addressLabel.Country = "UK";
            Assert.AreEqual(addressLabel.Country, "UK", "Country property not set as expected");
        }

        /// <summary>
        /// Test for AddressDisplayFormat property
        /// </summary>
        [TestMethod]
        public void AddressDisplayFormatTest()
        {
            AddressLabel addressLabel = new AddressLabel();
            Assert.AreEqual(addressLabel.AddressDisplayFormat, AddressDisplayFormat.InForm, "Expected AddressDisplayFormat to initially be InForm");

            AddressDisplayFormat testValue = AddressDisplayFormat.InLine;
            addressLabel.AddressDisplayFormat = testValue;
            Assert.AreEqual(addressLabel.AddressDisplayFormat, testValue, "AddressDisplayFormat property not set as expected");
        }

        /// <summary>
        /// Test for AddressDisplayFormat property
        /// </summary>
        [TestMethod(), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddressDisplayFormatBadParameterTest()
        {
            AddressLabel addressLabel = new AddressLabel();
            addressLabel.AddressDisplayFormat = (AddressDisplayFormat)99;
        }
    }
}
