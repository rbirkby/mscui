//-----------------------------------------------------------------------
// <copyright file="PickerBehavior.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//-----------------------------------------------------------------------

//v-plong - Main change was to change events listening to the textbox to exit immediately
//if this._enabled = false
Type.registerNamespace('NhsCui.Toolkit.Web');

NhsCui.Toolkit.Web.PickerDataType = function() { }
NhsCui.Toolkit.Web.PickerDataType.prototype = {
    DateTime : 0,
    TimeSpan : 1,
    NhsDate  : 2
}
NhsCui.Toolkit.Web.PickerDataType.registerEnum("NhsCui.Toolkit.Web.PickerDataType");

NhsCui.Toolkit.Web.SpecifierContentType = function() { }
NhsCui.Toolkit.Web.SpecifierContentType.prototype = {
    Number : 0,
    List : 1
}
NhsCui.Toolkit.Web.SpecifierContentType.registerEnum("NhsCui.Toolkit.Web.SpecifierContentType");

NhsCui.Toolkit.Web.SpecifierUnit = function() { }
NhsCui.Toolkit.Web.SpecifierUnit.prototype = {
    Day : 0,
    DayOfWeek : 1,
    Month : 2,
    Year : 3,
    Hour : 4,
    Minute : 5,
    Second : 6,
    Millisecond : 7,
    AMPMDesignator : 8
}
NhsCui.Toolkit.Web.SpecifierUnit.registerEnum("NhsCui.Toolkit.Web.SpecifierUnit");

