//-----------------------------------------------------------------------
// <copyright file="FilteredCollection.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>11-Feb-2008</date>
// <summary>The Filtered collection class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    #endregion

    /// <summary>
    /// Collection which returns data between two specified dates.
    /// </summary>
    public class FilteredCollection : NonFilteredCollection, ISupportTimeWindow
    {
        #region Properties
        /// <summary>
        /// Gets the first item in the data after sorting.
        /// </summary>
        /// <value>First item in the collection.</value>
        /// <returns>First item in the collection.</returns>
        public object FirstItem
        {
            get
            {
                if (this.Items.Count > 0)
                {
                    List<object> timePoints = this.Items as List<object>;
                    timePoints.Sort();

                    return timePoints[0];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the last item in the data after sorting.
        /// </summary>
        /// <value>Last item in the collection.</value>
        /// <returns>Last item in the collection.</returns>
        public object LastItem
        {
            get
            {
                if (this.Items.Count > 0)
                {
                    List<object> timePoints = this.Items as List<object>;
                    timePoints.Sort();

                    return timePoints[this.Items.Count - 1];
                }

                return null;
            }
        }
        #endregion

        #region ISupportTimeWindow Members

        /// <summary>
        /// Filters the data between two specified dates.
        /// </summary>
        /// <param name="startDate">Start date to be used while filtering.</param>
        /// <param name="endDate">End date to be used while filtering.</param>
        /// <returns>
        /// Returns a collection of data between the start and end date.
        /// </returns>
        public Collection<object> Filter(DateTime startDate, DateTime endDate)
        {
            List<object> timePoints = this.Items as List<object>;
            timePoints.Sort();

            Collection<object> filteredData = new Collection<object>();

            int startIndex = -1;
            int endIndex = -1;
            int lesserValueIndex = -1;

            for (int i = 0; i < timePoints.Count; i++)
            {
                DateTime dateTime = DataHelper.GetGraphPointDateTime(timePoints[i]);
                if (dateTime >= startDate && dateTime <= endDate)
                {
                    filteredData.Add(timePoints[i]);
                    if (startIndex == -1)
                    {
                        startIndex = i;
                    }

                    endIndex = i;
                }

                if (dateTime < startDate)
                {
                    lesserValueIndex = i;
                }
            }

            // Add the last point in the previous page.
            if (startIndex != -1 && startIndex != 0)
            {
                filteredData.Insert(0, timePoints[startIndex - 1]);
            }

            // Add the first point in the next page.
            if (endIndex != -1 && endIndex != timePoints.Count - 1)
            {
                filteredData.Add(timePoints[endIndex + 1]);
            }

            // If page has no data for the supplied time range, 
            // take the last value for previous page and first value for next page.
            if (filteredData.Count == 0)
            {
                if (lesserValueIndex != -1 && lesserValueIndex != timePoints.Count - 1)
                {
                    filteredData.Add(timePoints[lesserValueIndex]);
                    filteredData.Add(timePoints[lesserValueIndex + 1]);
                }
                else if (lesserValueIndex != -1 && lesserValueIndex == timePoints.Count - 1)
                {
                    filteredData.Add(timePoints[lesserValueIndex]);
                }
            }

            // this is now a sorted collection of filtered timepoints in the specified date time range.
            return filteredData;
        }
        #endregion
    }
}
