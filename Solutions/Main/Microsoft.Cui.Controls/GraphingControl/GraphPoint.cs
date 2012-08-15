//-----------------------------------------------------------------------
// <copyright file="GraphPoint.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Class used to denote a point on the graph.</summary>
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
    /// Control that denotes a point on the graph.
    /// </summary>
    public class GraphPoint : Canvas
    {
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GraphPoint.Y1"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(GraphPoint), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GraphPoint.Y2"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(GraphPoint), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GraphPoint.X1"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(DateTime), typeof(GraphPoint), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GraphPoint.X2"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(DateTime?), typeof(GraphPoint), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GraphPoint.DataMarkerTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataMarkerTemplateProperty =
            DependencyProperty.Register("DataMarkerTemplate", typeof(DataTemplate), typeof(GraphPoint), null);
        
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GraphPoint.Label"/> dependency property.
        /// </summary>        
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(FrameworkElement), typeof(GraphPoint), null);
                
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphPoint"/> class.
        /// </summary>
        public GraphPoint()
        {            
            this.Y1 = 0;
            this.Y2 = Double.NaN;
            this.X2 = null;
        }

        /// <summary>
        /// Gets or sets the Y co-ordinate of the graph point.
        /// </summary>
        /// <value>Y co-ordinate of the graph point.</value>
        public double Y1
        {
            get { return (double)this.GetValue(GraphPoint.Y1Property); }
            set { this.SetValue(GraphPoint.Y1Property, value); }
        }

        /// <summary>
        /// Gets or sets the Y pixel.
        /// </summary>
        /// <value>The Y pixel.</value>
        public double Y1Pixel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y2 co-ordinate of the graph point.
        /// </summary>
        /// <value>Y2 co-ordinate of the graph point.</value>
        public double Y2
        {
            get { return (double)this.GetValue(GraphPoint.Y2Property); }
            set { this.SetValue(GraphPoint.Y2Property, value); }
        }

        /// <summary>
        /// Gets or sets the Y pixel.
        /// </summary>
        /// <value>The Y2 pixel.</value>
        public double Y2Pixel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>Start date for the graph point.</value>
        public DateTime X1
        {
            get { return (DateTime)this.GetValue(GraphPoint.X1Property); }
            set { this.SetValue(GraphPoint.X1Property, value); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>End date for the graph point.</value>
        public DateTime? X2
        {
            get 
            {
                if (this.GetValue(GraphPoint.X2Property) != null)
                {
                    return (DateTime)this.GetValue(GraphPoint.X2Property);
                }

                return null;
            }

            set 
            { 
                this.SetValue(GraphPoint.X2Property, value); 
            }
        }

        /// <summary>
        /// Gets or sets the data marker template.
        /// </summary>
        /// <value>The data marker template.</value>
        public DataTemplate DataMarkerTemplate
        {
            get { return (DataTemplate)this.GetValue(GraphPoint.DataMarkerTemplateProperty); }
            set { this.SetValue(GraphPoint.DataMarkerTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public FrameworkElement Label
        {
            get { return (FrameworkElement)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the X pixel.
        /// </summary>
        /// <value>The X pixel.</value>
        public double X1Pixel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the x2 pixel.
        /// </summary>
        /// <value>The x2 pixel.</value>
        public double X2Pixel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the difference between Y1 and Y2 Pixels.
        /// </summary>
        /// <value>The Expected Graph Point Height.</value>
        public double Y1Y2Pixel
        {
            get { return Math.Abs(this.Y1Pixel - this.Y2Pixel); }
        }

        /// <summary>
        /// Gets the difference between X1 and X2 Pixels.
        /// </summary>
        /// <value>The Expected Graph Point width.</value>
        public double X1X2Pixel
        {
            get { return Math.Abs(this.X1Pixel - this.X2Pixel); }
        }
    }
}
