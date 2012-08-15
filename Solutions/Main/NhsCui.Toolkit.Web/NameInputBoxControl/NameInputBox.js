//-----------------------------------------------------------------------
// <copyright file="NameInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NHS Name InputBox</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

NhsCui.Toolkit.Web.NameInputBox = function(element) 
{
    NhsCui.Toolkit.Web.NameInputBox.initializeBase(this, [element]);

    this._familyNameInputBoxId = null;
    this._clientState = null;
    this._disposed = false;

    this._familyNameInputBoxChangeDelegate = null;          
};

NhsCui.Toolkit.Web.NameInputBox.prototype = 
{  
    // properties
    get_familyNameInputBoxId : function() 
    {
        /// <summary>Gets the id for the familyName input box</summary>
        return this._familyNameInputBoxId;
    },
    set_familyNameInputBoxId : function(value) 
    {
        /// <summary>Sets the id for the familyName input box</summary>
        this._familyNameInputBoxId=value;
    },
    
    dispose : function() 
    {
        if (this._disposed === true) 
        {
            return;
        }
        
        this._disposed = true;
    
        $clearHandlers(this._familyNameInputBox);
    
        NhsCui.Toolkit.Web.NameInputBox.callBaseMethod(this, 'dispose');
    },
    
    initialize : function() 
    {
        this._familyNameInputBox = $get(this.get_familyNameInputBoxId());
                        
        if (this._familyNameInputBoxChangeDelegate === null)
        {
            this._familyNameInputBoxChangeDelegate = Function.createDelegate(this, this._familyNameChangeHandler);          
        }
        $addHandler(this._familyNameInputBox, 'change', this._familyNameInputBoxChangeDelegate);
        
        NhsCui.Toolkit.Web.NameInputBox.callBaseMethod(this, 'initialize');
    },
    
    _familyNameChangeHandler : function(e) 
    {
        //If not already done uppercase the familyName
        if (e.target.value !== e.target.value.toUpperCase())
        {
            e.target.value = e.target.value.toUpperCase();
        }
    }
};

NhsCui.Toolkit.Web.NameInputBox.registerClass('NhsCui.Toolkit.Web.NameInputBox', Sys.UI.Control);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

