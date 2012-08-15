//-----------------------------------------------------------------------
// <copyright file="DataManagerEnum.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>10-Feb-2008</date>
// <summary>Data Manager Enumerator class </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Data
{
    #region "Using"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Collections.ObjectModel;
    #endregion

    /// <summary>
    /// Data Manager Enumerator class.
    /// </summary>
    public class DataManagerEnumerator : IEnumerator
    {
        /// <summary>
        /// DataManager instance variable.
        /// </summary>
        private DataManager dataManager;

        /// <summary>
        /// Current Ordinal position.
        /// </summary>
        private int position = -1;

        /// <summary>
        /// Gets or sets the Data Manager.
        /// </summary>
        /// <value>The data manager.</value>
        public DataManager DataManager
        {
            get
            {
                return this.dataManager;
            }

            set
            {
                this.dataManager = value;
            }
        }

        /// <summary>
        /// Gets the current object in the enumerator.
        /// </summary>
        /// <value>Gets the current object in the enumerator.</value>
        /// <returns>The current element in the collection.</returns>
        /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception>
        public object Current
        {
            get
            {
                if (this.position > this.dataManager.CacheRecordCount)
                {
                    throw new InvalidOperationException();
                }

                return this.dataManager.GetItem(this.position);
            }
        }

        /// <summary>
        /// Moves the cursor to next record in the enumerator.
        /// </summary>
        /// <returns>Returns true if the next record exists.</returns>
        public bool MoveNext()
        {
            this.position++;
            return (this.position < this.dataManager.CacheRecordCount);
        }

        /// <summary>
        /// Resets the ordinal position to before first row.
        /// </summary>
        public void Reset()
        {
            this.position = -1;
        }
    }
}
