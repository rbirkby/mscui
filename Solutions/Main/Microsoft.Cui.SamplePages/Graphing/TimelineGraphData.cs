//-----------------------------------------------------------------------
// <copyright file="TimelineGraphData.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>07-Oct-2009</date>
// <summary>Class to hold data for data for graphs in timeline view.</summary>
//-----------------------------------------------------------------------

#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Cui.Controls;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Type of the graph.
    /// </summary>
    public enum GraphType
    {
        /// <summary>
        /// TimeActivity graph.
        /// </summary>
        TimeActivity,

        /// <summary>
        /// TimeIBar graph.
        /// </summary>
        TimeIBar,

        /// <summary>
        /// Timeline graph.
        /// </summary>
        TimeLine
    }

    /// <summary>
    /// Class used to hold the data for a time event graph.
    /// </summary>
    public class TimelineGraphData
    {
        /// <summary>
        /// Member variable to hold the activities.
        /// </summary>
        private Dictionary<string, FilteredCollection> activities = new Dictionary<string, FilteredCollection>();

        /// <summary>
        /// List to hold the currently displayed activity types.
        /// </summary>
        private Collection<string> activityTypesDisplayed = new Collection<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineGraphData"/> class.
        /// </summary>
        public TimelineGraphData()
        {
            this.Height = double.NaN;
            this.MaxLabelStackLevels = -1;
            this.GraphId = string.Empty;
        }

        /// <summary>
        /// Gets or sets the graph id.
        /// </summary>
        /// <value>The graph id.</value>
        public string GraphId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the max stack label levels.
        /// </summary>
        /// <value>The max stack label levels.</value>
        public int MaxLabelStackLevels
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show label overcrowding notifications].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show label overcrowding notifications]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLabelOvercrowdingNotifications
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public string SectionName
        {
            get;
            set;
        }

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
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data for the graph.</value>
        public IEnumerable<object> Data
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Activities.
        /// </summary>
        /// <value>The Activities.</value>
        public Dictionary<string, FilteredCollection> Activities
        {
            get
            {
                return this.activities;
            }
        }

        /// <summary>
        /// Gets the activity types displayed.
        /// </summary>
        /// <value>The activity types displayed.</value>
        public Collection<string> ActivityTypesDisplayed
        {
            get
            {
                return this.activityTypesDisplayed;
            }
        }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public SolidColorBrush Background
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the graph.
        /// </summary>
        /// <value>The type of the graph.</value>
        public GraphType GraphType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public double Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show normal range].
        /// </summary>
        /// <value><c>true</c> if [show normal range]; otherwise, <c>false</c>.</value>
        public bool ShowNormalRange
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>The units.</value>
        public string Units
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the units description.
        /// </summary>
        /// <value>The units description.</value>
        public string UnitsDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the normal range description.
        /// </summary>
        /// <value>The normal range description.</value>
        public string NormalRangeDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y axis padding.
        /// </summary>
        /// <value>The Y axis padding.</value>
        public Thickness YAxisPadding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y axis max value.
        /// </summary>
        /// <value>The Y axis max value.</value>
        public double YAxisMaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y axis min value.
        /// </summary>
        /// <value>The Y axis min value.</value>
        public double YAxisMinValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y axis major interval.
        /// </summary>
        /// <value>The Y axis major interval.</value>
        public double YAxisMajorInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the Y axis interval min.
        /// </summary>
        /// <value>The height of the Y axis interval min.</value>
        public double YAxisIntervalMinHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the normal range max value.
        /// </summary>
        /// <value>The normal range max value.</value>
        public double NormalRangeMaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the normal range min value.
        /// </summary>
        /// <value>The normal range min value.</value>
        public double NormalRangeMinValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color of the interpolation line.
        /// </summary>
        /// <value>The color of the interpolation line.</value>
        public SolidColorBrush InterpolationLineColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the normal range brush.
        /// </summary>
        /// <value>The normal range brush.</value>
        public SolidColorBrush NormalRangeBrush
        {
            get;
            set;
        }

        /// <summary>
        /// Gets ot sets the default label mode for the graph.
        /// </summary>
        /// <value>Default label mode.</value>
        public LabelMode LabelMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the point template.
        /// </summary>
        /// <value>The point template.</value>
        public DataTemplate PointTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label template.
        /// </summary>
        /// <value>The label template.</value>
        public DataTemplate LabelTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data marker template.
        /// </summary>
        /// <value>The data marker template.</value>
        public DataTemplate DataMarkerTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public Style Style
        {
            get;
            set;
        }        
    }
}
