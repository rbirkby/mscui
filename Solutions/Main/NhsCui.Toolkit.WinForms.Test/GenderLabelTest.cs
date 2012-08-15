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

namespace NhsCui.Toolkit.WinForms.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;
    using NhsCui.Toolkit;
    using System.IO;
    using System.Globalization;

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.WinForms.GenderLabel and is intended
    ///to contain all NhsCui.Toolkit.WinForms.GenderLabel Unit Tests
    ///</summary>
    [TestClass()]
    public class GenderLabelTest
    {
        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            GenderLabel target = new GenderLabel();

            PatientGender val = PatientGender.Male;
            target.Value = val;
            Assert.AreEqual<PatientGender>(val, target.Value, "NhsCui.Toolkit.WinForms.GenderLabel.Value was not set correctly.");

            val = PatientGender.Female;
            target.Value = val;
            Assert.AreEqual<PatientGender>(val, target.Value, "NhsCui.Toolkit.WinForms.GenderLabel.Value was not set correctly.");

            val = PatientGender.NotKnown;
            target.Value = val;
            Assert.AreEqual<PatientGender>(val, target.Value, "NhsCui.Toolkit.WinForms.GenderLabel.Value was not set correctly.");
        }
    }
}
