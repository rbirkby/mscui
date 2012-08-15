//-----------------------------------------------------------------------
// <copyright file="Common.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>29-Jan-2007</date>
// <summary>Common client-side javascript for NhsCui.Toolkit.Web</summary>
//-----------------------------------------------------------------------
Type.registerNamespace("NhsCui.Toolkit.Web");

var NhsError = NhsCui.Toolkit.Web.NhsError = function() {
};
NhsError.formatException = function(message) {
    /// <param name="message" type="String" optional="true" mayBeNull="true"></param>
    /// <returns></returns>
    var e = Function._validateParams(arguments, [
        {name: "message", type: String, mayBeNull: true, optional: true}
    ]);
    if (e) throw e;

    var displayMessage = "NhsCui.Toolkit.Web.FormatException: " + message;

    var ex = Error.create(displayMessage, {name: 'NhsCui.Toolkit.Web.FormatException'});
    ex.popStackFrame();
    return ex;
};

NhsCui.Toolkit.Web.autoEllipseText = function(element, text, width)
{
   width -= NhsCui.Toolkit.Web.getPadding(element);
   element.innerHTML = '<span id="ellipsisSpan" style="white-space:nowrap;">' + text + '</span>';
   var inSpan = document.getElementById('ellipsisSpan');
   if(inSpan.offsetWidth > width)
   {
      var i = 1;
      inSpan.innerHTML = '';
      while(inSpan.offsetWidth < width && i < text.length)
      {
        inSpan.innerHTML = text.substr(0,i) + '...';         
        
        if(inSpan.offsetWidth > width)
        {
            inSpan.innerHTML = text.substr(0,i-1) + '...';
            break;
        }
        
        i++;          
      }
      
      element.innerHTML = inSpan.innerHTML; 
   }
   else
   {
        element.innerHTML = text;
   }
}

NhsCui.Toolkit.Web.autoEllipseLabelDataPair = function(labelElement, dataElement, labelText, dataText, width)
{
    // substracting 5 pixels to accomodate browser variances
    width-= 5;
    var element = document.createElement("SPAN");
    element.style.width = "1px";
    element.style.whiteSpace = "nowrap";
    element.innerHTML = labelText;
    
    labelElement.appendChild(element);    
    var labelWidth = element.offsetWidth;
    element.parentNode.removeChild(element);
    labelWidth = labelWidth > labelElement.offsetWidth ? labelWidth : labelElement.offsetWidth;
    var labelPadding = NhsCui.Toolkit.Web.getPadding(labelElement);
    if(labelWidth > width)
    {    
        NhsCui.Toolkit.Web.autoEllipseText(labelElement, labelText, width - labelElement.offsetLeft - labelPadding);
    }
    else
    {
        labelElement.innerHTML = labelText;
    }
    
    element.innerHTML = dataText;
    dataElement.appendChild(element);
    var dataWidth = element.offsetWidth;
    element.parentNode.removeChild(element);
    dataWidth = dataWidth > dataElement.offsetWidth ? dataWidth : dataElement.offsetWidth;
    var dataPadding = NhsCui.Toolkit.Web.getPadding(dataElement);
    
    if(dataWidth > width || (dataWidth + labelWidth) > width)
    {    
        NhsCui.Toolkit.Web.autoEllipseText(dataElement, dataText, width - labelWidth - labelElement.offsetLeft - dataPadding);            
    }
    else
    {
        dataElement.innerHTML = dataText;
    }
}

NhsCui.Toolkit.Web.getPadding = function(element)
{
    var padding = 0;
    if(element.currentStyle)
    {
        padding = parseInt(element.currentStyle.paddingLeft) + parseInt(element.currentStyle.paddingRight);
    }
    else
    {        
        padding = parseInt(document.defaultView.getComputedStyle(element, null).paddingLeft) + parseInt(document.defaultView.getComputedStyle(element, null).paddingRight);
    }
    
    return padding;
}

var RegexOptions={};
RegexOptions.IgnoreCase="i";
RegexOptions.Global="g";
