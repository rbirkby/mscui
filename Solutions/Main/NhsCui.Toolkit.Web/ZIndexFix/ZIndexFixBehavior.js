//-----------------------------------------------------------------------
// <copyright file="ZIndexFixBehavior.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side code for fix for IE (pre version 7) windowed control 
// z-index issue</summary>
//-----------------------------------------------------------------------
Type.registerNamespace('NhsCui.Toolkit.Web');

NhsCui.Toolkit.Web.ZIndexFixBehavior = function(element) { 
    NhsCui.Toolkit.Web.ZIndexFixBehavior.initializeBase(this, [element]);

};

NhsCui.Toolkit.Web.ZIndexFixBehavior.prototype = {
    //
    // Initialize
    //
    initialize : function() {
        NhsCui.Toolkit.Web.ZIndexFixBehavior.callBaseMethod(this, 'initialize');
        
        // for IE prior to version 7 apply a fix to stop windowed controls i.e. select elements
        // from showing through the dropdown
        if(Sys.Browser.agent === Sys.Browser.InternetExplorer && Sys.Browser.version < 7)
        {
            // ie specific code to dynamically insert an iframe into the document which will
            // be placed inbetween our target control and any windowed controls that might show through
            var element = this.get_element();
            if(element)
            {
                var iframeId = this.get_id() + "_zindex_fix";
                // place just behind the target control in the z-order
                var elementZIndex = parseInt(element.runtimeStyle.zIndex, 10);
                var iFrameZIndex = (isNaN(elementZIndex) ? 0 : Math.max(elementZIndex - 1, 0));
                var html = "<iframe id='" + iframeId + "' style='display:none;position:absolute;z-index:" +
                         iFrameZIndex + ";filter:progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0)' " +
                         "src='javascript:\"<html></html>\";' frameborder='0' scrolling='no'/>";
                document.body.insertAdjacentHTML("beforeEnd", html);
                this._iframe = $get(iframeId);
                // track any resizes of the target control so we can keep the iframe insync
                this._onPropertyChangeDelegate = Function.createDelegate(this, this._onPropertyChangeHandler);
                $addHandler(element, "propertychange", this._onPropertyChangeDelegate);        
            }
       }
        
    },
    //
    // Clean-up
    //
    dispose : function() {
        NhsCui.Toolkit.Web.ZIndexFixBehavior.callBaseMethod(this, 'dispose');
        
        var element = this.get_element();
        if(this.get_isInitialized() && element)
        {
            $removeHandler(element, "propertychange", this._onPropertyChangeDelegate);
        }
        
    },
    //
    // handle change in target's properties
    //
    _onPropertyChangeHandler : function() {
        var element = this.get_element();
        if(element && this._iframe)
        {
            var bounds = Sys.UI.DomElement.getBounds(element);
            // keep iframe insync with the target element
            this._iframe.style.display = element.style.display;
            this._iframe.style.left = bounds.x + "px";
            this._iframe.style.top = bounds.y + "px";
            this._iframe.style.width = bounds.width + "px";
            this._iframe.style.height = bounds.height + "px";
        }
    }

};

NhsCui.Toolkit.Web.ZIndexFixBehavior.registerClass('NhsCui.Toolkit.Web.ZIndexFixBehavior', Sys.UI.Behavior);