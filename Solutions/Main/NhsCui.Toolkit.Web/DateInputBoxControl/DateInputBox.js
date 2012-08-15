//-----------------------------------------------------------------------
// <copyright file="DateInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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

var DateFunctionality = function() {
};
DateFunctionality.prototype = {
    Complex:0,
    Simple:1
};
DateFunctionality.registerEnum("DateFunctionality");

NhsCui.Toolkit.Web.DateInputBox = function(element) {
    NhsCui.Toolkit.Web.DateInputBox.initializeBase(this, [element]);
    this._format = "d";
    this._cssClass = "nhs_dateinput";
    this._visibleDate = null;
    this._calendarDate = null;
    this._calendarImage = null;
    this._imageWidth = -1;    
    this._hasFocus = false;   
    this._calendarHasFocus = false; 
        
    this._popupDiv = null;
    this._calendarDiv = null;
    this._monthSection = null;
    this._prevYearArrow = null;
    this._nextYearArrow = null;
    this._yearSection = null;
    this._prevMonthArrow = null;
    this._nextMonthArrow = null;
    
    this._prevYearArrowImage = null;
    this._nextYearArrowImage = null;
    
    this._prevMonthArrowImage = null;
    this._nextMonthArrowImage = null;
    
    this._year = null;    
    this._month = null;
    
    this._today = null;
        
    this._popupBehavior = null;
    this._daysRow = null;
    this._daysBody = null;
    this._isOpen = false;
    this._button = null;     
    
    this._buttonParentDiv = null;
    this._buttonBaseDiv = null;
    this._buttonTopDiv = null;
        
    this._nullStrings = []; 
    this._valueIsNullString = false;    
    
    this.freeformMode = 0;
    this.arithmeticMode = 1;
    this.assistedFreeformMode = 2;
    this.nullEntryMode = 3;

    this._mode = this.freeformMode;
    this._totalWidthFixed = false;       
        
    //Setup events
    this._focusDelegate = Function.createDelegate(this, this._focusHandler);
    this._blurDelegate = Function.createDelegate(this, this._blurHandler);
    this._resizeDelegate = Function.createDelegate(this, this._resizeHandler);
    this._keyDownDelegate = Function.createDelegate(this, this._keyDownHandler);    
    this._keyPressDelegate = Function.createDelegate(this, this._keyPressHandler);      
    this._buttonClickDelegate = Function.createDelegate(this, this._buttonClickHandler);
    this._approxCheckedDelegate = Function.createDelegate(this, this._approxCheckedHandler);
    this._closeDelegate = Function.createDelegate(this, this._closeClickHandler);    
    this._pasteDelegate = Function.createDelegate(this, this._pastehandler);    
    this._propertyChangedDelegate = Function.createDelegate(this, this._propertyChangedHandler);
        
    // whether in finish editing
    this._inFinishEditing = false;
};

NhsCui.Toolkit.Web.DateInputBox.MinDateValue = -62135596800000;
NhsCui.Toolkit.Web.DateInputBox.MaxDateValue = 253402300799000;

