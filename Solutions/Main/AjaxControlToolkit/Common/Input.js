// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
Type.registerNamespace('AjaxControlToolkit.Input');

AjaxControlToolkit.Input.MouseRegion = function(x, y, width, height) {
    /// <summary>Defines a region on the document to track mouse activity without a DOM Element</summary>
    /// <param name="x" type="Number" integer="true"></param>
    /// <param name="y" type="Number" integer="true"></param>
    /// <param name="width" type="Number" integer="true"></param>
    /// <param name="height" type="Number" integer="true"></param>
    
    AjaxControlToolkit.Input.MouseRegion.initializeBase(this);
    
    this._x = x || 0;
    this._y = y || 0;
    this._width = width || 0;
    this._height = height || 0;
    this._active = false;
    this._enabled = true;
    this._mouseX = -1;
    this._mouseY = -1;
    this._dispatch$delegate = Function.createDelegate(this, this.dispatch);
}
AjaxControlToolkit.Input.MouseRegion.prototype = {
    
    add_click : function(handler) { 
        /// <summary>Adds a handler to the click event</summary>
        /// <param name="handler" type="Function"></param>
        
        this.get_events().addHandler("click", handler); 
    },
    remove_click : function(handler) { 
        /// <summary>Removes a handler from the click event</summary>
        /// <param name="handler" type="Function"></param>
        
        this.get_events().removeHandler("click", handler); 
    },
    
    add_mouseDown : function(handler) { 
        /// <summary>Adds a handler to the mouseDown event</summary>
        /// <param name="handler" type="Function"></param>
        
        this.get_events().addHandler("mousedown", handler); 
    },
    remove_mouseDown : function(handler) { 
        /// <summary>Removes a handler from the mouseDown event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().removeHandler("mousedown", handler); 
    },
    
    add_mouseUp : function(handler) { 
        /// <summary>Adds a handler to the mouseUp event</summary>
        /// <param name="handler" type="Function"></param>
        
        this.get_events().addHandler("mouseup", handler); 
    },
    remove_mouseUp : function(handler) { 
        /// <summary>Removes a handler from the mouseUp event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().removeHandler("mouseup", handler); 
    },
    
    add_mouseOver : function(handler) { 
        /// <summary>Adds a handler to the mouseOver event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().addHandler("mouseover", handler); 
    },
    remove_mouseOver : function(handler) { 
        /// <summary>Removes a handler from the mouseOver event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().removeHandler("mouseover", handler); 
    },
    
    add_mouseOut : function(handler) { 
        /// <summary>Adds a handler to the mouseOut event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().addHandler("mouseout", handler); 
    },
    remove_mouseOut : function(handler) { 
        /// <summary>Removes a handler from the mouseOut event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().removeHandler("mouseout", handler); 
    },
    
    add_mouseMove : function(handler) { 
        /// <summary>Adds a handler to the mouseMove event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().addHandler("mousemove", handler); 
    },
    remove_mouseMove : function(handler) { 
        /// <summary>Removes a handler from the mouseMove event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().removeHandler("mousemove", handler); 
    },
    
    add_contextMenu : function(handler) { 
        /// <summary>Adds a handler to the contextMenu event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().addHandler("contextmenu", handler); 
    },
    remove_contextMenu : function(handler) { 
        /// <summary>Removes a handler from the contextMenu event</summary>
        /// <param name="handler" type="Function"></param>

        this.get_events().removeHandler("contextmenu", handler); 
    },
    
    raiseEvent : function(e) {
        /// <summary>Raises an event based on a DomEvent</summary>
        /// <param name="handler" type="Sys.UI.DomEvent"></param>

        if (!this.get_enabled()) 
            return;
            
        var eh = this.get_events().getHandler(e.type);
        if (eh) eh(e);
    },
    
    get_enabled : function() { return this._enabled; },
    set_enabled : function(value) { this._enabled = value; },
    
    get_x : function() { return this._x; },
    set_x : function(value) { this._x = value; },
    
    get_mouseX : function() { return this._mouseX; },
    get_mouseY : function() { return this._mouseY; },
    get_mouseLocation : function() { return {x:this._mouseX,y:this._mouseY};},
    
    get_y : function() { return this._y; },
    set_y : function(value) { this._y = value; },
    
    get_width : function() { return this._width; },
    set_width : function(value) { this._width = value; },
    
    get_height : function() { return this._height; },
    set_height : function(value) { this._height = value; },
    
    get_location : function() { return {x:this._x,y:this._y};},
    set_location : function(value) { this._x = value.x; this._y = value.y; },
    
    get_size : function() { return {width:this._width,height:this._height};},
    set_size : function(value) { this._width = value.width; this._height = value.height; },
    
    get_bounds : function() { return {x:this._x,y:this._y,width:this._width,height:this._height};},
    set_bounds : function(value) { this.set_location(value); this.set_size(value); },

    initialize : function() {
        AjaxControlToolkit.Input.MouseRegion.callBaseMethod(this, "initialize");
        $common.addHandlers(document, {
            "mousemove":this._dispatch$delegate,
            "mouseup":this._dispatch$delegate,
            "mousedown":this._dispatch$delegate,
            "click":this._dispatch$delegate,
            "contextmenu":this._dispatch$delegate
        });
    },
    dispose : function() {
        $common.removeHandlers(document, {
            "mousemove":this._dispatch$delegate,
            "mouseup":this._dispatch$delegate,
            "mousedown":this._dispatch$delegate,
            "click":this._dispatch$delegate,
            "contextmenu":this._dispatch$delegate
        });
        AjaxControlToolkit.Input.MouseRegion.callBaseMethod(this, "dispose");
    },
    
    test : function(x, y) {
        return $common.containsPoint(this.get_bounds(), x, y);
    },
    testLast : function() {
        return this.test(this._mouseX, this._mouseY);
    },
    dispatch : function(e) {
        this._mouseX = e.clientX;
        this._mouseY = e.clientY;
        if (this.test(this._mouseX, this._mouseY)) {
            if(e.type == 'mousemove') {
                if(!this._active) {
                    var ev = new Sys.UI.DomEvent(e.rawEvent);
                    ev.type = "mouseover";
                    this.raiseEvent(ev);
                    this._active = true;
                }
            } 
            this.raiseEvent(e);
        }
        else if(this._active && (e.type == "mouseover" || e.type == "mousemove"))
        {
            var ev = new Sys.UI.DomEvent(e.rawEvent);
            ev.type = "mouseout";
            this.raiseEvent(ev);
            this._active = false;
        }
    },
    updateWith : function(elt) { this.set_bounds($common.getBounds(elt)); }
}
AjaxControlToolkit.Input.MouseRegion.registerClass("AjaxControlToolkit.Input.MouseRegion", Sys.Component);

