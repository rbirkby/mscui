//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBoxValidator.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for nhs timespan inputbox validator</summary>
//-----------------------------------------------------------------------

function TimeSpanInputBoxValidatorEvaluateIsValid(val) {
///<summary>Ask NhsCui.TimeInputBoxValidator to evaluate validity of the control it is validating
    
    var value = ValidatorGetValue(val.controltovalidate);
    var timeSpanInputBoxComponent = $get(val.controltovalidate).TimeSpanInputBox;
    
    //1. If empty return valid 
    if (ValidatorTrim(value).length == 0){
        return true;
    }
    
    //2. Try parsing the time. If parse is successful return true
    try{
        value = timeSpanInputBoxComponent._preParseUnits(value);
        var timespan = NhsTimeSpan.tryParse(value, timeSpanInputBoxComponent.get_isAge());
                        
        if(timespan){
            return true;
        }
        else{
            return false;
        }
    }
    catch(e){
        return false;
    }
}
