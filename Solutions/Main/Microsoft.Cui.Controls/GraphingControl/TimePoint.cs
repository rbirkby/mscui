//-----------------------------------------------------------------------
// <copyright file="TimePoint.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Sep-2008</date>
// <summary>Class used to denote a time point.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Globalization;
    using System.Text;
using System.ComponentModel;
using Microsoft.Cui.Controls.Common;
    #endregion

    /// <summary>
    /// Specifies a point on the graph at time axis.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1036")]
    public class TimePoint : IConvertible, IComparable
    {
        /// <summary>
        /// Member variable to hold date time of the point.
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// Initializes a new instance of TimePoint.
        /// </summary>
        public TimePoint()
        {
            this.Y1 = double.NaN;
            this.Y2 = double.NaN;
        }

         /// <summary>
        /// Gets or sets the first value at the specified time.
        /// </summary>
        /// <value>Value of first reading.</value>
        [TypeConverter(typeof(DoubleConverter))]      
        public double Y1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last value at the given time.
        /// </summary>
        /// <value>Value of last reading.</value>
        [TypeConverter(typeof(DoubleConverter))]
        public double Y2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>Value on Y axis.</value>
        public string Value
        {
            get { return this.dateTime.ToString(); }
            set { this.dateTime = Convert.ToDateTime(value, System.Globalization.CultureInfo.CurrentCulture); }
        }
        
        /// <summary>
        /// Gets or sets the date time at which the value was recorded.
        /// </summary>
        /// <value>DateTime at which value was recorded.</value>
        public DateTime DateTime
        {
            get { return this.dateTime; }
            set { this.dateTime = value; }
        }

        /// <summary>
        /// Gets or sets additional information about the data.
        /// </summary>
        /// <value>Additional information about the data.</value>
        public string AdditionalInformation
        {
            get;
            set;
        }

        /// <summary>
        /// Explicit cast of the TimePoint to a DateTime.
        /// </summary>
        /// <param name="timePoint">TimePoint object to Cast.</param>
        /// <returns>The converted DataTime value.</returns>
        public static explicit operator DateTime(TimePoint timePoint)
        {
            return timePoint.DateTime;
        }

        /// <summary>
        /// Overridden. Provides a string representation of the time point.
        /// </summary>
        /// <returns>String representation of the time point.</returns>
        public override string ToString()
        {
            if (double.IsNaN(this.Y2))
            {
                return this.Y1.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, "{0}/{1}", this.Y1, this.Y2);
            }
        }

        #region IConvertible Members

        /// <summary>
        /// Returns the TypeCode for this instance. 
        /// </summary>
        /// <returns>TypeCode for this instance.</returns>
        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Boolean value of this instance.</returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Byte value of this instance.</returns>
        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Char value of this instance.</returns>
        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent DateTime using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>DateTime value of this instance.</returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            return this.dateTime;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Decimal number using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Decimal value of this instance.</returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Double value of this instance.</returns>
        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Int16 value of this instance.</returns>
        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Int32 value of this instance.</returns>
        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Int64 value of this instance.</returns>
        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>SByte value of this instance.</returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>Single value of this instance.</returns>
        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent String using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>String value of this instance.</returns>
        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an Object of the specified Type that has an equivalent value, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType">The Type to which the value of this instance is converted. </param>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>An Object instance of type conversionType whose value is equivalent to the value of this instance.</returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>UInt16 value of this instance.</returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>UInt32 value of this instance.</returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information. 
        /// </summary>
        /// <param name="provider">Culture-specific information to be used while formatting.</param>
        /// <returns>UInt64 value of this instance.</returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// Compares two Microsoft.Cui.Controls.TimePoint instances and returns an integer that indicates whether the first 
        /// Microsoft.Cui.Controls.TimePoint instance is ealier than, 
        /// the same as or later than the second  Microsoft.Cui.Controls.TimePoint instance.
        /// </summary>
        /// <param name="obj">Microsoft.Cui.Controls.TimePoint instance.</param>
        /// <returns>
        /// 1 if first Microsoft.Cui.Controls.TimePoint instance is later than the second Microsoft.Cui.Controls.TimePoint instance,
        /// 0 if first  Microsoft.Cui.Controls.TimePoint instance is same as the second Microsoft.Cui.Controls.TimePoint instance or
        /// -1 if first  Microsoft.Cui.Controls.TimePoint instance is earlier than the second Microsoft.Cui.Controls.TimePoint instance.
        /// </returns>
        public int CompareTo(object obj)
        {
            TimePoint otherTimePoint = obj as TimePoint;
            if (otherTimePoint != null)
            {                
                return this.dateTime.CompareTo(otherTimePoint.dateTime);
            }
            else
            {
                throw new ArgumentException("Object is not a TimePoint");
            }
        }

        #endregion
    }
}
