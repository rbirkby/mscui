//-----------------------------------------------------------------------
// <copyright file="DateInputBoxValidator.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for nhs date inputbox validator</summary>
//-----------------------------------------------------------------------

function DateInputBoxValidatorEvaluateIsValid(val) {
///<summary>Ask NhsCui.DateInputBoxValidator to evaluate validity of the control it is validating
    var value = ValidatorGetValue(val.controltovalidate);
    
    var dateInputBoxComponent = $get(val.controltovalidate).DateInputBox;
    
    //1. Control has validated the value. return true.
    if (dateInputBoxComponent._validatedValue == true) {
        dateInputBoxComponent._validatedValue = false;
        return true;
    }
    
    //2. don't invalidate values like Today, Tomorrow if blur is not occured.
    if (! dateInputBoxComponent._blurOccured && 
    value == dateInputBoxComponent.get_value().toString(dateInputBoxComponent.get_displayDayOfWeek(), false, dateInputBoxComponent.get_displayDateAsText()))
        return true;
    
    //3. If empty return valid 
    if (ValidatorTrim(value).length == 0){
        return true;
    }
    
    //The InputBox element may have a value but it may be the Watermark if so it is "empty" so leave it
    if (val.watermarktext === ValidatorTrim(value)){
        //Essentially box is empty so return valid
        return true;
    }
    
    //4. check if value is null string
    if (dateInputBoxComponent._valueIsNullString === true){
         return true;
    }
    
    //5. Try parsing the date. If parse is successful return true
    try{
        var date = NhsDate.tryParse(value);
                        
        if(date){
        
            if (date.get_dateType() === DateType.Exact || 
                                    date.get_dateType() === DateType.Approximate){
                //6. Check date bounds
            
                if (date.get_dateValue().getTime() < NhsCui.Toolkit.Web.DateInputBox.MinDateValue ||
                                date.get_dateValue().getTime() > NhsCui.Toolkit.Web.DateInputBox.MaxDateValue){
                    return false;
                }
                else{
                    return true;
                }
            }
            else if (date.get_dateType() === DateType.YearMonth || 
                                    date.get_dateType() === DateType.Year){
                 //7. Check year bounds
            
                if (date.get_year() < new Date(NhsCui.Toolkit.Web.DateInputBox.MinDateValue).getFullYear() ||
                                date.get_year() > new Date(NhsCui.Toolkit.Web.DateInputBox.MaxDateValue).getFullYear()){
                    return false;
                }
                else{
                    return true;
                }                   
            }
            else{
                return true;
            }
        }
        else{
            return false;
        }
    }
    catch(e){
        return false;
    }
}