NhsCui.Toolkit.Web.DateInputBox.prototype = {  
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
                if (this.approximateControl) //the control exists, otherwise nothing to apply.
                {                           
                    Sys.UI.DomElement.removeCssClass(this.approximateControl, oldCheckBoxCss);                    
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
                Sys.UI.DomElement.addCssClass(this.approximateControl, value);
            }            
            this.raisePropertyChanged("checkBoxCssClass");                                      
        }
    },  
    initialize : function() {
        NhsCui.Toolkit.Web.DateInputBox.callBaseMethod(this, "initialize");
        var elt = this.get_element();
        elt.autocomplete = "off";
        this._button = this._createButton();
        this._resizeButton();
        $addHandlers(elt, 
            {
                focus : this._focusDelegate,
                blur : this._blurDelegate,
                keypress : this._keyPressDelegate,
                keydown : this._keyDownDelegate,
                resize : this._resizeDelegate,
                move : this._resizeDelegate,
                paste : this._pasteDelegate,
                DOMAttrModified : this._resizeDelegate
            });
            
        $addHandler(window, 'resize', this._resizeDelegate);
              
        NhsCuiValidation.SetValidationTargetToActualControl(this.get_element());
        
        this.add_propertyChanged(this._propertyChangedDelegate);
                      
        // Much like the evaluationfunction for a client side validator comes down as a string 
        // and then needs to be evaluated the valAttachedServerSide string needs to converted to an actual boolean 
        if (typeof(elt.parentNode.valAttachedServerSide)== "string"){
            elt.parentNode.valAttachedServerSide = Boolean.parse(elt.parentNode.valAttachedServerSide);
        }
        
        this._getWatermarkExtender();    
        
        this._updateApproximateElement();

        var checkBoxCss = this._getState().CheckBoxCssClass;
        if (checkBoxCss)
        {
            if ("" != checkBoxCss)
            {
                Sys.UI.DomElement.addCssClass(this.approximateControl, checkBoxCss);
            }
        }   
        
        this._enablePicker(this.get_dateType() === DateType.Exact || this.get_dateType() === DateType.Approximate);                                                                
                        
        if (this.get_displayDayOfWeek())
            this._setText(this._formattedDate());
    },    
    get_TooltipText : function() {
        return this.get_element().title;
    },
    set_TooltipText : function(value) {           
        this.get_element().title = value;
    },      
    get_calendarImage : function() {
        return this._calendarImage;
    },
    set_calendarImage : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._calendarImage !== value)
        {
            this._calendarImage = value;
            this.raisePropertyChanged('calendarImage');
        }
    },
    get_functionality : function() {
        return this._getState().Functionality;
    },
    set_functionality : function(value) {
        var e = Function._validateParams(arguments, [
            {name: "value", type: Number}
        ]);
        if (e) throw e;    
    
        this._getState().Functionality = value;
        
        if(value === DateFunctionality.Simple)
        {
            this.set_dateType(DateType.Exact);
        }  
        
        this._updateYearMonthStyle();
        this.raisePropertyChanged('functionality');
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        } 
    },
    
    get_calendarPosition : function() 
    {
        return this._getState().CalendarPosition;
    },
    set_calendarPosition : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: AjaxControlToolkit.PopupControlPopupPosition, mayBeNull: false}]);
        
        if (e) throw e;
    
        this._getState().CalendarPosition = value;                                             
    },
    
    get_displayDayOfWeek : function() {
        return this._getState().DisplayDayOfWeek;
    },
    set_displayDayOfWeek : function(value) {
        this._getState().DisplayDayOfWeek = value;        
        var picker = this._getPickerBehavior();
        if(picker)
        {
            picker.set_format(this.get_format());
        }        
        this.raisePropertyChanged("displayDayOfWeek");
        
        this._updateHtmlElementsToDateValue();
    },
    get_displayDateAsText : function() {
        return this._getState().DisplayDateAsText;
    },
    set_displayDateAsText : function(value) {
        this._getState().DisplayDateAsText = value;
        
        this.raisePropertyChanged("displayDateAsText");
        
        this._updateHtmlElementsToDateValue();
    },
    get_button : function() {
        return this._button;
    },        
    get_format : function() { 
        var gs = new GlobalizationService();        
        return (gs.shortDatePattern);
    },
    _get_nullStringValue : function() {
        var nullIndex = this.get_nullIndex();
        if (nullIndex >= 0 && nullIndex < this._nullStrings.length)
            return this._nullStrings[nullIndex];
        else
            return String.format("Null:{0}", nullIndex);
    },
    get_nullIndex : function() { 
        return this.get_value().get_nullIndex(); 
    },
    set_nullIndex : function(value) {
        var date=this.get_value();
        
        if(date.get_dateType() != DateType.NullIndex)
        {
            date.set_dateType(DateType.NullIndex);
        }
        
        date.set_nullIndex(value); 
        this.set_value(date);             
        this.raisePropertyChanged("nullindex");
    },
    get_allowApproximate : function() {
        return this._getState().AllowApproximate;
    },
    set_allowApproximate : function(value) {
        var e = Function._validateParams(arguments, [
            {name: "value", type: Boolean}
        ]);
        if (e) throw e;        
    
        this._getState().AllowApproximate = value;
                
        this.raisePropertyChanged('allowApproximate');
        this._updateApproximateElement();
    },
    get_nullStrings : function() {
        return this._nullStrings.join(",");
    },
    set_nullStrings : function(value) {
        if (value.length===0)
        {
            //NullStrings is empty so clear the null strings array
            this._nullStrings.length = 0;
        }
        else
        {
            this._nullStrings = value.split(",");
        }
        
        // check duplicate null string.
        for (i=0;i<this._nullStrings.length;i++)
        {
            for (j=i+1;j<this._nullStrings.length;j++)
            {
                if (this._nullStrings[i].toUpperCase() == this._nullStrings[j].toUpperCase())
                {                                        
                    throw Error.argument("value", NhsCui.Toolkit.Web.DateInputBoxResources.DuplicateNullString + "'" + this._nullStrings[j] + "'");
                }
            }
        }                             
        
        this.raisePropertyChanged('nullStrings');
        
        if (this.get_isInitialized()) {
            this._updateHtmlElementsToDateValue();
        }        
    },
    get_dateType : function() {
        return this.get_value().get_dateType();   
    },
    _setDateTypeToNullOnError : function() {
        this.get_value().set_dateType(DateType.Null);
    },
    set_dateType : function(value) {
    
        this.get_value().set_dateType(value);
        
        this.raisePropertyChanged("dateType");
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
            this._manageApproxEnable();
        }        
    },
    get_month : function() {
        return this.get_value().get_month();   
    },
    set_month : function(value) {
        this.get_value().set_month(value); 
        
        this.raisePropertyChanged("month"); 
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }        
    },    
    get_year : function() {
        return this.get_value().get_year();   
    },
    set_year : function(value) {
        this.get_value().set_year(value); 
    
        this.raisePropertyChanged("year");
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }        
    },
    get_dateValue : function() {
        return this.get_value().get_dateValue();   
    },
    set_dateValue : function(value) {
        this.get_value().set_dateValue(value); 
        
        this.raisePropertyChanged("dateValue");
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }        
    },
    get_value : function() {
        return this._getState().Value;        
    },
    set_value : function(value) {
        var e = Function._validateParams(arguments, [
            {name: "value", type: NhsDate, mayBeNull: false}
        ]);
        if (e) throw e;        

    
        if(this.get_functionality() === DateFunctionality.Simple && 
                value.get_dateType() !== DateType.Exact)
        {
            throw Error.argumentOutOfRange("value.dateType", value.get_dateType(), "Only DateType.Exact mode is allowed when Functionality is set to DateFunctionality.Simple");
        }
        
        this._getState().Value = value;
                
        this.raisePropertyChanged('value');
            
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
            this._manageApproxEnable();
        }          
    },
    get_watermarkText : function() {
        return this._getState().WatermarkText;
    },
    set_watermarkText : function(value) {
        var e = Function._validateParams(arguments, [
            {name: "value", type: String, mayBeNull: false}
        ]);
        if (e) throw e;
        this._getState().WatermarkText = value;        
        var extender = this._getWatermarkExtender();
        if(extender)
        {
            extender.set_WatermarkText(value);
        }
        this.raisePropertyChanged("watermarkText");
    },
    get_watermarkCssClass : function() {
        return this._getState().WatermarkCssClass;
    },
    set_watermarkCssClass  : function(value) {
        var e = Function._validateParams(arguments, [
            {name: "value", type: String, mayBeNull: false}
        ]);
        if (e) throw e;
        this._getState().WatermarkCssClass = value;        
        var extender = this._getWatermarkExtender();
        if(extender)
        {
            extender.set_WatermarkCssClass(value);
        }
        this.raisePropertyChanged("watermarkCssClass");
    },
    get_cssClass : function() {
        return this._cssClass;
    },
    set_cssClass : function(value) {
        if (this._cssClass != value) {
            if (this._cssClass && this._popupDiv)
            {
                Sys.UI.DomElement.removeCssClass(this._popupDiv, this._cssClass);
            }
            this._cssClass = value;
            if (this._cssClass && this._popupDiv)
            {
                Sys.UI.DomElement.addCssClass(this._popupDiv, this._cssClass);
            }
            this.raisePropertyChanged("cssClass");
        }
    },            
    
    add_ambiguousDate: function(handler) {
        this.get_events().addHandler("ambiguousDate", handler);
    },
    remove_ambiguousDate: function(handler) {
        this.get_events().removeHandler("ambiguousDate", handler);
    },
    _fireAmbiguousDate: function(inputBox, nhsDate) {
        
         var e = Function._validateParams(arguments, [
            {name: "inputBox", domElement: true, mayBeNull: false},
            {name: "nhsDate", type: NhsDate, mayBeNull: false, optional: false}
        ]);
        if (e) throw e;        
        
        //Build the first one of the two dates as is...
        var date1 = nhsDate.get_dateValue();
        
        //... the second date will reverse the day and the month
        var date2 = new Date(date1.getFullYear(), date1.getDate() - 1, date1.getMonth() + 1);
        
        var args = {
                        firstDate : date1,
                        secondDate : date2,
                        selectedDate : date1
                    };
   
        var handler = this.get_events().getHandler("ambiguousDate");
        if (handler)
        {
            handler(this, args);
        }
        
        return args.selectedDate;
    },        
 _createButton : function() {
        var element = this.get_element();
        var button = document.createElement("input");
        var buttonImg = this._buttonImg = document.createElement("input");
        buttonImg.type = "image";
        buttonImg.tabIndex = -1;
        buttonImg.src = NhsDate_GetWebResourceUrl();
        buttonImg.style.position = "relative";
        if (element && element.style && element.style.zIndex)
        {
            buttonImg.style.zIndex = element.style.zIndex+1;
        }
        else
        {
            buttonImg.style.zIndex = 1;
        }
        button.type = "button";
        button.id = element.id + "_calendar_button";
        if(Sys.Browser.agent === Sys.Browser.Firefox)
        {
            button.style.margin = "-1px 0px";
        }
        else
        {
            button.style.margin = "0px";
            button.style.marginRight = "-16px";
        }
        if(NhsCui.Toolkit.Web.DateInputBoxResources)
        {
            button.title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonTitle;
            this._buttonImg.title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonTitle;
        }
        element.style.marginRight = "0px";
        Sys.UI.DomElement.addCssClass(button, "nhs_calendar_button");
        element.parentNode.insertBefore(button, element.nextSibling);
        element.parentNode.insertBefore(buttonImg, button.nextSibling);
        $addHandler(button, "click", this._buttonClickDelegate);
        $addHandler(buttonImg, "click", this._buttonClickDelegate);
        return button;
    },    
    _resizeButton : function() 
    {
        var button = this.get_button();
        var element = this.get_element();
        var parent = element.parentNode;
        var size = element.offsetHeight;
        var buttonImg = this._buttonImg;
        parent.style.position = "relative";        
        if(size >= 18)
        {
            // Fix for PS#4779 - also applies to this control's Calendar image
            // Don't want the button to be a square based on control height - rather it should
            // occupy a percentage of the width based on the default size of the control of 
            // 126 wide and 22 high - otherwise a square control would be entirely filled by the spinbuttons
            var adjustedButtonWidth = Math.round(element.offsetWidth * 22 / 126);
            adjustedButtonWidth = adjustedButtonWidth >= 22 ? adjustedButtonWidth : 22;

            button.style.display = buttonImg.style.display = "";
            button.style.height = size + "px";
            button.style.width = adjustedButtonWidth + "px";
            if(!this._totalWidthFixed)
            {
                var currentStyle = this._getCurrentStyle(element);
                element.style.width = Math.max(element.offsetWidth - adjustedButtonWidth -
                      (parseInt(currentStyle.borderLeftWidth, 10) || 0) -
                      (parseInt(currentStyle.borderRightWidth, 10) || 0), 0) + "px";
               this._totalWidthFixed = true;
            }
            
            // Mozilla is aligning button from center. This is to make sure button is aligned at bottom.
            if(Sys.Browser.agent === Sys.Browser.Firefox && size > 20)
            {
                var top = (size - 20)/2;    
                if (top > 0)
                    {
                        button.style.top = top + "px";
                        button.style.margin = "0px";
                        button.style.position = "relative";    
                    }
            }

            // Logic of superimposing image on the button for FF and IE. Consider image as 16*16 square block.     

            if(Sys.Browser.agent === Sys.Browser.Firefox)
            {
                Sys.UI.DomElement.setLocation(buttonImg, button.offsetLeft + parseInt(adjustedButtonWidth/2, 10) - 8, button.offsetTop + parseInt(size/2, 10) - 8);                               
            }
            else
            {
                buttonImg.style.left = (-1 * adjustedButtonWidth/2) + 8 + "px";                              
                                
                // 4 is the difference between span and image height. adding 4 aligns the image with bottom
                buttonImg.style.top = (4 - (size/2 - 8))  + "px";
            }            
        }
        else
        {
             button.style.display = "none";
             buttonImg.style.display = "none";
        }
   },       
   _getCurrentStyle : function(element) {
        if(element.currentStyle)
        {
            return element.currentStyle;
        }
        var w = (element.ownerDocument ? element.ownerDocument : element.documentElement).defaultView;
        return ((w && (element !== w) && w.getComputedStyle) ? w.getComputedStyle(element, null) : element.style);
    },
   _ensureApproximateControlCreated : function()
    {
        if(!this.approximateControl)
        {
            // Members for the approximate checkbox
            var element = this.get_element();
            this.approximateControl = document.createElement("span");                        
            this.approximateControl.style.position = "relative";
            var input = this._chkApprox = document.createElement("input");
            var label = document.createElement("label");
            input.type = "checkbox";
            input.name = element.id + "_approximate";
            input.id = element.id + "_approximate";
            label.setAttribute("for", input.id);
            label.appendChild(document.createTextNode(NhsDateResources.Approximate));
            this.approximateControl.appendChild(input);
            this.approximateControl.appendChild(label);
            element.parentNode.insertBefore(this.approximateControl, this._buttonImg.nextSibling);
            $addHandler(input, 'click', this._approxCheckedDelegate);
        }
    },
    _updateApproximateElement : function() {
        if(this.get_allowApproximate() && this.get_functionality() == DateFunctionality.Complex)
        {
            this._ensureApproximateControlCreated();
            this.approximateControl.style.display = "inline";
            this._manageApproxEnable();
            this.approximateControl.childNodes[0].checked = (this.get_dateType() === DateType.Approximate);
        }
        else if(this.approximateControl)
        {
            this.approximateControl.style.display = "none";
        }
    },  
    _manageApproxEnable : function() {
        if (this._chkApprox)
            this.approximateControl.disabled = (this.get_dateType() !== DateType.Exact && this.get_dateType() !== DateType.Approximate)
    },
    _updateHtmlElementsToDateValue : function() {
        var textBox = this.get_element();
        
        var formattedDate = this._formattedDate();
        
        this._updateApproximateElement();
                        
        this._setText(formattedDate);
      
        this._enablePicker(this._hasFocus && (this.get_dateType() === DateType.Exact || this.get_dateType() === DateType.Approximate));
        
        if(this._isOpen)
        {
            this._updateCalendar();
        }
        
    },
    _getText : function() {
        var watermarkExtender = this._getWatermarkExtender();
        if(watermarkExtender) 
        {
            return watermarkExtender.get_Text();
        } 
        else 
        {
             return this.get_element().value;
        }
    },
    _setText : function(value) {
        var watermarkExtender = this._getWatermarkExtender();
        if(watermarkExtender) 
        {
            watermarkExtender.set_Text(value);
        } 
        else 
        {
             this.get_element().value = value;
        }
    },
    _formattedDate : function() {
        
        var value = this.get_value();
        var formattedDate;
                 
        if(value.get_dateType() === DateType.NullIndex)
        {
            formattedDate = this._get_nullStringValue();
        }
        else
        {
            if (this._hasFocus == true)
            {
                formattedDate = value.toString(false, 
                                        false, false);
            }
            else
            {
                 formattedDate = value.toString(this.get_displayDayOfWeek(), 
                                        false, this.get_displayDateAsText());
             }
        }
        
        return formattedDate;
    },
    _getWatermarkExtender : function() {
        var element = this.get_element();
        var extender = Sys.UI.Behavior.getBehaviorByName(element, 'TextBoxWatermarkBehavior');
        
        if(!extender && AjaxControlToolkit.TextBoxWatermarkBehavior)
        {
            extender = $create(AjaxControlToolkit.TextBoxWatermarkBehavior, 
                        {   "WatermarkText" : this.get_watermarkText(),
                            "WatermarkCssClass" : this.get_watermarkCssClass()
                         }, null, null, element);
        }
        
        this._watermarkExtender = extender;
        extender.dispose = this._disposeWatermark;
        return extender;
    },
    _getForm : function() {
        var elem = this.get_element();
        
        while(elem && elem.nodeName !== "FORM")
        {
            elem = elem.parentNode;
        }
        return elem;
    },      
    
    _disposeWatermark : function() {
    var e = this.get_element();
        
        if(e == null)
            return;
            
        if (this._watermarkChangedHandler) {
            AjaxControlToolkit.TextBoxWrapper.get_Wrapper(this.get_element()).remove_WatermarkChanged(this._watermarkChangedHandler);
            this._watermarkChangedHandler = null;
        }

        // Unhook from Sys.Preview.UI.TextBox if present
        if(e.control && this._propertyChangedHandler) {
            e.control.remove_propertyChanged(this._propertyChangedHandler);
            this._propertyChangedHandler = null;
        }

        // Detach events
        if (this._focusHandler) {
            $removeHandler(e, 'focus', this._focusHandler);
            this._focusHandler = null;
        }
        if (this._blurHandler) {
            $removeHandler(e, 'blur', this._blurHandler);
            this._blurHandler = null;
        }
        if (this._keyPressHandler) {
            $removeHandler(e, 'keypress', this._keyPressHandler);
            this._keyPressHandler = null;
        }

        // Clear watermark text to avoid confusion during Refresh/Back/Forward
        if(AjaxControlToolkit.TextBoxWrapper.get_Wrapper(this.get_element()).get_IsWatermarked()) {
            this.clearText(false);
        }

        AjaxControlToolkit.TextBoxWatermarkBehavior.callBaseMethod(this, 'dispose');  
    },           
    
    dispose : function() {
        var e = this.get_element();
        this.set_allowApproximate(false);
        if (this._popupBehavior) {
            this._popupBehavior.dispose();
            this._popupBehavior = null;
        }
        
         if (this._watermarkExtender) {            
            this._watermarkExtender.dispose();
            this._watermarkExtender = null;
        }
        
         if (this._pickerExtender) {
            this._pickerExtender.dispose();
            this._pickerExtender = null;
        }                
        
        if (this._popupDiv)
        {
            $common.removeHandlers(this._popupDiv, 
                {
                    dragstart:this._cancelDelegate,
                    select:this._cancelDelegate
                });
            this._popupDiv.parentNode.removeChild(this._popupDiv);
            this._popupDiv = null;
        }        
        if (this._prevYearArrow)
        {
            this._unWatchCell(this._prevYearArrow);
            this._prevYearArrow = null;
        }
        if (this._prevYearArrowImage)
        {
            this._unWatchCell(this._prevYearArrowImage);
            this._prevYearArrowImage = null;
        }
        if (this._nextYearArrow)
        {        
            this._unWatchCell(this._nextYearArrow);
            this._nextYearArrow = null;
        }
        if (this._nextYearArrowImage) {        
            this._unWatchCell(this._nextYearArrowImage);
            this._nextYearArrowImage = null;
        }
        if (this._year) {            
            this._unWatchCell(this._year);
            this._year = null;
        }
        if (this._prevMonthArrow)
        {
            this._unWatchCell(this._prevMonthArrow);
            this._prevMonthArrow = null;
        }
        if (this._prevMonthArrowImage)
        {
            this._unWatchCell(this._prevMonthArrowImage);
            this._prevMonthArrowImage = null;
        }
        if (this._nextMonthArrow)
        {        
            this._unWatchCell(this._nextMonthArrow);
            this._nextMonthArrow = null;
        }
        if (this._nextMonthArrowImage)
        {        
            this._unWatchCell(this._nextMonthArrowImage);
            this._nextMonthArrowImage = null;
        }
        if (this._month)
        {            
            this._unWatchCell(this._month);
            this._month = null;
        }
        if (this._today)
        {            
            this._unWatchCell(this._today);
            this._today = null;
        }        
                
        if (this._calendarDiv)
        {
            $removeHandler(this._calendarDiv, "focus", this._calendarFocusDelegate);
            $removeHandler(this._calendarDiv, "blur", this._calendarBlurDelegate);                        
            $removeHandler(this._calendarDiv, "keydown", this._calendarKeyDownDelegate);
        }
        if (this._daysRow)
        {
            for (var i = 0; i < this._daysBody.rows.length; i++)
            {
                var row = this._daysBody.rows[i];
                for (var j = 0; j < row.cells.length; j++)
                {
                    var cell = row.cells[j];                    
                    this._unWatchCell(cell);
                }
            }
            this._daysRow = null;
        }

        var elt = this.get_element();       
        if (this._buttonImg && elt && elt.parentNode)
            elt.parentNode.removeChild(this._buttonImg);         
        if (this._button)
        {
            this._button.parentNode.removeChild(this._button);
            $removeHandler(this._button, "click", this._buttonClickDelegate);
            this._button = null;
        }        
                               
        $common.removeHandlers(elt, 
            {
                focus : this._focusDelegate,
                blur : this._blurDelegate,
                keypress : this._keyPressDelegate,
                keydown : this._keyDownDelegate,
                resize : this._resizeDelegate,
                move : this._resizeDelegate,
                DOMAttrModified : this._resizeDelegate
           });

        $removeHandler(window, 'resize', this._resizeDelegate);
        
        this.remove_propertyChanged(this._propertyChangedDelegate);
        
        if(this.approximateControl)
        {
            $removeHandler(this.approximateControl.childNodes[0], 'click', this._approxCheckedDelegate);
        }        

        NhsCui.Toolkit.Web.DateInputBox.callBaseMethod(this, "dispose");
    },
    _getState : function() {
        if(!this._state)
        {
            var serializedState = NhsCui.Toolkit.Web.DateInputBox.callBaseMethod(this, 'get_ClientState');
            
            if (serializedState!==null && serializedState.length > 0)
            {
                this._state = Sys.Serialization.JavaScriptSerializer.deserialize(serializedState);
                this._state.Value = $create(NhsCui.Toolkit.Web.NhsDate, this._state.Value, null, null);
                if(this._state.DateIsApproximate)
                {
                    this._state.Value.set_dateType(DateType.Approximate);
                }
            }
            else
            {
                this._state = { Value : new NhsDate(),
                                Functionality : DateFunctionality.Complex,
                                DisplayDayOfWeek : false,
                                DisplayDateAsText : false,
                                AllowApproximate : false,
                                WatermarkText : "",
                                WatermarkCssClass : ""
                                };
            }
        }
        
        return this._state;
    }, 
    _saveState : function() {
        if(this._state)
        {
            this._state.DateIsApproximate = (this._state.Value.get_dateType() === DateType.Approximate);
                        
            var serializedState = Sys.Serialization.JavaScriptSerializer.serialize(this._state);
            NhsCui.Toolkit.Web.DateInputBox.callBaseMethod(this, 'set_ClientState', [serializedState]);
        }
    },
    _show : function() {
        if (!this._isOpen)
        {
            var value = this.get_value();
            if(value.get_dateType() === DateType.Exact || 
                        value.get_dateType() === DateType.Approximate)
            {
                this._calendarDate = value.get_dateValue();
            }
            else if(value.get_dateType() === DateType.Year)
            {               
                this._calendarDate = new Date(value.get_year(),0,1);
            }
            else if(value.get_dateType() === DateType.YearMonth)
            {
                this._calendarDate = new Date(value.get_year(),value.get_month() - 1,1);               
            }
            if(!this._popupDiv)
            {
                this._popupDiv = document.createElement("div");
                if(this.get_cssClass())
                {
                    Sys.UI.DomElement.addCssClass(this._popupDiv, this.get_cssClass());
                }
                this._buildCalendar();
                this._buildHeader();
                this._buildDays();
                this._buildFooter();
                
                // Insert at correct position in the DOM
                if (Sys.Browser.agent === Sys.Browser.InternetExplorer)
                    event.srcElement.parentElement.insertAdjacentElement("afterEnd",this._popupDiv);
                else    
                    this.get_element().parentNode.appendChild(this._popupDiv);
                
                this._popupBehavior = $create(AjaxControlToolkit.PopupBehavior, { parentElement : this.get_element(), positioningMode : this.get_calendarPosition()}, {}, {}, this._popupDiv);
            }
            this._isOpen = true;
            this.get_button().title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonCloseTitle;   
            this._buttonImg.title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonCloseTitle;         
            this._visibleDate = null;
            this._updateCalendar();
            //this._popupBehavior.adjustPopupPosition(this._popupBehavior.getBounds());          
            this._popupBehavior.positioningMode = this.get_calendarPosition();
            this._popupBehavior.show();
            this._sizeCalendar(); 
            
            // for top positions re-align the calendar after size change.
            if (this._popupBehavior.positioningMode > 3)
            {
                this._popupBehavior.hide();
                this._popupBehavior.show();
            }
                         
            this._calendarDiv.focus();            
        } 
    },
    _hide : function() {
        this._isOpen = false;
        this.get_button().title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonTitle;
        this._buttonImg.title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonTitle;        
        if(this._popupBehavior)
        {
           this._popupBehavior.hide();
        }
        this.get_element().focus();
    },
    _buildCalendar : function() {
        // create delegates
        this._cellMouseOverDelegate = Function.createDelegate(this, this._cellMouseOverHandler);
        this._cellMouseOutDelegate = Function.createDelegate(this, this._cellMouseOutHandler);
        this._calendarFocusDelegate = Function.createDelegate(this, this._calendarFocusHandler);
        this._calendarBlurDelegate = Function.createDelegate(this, this._calendarBlurHandler);
        this._footerHeaderBlurDelegate = Function.createDelegate(this, this._footerHeaderBlurHandler);
        this._footerHeaderFocusDelegate = Function.createDelegate(this, this._footerHeaderFocusHandler);
        this._calendarClickDelegate = Function.createDelegate(this, this._calendarClickHandler);
        this._calendarKeyDownDelegate = Function.createDelegate(this, this._calendarKeyDownHandler);
        this._cancelDelegate = Function.createDelegate(this, this._cancelHandler);
        this._calendarResizeDelegate = Function.createDelegate(this, this._sizeCalendar);

        var popupDiv = this._popupDiv;
        $common.addCssClasses(popupDiv, [
            "nhs_dateinput_container",
            this._cssClass
        ]);
        Sys.UI.DomElement.setVisible(popupDiv, false);
        this._calendarDiv = document.createElement("div");
        this._popupDiv.id = this.get_element().id + "_calendar_main_popup"
        this._calendarDiv.id = this.get_element().id + "_calendar_main_body"
        this._popupDiv.tabIndex = 0;
        this._calendarDiv.tabIndex = 0;
        $addHandler(this._calendarDiv, "focus", this._calendarFocusDelegate);
        $addHandler(this._calendarDiv, "blur", this._calendarBlurDelegate);
        $addHandler(this._calendarDiv, "keydown", this._calendarKeyDownDelegate);
        popupDiv.appendChild(this._calendarDiv);
        
        $addHandlers(popupDiv,
            {
                focus : this._footerHeaderFocusDelegate,
                dragstart : this._cancelDelegate,
                select : this._cancelDelegate,
                blur : this._footerHeaderBlurDelegate
            });
        document.body.appendChild(popupDiv);
    },
    
    _updateYearMonthStyle : function() {
    // updates styling of year and month based upon current functionality.
    
        if (this.get_functionality() == DateFunctionality.Simple)
        {
            if (this._year && this._month)
            {
                this._year.style.cursor = this._month.style.cursor = "auto";
                this._year.title = "";           
                this._month.title = "";
                this._year.style.textDecoration = this._month.style.textDecoration = "none";  
            }                    
        }
        else
        {
            if (this._year && this._month)
            {
                this._year.style.cursor = this._month.style.cursor = "pointer";
                this._year.title = NhsCui.Toolkit.Web.DateInputBoxResources.yearTip;
                this._month.title = NhsCui.Toolkit.Web.DateInputBoxResources.monthTip;
                this._year.style.textDecoration = this._month.style.textDecoration = "underline";          
            }
        }    
    },
    
    _buildHeader : function() {
        this._header = this._buildHeaderElement(this._calendarDiv, "nhs_dateinput_header", null);
        this._monthSection = this._buildHeaderElement(this._header, "nhs_dateinput_monthheader", null);
        this._prevMonthArrow = this._buildHeaderElement(this._monthSection, "nhs_dateinput_prev", "prevMonth");
        this._month = this._buildHeaderElement(this._monthSection, "nhs_dateinput_month_title", "month");              
        this._nextMonthArrow = this._buildHeaderElement(this._monthSection, "nhs_dateinput_next", "nextMonth");
        this._yearSection = this._buildHeaderElement(this._header, "nhs_dateinput_yearheader", null);
        this._prevYearArrow = this._buildHeaderElement(this._yearSection, "nhs_dateinput_prev", "prevYear");
        this._year = this._buildHeaderElement(this._yearSection, "nhs_dateinput_year_title", "year");
        this._nextYearArrow = this._buildHeaderElement(this._yearSection, "nhs_dateinput_next", "nextYear");
        this._updateYearMonthStyle();
    },
    _buildHeaderElement : function(parentNode, cssClass, mode) {
    
        var element = document.createElement("div");
        Sys.UI.DomElement.addCssClass(element, cssClass);
        if(mode)
        {
            element.mode = mode;
            element.id = this.get_element().id + "_calendar_" + mode;                       
            this._watchCell(element);                                      
        }
        
        $addHandler(element, "blur", this._footerHeaderBlurDelegate);  
        $addHandler(element, "focus", this._footerHeaderFocusDelegate);                
        parentNode.appendChild(element);
        return element;
    },
    _buildDays : function() {
        var days = this._days = document.createElement("div");
        Sys.UI.DomElement.addCssClass(days, "nhs_days");
        this._calendarDiv.appendChild(days);
        $addHandler(this._days, "focus", this._footerHeaderFocusDelegate);
        $addHandler(this._days, "blur", this._footerHeaderBlurDelegate);
        var dayIdBase = this.get_element().id + "_calendar_day_";        
        
        // Header Table
        var headerTable = this._headerTable = document.createElement("table");
        headerTable.id = dayIdBase + "_headerTable";
        headerTable.cellPadding = 0;
        headerTable.cellSpacing = 2;                
        headerTable.width = "100%";
        Sys.UI.DomElement.addCssClass(headerTable, "nhs_table_day_header");
        days.appendChild(headerTable);        
        $addHandler(this._headerTable, "focus", this._footerHeaderFocusDelegate);
        $addHandler(this._headerTable, "blur", this._footerHeaderBlurDelegate);
        
        var headerBody = this._headerBody = document.createElement("tbody");
        headerBody.id = dayIdBase + "_headerBody";
        headerTable.appendChild(headerBody);
        $addHandler(this._headerBody, "focus", this._footerHeaderFocusDelegate);
        $addHandler(this._headerBody, "blur", this._footerHeaderBlurDelegate);
        
        var daysHeaderRow = document.createElement("tr");
        daysHeaderRow.id = dayIdBase + "_daysHeaderRow";
        headerBody.appendChild(daysHeaderRow);   
        $addHandler(daysHeaderRow, "focus", this._footerHeaderFocusDelegate);
        $addHandler(daysHeaderRow, "blur", this._footerHeaderBlurDelegate);   
                
        var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;        
        
        // Calendar starting from monday.
        for (var i = 1; i < 8; i++)
        {
            var dayCell = document.createElement("td");           
            dayCell.id = dayIdBase + "_dayHeader" + i;
            this._setInnerText(dayCell, dtf.ShortestDayNames[i % 7]);
            Sys.UI.DomElement.addCssClass(dayCell, "nhs_dateinput_dayname");                       
            daysHeaderRow.appendChild(dayCell);
            $addHandler(dayCell, "focus", this._footerHeaderFocusDelegate);
            $addHandler(dayCell, "blur", this._footerHeaderBlurDelegate);   
        }                                   
        
        // Days Table
        var daysTable = this._daysTable = document.createElement("table");
        daysTable.id = dayIdBase + "_daysTable";
        daysTable.cellPadding = 0;
        daysTable.cellSpacing = 5;
        days.appendChild(daysTable);                                 
        var daysBody = this._daysBody = document.createElement("tbody");
        daysBody.id = dayIdBase + "_daysBody";
        daysTable.appendChild(daysBody);
        $addHandler(this._daysTable, "focus", this._footerHeaderFocusDelegate);
        $addHandler(this._daysTable, "blur", this._footerHeaderBlurDelegate);   
        $addHandler(this._daysBody, "focus", this._footerHeaderFocusDelegate);
        $addHandler(this._daysBody, "blur", this._footerHeaderBlurDelegate);           

        var idCount = 0;
        for (i = 0; i < 6; i++) 
        {
            var daysRow = document.createElement("tr");
            daysRow.id = dayIdBase + "_daysRow" + i;            
            daysBody.appendChild(daysRow);
            $addHandler(daysRow, "focus", this._footerHeaderFocusDelegate);
            $addHandler(daysRow, "blur", this._footerHeaderBlurDelegate);    
            for(var j = 0; j < 7; j++) {
                dayCell = document.createElement("td");
                this._setInnerText(dayCell, "&nbsp");            
                dayCell.mode = "day";
                dayCell.id = dayIdBase + (idCount++);
                Sys.UI.DomElement.addCssClass(dayCell, "nhs_dateinput_day");
                this._watchCell(dayCell);                
                daysRow.appendChild(dayCell);
                $addHandler(dayCell, "focus", this._footerHeaderFocusDelegate);
                $addHandler(dayCell, "blur", this._footerHeaderBlurDelegate);   
            }
        }
    },
    _setInnerText : function(element, text) 
    {
        var child = element.lastChild;
        while(child)
        {
            if(child.nodeType === 3)
            {
                element.removeChild(child);
            }
            child = child.prevousSibling;
        }
        
        if(text)
        {
            element.appendChild(document.createTextNode(text));
        }
    },
    _buildFooter : function() {
        // table for layout
        
        var footerTable = this._footerTable = document.createElement("table");
        $addHandler(footerTable, "focus", this._footerHeaderFocusDelegate);
        $addHandler(footerTable, "blur", this._footerHeaderBlurDelegate);    
        footerTable.width = "100%";
        footerTable.cellSpacing = 3;
        var footerBody = this._footerBody = document.createElement("tbody");
        this._footerTable.appendChild(this._footerBody); 
        $addHandler(footerBody, "focus", this._footerHeaderFocusDelegate);
        $addHandler(footerBody, "blur", this._footerHeaderBlurDelegate);  
        
        var footerRow = document.createElement("tr");
        var todayCell = this._todayCell = document.createElement("td");
        var closeCell = this._closeCell = document.createElement("td");
        
        todayCell.id = this.get_element().id + "_calendar_footer" + "_today_cell";
        todayCell.align ="left";
        closeCell.id = this.get_element().id + "_calendar_footer" + "_close_cell";
        closeCell.align ="right";
        $addHandler(this._todayCell, "blur", this._footerHeaderBlurDelegate);              
        $addHandler(this._closeCell, "blur", this._footerHeaderBlurDelegate); 
        $addHandler(this._todayCell, "focus", this._footerHeaderFocusDelegate);
        $addHandler(this._closeCell, "focus", this._footerHeaderFocusDelegate);          
        
        //Add Today Links
        var today = this._today = document.createElement("input");
        today.mode = "today";
        today.type = "button"
        today.id = this.get_element().id  + "_calendar_footer" + "_today_button";
        Sys.UI.DomElement.addCssClass(today, "nhs_dateinput_today");   
        today.title =  NhsCui.Toolkit.Web.DateInputBoxResources.todayTitle;     
        this._watchCell(today); 
        $addHandler(this._today, "blur", this._footerHeaderBlurDelegate);   
        $addHandler(this._today, "focus", this._footerHeaderFocusDelegate);           
        todayCell.appendChild(today);
        
        // Create close link
        var close = this._close = document.createElement("input");
        close.type = "button"
        close.value = NhsCui.Toolkit.Web.DateInputBoxResources.close;
        close.id = this.get_element().id  + "_calendar_footer" + "_close_button";
        Sys.UI.DomElement.addCssClass(close, "nhs_dateinput_close");
        close.title =  NhsCui.Toolkit.Web.DateInputBoxResources.closeTitle;
        $addHandler(this._close, "click", this._closeDelegate);
        $addHandler(this._close, "blur", this._footerHeaderBlurDelegate);
        $addHandler(this._close, "focus", this._footerHeaderFocusDelegate); 
        this._close.tabIndex = 0;       
        closeCell.appendChild(close);                        
        
        footerRow.appendChild(todayCell);
        footerRow.appendChild(closeCell);                
        
        this._footerBody.appendChild(footerRow);
        $addHandler(footerRow, "blur", this._footerHeaderBlurDelegate);   
        $addHandler(footerRow, "focus", this._footerHeaderFocusDelegate);     
        
        this._popupDiv.appendChild(this._footerTable);
    },
    
    _watchCell: function(cell) {
        $addHandlers(cell,
            {
                mouseover:this._cellMouseOverDelegate,
                mouseout:this._cellMouseOutDelegate,
                click:this._calendarClickDelegate
            });
    },
    _unWatchCell: function(cell) {
        $common.removeHandlers(cell,
            {
                mouseover:this._cellMouseOverDelegate,
                mouseout:this._cellMouseOutDelegate,
                click:this._calendarClickDelegate
            });
    },
    _updateCalendar : function() {
        var today = new Date().getDateOnly();
        var value = this.get_value().get_dateValue();
        if (this._visibleDate === null)
        {
            this._visibleDate = value;
            if (this._visibleDate === null)
            {
                this._visibleDate = today;
            }
        }
        var date = this._visibleDate;
        
        if (this._calendarDate === null)
        {
            this._calendarDate = date;
        }
        
        var startOfMonth = new Date(this._calendarDate.getFullYear(), this._calendarDate.getMonth(), 1);

        var startOfMonthDayOfWeek = startOfMonth.getDay();
        var daysInMonth = new Date(this._calendarDate.getFullYear(), this._calendarDate.getMonth() + 1, 0).getDate();
        var daysInLastMonth = new Date(this._calendarDate.getFullYear(), this._calendarDate.getMonth(), 0).getDate();
        
        //weeksInMonth will always be at least 4 because dayInWeek is always 7, the shortest month is 
        //28, 28 / 7 = 4 so initialise weeksInMonth to 4
        
        var weeksInMonth = 4;
        
        //If daysInMonth is anything over 28 we will need at least another week row so add one
        
        if (daysInMonth > 28){
            weeksInMonth++;
        }
        
        //Depending on where in the week the month start we may need 6 week rows
        //1 is added because starting day is monday in our calendar and not
        //Sunday.
        if ((startOfMonthDayOfWeek) > (7 - (daysInMonth - 28) + 1)){
            weeksInMonth++;
        }
        
        //This may look like an odd assignment but it works because startOfMonthDayOfWeek is zero based
        //and our calendar is starting from monday.
        var daysToBacktrack = (startOfMonthDayOfWeek ===1 ? 7 : startOfMonthDayOfWeek === 0 ? 6 : startOfMonthDayOfWeek - 1);
        
        var spanToGoBackFromStartOfMonth = AjaxControlToolkit.TimeSpan.fromDays(daysToBacktrack).subtract(AjaxControlToolkit.TimeSpan.fromHours(1));
        
        var startDate = startOfMonth.subtract(spanToGoBackFromStartOfMonth);
        
        var currentDate = startDate;
        
        for (var week = 0; week < this._daysBody.rows.length; week++) {
            var weekRow = this._daysBody.rows[week];
            for (var dayOfWeek = 0; dayOfWeek < weekRow.cells.length; dayOfWeek++)
            {
                var dayCell = weekRow.cells[dayOfWeek];
                this._setInnerText(dayCell, currentDate.getDate());
                dayCell.title = currentDate.format("D");
                dayCell.date = currentDate;
                $common.removeCssClasses(dayCell, [
                    "nhs_dateinput_other",
                    "nhs_dateinput_active"
                ]);
                Sys.UI.DomElement.addCssClass(dayCell, this._getCssClass(dayCell.date, 'd'));
                currentDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1);
            }
        }
        this._prevMonthArrow.date = new Date(startOfMonth.getFullYear(), startOfMonth.getMonth() - 1, startOfMonth.getDate());
        this._nextMonthArrow.date = new Date(startOfMonth.getFullYear(), startOfMonth.getMonth() + 1, startOfMonth.getDate());
        
        this._prevYearArrow.date = new Date(startOfMonth.getFullYear() - 1, startOfMonth.getMonth(), startOfMonth.getDate());
        this._nextYearArrow.date = new Date(startOfMonth.getFullYear() + 1, startOfMonth.getMonth(), startOfMonth.getDate());
        
        this._setInnerText(this._month, this._calendarDate.format("MMMM"));
        this._setInnerText(this._year,  this._calendarDate.format("yyyy"));
        this._setInnerText(this._prevMonthArrow, "<");
        this._setInnerText(this._nextMonthArrow, ">");
        this._setInnerText(this._prevYearArrow, "<");
        this._setInnerText(this._nextYearArrow, ">");
        
        if(NhsCui.Toolkit.Web.DateInputBoxResources)
        {
            this._prevMonthArrow.title = NhsCui.Toolkit.Web.DateInputBoxResources.prevMonth;            
            this._nextMonthArrow.title = NhsCui.Toolkit.Web.DateInputBoxResources.nextMonth;
            this._prevYearArrow.title = NhsCui.Toolkit.Web.DateInputBoxResources.prevYear;            
            this._nextYearArrow.title = NhsCui.Toolkit.Web.DateInputBoxResources.nextYear;
        }
        
        this._month.date = this._calendarDate;
        this._year.date = this._calendarDate;
        
        //Set Today link in bottom LH corner
        this._today.value = "Today";
        this._today.date = today;                     
        
        if(this.get_functionality() === DateFunctionality.Complex)
        {
            Sys.UI.DomElement.addCssClass(this._popupDiv, "nhs_dateinput_complex");
        }
        else
        {
            Sys.UI.DomElement.removeCssClass(this._popupDiv, "nhs_dateinput_complex");
        }
    },
    _fireChanged : function() {
        var elt = this.get_element();
        if (document.createEventObject) {
            elt.fireEvent("onchange");
        } else if (document.createEvent) {
            var e = document.createEvent("HTMLEvents");
            e.initEvent("change", true, true);
            elt.dispatchEvent(e);
        }
    },

    _isSelected : function(date, part) {
        var value = this.get_value();
        if (!value) return false;
        switch (part)
        {
            case 'd':
                if (date.getDate() != value.get_dateValue().getDate()) return false;
                // goto case 'M';
            case 'M':
                if (date.getMonth() != value.get_dateValue().getMonth()) return false;
                // goto case 'y';
            case 'y':
                if (date.getFullYear() != value.get_dateValue().getFullYear()) return false;
                break;
            default:
                break;
        }
        return true;
    },
    
    // Is the date passed is a weekend or a normal day.
    _isWeekend : function(date, part) {
        switch (part)
        {
            case 'd': return (date.getDay() == 0 || date.getDay() == 6);
            case 'M': return false;
            case 'y': return false;              
            default:
                break;
        }
        return false;
    },
    
    //Is the date passed in part of the current calendar month or is it part of the "other"
    _isOther : function(date, part) {
        var value = this._calendarDate;
        if (!value) return false;
        switch (part)
        {
            case 'd': return (date.getFullYear() != value.getFullYear() || date.getMonth() != value.getMonth());
            case 'M': return false;
            case 'y': 
                var minYear = (Math.floor(this._visibleDate.getFullYear() / 10) * 10);
                return date.getFullYear() < minYear || (minYear + 10) <= date.getFullYear();
            default:
                break;
        }
        return false;
    },
    _getCssClass : function(date, part) {
        if (this._isSelected(date, part))
        {
            return "nhs_dateinput_active";
        }
        else if (this._isOther(date, part))
        {
            return "nhs_dateinput_other";
        }
        else if (this._isWeekend(date, part))
        {
            return "nhs_dateinput_weekend";
        }
        else
        {
            return "";
        }
    },    
    _resetValidator : function(e) {
        this._validatedValue = true;
        NhsCuiValidation.FireAllAttachedValidators(this.get_element());
        this._validatedValue = false;
    },    
    _approxCheckedHandler : function(e) {        
        if(e.target.checked)
        {
            this.set_dateType(DateType.Approximate);
        } 
        else
        {
            this.set_dateType(DateType.Exact);
        }
        this._updateHtmlElementsToDateValue();
        this._resetValidator();        
    },
    _focusHandler : function(e) {
        this._hasFocus = true;
        var inputBox = this.get_element();              
        if (this.get_dateType() == DateType.Exact || this.get_dateType() == DateType.Approximate)
        {
            this._updateHtmlElementsToDateValue();
            NhsCuiValidation.FireAllAttachedValidators(this.get_element());            
        }
        
        if (this.get_dateType() == DateType.NullIndex)
            this._mode = this.nullEntryMode;
        
        this._selectRange(inputBox, 0, inputBox.value.length);
    },
    _blurHandler : function(e) {
        this._blurOccured = true;
        this._hasFocus = false;                
        this._finishEditing(this.get_element());              
        this._blurOccured = false;
    },
    _resizeHandler : function(e) {        
        if(this._button)
        {
            this._resizeButton();
        }
    },
    _cellMouseOverHandler : function(e) {                
        if (this.get_functionality() == DateFunctionality.Complex || (e.target.mode != "year" && e.target.mode != "month"))
            Sys.UI.DomElement.addCssClass(e.target, "nhs_dateinput_active");
    },
    _cellMouseOutHandler : function(e) {
        if (Sys.UI.DomElement.containsCssClass(e.target, "nhs_dateinput_active"))
            Sys.UI.DomElement.removeCssClass(e.target, "nhs_dateinput_active");
    },    
    _sizeCalendar : function(e) {
    var bounds = Sys.UI.DomElement.getBounds(this._daysTable); 
    this._popupDiv.style.width = bounds.width + "px";
    this._calendarDiv.style.width = bounds.width + "px";
    var cdrBound = Sys.UI.DomElement.getBounds(this._calendarDiv);
    var ftrBound = Sys.UI.DomElement.getBounds(this._footerTable);
    this._popupDiv.style.height = cdrBound.height + ftrBound.height + "px";
    },    
    
    _checkHide : function() {          
      var obj = document.NhsDate_current;
      if (obj._calendarHasFocus == false)            
        {
            obj._isOpen = false;
            obj.get_button().title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonTitle;
            obj._buttonImg.title = NhsCui.Toolkit.Web.DateInputBoxResources.CalendarButtonTitle;        
            if(obj._popupBehavior)
            {
                obj._popupBehavior.hide();
            }
        }                                      
    },
    
    _calendarFocusHandler : function(e) {
         this._calendarHasFocus = true;         
         var activeElement = this._findElementByCssClass(this._daysBody, "nhs_dateinput_active");
         if(!activeElement)
         {
            activeElement = this._daysBody.firstChild.firstChild;
         }
         
         this._setCalendarFocusElement(activeElement);
    },    
   
    _calendarBlurHandler : function(e) {                              
          this._setCalendarFocusElement(null);  
          this._calendarHasFocus = false;        
          document.NhsDate_current = this;    
          window.setTimeout(this._checkHide, 500);                              
   },
   
     _footerHeaderFocusHandler : function(e) {
         this._calendarHasFocus = true;
   },
   
   _footerHeaderBlurHandler : function(e) {
         this._calendarHasFocus = false;        
         window.setTimeout(this._checkHide, 500);
   },     
      
    _calendarKeyDownHandler : function(e) {
        
        var focusElement = this._getCalendarFocusElement();
        
        if(!focusElement)
        {
            focusElement = this._findElementByCssClass(this._daysBody, "nhs_dateinput_active");
        }
        
        var parentNode = (focusElement ? focusElement.parentNode : this._daysBody.firstChild);
        var childIndex = 0, monthYearSwitch = 4;
        var headerNodes = (this.get_functionality() === DateFunctionality.Complex ?
                            [this._prevMonthArrow, this._month, this._nextMonthArrow, 
                                this._prevYearArrow, this._year,  this._nextYearArrow] :
                            [this._prevMonthArrow, this._nextMonthArrow, 
                                this._prevYearArrow,  this._nextYearArrow]);
        
        if(focusElement)
        {
            // Array.indexOf doesn't work with childNodes
            var childNodes = (parentNode.tagName === "DIV" ? headerNodes : parentNode.childNodes);
            
            for(var i = 0; i < childNodes.length; i++)
            {
                if(childNodes[i] === focusElement)
                {
                    childIndex = i;
                    break;
                }
            }
        }
        
        var childIndexIncrement = 0;
        
        switch(e.keyCode)
        {
           case(Sys.UI.Key["esc"]) :
                this._hide();
                e.preventDefault();
                break;
                
           case(Sys.UI.Key["enter"]) :
                if(focusElement)
                {
                    this._calendarClickHandler({ target: focusElement });
                    e.preventDefault();
               }
                break;
             
           case(Sys.UI.Key["down"]) :
           case(Sys.UI.Key["up"]) :
                var down = (e.keyCode === Sys.UI.Key["down"]);
                if(parentNode.tagName === "DIV")
                {
                    parentNode = (down ? this._daysBody.firstChild : this._daysBody.lastChild);
                }
                else
                {
                    parentNode = (down ? parentNode.nextSibling : parentNode.previousSibling);
                }
                
                e.preventDefault();
                break;
               
            case(Sys.UI.Key["left"]) :
            case(Sys.UI.Key["right"]) :
               childIndex += (e.keyCode === Sys.UI.Key["right"] ? 1 : -1);
               e.preventDefault();
               break;
        }
        
        var childNodes = (parentNode && parentNode.tagName === "TR" ? parentNode.childNodes : headerNodes);

        childIndex = (childNodes.length + childIndex) % childNodes.length;
        this._setCalendarFocusElement(childNodes[childIndex]);

    },
    _findElementByCssClass : function(parentNode, cssClass) {
        var node = parentNode.firstChild;
        var matchElement = null;
        while(node && !matchElement)
        {
            if(node.nodeType === 1)
            {
                if(Sys.UI.DomElement.containsCssClass(node, cssClass))
                {
                    matchElement = node;
                }
                else
                {
                    matchElement = this._findElementByCssClass(node, cssClass);
                }
            }
            
            node = node.nextSibling;
        }
        return matchElement;
    },
    _setCalendarFocusElement : function(value) {
        var prevHoverCell = this._getCalendarFocusElement();
        
        if(prevHoverCell != null)
        {
            Sys.UI.DomElement.removeCssClass(prevHoverCell, "nhs_dateinput_focus");
        }
        
        if(value)
        {
            Sys.UI.DomElement.addCssClass(value, "nhs_dateinput_focus");
        }
    },
    _getCalendarFocusElement : function() {
        return this._findElementByCssClass(this._calendarDiv, "nhs_dateinput_focus");
    },              
    _calendarClickHandler : function(e) {        
        var cell = e.target;
        this._setCalendarFocusElement(null);
        Sys.UI.DomElement.removeCssClass(cell, "nhs_dateinput_active");
       
        switch(cell.mode)
        {
            
            case "prevMonth":
            case "nextMonth":
            case "prevYear":
            case "nextYear":
                this._calendarDate = cell.date;
                this._updateCalendar(); 
                break;          
            case "month":
                if(this.get_functionality() === DateFunctionality.Complex)
                {
                    var nhsDate = this.get_value();
                    
                    nhsDate.set_dateType(DateType.YearMonth);
                    nhsDate.set_month(cell.date.getMonth() + 1);
                    nhsDate.set_year(cell.date.getFullYear());
                                    
                    this.set_value(nhsDate);
                    this._hide();
                    NhsCuiValidation.FireAllAttachedValidators(this.get_element());
                }
                break;
            case "year":
                if(this.get_functionality() === DateFunctionality.Complex)
                {
                    var nhsDate = this.get_value();
                    
                    nhsDate.set_dateType(DateType.Year);
                    nhsDate.set_year(cell.date.getFullYear());
                    
                    this.set_value(nhsDate);
                    this._hide();
                    NhsCuiValidation.FireAllAttachedValidators(this.get_element());
                }
                break;                
            case "day":
                this._visibleDate = cell.date;
                this._calendarDate = cell.date;
                
                this.set_value(new NhsDate(cell.date));
                this._hide();
                
                // For day and today type validator is not fired at this stage. Validator is fired
                // in focusHandler because focussed value may be different if DisplayDateAsText or DisplayDayofWeek
                // is true.
                
                break;
            case "today":
                this._visibleDate = cell.date;
                this._calendarDate = cell.date;
                
                this.set_value(new NhsDate(cell.date));
                this._hide();
                // For day and today type validator is not fired at this stage. Validator is fired
                // in focusHandler because focussed value may be different if DisplayDateAsText or DisplayDayofWeek
                // is true.
                
                break;
            default:
                break;
        }
        if(e.stopPropagation)
        {
            e.stopPropagation();
            e.preventDefault();
        }
    },
       
    _buttonClickHandler : function(e) {
        // for end user this button works as a toggle, show and hide alternatively. We are not calling
        // hide in this function for internet explorer as checkHide() function 
        // [called on blur of calendar div] will automatically
        // hide the calendar.
        if (!this._isOpen)
        {
            this._show();
        }       
        else 
        {
            this._hide();
        }
         if(e.stopPropagation)
        {
            e.stopPropagation();
            e.preventDefault();
        }
        
        return false;
    },
    _propertyChangedHandler: function(sender, args) {
        this._saveState();
    },
    
    _closeClickHandler : function(e) {
    this._hide();    
    },    
   
    _cancelHandler : function(e) {
        e.stopPropagation();
        e.preventDefault();
    },
    _getPickerBehavior : function() {                
        return Sys.UI.Behavior.getBehaviorByName(this.get_element(), 'PickerBehavior');
    },
    _enablePicker : function(enable) {
        var picker = this._getPickerBehavior();
        if(!picker && enable && NhsCui.Toolkit.Web.PickerBehavior)
        {
            var element = this.get_element();
            picker = $create(NhsCui.Toolkit.Web.PickerBehavior, 
                        {   "acceptKeyDigitInput" : false,
                            "format" : this.get_format(),
                            "holdOwnValueState" : false,
                            "id": element.id + "_PickerExtender"
                         }, null, null, element);
        }
        
        this._pickerExtender = picker;
        
        if (picker && picker.get_enabled() != enable)
        {
            if(enable)
            {
                picker.set_value(this.get_value().get_dateValue());
            }
            picker.set_enabled(enable);
        }
    },
    _keyDownHandler : function(e) {
    
        if(e.keyCode === Sys.UI.Key["enter"])
        {
            //this._finishEditing(this.get_element());
            
            var inputBox = this.get_element();            
            this._selectRange(inputBox, inputBox.value.length, inputBox.value.length);
            
            // Implement LostFocusOnEnter functionality.
            inputBox.blur();
            this.get_button().focus();        
            
            // Enter key does other things. So swallow it early             
            e.preventDefault();
            e.stopPropagation();
        }
        
        if(e.altKey && e.keyCode === Sys.UI.Key["down"])
        {
            //show calendar
            this._show();

            if(e.preventDefault) e.preventDefault();

            return;
        }
        
        if(Sys.Browser.agent === Sys.Browser.InternetExplorer || 
                    Sys.Browser.agent === Sys.Browser.Safari)
        {
            // IE doesn't fire events for arrow keys in the keypress, so do it ourselves from the keydown
            // Safari fires weird codes in keypress and normal codes in keydown
            if(e.keyCode === Sys.UI.Key["up"] || e.keyCode === Sys.UI.Key["down"])
            {
                this._keyPressHandler({charCode:e.keyCode, altKey:e.altKey});
                e.preventDefault();
            }
        }
    },
    
    // keypress is the _only_ event to get the proper characters entered by the user. Other events
    // get the key pressed, not the character. 
    _keyPressHandler : function(e) {
        // Don't process tab
        if(e.charCode === Sys.UI.Key["tab"])
        {
            return;
        }
 
        var inputBox = this.get_element();
        var text = this._getText();
        var character = String.fromCharCode(e.charCode);
        var functionality = this.get_functionality();
        
        if(e.charCode === Sys.UI.Key["space"] && functionality === DateFunctionality.Complex &&
                this._nullStrings && this._nullStrings.length > 0)
        {
            var nullIndex = this._findNullIndex(text);
            
            if(nullIndex >= 0 || this._nextCharEnteredWillBeFirst(inputBox))
            {
                // cycle null strings
                nullIndex = (nullIndex >= 0 ? (nullIndex + 1) % this._nullStrings.length : 0);
                inputBox.value = this._nullStrings[nullIndex];
                this._mode = this.nullEntryMode;
                this._enablePicker(false);
                if(e.preventDefault) e.preventDefault();
                return;
            }
        }
        
        if((this._mode === this.assistedFreeformMode || this._mode === this.nullEntryMode) && 
            e.charCode >= Sys.UI.Key["space"] && functionality === DateFunctionality.Complex){
            if (this._checkFreeFormAssistedEntry(inputBox, character))
            {
                if(e.preventDefault) e.preventDefault();
            }
           
        } 
        else if(this._mode === this.freeformMode)
        {
            
            if(functionality === DateFunctionality.Complex && 
                this._checkFreeFormAssistedEntryTrigger(inputBox, character))
            {
                this._enablePicker(false);
                
                this._selectRange(inputBox, 1, text.length);
                
                // Under this mode we are in control of what goes into the textbox. So suppress
                if(e.preventDefault) e.preventDefault();
            } 
            else if((character === '+' || character === '-') && 
                    this._nextCharEnteredWillBeFirst(inputBox)) //i.e. the character is a +or- AND there are no other values in the InputBox
            {
                // Switch into arithmetic mode
                this._mode = this.arithmeticMode;
                this._enablePicker(false);
                this._setText(character);
                
                // Under this mode we are in control of what goes into the textbox. So suppress
                if(e.preventDefault) e.preventDefault();
            } 
            else
            { 
                
                if (!this._getPickerBehavior() || !this._getPickerBehavior()._willHandleKey(e.charCode, e.keyCode))
                {
                    this._mode = this.freeformMode;
                    this._enablePicker(false);
                    this._handleFreeFormTextInput(inputBox, e);
                }
            }
        } 
        else if(this._mode === this.arithmeticMode)
        {            
            // No 'y', 'm', 'w' or 'd' characters should be present
            if(text.indexOf(NhsDateResources.YearsUnit) === -1 &&
                     text.indexOf(NhsDateResources.MonthsUnit) === -1 && 
                     text.indexOf(NhsDateResources.DaysUnit) === -1 && 
                     text.indexOf(NhsDateResources.WeeksUnit) === -1)
            {
                if(e.charCode >= 48 && e.charCode <= 57)
                {
                    // Only allow 2 digits max
                    if( ((text.charAt(0) === '+' || text.charAt(0) === '-') && 
                        text.length <= 2) || text.length <= 1)
                    {
                        this._setText(text + character);
                    }
                } 
                else if((character.toUpperCase() === NhsDateResources.YearsUnit.toUpperCase() || 
                            character.toUpperCase() === NhsDateResources.MonthsUnit.toUpperCase() || 
                            character.toUpperCase() === NhsDateResources.WeeksUnit.toUpperCase() || 
                            character.toUpperCase() === NhsDateResources.DaysUnit.toUpperCase()))
                {
                    // Make sure there's at least 1 digit before we allow the 'y', 'm', 'w' or 'd'
                    if( ((text.charAt(0) !== '+' && text.charAt(0) !== '-') && 
                        text.length >= 1) || text.length >= 2)
                    {
                        this._setText(text + character);
                    }
                } 
                else if((character === '+' || character === '-') && text.length === 0)
                {
                    // We get here if the user has entered arithmetic mode, then deleted all text
                    this._setText(character);
                }
            }
            if(e.charCode === Sys.UI.Key["del"] || e.charCode === Sys.UI.Key["backspace"])
            {
                // Allow delete and backspace if in arithmetic mode
                return;
            }
            
            // Under this mode we are in control of what goes into the textbox. So suppress
            if(e.preventDefault) e.preventDefault();
        }
        
        // Handles key freezing case if we are in nullIndex mode
        if(!this._inputValidConsideringSpecialString(inputBox, character))
        {
            // Allow month entry in null strings mode.
            if (this._validMonth(inputBox, character))
                this._mode = this.freeformMode;
                     
            if(e.preventDefault) e.preventDefault();    
        }
    },

    //Returns true if the passed in InputBox is empty or all the text is selected. This is used when checking if a character entered as paret
    //of a keypress is at the beginning of the box
    _nextCharEnteredWillBeFirst : function(inputBox)
    {
        if(inputBox.value.length === 0)
        {
            return true;
        }
        else
        {
            var textSelectionPos = this._getSelectionPos(inputBox);
            
            return (textSelectionPos.start === 0);
        }
    },
    
    _pastehandler : function() {
    this._enablePicker(false);
    },
    
    _validMonth : function(inputBox, character) {
         var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;        
         var newEntry = inputBox.value.substring(0, this._getSelectionPos(inputBox).start) + character;
         var matches = new Array();
         this._appendStartsWithMatches(dtf.MonthNames, newEntry, matches);
         if (matches.length > 0)
         {            
            inputBox.value = newEntry;
            this._selectRange(inputBox, inputBox.value.length, inputBox.value.length);
            return true;
         }
         
         return false;
    }, 
    
    _inputValidConsideringSpecialString : function(inputBox, character) {
        var startIndex = this._getSelectionPos(inputBox).start;
        var newEntry = inputBox.value.substring(0, startIndex);         
        var matches = new Array(); 
        
        // return true for empty string
        if (inputBox.value.length == 0 || startIndex == 0)
            return true;                                    
            
        // check if string behind character is part of null string.
        this._appendStartsWithMatches(this._nullStrings, newEntry, matches);
        
        // Control contains a part of null string. Current input should be matching.
        if (matches.length > 0)
        {
            newEntry = inputBox.value.substring(0, startIndex) + character;
            var newMatches = new Array(); 
            this._appendStartsWithMatches(this._nullStrings, newEntry, newMatches);  
            if (newMatches.length > 0)
                return true;
            return false;                
        }
        else
            return true;     
    },
        
    _checkFreeFormAssistedEntry : function(inputBox, character) {
            
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
      
    _selectRange : function(inputBox, start, end) {
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
            range.moveEnd('character', end);
            range.select();
        }
    },
    _getSelectionPos : function(inputBox) {
        // Get selection start/end values
        if (inputBox.setSelectionRange)
        {
            // Gecko & Safari
            return {start:inputBox.selectionStart, end:inputBox.selectionEnd};
        }
        else if(document.selection && document.selection.createRange)
        {
            // IE
            inputBox.focus();
            return {
                    start:Math.abs(document.selection.createRange().moveStart("character", -1000000)),
                    end:Math.abs(document.selection.createRange().moveEnd("character", -1000000))
                   };
        }
    },    
    _checkFreeFormAssistedEntryTrigger : function(inputBox, character) {
        if(character === undefined) return false;
        
        //do not trigger FreeFormAssistedEntry if the inoutbox is not empty
        if (!this._nextCharEnteredWillBeFirst(inputBox))
        {
            return false;
        }
        // Check for free-form assisted entry (incl null times)
        var matches = new Array();               
        this._appendStartsWithMatches(this._nullStrings, character, matches);
        if(matches.length > 0 && this._mode !== this.assistedFreeformMode) 
        {
            this._mode = this.nullEntryMode;
        }
        
        if(matches.length > 0)
        {
			inputBox.value = matches[0];
            this._selectRange(inputBox, 
                            (matches.length == 1 ? inputBox.value.length : 1), 
                            inputBox.value.length);

            return true;
        }
        
        return false;
    },
    _appendStartsWithMatches : function(values, item, matches) {
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
    _handleFreeFormTextInput : function(inputBox, e) {
        
    },
    _finishEditing : function(inputBox)
    {
    /// <summary>Something has caused this editing session to finish e.g. Moving Focus elsewhere</summary>
    /// <param name="inputBox">The HTML TextBox that is the DateInputBox</param>
       if(this._inFinishEditing)
       {
            // recursion can occur if the control loses focus
            // during ambiguous date event
            return;
       }              
       
       var validatorAttached = false; // take the default position that a validator will not be attached
       this._inFinishEditing = true;
         
        // In current design there is no direct way to identify null string in base class.
        this._valueIsNullString = false;
              
       try
       {
            var text = this._getText().trim();
            var newDate = null;
            var functionality = this.get_functionality();
            
            if (text.length===0){
                // User has left the the date input as empty
                this.set_dateType(DateType.Null);
            }
            else{
                //If using complex functionality try parsing as an "Add Instruction" or as a Null String
                if(functionality === DateFunctionality.Complex)
                {
                   if(NhsDate.isAddInstruction(text))
                   {
                       newDate = this.get_value();
                       newDate.add(text);                
                   }
                   else
                   {       
                        var nullIndex = this._findNullIndex(text);
                        if(nullIndex >= 0)
                        {
                            newDate = new NhsDate();
                            newDate.set_dateType(DateType.NullIndex);
                            newDate.set_nullIndex(nullIndex);
                            this._valueIsNullString = true;
                            
                            // Refresh the status of validator. Previous value might be an invalid one.
                            NhsCuiValidation.FireAllAttachedValidators(this.get_element()); 
                        }
                    }
                }//functionality === DateFunctionality.Complex
                
                if(!newDate || newDate==null)
                {
                    //No date as a result of Add or Null Index parsing so try it as a valid date
                    var isValidDate;
                    var nhsCuiDateInputBoxValidator = NhsCuiValidation.GetAttachedValidatorOfSpecificType(this.get_element(), "NhsCui.DateInputBoxValidator");
                    
                    if (nhsCuiDateInputBoxValidator!==null){
                        //There is an NhsCui.DateInputBoxValidator attached use that to perform the validation 
                        validatorAttached = true;
                        
                        //Ask the validator to validate
                        ValidatorValidate(nhsCuiDateInputBoxValidator);
                        isValidDate = nhsCuiDateInputBoxValidator.isvalid;
                    }
                    else{
                        // No client side NhsCui validator was found
                        var testDate = NhsDate.tryParse(text);
                        
                        if(testDate){
                            //We have a valid date, now check it is within our bounds
                            if (testDate.get_dateType() === DateType.Exact || 
                                    testDate.get_dateType() === DateType.Approximate){
                                
                                //Check date bounds
                            
                                if (testDate.get_dateValue().getTime() < NhsCui.Toolkit.Web.DateInputBox.MinDateValue ||
                                                testDate.get_dateValue().getTime() > NhsCui.Toolkit.Web.DateInputBox.MaxDateValue){
                                    isValidDate = false;
                                }
                                else{
                                    isValidDate = true;
                                }
                            }
                            else if(testDate.get_dateType() === DateType.YearMonth || 
                                                    testDate.get_dateType() === DateType.Year){
                                 // Check year bounds
                            
                                if (testDate.get_year() < new Date(NhsCui.Toolkit.Web.DateInputBox.MinDateValue).getFullYear() ||
                                                testDate.get_year() > new Date(NhsCui.Toolkit.Web.DateInputBox.MaxDateValue).getFullYear()){
                                    isValidDate = false;
                                }
                                else{
                                    isValidDate = true;
                                }                   
                            }
                            else{
                                isValidDate = true;
                            }
                        }
                    }
                    
                    if(isValidDate)
                    {                
                        newDate = NhsDate.tryParse(text);
                        
                        if(newDate.get_dateType() === DateType.Exact && 
                                functionality === DateFunctionality.Complex &&
                                this.approximateControl && this.approximateControl.childNodes[0].checked)
                        {
                            newDate.set_dateType(DateType.Approximate);
                        }
                        
                        if(newDate.get_dateType() === DateType.Exact || 
                                    newDate.get_dateType() === DateType.Approximate)
                        {
                            var dateValue = newDate.get_dateValue();
                            // date is considered ambiguous if the day and month can be swapped
                            // to produce a valid date and the month name hasn't been
                            // specified
                            if(dateValue.getDate() <= 12 && 
                                dateValue.getDate() !== dateValue.getMonth() + 1 &&
                                !this._containsMonth(Sys.CultureInfo.CurrentCulture, text))
                            {
                               var selectedDate = this._fireAmbiguousDate(inputBox, newDate);
                               
                               newDate.set_dateValue(selectedDate);
                            }
                        }
                    }
                }//!newDate || newDate==null
                
                if(newDate && (functionality === DateFunctionality.Complex ||
                                                newDate.get_dateType() === DateType.Exact)){
                    this.set_value(newDate);
                }
                else
                {
                    //Parsing and validation has not allowed us to come up with a valid date
                    if (validatorAttached===true || this.get_element().parentNode.valAttachedServerSide===true){
                        //Leave the TextBox as it is but set the internal value to Null
                        this._setDateTypeToNullOnError();
                    }
                    else{
                        this._updateHtmlElementsToDateValue();
                    }
                }
            }//text.length===0
       }
       catch(e)
       {
            // Parsing and/or validation threw up an unhandled exception. 
            
            //Only set the elements back to a valid date if no validator is attached
            
            if (validatorAttached===true || this.get_element().parentNode.valAttachedServerSide===true){
                //Leave Text box in the state it is but set the internal value to Null
                this._setDateTypeToNullOnError();
            }
            else{
                this._updateHtmlElementsToDateValue();
            }
       }
       
       // Switch back to freeform
       this._mode = this.freeformMode;
       this._inFinishEditing = false;
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
    _containsMonth : function(culture, value) {
        var lowerValue = value.toLowerCase();
        var monthNames = [];
        Array.addRange(monthNames, culture.dateTimeFormat.MonthNames);
        Array.addRange(monthNames, culture.dateTimeFormat.AbbreviatedMonthNames);
        
        for(var i = 0; i < monthNames.length; i++)
        {
            if(monthNames[i].length > 0 && lowerValue.indexOf(monthNames[i].toLowerCase()) >= 0)
            {
                return true;
            }
        }
        return false;
    }        
};

NhsCui.Toolkit.Web.DateInputBox.registerClass("NhsCui.Toolkit.Web.DateInputBox", AjaxControlToolkit.BehaviorBase);

//This replacement of the Atlas 1.0 RTM function _getAbbrMonthIndex is due to a bug in Atlas
//This bug has been logged in their ProductStudio DB #61157

Sys.CultureInfo.prototype._getAbbrMonthIndex = function(value)
{
    if (!this._upperAbbrMonths)
    {
        this._upperAbbrMonths = this._toUpperArray(this.dateTimeFormat.AbbreviatedMonthNames);
    }
        
    return Array.indexOf(this._upperAbbrMonths, this._toUpper(value));    
};
