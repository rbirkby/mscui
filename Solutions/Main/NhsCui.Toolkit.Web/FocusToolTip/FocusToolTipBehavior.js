//-----------------------------------------------------------------------
// <copyright file="FocusToolTipBehavior.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>extender to show tooltip for associated element when it 
// receives the input focus</summary>
//-----------------------------------------------------------------------
Type.registerNamespace('NhsCui.Toolkit.Web');

//
// Extender to show tooltip for associated element when it 
// receives the input focus
//
NhsCui.Toolkit.Web.FocusToolTipBehavior = function(element) { 
    NhsCui.Toolkit.Web.FocusToolTipBehavior.initializeBase(this, [element]);

    this._toolTip = null;
    this._showToolTipTimeOutId = null;
    this._isMouseOver = false;
};

NhsCui.Toolkit.Web.FocusToolTipBehavior.prototype = {
    //
    // Initialize
    //
    initialize : function() {
        NhsCui.Toolkit.Web.FocusToolTipBehavior.callBaseMethod(this, 'initialize');
        
            var element = this.get_element();
            
            // create handlers for dom events
            this._onFocusDelegate = Function.createDelegate(this, this._onFocusHandler);
            this._onBlurDelegate = Function.createDelegate(this, this._onBlurHandler);
            this._onMouseOverDelegate = Function.createDelegate(this, this._onMouseOverHandler);
            this._onMouseOutDelegate = Function.createDelegate(this, this._onMouseOutHandler);
            this._onKeyDownDelegate = Function.createDelegate(this, this._onKeyDownHandler);
            
            // and for timeout delay before tooltip is shown
            this._showToolTipTimeOutDelegate = Function.createDelegate(this, this._showToolTipTimeOut);
            
            // handle dom events
            $addHandler(element, "focus", this._onFocusDelegate);        
            $addHandler(element, "blur", this._onBlurDelegate);        
            $addHandler(element, "mouseover", this._onMouseOverDelegate);        
            $addHandler(element, "mouseout", this._onMouseOutDelegate);        
            $addHandler(element, "keydown", this._onKeyDownDelegate);        
    },
    //
    // Clean-up
    //
    dispose : function() {
        
        if(this.get_isInitialized())
        {
            var element = this.get_element();
            
            $removeHandler(element, "focus", this._onFocusDelegate);        
            $removeHandler(element, "blur", this._onBlurDelegate);        
            $removeHandler(element, "mouseover", this._onMouseOverDelegate);        
            $removeHandler(element, "mouseout", this._onMouseOutDelegate);        
            $removeHandler(element, "keydown", this._onKeyDownDelegate);        
        }
        
        NhsCui.Toolkit.Web.FocusToolTipBehavior.callBaseMethod(this, 'dispose');
    },
    //
    // Css class to override default styling
    //
    get_CssClass : function() {    
        return this._cssClass;        
    },
    set_CssClass : function(value) {        
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }

        this._cssClass = value;        
        this.raisePropertyChanged('CssClass');
    },
    //
    // The id of the control whose title (or alt) attribute supplies the tooltip text
    // can be left unset if same as target control
    //
    get_ToolTipSourceControlID : function() {    
        return this._toolTipSourceControlID;        
    },
    set_ToolTipSourceControlID : function(value) {        
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }

        this._toolTipSourceControlID = value;        
        this.raisePropertyChanged('ToolTipSourceControlID');
    },
    //
    // Handle focus event on target element
    //
    _onFocusHandler : function() {
        if(this._showToolTipTimeOutId === null && !this._isMouseOver)
        {
           this._showToolTipTimeOutId = window.setTimeout(this._showToolTipTimeOutDelegate, 500);
        }
    },
    //
    // Handle blur event on target element
    //
    _onBlurHandler : function() {
        this._hideToolTip();
    },
    //
    // Handle mouse over event on target element
    //
    _onMouseOverHandler : function() {
        this._isMouseOver = true;
        this._hideToolTip();
    },
    //
    // Handle mouse out event on target element
    //
    _onMouseOutHandler : function() {
        this._isMouseOver = false;
    },
    //
    // Handle key down event on target element
    //
    _onKeyDownHandler : function() {
        this._hideToolTip();
    },
    //
    // Handle show tooltip timeout call
    //
    _showToolTipTimeOut: function() {
        this._showToolTipTimeOutId = null;
        if(!this._isMouseOver)
        {
            this._showToolTip();
        }
    },
    //
    // show tooltip
    //
    _showToolTip : function() {
       var element = this.get_element();
       var toolTipSourceControlId = this.get_ToolTipSourceControlID();
       var toolTipSourceControl = (toolTipSourceControlId ? $get(toolTipSourceControlId) : element);
       
       // get tooltip text from title or alt attribute
       var text = (toolTipSourceControl.title ? toolTipSourceControl.title : toolTipSourceControl.alt);
       
       if(typeof(text) === "string" && text.length > 0)
       {

            if(!this._toolTip)
            {
                this._toolTip = document.createElement("DIV");
                document.body.appendChild(this._toolTip);
            }
            
            if(this._toolTip.firstChild)
            {
                this._toolTip.removeChild(this._toolTip.firstChild);
            }
            
            this._toolTip.appendChild(document.createTextNode(text));
            
            // style tooltip
            this._applyToolTipSyle(this._toolTip);
            
            // set location
            var targetBounds = Sys.UI.DomElement.getBounds(element);
            Sys.UI.DomElement.setLocation(this._toolTip, targetBounds.x, targetBounds.y + targetBounds.height);
            
            // ensure visible
            this._toolTip.style.display = "";
        }
    },
    //
    // style tooltip
    //
    _applyToolTipSyle : function(toolTip) {
	    var cssClass = this.get_CssClass();

	    if(typeof(cssClass) === "string" && cssClass.length > 0)
	    {
	        toolTip.className = cssClass;
        }
        else
        {
            // apply default style
            toolTip.style.backgroundColor = "LemonChiffon";
            toolTip.style.border = "1px solid black";
            toolTip.style.lineHeight = "95%";
            toolTip.style.fontSize = "8pt";
            toolTip.style.padding = "3px";
            toolTip.style.zIndex = 99;
       }
    },
    //
    // hide tooltip
    //
    _hideToolTip : function() {
        // if we are about to show the tooltip, cancel it
        if(this._showToolTipTimeOutId !== null)
        {
            window.clearTimeout(this._showToolTipTimeOutId);
            this._showToolTipTimeOutId = null;
        }
        
        if(this._toolTip)
        {
            this._toolTip.style.display = "none";
        }
    }
};

NhsCui.Toolkit.Web.FocusToolTipBehavior.registerClass('NhsCui.Toolkit.Web.FocusToolTipBehavior', Sys.UI.Behavior);
