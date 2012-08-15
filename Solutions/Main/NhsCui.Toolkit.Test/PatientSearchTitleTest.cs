//-----------------------------------------------------------------------
// <copyright file="PatientSearchTitleTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-Jan-2007</date>
// <summary>Title class test</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.PatientSearch;
    using NhsCui.Toolkit.Web;

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.Title and is intended
    ///to contain all NhsCui.Toolkit.Title Unit Tests
    ///</summary>
    [TestClass()]
    public class PatientSearchTitleTest
    {
        /// <summary>
        ///A test for Gender
        ///</summary>
        [TestMethod(), Description("PatientSearch.Title tests")]
        public void GenderTest()
        {
            string name = "Fraulein";

            Title target = new Title(name);

            Gender val = Gender.Female;

            target.Gender = val;

            Assert.AreEqual<Gender>(val, target.Gender, "NhsCui.Toolkit.Title.Gender was not set correctly.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod(), Description("PatientSearch.Title tests")]
        public void NameTest()
        {
            string name = "Captain";

            Title target = new Title(name);

            string val = "Deacon";
            target.Name = val;

            Assert.AreEqual<string>(val, target.Name, "NhsCui.Toolkit.Title.Name was not set correctly.");
        }

        /// <summary>
        ///A test for Title (string)
        ///</summary>
        [TestMethod(), Description("PatientSearch.Title tests")]
        public void ConstructWithNameTest()
        {
            string name = "Captain";
            Title target = new Title(name);

            Assert.AreEqual<string>(name, target.Name, "NhsCui.Toolkit.PatientSearch.Title.Name was not set correctly.");
        }

        /// <summary>
        ///A test for Title (string, Gender)
        ///</summary>
        [TestMethod(), Description("PatientSearch.Title tests")]
        public void ConstructWithNameAndGenderTest()
        {
            string name = "Fraulein";
            Gender gender = Gender.Female;
            Title target = new Title(name, gender);

            Assert.AreEqual<string>(name, target.Name, "NhsCui.Toolkit.PatientSearch.Title.Name was not set correctly.");
            Assert.AreEqual<Gender>(gender, target.Gender, "NhsCui.Toolkit.PatientSearch.Title.Gender was not set correctly.");
        }
    }
}
