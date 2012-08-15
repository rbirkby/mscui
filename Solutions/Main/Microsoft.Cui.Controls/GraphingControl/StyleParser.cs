//-----------------------------------------------------------------------
// <copyright file="StyleParser.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>1-Nov-2008</date>
// <summary>Stle parser class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    
    /// <summary>
    /// Class used for parsing the style.
    /// </summary>
    internal class StyleParser
    {
        #region Error Messages
        /// <summary>
        /// Error message string used when the template part element is missing.
        /// </summary>
        private const string TemplatePartElementNullMessage = @"Could not find an element with name '{0}' in the template.";

        /// <summary>
        /// Error message string used when the template part element is of incorrect type.
        /// </summary>
        private const string TemplatePartElementTypeInvalidMessage = @"Element with name '{0}' in the template is of invalid type. Expected type is '{1}'.";

        /// <summary>
        /// Error message string used when the template part element is missing.
        /// </summary>
        private const string ResourceElementNullMessage = @"Could not find a resource with the key '{0}' in the template.";

        /// <summary>
        /// Error message string used when the template part element is of incorrect type.
        /// </summary>
        private const string ResourceElementTypeInvalidMessage = @"Resource with key '{0}' in the template is of invalid type. Expected type is '{1}'.";
        #endregion

        /// <summary>
        /// Private constructor to avoid instantiation.
        /// </summary>
        private StyleParser()
        {
        }

        /// <summary>
        /// Finds an element with the specified name.
        /// </summary>
        /// <typeparam name="T">Type of the element.</typeparam>
        /// <param name="rootElement">Element under which the element exists.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="raiseExceptions">Boolean to indicate whether to raise exceptions when not found.</param>
        /// <returns>If an element is found with the specified name and type then returns the element, else null.</returns>
        /// <remarks>If <paramref name="raiseExceptions"/> is true and the element is not found then an exception of type <see cref="System.ArgumentNullException"/> is thrown. 
        /// If an element is found but is not of specified type then an exception of type <see cref="System.ArgumentException"/> is thrown</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Method does not need a type parameter.")]
        internal static T FindName<T>(FrameworkElement rootElement, string elementName, bool raiseExceptions)
        {
            object obj = null;
            obj = rootElement.FindName(elementName);

            if (raiseExceptions)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementNullMessage, elementName));
                }

                if (obj.GetType() != typeof(T))
                {
                    throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementTypeInvalidMessage, elementName, typeof(T).FullName));
                }
            }

            return (T)obj;
        }

        /// <summary>
        /// Finds an resource with the specified key.
        /// </summary>
        /// <typeparam name="T">Type of the element.</typeparam>
        /// <param name="rootElement">Element under which the resource exists.</param>
        /// <param name="resourceKey">Resource key.</param>
        /// <param name="raiseExceptions">Boolean to indicate whether to raise exceptions when not found.</param>
        /// <returns>If an element is found with the specified name and type then returns the element, else null.</returns>
        /// <remarks>If <paramref name="raiseExceptions"/> is true and the element is not found then an exception of type <see cref="System.ArgumentNullException"/> is thrown. 
        /// If an element is found but is not of specified type then an exception of type <see cref="System.ArgumentException"/> is thrown</remarks>        
        internal static T FindResource<T>(FrameworkElement rootElement, string resourceKey, bool raiseExceptions)
        {
            object obj = null;
            obj = rootElement.Resources[resourceKey];

            if (raiseExceptions)
            {
                if (!rootElement.Resources.Contains(resourceKey))
                {
                    throw new ArgumentNullException(string.Format(System.Globalization.CultureInfo.CurrentCulture, ResourceElementNullMessage, resourceKey));
                }

                if (obj.GetType() != typeof(T))
                {
                    throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, ResourceElementTypeInvalidMessage, resourceKey, typeof(T).FullName));
                }
            }

            return (T)obj;
        }
    }
}
