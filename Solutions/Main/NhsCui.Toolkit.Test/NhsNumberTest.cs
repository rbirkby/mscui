//-----------------------------------------------------------------------
// <copyright file="NhsNumberTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests to test the NhsNumber class</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// NhsNumberTest class
    /// </summary>
    [TestClass]
    public class NhsNumberTest
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public NhsNumberTest()
        {
        }

        /// <summary>
        /// TestNullParameterConstructor
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw null argument exception from construction")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void NullParameterConstructorTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(null);
        }

        #region ParseNhsNumber Tests
 
        /// <summary>
        /// TestParseMethodBadParameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw null argument exception from ParseNhsNumber method with bad parameter")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ParseNhsNumberBadParameterTest()
        {
            NhsNumber targetNhsNumber = NhsNumber.ParseNhsNumber("ABC123");
        }

        /// <summary>
        /// TestParseMethodGoodParameter
        /// </summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ParseNhsNumberGoodParameterTest()
        {
            string testValue = "4372623623";
            string testExpectedOutput = "437 262 3623";

            NhsNumber targetNhsNumber = NhsNumber.ParseNhsNumber(testValue);

            Assert.AreEqual<string>(testExpectedOutput, targetNhsNumber.ToString(), "ParseNhsNumber method call should create valid NhsNumber.");
        }

        /// <summary>
        /// TestParseInvalidNhsNumberString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw null argument exception from ParseNhsNumber method with bad parameter")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ParseInvalidNhsNumberStringTest()
        {
            NhsNumber targetNhsNumber = NhsNumber.ParseNhsNumber("1234567890");
        }

        /// <summary>
        /// TestParseTooFewDigitsString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too few digits in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ParseTooFewDigitsStringTest()
        {
            NhsNumber targetNhsNumber = NhsNumber.ParseNhsNumber("123456789");
        }

        /// <summary>
        /// TestParseTooManyDigitsString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too many digits in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ParseTooManyDigitsStringTest()
        {
            NhsNumber targetNhsNumber = NhsNumber.ParseNhsNumber("12345678901");
        }

        /// <summary>
        /// TestParseAllDigitsSameString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from 10 digits all the same in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ParseAllDigitsSameStringTest()
        {
            NhsNumber targetNhsNumber = NhsNumber.ParseNhsNumber("5555555555");
        }

        #endregion
        
        #region Construction from string tests

        /// <summary>
        /// TestNegativeIntString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from negative number in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorNegativeIntStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("-123456789");
        }

        /// <summary>
        /// TestTooFewDigitsString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too few digits in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorTooFewDigitsStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("1234");
        }

        /// <summary>
        /// TestTooManyDigitsString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too many digits in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorTooManyDigitsStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("1234567890123");
        }

        /// <summary>
        /// TestFloatDoubleNumberString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from floating point number or double in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorFloatDoubleNumberStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("12345.12345");
        }

        /// <summary>
        /// TestAlphaCharsString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from alpha chars in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void ConstructorAlphaCharsStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("lze4sg7b9o");
        }

        /// <summary>
        /// TestAllDigitsSameString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from 10 digits all the same in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorAllDigitsSameStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("5555555555");
        }

        /// <summary>
        /// TestInvalidNhsNumberString
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from checksum of parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorInvalidNhsNumberStringTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber("1234567890");
        }

        /// <summary>
        /// TestValidNhsNumberFormattedInputString
        /// </summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void ConstructorValidNhsNumberFormattedInputStringTest()
        {
            string testValue = "437 262 3623";
            NhsNumber targetNhsNumber = new NhsNumber(testValue);

            Assert.AreEqual<string>(testValue, targetNhsNumber.ToString(), string.Format("Actual value [{1}] does not match expected [{0}]", testValue, targetNhsNumber.ToString()));
        }

        /// <summary>
        /// TestValidNhsNumberUnformattedInputString
        /// </summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public void ConstructorValidNhsNumberUnformattedInputStringTest()
        {
            string expectedFormattedValue = "437 262 3623";
            string testValue = "4372623623";

            NhsNumber targetNhsNumber = new NhsNumber(testValue);

            Assert.AreNotEqual<string>(testValue, targetNhsNumber.ToString(), string.Format("Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, targetNhsNumber.ToString()));
            Assert.AreEqual<string>(expectedFormattedValue, targetNhsNumber.ToString(), string.Format("Actual value [{1}] does not match expected [{0}]", expectedFormattedValue, targetNhsNumber.ToString()));
        }

        #endregion

        #region Construction from int tests

        /// <summary>
        /// TestNegativeNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from negative number in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorNegativeNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(-123456789);
        }

        /// <summary>
        /// TestNegativeNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too few digits in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorTooFewDigitsNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(1234);
        }

        /// <summary>
        /// TestTooManyDigitsNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too many digits in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorTooManyDigitsNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(1234567890123);
        }

        /// <summary>
        /// TestDoubleNhsNumberNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from double in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorDoubleNhsNumberNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(12345.12345d);
        }

        /// <summary>
        /// TestFloatNhsNumberNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from floating point number in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorFloatNhsNumberNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(12345.12345f);
        }

        /// <summary>
        /// TestAllDigitsSameNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from 10 digits all the same in parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorAllDigitsSameNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(5555555555);
        }

        /// <summary>
        /// TestInvalidNhsNumberNumeric
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from checksum of parsed string")]
        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "NhsCui.Toolkit.NhsNumber")]
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "targetNhsNumber")]
        public void ConstructorInvalidNhsNumberNumericTest()
        {
            NhsNumber targetNhsNumber = new NhsNumber(1234567890);
        }

        /// <summary>
        /// TestValidNhsNumberNumeric
        /// </summary>
        [TestMethod]
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public void ConstructorValidNhsNumberNumericTest()
        {
            long testValue = 4372623623;

            NhsNumber targetNhsNumber = new NhsNumber(testValue);

            Assert.IsNotNull(targetNhsNumber, string.Format("Unable to construct an NhsNumber from a valid numeric input [{0}]", testValue));
        }

        #endregion
    }
}
