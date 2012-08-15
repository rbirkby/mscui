//-------------------------------------------------------------------------------------------------------------
// <copyright file="MedicationGridBehavior.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Feb-2007</date>
// <summary>Script to create a client-side version of the Medication Grid Control. </summary>
//-------------------------------------------------------------------------------------------------------------
Type.registerNamespace("NhsCui.Toolkit.Web");


/// <summary>
/// The MedicationGridColumn enum is used to enumerate the MedicationGrid Columns
/// </summary>
NhsCui.Toolkit.Web.MedicationGridColumn = function() {
    /// <field name="StartDate" type="Number" integer="true">
    /// StartDate Column
    /// </field>
    /// <field name="DrugDetails" type="Number" integer="true">
    /// DrugDetails Column
    /// </field>
    /// <field name="Reason" type="Number" integer="true">
    /// Reason Column
    /// </field>
    /// <field name="Status" type="Number" integer="true">
    /// Status Column
    /// </field>    
    throw Error.invalidOperation();
};

NhsCui.Toolkit.Web.MedicationGridColumn.prototype = {
    StartDate : 0,
    DrugDetails : 1,
    Reason : 2,
    Status : 3
};

NhsCui.Toolkit.Web.MedicationGridColumn.registerEnum("NhsCui.Toolkit.Web.MedicationGridColumn", false);

NhsCui.Toolkit.Web.MedicationGridBehavior = function(element) {
    NhsCui.Toolkit.Web.MedicationGridBehavior.initializeBase(this, [element]);                    
    
    // Initialize Member Variables
    this._onScrollDelegate = null;    
    this._onLookAheadBehindItemClickDelegate = null;
    this._formSubmitDelegate = null;
    this._onMedicationLineClickDelegate = null;    
    this._onMedicationLineRightClickDelegate = null;
    this._onMedicationLineDoubleClickDelegate = null;
    this._onScrollDelegate = Function.createDelegate(this, this._onScroll);
    this._onLookAheadBehindItemClickDelegate = Function.createDelegate(this, this._onLookAheadBehindItemClick);
    this._formSubmitDelegate = Function.createDelegate(this, this._formSubmitHandler);
    this._onMedicationLineClickDelegate = Function.createDelegate(this, this._onMedicationLineClick);
    this._onMedicationLineRightClickDelegate = Function.createDelegate(this, this._onMedicationLineRightClick);
    this._onMedicationLineDoubleClickDelegate = Function.createDelegate(this, this._onMedicationLineDoubleClick);    
    this._onHeaderClickDelegate = Function.createDelegate(this, this._onHeaderClick);    
    this._keyDownDelegate = Function.createDelegate(this, this._keyDownHandler);    
    this._keyPressDelegate = Function.createDelegate(this, this._keyPressHandler);      
    this._focusDelegate = Function.createDelegate(this, this._focusHandler);      

    this._clickPostBack = false;
    this._dblclickPostBack = false;        
    this._callbackID = null;
    
    this._items = null;
    this._selectedItem = null;    
    this._selecteditems = null;
    
    // Property Vars
    this._showDosageDetails = true;
    this._showGraphics = false;
    this._showStatusDate = false;
    this._showLookAheadBehind = true;
    this._showReason = true;
    this._showStatus = true;        
    this._simpleMode = false;
    this._state = null;        
        
    // Control references    
    this._lookaheadID = null;
    this._lookaheadObj = null;
    this._lookaheadElement = null;
    this._lookbehindID = null;
    this._lookbehindObj = null;  
    this._lookbehindElement = null;          
    this._bodyDiv = null; 
    this._headerRowColumns = null;
    this._bodyColumns = null;
    this._bodyRowBounds = null;
        
    this._scrollTable = null;
    this._headerRowDiv = null;
    this._headerTable = null;
    this._bodyTable = null;
    this._tbody = null;
    this._bodyRows = null;            
    this._lastBottomItemIndex = -1;
    this._sortColumn = 0;
    this._sortAscending = true;
    
    // Is Vertical Scroll Bar visible
    this._scrollY = true;
    
    // Last Item Selected. Defaults to the row item so that on shift all items working from the top are selected
    this._lastSelectedItemIndex = 0;
    
    // Item with Focus
    this._focusItemIndex = 0;
    
    // Column Indices
    this._startDateHeaderColumn = 0;    
    this._dosageDetailsHeaderColumn = 1;
    this._reasonHeaderColumn = 2;
    this._statusHeaderColumn = 3;        
    
    this._startDateBodyColumn = 0;
    this._indicatorGraphicBodyColumn = 1;     
    this._criticalAlertGraphicBodyColumn = 1;     
    this._dosageDetailsBodyColumn = 3;
    this._reasonBodyColumn = 4;    
    this._statusBodyColumn = 5;    
        
    // Column Widths properties        
    this._startDateColumnWidth = 0;            
    this._drugDetailsColumnWidth = 0;            
    this._reasonColumnWidth = 0;        
    this._statusColumnWidth = 0;
    
    this._tableHeight = null;    
    this._bodyDivHeight = 0;  // Store the current BodyDiv offsetHeight to improve performance
    this._lastScrollTop = 0;  // used to determine direction of scroll
    this._scrollTop = 0;      // Store the current ScrollTop to improve performance
    
    this._scrollDir = 0;
    this._scrollTimer = null; // application scrolling generates a scroll event, this should be ignored
        
    this._ignoreNextScrollEvent = false;
};

