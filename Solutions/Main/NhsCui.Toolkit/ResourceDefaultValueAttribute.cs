//-----------------------------------------------------------------------
// <copyright file="ResourceDefaultValueAttribute.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The specialization of the DefaultValue attribute to read the value from the strongly-typed resource class.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// Specialization of the DefaultValue attribute to read the value from the strongly-typed resource class.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ResourceDefaultValueAttribute : DefaultValueAttribute
    {
        /// <summary>
        /// Type of Visual Studio generated strongly typed resource class
        /// </summary>
        private Type resourceType;

        /// <summary>
        /// name of the static property to read from the class
        /// </summary>
        private string propertyName;

        /// <summary>
        /// Constructs a new object given the resource class type and the property to be read. 
        /// </summary>
        /// <param name="resourceType">A type of Visual Studio generated strongly-typed resource class.</param>
        /// <param name="propertyName">The name of the static property to be read from the resource class.</param>
        public ResourceDefaultValueAttribute(Type resourceType, string propertyName) : base(propertyName)
        {
            this.resourceType = resourceType;
            this.propertyName = propertyName;
        }

        /// <summary>
        /// The default value.
        /// </summary>
        public override object Value
        {
            get
            {
                if (this.ResourceType != null && !string.IsNullOrEmpty(this.PropertyName))
                {
                    PropertyInfo propertyInfo = this.ResourceType.GetProperty(this.PropertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                    if (propertyInfo != null)
                    {
                        return propertyInfo.GetValue(null, null);
                    }
                }

                return base.Value;
            }
        }

        /// <summary>
        /// A type of strongly-typed resource class.
        /// </summary>
        public Type ResourceType
        {
            get
            {
                return this.resourceType;
            }
        }

        /// <summary>
        /// The name of the static property to be read from the resource class.
        /// </summary>
        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }
    }
}
