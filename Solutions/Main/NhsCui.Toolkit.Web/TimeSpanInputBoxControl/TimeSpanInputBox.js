//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for nhs date inputbox</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

// ===========================
// TimeSpanInputBox Main class
// ===========================
NhsCui.Toolkit.Web.TimeSpanInputBox = function(element) 
{
    NhsCui.Toolkit.Web.TimeSpanInputBox.initializeBase(this, [element]);
    
    this._timeSpanParseRegExFormat = "(?:(\\d+)\\s*(?:##UNITLIST##))?";

    // Setup events
    this._blurDelegate = Function.createDelegate(this, this._blurHandler);
    this._changeDelegate = Function.createDelegate(this, this._changeHandler);
    this._propertyChangedDelegate = Function.createDelegate(this, this._propertyChangedHandler);
    
    // Much like the evaluationfunction for a client side validator comes down as a string 
    // and then needs to be evaluated the valAttachedServerSide string needs to converted to an actual boolean 
    if (typeof(this.get_element().parentNode.valAttachedServerSide)== "string")
    {
        this.get_element().parentNode.valAttachedServerSide = Boolean.parse(this.get_element().parentNode.valAttachedServerSide);
    }           
};

// ================================
// TimeSpanInputBox Prototype class
// ================================
NhsCui.Toolkit.Web.TimeSpanInputBox.prototype = 
{ 
    // =================
    // Public Properties
   
    get_from : function()
    {
        return this._getState().Value.get_from();
    },
    set_from : function(value)
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Date}]);
        if (e) throw e;    
    
        this._getState().Value.set_from(value);

        this.raisePropertyChanged('from');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        } 
    },
    
    get_granularity : function()
    {
        return this._getState().Value.get_granularity();
    },
    set_granularity : function(value)
    {
        var e = Function._validateParams(arguments, [{name: "value", type: TimeSpanUnit}]);
        if (e) throw e;    
    
        this._getState().Value.set_granularity(value);
    
        this.raisePropertyChanged('granularity');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        } 
    },
    
    get_isAge : function() 
    {
        return this._getState().Value.get_isAge();
    },
    set_isAge : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Boolean}]);
        if (e) throw e;    

        this._getState().Value.set_isAge(value);
        
        this.raisePropertyChanged("isAge");
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        } 
    },

    get_text : function() 
    {
        return this.get_element().value;
    },
    set_text : function(value) 
    {
        value = this._preParseUnits(value);
        var newNhsTimeSpan = NhsTimeSpan.parseExact(value, this.get_isAge());
        if (newNhsTimeSpan !== null)
        {
            // We don't want to assign new NhsTimeSpan object directly as that would
            // overwrite existing properties like granularity, threshold etc. and we
            // [also have to offset the 'to' property value from any pre-existing value]
            // [[Apparently not - just overwite the From and To from the newNhsTimeSpan. 
            // Doesn't seem like a good ISV experience to me, overwriting values I might already have set...]]
            // var timeSpanTicks = newNhsTimeSpan.get_to().getTime() - newNhsTimeSpan.get_from().getTime();
            // this._getState().Value.set_to(new Date(this._getState().Value.get_from().getTime() + timeSpanTicks));
            this._getState().Value.set_from(newNhsTimeSpan.get_from());
            this._getState().Value.set_to(newNhsTimeSpan.get_to());
            this._getState().Text = this._getState().Value.toString(this.get_unitLength());
            this.get_element().value = this._getState().Value.toString(this.get_unitLength());
        }
        
        this.raisePropertyChanged("text");
    },

    get_threshold : function()
    {
        return this._getState().Value.get_threshold();
    },
    set_threshold : function(value)
    {
        var e = Function._validateParams(arguments, [{name: "value", type: TimeSpanUnit}]);
        if (e) throw e;    
    
        this._getState().Value.set_threshold(value);
    
        this.raisePropertyChanged('threshold');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        } 
    },
    
    get_to : function()
    {
        return this._getState().Value.get_to();
    },
    set_to : function(value)
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Date}]);
        if (e) throw e;    
    
        this._getState().Value.set_to(value);
    
        this.raisePropertyChanged('to');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        } 
    },
    
    get_Tooltip : function() {
        return this.get_element().title;
    },
    set_Tooltip : function(value) {           
        this.get_element().title = value;
    },  
        
    get_value : function() 
    {
        return this._getState().Value;
    },
    set_value : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: NhsTimeSpan, mayBeNull: false}]);
        
        if (e) throw e;
    
        this._getState().Value = value;
                
        this.raisePropertyChanged('value');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        }          
    },
    
     get_unitLength : function() 
    {
        return this._getState().UnitLength;
    },
    set_unitLength : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: TimeSpanUnitLength, mayBeNull: false}]);
        
        if (e) throw e;
    
        this._getState().UnitLength = value;                
                
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeSpanValue();
        }          
    },
    

    // ==============
    // Public Methods
   
    initialize : function() 
    {
        NhsCui.Toolkit.Web.TimeSpanInputBox.callBaseMethod(this, "initialize");

        var elt = this.get_element();
        elt.autocomplete = "off";

        $addHandler(elt, 'blur', this._blurDelegate);
        
        $addHandler(elt, 'change', this._changeHandler);
                
         NhsCuiValidation.SetValidationTargetToActualControl(this.get_element());

        this.add_propertyChanged(this._propertyChangedDelegate);
    },

    dispose : function() 
    {
        var elt = this.get_element();
        $removeHandler(elt, 'blur', this._blurDelegate);

        this.remove_propertyChanged(this._propertyChangedDelegate);
        
        NhsCui.Toolkit.Web.TimeSpanInputBox.callBaseMethod(this, "dispose");
    },

    saveState : function() 
    {
        if(this._state)
        {
            var serializedState = Sys.Serialization.JavaScriptSerializer.serialize(this._state);
            NhsCui.Toolkit.Web.TimeSpanInputBox.callBaseMethod(this, 'set_ClientState', [serializedState]);
        }
    },
    
    // ===============
    // Private Methods
   
    /// <summary>
    /// Implements the extra parsing logic to recognise truncated forms of long TimeSpanUnits
    /// </summary>
    /// <param name="value">Input string to preparse for truncated TSUs</param>
    /// <returns>string regularised to the long TimeSpanUnits version of the input string</returns>
    _preParseUnits : function(value)
    {
        // NOT the same as the NhsTimeSpan version...
        var parseExpression = this._buildParseRegularExpression();
        var regEx = new RegExp(parseExpression, "i");
        var wordMatch = regEx.exec(value);
        var returnValue = "";

        if (wordMatch !== null)
        {
            // now convert to full length versions for easier parsing by NhsTimeSpan...
            if (wordMatch[1] !== undefined && wordMatch[1].length > 0)
            {
                returnValue += parseInt(wordMatch[1], 10) + NhsTimeSpanResources.YearsLongUnit + " ";
            }
            if (wordMatch[2] !== undefined && wordMatch[2].length > 0)
            {
                returnValue += parseInt(wordMatch[2], 10) + NhsTimeSpanResources.MonthsLongUnit + " ";
            }
            if (wordMatch[3] !== undefined && wordMatch[3].length > 0)
            {
                returnValue += parseInt(wordMatch[3], 10) + NhsTimeSpanResources.WeeksLongUnit + " ";
            }
            if (wordMatch[4] !== undefined && wordMatch[4].length > 0)
            {
                returnValue += parseInt(wordMatch[4], 10) + NhsTimeSpanResources.DaysLongUnit + " ";
            }
            if (wordMatch[5] !== undefined && wordMatch[5].length > 0)
            {
                returnValue += parseInt(wordMatch[5], 10) + NhsTimeSpanResources.HoursLongUnit + " ";
            }
            if (wordMatch[6] !== undefined && wordMatch[6].length > 0)
            {
                returnValue += parseInt(wordMatch[6], 10) + NhsTimeSpanResources.MinutesLongUnit + " ";
            }
            if (wordMatch[7] !== undefined && wordMatch[7].length > 0)
            {
                returnValue += parseInt(wordMatch[7], 10) + NhsTimeSpanResources.SecondsLongUnit + " ";
            }
        }

        if (returnValue !== null && returnValue !== undefined && returnValue.length > 0)
        {
            return returnValue.trimEnd();
        }
        else
        {
            return value;
        }
    },

    /// <summary>
    /// Builds the RegEx that allows us to preParse a time span string. 
    /// </summary>
    /// <returns>regular expression string</returns>
    _buildParseRegularExpression : function()
    {
        var regExPattern = new Sys.StringBuilder();
        regExPattern.append("^");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.YearsUnit,
                                                    NhsTimeSpanResources.YearUnit,
                                                    NhsTimeSpanResources.YearsLongUnit,
                                                    NhsTimeSpanResources.YearLongUnit)));
        regExPattern.append("\\s*");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.MonthsUnit,
                                                    NhsTimeSpanResources.MonthUnit,
                                                    NhsTimeSpanResources.MonthsLongUnit,
                                                    NhsTimeSpanResources.MonthLongUnit)));
        regExPattern.append("\\s*");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.WeeksUnit,
                                                    NhsTimeSpanResources.WeekUnit,
                                                    NhsTimeSpanResources.WeeksLongUnit,
                                                    NhsTimeSpanResources.WeekLongUnit)));
        regExPattern.append("\\s*");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.DaysUnit,
                                                    NhsTimeSpanResources.DayUnit,
                                                    NhsTimeSpanResources.DaysLongUnit,
                                                    NhsTimeSpanResources.DayLongUnit)));
        regExPattern.append("\\s*");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.HoursUnit,
                                                    NhsTimeSpanResources.HourUnit,
                                                    NhsTimeSpanResources.HoursLongUnit,
                                                    NhsTimeSpanResources.HourLongUnit)));
        regExPattern.append("\\s*");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.MinutesUnit,
                                                    NhsTimeSpanResources.MinuteUnit,
                                                    NhsTimeSpanResources.MinutesLongUnit,
                                                    NhsTimeSpanResources.MinuteLongUnit)));
        regExPattern.append("\\s*");
        regExPattern.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.SecondsUnit,
                                                    NhsTimeSpanResources.SecondUnit,
                                                    NhsTimeSpanResources.SecondsLongUnit,
                                                    NhsTimeSpanResources.SecondLongUnit)));
        regExPattern.append("$");
        return regExPattern.toString();
    },

    /// <summary>
    /// Build reg ex to match single time span units
    /// </summary>
    /// <param name="unitNames">names used for unit</param>
    /// <returns>reg ex pattern</returns>
    _buildUnitRegEx : function(unitNames)
    {
        var enhancedUnitNames = new Array();
        for (var nameIndex = 0; nameIndex < unitNames.length; nameIndex++)
        {
            var partString = new Sys.StringBuilder();
            for (var charIndex = 0; charIndex < unitNames[nameIndex].length; charIndex++)
            {
                partString.append(unitNames[nameIndex].substr(charIndex, 1));
                if (Array.contains(enhancedUnitNames, partString.toString()) === false)
                {
                    Array.add(enhancedUnitNames, partString.toString());
                }
            }
        }
        
        var regExPattern = this._timeSpanParseRegExFormat.replace("##UNITLIST##", enhancedUnitNames.join("|"));
        return regExPattern;
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

    _updateHtmlElementsToTimeSpanValue : function() 
    {
        var textBox = this.get_element();

        var formattedTimeSpan = this._formattedTimeSpan();

        textBox.value = formattedTimeSpan;
    },

    _formattedTimeSpan : function() 
    {
        var value = this.get_value();
        var formattedTimeSpan;
                 
        formattedTimeSpan = value.toString(this.get_unitLength());
        
        return formattedTimeSpan;
    },

    _changeHandler : function(e)
    {
        Sys.Debug.trace("hggG");
    },

    _propertyChangedHandler : function(sender, args) 
    {
        this.saveState();
    },

    _blurHandler : function(e) 
    {
        var text = this.get_text();
        text = this._preParseUnits(text);
       
        try
        {
            var isValidTimeSpan;
            var nhsCuiTimeSpanInputBoxValidator = NhsCuiValidation.GetAttachedValidatorOfSpecificType(this.get_element(), "NhsCui.TimeSpanInputBoxValidator");
            var validatorAttached = false; // take the default position that a validator will not be attached
            
            if (nhsCuiTimeSpanInputBoxValidator!==null)
            {
                //There is an NhsCui.TimeSpanInputBoxValidator attached use that to perform the validation 
                validatorAttached = true;
                
                //Ask the validator to validate
                ValidatorValidate(nhsCuiTimeSpanInputBoxValidator);
                isValidTimeSpan = nhsCuiTimeSpanInputBoxValidator.isvalid;
            }
            else
            {
                // No client side NhsCui validator was found
                
                // NhsTimeSpan.tryParse is the current extent of our validation

                var testTimeSpan = NhsTimeSpan.tryParse(text, this.get_isAge());
                
                if(testTimeSpan)
                {
                    isValidTimeSpan = true;
                }
            }
            
            if (isValidTimeSpan)
            {
                var newNhsTimeSpan = NhsTimeSpan.tryParse(text, this.get_isAge());
                
                this.set_from(newNhsTimeSpan.get_from());
                this.set_to(newNhsTimeSpan.get_to());
                
                //This is necessary because the act of parsing and validating may well have caused it to reformat
                this._updateHtmlElementsToTimeSpanValue();
            }
            else
            {
                //Parsing and validation has not allowed us to come up with a valid timespan
                if (validatorAttached===true || this.get_element().parentNode.valAttachedServerSide===true)
                {
                    Sys.Debug.trace("Running code that will clear");
                    
                    // Reset the value's dates
                    // .N.B Yes we are bypassing the local this.set_from & this.set_to because we just want to set the 
                    // the date not have the control do stuff for us (such as running _updateHtmlElementsToTimeSpanValue)
                    var emptyNhsTimeSpan = NhsTimeSpan.tryParse("");
                    this._getState().Value.set_from(emptyNhsTimeSpan.get_from());
                    this._getState().Value.set_to(emptyNhsTimeSpan.get_to());
                }
                else
                {
                    this._updateHtmlElementsToTimeSpanValue();
                }
            }
        }
        catch(ex)
        {
            // Parsing and/or validation threw up an unhandled exception. 
            
            //Only set the elements back to a valid date if no validator is attached
            
            if (validatorAttached===true || this.get_element().parentNode.valAttachedServerSide===true)
            {
                // Reset the value's dates 
                // .N.B Yes we are bypassing the local this.set_from & this.set_to because we just want to set the 
                // the date not have the control do stuff for us
                var emptyNhsTimeSpan = NhsTimeSpan.tryParse("");
                this._getState().Value.set_from(emptyNhsTimeSpan.get_from());
                this._getState().Value.set_to(emptyNhsTimeSpan.get_to());
            }
            else
            {
                this._updateHtmlElementsToTimeSpanValue();
            }
        }
    },

    _getState : function() 
    {
        if(!this._state)
        {
            var serializedState = NhsCui.Toolkit.Web.TimeSpanInputBox.callBaseMethod(this, 'get_ClientState');
            
            if (serializedState !== null && serializedState.length > 0)
            {
                this._state = Sys.Serialization.JavaScriptSerializer.deserialize(serializedState);
                this._state.Value = $create(NhsCui.Toolkit.Web.NhsTimeSpan, this._state.Value, null, null);
            }
            else
            {
                this._state = { Value : new NhsTimeSpan()};
            }
        }
        
        return this._state;
    }       
};

NhsCui.Toolkit.Web.TimeSpanInputBox.registerClass("NhsCui.Toolkit.Web.TimeSpanInputBox", AjaxControlToolkit.BehaviorBase);
