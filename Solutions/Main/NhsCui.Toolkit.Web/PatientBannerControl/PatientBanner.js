//-----------------------------------------------------------------------
// <copyright file="PatientBanner.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side representation of the patient banner control</summary>
//-----------------------------------------------------------------------

Type.registerNamespace('NhsCui.Toolkit.Web');

NhsCui.Toolkit.Web.PatientBanner = function(associatedElement) {
    NhsCui.Toolkit.Web.PatientBanner.initializeBase(this, [associatedElement]);
    this._timerId = null;
    this._eventTargets = [];
    this._timerInterval = 50;
    this._expandIncrement = 20;
    this._zoneOneFocusTooltip = null;
    this._zoneTwoFocusTooltip = null;
    this._zIndexFix = null;
    this._zoneTwoPlaceholder = null;
    this._prevSize = null;
};

//
// Client-side class for patient banner
//    
NhsCui.Toolkit.Web.PatientBanner.prototype = {
    //
    // Initialize
    //    
    initialize : function() {
        NhsCui.Toolkit.Web.PatientBanner.callBaseMethod(this, 'initialize');
        
        this._hoverDelegate = Function.createDelegate(this, this._hoverHandler);
        this._unhoverDelegate = Function.createDelegate(this, this._unhoverHandler);
        this._clickDelegate = Function.createDelegate(this, this._clickHandler);    
        this._keypressDelegate = Function.createDelegate(this, this._keypressHandler);    
                
        this._doExpandDelegate = Function.createDelegate(this, this._doExpandHandler);
        this._doCollapseDelegate = Function.createDelegate(this, this._doCollapseHandler);
        this._resizeDelegate = Function.createDelegate(this, this._resizeHandler);
        
        this._ensureStandardHandlers(this._getGenderLabel());
        this._ensureStandardHandlers(this._getGender());
        this._ensureStandardHandlers(this._getIdentifierLabel());
        this._ensureStandardHandlers(this._getIdentifier());
                
        this._ensureStandardHandlers(this._getZoneTwoPermanent());      
        
        this._getGenderLabel().tabIndex = -1;
        this._getIdentifierLabel().tabIndex = -1;
        
        this._initializeAutoEllipseData();
        this._initializeFocusTooltips();
        this._initializeZIndexFix();  
        
        this._resizeHandler();
         
        //unhide patient banner
        this.get_element().style.visibility = "visible"; 
        $get(this.get_id() + "_patientDataCell").style.visibility = "visible";       
    },    
    //
    // Clean-up
    //    
    dispose : function() {
    
        if(this.get_isInitialized())
        {
            this._removeStandardHandlers(this._getZoneTwoPermanent());  
            this._removeStandardHandlers(this._getGenderLabel());
            this._removeStandardHandlers(this._getGender());
            this._removeStandardHandlers(this._getIdentifierLabel());
            this._removeStandardHandlers(this._getIdentifier()); 
            $removeHandler(window,  "resize", this._resizeDelegate);     
       }
        
        NhsCui.Toolkit.Web.PatientBanner.callBaseMethod(this, 'dispose');
    },
    //
    // Initialize the data for autoellipsis
    //
    _initializeAutoEllipseData : function() {    
        // Zone one data
        this._preferredNameLabel = this._getPreferredNameLabel() ? this._getPreferredNameLabel().innerHTML : "";
        this._preferredName = this._getPreferredName() ? this._getPreferredName().innerHTML : "";
        this._zoneOneMinHeight = this._getZoneOne().offsetHeight;
        
        // Zone two data
        this._subsectionOneTitleLabel = $get(this.get_id() + "_subsectionOneTitle").firstChild.innerHTML;
        this._subsectionOneTitleData = $get(this.get_id() + "_subsectionOneTitle").lastChild.innerHTML;
        this._subsectionTwoTitleLabel = $get(this.get_id() + "_subsectionTwoTitle").firstChild.innerHTML;
        this._subsectionTwoTitleData = $get(this.get_id() + "_subsectionTwoTitle").lastChild.innerHTML;
        this._subsectionThreeTitleLabel = $get(this.get_id() + "_subsectionThreeTitle").firstChild.innerHTML != "&nbsp;"  ? $get(this.get_id() + "_subsectionThreeTitle").firstChild.innerHTML : "";
        this._subsectionFourTitleLabel = $get(this.get_id() + "_subsectionFourTitle").firstChild.innerHTML != "&nbsp;" ? $get(this.get_id() + "_subsectionFourTitle").firstChild.innerHTML : "";
        this._subsectionFiveTitleLabel = $get(this.get_id() + "_allergySummary").innerHTML;
    },    
    //
    // Auto ellipse preferred name
    //
    _autoEllipseZoneOne : function() {
        // AutoEllipse preferred name        
        if(this._getPreferredNameLabel() && this._getPreferredName())
        {
            var preferredNameWidth = this._getZoneOne().clientWidth;
            
            if($get(this.get_id() + "_patientImage"))
            {
                preferredNameWidth -= $get(this.get_id() + "_patientImage").clientWidth + NhsCui.Toolkit.Web.getPadding($get(this.get_id() + "_patientImage"));
                
                // setting the preferred name div max width so that image and name will be displayed in same line
                $get(this.get_id() + "_patientNameCell").style.maxWidth = preferredNameWidth + "px";
            }
            
            NhsCui.Toolkit.Web.autoEllipseLabelDataPair(this._getPreferredNameLabel(), this._getPreferredName(), this._preferredNameLabel, this._preferredName, preferredNameWidth);
        }
    },
    //
    // Auto ellipse Zone two title data
    //
    _autoEllipseZoneTwo : function() {
    
        this._getZoneTwo().style.tableLayout = "fixed";
    
        //AutoEllipse SubsectionOneTitle
        var subsectionOneTitleCell = $get(this.get_id() + "_subsectionOneTitle");
        var subsectionOneWidth = subsectionOneTitleCell.offsetWidth > subsectionOneTitleCell.clientWidth ? subsectionOneTitleCell.clientWidth : subsectionOneTitleCell.offsetWidth;        
        NhsCui.Toolkit.Web.autoEllipseLabelDataPair(subsectionOneTitleCell.firstChild, subsectionOneTitleCell.lastChild, this._subsectionOneTitleLabel, this._subsectionOneTitleData, subsectionOneWidth);
        
        //AutoEllipse SubsectionTwoTitle
        var subsectionTwoTitleCell = $get(this.get_id() + "_subsectionTwoTitle");
        var subsectionTwoWidth = subsectionTwoTitleCell.offsetWidth > subsectionTwoTitleCell.clientWidth ? subsectionTwoTitleCell.clientWidth : subsectionTwoTitleCell.offsetWidth;
        NhsCui.Toolkit.Web.autoEllipseLabelDataPair(subsectionTwoTitleCell.firstChild, subsectionTwoTitleCell.lastChild, this._subsectionTwoTitleLabel, this._subsectionTwoTitleData, subsectionTwoWidth);
        
        //AutoEllipse SubsectionThreeTitle
        if(this._subsectionThreeTitleLabel != "")
        {
            var subsectionThreeTitleCell = $get(this.get_id() + "_subsectionThreeTitle");        
            var subsectionThreeWidth = subsectionThreeTitleCell.offsetWidth > subsectionThreeTitleCell.clientWidth ? subsectionThreeTitleCell.clientWidth : subsectionThreeTitleCell.offsetWidth;
            NhsCui.Toolkit.Web.autoEllipseText(subsectionThreeTitleCell, this._subsectionThreeTitleLabel, subsectionThreeWidth);
        }
        
        //AutoEllipse SubsectionFourTitle
        if(this._subsectionFourTitleLabel != "")
        {
            var subsectionFourTitleCell = $get(this.get_id() + "_subsectionFourTitle");        
            var subsectionFourWidth = subsectionFourTitleCell.offsetWidth > subsectionFourTitleCell.clientWidth ? subsectionFourTitleCell.clientWidth : subsectionFourTitleCell.offsetWidth;
            NhsCui.Toolkit.Web.autoEllipseText(subsectionFourTitleCell, this._subsectionFourTitleLabel, subsectionFourWidth);
        }
        //AutoEllipse SubsectionFiveTitle
        
        var subsectionFiveTitleCell = $get(this.get_id() + "_subsectionFive");
        var allerySummaryCell = $get(this.get_id() + "_allergySummary");
        var allerySummary = allerySummaryCell.innerHTML;        
        var allergyIcon = $get(this.get_id() + "_allergyIcon");
        var allergyIconWidth = $get(this.get_id() + "_allergyIcon").offsetWidth + $get(this.get_id() + "_allergyIcon").offsetLeft;
                        
        var subsectionFiveWidth = subsectionFiveTitleCell.offsetWidth > subsectionFiveTitleCell.clientWidth ? subsectionFiveTitleCell.clientWidth : subsectionFiveTitleCell.offsetWidth;                
        subsectionFiveWidth -= allergyIconWidth;
        subsectionFiveWidth -= 5;       
        allerySummaryCell = $get(this.get_id() + "_allergySummary");
                
        NhsCui.Toolkit.Web.autoEllipseText(allerySummaryCell, this._subsectionFiveTitleLabel, subsectionFiveWidth);
        
        this._getZoneTwo().style.tableLayout = "auto";
    },
    //
    // Align Zone one content vertically
    //    
    _alignZoneOneContent : function() {      
         
        var zoneOneWidth = this._getZoneOne().clientWidth;
        var patientNameWidth = $get(this.get_id() + "_patientNameCell").clientWidth;
        
        // hiding and unhiding patient name so as to get exact width of patient data.
        $get(this.get_id() + "_patientNameCell").style.display = "none";
        var patientDataWidth = $get(this.get_id() + "_patientDataCell").clientWidth;
        $get(this.get_id() + "_patientNameCell").style.display = "";
        
        var patientImageWidth = 0;
        
        if($get(this.get_id() + "_patientImage"))
        {
            patientImageWidth = $get(this.get_id() + "_patientImage").clientWidth + NhsCui.Toolkit.Web.getPadding($get(this.get_id() + "_patientImage"));
        }
        
        var showAllInOneLine = true;
        if((patientImageWidth + patientNameWidth + patientDataWidth) > zoneOneWidth)
        {
            showAllInOneLine = false;
        }
        
        // reset the paddings and apply new
        $get(this.get_id() + "_patientNameCell").style.paddingTop = "";
        $get(this.get_id() + "_patientNameCell").style.paddingBottom = "";
        if(!this._getPreferredName() && showAllInOneLine == true)        
        {
            $get(this.get_id() + "_patientNameCell").style.paddingTop = Math.max(0, Math.floor((this._zoneOneMinHeight - $get(this.get_id() + "_patientNameCell").offsetHeight) / 2)) + "px";
            $get(this.get_id() + "_patientNameCell").style.paddingBottom = $get(this.get_id() + "_patientNameCell").style.paddingTop;
        }
           
        // reset the paddings and apply new
        $get(this.get_id() + "_patientDataCell").style.paddingTop = "";
        $get(this.get_id() + "_patientDataCell").style.paddingBottom = "";     
        if(!($get(this.get_id() + "_dod")) && showAllInOneLine == true)
        {
            $get(this.get_id() + "_patientDataCell").style.paddingTop = Math.max(0, Math.floor((this._zoneOneMinHeight - $get(this.get_id() + "_patientDataCell").offsetHeight) / 2)) + "px";
            $get(this.get_id() + "_patientDataCell").style.paddingBottom = $get(this.get_id() + "_patientDataCell").style.paddingTop;
        }      
    },
    //
    // Set zone two height
    //
    _setZoneTwoHeight : function() {
        // Adjust the height only when allergies are present
        if($get(this.get_id() + "_allergyDetailsPanel"))
        {
            this._getZoneTwoNonPermanent().style.height = "0px";
            var currDisplayState = this._getZoneTwoNonPermanent().style.display;
            this._getZoneTwoNonPermanent().style.display = "";
            
            var subsectionOneHeight = $get(this.get_id() + "_address").offsetHeight;
            var subsectionTwoHeight = $get(this.get_id() + "_contactDetails").offsetHeight;
            
            $get(this.get_id() + "_allergyDetailsPanel").style.height = (subsectionOneHeight > subsectionTwoHeight ? subsectionOneHeight : subsectionTwoHeight) + "px";
            $get(this.get_id() + "_subsectionThree").style.height = $get(this.get_id() + "_allergyDetailsPanel").style.height;
            $get(this.get_id() + "_subsectionFour").style.height = $get(this.get_id() + "_allergyDetailsPanel").style.height;
                        
            this._getZoneTwoNonPermanent().style.height = "";             
            this._getZoneTwoNonPermanent().style.display = currDisplayState;
            
        }
    },
    //
    // get / set url of the expand image for zoneTwo
    //
    get_dropDownImage : function() {
        return this._dropDownImage;
    }, 
    set_dropDownImage : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._dropDownImage !== value)
        {
            this._dropDownImage = value;
            this.raisePropertyChanged('dropDownImage');
        }
    },
    //
    // get / set url of the expand image for zoneTwo (when expanded)
    //
    get_collapseImage : function() {
        return this._collapseImage;
    },
    set_collapseImage : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._collapseImage !== value)
        {
            this._collapseImage = value;
            this.raisePropertyChanged('collapseImage');
        }
    },    
    //
    // get / set css class to apply when hovered over zone 2
    //
    get_zoneTwoHoverStyle : function() {
        return this._zoneTwoHoverStyle;
    },
    set_zoneTwoHoverStyle : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._zoneTwoHoverStyle !== value)
        {
            this._zoneTwoHoverStyle = value;
            this.raisePropertyChanged('zoneTwoHoverStyle');
        }
    },
    //    
    // whether zone 2 is expanded or not
    //
    get_zoneTwoExpanded : function() {
        return (this._getZoneTwoNonPermanent().style.display === "");
    },
    set_zoneTwoExpanded : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this.get_zoneTwoExpanded() !== value)
        {
            this._toggleZoneTwoExpand();
        }
    },
    //
    // Zone one label hover style. Applied only when the label has tooltip
    //
    get_zoneOneLabelWithTooltipHoverStyle : function() {
        return this._zoneOneLabelWithTooltipHoverStyle;
    },
    set_zoneOneLabelWithTooltipHoverStyle : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._zoneOneLabelWithTooltipHoverStyle !== value)
        {
            this._zoneOneLabelWithTooltipHoverStyle = value;
            this.raisePropertyChanged('zoneOneLabelWithTooltipHoverStyle');
        }
    },
    //
    // Zone one data hover style. Applied only when the label has tooltip
    //
    get_zoneOneDataWithTooltipHoverStyle : function() {
        return this._zoneOneDataWithTooltipHoverStyle;
    },
    set_zoneOneDataWithTooltipHoverStyle : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: String }
                    ]);
        if (e)
        {
            throw e;
        }
            
        if(this._zoneOneDataWithTooltipHoverStyle !== value)
        {
            this._zoneOneDataWithTooltipHoverStyle = value;
            this.raisePropertyChanged('zoneOneDataWithTooltipHoverStyle');
        }
    },    
    //    
    // register / deregister for expandComplete event
    //
    add_expandComplete : function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
   	    this.get_events().addHandler('expandComplete', handler);
    },
    remove_expandComplete : function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
    	this.get_events().removeHandler('expandComplete', handler);
    },
    //    
    // register / deregister for collapseComplete event
    //
    add_collapseComplete : function(handler) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "handler", type: Function }
                    ]);
        if (e)
        {
            throw e;
        }
        
    	this.get_events().addHandler('collapseComplete', handler);
    },    
    //
    // raise expand complete event
    //
   _raiseExpandComplete : function() {
    	var handlers = this.get_events().getHandler('expandComplete');
    	if (handlers)
    	{
    		handlers(this, Sys.EventArgs.Empty);
    	}
    },
    //
    // raise collapse complete event
    //
    _raiseCollapseComplete : function() {
    	var handlers = this.get_events().getHandler('collapseComplete');
    	if (handlers)
    	{
    		handlers(this, Sys.EventArgs.Empty);
    	}
    },
    //
    // add standard mouse & keyboard event handlers to supplied element
    //
    _ensureStandardHandlers : function(element)
    {
        if(!Array.contains(this._eventTargets, element))
        {
            element.tabIndex = 0;
            
            $addHandler(element, "click", this._clickDelegate);
            $addHandler(element, "focus", this._hoverDelegate);        
            $addHandler(element, "blur", this._unhoverDelegate);        
            $addHandler(element, "mouseover", this._hoverDelegate);        
            $addHandler(element, "mouseout", this._unhoverDelegate);
            $addHandler(element, "keypress", this._keypressDelegate);
            if(!element.onclick)
            {
                element.onclick = function() {};
            }
            Array.add(this._eventTargets, element);
        }       
    },
    //
    // remove standard mouse & keyboard event handlers from supplied element
    //
    _removeStandardHandlers : function(element)
    {
        if(Array.contains(this._eventTargets, element))
        {
            $removeHandler(element, "click", this._clickDelegate);
            $removeHandler(element, "focus", this._hoverDelegate);        
            $removeHandler(element, "blur", this._unhoverDelegate);        
            $removeHandler(element, "mouseover", this._hoverDelegate);        
            $removeHandler(element, "mouseout", this._unhoverDelegate);        
            $removeHandler(element, "keypress", this._keypressDelegate);
            element.tabIndex = -1;  
            Array.remove(this._eventTargets, element);
        }       
    },
    //
    // handle click event
    //
    _clickHandler : function(e) {
        var target = this._getEventTarget(e.target);        
        if(target === this._getZoneTwoPermanent())
        {
           this._toggleZoneTwoExpand();
        }
        else if(target === this._getGender() || target === this._getIdentifier() || target === $get(this.get_id() + "_viewAllAddresses") || target === $get(this.get_id() + "_viewAllContactDetails") || target === $get(this.get_id() + "_viewAllergyRecord")) 
        {
           target.onclick();
        }        
    },
    //
    // handle keypress event
    //
    _keypressHandler : function(e) {
        if(!e.altKey && !e.ctrlKey && e.charCode === Sys.UI.Key.enter)
        {
            // treat enter as click
            this._clickHandler(e);
            e.preventDefault();
        }
    },
    //
    // handle hover over element
    //
    _hoverHandler : function(e) {
        this._applyHoverStyle(this._getEventTarget(e.target), Sys.UI.DomElement.addCssClass);
    },
    //
    // handle unhover over element
    //
    _unhoverHandler : function(e) {
        this._applyHoverStyle(this._getEventTarget(e.target), Sys.UI.DomElement.removeCssClass);
    },    
    //
    // resize window/element handler
    //
    _resizeHandler : function() {    
        this._currSize = Sys.UI.DomElement.getBounds(this._getZoneOne());
        if(this._prevSize == null || this._prevSize.height != this._currSize.height || this._prevSize.width != this._currSize.width)
        {
            if(this._prevSize != null)
            {
                $removeHandler(window,  "resize", this._resizeDelegate); 
            }
    
            this._autoEllipseZoneTwo();
            this._autoEllipseZoneOne();
            this._alignZoneOneContent();        
            this._setZoneTwoHeight();   
            
            if($get(this.get_id() + "_patientNameCell").offsetTop == $get(this.get_id() + "_patientDataCell").offsetTop && $get(this.get_id() + "_patientNameCell").offsetTop == 0)
            {
                if($get(this.get_id() + "_patientNameCell").style.paddingTop == "" && !this._getPreferredName())
                {
                    $get(this.get_id() + "_patientNameCell").style.paddingTop = Math.floor((this._getZoneOne().offsetHeight - $get(this.get_id() + "_patientNameCell").offsetHeight)/2) + "px";
                }
                if($get(this.get_id() + "_patientDataCell").style.paddingTop == "" && !$get(this.get_id() + "_dod"))
                {
                    $get(this.get_id() + "_patientDataCell").style.paddingTop = Math.floor((this._getZoneOne().offsetHeight - $get(this.get_id() + "_patientDataCell").offsetHeight)/2) + "px";                        
                }
            } 
                    
            this._prevSize = Sys.UI.DomElement.getBounds(this._getZoneOne());
            this._currSize = this._prevSize;
            
            $addHandler(window,  "resize", this._resizeDelegate); 
        }      
    },    
    //
    // Update the image shown on the zoneTwo title (depending on whether it has
    // the focus, etc)
    //
    _refreshZoneTwoExpandImage : function() {
        var image = this._getExpandImage();
        var imageUrl = null;
        
        if(this.get_zoneTwoExpanded())
        {
            imageUrl = this.get_collapseImage();
        }
        
        if(!imageUrl || imageUrl.length === 0)
        {
            imageUrl = this.get_dropDownImage();
        }
        
        image.src = imageUrl;
    },
    //
    // apply / remove hover style using supplied function
    //
    _applyHoverStyle : function(element, func) {
        if(element === this._getZoneTwoPermanent())
        {
            func(this._getZoneTwoPermanent(), this.get_zoneTwoHoverStyle());
        }
        else if(element === this._getGenderLabel() || element === this._getIdentifierLabel())
        {
            if(element.title != "")
            {
                func(element, this.get_zoneOneLabelWithTooltipHoverStyle())
            }            
        }
        else if(element === this._getGender() || element === this._getIdentifier())
        {
            if(element.title != "")
            {
                func(element, this.get_zoneOneDataWithTooltipHoverStyle())
            }            
        }
    },
    //
    // get event target given child element
    //
    _getEventTarget : function(child) {
        var element = child;

        while(element)
        {
            if(Array.contains(this._eventTargets, element))
            {
                return element;
            }
            
            element = element.parentNode;
       }
        
        return null;
    },
    //
    // get the expand button element
    //
    _getExpandImage : function() {
        return $get(this.get_id() + "_expandImage");
    },
    //
    // get the zoneOne element
    //
    _getZoneOne : function() {
        return $get(this.get_id() + "_zoneOne");
    },
    //
    // get Gender label element
    //
    _getGenderLabel : function() {
        return $get(this.get_id() + "_genderLabel");
    },
    //
    // get Gender element
    //
    _getGender : function() {
        return $get(this.get_id() + "_gender");
    },
    //
    // get Identifier label element
    //
    _getIdentifierLabel : function() {
        return $get(this.get_id() + "_identifierLabel");
    },
    //
    // get Identifier element
    //
    _getIdentifier : function() {
        return $get(this.get_id() + "_identifier");
    },
    //
    // get Preferred Name element
    //
    _getPreferredName : function() {
        return $get(this.get_id() + "_preferredName");
    },
    //
    // get Preferred Name Label element
    //
    _getPreferredNameLabel : function() {
        return $get(this.get_id() + "_preferredNameLabel");
    },
    //
    // get the zoneTwo element
    //
    _getZoneTwo : function() {
        return $get(this.get_id() + "_zoneTwo");
    },
    //
    // get the permanent zoneTwo element
    //
    _getZoneTwoPermanent : function() {
        return $get(this.get_id() + "_zoneTwoPermanent");
    },
    // get the non permanent zoneTwo element
    //
    _getZoneTwoNonPermanent : function() {
        return $get(this.get_id() + "_zoneTwoNonPermanent");
    },
    //
    // get the links row in non permanent zoneTwo element
    //
    _getZoneTwoNonPermanentLinksRow : function() {
        return $get(this.get_id() + "_zoneTwoNonPermanentLinksRow");
    },
    //
    // get element used to hold client state
    //
    _getClientState : function() {
        return $get(this.get_id() + "_clientState");
    },    
    //
    // toggle expand / collapse of zone 2
    //
    _toggleZoneTwoExpand : function() {    
        if(!this._timerId)
        {
            var doExpand = !this.get_zoneTwoExpanded();
            var delegate = (doExpand ? this._doExpandDelegate : this._doCollapseDelegate);
            
            this._timerId = window.setInterval(delegate, this._timerInterval);
         }
    },
    //
    // handle tick of timer used to expand zone 2
    //
    _doExpandHandler : function() {
        var zoneTwoPermanent = this._getZoneTwoPermanent();
        var zoneTwoNonPermanent = this._getZoneTwoNonPermanent()
        var zoneTwoNonPermanentLinksRow = this._getZoneTwoNonPermanentLinksRow();
        
        zoneTwoNonPermanent.style.display="";
        zoneTwoNonPermanentLinksRow.style.display="";
        
        window.clearInterval(this._timerId);
        this._timerId = null;
        if(this._getClientState())
        {
            this._getClientState().value = "1";
        }
        
        this._refreshZoneTwoExpandImage();
        this._raiseExpandComplete();
        zoneTwoPermanent.focus();        
    },
    //
    // handle tick of timer used to collapse zone 2
    //
    _doCollapseHandler : function() {        
        var zoneTwoPermanent = this._getZoneTwoPermanent();
        var zoneTwoNonPermanent = this._getZoneTwoNonPermanent();
        var zoneTwoNonPermanentLinksRow = this._getZoneTwoNonPermanentLinksRow();
        
        zoneTwoNonPermanent.style.display="none";
        zoneTwoNonPermanentLinksRow.style.display="none";
        
        window.clearInterval(this._timerId);
        this._timerId = null;
        if(this._getClientState())
        {
            this._getClientState().value = "";
        }
                
        this._refreshZoneTwoExpandImage();
        this._raiseCollapseComplete();
        zoneTwoPermanent.focus();       
    },
    //
    // set-up 'tooltips' to appear when zone 1 / zone 2 get focus
    //
    _initializeFocusTooltips : function() {
        if(NhsCui.Toolkit.Web.FocusToolTipBehavior)
        {
            this._zoneOneFocusTooltip = $create(NhsCui.Toolkit.Web.FocusToolTipBehavior, null, null, null, this._getZoneOne());
            this._zoneTwoFocusTooltip = $create(NhsCui.Toolkit.Web.FocusToolTipBehavior, null, null, null, this._getZoneTwoPermanent());
            $create(NhsCui.Toolkit.Web.FocusToolTipBehavior, null, null, null, this._getGenderLabel())
        }
    },
    //
    // set-up fix for dropdown not covering windowed controls on IE6
    //
    _initializeZIndexFix : function() {
        if(NhsCui.Toolkit.Web.ZIndexFixBehavior)
        {
            this._zIndexFix = $create(NhsCui.Toolkit.Web.ZIndexFixBehavior, null, null, null, this._getZoneTwoNonPermanent());
        }
    }
};

NhsCui.Toolkit.Web.PatientBanner.descriptor = {
    properties: [ { name: 'dropDownImage', type: String },
                    { name: 'collapseImage', type: String },
                    { name: 'zoneTwoHoverStyle', type: String },
                    { name: 'zoneOneLabelWithTooltipHoverStyle', type: String},
                    { name: 'zoneOneDataWithTooltipHoverStyle', type: String}],
    events: [ { name: 'collapseComplete' },
              { name: 'expandComplete' },
              { name: 'click' }  ]
};

NhsCui.Toolkit.Web.PatientBanner.registerClass('NhsCui.Toolkit.Web.PatientBanner', Sys.UI.Control);
