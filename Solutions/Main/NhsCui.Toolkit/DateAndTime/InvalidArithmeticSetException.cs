//-----------------------------------------------------------------------
// <copyright file="InvalidArithmeticSetException.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-Jan-2007</date>
// <summary>The exception that is thrown if the arithmetic resources used by the NhsDate.Add or NhsTime.Add methods are invalid.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown if the arithmetic resources used by the NhsDate.Add or NhsTime.Add methods are invalid.
    /// </summary>
    [Serializable]
    public class InvalidArithmeticSetException : Exception
    {
        #region Constructors
        /// <summary>
        /// Constructs an InvalidArithmeticException object. 
        /// </summary>
        /// <remarks>
        /// Thrown if the set of letters in DateArithmeticResources is not unique or if each value is not a single
        /// character in length. 
        /// </remarks>
        public InvalidArithmeticSetException() : base()
        {
        }

        /// <summary>
        /// Constructs an InvalidArithmeticException object with a specified error message. 
        /// </summary>
        /// <remarks>
        /// Thrown if the set of letters in DateArithmeticResources is not unique or if each value is not a single
        /// character in length.
        /// </remarks>
        /// <param name="message">The message that describes the error. </param>
        public InvalidArithmeticSetException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs an InvalidArithmeticException object with a specified error message
        /// and a reference to the inner exception that is the cause of the exception. 
        /// </summary>
        /// <remarks>
        /// Thrown if the set of letters in DateArithmeticResources is not unique or if each value is not a single
        /// character in length. 
        /// </remarks>
        /// <param name="message">The message that describes the error. </param>
        /// <param name="innerException">The exception that is the cause of the current
        /// exception or a null reference if no inner exception is specified. </param>
        public InvalidArithmeticSetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructs an InvalidArithmeticException object with serialized data. 
        /// </summary>
        /// <remarks>
        /// Thrown if the set of letters in DateArithmeticResources is not unique or if each value is not a single
        /// character in length. 
        /// </remarks>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected InvalidArithmeticSetException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
}
