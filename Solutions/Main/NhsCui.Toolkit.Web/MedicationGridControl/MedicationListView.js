//-----------------------------------------------------------------------
// <copyright file="MedicationListView.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>17-Jan-2007</date>
// <summary>Client-side javascript for nhs Medications List</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

/// <summary>
/// The MedicationListViewMode enum is used to define the display mode for the MedicationListView
/// </summary>
NhsCui.Toolkit.Web.MedicationListViewMode = function() {
    /// <field name="LookBehind" type="Number" integer="true">
    /// LookBehind View
    /// </field>
    /// <field name="LookAhead" type="Number" integer="true">
    /// Look Ahead View
    /// </field>
    throw Error.invalidOperation();
};

NhsCui.Toolkit.Web.MedicationListViewMode.prototype = {
    LookBehind : 0,
    LookAhead : 1        
};

NhsCui.Toolkit.Web.MedicationListViewMode.registerEnum("NhsCui.Toolkit.Web.MedicationListViewMode", false);

NhsCui.Toolkit.Web.MedicationListView = function(element) {
    NhsCui.Toolkit.Web.MedicationListView.initializeBase(this, [element]);                    
    // Tooltip member variables
    this._mode = 0;
    this._moreButton = null;
    this._moreButtonImage = null;
    this._moreButtonText = null;
    this._startIndex = 0;   
    this._medicationItemPositions = new Array(); 
    this._medicationItemsWrapper = null;
    this._medicationItems = null;
    this._medicationItemCount = 0;
    this._medicationListViewWidth = 0;
    this._leftmostVisibleItemIndex = -1;
    this._onItemClickHandler = Function.createDelegate(this, this._onItemClick);        
    this._keyDownDelegate = Function.createDelegate(this, this._keyDownHandler);    
    this._keyPressDelegate = Function.createDelegate(this, this._keyPressHandler);          
    
    // Next Item in the list. This is an additional return value from getPercentageWidth.
    // For LookBehind (Mode = 1) this would be the previous item, for LookAhead (Mode = 1) this would be the next item
    this._nextItem = 0;                  
};

