//-----------------------------------------------------------------------
// <copyright file="FoundText.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Jan-2007</date>
// <summary>FoundText struct - utility class to represent found text information</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.PatientSearch
{
    using System;

    /// <summary>
    /// utility class to represent found text information
    /// </summary>
    internal class FoundText
    {
        #region Member Vars

        /// <summary>
        /// The index of the start of the found text
        /// </summary>
        private int start = -1;

        /// <summary>
        /// The found text value
        /// </summary>
        private string value = string.Empty;

        /// <summary>
        /// The found text data type
        /// </summary>
        private FoundDataType foundDataType = FoundDataType.NotSpecified;

        #endregion

        #region Constructors

        /// <summary>
        /// FoundText constructor
        /// </summary>
        /// <param name="value">The found text value</param>
        /// <param name="start">The index of the start of the found text</param>
        internal FoundText(string value, int start)
        {
            this.value = value;
            this.start = start;
        }

        /// <summary>
        /// FoundText constructor
        /// </summary>
        /// <param name="value">The found text value</param>
        /// <param name="start">The index of the start of the found text</param>
        /// <param name="foundDataType">The found text data type</param>
        internal FoundText(string value, int start, FoundDataType foundDataType)
        {
            this.value = value;
            this.start = start;
            this.foundDataType = foundDataType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The length of the found text
        /// </summary>
        internal int Length
        {
            get
            {
                return this.value.Length;
            }
        }

        /// <summary>
        /// The index of the start of the found text
        /// </summary>
        internal int Start
        {
            get
            {
                return this.start;
            }

            set
            {
                this.start = value;
            }
        }

        /// <summary>
        /// The found text value
        /// </summary>
        internal string Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// The found text data type
        /// </summary>
        internal FoundDataType Type
        {
            get
            {
                return this.foundDataType;
            }

            set
            {
                this.foundDataType = value;
            }
        }

        #endregion
    }
}
