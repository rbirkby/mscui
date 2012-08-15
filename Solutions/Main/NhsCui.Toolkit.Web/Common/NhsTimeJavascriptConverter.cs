//-----------------------------------------------------------------------
// <copyright file="NhsTimeJavascriptConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// NhsTime class back and forth down to a format between javascript, giving a more friendly Javascript signature</summary>
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
    /// NhsTime class back and forth down to a format between javascript, giving a more friendly Javascript signature
    /// </summary>
    public class NhsTimeJavascriptConverter : JavaScriptConverter
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public NhsTimeJavascriptConverter()
        {
        }

        /// <summary>
        /// Gets a collection of the supported types
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(NhsCui.Toolkit.DateAndTime.NhsTime) })); }
        }

        /// <summary>
        ///  Builds a dictionary of name/value pairs
        /// </summary>
        /// <param name="obj">The Object to serialize</param>
        /// <param name="serializer">The JavaScriptSerializer responsible for the serialization</param>
        /// <returns>An IDictionary object that contains key/value pairs that represent the object�s data</returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            NhsTime time = obj as NhsTime;

            if (time != null)
            {
                //// Create the representation
                Dictionary<string, object> result = new Dictionary<string, object>();

                result.Add("nullIndex", time.NullIndex);                
                result.Add("timeType", time.TimeType);
                result.Add("timeValue", time.TimeValue);

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
            NhsTime deserialisationTarget = new NhsTime();

            deserialisationTarget.NullIndex = (dictionary.ContainsKey("_nullIndex") ? (int)dictionary["_nullIndex"] : (int)dictionary["nullIndex"]);            
            deserialisationTarget.TimeType = (dictionary.ContainsKey("_timeType") ? (TimeType)dictionary["_timeType"] : (TimeType)dictionary["timeType"]);
            deserialisationTarget.TimeValue = (dictionary.ContainsKey("_timeValue") ? ((DateTime)dictionary["_timeValue"]).ToLocalTime() : ((DateTime)dictionary["timeValue"]).ToLocalTime());

            return deserialisationTarget;
        }
    }
}
