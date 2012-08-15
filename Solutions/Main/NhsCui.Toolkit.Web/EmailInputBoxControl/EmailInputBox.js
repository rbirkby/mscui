//-----------------------------------------------------------------------
// <copyright file="EmailInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>Client-side javascript for NHS Email InputBox</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

NhsCui.Toolkit.Web.EmailInputBox = function(element) 
{
    NhsCui.Toolkit.Web.EmailInputBox.initializeBase(this, [element]);

    // RegExp for email address matching
    this._emailRegExp = new RegExp("^[A-Z0-9._%-]+@(?:[A-Z0-9-]+\\.)+[A-Z]{2,4}$", "gi");

    // event delegates
    this._blurDelegate = Function.createDelegate(this, this._blurHandler);

    this._disposed = false;
};

NhsCui.Toolkit.Web.EmailInputBox.prototype = 
{  
    initialize : function() 
    {
        var elt = this.get_element();

        $addHandler(elt, "blur", this._blurDelegate);
    },
    
    dispose : function() 
    {
        if (this._disposed === true) 
        {
            return;
        }
        
        this._disposed = true;

        var elt = this.get_element();
    
        $removeHandler(elt, "blur", this._blurDelegate);
    
        NhsCui.Toolkit.Web.EmailInputBox.callBaseMethod(this, 'dispose');
    },

    get_value : function() 
    {
        /// <value type="Number" integer="true">
        /// Email string value
        /// </value>
        return this.get_element().value;
    },
    set_value : function(value) 
    {
        if (this.get_element().value != value) 
        {
            if (this._emailRegExp.exec(value))
            {
                this.get_element().value = value;
                this.raisePropertyChanged('Value');
            }
        }
    },
    
    _blurHandler : function(e) 
    {
        this.set_value(this.get_element().value);
    }
};

NhsCui.Toolkit.Web.EmailInputBox.registerClass("NhsCui.Toolkit.Web.EmailInputBox", AjaxControlToolkit.BehaviorBase);
