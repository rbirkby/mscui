//-----------------------------------------------------------------------
// <copyright file="FilterControl.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The FilterControl class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Input;

    /// <summary>
    /// Implementation of the FilterControl class.
    /// </summary>
    public partial class FilterControl : UserControl
    {
        #region Private Members
        /// <summary>
        /// The selected brush.
        /// </summary>
        private Brush selectedFilterBrush = new SolidColorBrush(Colors.Orange);

        /// <summary>
        /// Indicates whether the drop down pop-up is open or not.
        /// </summary>
        private bool dropDownOpen;

        /// <summary>
        /// The unselected brush.
        /// </summary>
        private Brush unselectedFilterBrush;  //// ALTERED to "BRUSH" type here to enable the "_loaded" operation to pick up the existing gradient brush(WPF)  or solidbrush (silverlight)
                                              //// from the existing "past" button. For Silverlight "background" is "SolidColorBrush" and for WPF its "LinearGradientBrish"

        /// <summary>
        /// Specifies if the control should resize the children or not.
        /// </summary>
        private bool resizeChildren;

        /// <summary>
        /// Member variable to hold Button style.
        /// </summary>
        private Style buttonStyle;
       
        /// <summary>
        /// Saves the selection change parameters if selection change is deferred.
        /// </summary>
        private SelectionChangedEventArgs previousSelection;

        /// <summary>
        /// Stores the default background color for a button.
        /// </summary>
        private Brush defaultBackgroundColor;

        /// <summary>
        /// Initializes the on-hover background color for a button.
        /// </summary>
        private Brush hoverBackground;

        /// <summary>
        /// References the selected button.
        /// </summary>
        private Button presentSelection;

        /// <summary>
        /// References the Background color for the FilterDropDown.
        /// </summary>
        private Brush filterDropDownBackground;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterControl"/> class.
        /// </summary>
        public FilterControl()
        {            
            this.InitializeComponent();
            this.LostFocus += new RoutedEventHandler(this.FilterControl_LostFocus);
            this.defaultBackgroundColor = new SolidColorBrush(Colors.LightGray);
            this.hoverBackground = new SolidColorBrush(Colors.LightGray);
            this.presentSelection = this.FilterCurrentButton;
#if !SILVERLIGHT
            GradientStopCollection gSc = new GradientStopCollection();
            gSc.Add(new GradientStop()
            {
                Offset = 0.1,
                Color = Color.FromArgb(255, 189, 218, 247)
            });
            gSc.Add(new GradientStop()
            {
                Offset = 1,
                Color = Color.FromArgb(255, 255, 255, 255)
            }); 
            this.hoverBackground = new LinearGradientBrush(gSc);   
#endif
        }
                    
        #endregion

        #region Events and Delegates

        /// <summary>
        /// FilterBy event handler.
        /// </summary>
        public event System.EventHandler<System.EventArgs> OnFilter;

        /// <summary>
        /// SelectedIndexChanged event handler.
        /// </summary>
        public event System.EventHandler<System.EventArgs> SelectedIndexChanged;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether children should be resized on size change.
        /// </summary>
        /// <value>Children status on resize.</value>
        public bool ResizeChildren
        {
            get
            {
                return this.resizeChildren;
            }

            set
            {
                this.resizeChildren = value;
            }
        }

        /// <summary>
        /// Gets or sets the value for the drop down's selected index.
        /// </summary>
        /// <value>Selected Index.</value>
        public int SelectedIndex
        {
            get
            {
                return this.FilterDropDown.SelectedIndex;
            }

            set
            {
                this.FilterDropDown.SelectedIndex = value;
            }
        }        

        /// <summary>
        /// Gets the list of items.
        /// </summary>
        /// <value>Items list.</value>
        public System.Collections.IList Items
        {
            get
            {
                return this.FilterDropDown.Items;
            }
        }

        /// <summary>
        /// Gets the current button in the control.
        /// </summary>
        /// <value>Current button.</value>
        public Button CurrentButton
        {
            get 
            {
                return this.FilterCurrentButton;
            }
        }

        /// <summary>
        /// Gets or sets the style applied to buttons.
        /// </summary>
        /// <value>Button style.</value>
        public Style ButtonStyle
        {
            get { return this.buttonStyle; }
            set { this.buttonStyle = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the filter. 
        /// </summary>
        /// <param name="filter">Filter condition.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Exposing as method so that ISV could select the button")]
        public void SetFilter(string filter)
        {
            switch (filter)
            {
                case "Current":
                    this.SelectButton(this.FilterCurrentButton);
                    break;
                case "Past":
                case "PastTwoMonths":
                case "PastSixMonths":
                    this.SelectButton(this.FilterPastButton);
                    break;
                default:
                    this.SelectButton(this.FilterCurrentButton);
                    break;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Selects the specified filter button.
        /// </summary>
        /// <param name="button">Button to select.</param>
        private void SelectButton(Button button)
        {
            this.FilterCurrentButton.Background = this.unselectedFilterBrush;
            this.FilterPastButton.Background = this.unselectedFilterBrush;
            button.Background = this.selectedFilterBrush;
            this.presentSelection = button;
        }

        /// <summary>
        /// Handles the Click event of the FilterCurrentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>        
        private void FilterCurrentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.OriginalSource == sender)
            {
                this.FilterDropDown.SelectedIndex = -1;
                this.SelectButton(sender as Button);
                this.RaiseFilterEvent("Current");
            }
        }        
        
        /// <summary>
        /// Handles the Click event of the FilterPastButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void FilterPastButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.OriginalSource == sender)
            {
                this.FilterDropDown.SelectedIndex = -1;
                this.SelectButton(sender as Button);

                // PS 8166. Hide drop down on click of past button.
                if (this.dropDownOpen)
                {
                    this.HideDropDown();
                }

                this.RaiseFilterEvent("Past");
            }
        }

        /// <summary>
        /// Raises the OnFilter event if specified.
        /// </summary>
        /// <param name="filterCondition">Condition on which to filter the data.</param>
        private void RaiseFilterEvent(string filterCondition)
        {
            if (this.OnFilter != null)
            {
                this.OnFilter(this, new FilterEventArgs(filterCondition));
            }
        }

        /// <summary>
        /// Handles the Click event of the DropPastButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DropPastButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.dropDownOpen)
            {
                if (this.previousSelection == null)
                {
                    this.HideDropDown();                           
                }
                else
                {
                    this.DropPastListBox_SelectionChanged(this, this.previousSelection);
                    this.previousSelection = null;
                }
            }
            else
            {
                this.ShowDropDown();
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the DropPastListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void DropPastListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //////if (this.deferChange)
            //////{
            //////    this.previousSelection = e;
            //////    return;
            //////}            

            this.SelectButton(this.FilterPastButton);

            switch (this.FilterDropDown.SelectedIndex)
            {
                case 0:
                    this.RaiseFilterEvent("PastTwoMonths");
                    break;
                case 1:
                    this.RaiseFilterEvent("PastSixMonths");
                    break;
            }

            if (this.SelectedIndexChanged != null)
            {
                this.SelectedIndexChanged(this, e);
            }
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.unselectedFilterBrush = (Brush)this.FilterPastButton.Background; // Setting the unselected look to the default look linear gradient brush
            this.FilterDropDown.SelectedIndex = -1;
            this.FilterDropDown.SelectionChanged += new SelectionChangedEventHandler(this.DropPastListBox_SelectionChanged);
            
            if (null != this.buttonStyle)
            {
                this.CurrentButton.Style = this.buttonStyle;
                this.FilterPastButton.Style = this.buttonStyle;                
            }

#if !SILVERLIGHT
            GradientStopCollection gSc = new GradientStopCollection();
            gSc.Add(new GradientStop()
            {
                Offset = 0.986,
                Color = Color.FromArgb(255, 252, 245, 224)
            });
            gSc.Add(new GradientStop()
            {
                Offset = 0.01,
                Color = Color.FromArgb(255, 252, 196, 83)
            });
            gSc.Add(new GradientStop()
            {
                Offset = 0,
                Color = Color.FromArgb(255, 248, 246, 242)
            });

             this.selectedFilterBrush = new LinearGradientBrush(gSc);        
#endif
             this.FilterCurrentButton.Background = this.selectedFilterBrush;
        }

        /// <summary>
        /// Handles the LostFocus event of the filter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void FilterControl_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.dropDownOpen)
            {
                this.HideDropDown();
            }
        }

        /// <summary>
        /// Hides the drop down list.
        /// </summary>
        private void HideDropDown()
        {
            if (this.dropDownOpen)
            {               
                this.dropDownOpen = false;       
            }          
        }

        /// <summary>
        /// Displays the drop down list.
        /// </summary>
        private void ShowDropDown()
        {
            if (!this.dropDownOpen)
            {              
                this.dropDownOpen = true;
            }     
        }

        /// <summary>
        /// Displays the mouse hover effect.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterCurrentButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {            
#if !SILVERLIGHT
            Button temp = (sender as Button);
            if (this.presentSelection != temp)
            {
            this.defaultBackgroundColor = temp.Background; 
            temp.Background = this.hoverBackground;
            }          
#endif
        }

        /// <summary>
        /// Removes the mouse hover effect.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterCurrentButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {            