NhsCui.Toolkit.Web.PickerBehavior = function(element) {
    NhsCui.Toolkit.Web.PickerBehavior.initializeBase(this, [element]);
    this._hasFocus = false;
    this._globalizationService = new GlobalizationService();
    this._value = null;
    this._format = "d";
    this._enabled = true;
    this._get_holdOwnValueState = true;
    this._acceptKeyDigitInput = true;
    this._dataType = NhsCui.Toolkit.Web.PickerDataType.DateTime;
    this._specifiers = [];
    this._activeEditRegion = null;
    this._editRegions = null;
    this._pendingChars = new Sys.StringBuilder();
    this._valueChanging = false;
    this._defaultValue = null;
    this._nullable = true;
    this._keypressed = false;
    this._element_onfocus$delegate = Function.createDelegate(this, this._element_onfocus);
    this._element_onblur$delegate = Function.createDelegate(this, this._element_onblur);
    this._element_onmousedown$delegate = Function.createDelegate(this, this._element_onmousedown);
    this._element_onmouseup$delegate = Function.createDelegate(this, this._element_onmouseup);
    this._element_onkeydown$delegate = Function.createDelegate(this, this._element_onkeydown);
    this._element_onkeyup$delegate = Function.createDelegate(this, this._element_onkeyup);
    this._element_onkeypress$delegate = Function.createDelegate(this, this._element_onkeypress);
    this._element_onselect$delegate = Function.createDelegate(this, this._element_onselect);
    this._element_ondragstart$delegate = Function.createDelegate(this, this._element_ondragstart);
    this._element_onchange$delegate = Function.createDelegate(this, this._element_onchange);
    this._mousescrollhandler$delegate = Function.createDelegate(this, this._mousescrollhandler);
}
NhsCui.Toolkit.Web.PickerBehavior.prototype = {
    
    get_format : function() { 
        return this._format; 
    },
    set_format : function(value) { 
        if (this._format != value) {
            this._format = value; 
            this._formatSpecifiers = null; 
            //reset this._editRegions
            this._editRegions = null;
            this.raisePropertyChanged('format');
        }
    },
    get_enabled : function() { 
        return this._enabled; 
    },
    set_enabled : function(value) { 
        if (this._enabled != value) {
            this._enabled = value; 
            this.raisePropertyChanged('enabled');
        }
    },
    get_acceptKeyDigitInput : function() { 
        return this._acceptKeyDigitInput; 
    },
    set_acceptKeyDigitInput : function(value) { 
        if (this._acceptKeyDigitInput != value) {
            this._acceptKeyDigitInput = value; 
            this.raisePropertyChanged('acceptKeyDigitInput');
        }
    },
    get_holdOwnValueState : function() { 
        return this._get_holdOwnValueState; 
    },
    set_holdOwnValueState : function(value) { 
        if (this._get_holdOwnValueState != value) {
            this._get_holdOwnValueState = value; 
            this.raisePropertyChanged('holdOwnValueState');
        }
    },
    get_dataType : function() { 
        return this._dataType; 
    },
    set_dataType : function(value) { 
        if (this._dataType != value) {
            this._dataType = value; 
            this.raisePropertyChanged('dataType');
        }
    },
    get_defaultValue : function() { 
        return this._defaultValue; 
    },
    set_defaultValue : function(value) { 
        if (this._defaultValue != value) {
            this._defaultValue = value; 
            this.raisePropertyChanged('defaultValue');
        }
    },
    get_value : function()
    { 
       if (this.get_holdOwnValueState() == false)
       {
            //We are not holding our own state so take it straight off of the TextBox
            var elt = this.get_element();
            if (elt.value != null)
            {
                //console.log("Picker get_value 2");
                return this._parseTextValue(elt.value);
            }
            else
            {
                return null;
            }
       }
       else
       {
            if (this._value == null) {
                var elt = this.get_element();
                if (elt.value)
                {
                    this._value = this._parseTextValue(elt.value);
                }
            }
            return this._value;
       }
    },
    set_value : function(value, force) {
        if (typeof force == "undefined") force = false;
        
        //console.log("Picker set_value");
        
        var elt = this.get_element();
        if ((force || this._value != value) && !elt.disabled && !elt.readOnly) {
            this._value = value;
            this._valueChanging = true;
            
            var text = '';
            if (value) {
                var builder = new Sys.StringBuilder();
                var regions = this._getRegions();
                for (var i = 0; i < regions.length; i++) {
                    var region = regions[i];
                    if(region.type == 0) {
                        builder.append(region.text);
                    } else {
                        var specifier = region.spec;
                        var length = region.length;
                        var part = specifier.getTextPart(this._dataType, value);
                        builder.append(part);
                    }
                }
                text = builder.toString();
            }
            
            if(text != elt.value) {
                elt.value = text;
                this._fireChanged();
            }        
            
            this.raisePropertyChanged("value");
            this._valueChanging = false;
        }
    },
    get_nullable : function() { 
        return this._nullable; 
    },
    set_nullable : function(value) { 
        if (this._nullable != value) {
            this._nullable = value; 
            this.raisePropertyChanged("nullable");
        }
    },
    
    initialize : function() {
        var wasInitialized = this.get_isInitialized();
        NhsCui.Toolkit.Web.PickerBehavior.callBaseMethod(this, "initialize");
        
        var elt = this.get_element();
        $addHandler(elt, "focus", this._element_onfocus$delegate);
        $addHandler(elt, "blur", this._element_onblur$delegate);
        $addHandler(elt, "mousedown", this._element_onmousedown$delegate);
        $addHandler(elt, "mouseup", this._element_onmouseup$delegate);
        $addHandler(elt, "keydown", this._element_onkeydown$delegate);
        $addHandler(elt, "keyup", this._element_onkeyup$delegate);
        $addHandler(elt, "keypress", this._element_onkeypress$delegate);
        $addHandler(elt, "select", this._element_onselect$delegate);
        $addHandler(elt, "dragstart", this._element_ondragstart$delegate);
        $addHandler(elt, "change", this._element_onchange$delegate);
        $addHandler(document, 'DOMMouseScroll',this._mousescrollhandler$delegate);
        $addHandler(document, 'mousewheel',this._mousescrollhandler$delegate);
        
        // Get the value from the control
        var value = this.get_value();
        if (value == null && !this.get_nullable()) {
            value = this._getDefaultValue();
        }
        if (value) {
            this.set_value(value, true);
        } 
    },
    dispose : function() {
        var elt = this.get_element();
        if (elt == null)
            return;
        $removeHandler(elt, "focus", this._element_onfocus$delegate);
        $removeHandler(elt, "blur", this._element_onblur$delegate);
        $removeHandler(elt, "mousedown", this._element_onmousedown$delegate);
        $removeHandler(elt, "mouseup", this._element_onmouseup$delegate);
        $removeHandler(elt, "keydown", this._element_onkeydown$delegate);
        $removeHandler(elt, "keyup", this._element_onkeyup$delegate);
        $removeHandler(elt, "keypress", this._element_onkeypress$delegate);
        $removeHandler(elt, "select", this._element_onselect$delegate);
        $removeHandler(elt, "dragstart", this._element_ondragstart$delegate);
        $removeHandler(elt, "change", this._element_onchange$delegate);
        $removeHandler(document, 'DOMMouseScroll',this._mousescrollhandler$delegate);
        $removeHandler(document, 'mousewheel',this._mousescrollhandler$delegate);
        NhsCui.Toolkit.Web.PickerBehavior.callBaseMethod(this, "dispose");
    },
    
    _fireChanged : function() {
        
        //console.log("In Picker _fireChanged");
        var elt = this.get_element();
        if (document.createEventObject) {
            elt.fireEvent("onchange");
        } else if (document.createEvent) {
            var e = document.createEvent("HTMLEvents");
            e.initEvent("change", true, true);
            elt.dispatchEvent(e);
        }
    },
    _getFormatString : function() {
        var format = this._format;
        if (!format) {
            format = "F";
        }
        if (format.length == 1) {
            switch (this._dataType) {
                 case NhsCui.Toolkit.Web.PickerDataType.NhsTimeSpan: {
                    var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;
                    switch (format) {
                        case "d": format = dtf.ShortDatePattern; break;
                        case "D": format = dtf.LongDatePattern; break;
                        case "t": format = dtf.ShortTimePattern; break;
                        case "T": format = dtf.LongTimePattern; break;
                        case "F": format = dtf.FullDateTimePattern; break;
                        case "M": case "m": format = dtf.MonthDayPattern; break;
                        case "s": format = dtf.SortableDateTimePattern; break;
                        case "Y": case "y": format = dtf.YearMonthPattern; break;
                        default: throw Error.createError("'" + format + "' is not a valid date format");
                    }
                    break;
                }
                case NhsCui.Toolkit.Web.PickerDataType.DateTime: {
                    var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;
                    switch (format) {
                        case "d": format = dtf.ShortDatePattern; break;
                        case "D": format = dtf.LongDatePattern; break;
                        case "t": format = dtf.ShortTimePattern; break;
                        case "T": format = dtf.LongTimePattern; break;
                        case "F": format = dtf.FullDateTimePattern; break;
                        case "M": case "m": format = dtf.MonthDayPattern; break;
                        case "s": format = dtf.SortableDateTimePattern; break;
                        case "Y": case "y": format = dtf.YearMonthPattern; break;
                        default: throw Error.createError("'" + format + "' is not a valid date format");
                    }
                    break;
                }
                case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: {
                    switch (format) {
                        case "t": format = TimeSpan.ShortTimeSpanPattern; break;
                        case "T": format = TimeSpan.LongTimeSpanPattern; break;
                        case "F": format = TimeSpan.FullTimeSpanPattern; break;
                        default: throw Error.createError("'" + format + "' is not a valid TimeSpan format");
                    }
                }
            }
        }        
        return format;
    },
    _getSpecifiers : function() {
        switch(this._dataType) {
            case NhsCui.Toolkit.Web.PickerDataType.DateTime: return NhsCui.Toolkit.Web.PickerUtility.getDateTimeSpecifiers();
            case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: return NhsCui.Toolkit.Web.PickerUtility.getTimeSpanSpecifiers();
        }
        return [];
    },
    _getRegions : function() {
        if (this._editRegions == null) {
            var specifiers = this._getSpecifiers();
            var format = this._getFormatString();
            var re = NhsCui.Toolkit.Web.PickerUtility.createRegex(specifiers);
            var regions = [];
            var startIndex = 0;
            for(;;) {
                var index = re.lastIndex;
                var ar = re.exec(format);
                var pre = format.slice(index, ar ? ar.index : format.length);
                if(pre) {                    
                    regions.push({
                        type : 0,
                        text : pre,
                        startIndex : startIndex,
                        length : pre.length,
                        readOnly: true
                    });
                    startIndex += pre.length;
                }
                if(ar) {
                    for(var i = 0; i < specifiers.length; i++) {
                        if(specifiers[i].get_specifier() == ar[0]) {
                            regions.push({
                                type : 1,
                                spec : specifiers[i],
                                startIndex : startIndex,
                                length : specifiers[i].get_length(),
                                readOnly : specifiers[i].get_readOnly()
                            });
                            startIndex += specifiers[i].get_length();
                            break;
                        }
                    }
                } else break;
            }
            this._editRegions = regions;
        }
        return this._editRegions;
    },
    _updateRegionPositions : function(regions) {
        for(var i = 0; i < regions.length; i++) {
            var region = regions[i];
            var range = this._createRange(region);
            region.left = range.offsetLeft; 
            region.right = range.offsetLeft + range.boundingWidth; 
            region.top = range.offsetTop; 
            region.bottom = range.offsetTop + range.boundingHeight; 
        }
    },
    _showFocus : function() {
        if(this._enabled == false){
            return;
        }
        if(this._activeEditRegion != null) {
            var region = this._getRegions()[this._activeEditRegion];
            if (!region.readOnly) {
                var value = this.get_value();
                if (value) {
                    this.set_value(value);
                }
                this._selectRegion(region);
            }
        }
    },
    _selectRegion : function(region) {        
        var elt = this.get_element();
        if(elt.createTextRange) {
            var range = this._createRange(region);
            range.select();
        } else if(elt.setSelectionRange) {
            elt.setSelectionRange(region.startIndex, region.startIndex + region.length);
        }
    },
    _endEdit : function() {
        this._pendingChars.clear();        
    },
    _cancelEdit : function() {
        this._pendingChars.clear();        
    },
    _createRange : function(region) {
        var elt = this.get_element();
        var range = elt.createTextRange();
        range.moveStart("character", region.startIndex);
        range.moveEnd("character", - (elt.value.length - region.startIndex - region.length));
        return range;
    },
    _incrementValue : function(region, increment) {
        var value = this.get_value();
        if (value == null) {
            value = this._getDefaultValue();
        } else {
            value = region.spec.incrementPart(value, this._dataType, increment);
        }
        this.set_value(value);
        this._showFocus();
    },
    _getDefaultValue : function() {
        if (this._defaultValue) {
            return this._parseTextValue(this._defaultValue);
        }
        switch(this._dataType) {
            case NhsCui.Toolkit.Web.PickerDataType.DateTime: return new Date().getDateOnly();
            case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: return TimeSpan.fromTicks(0);
        }
        return null;
    },
    _parseTextValue : function(text) {
        //console.log("_parseTextValue " + text);
        var value = null;
        switch(this._dataType) {
            case NhsCui.Toolkit.Web.PickerDataType.DateTime:
                if(text) {
                    value = Date.parseInvariant(text, 'MMM d, yyyy','MMM d,y','d-MMM-yyyy','d-MMM-yy','d-MMMM-yyyy','d-MMMM-yy','MMM-yyyy','MMM-yy','MMMM-yyyy','MMMM-yy', 'MMM yyyy', 'MMMM yyyy','MMM/yyyy', 'MMMM/yyyy','d MMM yyyy', 'd MMMM yyyy', 'ddd dd-MMM-yyyy', 'MM/yyyy', 'MM-yyyy','MMM yy', 'MMMM yy','MMM/yy', 'MMMM/yy','d MMM yy', 'd MMMM yy', 'MM/yy', 'MM-yy', this._globalizationService.shortDatePattern, this._globalizationService.shortDatePatternWithDayOfWeek);
                } 
                if(isNaN(value)) {
                    value = null;
                    if(text.indexOf(":") != -1) {
                        value = new Date("0/0/0 " + text);
                    } 
                }
                break;
            case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: 
                if (text) {
                    value = TimeSpan.parse(text) ; 
                } else {
                    value = null;
                }
                break;
        }
        return value;
    },
    _element_onfocus : function(e) {
        this._hasFocus = true;
        if(this._activeEditRegion == null) {
            var regions = this._getRegions();
            for(var i = 0; i < regions.length; i++) {
                if(!regions[i].readOnly) {
                    this._activeEditRegion = i;
                    break;
                }
            }
        }
        //this._showFocus();
    },
    _element_onblur : function(e) {
        this._hasFocus = false;
        this._activeEditRegion = null;
        this._endEdit();
    },
    _element_onmousedown : function(e) {
        if(this._enabled == false){
            return;
        }
        if (Sys.Browser.agent === Sys.Browser.InternetExplorer) {
            var regions = this._getRegions();
            this._updateRegionPositions(regions);
            for(var i = 0; i < regions.length; i++) {
                var region = regions[i];
                if (e.offsetX >= region.left && e.offsetX <= region.right && e.offsetY >= region.top && e.offsetY <= region.bottom && !region.readOnly) {
                    this._activeEditRegion = i;
                    return;
                }
            }
            this._showFocus();
        } else {
            if(this.setSelectionRange)
                this.setSelectionRange(0, 0);
        }
    },
    _element_onmouseup : function(e) {
        if(this._enabled == false){
            return;
        }
        
        var selectedText;
        
        //If all of the text is selected leave the selection alone
        if (document.getSelection){
            selectedText = document.getSelection();
        }
        else if (document.selection){
            selectedText = document.selection.createRange().text;
        }
        
        if (selectedText == this.get_element().value){
            return;
        }
        
        var regions = this._getRegions();
        if (Sys.Browser.agent === Sys.Browser.InternetExplorer) {
            this._updateRegionPositions(regions);
            this._showFocus();
        } else {
            
            var elt = this.get_element();
            var selectionIndex = elt.selectionStart;
            for(var i = 0; i < regions.length; i++) {
                var region = regions[i];
                if(region.startIndex <= selectionIndex && selectionIndex <= (region.startIndex + region.length) && !region.isReadOnly) {
                    this._activeEditRegion = i;
                }
            }
            this._showFocus();
        }
    },
    
    _getActiveRegion : function() {

        var regions = this._getRegions();
        for(var i = 0; i < regions.length; i++)
        {
            if(!regions[i].readOnly)
            {
                return i;
                break;
            }
        }
    },
    
    _element_onkeydown : function(e) {
        if(this._enabled == false || e.altKey){
            return;
        }
        
        if (AjaxControlToolkit.CommonToolkitScripts.isKeyNavigation(e.keyCode) || 
            e.keyCode == Sys.UI.Key["backspace"] || 
            e.keyCode == Sys.UI.Key.del ||
            e.keyCode == Sys.UI.Key["windowsDelete"])
        {
            
            this._pendingChars.clear();
            var regions = this._getRegions();
            if(this._activeEditRegion == null)
            {
                this._activeEditRegion = this._getActiveRegion();
            }
            
            //alert("KeyCode " + e.keyCode);
            switch (e.keyCode) {
                case Sys.UI.Key["windowsDelete"]:
                case Sys.UI.Key.del:
                case Sys.UI.Key["backspace"]:
                {
                    if (this.get_nullable())
                    {
                        this._activeEditRegion = null;
                        this.set_value(null);
                        this._showFocus();
                    }
                    e.preventDefault();
                    e.stopPropagation();
                    break;
                }
                case Sys.UI.Key["left"]:
                {
                    if (this.get_value() == null)
                    {
                        return;
                    }
                    var originalRegion = this._activeEditRegion;
                    do {
                        this._activeEditRegion = (this._activeEditRegion - 1) % regions.length;
                        if (this._activeEditRegion < 0) this._activeEditRegion = regions.length - 1;
                    } while (this._activeEditRegion != originalRegion && regions[this._activeEditRegion].readOnly)
                    this._showFocus();
                    e.preventDefault();
                    e.stopPropagation();
                    break;
                }
                case Sys.UI.Key["right"]: {
                    if (this.get_value() == null) {
                        return;
                    }
                    var originalRegion = this._activeEditRegion;
                    do {
                        this._activeEditRegion = (this._activeEditRegion + 1) % regions.length;
                    } while (this._activeEditRegion != originalRegion && regions[this._activeEditRegion].readOnly)
                    this._showFocus();
                    e.preventDefault();
                    e.stopPropagation();
                    break;
                }
                case Sys.UI.Key["up"]:
                    this._incrementValue(regions[this._activeEditRegion], 1);
                    e.preventDefault();
                    e.stopPropagation();
                    break;
                case Sys.UI.Key["down"]:
                    this._incrementValue(regions[this._activeEditRegion], -1);
                    e.preventDefault();
                    e.stopPropagation();
                    break;
            }
            
        }
    },
    _mousescrollhandler : function(e) {
            
        if(this._hasFocus==false || this._enabled == false) return;
        
        var scrollDelta=0;
        if(e.rawEvent.wheelDelta) {
            scrollDelta = e.rawEvent.wheelDelta/120;
        } else if(e.rawEvent.detail) {
            scrollDelta = -e.rawEvent.detail/3;
        }
        
        var regions = this._getRegions();
        if(this._activeEditRegion == null)
        {
            this._activeEditRegion = this._getActiveRegion();
        }
        
        if (scrollDelta < 0)
        {
            this._incrementValue(regions[this._activeEditRegion], -1);
        }
        else
        {
            this._incrementValue(regions[this._activeEditRegion], 1);
        }

        e.preventDefault();        
    },
    _willHandleKey : function(charCode, keyCode)
    {
        var code=keyCode? charCode : charCode;
        
        //Check code against the set of keys this Extender responds to
        
        if (code == Sys.UI.Key.enter||
            code == Sys.UI.Key.esc ||
            code == Sys.UI.Key.tab ||
            code == Sys.UI.Key["return"]||
            code == Sys.UI.Key["windowsDelete"] ||
            code == Sys.UI.Key.del ||
            code == Sys.UI.Key["backspace"] ||
            code == Sys.UI.Key["up"] ||
            code == Sys.UI.Key["down"] ||
            code == Sys.UI.Key["backspace"] ||
            code == Sys.UI.Key["left"] ||
            code == Sys.UI.Key["right"])
        {
            return true
        } 
        else
        {
            return false;
        }
    },
    _element_onkeyup : function(e) {
        if(this._enabled == false){
            return;
        }
        
        var elt = this.get_element();
        if (!elt.createTextRange) {
            this._showFocus();
        }
    },
    _element_onkeypress : function(e) {
        if(this._enabled == false){
            return;
        }
        
        if(this._activeEditRegion == null) {
            this._activeEditRegion = this._getActiveRegion();
        }

        if (e.charCode == Sys.UI.Key["return"])
        {
            this._endEdit();
            this._showFocus();
            e.preventDefault();
        } 
        else if (e.charCode == Sys.UI.Key["esc"])
        {
            this._cancelEdit();
            this._showFocus();
            return;
        } 
        else if (e.charCode == Sys.UI.Key["tab"])
        {
            return;
        } 
        else if (AjaxControlToolkit.CommonToolkitScripts.isKeyDigit(e.charCode) && this.get_acceptKeyDigitInput() == true)
        {
            var value = this.get_value();
            if (value == null) {
                value = this._getDefaultValue();
            }
            var regions = this._getRegions();
            var activeRegion = regions[this._activeEditRegion];
            var c = String.fromCharCode(e.charCode);
            var length = activeRegion.spec.get_length();
            if(this._pendingChars.toString().length == length && length > 1) {
                var part = this._pendingChars.toString().substr(1, length - 1);
                this._pendingChars.clear();
                this._pendingChars.append(part);
            }
            this._pendingChars.append(c);
            var result = activeRegion.spec.updatePart(value, this._dataType, this._pendingChars.toString());
            if (result.wasUpdated) {
                this.set_value(result.value);
                this._showFocus();
            } else {
                this._pendingChars.clear();
                this._pendingChars.append(c);
                var result = activeRegion.spec.updatePart(value, this._dataType, this._pendingChars.toString());
                if (result.wasUpdated) {
                    this.set_value(result.value);
                    this._showFocus();
                } else {
                    this._pendingChars.clear();
                }
            }
            e.preventDefault();
        } 
        else
        {
//            var value = this.get_value();
//            if (value == null) {
//                value = this._getDefaultValue();
//            }
//            var regions = this._getRegions();
//            var activeRegion = regions[this._activeEditRegion];
//            var c = String.fromCharCode(e.charCode);            
            //var result = activeRegion.spec.updatePart(value, this._dataType, c);
            //if (result.wasUpdated) {
                //this.set_value(result.value);
                //this._showFocus();
            //}
            //this._pendingChars.clear();
            //e.preventDefault();
        }
    },
    _element_onselect : function(e) {
//        if(this._enabled == false){
//            return;
//        }
//        e.preventDefault();
//        e.stopPropagation();
    },
    _element_ondragstart : function(e) {
        e.preventDefault();
        e.stopPropagation();
    },
    _element_onchange : function(e) {
        //console.log("Picker _element_onchange");
        
        if (!this._valueChanging && this._enabled == true) {
            //console.log("Picker _element_onchange 1");
            var elt = this.get_element();
            if (elt.value)
            {
                //console.log("Picker _element_onchange 2");
                var value = this._parseTextValue(elt.value);
                this.set_value(value);
                //console.log("Picker _element_onchange 3");
            } 
            else 
            {
                //console.log("Picker _element_onchange 4");
                this._value = null;
            }
        }
    }
}
NhsCui.Toolkit.Web.PickerBehavior.registerClass("NhsCui.Toolkit.Web.PickerBehavior", AjaxControlToolkit.BehaviorBase);

