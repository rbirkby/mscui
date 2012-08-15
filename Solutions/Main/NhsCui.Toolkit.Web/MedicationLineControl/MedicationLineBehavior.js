//-----------------------------------------------------------------------
// <copyright file="MedicationLineBehavior.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Feb-2007</date>
// <summary>Script to create a client-side version of the Medication Line Control. Given a parent control, it will add the required children and set the data using a similar Medication JSON data</summary>
//-----------------------------------------------------------------------

Type.registerNamespace('NhsCui.Toolkit.Web');


/// <summary>
/// The MedicationStatus enum is used to define the Status of the MedicationLien
/// </summary>
NhsCui.Toolkit.Web.MedicationStatus = function() {
    /// <field name="Active" type="Number" integer="true">
    /// Medication is active
    /// </field>
    /// <field name="Suspended" type="Number" integer="true">
    /// Medication is suspended
    /// them to the available space.
    /// </field>
    throw Error.invalidOperation();
};

NhsCui.Toolkit.Web.MedicationStatus.prototype = {
    Active : 0,
    Suspended : 1        
};

NhsCui.Toolkit.Web.MedicationStatus.registerEnum("NhsCui.Toolkit.Web.MedicationStatus", false);

/// <summary>
/// Script to create a client-side version of the Medication Line Control.  Given a parent control, it will add the required children and set the data using a similar Medication JSON data
// </summary>
NhsCui.Toolkit.Web.MedicationLineBehavior = function(element) { 
    NhsCui.Toolkit.Web.MedicationLineBehavior.initializeBase(this, [element]);
    this._showDosageDetails = true;
    this._showGraphics = false;
    this._showStatusDate = false;
    this._showReason = true;
    this._showStatus = true;
    this._simpleMode = false;
    this._medication = null;   
    this._isSelected = false;                
    this._row = null;        
    this._clickPostBack = false;
    this._dblclickPostBack = false;        
    this._callbackID = null;
        
    // Handlers for the events
    this._onClickHandler = null;
    this._onDoubleClickHandler = null;
    this._onRightClickHandler = null;           
 
};

NhsCui.Toolkit.Web.MedicationLineBehavior.maxDisplayLength = 512;
NhsCui.Toolkit.Web.MedicationLineBehavior.ellipsis = '...';

