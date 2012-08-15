// -----------------------------------------------------------------------
// <copyright file="NhsNumber.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Ported JS version of C# NhsNumber class</summary>
// -----------------------------------------------------------------------

// Ported from C# version on 06-Sep-2007 - GMM

Type.registerNamespace("NhsCui.Toolkit.Web");

// =============================================================================
// Enums
// =============================================================================
var NhsNumberParseResult = function()
{
   // Specifies values for possible results of TryParseNhsNumber.
};

NhsNumberParseResult.prototype = 
{
    // The value was successfully parsed. 
    Success : 0,
    
    // The value was successfully parsed but did not contain a valid
    // NHS identifier. The value failed a CheckSum calculation. 
    FailedChecksum : 1,
    
    // The value contained too many or too few digits. A valid NHS identifier must
    // contain exactly 10 non-alphabetic digits which cannot all be the same. 
    FailedDigitCount : 2,
    
    // The value contained alphabetic characters. A valid NHS identifier must
    // contain exactly 10 non-alphabetic digits which cannot all be the same. 
    FailedAlphaCharacterContent : 3,
    
    // The value digits cannot all be the same. A valid NHS identifier must
    // contain exactly 10 non-alphabetic digits which cannot all be the same. 
    FailedAllSameDigits : 4,
    
    // The value could not be parsed. A valid NHS identifier must contain
    // exactly 10 non-alphabetic digits which cannot all be the same. 
    FailedUnknownReason : 5        
};

NhsNumberParseResult.registerEnum("NhsNumberParseResult", true);

// =============================================================================
// NhsNumber class
// =============================================================================
var NhsNumber = NhsCui.Toolkit.Web.NhsNumber = function() 
{
    NhsCui.Toolkit.Web.NhsNumber.initializeBase(this);

    // =============================================================================
    // Member Vars

    // Internal string for the instance var of the NHS identifier.
    this._nhsNumber = "";

    //=============================================================================
    // Constructor args
    if (arguments.length === 1 && typeof arguments[0] === "string")
    {
        // Construct an NhsNumber object using a string.
        this._setNhsNumber(arguments[0]);
    }
    else if (arguments.length === 1 && typeof arguments[0] === "number")
    {
        // Construct an NhsNumber object using an int. 
        var nhsNumberString = arguments[0].toString();
        this._setNhsNumber(nhsNumberString);
    }
    else if (arguments.length === 1 && arguments[0] === null)
    {
        throw Error.argumentNull(arguments.toString(), NhsNumberExceptionResources.InvalidNhsNumberMessageUnknownError);
    }
};

//=============================================================================
// Statics
//=============================================================================

// RegEx to enforce no alphabetic characters.
NhsNumber._alphasRegEx = "[A-Za-z]";

// RegEx to enforce digits only.
NhsNumber._nonDigitCharactersRegEx = "\\D";

// RegEx to check for single-digit-only repeating number.
NhsNumber._repeatingDigitsRegEx = "0{10}|1{10}|2{10}|3{10}|4{10}|5{10}|6{10}|7{10}|8{10}|9{10}";

NhsNumber.tryParseNhsNumber = function(value)
{
    // Converts the specified string representation of an NHS identifier to its correctly-formatted equivalent. 
    
    // <remarks>
    // If parsing fails, the out parsedValue is not changed. 
    // </remarks>
    // <param name="value">A string containing an NHS identifier to be parsed. </param>
    // <param name="result">When this method returns, it contains the formatted NHS identifier equivalent to 
    // that contained in value if the conversion succeeded; otherwise it contains the original value. The conversion fails 
    // if the value parameter is null or does not contain a valid string 
    // representation of an NHS identifier.</param>
    // <returns>An NhsNumberParseResult indicating the outcome of the attempted parse operation.</returns>

    var e = Function._validateParams(arguments, [{name: "value", type: String, mayBeNull: false, optional: false}]);
    if (e) throw e;

    var returnValue = NhsNumberParseResult.FailedUnknownReason;
    var parsedValue = null;
    var defaultResult = "";

    var wordMatch = value.match(new RegExp(NhsNumber._alphasRegEx, "gi"));
    if (wordMatch !== null)
    {
        returnValue = NhsNumberParseResult.FailedAlphaCharacterContent;
    }
    else
    {
        parsedValue = value.replace(new RegExp(NhsNumber._nonDigitCharactersRegEx, "gi"), "");
        if (parsedValue.length !== 10)
        {
            returnValue = NhsNumberParseResult.FailedDigitCount;
        }
        else if (NhsNumber.NhsNumberCheckSumIsValid(parsedValue) === false)
        {
            returnValue = NhsNumberParseResult.FailedChecksum;
        }
        else if (parsedValue.match(new RegExp(NhsNumber._repeatingDigitsRegEx, "gi")) !== null)
        {
            returnValue = NhsNumberParseResult.FailedAllSameDigits;
        }
        else
        {
            returnValue = NhsNumberParseResult.Success;
        }
    }

    if (returnValue !== NhsNumberParseResult.Success)
    {
        parsedValue = defaultResult;
    }
    
    var returnObject = {returnValue:returnValue, parsedValue:parsedValue};
    
    return returnObject;
};