NhsCui.Toolkit.Web.MedicationListView.prototype = {       
    // keydown event receives the Key Up and Key Down presses which are not received by the KeyPress handler       
    _keyDownHandler : function(e) {
        
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
    
    // keypress - handles navigation
    _keyPressHandler : function(e) {
    },
    
    ///<summary>
    /// Initialize Behavior
    ///</summary>    
    initialize : function() {
        NhsCui.Toolkit.Web.MedicationListView.callBaseMethod(this, "initialize");
        
        
        var element = this.get_element();                     
        
        // Add a more tag
        this._addmoreButton();
                
        var element = this.get_element();        
        var medicationItemCount = 0;
        this._medicationItemsWrapper = element.childNodes[1];
        if (this._medicationItemsWrapper.nodeType === 3)
        {
            this._medicationItemsWrapper = element.childNodes[2];
        }
        
        element = this._medicationItemsWrapper;
        this._medicationItemsWrapper = this._medicationItemsWrapper.childNodes[0]; 
        if (this._medicationItemsWrapper.nodeType === 3)
        {
            this._medicationItemsWrapper = element.childNodes[1];
        }
        
        this._medicationListViewWidth = this._medicationItemsWrapper.offsetWidth;                
        this._medicationItems = new Array();        
    
        // Get Medications
        this._getMedications();
        this._getMedicationPositions();
        
        $addHandlers(element, 
            {
                keypress : this._keyPressDelegate,
                keydown : this._keyDownDelegate
            });
                           
    },        
    
    ///<summary>
    /// Dispose of any handlers
    ///</summary>        
    dispose : function() {       
        NhsCui.Toolkit.Web.MedicationListView.callBaseMethod(this, "dispose");
        
        if (this._moreButton)
        {
            $clearHandlers(this._moreButton);
        }
        
        this._onMoreClickHandler = null;
                
        if (this._onItemClickHandler)
        {
	        for (i = 0; i < this._medicationItemCount;i++)
	        {	    
	            $removeHandler(this._medicationItems[i], 'click', this._onItemClickHandler);        	        
	        }   
	        this._onItemClickHandler = null;
	    }
                
        
        
        this._onMoreClick = null;
        this._onItemClick = null;
    },  
    
    /// <summary>
    /// Get the left more visible item index (used by the More Button)
    /// </summary>             
    get_leftMostVisibleItemIndex: function()
    {
        return this._leftmostVisibleItemIndex;
    },
    
    ///<summary>
    /// Get/Set DisplayMode. Mode = 0 LookBehind, Mode = 1 LookAhead
    ///</summary>         
    get_mode : function() {
        return this._mode;
    },    
    set_mode : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Number }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._mode !== value)
        {
            this._mode = value;                                    
            this.raisePropertyChanged('mode');            
        }
    },                                 
    
    /// <summary>
    //  register / deregister for click event
    /// </summary>
    add_itemClick: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
                
   	    this.get_events().addHandler('itemClick', handler);
    },
    remove_itemClick: function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
    	this.get_events().removeHandler('itemClick', handler);
    },          
    
    /// <summary>
    /// Set Item Width
    /// </summary>    
    set_width : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Number , mayBeNull: true}
                    ]);
        if (e)
        {
            throw e;
        }
            
        if (value === null)
        {
            this._medicationItemsWrapper.parentNode.style.width = '';
        }
        else
        {            
            this._medicationItemsWrapper.parentNode.style.width = value.toString() + "px";
        }
        
        this.raisePropertyChanged('width');            
    },                

    /// <summary>
    /// Get/Show View visibility
    /// </summary>            
    get_visible: function()
    {
        return (this.get_element().style.display !== "none");
    },        
    set_visible: function(value)
    {
        this.get_element().style.display = (value == true ? '' : 'none');
    },        
            
    ///<summary>
    /// Update the set of visible items 
    ///</summary>    
    scroll : function(item) {                    
        var scrollLeft = 0; 
        
        this._getMedicationPositions();                                    
        var offset = this.get_element().offsetWidth;
        // Dependant on the mode, get the position of the previous/next item that is not visible
        if (this._mode === NhsCui.Toolkit.Web.MedicationListViewMode.LookBehind) 
        {                    
            scrollLeft = this._getPosition(item, offset);
        }
        else 
        {                     
            if (item === -1)
            {
                scrollLeft = this._getPosition(0, offset);
            }
            else
            {   
                scrollLeft = this._getPosition(this._medicationItemCount - item, offset)
            }
        }  
                
        // To determine the more count, count the number of items with a left position less than or equal to the right edge of the more tag                                         
        var moreCount = 0;
        if (scrollLeft < 0)
        {                        
            // Determine the left most visible item            
            this._leftmostVisibleItemIndex = this._getItemBeforePosition(this._moreButton.offsetWidth, scrollLeft);
            
            // Determine the more count
            moreCount = this._leftmostVisibleItemIndex + 1;
            this._setMoreInfo(moreCount);                                                      
            
            // Now measure that again with the count and image set, as this may have caused a greater overlap
            this._leftmostVisibleItemIndex = this._getItemBeforePosition(this._moreButton.offsetWidth, scrollLeft);
                        
            // Determine the more count
            moreCount = this._leftmostVisibleItemIndex + 1;
            this._setMoreInfo(moreCount);                                                                                          
        }
        else
        {
            this._setMoreInfo(0);        
        }                        
        
        if (parseInt(this._medicationItemsWrapper.style.left, 10) != scrollLeft && scrollLeft)
        {            
            this._medicationItemsWrapper.style.left = scrollLeft + "px";              
        }                
                
        this._hideNonVisibleItems();
    },

    /// <summary>
    /// Select an item. Note index is relative to the grid order
    /// </summary>
    selectItem: function(gridIndex, isSelected)
    {
        var index = this._toListIndex(gridIndex);
        if (this._medicationItems[index])
        {
            if (isSelected)
            {
                Sys.UI.DomElement.addCssClass(this._medicationItems[index], "nhscui_mg_look_selected");
            }
            else
            {
                Sys.UI.DomElement.removeCssClass(this._medicationItems[index], "nhscui_mg_look_selected");
            }            
        }
    },    
     
    /// <summary>
    /// Exchange two Names - used for sorting
    /// </summary>             
    exchange: function(i, j)
    {     
        // exchange adjacent items
        if (this._mode === NhsCui.Toolkit.Web.MedicationListViewMode.LookBehind) 
        {                    
            this._medicationItemsWrapper.insertBefore(this._medicationItems[this._toListIndex(i)], this._medicationItems[this._toListIndex(j)]);
        }
        else
        {
            this._medicationItemsWrapper.insertBefore(this._medicationItems[this._toListIndex(j)], this._medicationItems[this._toListIndex(i)]);                                
        }
    },     
    
    // #region Private Methods 
    
    /// <summary>
    /// Set more count and critical alert graphic in more
    /// </summary>        
    _setMoreInfo: function(moreCount)
    {
        if (moreCount > 0)
        {                            
            // Display the more tag
            this._moreButtonText.nodeValue = moreCount;
            var imageSrc = this._getCriticalAlertGraphic(moreCount - 1);
            if (imageSrc !== null && imageSrc != '')
            {
                this._moreButtonImage.src = imageSrc;
                this._moreButtonImage.style.display = '';
            }
            else
            {
                
                this._moreButtonImage.style.display = "none";
                this._moreButtonImage.src = '';
            }
            
            this._moreButton.style.display = '';   
        }                      
        else        
        {
            // Do not display the more button
            if (this._moreButton.style.display != "none")
            {
                this._moreButton.style.display = "none";                
            }
                        
            // Reset to the default place holder (used for measurement) and no more iamge
            this._moreButtonImage.src = '';
            this._moreButtonImage.display = 'none';
            this._moreButtonText.nodeValue = "000";            
        }    
    },
    
    /// <summary>
    /// The critical alert graphic to use is the first critical alert graphic specified within the range of non-visible items
    /// </summary>    
    _getCriticalAlertGraphic: function(index)
    {        
        while (index >= 0)
        {            
            var images = this._medicationItems[index].getElementsByTagName("IMG");
            if  (images.length > 0)
            {
                if (images.length > 1)
                {   
                    var secondImage = images[1];
                    if (secondImage.id.indexOf("CriticalAlertGraphic") !== -1 && secondImage.src != '' && secondImage.style.display != 'none')
                    {
                        return secondImage.src;
                    }                                         
                }
                
                                
                var firstImage = images[0];
                if (firstImage.id.indexOf("CriticalAlertGraphic") !== -1 && firstImage.src != '' && firstImage.style.display != 'none')
                {
                    return firstImage.src;
                }                
            }
            index = index - 1;
        }    
        
        return null;
    },
    
    /// <summary>
    /// Get Medication Rows
    /// </summary>
    _getMedications : function() 
    {
        var medicationItemCount = 0;
        this._medicationItems = new Array();        
        var allDrugNodes = this._medicationItemsWrapper.childNodes;                                
        
        var drugCount = allDrugNodes.length;	    
	    // Clean Header Row Columns by removing text node
	    for (i = drugCount -  1; i >= 0;i--)
	    {	    	    
	        if (allDrugNodes[i].nodeType === 3)
		    {
		        this._medicationItemsWrapper.removeChild(allDrugNodes[i]);
		    }
        }		    
	    
	    drugCount = allDrugNodes.length;	    
	    this._medicationItems = this._medicationItemsWrapper.childNodes;
	    this._medicationItemCount = this._medicationItems.length;
	    
	    for (i = 0; i < this._medicationItemCount;i++)
	    {	    	        	        
	        $addHandler(this._medicationItems[i], 'click', this._onItemClickHandler);        	        
	    }     	    	    
    },    

    /// <summary>
    /// Get Medication Positions
    /// </summary>
    _getMedicationPositions : function() 
    {            
	    // Clean Header Row Columns by removing text node
	    for (i = 0; i < this._medicationItemCount ;i++)
	    {	    
            if (Sys.Browser.agent === Sys.Browser.InternetExplorer) 
            {
                this._medicationItemPositions[i] = this._medicationItems[i].offsetLeft - this._medicationItemsWrapper.offsetLeft;
            }        
            else
            {
                this._medicationItemPositions[i] = this._medicationItems[i].offsetLeft;
            }	    	        
	    }     	    	            
    },    
        
    /// <summary>
    /// </summary>
    _addmoreButton: function()
    {
        var element = this.get_element();
        this._moreButton = document.createElement("div");
        // By default does not display the div        
        this._moreButton.style.position = "absolute";
        this._moreButton.style.styleFloat = this._moreButton.style.cssFloat = "left";
        this._moreButton.style.zIndex = "1";
        this._moreButton.className = "nhscui_mg_look_morebutton";
        this._moreButton.appendChild(document.createTextNode("< "));
        this._moreButton.appendChild(document.createElement("img"));
        this._moreButton.appendChild(document.createTextNode(" 000"));
        this._moreButton.appendChild(document.createTextNode(" more"));
        
        this._moreButtonImage = this._moreButton.childNodes[1];
        this._moreButtonText = this._moreButton.childNodes[2];        
        
        this._moreButton.style.display = "none";   
        
        element.insertBefore(this._moreButton, element.firstChild);                
        
        this._onMoreClickHandler = Function.createDelegate(this, this._onMoreClick);                
        $addHandler(this._moreButton, 'click', this._onMoreClickHandler);        
    },        
       
    ///<summary>
    /// If the More button has been clicked
    ///</summary>        
    _onMoreClick : function(e)
    {
        if (this._moreButton === null || this._moreButton.style.display === "none")
        {
            // If the element has not been initialized, or is not visible, perform no action
            return;
        }
        
        if (this._leftmostVisibleItemIndex !== -1)
        {                
            var onitemclickHandler = this.get_events().getHandler("itemClick");
                        
            if (onitemclickHandler) {
                onitemclickHandler(this, this._toGridIndex(this._leftmostVisibleItemIndex));
            }                
        }        
    },

    ///<summary>
    /// If the More button has been clicked
    ///</summary>        
    _onItemClick : function(e)
    {
        var node = e.target;
        while (node != null && node.className.indexOf("nhscui_mg_look_cell") === -1)
        {
            node = node.parentNode;
        };
        
        if (node)
        {        
            // Find the index of this item
            var onitemclickHandler = this.get_events().getHandler("itemClick");
            if (onitemclickHandler) {
                onitemclickHandler(this, this._toGridIndex(parseInt(this._getChildIndex(node), 10)));
            }        
        }     
        
        e.stopPropagation();
        e.preventDefault();   
    },    

    /// <summary>
    /// Transform the relative index used by the Grid to a valid index for the List View. 
    /// </summary>        
    _toListIndex: function(index)
    {
        if (this._mode === NhsCui.Toolkit.Web.MedicationListViewMode.LookBehind) 
        {                    
            return index;
        }
        else 
        {                        
            return this._medicationItemCount - index - 1;
        }  
    },
    
    /// <summary>
    /// Transform the relative index used by the MedicationListView to a valid index for the grid. 
    /// </summary>        
    _toGridIndex: function(index)
    {
        if (this._mode === NhsCui.Toolkit.Web.MedicationListViewMode.LookBehind) 
        {                    
            return index;
        }
        else 
        {                        
            return this._medicationItemCount - index - 1;
        }  
    },
    
    /// <summary>
    /// Find index of Child
    /// </summary>            
    _getChildIndex: function(item)
    {
        for (var index = 0; index < this._medicationItemCount; index++)
        {
            if (item === this._medicationItems[index])
            {
                return index;
            }
        }            
    },

    /// <summary>
    /// Hide non-visible items
    /// </summary>                    
    _hideNonVisibleItems: function()
    {            
        var medicationItemCount = this._medicationItemCount;
        
        var containerWidth = this._medicationItemsWrapper.offsetWidth - 1;        
        var moreButtonWidth = this._moreButton.offsetWidth;
        var scrollLeft = 0;                

        scrollLeft = this._medicationItemsWrapper.offsetLeft;
        
        var index = 0;
        var visibleDetected = false;        
        var pastVisibleItems = false;        
        
        do
        {    
            if (!pastVisibleItems)
            {        
                var visible = this._isItemByIndexFullyVisible(index, scrollLeft, containerWidth, moreButtonWidth);
                var visibility = "hidden";
                if (visible)
                {
                    visibility = "visible";
                    visibleDetected = true;
                }      
                else if (visibleDetected)
                {
                    pastVisibleItems = true;
                }                        
                
                var medicationItem = this._medicationItems[index];             
                if (medicationItem.style.visibility != visibility)
                {
                    // Big performance hit
                    medicationItem.style.visibility = visibility;
                }
            }
            else            
            {
                this._medicationItems[index].visibility = "hidden";
            }
            
            index++;
        } while (index < medicationItemCount);                   
    },  

    /// <summary>
    /// Is item fully visible
    /// </summary>                    
    _isItemByIndexFullyVisible: function(index, scrollLeft, containerWidth, moreButtonWidth)
    {        
        // IE adjusted the offset of the items  not relative to the wrapper but to its parent
        // therefore it is not required to adjust the offsets in order to determine their true position
        
        // Sys.UI.DomElement.getBounds did not calculate the position as desired
        var left = this._medicationItemPositions[index] + scrollLeft;        
        return ( left > moreButtonWidth && left < containerWidth);
    },  
                
    /// <summary>
    /// Is item fully visible
    /// </summary>                    
    _isItemFullyVisible: function(element, scrollLeft, containerWidth, moreButtonWidth)
    {        
        // IE adjusted the offset of the items  not relative to the wrapper but to its parent
        // therefore it is not required to adjust the offsets in order to determine their true position
        
        // Sys.UI.DomElement.getBounds did not calculate the position as desired
        var left = element.offsetLeft + scrollLeft;        
        return ( left > moreButtonWidth && left < containerWidth);
    },    
            
    /// <summary>
    /// Get the last item in the list with a left position before the given position
    /// </summary>    
    /// <param name="position">Right-bounding position</param>    
    /// <param name="startIndex">Start Index (optional)</param>    
    _getItemBeforePosition : function(position, offset, startIndex)
    {
        var index = startIndex;
        if (typeof(startIndex) === 'undefined')
        {
            index = this._medicationItemPositions.length - 1;            
        }    
                                
        do
        {
            if (this._medicationItemPositions[index] + offset < position)
            {
                return index;
            }
            index--;
            
        } while (index > -1)
        
        return -1;
    },
          
    /// <summary>
    /// Get the position of an item relative to an offsetPosition.
    /// </summary>
    _getPosition :function(itemIndex, offset)
    {    
        if (itemIndex < 0)
        {
            itemIndex = 0;
        }

        if (itemIndex >= this._medicationItemCount)
        {
            itemIndex = this._medicationItemCount - 1;
        }
        
        return offset - this._medicationItemPositions[itemIndex];
    
    }
    
    // #endregion
};

NhsCui.Toolkit.Web.MedicationListView.registerClass("NhsCui.Toolkit.Web.MedicationListView", AjaxControlToolkit.BehaviorBase);
