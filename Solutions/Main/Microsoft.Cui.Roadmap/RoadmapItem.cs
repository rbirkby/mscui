//-----------------------------------------------------------------------
// <copyright file="RoadMapItem.cs" company="Microsoft Corporation copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation copyright 2007 - 2010.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>23-Jan-2009</date>
// <summary>RoadMapItem class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Roadmap
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
    /// RoadMapItem class.
    /// </summary>
    public class RoadmapItem
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description tool tip.
        /// </summary>
        /// <value>The description tool tip.</value>
        public string DescriptionToolTip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI property.</value>
        public string Link
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current status template.
        /// </summary>
        /// <value>The current status template.</value>
        public DataTemplate CurrentStatusTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the quarter one template.
        /// </summary>
        /// <value>The quarter one template.</value>
        public DataTemplate QuarterOneTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the quarter two template.
        /// </summary>
        /// <value>The quarter two template.</value>
        public DataTemplate QuarterTwoTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the quarter three template.
        /// </summary>
        /// <value>The quarter three template.</value>
        public DataTemplate QuarterThreeTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the quarter four template.
        /// </summary>
        /// <value>The quarter four template.</value>
        public DataTemplate QuarterFourTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public DataTemplate Template
        {
            get;
            set;
        }
    }
}
