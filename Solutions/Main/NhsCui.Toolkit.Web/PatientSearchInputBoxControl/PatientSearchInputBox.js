//-----------------------------------------------------------------------
// <copyright file = "PatientSearchInputBox.js" company = "Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-Feb-2007</date>
// <summary>Client-side javascript for NHS Patient Search InputBox</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

// ================================
// PatientSearchInputBox Main class
// ================================
NhsCui.Toolkit.Web.PatientSearchInputBox = function(element) 
{
    NhsCui.Toolkit.Web.PatientSearchInputBox.initializeBase(this, [element]);
    this._parser = new Parser();

    // Setup events
    this._formSubmitDelegate = Function.createDelegate(this, this._formSubmitHandler);
};

// =====================================
// PatientSearchInputBox Prototype class
// =====================================
NhsCui.Toolkit.Web.PatientSearchInputBox.prototype = 
{    
    // Parser properties
    get_address : function()
    {
        return this._parser.get_address();
    },
    
    get_age : function()
    {
        return this._parser.get_age();
    },
    
    get_ageUpper : function()
    {
        return this._parser.get_ageUpper();
    },
    
    get_commonFamilyNames : function()
    {
        return this._parser.get_commonFamilyNames();
    },
    set_commonFamilyNames : function(value)
    {
        this._parser.set_commonFamilyNames(value);
        this._get_state().CommonFamilyNames = value;
    },
    
    get_dateOfBirth : function()
    {
        return this._parser.get_dateOfBirth();
    },
    
    get_dateOfBirthUpper : function()
    {
        return this._parser.get_dateOfBirthUpper();
    },
    
    get_endGroupDelimiter : function()
    {
        return this._parser.get_endGroupDelimiter();
    },
    set_endGroupDelimiter : function(value)
    {
        this._parser.set_endGroupDelimiter(value);
        this._get_state().EndGroupDelimiter = value;
    },
    
    get_familyName : function()
    {
        return this._parser.get_familyName();
    },
    
    get_gender : function()
    {
        return this._parser.get_gender();
    },
    
    get_givenName : function()
    {
        return this._parser.get_givenName();
    },
    
    get_informationDelimiter : function()
    {
        return this._parser.get_informationDelimiter();
    },
    set_informationDelimiter : function(value)
    {
        this._parser.set_informationDelimiter(value);
        this._get_state().InformationDelimiter = value;
    },
    
    get_informationFormat : function()
    {
        return this._parser.get_informationFormat();
    },
    set_informationFormat : function(value)
    {
        this._parser.set_informationFormat(value);
        this._get_state().InformationFormat = value;
    },
    
    get_isCommonFamilyName : function()
    {
        return this._parser.get_isCommonFamilyName();
    },
    
    get_isDateOfBirthAgeMismatch : function()
    {
        return this._parser.get_isDateOfBirthAgeMismatch();
    },

    get_isGenderTitleMismatch : function()
    {
        return this._parser.get_isGenderTitleMismatch();
    },
    
    get_isMandatoryInformationEntered : function()
    {
        return this._parser.get_isMandatoryInformationEntered();
    },
    
    get_mandatoryInformation : function()
    {
        return this._parser.get_mandatoryInformation();
    },
    set_mandatoryInformation : function(value)
    {
        this._parser.set_mandatoryInformation(value);
        this._get_state().MandatoryInformation = value;
    },
    
    get_maximumAge : function()
    {
        return this._parser.get_maximumAge();
    },
    set_maximumAge : function(value)
    {
        this._parser.set_maximumAge(value);
        this._get_state().MaximumAge = value;
    },
    
    get_nhsNumber : function()
    {
        return this._parser.get_nhsNumber();
    },
    
    get_postcode : function()
    {
        return this._parser.get_postcode();
    },
    
    get_startGroupDelimiter : function()
    {
        return this._parser.get_startGroupDelimiter();
    },
    set_startGroupDelimiter : function(value)
    {
        this._parser.set_startGroupDelimiter(value);
        this._get_state().StartGroupDelimiter = value;
    },
    
    get_text : function()
    {
        return this._parser.get_text();
    },
    set_text : function(value)
    {
        this.get_element().value = value;
        this._parser.set_text(value);
    },
    
    get_title : function()
    {
        return this._parser.get_title();
    },
    
    get_titles : function()
    {
        return this._parser.get_titles();
    },
    set_titles : function(value)
    {
        if (value.length != undefined)
        {
            if (typeof value[0] == 'string')
            {
                this._get_state().Titles = value;

                // need to convert a string array to a Title array for the parser
                var titleObjects = new Array();
                for (var titleIndex = 0; titleIndex < value.length; titleIndex+=2)
                {
                    var newTitle = new Title(value[titleIndex], eval("Gender." + value[titleIndex+1]));
                    titleObjects.push(newTitle);
                }
                this._parser.set_titles(titleObjects);
            }
            else
            {
                this._parser.set_titles(value);

                // need to convert a Title array to a string array - for state
                var titlesArray = new Array();
                for (var titleIndex = 0; titleIndex < value.length; titleIndex++)
                {
                    titlesArray.push(value[titleIndex].get_name());
                    titlesArray.push(value[titleIndex].get_gender.toString());
                }
                this._get_state().Titles = value;

            }
        }
    },
    
    get_unmatchedText : function()
    {
        return this._parser.get_unmatchedText();
    },
    
    get_cssClass : function() 
    {
        return this._cssClass;
    },
    set_cssClass : function(value) 
    {
        if (this._cssClass !=  value) 
        {
            if (this._cssClass && this.get_isInitialized()) 
            {
                Sys.UI.DomElement.removeCssClass(this._popupDiv, this._cssClass);
            }
            this._cssClass = value;
            if (this._cssClass && this.get_isInitialized()) 
            {
                Sys.UI.DomElement.addCssClass(this._popupDiv, this._cssClass);
            }
            this.raisePropertyChanged("cssClass");
        }
    },
    
    _get_state : function() 
    {
        if(!this._state)
        {
            var serializedState = NhsCui.Toolkit.Web.PatientSearchInputBox.callBaseMethod(this, 'get_ClientState');
            
            if (serializedState != null && serializedState.length > 0)
            {
                this._state = Sys.Serialization.JavaScriptSerializer.deserialize(serializedState);
            }
        }
        
        return this._state;
    }, 
    _save_state : function() 
    {
        if(this._state)
        {
            var serializedState = Sys.Serialization.JavaScriptSerializer.serialize(this._state);
            NhsCui.Toolkit.Web.PatientSearchInputBox.callBaseMethod(this, 'set_ClientState', [serializedState]);
        }
    },
    
    _get_form : function() 
    {
        var elem = this.get_element();
        
        while(elem && elem.nodeName !== "FORM")
        {
            elem = elem.parentNode;
        }
        return elem;
    },    

    initialize : function() 
    {
        NhsCui.Toolkit.Web.PatientSearchInputBox.callBaseMethod(this, "initialize");

        $addHandler(this._get_form(), 'submit', this._formSubmitDelegate);

        // Transfer state 
        this.set_commonFamilyNames(this._get_state().CommonFamilyNames);
        this.set_endGroupDelimiter(this._get_state().EndGroupDelimiter);
        this.set_informationDelimiter(this._get_state().InformationDelimiter);
        this.set_informationFormat(this._get_state().InformationFormat);
        this.set_mandatoryInformation(this._get_state().MandatoryInformation);
        this.set_maximumAge(this._get_state().MaximumAge);
        this.set_startGroupDelimiter(this._get_state().StartGroupDelimiter);
        this.set_titles(this._get_state().Titles);
    },
    
    _getForm : function() 
    {
        var elem = this.get_element();
        
        while(elem && elem.nodeName !== "FORM")
        {
            elem = elem.parentNode;
        }

        return elem;
    },    

    dispose : function() 
    {
        var form = this._getForm();
        if(form)
        {
            $removeHandler(form, 'submit', this._formSubmitDelegate);
        }
        
        NhsCui.Toolkit.Web.PatientSearchInputBox.callBaseMethod(this, "dispose");
    },
    
    parse : function()
    {
        this._parser.set_text(this.get_element().value);
        this._parser.parse();
    },

    _formSubmitHandler : function(sender, args) 
    {
        this._save_state();
    }
};

NhsCui.Toolkit.Web.PatientSearchInputBox.registerClass("NhsCui.Toolkit.Web.PatientSearchInputBox", AjaxControlToolkit.BehaviorBase);
