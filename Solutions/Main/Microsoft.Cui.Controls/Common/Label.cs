//-----------------------------------------------------------------------
// <copyright file="Label.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Feb-2009</date>
// <summary>Label control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Automation.Peers;

    /// <summary>
    /// Class used to represent Label control.
    /// </summary>
    public class Label : BaseLabel
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.Label.Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(Label), new PropertyMetadata(new PropertyChangedCallback(OnTextChanged)));        
        #endregion

        /// <summary>
        /// Member variable to hold text block element.
        /// </summary>
        private TextBlock textBlock;

        /// <summary>
        /// Fires when Text changes.
        /// </summary>
        public event RoutedEventHandler TextChanged;

        #region Public Properties
        /// <summary>
        /// Gets or sets the text for the label.
        /// </summary>
        /// <value>Text for the label.</value>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets the required width for label based on the current display value.
        /// </summary>
        /// <value>Required width for label.</value>
        public double RequiredWidth
        {
            get
            {
                if (this.textBlock != null)
                {
                    return TextBlockHelper.GetDesiredWidth(this.textBlock);
                }

                return double.NaN;
            }
        }
        #endregion

        /// <summary>
        /// Overridden. Applies the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.textBlock = this.GetTemplateChild("ELEMENT_TextBlock") as TextBlock;
        }

        #region Internal Methods
        /// <summary>
        /// Ellipses the display value based on the content.
        /// </summary>
        /// <param name="availableWidth">Available width for the label.</param>
        internal void AutoEllipse(double availableWidth)
        {
            if (!double.IsInfinity(availableWidth) && !double.IsNaN(availableWidth) && !string.IsNullOrEmpty(this.Text))
            {                
                Size infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
                this.Measure(infiniteSize);

                int substringLength = this.Text.Length;
                while (this.RequiredWidth > availableWidth && substringLength > 0)
                {
                    string truncatedValue = this.Text.Substring(0, substringLength);
                    truncatedValue += "...";
                    substringLength--;
                    this.UpdateDisplayValue(truncatedValue);
                    this.Measure(infiniteSize);
                }
            }
        }

        /// <summary>
        /// Resets the display value.
        /// </summary>
        internal void ResetDisplayValue()
        {
            this.UpdateDisplayValue(this.Text);            
        }
        #endregion

        #region Automation

        /// <summary>
        /// Automation object for the label class.
        /// </summary>
        /// <returns>Automation object for label class.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new LabelAutomationPeer(this);
        }

        #endregion
        
        #region Property Changed Callbacks
        /// <summary>
        /// Handles the Text property changed event.
        /// </summary>
        /// <param name="d">Label whose text property was changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Label label = d as Label;
            if (label != null)
            {
                label.UpdateDisplayValue(e.NewValue as string);
                if (label.TextChanged != null)
                {
                    label.TextChanged(label, new RoutedEventArgs());
                }
            }
        }
        #endregion

        /// <summary>
        /// Updates the display value property.
        /// </summary>
        /// <param name="newValue">Value for the display value.</param>
        private void UpdateDisplayValue(string newValue)
        {
            this.CanChangeDisplayValue = true;
            this.DisplayValue = newValue;
            this.CanChangeDisplayValue = false;
        }
    }
}
