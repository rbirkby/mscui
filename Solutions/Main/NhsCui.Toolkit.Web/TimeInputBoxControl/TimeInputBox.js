//-----------------------------------------------------------------------
// <copyright file="TimeInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>03-Jan-2007</date>
// <summary>Client-side javascript for NHS time inputbox</summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

Type.registerNamespace('NhsCui.Toolkit.Web');

var TimeFunctionality = function() 
{
};
TimeFunctionality.prototype = 
{
        Complex:0,
        Simple:1
};
TimeFunctionality.registerEnum("TimeFunctionality");

NhsCui.Toolkit.Web.TimeInputBox = function (element) 
{
    NhsCui.Toolkit.Web.TimeInputBox.initializeBase(this, [element]);

    this.freeformMode = 0;
    this.arithmeticMode = 1;
    this.assistedFreeformMode = 2;
    this.nullEntryMode = 3;

    this._mode = this.freeformMode;

    this._nullStrings = [];
    this._lastSelectionPos = null;
    this._hasFocus = false;
    this._enabled = true;
    this._valueIsNullString = false; 
    this._currentValue = "";       
    
    // Tracks if the control has focus or not. It includes internal textbox and spin buttons.
    // has focus tracks the focus for textbox only.
    this._controlHasFocus = false;
    
    this._keyDownDelegate = Function.createDelegate(this, this._keyDownHandler);
    this._keyPressDelegate = Function.createDelegate(this, this._keyPressHandler);
    this._clickDelegate = Function.createDelegate(this, this._clickHandler);
    this._mouseScrollDelegate = Function.createDelegate(this, this._mouseScrollHandler);
    this._blurDelegate = Function.createDelegate(this, this._blurHandler);
    this._focusDelegate = Function.createDelegate(this, this._focusHandler);
    this._resizeDelegate = Function.createDelegate(this, this._resizeHandler);
    this._dragDelegate = Function.createDelegate(this, this._dragHandler);
    this._approxCheckedDelegate = Function.createDelegate(this, this._approxCheckedHandler);
    this._upButtonClickDelegate = Function.createDelegate(this, this._upButtonClickHandler);    
    this._downButtonClickDelegate = Function.createDelegate(this, this._downButtonClickHandler);
    this._propertyChangedDelegate = Function.createDelegate(this, this._propertyChangedHandler);                
        
    this._upButton = null;
    this._downButton = null;           
    
    // Much like the evaluationfunction for a client side validator comes down as a string 
    // and then needs to be evaluated the valAttachedServerSide string needs to converted to an actual boolean 
    if (typeof(this.get_element().parentNode.valAttachedServerSide)== "string")
    {
        this.get_element().parentNode.valAttachedServerSide = Boolean.parse(this.get_element().parentNode.valAttachedServerSide);
    }
};
NhsCui.Toolkit.Web.TimeInputBox.prototype = 
{
    get_checkBoxCssClass : function() {
        return this._getState().CheckBoxCssClass;
    },
    set_checkBoxCssClass  : function(value) {        
        var e = Function._validateParams(arguments, [
            {name: "value", type: String, mayBeNull: false}
        ]);

        if (e) throw e;
        var oldCheckBoxCss = this._getState().CheckBoxCssClass;
        var bSameValueAsBefore = false;
        if (oldCheckBoxCss)
        {
            if (oldCheckBoxCss != value) //only change css if different
            {                                
                if (this._approximateControl) //the control exists, otherwise nothing to apply.
                {                           
                    Sys.UI.DomElement.removeCssClass(this._approximateControl, oldCheckBoxCss);                    
                }
            }
            else
            {
                bSameValueAsBefore = true;
            }
        }
        
        if(!bSameValueAsBefore)
        { 
            this._getState().CheckBoxCssClass = value; //set the bags new checkbox css style
            if (value)
            {
                Sys.UI.DomElement.addCssClass(this._approximateControl, value);
            }            
            this.raisePropertyChanged("checkBoxCssClass");                                      
        }
    },
    get_enabled : function() 
    { 
        return this._enabled; 
    },
    set_enabled : function(value) 
    { 
        if (this._enabled != value) 
        {
            var inputBox = this.get_element();
            this._enabled = value;
            inputBox.disabled = !value;
            if(this._upButton)
            {
                this._upButton.disabled = !value;
                this._downButton.disabled = !value;
            }
            
            if(this._approximateControl)
            {
                this._approximateControl.childNodes[0].disabled = !value;
            }
            this.raisePropertyChanged('enabled');
        }
    },
    
    get_functionality : function() 
    {
        return this._getState().Functionality;
    },
    set_functionality : function(value) 
    {
       var e = Function._validateParams(arguments, [{name: "value", type: Number}]);
        if (e) throw e;    
    
        this._getState().Functionality = value;
        
        if(value === TimeFunctionality.Simple)
        {
            this.set_timeType(TimeType.Exact);
        }
        
        this.raisePropertyChanged('functionality');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        } 
    },       
    get_TooltipText : function() {
        return this.get_element().title;
    },
    set_TooltipText : function(value) {           
        this.get_element().title = value;
    },        
    get_allowApproximate : function() 
    {
        return this._getState().AllowApproximate;
    },
    set_allowApproximate : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Boolean}]);
        if (e) throw e;
             
        this._getState().AllowApproximate = value;
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }      
        
        this.raisePropertyChanged('allowApproximate');
        this._updateApproximateElement();
    },
    
    get_displaySeconds : function() 
    {
        return this._getState().DisplaySeconds;
    },
    set_displaySeconds : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Boolean}]);
        if (e) throw e;
             
        this._getState().DisplaySeconds = value;
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }      
        
        this.raisePropertyChanged('displaySeconds');
    },
    
    get_display12Hour : function() 
    {
        return this._getState().Display12Hour;
    },
    set_display12Hour : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Boolean}]);
        if (e) throw e;
             
        this._getState().Display12Hour = value;
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }      
        
        this.raisePropertyChanged('display12Hour');
    },
    
    get_displayAMPM : function() 
    {
        return this._getState().DisplayAMPM;
    },
    set_displayAMPM : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: Boolean}]);
        if (e) throw e;
             
        this._getState().DisplayAMPM = value;
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }      
        
        this.raisePropertyChanged('displayAMPM');
    },
    
    get_nullStrings : function() 
    {
        return this._nullStrings.join(",");
    },
    set_nullStrings : function(value) 
    {
        this._nullStrings = (value && value.length > 0 ? value.split(",") : []);
        
        // check duplicate null string.
        for (i=0;i<this._nullStrings.length;i++)
        {
            for (j=i+1;j<this._nullStrings.length;j++)
            {
                if (this._nullStrings[i].toUpperCase() == this._nullStrings[j].toUpperCase())
                {                                        
                    throw Error.argument("value", NhsCui.Toolkit.Web.TimeInputBoxResources.DuplicateNullString + "'" + this._nullStrings[j] + "'");
                }
            }
        }
        
        this.raisePropertyChanged('nullStrings');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }        
    },
    
    get_value : function() 
    {
        return this._getState().Value;
    },
    set_value : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: NhsTime, mayBeNull: false}]);
        if (e) throw e;        
    
        if(this.get_functionality() === TimeFunctionality.Simple && value.get_timeType() !== TimeType.Exact) 
        {
            throw Error.argumentOutOfRange("value.timeType", value.get_timeType(), "Only TimeType.Exact mode is allowed when Functionality is set to TimeFunctionality.Simple");
        }
        
        this._getState().Value = value;
    
        this.raisePropertyChanged('value');
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
            
        }  
    },       
    
    get_timeType : function() 
    {
        return this.get_value().get_timeType();
    },
    set_timeType : function(value) 
    {
        var time = this.get_value();
        time.set_timeType(value);
        this.set_value(time);    
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }  
    },       
    
    _setTimeTypeToNullOnError : function() 
    {
        this.get_value().set_timeType(TimeType.Null);
    },
    
    get_timeValue : function() 
    {
        return this.get_value().get_timeValue();
    },
    set_timeValue : function(value) 
    {
       var time = this.get_value();
        time.set_timeValue(value);
        this.set_value(time);
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToTimeValue();
        }          
    },
    
    get_validatableString : function()
    {        
       return this.get_element().value;        
    },
    
    _ensureApproximateControlCreated : function()
    {
        if(!this._approximateControl)
        {
            // Members for the approximate checkbox
            var element = this.get_element();
            this._approximateControl = document.createElement("span");
            var input = document.createElement("input");
            var label = document.createElement("label");
            input.type = "checkbox";
            input.name = element.id + "_approximate";
            input.id = element.id + "_approximate";
            input.disabled = !this._enabled;
            label.setAttribute("for", input.id);
            label.appendChild(document.createTextNode(NhsTimeResources.Approximate));
            this._approximateControl.appendChild(input);
            this._approximateControl.appendChild(label);
            element.parentNode.insertBefore(this._approximateControl, element.nextSibling);         
            $addHandler(input, 'click', this._approxCheckedDelegate);
            var checkboxCssStyle = this._getState().CheckBoxCssClass;
            if (checkboxCssStyle)
            {
                Sys.UI.DomElement.addCssClass(this._approximateControl, checkboxCssStyle);
            }
        }
    },
    
    _updateApproximateElement : function() 
    {        
        if(this.get_allowApproximate() && this.get_functionality() === TimeFunctionality.Complex)
        {
            this._ensureApproximateControlCreated();
            this._approximateControl.style.display = "inline";
            var approxCheckBox = this._approximateControl.childNodes[0];
            var timeType = this.get_timeType();
            approxCheckBox.checked = (timeType === TimeType.Approximate);
            approxCheckBox.disabled = (timeType !== TimeType.Approximate && timeType !== TimeType.Exact);
        }
        else if(this._approximateControl)
        {
            this._approximateControl.style.display = "none";
        }
    },
    
    _updateHtmlElementsToTimeValue : function() 
    {
        var textBox = this.get_element();
        
        this._updateApproximateElement();
        
        var formattedTime = this._getFormattedTime();
        if(formattedTime !== textBox.value)
        {
            textBox.value = formattedTime;
        }
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
    
    initialize : function() 
    {
        NhsCui.Toolkit.Web.TimeInputBox.callBaseMethod(this, 'initialize');
        var elt = this.get_element();
        elt.autocomplete = "off";
        
        $addHandler(elt, 'keydown', this._keyDownDelegate);
        $addHandler(elt, 'keypress', this._keyPressDelegate);
        $addHandler(elt, 'click', this._clickDelegate);
        $addHandler(document, 'DOMMouseScroll', this._mouseScrollDelegate);
        $addHandler(document, 'mousewheel', this._mouseScrollDelegate);
        $addHandler(elt, 'blur', this._blurDelegate);                
        $addHandler(elt, 'focus', this._focusDelegate);
        $addHandler(elt, 'resize', this._resizeDelegate);
        $addHandler(elt, 'move', this._resizeDelegate);
        $addHandler(elt, 'DOMAttrModified', this._resizeDelegate);
        $addHandler(window, 'resize', this._resizeDelegate);
        $addHandler(elt, 'dragover', this._dragDelegate);
        $addHandler(elt, 'dragenter', this._dragDelegate);
        $addHandler(elt, 'drop', this._dragDelegate);
              
       NhsCuiValidation.SetValidationTargetToActualControl(this.get_element());        
        
        this.add_propertyChanged(this._propertyChangedDelegate);

        this._addSpinButtons();        
        this._updateApproximateElement();
        
        var checkBoxCss = this._getState().CheckBoxCssClass;
        if (checkBoxCss)
        {
            if ("" != checkBoxCss)
            {
                Sys.UI.DomElement.addCssClass(this._approximateControl, checkBoxCss);
            }
        } 
    },
    
    dispose : function() 
    {
        var elt = this.get_element();        
        var parentElement = elt.parentNode;     
        this.set_allowApproximate(false);
        $removeHandler(elt, 'keydown', this._keyDownDelegate);
        $removeHandler(elt, 'keypress', this._keyPressDelegate);
        $removeHandler(elt, 'click', this._clickDelegate);
        $removeHandler(document, 'DOMMouseScroll', this._mouseScrollDelegate);
        $removeHandler(document, 'mousewheel', this._mouseScrollDelegate);
        $removeHandler(elt, 'blur', this._blurDelegate);   
        $removeHandler(elt, 'focus', this._focusDelegate);   
        $removeHandler(elt, 'resize', this._resizeDelegate);
        $removeHandler(elt, 'move', this._resizeDelegate);
        $removeHandler(elt, 'DOMAttrModified', this._resizeDelegate);
        $removeHandler(elt, 'dragover', this._dragDelegate);   
        $removeHandler(elt, 'dragenter', this._dragDelegate);  
        $removeHandler(elt, 'drop', this._dragDelegate);
        $removeHandler(window, 'resize', this._resizeDelegate);        
        $removeHandler(this._upButton, 'click', this._upButtonClickDelegate);
        $removeHandler(this._downButton, 'click', this._downButtonClickDelegate);        
        parentElement.removeChild(this._upButton);        
        parentElement.removeChild(this._downButton);         
        
        if(this._approximateControl)
        {
            $removeHandler(this._approximateControl.childNodes[0], 'click',this._approxCheckedDelegate);
        }
        
        this.remove_propertyChanged(this._propertyChangedDelegate);
        
        NhsCui.Toolkit.Web.TimeInputBox.callBaseMethod(this, 'dispose');
    },   
    
    _getState : function() 
    {
        if(!this._state)
        {
            var serializedState = NhsCui.Toolkit.Web.TimeInputBox.callBaseMethod(this, 'get_ClientState');
            
            if (serializedState != null && serializedState.length > 0)
            {
                this._state = Sys.Serialization.JavaScriptSerializer.deserialize(serializedState);
                
                //Swap a JSON'ed NhsTime for a real one
                this._state.Value = $create(NhsCui.Toolkit.Web.NhsTime, this._state.Value, null, null);
            }
            else
            {
                this._state = { "Value" : new NhsTime(),
                                "Functionality" : TimeFunctionality.Complex,
                                "AllowApproximate" : false,
                                "DisplaySeconds" : false,
                                "Display12Hour" : false,
                                "DisplayAMPM" : false
                                };
            }
        }
        
        return this._state;
    }, 
    
    _saveState : function() 
    {   
        if(this._state)
        {
            var serializedState = Sys.Serialization.JavaScriptSerializer.serialize(this._state);
            NhsCui.Toolkit.Web.TimeInputBox.callBaseMethod(this, 'set_ClientState', [serializedState]);
        }
    },
    
    _addSpinButtons : function() 
    {
        var element = this.get_element();                     
        var parentElement = element.parentNode;
                                    
        // Up button
        this._upButton = this._createSpinButton(true);
        this._upButton.id = this.get_id() + "_up";

        // Down button
        this._downButton = this._createSpinButton(false);
        this._downButton.id = this.get_id() + "_down";
               
        parentElement.appendChild(this._upButton);
        parentElement.appendChild(this._downButton);        
               
        this._positionSpinButtons();
                
        $addHandler(this._upButton, "click", this._upButtonClickDelegate);        
        $addHandler(this._downButton, "click", this._downButtonClickDelegate);
    },
    
    _positionSpinButtons : function() 
    {
        var element = this.get_element();                     
        var parentElement = element.parentNode;
        parentElement.style.position = "relative";
        
        var borders = CommonToolkitScripts.getBorderBox(element);
        if(Sys.Browser.agent === Sys.Browser.InternetExplorer && borders.left === 0)
        {
            // getBorderBox doesn't seem to work on ie
            borders.left = Math.floor((element.offsetWidth - element.clientWidth) / 2);
            borders.top =  Math.floor((element.offsetHeight - element.clientHeight) / 2);
            borders.right = borders.left;
            borders.bottom = borders.top;
        }
        
        // Fix for PS#4779
        // Don't want the button to be a square based on control height - rather it should
        // occupy a percentage of the width based on the default size of the control of 
        // 146 wide and 22 high - otherwise a square control would be entirely filled by the spinbuttons
        var adjustedButtonWidth = Math.round(element.offsetWidth * 22 / 146);
        adjustedButtonWidth = this._buttonWidth = adjustedButtonWidth >= 22 ? adjustedButtonWidth : 22;
        
        var yAxis;
        if(Sys.Browser.agent === Sys.Browser.Firefox)
        {
            yAxis = element.offsetTop + borders.top;
        }
        else
        {
            yAxis = borders.top + 1;
        }
        
        var upBounds = { x : element.offsetLeft + element.offsetWidth - adjustedButtonWidth - borders.right,
                            y : yAxis - 1,
                            width : adjustedButtonWidth,
                            height : Math.round((element.offsetHeight - borders.top - borders.bottom) / 2) + 1};
        var downBounds = { x : upBounds.x,
                            y : upBounds.y + upBounds.height,
                            width : upBounds.width,
                            height : upBounds.height };

        // Fix for PS#6142 - tweaked the sizing/positioning to avoid spinners being drawn too small or in the
        // wrong place and leaving a small margin of the textbox around them - problems with wrong tooltip
        // firing.
        // NB: There will still be this problem on occasions if the CSS is changed or the page is scaled
        // using Ctrl+/Ctrl- owing to sizing/positioning calcs rounding issues OR if the mouse is exactly
        // positioned on the 1px border of the textbox surrounding the spinners...
        if(Sys.Browser.agent === Sys.Browser.Firefox)
        {
            upBounds.x++;
            downBounds.x++;
        }
        
        this._setElementBounds(this._upButton, upBounds);
        this._setElementBounds(this._downButton, downBounds);        
    },
    
    _setElementBounds : function(element, bounds) 
    {
        element.style.width = (bounds.width > 0 ? bounds.width : 0) + "px";
        element.style.height = (bounds.height > 0 ? bounds.height : 0) + "px";
        Sys.UI.DomElement.setLocation(element, bounds.x, bounds.y);
    },
    
    _createSpinButton : function(up) 
    {
        var button = document.createElement("input");
        button.type = "button";
        button.style.borderWidth = "0px";
        
        if (Sys.Browser.agent == Sys.Browser.InternetExplorer)
        {
            button.style.fontFamily = "Webdings";
            button.value = (up ? "5" : "6");
            button.style.fontSize = "12px";
        }
        else
        {
            button.style.fontFamily = "Tahoma, Arial, sans-serif";
            button.value = (up ? "\u25B2" : "\u25BC");
            button.style.fontSize = "6px";
            button.style.fontWeight = "bold";
        }
        
        button.tabIndex = -1;
        button.disabled = !this._enabled;
        
        if(NhsCui.Toolkit.Web.TimeInputBoxResources)
        {
            button.title = (up ? NhsCui.Toolkit.Web.TimeInputBoxResources.SpinUpButtonTitle : NhsCui.Toolkit.Web.TimeInputBoxResources.SpinDownButtonTitle);
        }
        
        button.style.textAlign = "center";
        button.style.backgroundColor = "ButtonFace";
        button.style.overflow = "hidden";
        button.style.lineHeight = "1em";
   
        return button;
    },
    
    _mouseScrollHandler : function(e) 
    {
        if(!this._enabled || !this._hasFocus) return;
        
        var delta = 0;
        if(e.rawEvent.wheelDelta)
        {
            delta = e.rawEvent.wheelDelta / 120;
        }
        else if(e.rawEvent.detail)
        {
            delta = -e.rawEvent.detail / 3;
        }
        
        var key= (delta < 0) ? Sys.UI.Key["down"] : Sys.UI.Key["up"];
        
        e.preventDefault();
        
        for(var i = 0; i < Math.abs(delta); i++)
        {
            this._keyDownHandler({keyCode:key});
        }
    },
        
    _resizeHandler : function(e) 
    {      
        this._positionSpinButtons();
    },
    
    _focusHandler : function(e) 
    {
        if(this._enabled)
        {           
            var inputBox = this.get_element();            
            this._hasFocus = true;
            this._controlHasFocus = true;
            this._currentValue = inputBox.value;
        }
    },
    
    _clickHandler : function() 
    {
        if(this._enabled)
        {            
            this._getSelectionPos(this.get_element());
        }
    },       
    
    _setLastFieldIndex : function()
    {
        var displayAMPM = this.get_displayAMPM();
        var inputBox = this.get_element();
        var amDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.AMDesignator.toLowerCase();
        var pmDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.PMDesignator.toLowerCase();
        this._lastSelectionPos = {start:inputBox.selectionStart, end:inputBox.selectionEnd};   
        if(displayAMPM && (inputBox.value.indexOf(amDesignator) > 0 || inputBox.value.indexOf(pmDesignator) > 0))
        {
             this._lastSelectionPos.end = inputBox.value.length - 4;
             this._lastSelectionPos.start = this._lastSelectionPos.end - 2;
        }
        else
        {
             this._lastSelectionPos.end = inputBox.value.length;
             this._lastSelectionPos.start = this._lastSelectionPos.end - 2;
        }                   
    }, 
        
    _blurHandler : function(e) 
    {       
        if(this._enabled)
        {                                   
            var inputBox = this.get_element();                      
                        
            this._hasFocus = false;
            this._finishEditing(inputBox);
            
             if (inputBox.value !== this._currentValue)
             {
                this._raiseChange();                
             }
            
            // Set the flag for focus going completely out of this control (including spin buttons.)
            if (Sys.Browser.agent == Sys.Browser.InternetExplorer || Sys.Browser.agent == Sys.Browser.Safari)
            {
                if (document.activeElement == null || (document.activeElement.id != this.get_id() + "_up" && document.activeElement.id != this.get_id() + "_down"))
                {
                    this._controlHasFocus = false;                                                   
                }
            }
        }
    },
    
    _upButtonClickHandler : function(e) 
    {       
        var element = this.get_element();                     
        var parentElement = element.parentNode;
        
        this._upAndDown(true);
    },       
    
    _downButtonClickHandler : function(e) 
    {
        this._upAndDown(false);
    },
    
    _upAndDown : function(doUpAction) 
    {
        /// <summary>This is the handler logic for both the upButton and the downButton</summary>
        // The doUpAction boolean decides whether we are doing an "up" or a "down"
        
        var inputBox = this.get_element();
        
        if(!this._hasFocus)
        {
            if (!this._controlHasFocus){
            this._setLastFieldIndex();
            }
           
            if(this._lastSelectionPos){              
                this._selectRange(inputBox, this._lastSelectionPos.start, this._lastSelectionPos.end);
            }                        
            
            if(!inputBox.disabled){
                
                try{
                    inputBox.focus();
                }
                catch(e){
                    //ignore
                }
            }
        }
        this._handleFreeFormTextInput(inputBox, {charCode: (doUpAction ? Sys.UI.Key["up"] : Sys.UI.Key["down"])});
        this._finishEditing(inputBox);
    },
    
    _dragHandler : function(e) 
    {
        if(this._enabled)
        {
            // Cancel dragdrop
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
    },
    
    _approxCheckedHandler : function(e) 
    {
        if(this._approximateControl.childNodes[0].checked) 
        {
            this.set_timeType(TimeType.Approximate);
        }
        else 
        {
            this.set_timeType(TimeType.Exact);
        }
        this._updateHtmlElementsToTimeValue();
    },
    _propertyChangedHandler: function(sender, args) {
        this._saveState();
    },
    
    _getFormattedTime : function() 
    {
        var value = this.get_value();
        
        if(value.get_timeType() === TimeType.NullIndex && this._nullStrings &&
                value.get_nullIndex() >= 0 && value.get_nullIndex() < this._nullStrings.length)
        {
            return this._nullStrings[value.get_nullIndex()];
        }
        else 
        {
            return value.toString(false, 
                                    Sys.CultureInfo.CurrentCulture, 
                                    this.get_displaySeconds(),
                                    this.get_display12Hour(),
                                    this.get_displayAMPM());
        }
    },                   
    
    _finishEditing : function(inputBox) 
    {                 
        // take the default position that a validator will not be attached
        var validatorAttached = false;         
        
        // In current design there is no direct way to identify null string in base class.
        this._valueIsNullString = false;
        
        //var text = inputBox.value.replace(new RegExp("\\.", "gi"), Sys.CultureInfo.CurrentCulture.dateTimeFormat.TimeSeparator);
        var text = inputBox.value;
        try
        {
            var updateTime = true;
            var newTime = null;
            
            var functionality = this.get_functionality();
            
            //If using complex functionality try parsing as an "Add Instruction" or as a Null String
            if(functionality === TimeFunctionality.Complex)
            {
                if(NhsTime.isAddInstruction(text))
                {
                    newTime = this.get_value();
                    newTime.add(text);
                    updateTime = false;
                }
                else
                {
                    var nullIndex = this._findNullIndex(text);
                    if(nullIndex >= 0)
                    {
                        newTime = new NhsTime();
                        newTime.set_timeType(TimeType.NullIndex);
                        newTime.set_nullIndex(nullIndex);
                        this._valueIsNullString = true;
                    }
                }
            }
            
            if(!newTime)
            {
                //No date as a result of Add or Null Index parsing so try it as a valid time
                var isValidTime;
               var nhsCuiTimeInputBoxValidator = NhsCuiValidation.GetAttachedValidatorOfSpecificType(this.get_element(), "NhsCui.TimeInputBoxValidator");
                
                if (nhsCuiTimeInputBoxValidator !== null){
                    //There is an NhsCui.TimeInputBoxValidator attached use that to perform the validation 
                    validatorAttached = true;
                    
                    ValidatorValidate(nhsCuiTimeInputBoxValidator);
                    isValidTime = nhsCuiTimeInputBoxValidator.isvalid;
                }
                else
                {
                    // NhsTime.tryParse is the current extent of our validation
                    
                    // var testTime = NhsTime.tryParse(text);
                    var testTime = NhsTime.tryParse(text);
                    
                    if(testTime)
                    {
                        isValidTime = true;
                    }
                }
                
                if(isValidTime)
                {                
                    newTime = NhsTime.tryParse(text);
                    
                    //Now that we have a valid time do the approximate check
                    if(newTime.get_timeType() === TimeType.Exact && 
                        this._approximateControl && this._approximateControl.childNodes[0].checked)
                    {
                        newTime.set_timeType(TimeType.Approximate);
                    }
                }
            }
            
            if(newTime && (functionality === TimeFunctionality.Complex ||
                                            newTime.get_timeType() === TimeType.Exact))
            {
                var value = this.get_value();
                if(updateTime && (value.get_timeType() === TimeType.Exact || value.get_timeType() === TimeType.Approximate) &&
                    (newTime.get_timeType() === TimeType.Exact || newTime.get_timeType() === TimeType.Approximate))
                {
                    // update the time AND preserve any date
                    var milliSecsPerDay = 86400000;
                    var milliSecs = Math.floor(value.get_timeValue().getTime() / milliSecsPerDay) * milliSecsPerDay +
                                    newTime.get_timeValue().getTime() % milliSecsPerDay;
                    
                    value.get_timeValue().setTime(milliSecs);
                    value.set_timeType(newTime.get_timeType());                      
                    this.set_value(value);
                }
                else
                {
                    this.set_value(newTime);
                }
                
                NhsCuiValidation.FireAllAttachedValidators(this.get_element());                                   
            }
            else
            {
                 //Parsing and validation has not allowed us to come up with a valid time
                
                if (validatorAttached===true || this.get_element().parentNode.valAttachedServerSide===true){
                    //Leave the TextBox as it is but set the internal value to Null
                    this._setTimeTypeToNullOnError();
                }
                else
                {
                    this._updateHtmlElementsToTimeValue();
                }
            }
        }
        catch(e)
        {            
            // Parsing and/or validation threw up an unhandled exception. 
            
            //Only set the elements back to a valid time if no validator is attached
             
            if (validatorAttached===true || this.get_element().parentNode.valAttachedServerSide==true){
                //Leave Text box in the state it is but set the internal value to Null
                this._setTimeTypeToNullOnError();
            }
            else
            {
                this._updateHtmlElementsToTimeValue();
            }
        }
        
        // switch back to freeform
        this._mode = this.freeformMode;                            
    },
    
   // This function is different from NhsCuiValidation.GetAttachedValidatorOfSpecificType.
   // It returns an array of all the validators.
    _getAttachedNhsCuiTimeInputBoxValidator : function()
    {
        ///<summary>If present return the attached NhsCui.TimeInputBoxValidator</summary>
        var validatorArray = new Array();
        if (this.get_element().Validators)
        {
            //Loop through all the validators attached to the current element looking for one marked as an NhsCui.TimeInputBoxValidator
            for (var i = 0; i < this.get_element().Validators.length;i++)
            {
                if (this.get_element().Validators[i].valtype === "NhsCui.TimeInputBoxValidator")
                {
                    validatorArray[i] =  this.get_element().Validators[i];
                }
            }
        }
        
        return validatorArray;
    },  

    _findNullIndex : function(value) {
        var lowerValue = value.trim().toLowerCase();
        for(var i = 0; i < this._nullStrings.length; i++)
        {
            if(this._nullStrings[i].toLowerCase() === lowerValue)
            {
                return i;
            }
        }
        return -1;
    },
    
    _keyDownHandler : function(e) 
    {
        var inputBox = this.get_element();                        
        if (!this._enabled)
        {
            return;
        }                                                      
    
        if(e.keyCode === Sys.UI.Key["enter"])
        {            
            //this._finishEditing(this.get_element());            
            
            this._selectRange(inputBox, inputBox.value.length, inputBox.value.length);
            
            // Implement LostFocusOnEnter functionality.
            this.get_element().blur();            
            
            // Enter key does other things. So swallow it early            
            e.preventDefault();
            e.stopPropagation();
        }
        
        if(Sys.Browser.agent === Sys.Browser.InternetExplorer ||
                    Sys.Browser.agent === Sys.Browser.Safari)
        {
            // swallow delete and backspace (IE only)
            if((e.keyCode === Sys.UI.Key["del"] || e.keyCode === Sys.UI.Key["backspace"]) && 
                                    this._mode !== this.arithmeticMode)
            {
                if(e.preventDefault)e.preventDefault();
            }      
            
            // For Shift + End update the last selection so that up down button hv the latest info.
            if (e.keyCode == Sys.UI.Key["end"])
            {
                if (window.event.shiftKey)
                    this._lastSelectionPos =  {start:0, end:inputBox.value.length};                            
                else if (!window.event.altKey)
                    this._lastSelectionPos =  {start:inputBox.value.length, end:inputBox.value.length};                            
            }
            
            if (e.keyCode == Sys.UI.Key["home"] && (!window.event.altKey) && (!window.event.shiftKey))
            {
                this._lastSelectionPos =  {start:0, end:0};
            }                
        }
        
        if(this._isNavigationKey(e.keyCode))
        {
            this._handleFreeFormTextInput(this.get_element(), {charCode:e.keyCode});
            if(e.preventDefault)e.preventDefault();
        }
   },

    // keypress is the _only_ event to get the proper characters entered by the user. Other events
    // get the key pressed, not the character. 
    _keyPressHandler : function(e) 
    {        
        // Fix for Mozilla. It seems swallow of enter key in keydown isn't working.
        if(e.charCode == Sys.UI.Key["enter"] && Sys.Browser.agent != Sys.Browser.InternetExplorer &&
                    Sys.Browser.agent != Sys.Browser.Safari)
        {
                  e.preventDefault();
                  e.stopPropagation();                  
                  var inputBox = this.get_element();
                  
                  // Ensure that the cursor ends up at the end of the revised/formatted text
                  inputBox.focus();
                  inputBox.value = inputBox.value;                  
                  return;
        }       
        
        // Don't do our processing on any ctrl-modified keys
        if (e.ctrlKey) return;
 
        // Don't process tab
        if(this._enabled == false || e.charCode === Sys.UI.Key["tab"]) return;
        
        var inputBox = this.get_element();
        var character = String.fromCharCode(e.charCode);       
        if(this.get_functionality() === TimeFunctionality.Simple)
        {
            if(this._isNavigationKey(e.charCode) == false)
            {
                this._handleFreeFormTextInput(inputBox, e);
            }
        }
        else
        {
            if(e.charCode === Sys.UI.Key["space"] && this._nullStrings.length > 0)
            {
                // Space bar. So cycle through nullStrings
                var i=0;
                for(; i<this._nullStrings.length; i++)
                {
                    if(this._nullStrings[i] === inputBox.value.trim())
                    {
                        i++;
                        break;
                    }
                }
                inputBox.value = this._nullStrings[i % this._nullStrings.length];
                this._mode = this.nullEntryMode;
            }     
            else if(this._mode === this.assistedFreeformMode && (e.charCode >= Sys.UI.Key["space"]))
            {
                if(!this._checkFreeFormAssistedEntry(inputBox, e.charCode) && this._isDigit(e.charCode))
                {
                    this._mode = this.freeformMode;
                }
            }
            
            if(this._mode === this.freeformMode)
            {

                // Determine whether an arithmetic switch is allowed at the current cursor pos. 
                if((character === NhsTimeResources.HoursUnit || 
                    character === NhsTimeResources.MinutesUnit) && 
                    this._arithmeticSwitchAllowed(inputBox))
                {
                    this._mode = this.arithmeticMode;
                    inputBox.value = inputBox.value.substring(0, this._getCursorPos()) + character;
                    this._selectRange(inputBox, inputBox.value.length, inputBox.value.length);                    
                }
                else if(this._checkFreeFormAssistedEntryTrigger(inputBox, e.charCode))
                {
                    // switch mode
                    this._mode = this.assistedFreeformMode;
                    
                    this._selectRange(inputBox, 1, inputBox.value.length);
                }
                else if(character === '+' || character === '-')
                {
                    // Switch into arithmetic mode
                    this._mode = this.arithmeticMode;
                    inputBox.value = character;
                }
                else
                {
                    if(!this._isNavigationKey(e.charCode))
                    {
                        this._handleFreeFormTextInput(inputBox, e);
                    }
                }
                
            }
            else if(this._mode === this.arithmeticMode)
            {
                // No 'h' or 'm' characters should be present
                if(inputBox.value.indexOf(NhsTimeResources.HoursUnit) === -1 && 
                        inputBox.value.indexOf(NhsTimeResources.MinutesUnit)  === -1) 
                {
                    if(this._isDigit(e.charCode))
                    {
                        // Only allow 2 digits max
                        if( ((inputBox.value.charAt(0) === '+' || inputBox.value.charAt(0) === '-') && 
                                inputBox.value.length <= 2) || inputBox.value.length <= 1)
                        {
                            inputBox.value = inputBox.value + character;
                        }
                    }
                    else if (character.toUpperCase() === NhsTimeResources.HoursUnit.toUpperCase() || 
                                character.toUpperCase() === NhsTimeResources.MinutesUnit.toUpperCase())
                    {                            
                        // Make sure there's at least 1 digit before we allow the 'h' or 'm'
                        if( ((inputBox.value.charAt(0) !== '+' && inputBox.value.charAt(0) !== '-') && 
                                     inputBox.value.length >= 1) || inputBox.value.length >= 2)
                        {
                           inputBox.value = inputBox.value + character;
                        }
                    }
                    else if((character === '+' || character === '-') && inputBox.value.length === 0)
                    {
                        // We get here if the user has entered arithmetic mode, then deleted all text
                        inputBox.value = character;
                    }
                }
                if(e.charCode === Sys.UI.Key["del"] || e.charCode === Sys.UI.Key["backspace"])
                {
                    // Allow delete and backspace if in arithmetic mode
                    return;
                }
            }
        }
        
        // We are in total control of what goes into the textbox. Suppress everything
        if (e.preventDefault) e.preventDefault();                               
    },               
    
    _raiseChange : function()
    {
        var inputBox = this.get_element();
        if(Sys.Browser.agent === Sys.Browser.Firefox)
        {
            var e = document.createEvent('HTMLEvents');
            e.initEvent('change', false, false);
            inputBox.dispatchEvent(e);
        }
        else
        {
            inputBox.fireEvent("onchange");           
        }
    },
    
    _arithmeticSwitchAllowed : function(inputBox) 
    {
        // User enters a 'h' or 'm' after the first digit, but before the ':'
        var cursorPos = this._getCursorPos();
        return (cursorPos > 0 && cursorPos <= 2);
    },
    
    _updateSelForUpDown : function(inputBox, curSelection) 
    {
         // As per design guide, if complete text is selected then lowest field should be
        // Incremented/Decremented.
        
        var newSelection = {start:curSelection.start, end:curSelection.end};
        
        if (curSelection.end - curSelection.start == inputBox.value.length)
        {
            var displayAMPM = this.get_displayAMPM();
            var amDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.AMDesignator.toLowerCase();
            var pmDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.PMDesignator.toLowerCase();
            
            if(displayAMPM && (inputBox.value.indexOf(amDesignator) > 0 || inputBox.value.indexOf(pmDesignator) > 0))
            {
                newSelection.end = curSelection.end - 4;
                newSelection.start = newSelection.end - 2;
            }            
            else
            {
                newSelection.start = newSelection.end - 2;
            }                        
        }
        
        return newSelection;
    },
    
    _getSelectionPos : function(inputBox) 
    {
        // Get selection start/end values
        if (inputBox.setSelectionRange)
        {
            // Gecko & Safari
            this._lastSelectionPos =  {start:inputBox.selectionStart, end:inputBox.selectionEnd};
        }
        else if(document.selection && document.selection.createRange)
        {
            // IE
				var range = document.selection.createRange();
				var selLength = range.text.length;
				range.moveStart ('character', -1 * inputBox.value.length);
				this._lastSelectionPos = { start:range.text.length - selLength, 
						end:range.text.length };
        }       
         
        return this._lastSelectionPos;
    },
    
    _getCursorPos : function() 
    {
        return this._getSelectionPos(this.get_element()).start;
    },
    
    _checkFreeFormAssistedEntry : function(inputBox, charCode) 
    {
        if(typeof(charCode) === "undefined")
        {
            return false;
        }
    
        var character = String.fromCharCode(charCode);

        var newEntry = inputBox.value.substring(0, this._getSelectionPos(inputBox).start) + character;
            
        // Check for free-form assisted entry (incl null times)
        var matches = new Array();        
        this._appendStartsWithMatches(this._nullStrings, newEntry, matches);
        
        if(matches.length > 0)
        {
            inputBox.value = matches[0];
            this._selectRange(inputBox, 
                            (matches.length == 1 ? inputBox.value.length : newEntry.length), 
                            inputBox.value.length);

            return true;
        }
        
        return false;
    },
    
    _appendStartsWithMatches : function(values, item, matches) 
    {
        var lowerItem = item.toLowerCase();
		for(var i = 0; i < values.length; i++)
		{
			if(values[i].toLowerCase().indexOf(lowerItem) == 0)
			{
			    matches.push(values[i]);
			}
		}
        return matches;
    },       
    
    _checkFreeFormAssistedEntryTrigger : function(inputBox, charCode) 
    {
        return this._checkFreeFormAssistedEntry(inputBox, charCode);
    },
    
    _selectRange : function(inputBox, start, end) 
    {
      this._lastSelectionPos = { "start" : start, "end" : end };

       if (inputBox.setSelectionRange)
       {
            // Gecko & Safari
            inputBox.setSelectionRange(start, end);
       }
       else if(document.selection && document.selection.createRange)
       {
            // IE
            var range = inputBox.createTextRange();
            range.collapse(true);
            range.moveStart('character', start);
            range.moveEnd('character', end - start);
            range.select();
        }
    },
    
    _isDigit : function(charCode) 
    {
        return charCode >= 48 && charCode <= 57;
    },
    
    _isNavigationKey : function(charCode) 
    {
        return (charCode === Sys.UI.Key["up"] || charCode === Sys.UI.Key["down"] || 
            charCode === Sys.UI.Key["left"] || charCode === Sys.UI.Key["right"]);
    },
    
    _handleFreeFormTextInput : function(inputBox, e) 
    {
        var displaySeconds = this.get_displaySeconds();
        var displayAMPM = this.get_displayAMPM();
        var display12Hour = this.get_display12Hour();
        var separatorListPattern = Sys.CultureInfo.CurrentCulture.dateTimeFormat.TimeSeparator + "\\.";
        var amDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.AMDesignator.toLowerCase();
        var pmDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.PMDesignator.toLowerCase();
        var minuteOrHourPattern = "[" + separatorListPattern + "](\\d{2})";
        var regex = new RegExp("^(\\d{1,2})" + minuteOrHourPattern + 
                            (displaySeconds ? minuteOrHourPattern : "") + 
                            (displayAMPM ? "(\\s\\((" + amDesignator + "|" + pmDesignator + ")\\))?" : "") + "$", "i");
       
        //Do not try String.fromCharCode on charCode unless it is a charCode in the underlying event data
        var character = String.fromCharCode(e.charCode).toLowerCase();
        
        var timeParts = regex.exec(inputBox.value);

        if(timeParts)
        {
            var selectionPos = this._getSelectionPos(inputBox);
            
            if(e.charCode == Sys.UI.Key["up"] || e.charCode == Sys.UI.Key["down"])
            {
                selectionPos = this._updateSelForUpDown(inputBox, selectionPos);
            }
            
            var hourIndex = 0, minuteIndex = 1, secondIndex = -1, amPmIndex = -1;
            var hourDigits = timeParts[hourIndex + 1].length;
            var startPositions = [0, hourDigits + 1];
            var lengths = [hourDigits, 2];
            var maxIndex = minuteIndex;
            var parsedTimeParts = [parseInt(timeParts[hourIndex + 1], 10), 
                                    parseInt(timeParts[minuteIndex + 1], 10)];
            var isAm = true;
           
            if(displaySeconds)
            {
                secondIndex = (++maxIndex);
                startPositions.push(startPositions[minuteIndex] + 3);
                lengths.push(2);
                parsedTimeParts.push(parseInt(timeParts[secondIndex + 1], 10));
            }
            
            if(displayAMPM && timeParts[maxIndex + 2] && timeParts[maxIndex + 2].length > 0)
            {
                startPositions.push(startPositions[maxIndex] + 4);
                lengths.push(2);
                amPmIndex = (++maxIndex);
                isAm = (timeParts[amPmIndex + 2].toLowerCase() === amDesignator);
            }
                       
            // are hours, minutes or seconds selected?
            var selIndex = hourIndex;
            for(var i = startPositions.length - 2; i >= 0 ; i--)
            {
                if(selectionPos.start >= (startPositions[i] + lengths[i]))
                {
                    selIndex = i + 1;
                    break;
                }
            }
            
            var selOffset = Math.max(0, selectionPos.start - startPositions[selIndex]);
           
            switch(e.charCode)
            {
                case(Sys.UI.Key["right"]) :
                case(Sys.UI.Key["left"]) :
                    var increment = (e.charCode === Sys.UI.Key["right"] ? 1 : -1);
                    // roll round
                    selIndex = (maxIndex + 1 + selIndex + increment) % (maxIndex + 1);
                    selectionPos.start = startPositions[selIndex];
                    selectionPos.end = startPositions[selIndex] + lengths[selIndex];
                    break;
                    
                 case(Sys.UI.Key["up"]) :
                 case(Sys.UI.Key["down"]) :
                    // If Up or Down keys pressed                        
                    if(selIndex === amPmIndex)
                    {
                        if(display12Hour && parsedTimeParts[hourIndex] <= 12)
                        {
                            isAm = !isAm;
                        }
                    }
                    else
                    {
                        // roll round
                        var increment = (e.charCode === Sys.UI.Key["up"] ? 1 : -1);
                        var minValue = (selIndex === hourIndex && display12Hour ? 1 : 0);
                        var maxValue = (selIndex === hourIndex ? (display12Hour ? 12 : 23) : 59);
                        parsedTimeParts[selIndex] += increment;
                        
                        if(parsedTimeParts[selIndex] > maxValue)
                        {
                            parsedTimeParts[selIndex] = minValue;
                            if (!display12Hour)
                            {
                                isAm = true;
                            }
                        }
                        else if(parsedTimeParts[selIndex] < minValue)
                        {
                             parsedTimeParts[selIndex] = maxValue;
                            if (!display12Hour)
                            {
                                isAm = false;
                            }
                        }
                        
                        if(display12Hour && selIndex === hourIndex)
                        {
                            if((parsedTimeParts[selIndex] === 12 && increment === 1) ||
                                (parsedTimeParts[selIndex] === 11 && increment === -1))
                            {
                                isAm = !isAm;
                            }
                        }                        
                    }
                    selectionPos.start = startPositions[selIndex];
                    selectionPos.end = startPositions[selIndex] + lengths[selIndex];
                    break;
                    
                default:
                    if(selIndex === amPmIndex)
                    {
                        if(character === amDesignator.charAt(selOffset) || character === pmDesignator.charAt(selOffset))
                        {
                            if(selOffset === 0)
                            {
                                isAm = (character === amDesignator.charAt(0));
                            }
                            selectionPos.start = startPositions[selIndex] + selOffset + 1;
                        }
                    }
                    else
                    {
                        if(this._isDigit(e.charCode))
                        {
                            var value = parseInt(character, 10);
                            if(selOffset === 0)
                            {
                                 parsedTimeParts[selIndex] = value * 10 + 
                                                parsedTimeParts[selIndex] % 10;
                            }
                            else
                            {
                                 parsedTimeParts[selIndex] = value + 
                                                Math.floor(parsedTimeParts[selIndex] / 10) * 10;
                            }
                            selectionPos.start = startPositions[selIndex] + selOffset + 1;
                        }
                        else if(separatorListPattern.indexOf(character) >= 0)
                        {
                            // if user was editing most significant digit assume this
                            // is actually least significant digit
                            if(selOffset === 1)
                            {
                                parsedTimeParts[selIndex] = Math.floor(parsedTimeParts[selIndex] / 10);
                            }
                            
                            if(selIndex < maxIndex)
                            {
                                if(selOffset >= 1)
                                {
                                    selectionPos.start = startPositions[selIndex + 1];
                                }
                                else
                                {
                                    selectionPos.start = startPositions[selIndex];
                                }
                            }
                        }
                    }
                    
                    selectionPos.end = selectionPos.start + 1;
                    break;
            }
                                   
            var formattedTimeParts = new Array(parsedTimeParts.length);
            for (var i = 0; i < parsedTimeParts.length; i++)
            {
                formattedTimeParts[i] = parsedTimeParts[i].format("d2");
            }
            
            inputBox.value = formattedTimeParts.join(Sys.CultureInfo.CurrentCulture.dateTimeFormat.TimeSeparator);
            
            if(displayAMPM === true)
            {
                if (display12Hour || (!display12Hour && parsedTimeParts[hourIndex] < 12))
                {
                    inputBox.value += " (" + (isAm ? amDesignator : pmDesignator) + ")";
                }
            }

            this._selectRange(inputBox, selectionPos.start, selectionPos.end);
        }
        else if(this._isDigit(e.charCode))
        {
            // No valid time in the inputbox, so assume we've just changed modes
            var formattedMinutesOrSeconds = Sys.CultureInfo.CurrentCulture.dateTimeFormat.TimeSeparator + "00";
            inputBox.value = character + "0" + formattedMinutesOrSeconds + 
                                (displaySeconds ? formattedMinutesOrSeconds : "") + 
                                (displayAMPM ? " (" + amDesignator + ")" : "");
            this._selectRange(inputBox, 1, 2);           
        }       
    },
    
    _isValidTime : function()
    {    
        var validTime;
        validTime = true;
        var nhsCuiTimeInputBoxValidators = this._getAttachedNhsCuiTimeInputBoxValidator();
        
        if (nhsCuiTimeInputBoxValidators.length != 0){           
            for(i=0; i< nhsCuiTimeInputBoxValidators.length; i++)
            {
               if (validTime == true)     
                    validTime = nhsCuiTimeInputBoxValidators[i].isvalid;
            }
       }
       
       return validTime;                   
    },
    
    _checkArithmeticInput : function(e)
    {      
        var inputBox = this.get_element();
        var selPos = this._getSelectionPos(inputBox);
        //NhsTime.isAddInstruction(text)
        if (this.get_functionality() === TimeFunctionality.Complex && selPos.start === 0 && selPos.end === inputBox.value.length && this._isValidTime())
        {
            // If it is a digit or a paste operation for the whole content...
            if (this._isDigit(e.charCode))
            {               
                this._mode = this.arithmeticMode;
                return true;               
            }
        }
        
        return false;
    },          
    
    _tidyNonDelimitedInput : function(sourceText)
    {
        var separator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.TimeSeparator;
        
        if (sourceText.length === 3)
        {
            sourceText = sourceText.substr(0, 1) + separator + sourceText.substr(1, 2);
        }
        else if (sourceText.length === 4)
        {
            sourceText = sourceText.substr(0, 2) + separator + sourceText.substr(2, 2); 
        }
        
        return sourceText;
    }
};

NhsCui.Toolkit.Web.TimeInputBox.registerClass('NhsCui.Toolkit.Web.TimeInputBox', AjaxControlToolkit.BehaviorBase);
