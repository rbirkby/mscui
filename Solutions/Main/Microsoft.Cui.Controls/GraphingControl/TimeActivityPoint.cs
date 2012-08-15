//-----------------------------------------------------------------------
// <copyright file="TimeActivityPoint.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>21-Sep-2009</date>
// <summary>TimeActivityPoint class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.ComponentModel;
using System.Windows;

    /// <summary>
    /// Class used to represent a TimePoint in TimeActivityGraph.
    /// </summary>
    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1036")]
    public class TimeActivityPoint : IConvertible, IComparable
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime StartDate 
        {
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the end date.  
        /// </summary>
        /// <value>The end date.</value>
        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime? EndDate
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>The additional information.</value>
        public object AdditionalInformation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data marker template.
        /// </summary>
        /// <value>The data marker template.</value>
        public DataTemplate DataMarkerTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public FrameworkElement Label
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the time point has duration.
        /// </summary>
        /// <value>Value indicating whether the time point has duration.</value>
        public bool HasDuration
        {
            get
            {
                return this.EndDate.HasValue && this.EndDate.Value.Subtract(this.StartDate).Ticks > 0;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.AdditionalInformation.ToString();
        }

        #region IConvertible Members

        /// <summary>
        /// Returns the <see cref="T:System.TypeCode"/> for this instance.
        /// </summary>
        /// <returns>
        /// The enumerated constant that is the <see cref="T:System.TypeCode"/> of the class or value type that implements this interface.
        /// </returns>
        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A Boolean value equivalent to the value of this instance.
        /// </returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 8-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A Unicode character equivalent to the value of this instance.
        /// </returns>
        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.DateTime"/> using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> instance equivalent to the value of this instance.
        /// </returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
           return this.StartDate;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.Decimal"/> number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="T:System.Decimal"/> number equivalent to the value of this instance.
        /// </returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A double-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        public double ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 16-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public short ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 32-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public int ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 64-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public long ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 8-bit signed integer equivalent to the value of this instance.
        /// </returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A single-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        public float ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an <see cref="T:System.Object"/> of the specified <see cref="T:System.Type"/> that has an equivalent value, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType">The <see cref="T:System.Type"/> to which the value of this instance is converted.</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> instance of type <paramref name="conversionType"/> whose value is equivalent to the value of this instance.
        /// </returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 16-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 32-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 64-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        #endregion       
    
        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="obj"/> is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            TimeActivityPoint otherTimePoint = obj as TimeActivityPoint;
            if (otherTimePoint != null)
            {
                return this.StartDate.CompareTo(otherTimePoint.StartDate);
            }
            else
            {
                throw new ArgumentException("Object is not a TimeActivityPoint");
            }
        }

        #endregion
    }
}
