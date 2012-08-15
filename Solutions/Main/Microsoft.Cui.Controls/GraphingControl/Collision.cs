//-----------------------------------------------------------------------
// <copyright file="Collision.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Dec-2008</date>
// <summary>The collision class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Collision class.
    /// </summary>
    public class Collision
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Collision"/> class.
        /// </summary>
        /// <param name="clusterStartElement">The cluster start element.</param>
        /// <param name="clusterEndElement">The cluster end element.</param>
        /// <param name="clusterShowingIcon">If set to <c>true</c> [cluster showing icon].</param>
        public Collision(FrameworkElement clusterStartElement, FrameworkElement clusterEndElement, bool clusterShowingIcon)
        {
            this.ClusterEndElement = clusterEndElement;
            this.ClusterStartElement = clusterStartElement;
            this.ClusterShowingIcon = clusterShowingIcon;
        }

        /// <summary>
        /// Gets or sets the cluster start element.
        /// </summary>
        /// <value>The cluster start element.</value>
        public FrameworkElement ClusterStartElement
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cluster end element.
        /// </summary>
        /// <value>The cluster end element.</value>
        public FrameworkElement ClusterEndElement
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [cluster showing icon].
        /// </summary>
        /// <value><c>True</c> If [cluster showing icon]; otherwise, <c>false</c>.</value>
        public bool ClusterShowingIcon
        {
            get;
            set;
        }
    }
}