NhsNumber.parseNhsNumber = function(value)
{
    // Converts the specified string representation of an NHS identifier to its correctly-formatted equivalent.
    // <remarks>
    // Throws an exception if the string cannot be parsed.
    // </remarks>
    // <param name="value">A string containing an NHS identifier to be parsed. </param>
    // <returns>An NHS identifier constructed from the value parameter. </returns>
    var parsedValue = "";

    var resultObject = NhsNumber.tryParseNhsNumber(value);
    
    if (resultObject.returnValue === NhsNumberParseResult.Success)
    {
        return new NhsNumber(resultObject.parsedValue);
    }
    else
    {
        if (resultObject.returnValue === NhsNumberParseResult.FailedAlphaCharacterContent)
        {
            throw Error.argument(value, NhsNumberExceptionResources.InvalidNhsNumberMessageAlphaChars);
        }
        else if (resultObject.returnValue === NhsNumberParseResult.FailedChecksum)
        {
            throw Error.argument(value, NhsNumberExceptionResources.InvalidNhsNumberMessageChecksum);
        }
        else if (resultObject.returnValue === NhsNumberParseResult.FailedDigitCount)
        {
            throw Error.argument(value, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitCount);
        }
        else if (resultObject.returnValue === NhsNumberParseResult.FailedAllSameDigits)
        {
            throw Error.argument(value, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitsAllSame);
        }
        else
        {
            throw Error.argument(value, NhsNumberExceptionResources.InvalidNhsNumberMessageUnknownError);
        }
    }
};

NhsNumber.NhsNumberCheckSumIsValid = function(identifier)
{
    // Performs the NHS identifier CheckSum calculation. 
    // <param name="identifier">A numeric string representing an NHS identifier.</param>
    // <returns>True if numeric string conforms to NHS identifier CheckSum, otherwise false.</returns>
    var calcResult = 0;

    for (var digitIndex = 0; digitIndex < 9; digitIndex++)
    {
        calcResult += parseInt(identifier.substr(digitIndex, 1), 10) * (10 - digitIndex);
    }

    calcResult = calcResult % 11;

    calcResult = 11 - calcResult;
    if (calcResult === 11)
    {
        calcResult = 0;
    }

    if (parseInt(identifier.substr(9, 1), 10) === calcResult)
    {
        return true;
    }
    else
    {
        return false;
    }
};

//=============================================================================
// NhsNumber prototype
//=============================================================================
NhsCui.Toolkit.Web.NhsNumber.prototype = 
{
    // Provides the string representation of NhsNumber.
    toString : function()
    {
        return this._nhsNumber;
    },

    _setNhsNumber : function(nhsNumber)
    {
        // Parses/sets the NHS Number.
        // <param name="nhsNumber">String to assign to the NHS Number.</param>
        // Parse the text passed in to see if meets the formatting criteria
        // If it is store the value and format it for display
        var parsedValue;

        var resultObject = NhsNumber.tryParseNhsNumber(nhsNumber);
        if (resultObject.returnValue === NhsNumberParseResult.Success)
        {
            this._nhsNumber = resultObject.parsedValue.substr(0, 3) + " " + resultObject.parsedValue.substr(3, 3) + " " + resultObject.parsedValue.substr(6, 4);
        }
        else
        {
            if (resultObject.returnValue === NhsNumberParseResult.FailedAlphaCharacterContent)
            {
                throw Error.argument(nhsNumber, NhsNumberExceptionResources.InvalidNhsNumberMessageAlphaChars);
            }
            else if (resultObject.returnValue === NhsNumberParseResult.FailedChecksum)
            {
                throw Error.argument(nhsNumber, NhsNumberExceptionResources.InvalidNhsNumberMessageChecksum);
            }
            else if (resultObject.returnValue === NhsNumberParseResult.FailedDigitCount)
            {
                throw Error.argument(nhsNumber, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitCount);
            }
            else if (resultObject.returnValue === NhsNumberParseResult.FailedAllSameDigits)
            {
                throw Error.argument(nhsNumber, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitsAllSame);
            }
            else
            {
                throw Error.argument(nhsNumber, NhsNumberExceptionResources.InvalidNhsNumberMessageUnknownError);
            }
        }
    }
};

NhsCui.Toolkit.Web.NhsNumber.registerClass('NhsCui.Toolkit.Web.NhsNumber', Sys.Component);

