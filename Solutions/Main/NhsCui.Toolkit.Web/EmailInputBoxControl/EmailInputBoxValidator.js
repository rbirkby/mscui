//-----------------------------------------------------------------------
// <copyright file="EmailInputBoxValidator.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for nhs email inputbox validator</summary>
//-----------------------------------------------------------------------

function EmailInputBoxValidatorEvaluateIsValid(val)
{
    // Ask NhsCui.EmailInputBoxValidator to evaluate validity of the control it is validating
    var value = ValidatorGetValue(val.controltovalidate);
    
    // If empty return valid 
    if (ValidatorTrim(value).length == 0)
    {
        return true;
    }
    
    // Try checking the value with a standard RegExp. If successful return true
    try
    {
        // RegExp for email address matching
        var emailRegExp = new RegExp("^[A-Z0-9._%-]+@(?:[A-Z0-9-]+\\.)+[A-Z]{2,4}$", "gi");
            
        var match = emailRegExp.exec(value);
                        
        if (match !== null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    catch(e)
    {
        return false;
    }
}
