//-----------------------------------------------------------------------
// <copyright file="NhsCuiValidation.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for nhs validation</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

var NhsCuiValidation = NhsCui.Toolkit.Web.NhsCuiValidation = function() {
    NhsCui.Toolkit.Web.NhsCuiValidation.initializeBase(this);


};

NhsCuiValidation.SetValidationTargetToActualControl = function (element)
{
    //<summary>For any attached Validators set the controltovalidate to be the actual control's id/summary>
    if (element.Validators){
        for(var validatorIndex = 0;validatorIndex < element.Validators.length;validatorIndex++){
        
            var validator = element.Validators[validatorIndex];
            
            //Switch the controltovalidate to be the id of the internal TextBox rather than the containing SPAN
            validator.controltovalidate = element.id;
        }
    }
};
 
NhsCuiValidation.GetAttachedValidatorOfSpecificType = function (element, validatorType)
{
    ///<summary>If present return the attached NhsCui.DateInputBoxValidator</summary>
    if (element.Validators){
        //Loop through all the validators attached to the current element looking for one marked with a specific valType
        for (var i = 0; i < element.Validators.length;i++){
            if (element.Validators[i].valtype){
                if (element.Validators[i].valtype===validatorType){
                    return element.Validators[i];
                }
            }
        }
    }
    
    return null;
}; 

NhsCuiValidation.FireAllAttachedValidators = function (element)
{
     ///<summary>Loop through all validators attached to element, firing each one as we go</summary> 
    if (element.Validators){
        //Loop through all the validators attached to element
        for (var i = 0; i < element.Validators.length;i++){
            // ASP.NET function that lives in WebUiValidation.js
            ValidatorValidate(element.Validators[i]);
        }
        
        // ASP.NET function that lives in WebUiValidation.js
        ValidatorUpdateIsValid();
    }
}; 

NhsCui.Toolkit.Web.NhsCuiValidation.registerClass('NhsCui.Toolkit.Web.NhsCuiValidation', Sys.Component)

