//-----------------------------------------------------------------------
// <copyright file="NhsDateJavascriptConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09-Feb-2007</date>
// <summary>Custom JavaScriptConverter that converts the format of the 
// NhsDate class back and forth down to a format btween javascript, giving a more friendly Javascript signature</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.Web.Script.Serialization;
    using System.Globalization;

    using NhsCui.Toolkit.DateAndTime;

    /// <summary>Custom JavaScriptConverter that converts the format of the 
    /// NhsDate class back and forth down to a format btween javascript, giving a more friendly Javascript signature
    /// </summary>
    public class NhsDateJavascriptConverter : JavaScriptConverter
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public NhsDateJavascriptConverter()
        {
        }

        /// <summary>
        /// Gets a collection of the supported types
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(NhsCui.Toolkit.DateAndTime.NhsDate) })); }
        }

        /// <summary>
        ///  Builds a dictionary of name/value pairs
        /// </summary>
        /// <param name="obj">The Object to serialize</param>
        /// <param name="serializer">The JavaScriptSerializer responsible for the serialization</param>
        /// <returns>An IDictionary object that contains key/value pairs that represent the object�s data</returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            NhsDate date = obj as NhsDate;
            
            if (date != null)
            {
                //// Create the representation
                Dictionary<string, object> result = new Dictionary<string, object>();

                result.Add("dateType", date.DateType);
                result.Add("dateValue", date.DateValue);
                result.Add("month", date.Month);
                result.Add("nullIndex", date.NullIndex);
                result.Add("year", date.Year);

                return result;
            }

            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Converts the provided dictionary into an object of the specified type
        /// </summary>
        /// <param name="dictionary">An IDictionary instance of property data stored as name/value pairs</param>
        /// <param name="type">The Type of the resulting object</param>
        /// <param name="serializer">The JavaScriptSerializer instance</param>
        /// <returns>The deserialized Object</returns>
        /// <remarks>KeyboardShortcut is never sent back up to the server so we do not support Deserialization</remarks>
        /// <exception cref="NotImplementedException">Always returns this exception</exception>
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            NhsDate deserialisationTarget = new NhsDate();

            deserialisationTarget.DateType = (dictionary.ContainsKey("_dateType") ? (DateType)dictionary["_dateType"] : (DateType)dictionary["dateType"]);
            deserialisationTarget.DateValue = (dictionary.ContainsKey("_dateValue") ? ((DateTime)dictionary["_dateValue"]).ToLocalTime() : ((DateTime)dictionary["dateValue"]).ToLocalTime());
            deserialisationTarget.Month = (dictionary.ContainsKey("_month") ? (int)dictionary["_month"] : (int)dictionary["month"]);
            deserialisationTarget.NullIndex = (dictionary.ContainsKey("_nullIndex") ? (int)dictionary["_nullIndex"] : (int)dictionary["nullIndex"]);            
            deserialisationTarget.Year = (dictionary.ContainsKey("_year") ? (int)dictionary["_year"] : (int)dictionary["year"]);

            return deserialisationTarget;
        }
    }
}
