//-----------------------------------------------------------------------
// <copyright file="TimeSpanLabelDesigner.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>02-Feb-2007</date>
// <summary>Designer for TimeSpanLabel.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Web.UI.Design;
    using System.ComponentModel;
    using System.Globalization;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Designer for TimeSpanLabel.
    /// </summary>
    internal class TimeSpanLabelDesigner : ControlDesigner
    {
        /// <summary>
        /// Initializes the control designer and loads the specified component. 
        /// </summary>
        /// <param name="component">The control being designed. </param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            TimeSpanLabel control = (TimeSpanLabel)component;

            // ensure from and to attributes are set
            this.EnsureAttributeDefault("From", control.From);
            this.EnsureAttributeDefault("To", control.To);
        }

        /// <summary>
        /// If the attrributes doesn't have a value set it to the one supplied
        /// </summary>
        /// <param name="attributeName">attribute name</param>
        /// <param name="defaultValue">default value</param>
        private void EnsureAttributeDefault(string attributeName, object defaultValue)
        {
            if (string.IsNullOrEmpty(this.Tag.GetAttribute(attributeName)))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(defaultValue);
                if (converter != null)
                {
                    this.Tag.SetAttribute(attributeName, converter.ConvertToInvariantString(defaultValue));
                }
            }
        }
    }
}
