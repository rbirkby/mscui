//-----------------------------------------------------------------------
// <copyright file="GroupMember.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>6-Oct-2008</date>
// <summary>The GroupMember class. </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
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

    /// <summary>
    /// Members of Group By ComboBox.
    /// </summary>
    public class GroupMember
    {
    #region Private Members
        /// <summary>
        /// Display value.
        /// </summary>
        private string displayValue;

        /// <summary>
        /// Data value.
        /// </summary>
        private string dataValue;
    #endregion

    #region Properties
        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <value>
        /// Value displayed in the ComboBox.
        /// </value>
        public string DisplayValue
        {
            get
            {
                return this.displayValue;
            }

            set
            {
               this.displayValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        /// <value>
        /// Value passed on to the Data Provider layer.
        /// </value>
        public string DataValue
        {
            get
            {
                return this.dataValue;
            }

            set
            {
                this.dataValue = value;
            }
        }
    #endregion
    }
}