NhsCui.Toolkit.Web.MedicationGridBehavior.prototype = {      
    ///<summary>
    /// Initialize Behavior
    ///</summary>    
    initialize : function() {
        var element = this.get_element();                   
        NhsCui.Toolkit.Web.MedicationGridBehavior.callBaseMethod(this, "initialize");                
        var form = this._getForm();
        
        if(form)
        {
            $addHandler(form, 'submit', this._formSubmitDelegate);
        }        
                
        $addHandlers(element, 
            {
                focus: this._focusDelegate,
                keypress : this._keyPressDelegate,
                keydown : this._keyDownDelegate
            });
                
        // Initialize Elements        
        this._initializeElements();
        
        if (element.offsetHeight !== 0)
        {                        
            // Match Header and Body Columns
            this._resizeTableColumns();	
              
            if (this._bodyRows && this._bodyRows.length > 0)
            {          	        	            
                this._getBodyRowBounds();
                this._setLookAheadBehindPosition();	            
            }	                                            
            
            // Display the Grid
            this._showGrid();              
            
            // Hide out of Bounds Items 
            this._hideOutOfBoundsItems();                                                         
        }

        // Set focus on first item
        this._setFocus(0);  
    },
       
    ///<summary>
    /// Dispose of any handlers
    ///</summary>        
    dispose : function() {
        var element = this.get_element();       
        
        if(this._bodyDiv)
        {
            $clearHandlers(this._bodyDiv);
        }
        
        this._onScrollDelegate = null;
        this._bodyDiv = null;
        this._onScroll = null;
        if (this._lookbehindObj)
        {
            if (this._onLookAheadBehindItemClickDelegate)
            {
                this._lookbehindObj.remove_itemClick(this._onLookAheadBehindItemClickDelegate);            
            }
            this._lookbehindObj.dispose();
            this._lookbehindObj = null;            
        }        
        
        if (this._lookaheadObj)
        {
            if (this._onLookAheadBehindItemClickDelegate)
            {
                this._lookaheadObj.remove_itemClick(this._onLookAheadBehindItemClickDelegate);            
            }
            this._lookaheadObj.dispose();            
        }                
        
        this._onLookAheadBehindItemClickDelegate = null;
        
        // Remove header click event handler
        if (this._onHeaderClickDelegate)
        {
            var headerRowCount = this._headerRowColumns.length;
            for (var headerRow =0; this._headerRowColumns < headerRowCount; headerRow++)
            {
                $removeHandler(this._headerRowColumns[headerRow], 'click', this._onHeaderClickDelegate);
            }
            
            this._onHeaderClickDelegate = null;
        }
                   
        // Remove MedicationLine click handlers                          
        if (this._bodyRows)
        {
            var bodyRowCount = this._bodyRows.length;
            for (var bodyRow =0; bodyRow < bodyRowCount; bodyRow++)
            {
                $clearHandlers(this._bodyRows[bodyRow]);                
            }
            
            this._bodyRows = null;
        }                              
                
        var form = this._getForm();
        if(form)
        {
            $removeHandler(form, 'submit', this._formSubmitDelegate);
        }           
        
        $common.removeHandlers(element, 
            {
                focus : this._focusDelegate,
                keypress : this._keyPressDelegate,
                keydown : this._keyDownDelegate
           });                                             
        
        this._onScrollDelegate = null;
        this._onMedicationLineClickDelegate = null;        
        this._onLookAheadBehindItemClickDelegate = null;
        this._formSubmitDelegate = null;
        this._onMedicationLineClickDelegate = null;
        this._onMedicationLineRightClickDelegate = null;
        this._onMedicationLineDoubleClickDelegate = null;        
        
        NhsCui.Toolkit.Web.MedicationGridBehavior.callBaseMethod(this, "dispose");        
    },
    
    
    /// <summary>
    //  Update control based on show flags
    /// </summary>
    update: function()
    {      
        if (this.get_element().offsetHeight !== 0)
        {  
            this._headerRowDiv.style.width = '';	    
            this._bodyDiv.style.width = '';	  
            this.get_element().style.width = '';
                          
            this._measureTableHeight();
            this._updateHeader();        
            this._updateRows();
            this._updateLookAheadBehind();                
            this._setDefaultWidths();
            // Remeasure the columns to see if word wrapping has forced the columns width            
            this._resizeCells();           
	        this._resizeScrollArea();	   
            this._getBodyRowBounds();	        
	        this._showGrid();	         	    
	        this._hideOutOfBoundsItems();	    	    
	        this._setLookAheadBehindPosition();        
	    }
    } ,   
        
    /// <summary>
    /// Add click handler for each of the MedicationLine's
    /// </summary>                 
    _initializeMedicationLines: function()
    {        
        if (this._bodyRows !== null)
        {
            var bodyRowCount = this._bodyRows.length;
            for (var bodyRow =0; bodyRow < bodyRowCount; bodyRow++)
            {
                $addHandler(this._bodyRows[bodyRow], 'click', this._onMedicationLineClickDelegate);
                $addHandler(this._bodyRows[bodyRow], 'dblclick', this._onMedicationLineDoubleClickDelegate);
                $addHandler(this._bodyRows[bodyRow], 'contextmenu', this._onMedicationLineRightClickDelegate);
            }
        }    
    },    
    
    /// <summary>
    /// Initialize Header
    /// </summary>                 
    _initializeHeader: function()
    {
        // Get array of header and first set of columns in the grid	    	    
        var headerRow = this._headerTable.rows[0];                
        this._headerRowColumns = headerRow.cells;    
        var headerRowCount = this._headerRowColumns.length;
        for (var headerRow =0; headerRow < headerRowCount; headerRow++)
        {
            $addHandler(this._headerRowColumns[headerRow], 'click', this._onHeaderClickDelegate);
        }                
    },
    
    /// <summary>
    /// Initialize Look Objects
    /// </summary>             
    _initializeLookAheadBehindObjects: function()
    {
        if (this._lookbehindElement !== null && this._lookbehindObj === null)
        {
            this._lookbehindObj = $create(NhsCui.Toolkit.Web.MedicationListView, {mode: 0}, null, null, this._lookbehindElement);            
            this._lookbehindObj.add_itemClick(this._onLookAheadBehindItemClickDelegate);            
        }                        

        if (this._lookaheadElement !== null && this._lookaheadObj === null)
        {
            this._lookaheadObj = $create(NhsCui.Toolkit.Web.MedicationListView, {mode: 1}, null, null, this._lookaheadElement);            
            this._lookaheadObj.add_itemClick(this._onLookAheadBehindItemClickDelegate);            
        }                                
    },        
              
    /// <summary>
    /// Initialize Elements
    /// </summary>            
    _initializeElements: function()
    {       
        var element = this.get_element();           
        this._bodyDiv = $get(element.id + "_bodyDiv", element);                        
        if (this._bodyDiv)
        {
            $addHandler(this._bodyDiv, 'scroll', this._onScrollDelegate);                            
            this._scrollTable = $get(element.id + "_scrollTable");                                          
                                    
	        this._headerRowDiv = this._getElementAt(this._scrollTable, 4);
	        this._headerTable = this._headerRowDiv.getElementsByTagName("TABLE")[0];    	    
	        this._bodyTable = this._bodyDiv.getElementsByTagName("TABLE")[0];
	        this._tbody = this._bodyTable.tBodies[0];
	        
	        if (typeof(this._tbody) !== "undefined" && this._tbody !== null)
	        {
	            this._bodyRows = this._tbody.rows;  	        
	        }
	        else
	        {
	            this._bodyRows = null;
	        }	        

            this._initializeHeader();                        
            
            if (this._bodyTable.rows.length > 0)
            {
                this._bodyColumns = this._bodyTable.rows[0].cells;	            
            }
            
            if (this._headerTable)
            {
                this._headerTable.style.borderCollapse = '';
            }
            
            if (this._bodyTable)
            {
                this._bodyTable.style.borderCollapse = '';
            }

            // Initialize LookAhead/LookBehind objects            
            this._initializeLookAheadBehindObjects(); 
            	        
	        // If rows have been defined, Initialize the MedicationLine event handler, and set the LookAhead/Behind to the start position
	        if (this._bodyRows && this._bodyRows.length > 0)
	        {          	        
	            this._initializeMedicationLines();
	        }	  
	        else
	        {
                this._lookbehindObj.set_visible(false);
                this._lookaheadObj.set_visible(false);	        
	        }  
	        
	        this._measureTableHeight();
		}          
    },
    
    
    /// <summary>
    /// Measure Elements
    /// </summary>            
    _measureTableHeight: function()
    {                            
       if (this.get_element().offsetHeight !== 0 && this._bodyDiv && this._tableHeight === null)
        {
            // For the time being using this hack to measure the height.
            // Currently IE6 doesn't measure the height correctly
            var hiddenDIV = document.createElement("DIV");
            document.body.appendChild(hiddenDIV);
            hiddenDIV.style.visibility = "hidden";
            hiddenDIV.style.height = this.get_element().style.height;                                      
            this._tableHeight = hiddenDIV.offsetHeight;
            document.body.removeChild(hiddenDIV);
            hiddenDIV = null;              
	    }     
	},         
          
    ///<summary>
    /// Get Selected Item (returns first if multiple selected)
    ///</summary>         
    get_selectedItem : function() {
        var selectedItems = this._getSelectedItems();
        
        if (selectedItems.length > 0)
        {
            return selectedItems[0];
        }
    },    
    
    ///<summary>
    /// Get Selected Items
    ///</summary>         
    get_selectedItems : function() {
        return this._getSelectedItems();
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
        }
        
        this.raisePropertyChanged('SimpleMode');
    },        

    /// <summary>
    /// Get ShowLookAheadBehind Flag
    /// </summary>        
    get_showLookAheadBehind : function() {
        return this._showLookAheadBehind;
    },
    
    /// <summary>
    /// Set ShowLookAheadBehind Flag
    /// </summary>            
    set_showLookAheadBehind : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean, mayBeNull: true}
                    ]);
        if (e)
        {
            throw e;
        }
                        
        this._showLookAheadBehind = value;        
        this.raisePropertyChanged('ShowLookAheadBehind');
    },  

    /// <summary>
    /// Get LookAhead Object
    /// </summary>        
   get_lookaheadObj: function() {        
        this._initializeLookAheadBehindObjects();
        return this._lookaheadObj;
    },
    
    /// <summary>
    /// Get LookAhead ID
    /// </summary>        
   get_lookaheadID : function() {        
        return this._lookaheadID;
    },        
    /// <summary>
    /// Set Look Ahead ID
    /// </summary>            
    set_lookaheadID : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String, mayBeNull: true}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._lookaheadID = value; 
        if (this._lookaheadID)
        {
            this._lookaheadElement = $get(this._lookaheadID, this.get_element());                            
        }
        this.raisePropertyChanged('LookAheadID');
    },  

    /// <summary>
    /// Get LookAhead Object
    /// </summary>        
   get_lookbehindObj: function() {        
        this._initializeLookAheadBehindObjects();
        return this._lookbehindObj;
    },

    /// <summary>
    /// Get LookAhead ID
    /// </summary>        
   get_lookbehindID : function() {   
        return this._lookbehindID;
    },    
    /// <summary>
    /// Set Look Ahead ID
    /// </summary>            
    set_lookbehindID : function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String, mayBeNull: true}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._lookbehindID = value;           
        if (this._lookbehindID)
        {
            this._lookbehindElement = $get(this._lookbehindID, this.get_element());                                    
        }
        this.raisePropertyChanged('LookBehindID');
    }, 
              
    /// <summary>
    /// Get/Set StartDate Column Width
    /// </summary>        
    get_startDateColumnWidth : function() {
        return this._startDateColumnWidth;
    },    
    set_startDateColumnWidth: function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._startDateColumnWidth = value;                       
        this.raisePropertyChanged('StartDateColumnWidth');
    },      

    /// <summary>
    /// Get/Set Reason Column Width
    /// </summary>        
    get_reasonColumnWidth : function() {
        return this._reasonColumnWidth;
    },    
    set_reasonColumnWidth: function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._reasonColumnWidth = value;                       
        this.raisePropertyChanged('ReasonColumnWidth');
    },  

    /// <summary>
    /// Get/Set Drug Details Column Width
    /// </summary>        
    get_drugDetailsColumnWidth : function() {
        return this._drugDetailsColumnWidth;
    },    
    set_drugDetailsColumnWidth: function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._drugDetailsColumnWidth = value;                       
        this.raisePropertyChanged('DrugDetailsColumnWidth');
    },  

    /// <summary>
    /// Get/Set Status Column Width
    /// </summary>        
    get_statusColumnWidth : function() {
        return this._statusColumnWidth;
    },    
    set_statusColumnWidth: function(value) {    
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String, mayBeNull: false}
                    ]);
        if (e)
        {
            throw e;
        }
            
        this._statusColumnWidth = value;                       
        this.raisePropertyChanged('StatusColumnWidth');
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
    _onClick: function(id) {
        var onclickHandler = this.get_events().getHandler("click");        
        var eventArg = new NhsCui.Toolkit.Web.MedicationEventArgs(id);
        if (onclickHandler) {
            onclickHandler(this, eventArg);
        }
        
        // Do Postback
        if (this._clickPostBack)
        {
            var args = '0,' + id.toString();                 // 0 indicates Click Event. Pass back the ID of the Medication
            id = this._callbackID;

            this._saveState();
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
    _onDoubleClick: function(id) {
        var ondoubleclickHandler = this.get_events().getHandler("doubleclick");
        var eventArg = new NhsCui.Toolkit.Web.MedicationEventArgs(id);
        
        if (ondoubleclickHandler) {
            ondoubleclickHandler(this, eventArg);
        }
        
        // Do Postback
        if (this._dblclickPostBack)
        {
            var args = '1,' + id.toString();                 // 1 indicates DoubleClick Event. Pass back the ID of the Medication
            id = this._callbackID;

            this._saveState();
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
    _onRightClick : function(id) {
        var onrightclickHandler = this.get_events().getHandler("rightclick");
        var eventArg = new NhsCui.Toolkit.Web.MedicationEventArgs(id);
        
        if (onrightclickHandler) {
            onrightclickHandler(this, eventArg);
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
    /// Get Form
    /// </summary>                                      
    _getForm : function() {
        var elem = this.get_element();
        
        while(elem && elem.nodeName !== "FORM")
        {
            elem = elem.parentNode;
        }
        return elem;
    },   


    /// <summary>
    /// Get medication by it's id
    /// </summary>        
   get_medication: function(medicationID) {
        var bodyRowIndex = 0;
        var bodyRowCount = this._bodyRows.length;                
        var match = null;
        
        while (bodyRowIndex < bodyRowCount && match === null)
        {        
            var bodyRow = this._bodyRows[bodyRowIndex];
            var rowID = bodyRow.getAttribute("medicationId");
            if (medicationID === rowID)
            {
                return this._extractMedication(bodyRowIndex, medicationID);
            }
            
            bodyRowIndex++;
        };             
        
        return null;               
    },

    /// <summary>
    /// Extract Medication 
    /// </summary>             
    _extractMedication: function(index, medicationID)
    {        
        var startDate = NhsDate.parse(this._getRowText(index, 0)).get_dateValue();
        var statusDate = NhsDate.parse(this._getStatusDate(index)).get_dateValue();
        var status = this._getStatus(index);           
        var medicationNames = this._getMedicationNames(index);
        var dosageText = this._getDosageText(index);
        var reason = this._getRowText(index, 4);
        return {
            StartDate: startDate,
            MedicationNames: medicationNames,
            DosageText: dosageText,
            MedicationID: medicationID,
            Reason: reason,
            StatusDate: statusDate,
            Status: status
            };                                    
    },
    
    /// <summary>
    /// On  Form-submit, Save the current state
    /// </summary>             
    _formSubmitHandler : function(sender, args) {
        this._saveState();
    },
       
         
    /// <summary>
    /// Get/Set State
    /// </summary>                               
    _getState : function() {
        if(!this._state)
        {
            this._state = NhsCui.Toolkit.Web.MedicationGridBehavior.callBaseMethod(this, 'get_ClientState');
        }
        
        return this._state;
    }, 
    _saveState : function() {
        this._state = this._getSelectedItems();
        NhsCui.Toolkit.Web.MedicationGridBehavior.callBaseMethod(this, 'set_ClientState', [this._state]);
    },
        
    
    /// <summary>
    /// Get Array of Selected Items
    /// </summary>                 
    _getSelectedItems: function()
    {                    
        var selectedItems = new Array();
        if (this._bodyRows)
        {
            var bodyRowCount = this._bodyRows.length;
            var item = 0;
            
            for (var bodyRow = 0; bodyRow < bodyRowCount; bodyRow++)
            {
                if (this._bodyRows[bodyRow].getAttribute("isSelected") === "true")
                {
                    selectedItems[item] = this._bodyRows[bodyRow].getAttribute("medicationId");
                    item ++;
                }
            }
        }
        
        return selectedItems;
    },
                            
    /// <summary>
    /// Get scroll direction +1 down, -1 up, 0 no change
    /// </summary>                 
    _getScrollDir: function(delta)
    {
        if (delta > 0)
        {
            return 1;
        }
        else if (delta < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }          
    },
    
    /// <summary>
    /// On Header Click
    /// </summary>                 
    _onHeaderClick: function(e)
    {
        if (e.target.parentNode)
        {
            var headerColumn = e.target.parentNode.cellIndex;                                    
            if (this._sortColumn == headerColumn)
            {
                this._sortAscending = !this._sortAscending;
            }
            
            this._sortColumn = headerColumn;            
            this._sortTable(headerColumn, this._sortAscending);
        }
    },
    
    /// <summary>
    /// On MedicationLine Click
    /// </summary>             
    _onMedicationLineClick: function(e)
    {
        var target = e.target;
        while (target.tagName != "TR")
        {
            target = target.parentNode;
        } 
                
        if (target)
        {                
            this._onItemSelected(target, e.ctrlKey, e.shiftKey);                    
            this._onClick(target.getAttribute("medicationId"));
        }                
    },    

    /// <summary>
    /// On MedicationLine Double Click
    /// </summary>             
    _onMedicationLineDoubleClick: function(e)
    {
        var target = e.target;
        while (target.tagName != "TR")
        {
            target = target.parentNode;
        } 
                
        if (target)
        {                
            this._onItemSelected(target, e.ctrlKey, e.shiftKey);                
            this._onDoubleClick(target.getAttribute("medicationId"));
        }            
    }, 
    
    /// <summary>
    /// On MedicationLine Right Click
    /// </summary>             
    _onMedicationLineRightClick: function(e)
    {
        var target = e.target;
        while (target.tagName != "TR")
        {
            target = target.parentNode;
        } 
                
        if (target)
        {                    
            e.stopPropagation();
            e.preventDefault();                
            this._onRightClick(target.getAttribute("medicationId"));
        }
    }, 
                   
    ///<summary>
    /// On Element Scroll Event Handler
    ///</summary>         
    _onScroll: function(e)            
    {                     
        if (this._scrollTimer !== null) {
            window.clearTimeout(this._scrollTimer);
            this._scrollTimer = null;
        } 
        
        this._scrollTop = this._bodyDiv.scrollTop;
                    
        if (this._ignoreNextScrollEvent === true)        
        {            
            this._ignoreNextScrollEvent = false;
            return;
        }
                       
        this._hideOutOfBoundsItems();                                       
        this._scrollTimer = window.setTimeout(Function.createDelegate(this, this._onScrollTimeout), 200);        
    },  
        
    /// <summary>
    /// Scroll Timed out
    /// </summary>                         
    _onScrollTimeout: function()
    {
        if (this._scrollTimer !== null) {
            window.clearTimeout(this._scrollTimer);
            this._scrollTimer = null;      
        }    
        
        var element = this.get_element();                                       
        var scrollHeight = this._bodyDiv.scrollHeight;                
        var topItemIndex = this._findNextVisibleElement(this._scrollTop); 
        
        this._scrollTop = this._bodyDiv.scrollTop;
        
        var scrollDelta = this._scrollTop - this._lastScrollTop;
        
        this._lastScrollTop = this._scrollTop;                                                
        this._scrollDir = this._getScrollDir(scrollDelta); 
                       
        if (this._scrollDir !== 0 && topItemIndex != null)
        {
            if (this._scrollDir === -1 && topItemIndex > 0)
            {
                topItemIndex -= 1;
                topItem = this._bodyRows[topItemIndex];
            }
            else
            {
                topItem = this._bodyRows[topItemIndex];
            }

            if (this._scrollTop != topItem.offsetTop)
            {
                // If Scrolled to bottom or top, another scroll event is fired, so should not be ignored
                if ((topItemIndex === 0 && this._scrollDir === -1) ||
                   (this._bodyDivHeight + topItem.offsetTop >= scrollHeight  && this._scrollDir === 1))
                {
                    this._ignoreNextScrollEvent = false;                                        
                }
                else
                {
                    this._ignoreNextScrollEvent = true;
                }
                
                this._bodyDiv.scrollTop = topItem.offsetTop + 1;   
                this._scrollTop = this._bodyDiv.scrollTop;    
            }                         
                        
            this._hideOutOfBoundsItems();            
            this._setLookAheadBehindPosition();
        }        
    },    

    /// <summary>
    /// Show all items
    /// </summary>                 
    _showAllItems: function()
    {
        var bodyRowCount = this._bodyRows.length;
        for (var bodyRowIndex = 0; bodyRowIndex < bodyRowCount; bodyRowIndex++)
        {
            var bodyRow = this._bodyRows[bodyRowIndex];
            bodyRow.style.visibility = "visible";
        }
    },  
      
    /// <summary>
    /// Hide all non-visible items
    /// </summary>                 
    _hideOutOfBoundsItems: function()
    {        
        if (this._bodyRows && this._bodyRows.length > 0 && this._scrollY)        
        {            
            var bodyRowCount = this._bodyRows.length;
            for (var bodyRowIndex = 0; bodyRowIndex < bodyRowCount; bodyRowIndex++)
            {                
                var bodyRow = this._bodyRows[bodyRowIndex];
                bodyRow.style.visibility = this._isItemByIndexFullyVisible(bodyRowIndex) ? "visible" : "hidden";
            }            
        }
    },

    /// <summary>
    /// Get First Visible Element
    /// </summary>                 
    _getFirstVisibleElementIndex: function()
    {
        var bodyRowCount = this._bodyRows.length;        
        for (var bodyRowIndex = 0; bodyRowIndex < bodyRowCount; bodyRowIndex++)
        {            
            //var bodyRow = this._bodyRows[bodyRowIndex];
            if (this._isItemByIndexFullyVisible(bodyRowIndex))
            {
                return bodyRowIndex;
            }
        }        
        
        return null;
    },    
    /// <summary>
    /// Get Last Visible Element
    /// </summary>                 
    _getLastVisibleElementIndex: function(startPosition)
    {
        var bodyRowCount = this._bodyRows.length;        
        if (typeof(startPosition) !== "undefined")
        {
            // If start position is specified, work down from the startPosition
            for (var bodyRowIndex = startPosition; bodyRowIndex < bodyRowCount; bodyRowIndex++)
            {            
                if (!this._isItemByIndexFullyVisible(bodyRowIndex))
                {
                    return bodyRowIndex - 1;
                }                
            }    
            
            return bodyRowCount - 1;
        }
        else
        {        
            for (var bodyRowIndex = bodyRowCount -1; bodyRowIndex >=  0; bodyRowIndex--)
            {            
                if (this._isItemByIndexFullyVisible(bodyRowIndex))
                {
                    return bodyRowIndex;
                }
            }
        }        
        
        return null;
    },
    
    /// <summary>
    /// The More button has been clicked for the LookBehind object
    /// </summary>             
    _onLookAheadBehindItemClick: function(sender, topIndex) 
    {            
        if (this._bodyRows !== null)
        {            
            var row = this._bodyRows[topIndex];
            if (typeof(row) !== "undefined")
            {
                this._ignoreNextScrollEvent = true;                
                this._bodyDiv.scrollTop = row.offsetTop + 1;                
                this._scrollTop = this._bodyDiv.scrollTop;
                this._hideOutOfBoundsItems();                
                this._setLookAheadBehindPosition();
            }
        }
    },    
    
    /// <summary>
    /// Select Item, and unselect all other items
    /// </summary>                 
    _onItemSelected: function(element, ctrlPressed, shiftPressed)
    {       
        // Notes:
        //  1. As per Explorer Ctrl takes shift precedence over ctrl        
        //  2. Ctrl toggles selected item
        //  3. Shift selects from the last selected item (either by Ctrl or standard click, but not by shift) till this item                
                              
        // Check key state
        
        // Toggle Selection in the lookahead/lookbehind
        var rowIndex = this._getRowIndex(element);
                
        // Toggle State
        if (shiftPressed)
        {   
            this._selectItems(rowIndex, this._lastSelectedItemIndex);
        }        
        else if (ctrlPressed)
        {
            this._toggleItemSelection(element, rowIndex);
            this._lastSelectedItemIndex = rowIndex;               
            this._setFocus(rowIndex); 
        }
        else
        {
            this._selectOnly(element, rowIndex);
            this._lastSelectedItemIndex = rowIndex;            
            this._setFocus(rowIndex);  
        }
    },
 
    // focus
    _focusHandler : function(e) {        
    },
                    
    // keydown event receives the Key Up and Key Down presses which are not received by the KeyPress handler       
    _keyDownHandler : function(e) {
        
        if(e.charCode === Sys.UI.Key["tab"])
        {
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
                
            }
        }

        if(e.keyCode === Sys.UI.Key["up"] || e.keyCode === Sys.UI.Key["down"])
        {
            e.preventDefault();        
        }
    },
    
    // keypress - handles navigation
    _keyPressHandler : function(e) {
        // Don't process tab
        if(e.charCode === Sys.UI.Key["tab"])
        {
            return;
        }
        else if(e.charCode === Sys.UI.Key["space"])
        {
            // Simulate item select
            this._onItemSelected(this._bodyRows[this._focusItemIndex], e.ctrlKey, e.shiftKey);        
            e.preventDefault();        
        }        
        else if(e.charCode === Sys.UI.Key["down"])
        {
            this._setFocus(this._focusItemIndex + 1);        
        }
        else if(e.charCode === Sys.UI.Key["up"])
        {
            this._setFocus(this._focusItemIndex - 1);                
        }
        
    },
    
    /// <summary>
    /// Set focus on an item. On a single item can have focus at any time
    /// </summary>                          
    _setFocus: function(itemIndex)
    {        
        if (!this._bodyRows || this._bodyRows.length === 0)
        {
            return;
        }
        
        if (itemIndex < 0)
        {
            itemIndex = 0;
        }
        
        if (itemIndex >= this._bodyRows.length)
        {
            itemIndex = this._bodyRows.length - 1;
        }

        if (this._focusItemIndex === itemIndex)
        {
            return;
        }
        
        // Remove existing attribute and styling
        var previousElement = this._bodyRows[this._focusItemIndex];
        if (previousElement)
        {
            previousElement.setAttribute('hasFocus', false);        
            Sys.UI.DomElement.removeCssClass(previousElement, "nhscui_ml_focus");                        
        }
    
        // Add focus to new item
        this._focusItemIndex = itemIndex;
        var element = this._bodyRows[this._focusItemIndex];        
        element.setAttribute('hasFocus', true);        
        Sys.UI.DomElement.addCssClass(element, "nhscui_ml_focus");                                        
                
        // Check if the element is visible - if not, scroll it into view
        if (!this._isItemFullyVisible(element))
        {            
            this._ignoreNextScrollEvent = true;
                        
            if (this._isItemAboveVisible(element))
            {                
                // Item is above the currently visible                        
                this._bodyDiv.scrollTop = element.offsetTop + 1;
                this._scrollTop = this._bodyDiv.scrollTop;    
                this._hideOutOfBoundsItems();                
                this._setLookAheadBehindPosition()
            }
            else
            {                
                // Item is below the currently visible                            
                this._bodyDiv.scrollTop = (element.offsetTop + element.offsetHeight) - this._bodyDivHeight;                
                this._scrollTop = this._bodyDiv.scrollTop;    
                this._hideOutOfBoundsItems();                
                this._setLookAheadBehindPosition();
            }
        }                
    },
    
    /// <summary>
    /// Toggle item selection
    /// </summary>                      
    _toggleItemSelection: function(element, rowIndex)
    {
        var isSelected = element.getAttribute("isSelected");
        isSelected  = (isSelected === "true" ? false : true); 
        this._selectItem(element, rowIndex, isSelected);
    },

    /// <summary>
    /// Select the given item only, unselect all other
    /// </summary>                      
    _selectOnly: function(element)
    {    
        if (this._bodyRows)
        {
            var bodyRowCount = this._bodyRows.length;
            for (var bodyRowIndex = 0; bodyRowIndex < bodyRowCount; bodyRowIndex++)
            {
                var bodyRow = this._bodyRows[bodyRowIndex];
                if (bodyRow)
                {                                    
                    this._selectItem(bodyRow, bodyRowIndex, bodyRow === element);
                }
            }                        
        }
        
    }, 
         
    /// <summary>
    /// Select Multiple items
    /// </summary>                               
    _selectItems: function(index1, index2)
    {
        if (this._bodyRows)
        {
            var bodyRowCount = this._bodyRows.length;
            var startIndex;
            var endIndex;
    
            // Set the start and end indices
            if (index1 < index2)
            {
                startIndex = index1;
                endIndex = index2;
            }
            else
            {
                // Reverse order if index 1 is after index 2
                startIndex = index2;
                endIndex = index1;
            }
            
            bodyRowCount = this._bodyRows.length;
            // Select items from the last select down to the current                                 
            for (var bodyRowIndex = 0; bodyRowIndex < bodyRowCount; bodyRowIndex++)
            {
                var bodyRow = this._bodyRows[bodyRowIndex];
                if (bodyRow)
                {                                    
                    this._selectItem(bodyRow, bodyRowIndex, bodyRowIndex >= startIndex && bodyRowIndex <= endIndex);
                }
            }                        
        }    
    },   
          
    /// <summary>
    /// Select item
    /// </summary>                      
    _selectItem: function(element, rowIndex, isSelected)
    {
        var currentState = (element.getAttribute("isSelected") == "true" ? true : false);
        if (currentState === isSelected)
        {
            return;
        }
        
        element.setAttribute('isSelected', isSelected.toString());
        
        if (isSelected === true)
        {
            Sys.UI.DomElement.addCssClass(element, "nhscui_ml_selected");            
            
        }
        else
        {            
            Sys.UI.DomElement.removeCssClass(element, "nhscui_ml_selected");
        }            
                
        if (this.get_lookaheadObj().get_visible())
        {
            this._lookbehindObj.selectItem(rowIndex, isSelected);
        }
        
        if (this.get_lookaheadObj().get_visible())
        {
            this._lookaheadObj.selectItem(rowIndex, isSelected);
        }    
    },
             
    /// <summary>
    /// Iterate through the list of items finding the index of the item
    /// </summary>                 
    _getRowIndex: function(row)
    {
        var rowIndex = 0;
        var rowCount = this._tbody.rows.length;
        while (this._tbody.rows[rowIndex].id !== row.id && rowIndex < rowCount)
        {
            rowIndex++;
        }
        
        if (this._tbody.rows[rowIndex].id === row.id)
        {
            return rowIndex;
        }
    },
            
    /// <summary>
    /// Horizontally scroll the look-ahead/look-behind controls
    /// </summary>              
    _setLookAheadBehindPosition: function()
    {        
        if (this._bodyRows !== null && this._bodyRows.length > 0)
        {
            var topItemIndex = 0;  
            var bottomItemIndex = this._bodyRows.length;
            
            if (this._scrollY)
            {
                topItemIndex = this._getFirstVisibleElementIndex();
                bottomItemIndex = this._getLastVisibleElementIndex(topItemIndex) + 1;
            }
                                        
            // Scroll Look Ahead - Look Behind                                               
            if (this._lookbehindObj && this._lookbehindObj.get_visible())
            {        
                this._lookbehindObj.scroll(topItemIndex);                
            }

            if (this._lookaheadObj && this._lookaheadObj.get_visible())
            {
                this._lookaheadObj.scroll(bottomItemIndex);
            }  
        }      
    },
    
    /// <summary>
    /// Iterate through the list of items finding the first item that has an offsetTop greater than the top coordinate    
    /// This will indicate an item that is completely visible (subject to enough vertical height)
    /// </summary>                    
    _findNextVisibleElement: function(top)
    {
        var bodyRowIndex = 0;
        var bodyRowCount = this._bodyRows.length;                
        var match = null;
        
        while (bodyRowIndex < bodyRowCount && match === null)
        {        
            var bodyRow = this._bodyRows[bodyRowIndex];
            var offsetTop = this._bodyRows[bodyRowIndex].offsetTop;
            if (offsetTop && offsetTop > top)
            {
                match = bodyRowIndex;
            }
            else
            {
                bodyRowIndex++;
            }
        };            
        
        
        return match;
    },        
    
    /// <summary>
    /// To improve performance, get the bounds of the elements on initialize only
    /// </summary>                    
    _getBodyRowBounds: function()
    {        
        // Give one pixel lee way        
        if (this._bodyRows)
        {
            this._bodyRowBounds = new Array();
            var bodyRowIndex = 0;
            var bodyRowCount = this._bodyRows.length;                        
            var bodyRow = null;
            var bounds = null;
                        
            while (bodyRowIndex < bodyRowCount)
            {        
                bodyRow = this._bodyRows[bodyRowIndex];   
                this._bodyRowBounds[bodyRowIndex] = new Sys.UI.Bounds(0, bodyRow.offsetTop, 0, bodyRow.offsetHeight);         
                bodyRowIndex++;
            };
        }
    }, 

    /// <summary>
    /// Is item fully visible
    /// </summary>                    
    _isItemByIndexFullyVisible: function(index)
    {   
        var bodyRow = this._bodyRowBounds[index];
        if (typeof(bodyRow) === "undefined")
        {
            return false;
        }
        else
        {     
            // Give one pixel lee way
            var elementTop = bodyRow.y;
            var elementBottom = elementTop + bodyRow.height;
            var scrollBottom = this._scrollTop + this._bodyDivHeight;        
            
            return (elementTop >= this._scrollTop - 1 && elementBottom < scrollBottom + 1);
        }
    },    
            
    /// <summary>
    /// Is item fully visible
    /// </summary>                    
    _isItemFullyVisible: function(element)
    {        
        // Give one pixel lee way
        var elementTop = element.offsetTop;
        var itemBottom = elementTop + element.offsetHeight - 1;                
        var scrollBottom = this._scrollTop + this._bodyDivHeight + 1;        
        
        return (elementTop >= this._scrollTop - 1 && itemBottom < scrollBottom);
    },        

    /// <summary>
    /// Is item above the visible range
    /// </summary>                    
    _isItemAboveVisible: function(element)
    {
        return (element.offsetTop < this._scrollTop);
    },        
      
    /// <summary>
    /// Display Grid
    /// </summary>            
    _showGrid: function()
    {           
	    // Clear float from more DIV	    
	    this._scrollTable.style.clear = "both";
	    
	    // Display Table             
        this._scrollTable.style.visibility = "visible";	    
        this.get_element().style.visibility = "visible";                    
    },

    ///<summary>
    /// Resize the Columns of the table to match the specified widths, and also limits due to word-wrapping
    ///</summary>         
    _resizeTableColumns: function()
    {	        
        // Set the default widths first as a first pass. 
        this._setDefaultWidths();
	    	    
	    if (this._bodyRows != null && this._bodyRows.length > 0)
	    {	    	        
            // Remeasure the columns to see if word wrapping has forced the columns width    
            this._resizeCells();    	
	    }
	    
	    this._resizeScrollArea();
	},
  
    ///<summary>
    /// Resize Scroll Area
    ///</summary>         
    _resizeScrollArea: function()    
    {
        if (this._bodyDiv === null)
        {
            return;
        }
        
	    var diffWidth = 0;
	    var diffHeight = 0;	    	    
	    var width = 0;						
	    var columnWidth = 0; 
	    var scrollbarWidth = 17;
	        	
	    this._bodyDiv.style.overflow = "auto";	    
	    this._bodyDiv.style.overflowX = "hidden";
	    this._bodyDiv.style.overflowY = "scroll";
        this._bodyDiv.style.overflowX = "hidden";				
                	    
	    var height = this._tableHeight;
	    
        if (this._lookaheadObj && this._lookaheadObj.get_visible())
        {
            height = height - this._lookaheadElement.offsetHeight;                            
        }

        if (this._lookbehindObj && this._lookbehindObj.get_visible())
        {
            height = height - this._lookbehindElement.offsetHeight;
        }

        height = height - this._headerRowDiv.offsetHeight;	    
	    
	    // If the height of rows is less than the defined height of the grid - do not show Scroll Bar
	    if (this._bodyTable.offsetHeight < height)
	    {		    
		    this._bodyDiv.style.overflowY = "hidden";
		    this._scrollY = false;
	    }
	    else
	    {
	        this._scrollY = true;
	    }
	    	    
	    if (!this._scrollY)
	    {
	        scrollbarWidth = 0;	        
	    }

        // Resize the Header
        this._setSafeWidth(this._headerRowDiv, this._headerTable.offsetWidth + scrollbarWidth);	        
	    this._headerRowDiv.style.overflow = "hidden";			    
	    
	    // Resize the Body
	    if (height > 0)
	    {        
	        this._bodyDiv.style.height = height.toString() + "px";	    	    
	    }
	    
	    this._setSafeWidth(this._bodyDiv, this._bodyTable.offsetWidth + scrollbarWidth);				
	    if (!this._scrollY)	    
	    {
	        // Hide the Scroll bar if not scrolling
            this._bodyDiv.style.overflow = "hidden";
	    }
	    
	    // Resize MedicationGrid Div	
	    var element = this.get_element();   
	    var bounds = Sys.UI.DomElement.getBounds(this._headerRowDiv);
	    element.style.width = bounds.width + 1 + "px";
	    
	    this._bodyDivHeight = this._bodyDiv.offsetHeight;
	    
        width = bounds.width + 2;
	    
	    // Set the LookAhead/LookBehind to the same width
	    if (this.get_lookaheadObj().get_visible())
	    {	            
	        this.get_lookaheadObj().set_width(width);
	    }
	    
	    if (this.get_lookbehindObj().get_visible())
	    {
	        this.get_lookbehindObj().set_width(width);
	    }        	    
	},

    /// <summary>
    /// Find the first DOM Element at the given childNode depth
    /// </summary>         
    _getElementAt: function(node, depth)
    {       
        var element = node;
        for (var i = 0; i < depth; i++)
        {
            element = this._getElement(element);
            if (element === null)
            {
                return null;
            }                      
        }
        
        return element;
    },

    /// <summary>
    /// Find the nth instance of DOM Element within the childNodes 
    /// </summary>         
    _getElement: function(node, instance)
    {
        if (!instance)
        {
            instance = 0;
        }
        
        var nodes = node.childNodes;
        var currentInstance = 0;
        for (var i = 0; i < nodes.length; i++)
        {
            if (typeof(nodes[i].tagName) !== "undefined")
            {
                if (currentInstance === instance)        
                {
                    return nodes[i];
                }   
                
                currentInstance++;         
            }
        }
        
        return null;
    },

    /// <summary>
    /// Find the first text within the children and their descendants of the given node
    /// </summary>         
    _getTextNode: function(node)
    {        
        var nodes = node.childNodes;        
        for (var i = 0; i < nodes.length; i++)
        {
            if (nodes[i].nodeType === 3)
            {
                return nodes[i];
            }
            else if (typeof(nodes[i].childNodes) !== "undefined" && nodes[i].childNodes.length > 0)
            {
                var recursiveNode = this._getTextNode(nodes[i]);
                if (recursiveNode != null)
                {
                    return recursiveNode;
                }                
            }
        }
        
        return null;
    },       
    /// <summary>
    /// Set the Default Widths of the columns based on the values set by the user.
    /// These cannot be guaranteed due to word wrapping and images
    /// </summary>         
    _setDefaultWidths: function()
    {               
        // Set Header Column Widths       
        //    Set Start Date Column Width        
        this._setHeaderColumnWidth(this._startDateHeaderColumn, this._startDateColumnWidth);
        
        //    Set Dosage Details Column Width
        this._setHeaderColumnWidth(this._dosageDetailsHeaderColumn, this._drugDetailsColumnWidth);                                
        
        //    Set Reason Column Width
        if (this._showReason)
        {
            this._setHeaderColumnWidth(this._reasonHeaderColumn, this._reasonColumnWidth);
        }

        // Set Status Column Width
        if (this._showStatus || this._showStatusDate)
        {
            this._setHeaderColumnWidth(this._statusHeaderColumn, this._statusColumnWidth);
        }
                
        // Match the Body Columns to the Header Column Widths               
        // Set Start Date Column Width
        this._matchToHeaderColumn(this._startDateHeaderColumn, this._startDateBodyColumn);

        // Set Dosage Details Column Width
        var offset = 0;
        if (this._showGraphics && this._bodyColumns)
        {                     
            // Subtract the width of the two graphics columns as these cannot be sized
            offset = (this._bodyColumns[this._dosageDetailsBodyColumn].offsetLeft - this._bodyColumns[this._indicatorGraphicBodyColumn].offsetLeft) - 1;            
            if (Sys.Browser.agent === Sys.Browser.InternetExplorer) 
	        {   
	            offset -= 1;
	        }            
        }                                          
        this._matchToHeaderColumn(this._dosageDetailsHeaderColumn, this._dosageDetailsBodyColumn, offset + 1);        

        // Set Reason Column Width
        if (this._showReason)
        {
            this._matchToHeaderColumn(this._reasonHeaderColumn, this._reasonBodyColumn);
        }

        // Set Status Column Width
        if (this._showStatus || this._showStatusDate)
        {
            this._matchToHeaderColumn(this._statusHeaderColumn, this._statusBodyColumn);            
        }
        
        // Remove width limitations until the columns have been resized, otherwise limits will be enforced        
        this._headerRowDiv.style.width = '';	    
        this._headerTable.style.width = '';	    
        this._bodyDiv.style.width = '';	  
        this.get_element().style.width = '';
    },       

    /// <summary>
    /// Set width of an element. If width is less than 0, limit to zero
    /// </summary>         
    _setSafeWidth: function(element, width)
    { 
        if (width < 0)
        {
            width = 0;
        }             
        
        element.style.width = width + "px";
    },

    /// <summary>
    /// Set width of a column
    /// </summary>         
    _setColumnWidth: function(column, columnWidth)
    {              
        if (typeof(columnWidth) == 'number')
        {
            if (columnWidth <= 0)
            {
                columnWidth = 1;
            }
                        
            if (column)
            {
                column.style.width = columnWidth + "px";
            }            
        }
        else
        {        
            // Set Start Date Column Width
            if (column)
            {
                // IE is not resizing all the cells in the column - need to apply to all styles.
                // TODO: Apply the width to an underlying stylesheet style rather than to each column                                
                column.style.width = columnWidth;
                column.style.width = column.offsetWidth + "px";
            }
        }
    }, 

    /// <summary>
    /// Set width of a header column
    /// </summary>         
    _setHeaderColumnWidth: function(columnIndex, columnWidth)
    {                 
        this._setColumnWidth(this._headerRowColumns[columnIndex], columnWidth);            
    },     

    /// <summary>
    /// Set width of a body column
    /// </summary>         
    _setBodyColumnWidth: function(columnIndex, columnWidth, limit)
    {                 
        if (this._bodyColumns)
        {
            var firstRow =  this._bodyColumns[columnIndex].parentNode;
            var rows =  firstRow.parentNode.rows;                       
            var rowCount =  rows.length;
            
            // Override the row count to only update a limit number of rows
            if (typeof(limit) !== "undefined")
            {
                rowCount = limit;
            }
            var row = 0;
            
            do
            {                
                this._setColumnWidth(rows[row].cells[columnIndex], columnWidth);            
                row++;        
            } while (row < rowCount);
        }    
    },      
        
    /// <summary>
    /// Match to header column
    /// </summary>             
    _matchToHeaderColumn: function(headerColumnIndex, bodyColumnIndex, offset)
    {        
        // Get the width of the header column
        var headerColumnWidth = this._getColumnWidth(this._headerRowColumns, headerColumnIndex);        
        
        if (typeof(offset) !== "undefined")
        {
            headerColumnWidth -= offset;
        }
        
        // Set the first body column to this width
        this._setBodyColumnWidth(bodyColumnIndex, headerColumnWidth);
                
        // Now measure the column 
        var bodyColumnWidth = this._getColumnWidth(this._bodyColumns, bodyColumnIndex);
        
        bodyColumnWidth = headerColumnWidth - (bodyColumnWidth - headerColumnWidth);
        this._setBodyColumnWidth(bodyColumnIndex, bodyColumnWidth);        
    },

    /// <summary>
    /// Match to body column
    /// </summary>             
    _matchToBodyColumn: function(headerColumnIndex, bodyColumnIndex, offset)
    {        
        // Get the width of the header column
        var bodyColumnWidth = this._getColumnWidth(this._bodyColumns, bodyColumnIndex);        
        
        if (typeof(offset) !== "undefined")
        {
            bodyColumnWidth += offset;
        }
        
        // Set the first body column to this width
        this._setHeaderColumnWidth(headerColumnIndex, bodyColumnWidth);
                
        // Now measure the column 
        var headerColumnWidth = this._getColumnWidth(this._headerRowColumns, headerColumnIndex);
        
        headerColumnWidth = bodyColumnWidth - (headerColumnWidth - bodyColumnWidth);
        this._setHeaderColumnWidth(headerColumnIndex, headerColumnWidth);        
    },
        
    /// <summary>
    /// Get Width of column by measuring its position relative to the next column, rather than its width
    /// </summary>             
    _getColumnWidthUsingNextVisible: function(columns, columnIndex)
    {
        var nextColumn = this._getNextVisibleColumnInList(columns, columnIndex);        
        var column = columns[columnIndex];
        
        if (nextColumn === null || typeof(nextColumn) === "undefined")
        {
            return column.parentNode.offsetWidth - column.offsetLeft - 1;
        }
        else
        {
            // What we can do is align the element sch
            return nextColumn.offsetLeft - column.offsetLeft - 1;
        }
    }, 
    
    /// <summary>
    /// Get Next Visible Column
    /// </summary>                
    _getNextVisibleColumnInList : function(columns, index)
    {    
        var columnCount = columns.length;
        
        if (index >= columnCount)
        {
            return null;
        }
        
        index ++;
        while (index < columnCount && columns[index].style.display == "none")
        {
            index++;
        }
        
        return columns[index];
    },  
        
    /// <summary>
    /// Get Width of column by measuring its position relative to the next column, rather than its width
    /// </summary>             
    _getColumnWidth: function(columns, columnIndex)
    {
        if (columns)
        {            
            return columns[columnIndex].clientWidth;
        }
        else
        {
            return 0;
        }
    },  
             
    /// <summary>
    /// Resize the great of the header or body column
    /// </summary>                 
    _resizeCell: function(headerColumnIndex, bodyColumnIndex)
    {           
        var offset = 0;
        if (this._bodyColumns && bodyColumnIndex === this._dosageDetailsBodyColumn)
        {                     
            if (this._showGraphics)
            {
                // Subtract the width of the two graphics columns as these cannot be sized
                offset = (this._bodyColumns[this._dosageDetailsBodyColumn].offsetLeft - this._bodyColumns[this._indicatorGraphicBodyColumn].offsetLeft) - 1;            
            }       

            if (this._scrollY && this._showGraphics)
            {                                
                offset += 1;            
            }                
        }                                                              
    
        var bodyWidth = this._getColumnWidth(this._bodyColumns, bodyColumnIndex);
        var headerWidth = this._getColumnWidth(this._headerRowColumns, headerColumnIndex);                        
        
        if (bodyWidth > headerWidth)
        {
           this._matchToBodyColumn(headerColumnIndex, bodyColumnIndex, offset);         
           bodyWidth = this._getColumnWidth(this._bodyColumns, bodyColumnIndex);
           headerWidth = this._getColumnWidth(this._headerRowColumns, headerColumnIndex);                                   
          
           // Wrapping restriction may prevent the required width to be set. If this is the case, resize the Body columns to match the header width          
           this._matchToHeaderColumn(headerColumnIndex, bodyColumnIndex, offset);                        
        }    
        else if (bodyWidth != headerWidth)
        {                                    
           this._matchToHeaderColumn(headerColumnIndex, bodyColumnIndex, offset);            
           bodyWidth = this._getColumnWidth(this._bodyColumns, bodyColumnIndex);
           headerWidth = this._getColumnWidth(this._headerRowColumns, headerColumnIndex);                                   
           
           // Wrapping restriction may prevent the required width to be set. If this is the case, resize the Header columns to match the Body width                     
           this._matchToBodyColumn(headerColumnIndex, bodyColumnIndex, offset);         
        }
    },
    
    /// <summary>
    /// Resize the Cells widths to match the greater of the body and header column widths
    /// </summary>         
    _resizeCells: function()    
    {            
        if (this._bodyColumns && this._bodyColumns.length > 0)
        {
            this._resizeCell(this._startDateHeaderColumn, this._startDateBodyColumn);
            
            // Set Dosage Details Column Width
            this._resizeCell(this._dosageDetailsHeaderColumn, this._dosageDetailsBodyColumn);

            if (this._showReason)
            {
                // Set Reason Column Width
                this._resizeCell(this._reasonHeaderColumn, this._reasonBodyColumn);
            }
                    
            // Set Status Column Width
            if (this._showStatus || this._showStatusDate)
            {
                this._resizeCell(this._statusHeaderColumn, this._statusBodyColumn);
            }
        }
    },       
        
    /// <summary>
    /// Sort Table 
    /// </summary>                
    sort: function(column, ascending)
    {        
        var e = Function._validateParams(arguments, 
                    [
                        { name: "column", type: NhsCui.Toolkit.Web.MedicationGridColumn, mayBeNull: false},
                        { name: "ascending", type: Boolean, mayBeNull: true, optional: true}
                    ]);
        if (e)
        {
            throw e;
        }
        
        if (typeof(ascending) === "undefined" || ascending === null)
        {
            // Default to ascending
            ascending = true;
        }
        
        this._sortTable(column, ascending);
    },
            
    /// <summary>
    /// Sort Table - Internal Method
    /// </summary>         
    _sortTable: function(headerColumn, ascending)
    {
        items = this._bodyRows;        
        var N = items.length;
        
        var parent = this._bodyRows[0].parentNode;    
        
        var bodyColumn = 0;    
        switch (headerColumn)
        {
            case NhsCui.Toolkit.Web.MedicationGridColumn.StartDate:
                bodyColumn = 0;
                break;
            case NhsCui.Toolkit.Web.MedicationGridColumn.DrugDetails:
                bodyColumn = 3;
                break;
            case NhsCui.Toolkit.Web.MedicationGridColumn.Reason:
                bodyColumn = 4;
                break;
            case NhsCui.Toolkit.Web.MedicationGridColumn.Status:
                bodyColumn = 5;
                break;
            default:
                bodyColumn = 0;
                break;                                                
        }

        // bubble sort - not very efficient but ok for short lists
        for(var j=N-1; j > 0; j--) {
            for(var i=0; i < j; i++) {
                if (bodyColumn !== 0)
                {
                    var secondval1 = NhsDate.parse(this._getRowText(i+1, 0)).get_dateValue();
                    var secondval2 = NhsDate.parse(this._getRowText(i, 0)).get_dateValue();                
                }

                if (bodyColumn === 0)
                {
                    var val1 = NhsDate.parse(this._getRowText(i+1, 0)).get_dateValue();
                    var val2 = NhsDate.parse(this._getRowText(i, 0)).get_dateValue();
                                        
                    if (this._compare(val1, val2, ascending))
                    {
                        this._exchange(parent, i+1, i);
                    }
                }
                else if (bodyColumn === 5)
                {
                    var statusDate1 = NhsDate.parse(this._getStatusDate(i+1)).get_dateValue();                                               
                    var statusDate2 = NhsDate.parse(this._getStatusDate(i)).get_dateValue();  
                        
                    if ( (this._showStatusDate && statusDate1.getTicks() != statusDate2.getTicks()) || !this._showStatus)
                    {                                     
                        if (!this._showStatus)
                        {
                            if (this._compareDates(statusDate1, statusDate2, ascending, secondval1, secondval2))
                            {
                                this._exchange(parent, i+1, i);
                            }
                        }
                        else
                        {
                            if (this._compareDates(statusDate1, statusDate2, ascending))
                            {
                                this._exchange(parent, i+1, i);
                            }
                        }
                        continue;
                        // If showing status date, and they are different, sort by start date 
                    }                    
                        
                    var status1 = this._getStatus(i+1);
                    var status2 = this._getStatus(i);
                                        
                    if (this._compare(status1, status2, ascending, secondval1, secondval2)) 
                    {
                        this._exchange(parent, i+1, i);
                    }
                }                
                else
                {
                    if (this._compare(this._getRowText(i+1, bodyColumn), this._getRowText(i, bodyColumn), ascending, secondval1, secondval2)) 
                    {
                        this._exchange(parent, i+1, i);
                    }                        
                }
            }
        }
                
        
        this._hideOutOfBoundsItems();
        
        return true;
    },

   /// <summary>
    /// Get StartDate from a row
    /// </summary>             
    _getStartDate: function(i)
    {
        var node = this._bodyRows[i].getElementsByTagName("TD")[0];
        if(node.childNodes.length === 0) return "";
        var div = node.getElementsByTagName("DIV")[0];
        var startDateNode = div.getElementsByTagName("SPAN")[0]                                

        if (node.innerText != undefined)
        {
            return startDateNode.innerText;         
        }
        else
        {
            return startDateNode.textContent;
        }                        
                    
    },
    
    /// <summary>
    /// Get Medication Names
    /// </summary>             
    _getMedicationNames: function(i)
    {
        var node = this._bodyRows[i].getElementsByTagName("TD")[3];
        if(node.childNodes.length === 0) return "";
        var div = node.getElementsByTagName("DIV")[0];
        var medLabel = div.getElementsByTagName("A")[0]                                
        
        if (medLabel.innerText != undefined)
        {
            return medLabel.innerText;         
        }
        else
        {
            return medLabel.textContent;
        }                        
    },    
    
    /// <summary>
    /// Get Dosage Text
    /// </summary>             
    _getDosageText: function(i)
    {
        var node = this._bodyRows[i].getElementsByTagName("TD")[3];
        if(node.childNodes.length === 0) return "";
        var div = node.getElementsByTagName("DIV")[0];
        var dosageNode = div.getElementsByTagName("SPAN")[2]                                
        
        if (dosageNode.innerText != undefined)
        {
            return dosageNode.innerText;         
        }
        else
        {
            return dosageNode.textContent;
        }                        
    },       
    
    /// <summary>
    /// Get StatusDate
    /// </summary>             
    _getStatusDate: function(i)
    {
        var node = this._bodyRows[i].getElementsByTagName("TD")[5];
        if(node.childNodes.length === 0) return "";
        var div = node.getElementsByTagName("DIV")[0];
        var statusDateNode = div.getElementsByTagName("SPAN")[0]                                

        if (statusDateNode.innerText != undefined)
        {
            return statusDateNode.innerText;         
        }
        else
        {
            return statusDateNode.textContent;
        }   
    },
    
    /// <summary>
    /// Get Status
    /// </summary>             
    _getStatus: function(i)
    {
        var node = this._bodyRows[i].getElementsByTagName("TD")[5];
        if(node.childNodes.length === 0) return "";
        var div = node.getElementsByTagName("DIV")[0];
        var statusNode = div.getElementsByTagName("SPAN")[2]                                
        var status = "";
        
        if (statusNode.innerText !== undefined)
        {
            status = statusNode.innerText;         
        }
        else
        {
            status = statusNode.textContent;
        }       
        
        return status;                 
    },
        
    /// <summary>
    /// Get Row
    /// </summary>             
    _getRowText: function(i, column)
    {
        var node = this._bodyRows[i].getElementsByTagName("TD")[column];
        var text = "";
        if(node.childNodes.length === 0) return "";
        if (node.innerText != undefined)
        {
            text = node.innerText;         
        }
        else
        {
            text = node.textContent;
        }      
        
        return text;  
    },

    /// <summary>
    /// Compare to column values
    /// </summary>             
    _compareDates: function(val1, val2, ascending, secondval1, secondval2)
    {
        if (val1.getTicks() == val2.getTicks() && typeof(secondval1) !== "undefined" && typeof(secondval2) !== "undefined")
        {
            return (ascending) ? secondval1 < secondval2 :  secondval1 > secondval2;
        }
        else
        {        
            return (ascending) ? val1 < val2 :  val1 > val2;
        }
    },
    
    /// <summary>
    /// Compare to column values
    /// </summary>             
    _compare: function(val1, val2, ascending, secondval1, secondval2)
    {
        if (val1 == val2 && typeof(secondval1) !== "undefined" && typeof(secondval2) !== "undefined")
        {
            return (ascending) ? secondval1 < secondval2 :  secondval1 > secondval2;
        }
        else
        {        
            return (ascending) ? val1 < val2 :  val1 > val2;
        }
    },

    /// <summary>
    /// Exchange 2 rows
    /// </summary>                 
    _exchange: function(parent, i, j)
    {
        // exchange adjacent items
        parent.insertBefore(this._bodyRows[i], this._bodyRows[j]);
        
        this._lookaheadObj.exchange(i, j);
        this._lookbehindObj.exchange(i, j);
        this._setLookAheadBehindPosition();                        
    },
        
    /// <summary>
    /// Show/Hide the LookAhead/LookBehind views
    /// </summary>                             
    _updateLookAheadBehind: function()
    {
        if (this.get_lookaheadObj())
        {
            this._lookaheadObj.set_visible(this._showLookAheadBehind);
        }
        
        if (this.get_lookbehindObj())
        {        
            this._lookbehindObj.set_visible(this._showLookAheadBehind);
        }                
    },
    
    /// <summary>
    /// Update the header properties
    /// </summary>                         
    _updateHeader: function()
    {       
        var reasonHeader = this._headerRowColumns[2];               
        var statusHeader = this._headerRowColumns[3];       
        
        // If reason is hidden, do not show the reason header
        if (reasonHeader)
        {
            this._showElement(reasonHeader, this._showReason);
        }
        
        // If both Status and Status Date are hidden, do not show the Status Column
        if (statusHeader)        
        {
            this._showElement(statusHeader, this._showStatus || this._showStatusDate);            
        }
    },
    
    /// <summary>
    /// Update the properties of each row
    /// </summary>                     
    _updateRows :function()
    {    
        if (this._bodyRows)
        {
            var bodyRowCount = this._bodyRows.length;
            for (var bodyRow =0; bodyRow < bodyRowCount; bodyRow++)
            {
                this._updateRow(this._bodyRows[bodyRow])
            }                        
        }        
    },
        
    /// <summary>
    /// Update the properties of a row
    /// </summary>                             
    _updateRow :function(element)
    {
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
            this._showElement(statusCell, this._showStatus || this._showStatusDate);
                    
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
    },

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
    
NhsCui.Toolkit.Web.MedicationGridBehavior.registerClass('NhsCui.Toolkit.Web.MedicationGridBehavior', AjaxControlToolkit.BehaviorBase);

NhsCui.Toolkit.Web.MedicationEventArgs = function(medicationID) {
    /// <summary>
    /// Event arguments for the MedicationEventArgs's Click, RightClick or Double events
    /// </summary>
    /// <param name="medicationID" type="String">
    /// Rating
    /// </param>
    NhsCui.Toolkit.Web.MedicationEventArgs.initializeBase(this);

    this._medicationID = medicationID;
};

NhsCui.Toolkit.Web.MedicationEventArgs.prototype = {
    get_MedicationID: function() {
        /// <value type="String">
        /// MedicationID
        /// </value>
        return this._medicationID;
    }
};


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

NhsCui.Toolkit.Web.MedicationEventArgs.registerClass('NhsCui.Toolkit.Web.MedicationEventArgs', Sys.EventArgs);