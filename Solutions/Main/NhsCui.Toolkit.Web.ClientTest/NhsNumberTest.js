//-----------------------------------------------------------------------
// <copyright file="NhsNumberTest.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>06-Sep-2007</date>
// <summary>Client-side javascript for NhsNumber tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

/// <summary>
/// Unit tests to test the NhsNumber class in the NhsCui.Toolkit.Web namespace
/// </summary>
var NhsNumberTest = NhsCui.Toolkit.Web.Tests.NhsNumberTest = function() 
{
};

NhsCui.Toolkit.Web.Tests.NhsNumberTest.prototype = 
{
    // 1. [ExpectedException(typeof(System.ArgumentNullException), "Should throw null argument exception from construction")]
    NullParameterConstructorTest : function()
    {
        /// <summary>
        /// TestNullParameterConstructor
        /// </summary>
        var targetNhsNumber = new NhsNumber(null);
    },

    // ParseNhsNumber Tests...
    
    // 2. [ExpectedException(typeof(System.ArgumentException), "Should throw null argument exception from ParseNhsNumber method with bad parameter")]
    ParseNhsNumberBadParameterTest : function()
    {
        /// <summary>
        /// TestParseMethodBadParameter
        /// </summary>
        var targetNhsNumber = NhsNumber.parseNhsNumber("ABC123");
    },

    ParseNhsNumberGoodParameterTest : function()
    {
        /// <summary>
        /// TestParseMethodGoodParameter
        /// </summary>
        var testValue = "4372623623";
        var testExpectedOutput = "437 262 3623";

        var targetNhsNumber = NhsNumber.parseNhsNumber(testValue);

        Assert.AreEqual(testExpectedOutput, targetNhsNumber.toString(), "ParseNhsNumber method call should create valid NhsNumber.");
    },

    // 3. [ExpectedException(typeof(System.ArgumentException), "Should throw null argument exception from ParseNhsNumber method with bad parameter")]
    ParseInvalidNhsNumberStringTest : function()
    {
        /// <summary>
        /// TestParseInvalidNhsNumberString
        /// </summary>
        var targetNhsNumber = NhsNumber.parseNhsNumber("1234567890");
    },

    // 4. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too few digits in parsed string")]
    ParseTooFewDigitsStringTest : function()
    {
        /// <summary>
        /// TestParseTooFewDigitsString
        /// </summary>
        var targetNhsNumber = NhsNumber.parseNhsNumber("123456789");
    },

    // 5. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too many digits in parsed string")]
    ParseTooManyDigitsStringTest : function()
    {
        /// <summary>
        /// TestParseTooManyDigitsString
        /// </summary>
        var targetNhsNumber = NhsNumber.parseNhsNumber("12345678901");
    },

    // 6. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from 10 digits all the same in parsed string")]
    ParseAllDigitsSameStringTest : function()
    {
        /// <summary>
        /// TestParseAllDigitsSameString
        /// </summary>
        var targetNhsNumber = NhsNumber.parseNhsNumber("5555555555");
    },

    // Construction from string tests...
    
    // 7. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from negative number in parsed string")]
    ConstructorNegativeIntStringTest : function()
    {
        var targetNhsNumber = new NhsNumber("-123456789");
    },
    
    // 8. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too few digits in parsed string")]
    ConstructorTooFewDigitsStringTest : function()
    {
        /// <summary>
        /// TestTooFewDigitsString
        /// </summary>
        var targetNhsNumber = new NhsNumber("1234");
    },

    // 9. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too many digits in parsed string")]
    ConstructorTooManyDigitsStringTest : function()
    {
        /// <summary>
        /// TestTooManyDigitsString
        /// </summary>
        var targetNhsNumber = new NhsNumber("1234567890123");
    },

    // 10. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from floating point number or double in parsed string")]
    ConstructorFloatDoubleNumberStringTest : function()
    {
        /// <summary>
        /// TestFloatDoubleNumberString
        /// </summary>
        var targetNhsNumber = new NhsNumber("12345.12345");
    },

    // 11. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from alpha chars in parsed string")]
    ConstructorAlphaCharsStringTest : function()
    {
        /// <summary>
        /// TestAlphaCharsString
        /// </summary>
        var targetNhsNumber = new NhsNumber("lze4sg7b9o");
    },

    // 12. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from 10 digits all the same in parsed string")]
    ConstructorAllDigitsSameStringTest : function()
    {
        /// <summary>
        /// TestAllDigitsSameString
        /// </summary>
        var targetNhsNumber = new NhsNumber("5555555555");
    },

    // 13. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from checksum of parsed string")]
    ConstructorInvalidNhsNumberStringTest : function()
    {
        /// <summary>
        /// TestInvalidNhsNumberString
        /// </summary>
        var targetNhsNumber = new NhsNumber("1234567890");
    },

    ConstructorValidNhsNumberFormattedInputStringTest : function()
    {
        /// <summary>
        /// Test the constructor with a valid string
        /// </summary>

        var testValue = "437 262 3623";
        var targetNhsNumber = new NhsNumber(testValue);
        
        Assert.AreEqual(testValue, targetNhsNumber.toString(), "Actual value [" + targetNhsNumber.toString() + "] does not match expected [" + testValue + "]");
    },

    // Construction from int tests...
    
    // 14. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from negative number in parsed string")]
    ConstructorNegativeNumericTest : function()
    {
        /// <summary>
        /// TestNegativeNumeric
        /// </summary>
        var targetNhsNumber = new NhsNumber(-123456789);
    },

    // 15. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too few digits in parsed string")]
    ConstructorTooFewDigitsNumericTest : function()
    {
        /// <summary>
        /// TestNegativeNumeric
        /// </summary>
        var targetNhsNumber = new NhsNumber(1234);
    },

    // 16. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from too many digits in parsed string")]
    ConstructorTooManyDigitsNumericTest : function()
    {
        /// <summary>
        /// TestTooManyDigitsNumeric
        /// </summary>
        var targetNhsNumber = new NhsNumber(1234567890123);
    },

    // 17. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from double in parsed string")]
    ConstructorDecimalNhsNumberNumericTest : function()
    {
        /// <summary>
        /// TestDoubleNhsNumberNumeric
        /// </summary>
        var targetNhsNumber = new NhsNumber(12345.12345);
    },

    // 18. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from 10 digits all the same in parsed string")]
    ConstructorAllDigitsSameNumericTest : function()
    {
        /// <summary>
        /// TestAllDigitsSameNumeric
        /// </summary>
        var targetNhsNumber = new NhsNumber(5555555555);
    },

    // 19. [ExpectedException(typeof(System.ArgumentException), "Should throw argument exception from checksum of parsed string")]
    ConstructorInvalidNhsNumberNumericTest : function()
    {
        /// <summary>
        /// TestInvalidNhsNumberNumeric
        /// </summary>
        var targetNhsNumber = new NhsNumber(1234567890);
    },

    ConstructorValidNhsNumberNumericTest : function()
    {
        /// <summary>
        /// Test the constructor with a valid int
        /// </summary>

        var testValue = 4372623623;

        var targetNhsNumber = new NhsNumber(testValue);

        Assert.IsTrue(targetNhsNumber !== null, "Unable to construct an NhsNumber from a valid numeric input [" + testValue.toString() + "]");
    }
};

