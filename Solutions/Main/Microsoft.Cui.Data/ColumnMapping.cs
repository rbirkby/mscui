//-----------------------------------------------------------------------
// <copyright file="ColumnMapping.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>07-Feb-2008</date>
// <summary> Contains the Column mapping information.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Data
{
    #region "Using"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    #endregion

    /// <summary>
    /// This class stores the column mapping information. 
    /// </summary>
    public class ColumnMapping
    {
        #region Private Members
        /// <summary>
        /// Column name in the Grid.
        /// </summary>
        private string gridColumnName = string.Empty;

        /// <summary>
        /// Column name in the Data.
        /// </summary>
        private string dataColumnName = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates a new ColumnMapping object with the provided GridColumnName and DataColumnName.
        /// </summary>
        /// <param name="gridColumnName">Name of the column in the Grid.</param>
        /// <param name="dataColumnName">Name of the column in the ISV data.</param>
        public ColumnMapping(string gridColumnName, string dataColumnName)
        {
            this.gridColumnName = gridColumnName;
            this.dataColumnName = dataColumnName;
        }

        /// <summary>
        /// Instantiates a new ColumnMapping object.
        /// </summary>
        public ColumnMapping()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Grid column name.
        /// </summary>
        /// <value>The name of the grid column.</value>
        public string GridColumnName
        {
            get
            {
                return this.gridColumnName;
            }

            set
            {
                this.gridColumnName = value;
            }
        }

        /// <summary>
        /// Gets or sets the Data column name.
        /// </summary>
        /// <value>The name of the data column.</value>
        public string DataColumnName
        {
            get
            {
                return this.dataColumnName;
            }

            set
            {
                this.dataColumnName = value;
            }
        }
        #endregion
    }
}
