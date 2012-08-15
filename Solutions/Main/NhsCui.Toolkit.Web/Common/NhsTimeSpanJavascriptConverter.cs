//-----------------------------------------------------------------------
// <copyright file="NhsTimeSpanJavascriptConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-May-2007</date>
// <summary>Custom JavaScriptConverter that converts the format of the 
// NhsTimeSpan class back and forth down to a format between javascript, 
// giving a more friendly Javascript signature</summary>
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
    /// NhsTimeSpan class back and forth down to a format btween javascript, giving a more friendly Javascript signature
    /// </summary>
    public class NhsTimeSpanJavascriptConverter : JavaScriptConverter
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public NhsTimeSpanJavascriptConverter()
        {
        }

        /// <summary>
        /// Gets a collection of the supported types
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(NhsCui.Toolkit.DateAndTime.NhsTimeSpan) }));
            }
        }

        /// <summary>
        ///  Builds a dictionary of name/value pairs
        /// </summary>
        /// <param name="obj">The Object to serialize</param>
        /// <param name="serializer">The JavaScriptSerializer responsible for the serialization</param>
        /// <returns>An IDictionary object that contains key/value pairs that represent the object�s data</returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            NhsTimeSpan timeSpan = obj as NhsTimeSpan;

            if (timeSpan != null)
            {
                //// Create the representation
                Dictionary<string, object> result = new Dictionary<string, object>();

                result.Add("from", timeSpan.From);
                result.Add("to", timeSpan.To);
                result.Add("granularity", timeSpan.Granularity);
                result.Add("isAge", timeSpan.IsAge);
                result.Add("threshold", timeSpan.Threshold);

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
            NhsTimeSpan deserialisationTarget = new NhsTimeSpan();

            deserialisationTarget.From = (dictionary.ContainsKey("_from") ? ((DateTime)dictionary["_from"]).ToLocalTime() : ((DateTime)dictionary["from"]).ToLocalTime());
            deserialisationTarget.To = (dictionary.ContainsKey("_to") ? ((DateTime)dictionary["_to"]).ToLocalTime() : ((DateTime)dictionary["to"]).ToLocalTime());
            deserialisationTarget.Granularity = (dictionary.ContainsKey("_granularity") ? (TimeSpanUnit)dictionary["_granularity"] : (TimeSpanUnit)dictionary["granularity"]);
            deserialisationTarget.IsAge = (dictionary.ContainsKey("_isAge") ? (bool)dictionary["_isAge"] : (bool)dictionary["isAge"]);
            deserialisationTarget.Threshold = (dictionary.ContainsKey("_threshold") ? (TimeSpanUnit)dictionary["_threshold"] : (TimeSpanUnit)dictionary["threshold"]);

            return deserialisationTarget;
        }
    }
}