// 1.
NhsNumberTest.prototype.NullParameterConstructorTest.expectedException={name:"Sys.ArgumentNullException", 
                                                         message:"Should throw null argument exception from construction"};
// 2.
NhsNumberTest.prototype.ParseNhsNumberBadParameterTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from ParseNhsNumber method with bad parameter"};
// 3.
NhsNumberTest.prototype.ParseInvalidNhsNumberStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from ParseNhsNumber method with bad parameter"};
// 4.
NhsNumberTest.prototype.ParseTooFewDigitsStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from too few digits in parsed string"};
// 5.
NhsNumberTest.prototype.ParseTooManyDigitsStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from too many digits in parsed string"};
// 6.
NhsNumberTest.prototype.ParseAllDigitsSameStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from 10 digits all the same in parsed string"};
// 7.
NhsNumberTest.prototype.ConstructorNegativeIntStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from negative number in parsed string"};
// 8.
NhsNumberTest.prototype.ConstructorTooFewDigitsStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from too few digits in parsed string"};
// 9.
NhsNumberTest.prototype.ConstructorTooManyDigitsStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from too many digits in parsed string"};
// 10.
NhsNumberTest.prototype.ConstructorFloatDoubleNumberStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from floating point number or double in parsed string"};
// 11.
NhsNumberTest.prototype.ConstructorAlphaCharsStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from alpha chars in parsed string"};
// 12.
NhsNumberTest.prototype.ConstructorAllDigitsSameStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from 10 digits all the same in parsed string"};
// 13.
NhsNumberTest.prototype.ConstructorInvalidNhsNumberStringTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from checksum of parsed string"};
// 14.
NhsNumberTest.prototype.ConstructorNegativeNumericTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from negative number in parsed string"};
// 15.
NhsNumberTest.prototype.ConstructorTooFewDigitsNumericTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from too few digits in parsed string"};
// 16.
NhsNumberTest.prototype.ConstructorTooManyDigitsNumericTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from too many digits in parsed string"};
// 17.
NhsNumberTest.prototype.ConstructorDecimalNhsNumberNumericTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from double in parsed string"};
// 18.
NhsNumberTest.prototype.ConstructorAllDigitsSameNumericTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from 10 digits all the same in parsed string"};
// 19.
NhsNumberTest.prototype.ConstructorInvalidNhsNumberNumericTest.expectedException={name:"Sys.ArgumentException", 
                                                         message:"Should throw argument exception from checksum of parsed string"};
