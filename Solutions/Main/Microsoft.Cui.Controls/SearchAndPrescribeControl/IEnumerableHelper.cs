//-----------------------------------------------------------------------
// <copyright file="IEnumerableHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>18-Sep-2009</date>
// <summary>
//      Provides helper methods for dealing with IEnumerables.
// </summary>
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
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Provides helper methods for dealing with IEnumerables.
    /// </summary>
    public static class IEnumerableHelper
    {
        /// <summary>
        /// Gets an array of objects from an enumerator.
        /// </summary>
        /// <param name="enumerable">The IEnumerable.</param>
        /// <returns>An array of objects.</returns>
        public static object[] GetItems(IEnumerable enumerable)
        {
            if (enumerable != null)
            {
                IEnumerator enumerator = enumerable.GetEnumerator();
                if (enumerator != null)
                {
                    List<object> items = new List<object>();
                    enumerator.Reset();
                    while (enumerator.MoveNext())
                    {
                        items.Add(enumerator.Current);
                    }

                    return items.ToArray();
                }
            }

            return new object[0];
        }

        /// <summary>
        /// Gets a range of objects from an IEnumerable.
        /// </summary>
        /// <param name="items">The list of objects.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <returns>An array of object.</returns>
        public static object[] GetRangeOfItems(IEnumerable items, int startIndex, int endIndex)
        {
            if (items != null)
            {
                IEnumerator enumerator = items.GetEnumerator();
                enumerator.Reset();
                List<object> range = new List<object>();
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }

                    range.Add(enumerator.Current);
                }

                return range.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Gets a count of items in an IEnumerable.
        /// </summary>
        /// <param name="items">The IEnumeral source.</param>
        /// <returns>The number of items in the source.</returns>
        public static int GetItemCount(IEnumerable items)
        {
            int count = 0;

            if (items != null)
            {
                IEnumerator enumerator = items.GetEnumerator();
                enumerator.Reset();

                while (enumerator.MoveNext())
                {
                    count++;
                }
            }

            return count;
        }
    }
}
