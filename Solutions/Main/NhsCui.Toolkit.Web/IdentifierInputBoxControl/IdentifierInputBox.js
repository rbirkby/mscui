//-----------------------------------------------------------------------
// <copyright file="IdentifierInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NHS Identifier InputBox</summary>
//-----------------------------------------------------------------------

/// <reference name="NhsNumber.js"/>

Type.registerNamespace("NhsCui.Toolkit.Web");

NhsCui.Toolkit.Web.IdentifierInputBox = function(element) 
{
    NhsCui.Toolkit.Web.IdentifierInputBox.initializeBase(this, [element]);

    // event delegates
    this._identifierInputBoxBlurDelegate = null;          
    
    this._identifierTextBox = null;
    
    // Key Press event - disable for now, till we fix the auto-formatting
    // this._identifierInputBoxKeyPressDelegate = null;          

    // the NhsNumber value
    this._value = "";

    // Value to use as the delimiter character
    this._delimiterCharacter = " ";
    
    this._disposed = false;
};

NhsCui.Toolkit.Web.IdentifierInputBox.prototype = 
{  
    dispose : function() 
    {
        if (this._disposed === true) 
        {
            return;
        }
        
        this._disposed = true;

        $clearHandlers(this._identifierTextBox);
    
        NhsCui.Toolkit.Web.IdentifierInputBox.callBaseMethod(this, 'dispose');
    },

    initialize : function() 
    {
        this._identifierTextBox = this.get_element();

        this._identifierTextBox.autocomplete = "off";
        this._identifierTextBox.maxlength = "12";
        
        // Blur event
        if (this._identifierInputBoxBlurDelegate === null)
        {
            this._identifierInputBoxBlurDelegate = Function.createDelegate(this, this._blurHandler);          
        }
        $addHandler(this._identifierTextBox, "blur", this._identifierInputBoxBlurDelegate);
        
        // Change event
        $addHandler(this._identifierTextBox, "change", this._identifierInputBoxBlurDelegate);
        
        this.set_text(this._identifierTextBox.value);
    },

    get_delimiterCharacter : function() 
    {
        return this._delimiterCharacter;
    },
    set_delimiterCharacter : function(text) 
    {
        if (this._delimiterCharacter != text) 
        {
            this._delimiterCharacter = text;
            this.raisePropertyChanged('Delimiter');
        }
    },

    get_text : function() 
    {
        return this._text;
    },
    set_text : function(value) 
    {
        if (this._text != value) 
        {
            var nhsReturnObject = NhsNumber.tryParseNhsNumber(value);
            if (nhsReturnObject.returnValue === 0)
            {
                this.get_element().value = new NhsNumber(value).toString().replace(new RegExp(" ", "gi"), this._delimiterCharacter);
                this._value = this.get_element().value;
                this.raisePropertyChanged('Text');
            }
        }
    },

    get_value : function() 
    {
        return this._value;
    },
    set_value : function(value) 
    {
        if (this._value != value) 
        {
            var nhsReturnObject = NhsNumber.tryParseNhsNumber(value);
            if (nhsReturnObject.returnValue === 0)
            {
                this.get_element().value = new NhsNumber(value).toString().replace(new RegExp(" ", "gi"), this._delimiterCharacter);
                this._value = this.get_element().value;
                this.raisePropertyChanged('Value');
            }
        }
    },
    
    _blurHandler : function(e) 
    {
        this.set_value(this.get_element().value);
        //this._fireAllAttachedValidators(this.get_element());
    },

    _fireAllAttachedValidators : function(element)
    {
        ///<summary>Loop through all validators attached to element, firing each one as we go</summary> 
        if (element.Validators)
        {
            //Loop through all the validators attached to element
            for (var i = 0; i < element.Validators.length;i++)
            {
                // ASP.NET function that lives in WebUiValidation.js
                ValidatorValidate(element.Validators[i]);
            }
            
            // ASP.NET function that lives in WebUiValidation.js
            ValidatorUpdateIsValid();
        }
    }
};

NhsCui.Toolkit.Web.IdentifierInputBox.registerClass("NhsCui.Toolkit.Web.IdentifierInputBox", AjaxControlToolkit.BehaviorBase);
