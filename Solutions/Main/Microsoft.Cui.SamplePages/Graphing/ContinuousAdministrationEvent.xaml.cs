//-----------------------------------------------------------------------
// <copyright file="ContinuousAdministrationEvent.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010..
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
// <date>20-Nov-2009</date>
// <summary>Sample control for Contnuous admin marker.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.Cui.Controls;

    /// <summary>
    /// User control for continuous admin marker.
    /// </summary>
    //// *** Do not use this class to display events with Significant Duration in clinical applications. ***
    //// The display of medications of Significant Duration was not explored in the
    //// 'Design Guidance – Timeline View' document. 
    //// Any visual representation of these events in Timeline will need to be re-evaluated in line
    //// with the more up-to-date guidance published in the 'Design Guidance – Drug Administration' document.
    public partial class ContinuousAdministrationEvent : Canvas
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContinuousAdministrationEvent"/> class.
        /// </summary>
        public ContinuousAdministrationEvent()
        {
            InitializeComponent();
        }                

        /// <summary>
        /// Handles the SizeChanged event of the Canvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            if (graphPoint != null)
            {
                if (e.NewSize.Width <= this.MaxWidth)
                {
                    if (e.NewSize.Width >= 32)
                    {
                        this.Start.Visibility = Visibility.Visible;
                        this.End.Visibility = Visibility.Visible;
                        this.Zoom.Visibility = Visibility.Collapsed;
                        Canvas.SetLeft(this.End, graphPoint.X1X2Pixel - this.End.Width);
                    }
                    else
                    {
                        this.Start.Visibility = Visibility.Visible;
                        this.End.Visibility = Visibility.Collapsed;
                        this.Zoom.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    this.Zoom.Visibility = Visibility.Collapsed;
                    if (graphPoint.X1Pixel >= 0 && graphPoint.X1Pixel <= this.MaxWidth)
                    {
                        this.Start.Visibility = Visibility.Visible;
                    }

                    if (graphPoint.X2Pixel >= 0 && graphPoint.X2Pixel <= this.MaxWidth)
                    {
                        this.End.Visibility = Visibility.Visible;
                        Canvas.SetLeft(this.End, graphPoint.X2Pixel - this.End.Width);
                    }
                }
            }
        }
    }
}
