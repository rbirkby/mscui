//-----------------------------------------------------------------------
// <copyright file="TimeInputBoxValidator.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>18-May-2007</date>
// <summary>Client-side javascript for nhs time inputbox validator</summary>
//-----------------------------------------------------------------------

function TimeInputBoxValidatorEvaluateIsValid(val) {
///<summary>Ask NhsCui.TimeInputBoxValidator to evaluate validity of the control it is validating
    
    Sys.Debug.trace("TimeInputBoxValidatorEvaluateIsValid");
    
    // The TimeInputBox applies extra porcessing to its value OnBlur. Therefore we are unble to just read the 
    // text and check it's value
    // var value = ValidatorGetValue(val.controltovalidate);
    
    var timeInputBoxComponent = $get(val.controltovalidate).TimeInputBox;
    
    var value = timeInputBoxComponent.get_validatableString();
    
    //1. If empty return valid 
    if (ValidatorTrim(value).length == 0){
        Sys.Debug.trace("TimeInputBoxValidatorEvaluateIsValid - Valid - Empty");
        return true;
    }
    
    //2. check if value is null string
    if (timeInputBoxComponent._valueIsNullString === true){
         return true;
    }
    
    //3. Try parsing the time. If parse is successful return true
    try{
        var time = NhsTime.tryParse(value);
                        
        if(time){
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