NhsCui.Toolkit.Web.MedicationLineBehavior.prototype = {                                           
     
    /// <summary>
    /// Initialize
    /// </summary>
    initialize : function() {       
        NhsCui.Toolkit.Web.MedicationLineBehavior.callBaseMethod(this, 'initialize');                                 
        this._onClickHandler = Function.createDelegate(this, this._onClick);
        this._onRightClickHandler = Function.createDelegate(this, this._onRightClick);
        this._onDoubleClickHandler = Function.createDelegate(this, this._onDoubleClick);        
        
        var element = this.get_element();
        
        if (element.tagName === "TABLE" && element.rows.length > 0)
        {
            this._row = element.rows[0];
        }
        
        $addHandler(element, 'click', this._onClickHandler);
        $addHandler(element, 'dblclick', this._onDoubleClickHandler);
        $addHandler(element, 'contextmenu', this._onRightClickHandler);
    },
    
    /// <summary>
    /// Clean up
    /// </summary>
    dispose : function() {        
        if (this._medlabelBehavior)
        {
            this._medlabelBehavior.dispose();
            this._medlabelBehavior = null;
        }
        
        if (this._onClickHandler) {
            if (this.get_element())
            {
                $removeHandler(this.get_element(), 'click', this._onClickHandler);
            }
            this._onClickHandler = null;
        }
        
        if (this._onDoubleClickHandler) {
            if (this.get_element())
            {
                $removeHandler(this.get_element(), 'dblclick', this._onDoubleClickHandler);
            }
            this._onDoubleClickHandler = null;
        }        

        if (this._onRightClickHandler) {
            if (this.get_element())
            {
                $removeHandler(this.get_element(), 'contextmenu', this._onRightClickHandler);
            }
            this._onRightClickHandler = null;
        } 
        
        this._onClick = null;
        this._onDoubleClick = null;
        this._onRightClick = null;               
        
        NhsCui.Toolkit.Web.MedicationLineBehavior.callBaseMethod(this, 'dispose');
    },
    
    /// <summary>
    /// Get/Set State
    /// </summary>                               
    _getState : function() {
        if(!this._state)
        {
            this._state = NhsCui.Toolkit.Web.MedicationLineBehavior.callBaseMethod(this, 'get_ClientState');
        }
        
        return this._state;
    }, 
    _saveState : function() {
        this._state = this.get_isSelected();
        NhsCui.Toolkit.Web.MedicationLineBehavior.callBaseMethod(this, 'set_ClientState', [this._state]);
    },
        
    /// <summary>    
    /// Get Show Dosage Details
    /// </summary>        
   get_showDosageDetails : function() {
        return this._showDosageDetails;
    },
    
    /// <summary>
    /// Set Show Dosage Details
    /// </summary>            
    set_showDosageDetails : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._showDosageDetails = value;    
        
        if (this.get_isInitialized())
        {        
            if (value)
            {
                this._simpleMode = false;
            }    
        
            this.update();
        }
        this.raisePropertyChanged('ShowDosageDetails');
    },
    
    /// <summary>
    /// Get ShowGraphics flag
    /// </summary>        
   get_showGraphics : function() {
        return this._showGraphics;
    },
    
    /// <summary>
    /// Set ShowGraphics flag
    /// </summary>            
    set_showGraphics : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
                        
        this._showGraphics = value;     
        if (this.get_isInitialized())
        {                
            if (value)
            {
                this._simpleMode = false;
            }    
               
            this.update(); 
        }          
        this.raisePropertyChanged('ShowGraphics');
    },    

    /// <summary>
    /// Get ShowStatusDate flag
    /// </summary>        
   get_showStatusDate : function() {
        return this._showStatusDate;
    },
    
    /// <summary>
    /// Set ShowStatusDate flag
    /// </summary>            
    set_showStatusDate : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._showStatusDate = value;        
        
        if (this.get_isInitialized())
        {                            
            if (value)
            {
                this._simpleMode = false;
            }    
            
            this.update();
        }
        
        this.raisePropertyChanged('ShowStatusDate');
    },  
      
    /// <summary>
    /// Get ShowReason flag
    /// </summary>        
   get_showReason : function() {
        return this._showReason;
    },
    
    /// <summary>
    /// Set ShowReason flag
    /// </summary>            
    set_showReason : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._showReason = value; 
        
        if (this.get_isInitialized())
        {                                    
            if (value)
            {
                this._simpleMode = false;
            }    
                  
            this.update();               
        }
        this.raisePropertyChanged('ShowReason');
    },  
 
    /// <summary>
    /// Get ShowStatus flag
    /// </summary>        
   get_showStatus : function() {
        return this._showStatus;
    },
    
    /// <summary>
    /// Set ShowStatus flag
    /// </summary>            
    set_showStatus : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._showStatus = value;        
        
        if (this.get_isInitialized())
        {                                            
            if (value)
            {
                this._simpleMode = false;
            }    
            
            this.update();
        }
        this.raisePropertyChanged('ShowStatus');
    },     
    
    /// <summary>
    /// Get SimpleMode flag
    /// </summary>        
   get_simpleMode : function() {
        return this._simpleMode;
    },
    
    /// <summary>
    /// Set SimpleMode flag
    /// </summary>            
    set_simpleMode : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._simpleMode = value;     
        
        if (this.get_isInitialized())
        {                                                            
            if (this._simpleMode === true)
            {
                // If Simple Mode, disable all other flags
                this._showDosageDetails = false;
                this._showGraphics = false;
                this._showStatusDate = false;
                this._showReason = false;
                this._showStatus = false;        
            }   
            
            this.update();
        }
        
        this.raisePropertyChanged('SimpleMode');
    },  

    /// <summary>
    /// Get IsSelected flag
    /// </summary>        
   get_isSelected: function() {
        return this._isSelected;
    },
    
    /// <summary>
    /// Set IsSelected flag
    /// </summary>            
    set_isSelected : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
        
        if (this._isSelected != value)
        {        
            this._isSelected = value;                 
            var element = this.get_element();
            element.setAttribute('isSelected', this._isSelected.toString());
        
            if (this._isSelected === true)
            {
                Sys.UI.DomElement.addCssClass(this._row, "nhscui_ml_selected");                            
            }
            else
            {            
                Sys.UI.DomElement.removeCssClass(this._row, "nhscui_ml_selected");
            }                                                       
            
            this._saveState();
            
            this.raisePropertyChanged('IsSelected');
        }            
    },  

    /// <value type="String">
    /// ID of the ClientCallBack
    /// </value>    
    get_callbackID : function() {
        return this._callbackID;
    },
    set_callbackID : function(value) {
        this._callbackID = value;
    },
    
    /// <summary>
    //  Get/Set if Click Handler on server side has been defined
    /// </summary>
    get_clickPostBack: function() {
        return this._clickPostBack;
    },    
    set_clickPostBack : function(value) {
        this._clickPostBack = value;
    },    

    /// <summary>
    //  Get/Set if Double Click Handler on server side has been defined
    /// </summary>
    get_dblclickPostBack: function() {
        return this._dblclickPostBack;
    },    
    set_dblclickPostBack: function(value) {
        this._dblclickPostBack = value;
    },    

    /// <summary>
    //  register / deregister for click event
    /// </summary>
    add_click: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
                
   	    this.get_events().addHandler('click', handler);
    },
    remove_click: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
    	this.get_events().removeHandler('click', handler);
    },          
   _onClick: function(e) {
        var onclickHandler = this.get_events().getHandler("click");
        if (onclickHandler) {
            onclickHandler(this, Sys.EventArgs.Empty);
        }                
        
        // Do Postback
        if (this._clickPostBack)
        {
            var args = '0';                 // 0 indicates Click Event
            var id = this._callbackID;

            __doPostBack(id, args);
        }             
    },
          
    
    /// <summary>
    //  register / deregister for double click event
    /// </summary>
    add_doubleClick: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
                
   	    this.get_events().addHandler('doubleclick', handler);
    },    
    remove_doubleClick: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
    	this.get_events().removeHandler('doubleclick', handler);
    }, 
    _onDoubleClick: function() {
        var ondoubleclickHandler = this.get_events().getHandler("doubleclick");
        if (ondoubleclickHandler) {
            ondoubleclickHandler(this, Sys.EventArgs.Empty);
        }
        
        // Do Postback
        if (this._dblclickPostBack)
        {
            var args = '1';                 // 1 indicates Double Click Event
            var id = this._callbackID;

            __doPostBack(id, args);
        }                     
    },         
    
    /// <summary>
    //  register / deregister for right click event
    /// </summary>
    add_rightClick: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
                
   	    this.get_events().addHandler('rightclick', handler);
    },    
    remove_rightClick: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
    	this.get_events().removeHandler('rightclick', handler);
    }, 
    _onRightClick : function(e) {
        var onrightclickHandler = this.get_events().getHandler("rightclick");
        if (onrightclickHandler) {
            e.stopPropagation();
            e.preventDefault();        
            onrightclickHandler(this, Sys.EventArgs.Empty);
        }
    },        
    
    _onError : function(message, context) {
        /// <summary>
        /// Error handler for the callback
        /// </summary>
        /// <param name="message" type="String">
        /// Error message
        /// </param>
        /// <param name="context" type="Object">
        /// Context
        /// </param>
        alert('An unhandled exception has occurred:\n' + message);
    },
    
    _receiveServerData : function(arg, context) {
        /// <summary>
        /// Handler for successful return from callback
        /// </summary>
        /// <param name="arg" type="Object">
        /// Argument
        /// </param>
        /// <param name="context" type="Object">
        /// Context
        /// </param>
        context._waitingMode(false);
        context.raiseEndClientCallback(arg);
    },
        
    /// <summary>
    //  Update control based on show flags
    /// </summary>
    update: function()
    { 
        // If not completely initialized, do not update        
        var element = this.get_element();
        
        // Get DosageDetails spacer        
        var dosageSpacer = $get(element.id + "_dosspc");
        this._showElement(dosageSpacer, this._showDosageDetails);        
        
        // Get DosageDetails span        
        var dosageText = $get(element.id + "_dostxt");
        this._showElement(dosageText, this._showDosageDetails);
        
        // Get Graphics Cells
        var indicatorCell = $get(element.id + "_indicatorCell");
        this._showElement(indicatorCell, this._showGraphics);

        var criticalCell = $get(element.id + "_criticalCell");
        this._showElement(criticalCell, this._showGraphics);
        
        // Get Status Cell        
        if (!this._showStatus && !this._showStatusDate)        
        {
            var statusCell = $get(element.id + "_stsCell");
            this._showElement(statusCell, false);
        }
        else
        {        
            var statusCell = $get(element.id + "_stsCell");
            this._showElement(statusCell, true);
                    
            // Get StatusDate Span
            var statusDateSpan = $get(element.id + "_statusDate");
            this._showElement(statusDateSpan, this._showStatusDate);

            // Get Status Spacer  
            var statusSpacer = $get(element.id + "_stsspc");
            this._showElement(statusSpacer, (this._showStatus && this._showStatusDate));
            
            // Get Status Span  
            var statusSpan = $get(element.id + "_ststxt");
            this._showElement(statusSpan, this._showStatus);
        }               
         
        // Get Reason Cell  
        var reasonCell = $get(element.id + "_rsCell");
        this._showElement(reasonCell, this._showReason);
    } ,   
    
    /// <summary>
    /// Show or a hide an element
    /// </summary>    
    _showElement: function(element, show)
    {
        if (element)
        {
            element.style.display = show ? '' : 'none';           
        }        
    }        
};

var MedicationLineBehaviorResources = 
{
    MedicationLineBehaviorExceptionMessages : {
        DisplayLengthExceeded: "{0} display length of {1} exceeds limit of {2} characters.",        
        InvalidMedicationType: "Medication must be of type object",
        CannotBeEmpty:"Parameter must be defined.",                   
        InvalidArgument:"Invalid Argument",        
        InvalidStatus: "Invalid Status"       
    }
};


NhsCui.Toolkit.Web.MedicationLineBehavior.registerClass('NhsCui.Toolkit.Web.MedicationLineBehavior', AjaxControlToolkit.BehaviorBase);

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

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();