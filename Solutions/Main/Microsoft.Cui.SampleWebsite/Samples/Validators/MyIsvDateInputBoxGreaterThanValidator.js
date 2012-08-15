// (c) 2008 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx.
//
// This document and/or software (�this Content�) has been created in partnership
// with the National Health Service (NHS) in England.  Intellectual Property Rights
// to this Content are jointly owned by Microsoft and the NHS in England, although 
// both Microsoft and the NHS are entitled to independently exercise their rights
// of ownership.  Microsoft acknowledges the contribution of the NHS in England
// through their Common User Interface programme to this Content.  Readers are 
// referred to www.cui.nhs.uk for further information on the NHS CUI Programme.

function MyIsvDateInputBoxGreaterThanValidatorEvaluateIsValid(val) {
///<summary>Ask Microsoft.Cui.SampleWebsite.Samples.Validators.MyIsvDateInputBoxGreaterThanValidator to evaluate validity of the control it is validating
    var value = ValidatorGetValue(val.controltovalidate);
    
    //1. If empty return valid 
    if (ValidatorTrim(value).length == 0){
        return true;
    }
    
    //2. Try parsing the time. If parse is successful carry on
    try{
        var date = NhsDate.tryParse(value);
                        
        if(date){
            valueToCompare = ValidatorGetValue(val.comparedatebox);
            
            var dateToCompare = NhsDate.tryParse(valueToCompare);
            
            if (dateToCompare){
                // Only now that we have two valid dates can we apply our actual validation logic
                if (date.get_dateValue() > dateToCompare.get_dateValue()){
                    return true;
                }
                else{
                    return false;
                }
            }
            else{
                // Cannot evaluate both dates so have to assume true
                return true;
            }
            
        }
        else{
            // Cannot evaluate both dates so have to assume true
            return true;
        }
    }
    catch(e){
        return false;
    }
}

