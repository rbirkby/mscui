//-----------------------------------------------------------------------
// <copyright file="TelephoneInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>Client-side javascript for NHS Telephone InputBox</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

NhsCui.Toolkit.Web.TelephoneInputBox = function(element) 
{
    /// <summary>TelephoneInputBox constructor</summary>
    NhsCui.Toolkit.Web.TelephoneInputBox.initializeBase(this, [element]);
};

NhsCui.Toolkit.Web.TelephoneInputBox.prototype = 
{  
    // properties
    get_countryCode : function() {
        /// <summary>Gets the text for the phone number's current country code</summary>
        var countryCodeElement = GetElementsByClassName("country-code", "input", this.get_element())[0];
        
        return countryCodeElement.value;
    },
    set_countryCode : function(value) {
        /// <summary>Sets the text for the phone number's current country code</summary>
        var countryCodeElement = GetElementsByClassName("country-code", "input", this.get_element())[0];
        
        countryCodeElement.value = value;
    },
    get_areaCode : function() {
        /// <summary>Gets the text for the phone number's current area code</summary>
        var areaCodeElement = GetElementsByClassName("area-code", "input", this.get_element())[0];
        
        return areaCodeElement.value;
    },
    set_areaCode : function(value) {
        /// <summary>Sets the text for the phone number's current area code</summary>
        var areaCodeElement = GetElementsByClassName("area-code", "input", this.get_element())[0];
        
        areaCodeElement.value = value;
    },
    
    get_localNumber : function() {
        /// <summary>Gets the text for the phone number's current area code</summary>
        var localNumberElement = GetElementsByClassName("local-number", "input", this.get_element())[0];
        
        return localNumberElement.value;
    },
    set_localNumber : function(value) {
        /// <summary>Sets the text for the phone number's local number</summary>
        var localNumberElement = GetElementsByClassName("local-number", "input", this.get_element())[0];
        
        localNumberElement.value = value;
    },
    
    dispose : function() {
        if(this._disposed===true) return;
        this._disposed=true;
    
        NhsCui.Toolkit.Web.TelephoneInputBox.callBaseMethod(this, 'dispose');
    },
    initialize : function() {
        NhsCui.Toolkit.Web.TelephoneInputBox.callBaseMethod(this, 'initialize');
    },
    _postCodeChangeHandler : function(e) {
        
        //If not already done uppercase the postcode
        if (e.target.value !== e.target.value.toUpperCase())
        {
            e.target.value = e.target.value.toUpperCase();
        }
    }
};

NhsCui.Toolkit.Web.TelephoneInputBox.registerClass("NhsCui.Toolkit.Web.TelephoneInputBox", Sys.UI.Control);

function GetElementsByClassName(className, htmlTag, element){
	var classNameRegEx = new RegExp("(^|\\s)" + className + "(\\s|$)");
	var tag = htmlTag || "*";
	var elm = element || document;
	var elements = (tag == "*" && elm.all)? elm.all : elm.getElementsByTagName(tag);
	var returnElements = [];
	var current;
	var length = elements.length;
	for(var i=0; i<length; i++){
		current = elements[i];
		if(classNameRegEx.test(current.className)){
			returnElements.push(current);
		}	
	}
	return returnElements;
}