NhsCui.Toolkit.Web.PickerFormatSpecifier = function(specifier, unit, type, list, length, readOnly) {
    this._specifier = specifier || null;
    this._unit = typeof(unit) == 'undefined' ? NhsCui.Toolkit.Web.SpecifierUnit.Number : unit;
    this._type = typeof(type) == 'undefined' ? NhsCui.Toolkit.Web.SpecifierContentType.Number : type;
    this._list = typeof(list) == 'undefined' ? [] : list;
    this._length = typeof(length) == 'undefined' ? null : length;
    this._readOnly = typeof(readOnly) == 'undefined' ? false : readOnly;
}
NhsCui.Toolkit.Web.PickerFormatSpecifier.prototype = {
    get_specifier : function() { return this._specifier; },
    set_specifier : function(value) { this._specifier = value; },
    get_type : function() { return this._type; },
    set_type : function(value) { this._type = value; },
    get_list : function() { return this._list; },
    set_list : function(value) { this._list = value; },
    get_readOnly : function() { return this._readOnly; },
    set_readOnly : function(value) { this._readOnly = value; },
    get_length : function() {
        if (this._length == null) {
            this._length = this._specifier.length;
            if(this._list != null && this._list.length > 0) {
                for(var i = 0; i < this._list.length; i++) {
                    if(this._list[i].length > this._length) {
                        this._length = this._list[i].length;
                    }
                }
            }
        }
        return this._length;
    },
    set_length : function(value) { this._length = value; },
    
    getTextPart : function(dataType, value) {
        var part = this.getPart(dataType, value);
        switch (this._type) {
            case NhsCui.Toolkit.Web.SpecifierContentType.Number: 
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Year: return AjaxControlToolkit.CommonToolkitScripts.padLeft(part, this.get_length() <= 2 ? 2 : 4, '0', true);
                    case NhsCui.Toolkit.Web.SpecifierUnit.Month: return AjaxControlToolkit.CommonToolkitScripts.padLeft(part, this.get_length(), this._specifier.length == this.get_length() ? '0' : ' ');
                    case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: return AjaxControlToolkit.CommonToolkitScripts.padRight(part, this.get_length(), '0', true);
                    default: return AjaxControlToolkit.CommonToolkitScripts.padLeft(part, this.get_length(), this._specifier.length == this.get_length() ? '0' : ' ', true);
                }
            case NhsCui.Toolkit.Web.SpecifierContentType.List: 
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Month: return AjaxControlToolkit.CommonToolkitScripts.padRight(this._list[part - 1], this.get_length(), ' ', true);
                    default: return AjaxControlToolkit.CommonToolkitScripts.padRight(this._list[part], this.get_length(), ' ', true);
                }                
        }
    },
    getPart : function(dataType, value) {
        switch (dataType) {
            case NhsCui.Toolkit.Web.PickerDataType.DateTime:                
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Year: return value.getFullYear();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Month: return value.getMonth() + 1;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Day: return value.getDate();
                    case NhsCui.Toolkit.Web.SpecifierUnit.DayOfWeek: return value.getDay();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Hour:
                        if(this._specifier == "H" || this._specifier == "HH") {
                            return value.getHours();
                        } else {
                            var hours = value.getHours() % 12;
                            if (hours == 0) hours = 12;
                            return hours;
                        }                        
                    case NhsCui.Toolkit.Web.SpecifierUnit.Minute: return value.getMinutes();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Second: return value.getSeconds();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: return value.getMilliseconds();
                    case NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator: return Math.floor(value.getHours() / 12);
                }
                break;
            case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: 
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Day: return value.getDays();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Hour: return value.getHours();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Minute: return value.getMinutes();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Second: return value.getSeconds();
                    case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: return value.getMilliseconds();
                }
                break;
        }
        return null;
    },
    incrementPart : function(value, dataType, increment) {
        if (this.get_readOnly()) return value;
        switch (dataType) {
            case NhsCui.Toolkit.Web.PickerDataType.DateTime: {
                var y = value.getFullYear();
                var M = value.getMonth();
                var d = value.getDate();
                var day = value.getDay();
                var h = value.getHours();
                var m = value.getMinutes();
                var s = value.getSeconds();
                var n = value.getMilliseconds();
                
                var am = (h >= 0 && h < 12);
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Year: y = (y + increment); break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Month: M = (M + increment) % 12; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.DayOfWeek:
                    case NhsCui.Toolkit.Web.SpecifierUnit.Day: d = (d + increment) % (new Date(y, M + 1, 0).getDate()); break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Hour: h = (h + increment) % 24; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Minute: m = (m + increment) % 60; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Second: s = (s + increment) % 60; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: n = (n + increment) % 1000; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator: 
                        if (increment != 0) {
                            am = !am;
                        }
                        if (am && h >= 12) {
                            h-= 12;
                        } else if (!am && h < 12) {
                            h+= 12;
                        }
                        break;
                }
                if (y < 0) y = 9999;
                if (M < 0) M = 11;
                if (d < 1) d = (new Date(y, M + 1, 0).getDate());
                if (h < 0) h = 23;
                if (m < 0) m = 59;
                if (s < 0) s = 59;
                if (n < 0) n = 999;
                if (M != value.getMonth() && d > (new Date(y, M + 1, 0).getDate())) d = (new Date(y, M + 1, 0).getDate());
                value = new Date(y, M, d, h, m, s, n);
                // constructor doesn't work with years such as '1'
                value.setFullYear(y);
                break;
            }
            case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: {
                var d = value.getDays();
                var h = value.getHours();
                var m = value.getMinutes();
                var s = value.getSeconds();
                var n = value.getMilliseconds();
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Day: d = (d + increment); break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Hour: h = (h + increment) % 24; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Minute: m = (m + increment) % 60; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Second: s = (s + increment) % 60; break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: n = (n + increment) % 1000; break;
                }
                if (d < 0) d = 999;
                if (h < 0) h = 23;
                if (m < 0) m = 59;
                if (s < 0) s = 59;
                if (n < 0) n = 999;
                value = new TimeSpan(d, h, m, s, n);
                break;
            }
        }
        return value;
    },
    updatePart : function(value, dataType, part) {
        if (this.get_readOnly()) return value;        
        var wasUpdated = false;        
        switch (this._type) {
            case NhsCui.Toolkit.Web.SpecifierContentType.List: {                
                var start = 0;
                switch (this._unit) {
                    case NhsCui.Toolkit.Web.SpecifierUnit.Month: start = value.getMonth(); break;
                    case NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator: start = value.getHours() < 12 ? 0 : 1; break;
                }
                var index = start;
                while ((index = ((index + 1) % this._list.length)) != start) {
                    if (this._list[index].toUpperCase().startsWith(part.toUpperCase())) {
                        switch (dataType) {
                            case NhsCui.Toolkit.Web.PickerDataType.DateTime: {
                                var M = value.getMonth();
                                var h = value.getHours();
                                switch (this._unit) {
                                    case NhsCui.Toolkit.Web.SpecifierUnit.Month: M = index; wasUpdated = true; break;
                                    case NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator: 
                                        if (index == 0 && h > 11) {
                                            h-=12; 
                                            wasUpdated = true; 
                                        } else if(index = 1 && h < 12) {
                                            h+=12;
                                            wasUpdated = true; 
                                        }
                                }
                                value = new Date(value.getFullYear(), M, value.getDate(), h, value.getMinutes(), value.getSeconds(), value.getMilliseconds());
                                break;
                            }
                        }
                        break;
                    }
                }
                break;
            }
            case NhsCui.Toolkit.Web.SpecifierContentType.Number: {        
                switch (dataType) {
                    case NhsCui.Toolkit.Web.PickerDataType.DateTime: {
                        var v = part;
                        if (typeof(part) == 'string') v = 1 * v;
                        if (isNaN(v)) break;
                        var y = value.getFullYear();
                        var M = value.getMonth();
                        var d = value.getDate();
                        var h = value.getHours();
                        var m = value.getMinutes();
                        var s = value.getSeconds();
                        var n = value.getMilliseconds();
                        switch (this._unit) {
                            case NhsCui.Toolkit.Web.SpecifierUnit.Year: 
                                if(part.length == 4) y = v; 
                                else if(part.length == 3) y = Math.floor(new Date().getFullYear() / 1000) * 1000 + v;
                                else if(part.length == 2) y = Math.floor(new Date().getFullYear() / 100) * 100 + v;
                                else if(part.length == 1) y = Math.floor(new Date().getFullYear() / 10) * 10 + v;
                                break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Month: M = (v - 1); break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Day: d = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Hour: 
                                if (this._specifier.charAt(0) == 'h' && v <= 0 || v > 12) break;
                                if (h < 12 || this._specifier.charAt(0) == 'H') {
                                    h = v; 
                                    if (h == 12) h = 0;
                                } else {
                                    h = v + 12;
                                    if (h == 24) h = 12;
                                } 
                                break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Minute: m = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Second: s = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: n = v; break;
                        }
                        if (y < 0) y = 0;
                        if (M < 0) M = 0;
                        if (d < 1) d = 1;
                        if (h < 0) h = 0;
                        if (m < 0) m = 0;
                        if (s < 0) s = 0;
                        if (n < 0) n = 0;
                        if ((y > 9999) || (M > 11) || (d > (new Date(y, M + 1, 0).getDate())) || (h > 23) || (m > 59) || (s > 59) || (n > 999)) {
                           break;
                        }
                        value = new Date(y, M, d, h, m, s, n);
                        wasUpdated = true;
                        break;
                    }
                    case NhsCui.Toolkit.Web.PickerDataType.TimeSpan: {
                        var v = part;
                        if (typeof(part) == 'string') v = 1 * v;
                        if (isNaN(v)) break;

                        var d = value.getDays();
                        var h = value.getHours();
                        var m = value.getMinutes();
                        var s = value.getSeconds();
                        var n = value.getMilliseconds();
                        switch (this._unit) {
                            case NhsCui.Toolkit.Web.SpecifierUnit.Day: d = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Hour: h = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Minute: m = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Second: s = v; break;
                            case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: n = v; break;
                        }
                        if (d < 0) d = 0;
                        if (h < 0) h = 0;
                        if (m < 0) m = 0;
                        if (s < 0) s = 0;
                        if (n < 0) n = 0;
                        if ((d > 999) || (h > 23) || (m > 59) || (s > 59) || (n > 999)) {
                            break;
                        }
                        value = new TimeSpan(d, h, m, s, n);
                        wasUpdated = true;
                        break;
                    }
                    default: 
                        break;
                }
            }
        }
        return { value: value, wasUpdated : wasUpdated };
    },
    toString : function() { return '{' + this._specifier + '}'; }
}
NhsCui.Toolkit.Web.PickerFormatSpecifier.registerClass("NhsCui.Toolkit.Web.PickerFormatSpecifier", null);

