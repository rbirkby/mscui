//-----------------------------------------------------------------------
// <copyright file="ISupportTimeWindow.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>ISupportTimeWindow Interface.</summary>
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
    #endregion

    /// <summary>
    /// Interface used to specify the time window supported in a graph page.
    /// </summary>
    public interface ISupportTimeWindow
    {
        /// <summary>
        /// Gets the first item.
        /// </summary>
        object FirstItem
        {
            get;            
        }

        /// <summary>
        /// Gets the last item.
        /// </summary>
        object LastItem
        {
            get;            
        }

        /// <summary>
        /// Filters and returns the data betweem the specified dates.
        /// </summary>
        /// <param name="startDate">Start date of the data to be included.</param>
        /// <param name="endDate">End date of the data to be included.</param>
        /// <returns>
        /// Returns a collection of filtered objects between the specified dates.
        /// </returns>
        System.Collections.ObjectModel.Collection<object> Filter(DateTime startDate, DateTime endDate);
    }
}
