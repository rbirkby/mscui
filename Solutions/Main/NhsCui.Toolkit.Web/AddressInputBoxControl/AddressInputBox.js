//-----------------------------------------------------------------------
// <copyright file="AddressInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>16-Aug-2007</date>
// <summary>The control used to enter an address. </summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

NhsCui.Toolkit.Web.AddressInputBox = function(element) {
    /// <summary>AddressInputBox constructor</summary>
    NhsCui.Toolkit.Web.AddressInputBox.initializeBase(this, [element]);

    this._postcodeInputBoxId= null;
    this._clientState=null;
    this._disposed=false;
            
    // Initialise all the Delegates
    this._postcodeInputBoxChangeDelegate = null;          
};

NhsCui.Toolkit.Web.AddressInputBox.prototype={
    // properties
    get_postcodeInputBoxId : function() {
        /// <summary>Gets the id for the postcode input box</summary>
        return this._postcodeInputBoxId;
    },
    set_postcodeInputBoxId : function(value) {
        /// <summary>Sets the id for the postcode input box</summary>
        this._postcodeInputBoxId=value;
    },
    dispose : function() {
        if(this._disposed===true) return;
        this._disposed=true;
    
        $clearHandlers(this._postCodeInputBox);
    
        NhsCui.Toolkit.Web.AddressInputBox.callBaseMethod(this, 'dispose');
    },
    initialize : function() {
        this._postCodeInputBox = $get(this.get_postcodeInputBoxId());
                        
        if (this._postcodeInputBoxChangeDelegate===null){
            this._postcodeInputBoxChangeDelegate = Function.createDelegate(this, this._postCodeChangeHandler);          
        }
        $addHandler(this._postCodeInputBox, 'change',this._postcodeInputBoxChangeDelegate);
        
        NhsCui.Toolkit.Web.AddressInputBox.callBaseMethod(this, 'initialize');
    },
    _postCodeChangeHandler : function(e) {
        
        //If not already done uppercase the postcode
        if (e.target.value !== e.target.value.toUpperCase())
        {
            e.target.value = e.target.value.toUpperCase();
        }
    }
};

NhsCui.Toolkit.Web.AddressInputBox.registerClass('NhsCui.Toolkit.Web.AddressInputBox', Sys.UI.Control);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
