//-----------------------------------------------------------------------
// <copyright file="MedicationNameLabelBehavior.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>29/01/2007 08:20:29</date>
// <summary>Script to create a client-side version of the MedicationName Label.  Given a parent control, it will add the required children and set the data using a similar MedicationName JSON data</summary>
//-----------------------------------------------------------------------

Type.registerNamespace('NhsCui.Toolkit.Web');


/// <summary>
/// Script to create a lient-side version of the MedicationName Label.  Given a parent control, it will add the required 
/// Children and set the data using a similar MedicationName structure
/// </summary>
/// <remarks>
/// TODO What if no image is provided - how to space it so that an image space is display? Don't think this should be part of the MedicationNameLabelBehavior within the actual grid
/// </remarks>

NhsCui.Toolkit.Web.MedicationNameLabelBehavior = function(element) { 
    NhsCui.Toolkit.Web.MedicationNameLabelBehavior.initializeBase(this, [element]);
    this._showGraphics = true;
        
    // Handlers for the events
    this._onClickHandler = null;               
};
  
NhsCui.Toolkit.Web.MedicationNameLabelBehavior.prototype = {
    /// <summary>
    /// Initialize
    /// </summary>
    initialize : function() {
        NhsCui.Toolkit.Web.MedicationNameLabelBehavior.callBaseMethod(this, 'initialize');                
        var element = this.get_element();                
        this._onClickHandler = Function.createDelegate(this, this._onClick);        
        $addHandler(element, 'click', this._onClickHandler);
        this.update();        
    },
    
    /// <summary>
    /// Clean up
    /// </summary>
    dispose : function() {                
        var element = this.get_element();
        if (this._onClickHandler) {
                $clearHandlers(element);
            }
            
        this._onClickHandler = null;
        
        NhsCui.Toolkit.Web.MedicationNameLabelBehavior.callBaseMethod(this, 'dispose');        
    },    

    /// <summary>
    /// Get Show Graphics Flag
    /// </summary>        
   get_showGraphics : function() {
        return this._showGraphics;
    },
    
    /// <summary>
    /// Set Show Graphics Flag
    /// </summary>            
    set_showGraphics : function(value) {
        var e = Function._validateParams(arguments, 
                    [
                        { name: "value", type: Boolean}
                    ]);
        if (e)
        {
            throw e;
        }
    
        if (value !== this._showGraphics)
        {
            this._showGraphics = value;
            
            if (this.get_isInitialized())
            {                    
                this.update();
            }
            this.raisePropertyChanged('ShowGraphics');
        }
    },                         
    
    //    
    // register / deregister for click event
    //
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
    },
        
    get_click : function() {
        return this.get_events().getHandler("click");
    },
    set_click : function(value) {
        if (value && (0 < value.length)) {
            var func = CommonToolkitScripts.resolveFunction(value);
            if (func) { 
                this.add_click(func);
            } else {
                throw Error.argumentType('value', typeof(value), 'Function', 'click handler not a function, function name, or function text.');
            }
        }
    },   
                                           
    /// <summary>
    /// Update Element
    /// </summary>    
    update : function()
    {  
        // Find the Graphics (images)
        var indicatorGraphic = this._findImage("_IndicatorGraphic");
        var criticialAlertGraphic = this._findImage("_CriticalAlertGraphic");
        
        // Show/Hide dependant on the ShowGraphics flag
        if (indicatorGraphic !== null)
        {        
            indicatorGraphic.style.display = this._showGraphics ? '' : 'none';
        }
        
        if (criticialAlertGraphic != null)
        {
            criticialAlertGraphic.style.display = this._showGraphics ? '' : 'none';
        }                                
    },     
    
    /// <summary>
    /// Find the Critical Alert or Indicator Graphic controls
    /// </summary>    
    _findImage: function(name)
    {
        var element = this.get_element();
        // Search the child controls for an image with id ending with the name
        var nodeIndex =0;        
        var childrenCount = element.childNodes.length;
        
        if (childrenCount > 0)
        {
            do
            {
                var childNode = element.childNodes[nodeIndex];                
                if (childNode && childNode.nodeType !== 3 && childNode.tagName.toLowerCase() == "img" && childNode.id.endsWith(name) === true)
                {
                    return childNode;
                }
                nodeIndex ++;
            } while (nodeIndex < childrenCount);
        }
        
        return null;
    }
};

NhsCui.Toolkit.Web.MedicationNameLabelBehavior.registerClass('NhsCui.Toolkit.Web.MedicationNameLabelBehavior', AjaxControlToolkit.BehaviorBase);