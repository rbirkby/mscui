//-----------------------------------------------------------------------
// <copyright file="LevelOfDetail.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>20-Feb-2008</date>
// <summary>The LevelOfDetail Control class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using System;
    using System.Globalization;

    /// <summary>
    /// Implementation of the LevelOfDetail Control class.
    /// </summary>
    [TemplatePart(Name = LevelOfDetail.IncrementButtonElementName, Type = typeof(Button))]
    [TemplatePart(Name = LevelOfDetail.DecrementButtonElementName, Type = typeof(Button))]
    [TemplatePart(Name = LevelOfDetail.TickContainerElementName, Type = typeof(Panel))]
    public class LevelOfDetail : Control
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <c href="Microsoft.Cui.Controls.LevelOfDetail.NumberOfLevels" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty NumberOfLevelsProperty = DependencyProperty.Register("NumberOfLevels", typeof(int), typeof(LevelOfDetail), new PropertyMetadata(new PropertyChangedCallback(NumberOfLevels_Callback)));

        /// <summary>
        /// Identifies the <c href="Microsoft.Cui.Controls.LevelOfDetail.CurrentLevel" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CurrentLevelProperty = DependencyProperty.Register("CurrentLevel", typeof(int), typeof(LevelOfDetail), new PropertyMetadata(new PropertyChangedCallback(CurrentLevelChanged_Callback)));

        /// <summary>
        /// Identifies the <c href="Microsoft.Cui.Controls.LevelOfDetail.Label" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LevelOfDetail), new PropertyMetadata("Level of Detail"));
        #endregion

        #region TemplatePart Names
        /// <summary>
        /// Template part name for increment button.
        /// </summary>
        private const string IncrementButtonElementName = "ELEMENT_IncrementButton";

        /// <summary>
        /// Template part name for decrement button.
        /// </summary>
        private const string DecrementButtonElementName = "ELEMENT_DecrementButton";

        /// <summary>
        /// Template part name for ticks container.
        /// </summary>
        private const string TickContainerElementName = "ELEMENT_TickContainer";

        /// <summary>
        /// Resource key name for TickTemplate.
        /// </summary>
        private const string TickTemplateName = "TickTemplate";
        #endregion

        #region TemplatePart Elements
        /// <summary>
        /// Member variable to hold increment button.
        /// </summary>
        private Button incrementButton;

        /// <summary>
        /// Member variable to hold decrement button.
        /// </summary>
        private Button decrementButton;

        /// <summary>
        /// Member variable to hold tick container.
        /// </summary>
        private Panel tickContainer;

        /// <summary>
        /// Member variable to indicate whether the value is being set as part of coercion.
        /// </summary>
        private bool coercing;
        #endregion

        #region Contructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelOfDetail"/> class.
        /// </summary>
        public LevelOfDetail()
        {
            this.DefaultStyleKey = typeof(LevelOfDetail);
        }
        #endregion

        #region Events
        /// <summary>
        /// Event for level of detail changed.
        /// </summary>
        public event System.EventHandler<System.EventArgs> OnLevelOfDetailChange;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the text for the label.
        /// </summary>
        /// <value>Value for the label.</value>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum number of levels.
        /// </summary>
        /// <value>Value for maximum number of levels.</value>
        public int NumberOfLevels
        {
            get { return (int)GetValue(NumberOfLevelsProperty); }
            set { SetValue(NumberOfLevelsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current level.
        /// </summary>
        /// <value>Value for the current selected level.</value>
        public int CurrentLevel
        {
            get { return (int)GetValue(CurrentLevelProperty); }
            set { SetValue(CurrentLevelProperty, value); }
        }
        #endregion        

        #region Overridden Methods
        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Validate();

            this.incrementButton = this.GetTemplateChild(IncrementButtonElementName) as Button;
            this.decrementButton = this.GetTemplateChild(DecrementButtonElementName) as Button;
            this.tickContainer = this.GetTemplateChild(TickContainerElementName) as Panel;

            LevelOfDetail.AssertTemplatePart(this.incrementButton, IncrementButtonElementName);
            LevelOfDetail.AssertTemplatePart(this.decrementButton, DecrementButtonElementName);
            LevelOfDetail.AssertTemplatePart(this.tickContainer, TickContainerElementName);

            this.incrementButton.Click += new RoutedEventHandler(this.IncrementButton_Click);
            this.decrementButton.Click += new RoutedEventHandler(this.DecrementButton_Click);

#if !SILVERLIGHT
            this.UpdateContentMargins();
#endif
            this.OnNumberOfLevelsChange();
        }       
        #endregion

        #region Dependency property callbacks
        /// <summary>
        /// Handles the property changed call back of the number of levels.
        /// </summary>
        /// <param name="d">Dependency object whose property got changed.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private static void NumberOfLevels_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LevelOfDetail lodControl = d as LevelOfDetail;
            if (lodControl != null && e.OldValue != e.NewValue && lodControl.tickContainer != null)
            {
                lodControl.OnNumberOfLevelsChange();
            }
        }

        /// <summary>
        /// Handles the property changed call back of the current level.
        /// </summary>
        /// <param name="d">Dependency object whose property got changed.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private static void CurrentLevelChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LevelOfDetail lodControl = d as LevelOfDetail;
            if (lodControl != null && lodControl.tickContainer != null && !lodControl.coercing)
            {
                if (lodControl.CurrentLevel < 1 || lodControl.CurrentLevel > lodControl.NumberOfLevels)
                {
                    lodControl.coercing = true;
                    lodControl.CurrentLevel = (int)e.OldValue;
                    lodControl.coercing = false;
                }
                else
                {
                    lodControl.OnCurrentLevelChange((int)e.OldValue, (int)e.NewValue);
                }                
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Asserts whether an element has been found in the given control template.
        /// </summary>
        /// <param name="element">Element that needs to be asserted.</param>
        /// <param name="templatePartName">Template part name for the element.</param>
        private static void AssertTemplatePart(FrameworkElement element, string templatePartName)
        {
            if (element == null)
            {                
                throw new ArgumentNullException(templatePartName, string.Format(CultureInfo.CurrentCulture, "Element with template name '{0}' was not found in the control template.", templatePartName));
            }
        }

        /// <summary>
        /// Raises an ArgumentException.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="message">Details of the exception.</param>
        private static void RaiseArgumentException(string propertyName, string message)
        {
            throw new ArgumentException(propertyName, message);
        }

        #if !SILVERLIGHT
        /// <summary>
        /// Sets the margin for the child at given index.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="childIndex">Index of the child.</param>
        /// <param name="thickness">Margin for the child.</param>
        /// <remarks>Sets the margin only if the child at given index is a shape.</remarks>
        private static void SetShapeMargin(Panel element, int childIndex, Thickness thickness)
        {
            if (element.Children.Count > childIndex)
            {
                Shape shape = element.Children[childIndex] as Shape;
                if (shape != null)
                {
                    shape.Margin = thickness;
                }
            }
        }

        /// <summary>
        /// Updates the buttons content margins.
        /// </summary>        
        /// <remarks>Only for WPF as Paths are being rendered differently.</remarks>
        private void UpdateContentMargins()
        {
            Grid incrementButtonGrid = this.incrementButton.Content as Grid;
            Grid decrementButtonGrid = this.decrementButton.Content as Grid;
            if (incrementButtonGrid != null && decrementButtonGrid != null)
            {
                SetShapeMargin(incrementButtonGrid, 0, new Thickness(0, -0.5, 0, 0));
                SetShapeMargin(incrementButtonGrid, 1, new Thickness(0));
                SetShapeMargin(incrementButtonGrid, 2, new Thickness(0));

                SetShapeMargin(decrementButtonGrid, 0, new Thickness(0, -0.5, 0, 0));
                SetShapeMargin(decrementButtonGrid, 1, new Thickness(0));                
            }
        }        
#endif

        /// <summary>
        /// Handles the click event of decrement button.
        /// </summary>
        /// <param name="sender">Decrement button.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentLevel > 1)
            {
                this.CurrentLevel--;
            }
        }

        /// <summary>
        /// Handles the click event of increment button.
        /// </summary>
        /// <param name="sender">Increment button.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentLevel < this.NumberOfLevels)
            {
                this.CurrentLevel++;
            }
        }

        /// <summary>
        /// Handles the mouse left button up event of the tick.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void Tick_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LevelOfDetailTick tick = sender as LevelOfDetailTick;
            if (tick != null)
            {
                int tickIndex = Convert.ToInt32(tick.Tag, CultureInfo.CurrentCulture);
                this.CurrentLevel = tickIndex;
            }
        }

        /// <summary>
        /// Clears and Adds the ticks to the container.
        /// </summary>
        private void OnNumberOfLevelsChange()
        {
            this.ClearTicks();
            this.AddTicks();
        }
        
        /// <summary>
        /// Adds ticks to Tick container.
        /// </summary>
        private void AddTicks()
        {
            for (int tickIndex = 0; tickIndex < this.NumberOfLevels; tickIndex++)
            {
                if (this.tickContainer.Resources.Contains(TickTemplateName))
                {
                    LevelOfDetailTick tick = (this.tickContainer.Resources[TickTemplateName] as DataTemplate).LoadContent() as LevelOfDetailTick;

                    if (tick != null)
                    {
                        tick.Tag = tickIndex + 1;                        
                        if (tickIndex + 1 == this.CurrentLevel)
                        {
                            tick.Selected = true;
                        }

                        tick.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Tick_MouseLeftButtonUp);
                        this.tickContainer.Children.Add(tick);
                    }
                }
            }
        }       

        /// <summary>
        /// Clears the ticks from tick container.
        /// </summary>
        private void ClearTicks()
        {
            this.tickContainer.Children.Clear();
        }

        /// <summary>
        /// Handles the current level value change.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void OnCurrentLevelChange(int oldValue, int newValue)
        {           
            if (oldValue >= 0 && oldValue <= this.tickContainer.Children.Count && newValue > 0 && newValue <= this.tickContainer.Children.Count)
            {
                LevelOfDetailTick oldTick = null;
                LevelOfDetailTick newTick = null;

                if (oldValue > 0)
                {
                    oldTick = this.tickContainer.Children[oldValue - 1] as LevelOfDetailTick;
                    if (oldTick != null)
                    {
                        oldTick.Selected = false;
                    }
                }

                if (newValue > 0)
                {
                    newTick = this.tickContainer.Children[newValue - 1] as LevelOfDetailTick;
                    if (newTick != null)
                    {
                        newTick.Selected = true;
                    }
                }

                this.RaiseLevelOfDetailChangeEvent(oldValue, newValue);                
            }            
        }

        /// <summary>
        /// Raises the level of detail changed event.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void RaiseLevelOfDetailChangeEvent(int oldValue, int newValue)
        {
            if (this.OnLevelOfDetailChange != null)
            {
                this.OnLevelOfDetailChange(this, new RoutedPropertyChangedEventArgs<double>(oldValue, newValue));
            }
        }

        /// <summary>
        /// Validates the data.
        /// </summary>
        private void Validate()
        {
            if (this.CurrentLevel > this.NumberOfLevels)
            {
                LevelOfDetail.RaiseArgumentException("CurrentLevel", "CurrentLevel is more than NumberOfLevels.");
            }

            if (this.CurrentLevel < 0)
            {
                LevelOfDetail.RaiseArgumentException("CurrentLevel", "Invalid CurrentLevel.");
            }

            if (this.NumberOfLevels < 0)
            {
                LevelOfDetail.RaiseArgumentException("NumberOfLevels", "Invalid NumberOfLevels.");
            }
        }       
        #endregion
    }
}
