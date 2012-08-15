//-----------------------------------------------------------------------
// <copyright file="GenderInputBox.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NHS Gender InputBox</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

//=============================================================================
// Enums
//=============================================================================
var PatientGender = function()
{
    /// <summary>
    /// Gender is a public enumeration of supported genders
    /// </summary>
    throw Error.invalidOperation();
};

PatientGender.prototype = 
{
    /// <summary>
    /// Enum to indicate male gender.
    /// </summary>
    Male : 0,

    /// <summary>
    /// Enum to indicate female gender.
    /// </summary>
    Female : 1,

    /// <summary>
    /// Enum to indicate gender is not known.
    /// </summary>
    NotKnown : 2,

    /// <summary>
    /// Enum to indicate gender is not specified.
    /// </summary>
    NotSpecified : 3
};

PatientGender.registerEnum("PatientGender");

NhsCui.Toolkit.Web.GenderInputBox = function(element) 
{
    NhsCui.Toolkit.Web.GenderInputBox.initializeBase(this, [element]);
    
    this._value = PatientGender.NotKnown;
    
};

NhsCui.Toolkit.Web.GenderInputBox.prototype = 
{  
    initialize : function() 
    {
    },
    
    get_Value : function() 
    {
        /// <value type="Number" integer="true">
        /// PatientGender enum value
        /// </value>
        return this._value;
    },
    set_Value : function(value) 
    {
        if (this._value != value) 
        {
            this._value = value;
            this.raisePropertyChanged('Value');
        }
    }
};

NhsCui.Toolkit.Web.GenderInputBox.registerClass("NhsCui.Toolkit.Web.GenderInputBox", AjaxControlToolkit.BehaviorBase);
