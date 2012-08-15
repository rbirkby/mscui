//-----------------------------------------------------------------------
// <copyright file="CurvedInterpolationLine.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>07-Sep-2009</date>
// <summary>Class used to represent CurvedInterpolationLine control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Microsoft.Cui.Controls;

    /// <summary>
    /// Class to represent curved interpolation line.
    /// </summary>
    public partial class CurvedInterpolationLine : UserControl
    {        
        #region Member variables
        /// <summary>
        /// Member variable to hold curve length.
        /// </summary>
        private double curveLength = 7d;

        /// <summary>
        /// Member variable to hold curve height.
        /// </summary>
        private double curveHeight = 7d;        
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CurvedInterpolationLine"/> class.
        /// </summary>
        public CurvedInterpolationLine()
        {
            InitializeComponent();
            this.HasDuration = true;
            this.Loaded += new RoutedEventHandler(CurvedInterpolationLine_Loaded);
        }              
        #endregion        

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance has duration.
        /// </summary>
        /// <value>
        /// If this instance has duration <c>true</c>; otherwise, <c>false</c>.
        /// </value>
        public bool HasDuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [open duration].
        /// </summary>
        /// <value>If [open duration] <c>true</c>; otherwise, <c>false</c>.</value>
        public bool OpenDuration
        {
            get;
            set;
        }
        #endregion        
        
        #region Private methods
        /// <summary>
        /// Sets the polyline points.
        /// </summary>
        private void SetPolylinePoints()
        {
            if (this.PolyLine != null)
            {
                this.PolyLine.Points.Clear();                

                PointCollection points = new PointCollection();
                
                GraphPoint graphPoint = this.DataContext as GraphPoint;

                /* 
                Every panel in the graph receives all the activities that happened between the current panels 
                start date and end date. Additionally, it also receives last activity from the previous panel 
                and the first point from next panel. 
                
                An activity can have start and end date and it may happen that an activity has started in one panel 
                and ending in another or it can start and end in the same panel.
                
                TimeActivityGraph will set GraphPoint instance as the DataContext for the UIElement supplied as DataMarkerElement.
                This instance will contain details about the start and end co-ordinates of the interpolation line with respect to the current panel.
                If start time belongs to previous panel, then X1 property will have value as lesser than 0,
                If end time belongs to previous panel, then X2 property will have value as lesser than 0,
                If start time belongs to next panel, then X1 property will have value as greater than the current panels MaxWidth and 
                If end time belongs to next panel, then X2 property will have value as lesser than the current panels MaxWidth
                  
                Following code takes into consideration different scenarios and tries to render a polyline 
                with differnt shapes based the start and end dates.                
                */

                if ((graphPoint.X1Pixel < 0 && graphPoint.X2Pixel <= 0) || (graphPoint.X1Pixel >= this.MaxWidth && graphPoint.X2Pixel > this.MaxWidth))
                {
                    // Line does not belong to this panel.
                    return;
                }

                double distance = graphPoint.X2Pixel - graphPoint.X1Pixel;
                this.PolyLine.Visibility = Visibility.Visible;

                if (this.HasDuration)
                {
                    this.AdjustCurveLength(distance);
                    if (graphPoint.X1Pixel >= -this.curveLength && graphPoint.X2Pixel <= this.MaxWidth + this.curveLength)
                    {
                        // Completely in this panel
                        double left = 0;
                        if (graphPoint.X1Pixel < 0)
                        {
                            left = graphPoint.X1Pixel;
                        }

                        points.Add(new Point(left, this.curveHeight));
                        points.Add(new Point(this.curveLength + left, 0));
                        points.Add(new Point(graphPoint.X1X2Pixel - this.curveLength + left, 0));
                        points.Add(new Point(graphPoint.X1X2Pixel + left, this.curveHeight));
                    }
                    else if (graphPoint.X1Pixel >= 0 && graphPoint.X2Pixel > this.MaxWidth)
                    {
                        // Only start in this panel
                        if (graphPoint.X1Pixel + this.curveLength < this.MaxWidth)
                        {
                            points.Add(new Point(0, this.curveHeight));
                            points.Add(new Point(this.curveLength, 0));                            
                            points.Add(new Point(this.MaxWidth - graphPoint.X1Pixel, 0));
                        }                                                
                    }
                    else if (graphPoint.X1Pixel < 0 && graphPoint.X2Pixel >= 0 && graphPoint.X2Pixel <= this.MaxWidth)
                    {
                        // Only end in this panel
                        if (graphPoint.X2Pixel > this.curveLength)
                        {
                            points.Add(new Point(0, 0));
                            points.Add(new Point(graphPoint.X2Pixel - this.curveLength, 0));
                            points.Add(new Point(graphPoint.X2Pixel, this.curveHeight));
                        }                                             
                    }
                    else if (graphPoint.X1Pixel < 0 && graphPoint.X2Pixel > this.MaxWidth)
                    {
                        // Both start and end are out of this panel
                        points.Add(new Point(0, 0));
                        points.Add(new Point(this.MaxWidth, 0));
                    }
                }
                else if (this.OpenDuration)
                {                    
                    if (graphPoint.X1Pixel >= -this.curveLength)
                    {
                        double left = 0;
                        if (graphPoint.X1Pixel < 0)
                        {
                            left = graphPoint.X1Pixel;
                        }

                        points.Add(new Point(left, this.curveHeight));
                        points.Add(new Point(this.curveLength + left, 0));

                        if (this.MaxWidth - graphPoint.X1Pixel > this.curveLength)
                        {
                            points.Add(new Point(this.MaxWidth - graphPoint.X1Pixel, 0));
                        }
                    }
                    else
                    {
                        points.Add(new Point(0, 0));
                        points.Add(new Point(this.MaxWidth, 0));
                    }
                }
                else
                {
                    this.curveHeight = 8;
                    this.curveLength = 4;
                    points.Add(new Point(0, this.curveHeight));
                    points.Add(new Point(this.curveLength, 0));
                    points.Add(new Point(-this.curveLength, 0));
                    points.Add(new Point(0, this.curveHeight));

                    this.PolyLine.StrokeThickness = 1;
                    this.PolyLine.Fill = this.PolyLine.Stroke;
                }
                 
                this.PolyLine.Points = points;                
            }
        }

        /// <summary>
        /// Adjusts the length of the curve.
        /// </summary>
        /// <param name="distance">The distance.</param>
        private void AdjustCurveLength(double distance)
        {
            this.curveLength = 8;

            if (distance < 3 * this.curveLength)                
            {
                this.curveLength = (int)(distance / 3);
            }            
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the Loaded event of the CurvedInterpolationLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CurvedInterpolationLine_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetPolylinePoints();
        }         
        #endregion
    }
}