NhsCui.Toolkit.Web._PickerUtility = function() {
    this._dateTimeSpecifiers = null;
    this._timeSpanSpecifiers = null;
}
NhsCui.Toolkit.Web._PickerUtility.prototype = {
    /// summary:
    ///   gets the valid DateTime format specifiers
    getDateTimeSpecifiers : function() {
        if (this._dateTimeSpecifiers == null) {
            var ar = [];
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("yyyy", NhsCui.Toolkit.Web.SpecifierUnit.Year, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("yyy", NhsCui.Toolkit.Web.SpecifierUnit.Year, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("yy", NhsCui.Toolkit.Web.SpecifierUnit.Year, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("y", NhsCui.Toolkit.Web.SpecifierUnit.Year, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("MMMM", NhsCui.Toolkit.Web.SpecifierUnit.Month, NhsCui.Toolkit.Web.SpecifierContentType.List, Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("MMM", NhsCui.Toolkit.Web.SpecifierUnit.Month, NhsCui.Toolkit.Web.SpecifierContentType.List, Sys.CultureInfo.CurrentCulture.dateTimeFormat.AbbreviatedMonthNames));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("MM", NhsCui.Toolkit.Web.SpecifierUnit.Month, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("M", NhsCui.Toolkit.Web.SpecifierUnit.Month, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("dddd", NhsCui.Toolkit.Web.SpecifierUnit.DayOfWeek, NhsCui.Toolkit.Web.SpecifierContentType.List, Sys.CultureInfo.CurrentCulture.dateTimeFormat.DayNames));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("ddd", NhsCui.Toolkit.Web.SpecifierUnit.DayOfWeek, NhsCui.Toolkit.Web.SpecifierContentType.List, Sys.CultureInfo.CurrentCulture.dateTimeFormat.AbbreviatedDayNames));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("dd", NhsCui.Toolkit.Web.SpecifierUnit.Day, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("d", NhsCui.Toolkit.Web.SpecifierUnit.Day, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("hh", NhsCui.Toolkit.Web.SpecifierUnit.Hour, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("h", NhsCui.Toolkit.Web.SpecifierUnit.Hour, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("HH", NhsCui.Toolkit.Web.SpecifierUnit.Hour, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("H", NhsCui.Toolkit.Web.SpecifierUnit.Hour, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("mm", NhsCui.Toolkit.Web.SpecifierUnit.Minute, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("m", NhsCui.Toolkit.Web.SpecifierUnit.Minute, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("ss", NhsCui.Toolkit.Web.SpecifierUnit.Second, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("s", NhsCui.Toolkit.Web.SpecifierUnit.Second, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("nnnn", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("nnn", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("nn", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("n", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("tt", NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator, NhsCui.Toolkit.Web.SpecifierContentType.List, [Sys.CultureInfo.CurrentCulture.dateTimeFormat.AMDesignator, Sys.CultureInfo.CurrentCulture.dateTimeFormat.PMDesignator]));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("t", NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator, NhsCui.Toolkit.Web.SpecifierContentType.List, [Sys.CultureInfo.CurrentCulture.dateTimeFormat.AMDesignator.charAt(0), Sys.CultureInfo.CurrentCulture.dateTimeFormat.PMDesignator.charAt(0)]));
            this._dateTimeSpecifiers = ar;
        }
        return this._dateTimeSpecifiers; 
    },

    /// summary:
    ///   gets the valid TimeSpan format specifiers
    getTimeSpanSpecifiers : function() { 
        if (this._timeSpanSpecifiers == null) {
            var ar = [];
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("dd", NhsCui.Toolkit.Web.SpecifierUnit.Day, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 3));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("d", NhsCui.Toolkit.Web.SpecifierUnit.Day, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 3));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("hh", NhsCui.Toolkit.Web.SpecifierUnit.Hour, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("h", NhsCui.Toolkit.Web.SpecifierUnit.Hour, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("mm", NhsCui.Toolkit.Web.SpecifierUnit.Minute, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("m", NhsCui.Toolkit.Web.SpecifierUnit.Minute, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("ss", NhsCui.Toolkit.Web.SpecifierUnit.Second, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("s", NhsCui.Toolkit.Web.SpecifierUnit.Second, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 2));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("nnnn", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("nnn", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("nn", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            Array.add(ar, new NhsCui.Toolkit.Web.PickerFormatSpecifier("n", NhsCui.Toolkit.Web.SpecifierUnit.Millisecond, NhsCui.Toolkit.Web.SpecifierContentType.Number, null, 4));
            this._timeSpanSpecifiers = ar;
        }
        return this._timeSpanSpecifiers; 
    },
    
    /// summary:
    ///   creates a regex that can be used to match specifiers
    createRegex : function(specifiers) {
        var builder = new Sys.StringBuilder();
        builder.append("/");
        for(var i = 0; i < specifiers.length; i++) {
            if(i > 0) {
                builder.append("|");
            }
            builder.append(specifiers[i].get_specifier());
        }
        builder.append("/g");
        return eval(builder.toString());
    }
}
NhsCui.Toolkit.Web._PickerUtility.registerClass("NhsCui.Toolkit.Web._PickerUtility", null);

NhsCui.Toolkit.Web.PickerUtility = new NhsCui.Toolkit.Web._PickerUtility();

//Sys.Application.notifyScriptLoaded();