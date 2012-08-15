//-----------------------------------------------------------------------
// <copyright file="LevelOfDetailTick.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-May-2009</date>
// <summary>The LevelOfDetailTick class used by the Level of Detail control.</summary>
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

    /// <summary>
    /// Implementation for Level of detail tick.
    /// </summary>
    public class LevelOfDetailTick : Control
    {
        /// <summary>
        /// Identifies the <c href="Microsoft.Cui.Controls.Selected" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(bool), typeof(LevelOfDetailTick), new PropertyMetadata(false, new PropertyChangedCallback(OnSelectionChanged)));

        /// <summary>
        /// Visual state name for the selected state.
        /// </summary>
        private const string SelectedStateName = "Selected";

        /// <summary>
        /// Visual state name for unselected state.
        /// </summary>
        private const string UnSelectedStateName = "UnSelected";      

        /// <summary>
        /// Initializes a new instance of <c href="Microsoft.Cui.Controls.LevelOfDetailTick" /> control.
        /// </summary>
        public LevelOfDetailTick()
        {
            this.DefaultStyleKey = typeof(LevelOfDetailTick);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current level of detail is selected.
        /// </summary>
        /// <value>True if the current tick is selected; otherwise false.</value>
        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.TransitionState();
        }

        /// <summary>
        /// Overridden. Handles the Mouse enter event on the control.
        /// </summary>
        /// <param name="e">Event args containing Mouse information.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Overridden. Handles the Mouse leave event on the control.
        /// </summary>
        /// <param name="e">Event args containing Mouse information.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);            
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Handles the property changed event of the selected property.
        /// </summary>
        /// <param name="d">DependencyObject whose selected property changed.</param>
        /// <param name="e">Event args containing instance data.</param>
        private static void OnSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LevelOfDetailTick tick = d as LevelOfDetailTick;
            if (tick != null && e.OldValue != e.NewValue)
            {
                tick.TransitionState();
            }
        }

        /// <summary>
        /// Transitions the control state based on the selected property.
        /// </summary>        
        private void TransitionState()
        {
            if (this.Selected)
            {
                VisualStateManager.GoToState(this, SelectedStateName, true);
            }
            else
            {
                VisualStateManager.GoToState(this, UnSelectedStateName, true);
            }
        }
    }
}