#if !SILVERLIGHT
            Button temp = (sender as Button);
            if (this.presentSelection != temp)
            {
            temp.Background = this.defaultBackgroundColor;
            }
            else
            {
            temp.Background = this.selectedFilterBrush;
            }
#endif
        }

        /// <summary>
        /// Displays the mouse hover effect.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterPastButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
#if !SILVERLIGHT
            Button temp = (sender as Button);
            if (this.presentSelection != temp)
            {
            this.defaultBackgroundColor = temp.Background; 
            temp.Background = this.hoverBackground;
            }
#endif
        }

        /// <summary>
        /// Removes the mouse hover effect.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterPastButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
#if !SILVERLIGHT
            Button temp = (sender as Button);
            if (this.presentSelection != temp)
            {
            temp.Background = this.defaultBackgroundColor;
            }
            else
            {
            temp.Background = this.selectedFilterBrush;
            }
#endif
        }

        /// <summary>
        /// Displays the mouse hover effect.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void DropPastButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
#if !SILVERLIGHT
            Button temp = (sender as Button);
            this.defaultBackgroundColor = temp.Background; 
            temp.Background = this.hoverBackground;
#endif
        }

        /// <summary>
        /// Removes the mouse hover effect.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void DropPastButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
#if !SILVERLIGHT
            Button temp = (sender as Button);
            if (this.presentSelection != temp)
            {
            temp.Background = this.defaultBackgroundColor;
            }
            else
            {
            temp.Background = this.selectedFilterBrush;
            }
#endif
        }

        /// <summary>
        /// Handles drop down got focus event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterDropDown_GotFocus(object sender, RoutedEventArgs e)
        {
            this.filterDropDownBackground = this.FilterDropDown.Background;
#if !SILVERLIGHT            
            this.FilterDropDown.Background = new SolidColorBrush(Colors.Transparent);
#endif
        }

        /// <summary>
        /// Handles drop down lost focus event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterDropDown_LostFocus(object sender, RoutedEventArgs e)
        {            
#if !SILVERLIGHT
            if (this.filterDropDownBackground != null)
            {
                this.FilterDropDown.Background = this.filterDropDownBackground;               
            }
#endif
        }

        /// <summary>
        /// Handles key down event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void FilterDropDown_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.PageUp)
            {
                this.FilterDropDown.SelectedIndex = 0;
            }
            else if (e.Key == Key.PageDown)
            {
                this.FilterDropDown.SelectedIndex = this.FilterDropDown.Items.Count - 1;
            }
        }
        #endregion        
    }
}
