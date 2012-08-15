//-----------------------------------------------------------------------
// <copyright file="DataHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>21-Oct-2008</date>
// <summary>Data helper utility class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    #endregion

    /// <summary>
    /// Data helper utility class.
    /// </summary>
    internal class DataHelper
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="DataHelper"/> class from being created.
        /// </summary>
        private DataHelper()
        {
        }

        /// <summary>
        /// Gets the first DateTime in the data series.
        /// </summary>
        /// <param name="dataSeries">Data series which needs to be analyzed.</param>
        /// <returns>First DateTime in the data series.</returns>
        internal static DateTime GetFirstDateTime(object dataSeries)
        {
            DateTime? firstDate = null;
            if (null != dataSeries)
            {
                // If dataseries supports filtering then we get the first item date time.
                ISupportTimeWindow filter = dataSeries as ISupportTimeWindow;
                if (filter != null)
                {
                    return DataHelper.GetGraphPointDateTime(filter.FirstItem);
                }

                // If dataseries does not supports filtering then we need to manually find out the first date time object.
                IEnumerable enumerable = dataSeries as IEnumerable;
                if (null != enumerable)
                {
                    foreach (object o in enumerable)
                    {
                        DateTime dt = DataHelper.GetGraphPointDateTime(o);

                        if (!firstDate.HasValue || firstDate.Value == null || firstDate > dt)
                        {
                            firstDate = dt;
                        }
                    }
                }
            }

            return firstDate.GetValueOrDefault(DateTime.MinValue);
        }

        /// <summary>
        /// Gets the last DateTime in the data series.
        /// </summary>
        /// <param name="dataSeries">Data series which needs to be analyzed.</param>
        /// <returns>Last DateTime in the data series.</returns>
        internal static DateTime GetLastDateTime(object dataSeries)
        {
            DateTime? lastDate = null;
            if (null != dataSeries)
            {
                // If dataseries supports filtering then we get the first item date time.
                ISupportTimeWindow filter = dataSeries as ISupportTimeWindow;
                if (filter != null)
                {
                    return DataHelper.GetGraphPointDateTime(filter.LastItem);
                }

                // If dataseries does not supports filtering then we need to manually find out the first date time object.
                IEnumerable enumerable = dataSeries as IEnumerable;
                if (null != enumerable)
                {
                    foreach (object o in enumerable)
                    {
                        DateTime dt = DataHelper.GetGraphPointDateTime(o);
                        if (!lastDate.HasValue || lastDate.Value == null || lastDate < dt)
                        {
                            lastDate = dt;
                        }
                    }
                }
            }

            return lastDate.GetValueOrDefault(DateTime.MinValue);
        }

        /// <summary>
        /// Gets the last data point value in the data series.
        /// </summary>
        /// <param name="dataSeries">Data series which needs to be analyzed.</param>
        /// <returns>Last data point in the data series.</returns>
        internal static object GetLastDataPoint(object dataSeries)
        {
            object dataPoint = null;
            DateTime? lastDate = null;

            if (null != dataSeries)
            {
                // If dataseries supports filtering then we get the first item date time.
                ISupportTimeWindow filter = dataSeries as ISupportTimeWindow;
                if (filter != null)
                {
                    return filter.LastItem;
                }

                // If dataseries does not supports filtering then we need to manually find out the first date time object.
                IEnumerable enumerable = dataSeries as IEnumerable;
                if (null != enumerable)
                {
                    foreach (object o in enumerable)
                    {
                        DateTime dt = DataHelper.GetGraphPointDateTime(o);
                        if (!lastDate.HasValue || lastDate.Value == null || lastDate < dt)
                        {
                            lastDate = dt;
                            dataPoint = o;
                        }
                    }
                }
            }

            return dataPoint;
        }        

        /// <summary>
        /// Gets the DateTime cast of the TimePoint.
        /// </summary>
        /// <param name="timePoint">The TimePoint element to plot.</param>
        /// <returns>The equivalent DateTime value.</returns>
        internal static DateTime GetGraphPointDateTime(object timePoint)
        {
            DateTime dt;

            if (timePoint is IConvertible)
            {
                dt = Convert.ToDateTime(timePoint, System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                Type type = timePoint.GetType();
                object[] param = new object[1] { timePoint };
                dt = (DateTime)type.InvokeMember("op_Explicit", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, timePoint, param, null, CultureInfo.CurrentCulture, null);
            }

            return dt;
        }

        /// <summary>
        /// Filters the data between two specified dates.
        /// </summary>
        /// <param name="collection">Collection of point to filter.</param>
        /// <param name="startDate">Start date to be used while filtering.</param>
        /// <param name="endDate">End date to be used while filtering.</param>
        /// <returns>
        /// Returns a collection of data between the start and end date.
        /// </returns>
        internal static Collection<object> Filter(object collection, DateTime startDate, DateTime endDate)
        {
            IEnumerable enumerable = collection as IEnumerable;           
            Collection<object> filteredData = new Collection<object>();

            if (enumerable != null)
            {
                List<object> timePoints = enumerable.Cast<object>().ToList<object>();
                timePoints.Sort();

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
                    else if (lesserValueIndex == timePoints.Count - 1)
                    {
                        filteredData.Add(timePoints[lesserValueIndex]);
                    }
                }
            }

            // this is now a sorted collection of filtered timepoints in the specified date time range.
            return filteredData;
        }
    }
}
