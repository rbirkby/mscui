//-----------------------------------------------------------------------
// <copyright file="VisualFocusLine.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Sep-2008</date>
// <summary>The VisualFocusLine Control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Shapes;
    using Microsoft.Cui.Controls.GraphingControl;

    /// <summary>
    /// Control for visual focus line.
    /// </summary>
    public class VisualFocusLine : Control
    {
        #region Template Part Names
        /// <summary>
        /// Template part name for the visual focus line.
        /// </summary>
        private const string VisualFocusLineElementName = "ELEMENT_VisualFocusLine";

        /// <summary>
        /// Template part name for the top place holder.
        /// </summary>
        private const string TopPlaceHolderElementName = "ELEMENT_TopPlaceHolder";

        /// <summary>
        /// Template part name for the bottom place holder.
        /// </summary>
        private const string BottomPlaceHolderElementName = "ELEMENT_BottomPlaceHolder";

        /// <summary>
        /// Template part name for the top date time text block.
        /// </summary>
        private const string TopTextBlockElementName = "ELEMENT_TopDateTimeTextBlock";

        /// <summary>
        /// Template part name for the bottom date time text block.
        /// </summary>
        private const string BottomTextBlockElementName = "ELEMENT_BottomDateTimeTextBlock";

        /// <summary>
        /// Template part name for the visual focus element.
        /// </summary>
        private const string FocusVisualElementName = "FocusVisualElement";
        #endregion

        #region Error Messages
        /// <summary>
        /// Error message string used when the template part element is missing.
        /// </summary>
        private const string TemplatePartElementNullMessage = @"Could not find an element with name '{0}' in the template.";

        /// <summary>
        /// Error message string used when the template part element is of incorrect type.
        /// </summary>
        private const string TemplatePartElementTypeInvalidMessage = @"Element with name '{0}' in the template is of invalid type. Expected type is '{1}'.";
        #endregion

        #region Template Elements
        /// <summary>
        /// Member variable to hold line in the visual focus line template.
        /// </summary>
        private Line vflLine;

        /// <summary>
        /// Member variable to hold top placeholder in the visual focus line template.
        /// </summary>
        private FrameworkElement topDateTimePlaceHolder;

        /// <summary>
        /// Member variable to hold bottom placeholder in the visual focus line template.
        /// </summary>
        private FrameworkElement bottomDateTimePlaceHolder;

        /// <summary>
        /// Member variable to hold top textblock in the visual focus line template.
        /// </summary>
        private TextBlock topDateTimeTextBlock;

        /// <summary>
        /// Member variable to hold bottom textblock in the visual focus line template.
        /// </summary>
        private TextBlock bottomDateTimeTextBlock;

        /// <summary>
        /// Member variable to hold focus visual.
        /// </summary>
        private FrameworkElement focusVisual;
        #endregion

        #region Member Variables
        /// <summary>
        /// Member variable to hold datetime for the visual focus line.
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// Member variable to hold xoffset for the visual focus line.
        /// </summary>
        private double xoffset;        

        /// <summary>
        /// Member variable to indicate whether the template part elements are loaded.
        /// </summary>
        private bool templatePartsLoaded;

        /// <summary>
        /// Member variable indicating whether VFL is currently holding the focus.
        /// </summary>
        private bool focussed;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualFocusLine"/> class.
        /// </summary>
        public VisualFocusLine()
        {
            this.DefaultStyleKey = typeof(VisualFocusLine);
            this.SizeChanged += new SizeChangedEventHandler(this.VisualFocusLine_SizeChanged);
            this.DateFormatString = GraphingResources.VisualFocusLineDateFormat;
            this.AllowDragDrop = true;
        }
        #endregion

        #region Events
        /// <summary>
        /// Raised when visual focus line is being moved.
        /// </summary>
        public event RoutedEventHandler VisualFocusLineMove;

        /// <summary>
        /// Raised when visual focus line move is completed.
        /// </summary>
        public event MouseButtonEventHandler VisualFocusLineDragComplete;
        #endregion

        #region Public Properties
#if SILVERLIGHT
        /// <summary>
        /// Gets or sets a value indicating whether the mouse is currently being captured.
        /// </summary>
        /// <value>Value indicating whether the mouse is currently being captured.</value>
        public bool IsMouseCaptured
        {
            get;
            set;
        }
#endif

        /// <summary>
        /// Gets or sets the Xoffset for the visual focus line.
        /// </summary>
        /// <value>Xoffset for the visual focus line.</value>
        public double XOffset
        {
            get 
            { 
                return this.xoffset; 
            }

            set 
            {
                this.xoffset = value;
                this.SetVFL();
            }
        }

        /// <summary>
        /// Gets or sets the datetime for the visual focus line.
        /// </summary>
        /// <value>Datetime for the visual focus line.</value>
        public DateTime DateTime
        {
            get 
            {
                return this.dateTime; 
            }

            set 
            {
                this.dateTime = value;
                this.SetVFL();
            }
        }

        /// <summary>
        /// Gets or sets the date format for the date displayed on visual focus line.
        /// </summary>
        /// <value>Date format for the date displayed on visual focus line.</value>
        public string DateFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the thickness of the visual focus line.
        /// </summary>
        /// <value>Thickness of the visual focus line.</value>
        public double VisualFocusLineThickness
        {
            get
            {
                if (this.vflLine != null)
                {
                    return this.vflLine.StrokeThickness;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow drag drop].
        /// </summary>
        /// <value>If [allow drag drop]<c>true</c>; otherwise, <c>false</c>.</value>
        public bool AllowDragDrop
        {
            get;
            set;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Overriden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            this.vflLine = this.GetTemplateChild<Line>(VisualFocusLineElementName, true);
            this.topDateTimePlaceHolder = this.GetTemplateChild<FrameworkElement>(TopPlaceHolderElementName, true);
            this.bottomDateTimePlaceHolder = this.GetTemplateChild<FrameworkElement>(BottomPlaceHolderElementName, true);
            this.topDateTimeTextBlock = this.GetTemplateChild<TextBlock>(TopTextBlockElementName, true);
            this.bottomDateTimeTextBlock = this.GetTemplateChild<TextBlock>(BottomTextBlockElementName, true);

            this.focusVisual = this.GetTemplateChild<FrameworkElement>(FocusVisualElementName, false);
            
            this.templatePartsLoaded = true;            
            this.SetVFL();

            if (this.xoffset != 0)
            {
                this.focussed = true;
                this.ShowFocusVisual(true);
            }

#if SILVERLIGHT
            this.IsMouseCaptured = false;
            this.vflLine.UseLayoutRounding = false;
#else
            this.vflLine.SnapsToDevicePixels = true;
#endif
        }

        /// <summary>
        /// Overridden. Handles the mouse enter event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);            
            this.ShowFocusVisual(true);

            if (this.AllowDragDrop)
            {
                this.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// Overridden. Handles the mouse leave event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);            
            this.ShowFocusVisual(false);
            this.Cursor = null;
        }

        /// <summary>
        /// Overridden. Handles the got focus event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.focussed = true;
            this.ShowFocusVisual(true);
        }

        /// <summary>
        /// Overridden. Handles the lost focus event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            this.focussed = false;
            this.ShowFocusVisual(false);
        }

        /// <summary>
        /// Overridden. Handles the key down event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!e.Handled)
            {
                bool ctrlKeyPressed;
                bool shiftKeyPressed;
                bool altKeyPressed;

                KeyboardHelper.GetMetaKeyState(out ctrlKeyPressed, out shiftKeyPressed, out altKeyPressed);

                if (!altKeyPressed)
                {
                    switch (e.Key)
                    {
                        case Key.Left:
                        case Key.Right:
                            if (this.VisualFocusLineMove != null)
                            {
                                this.VisualFocusLineMove(this, e);
                            }

                            e.Handled = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Overridden. Handles the mouse move event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

#if SILVERLIGHT
            if (this.IsMouseCaptured)
            {
                if (this.VisualFocusLineMove != null)
                {
                    this.VisualFocusLineMove(this, e);
                }
            }
#else
            if (this.vflLine.IsMouseCaptured)
            {
                if (this.VisualFocusLineMove != null)
                {
                    this.VisualFocusLineMove(this, e);
                }
            }
#endif
        }

        /// <summary>
        /// Overridden. Handles the mouse left button down event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);           

            if (!this.IsMouseCaptured)
            {
#if SILVERLIGHT
                this.IsMouseCaptured = this.vflLine.CaptureMouse();
#else
                this.vflLine.CaptureMouse();
#endif
            }

            this.Focus();
        }

        /// <summary>
        /// Overridden. Handles the mouse left button up event of the visual focus line.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

#if SILVERLIGHT
            this.IsMouseCaptured = false;
#endif
            this.vflLine.ReleaseMouseCapture();

            if (this.VisualFocusLineDragComplete != null)
            {
                this.VisualFocusLineDragComplete(this, e);
            }
        }
        #endregion

        #region Property Changed Callback
        /// <summary>
        /// Handles the property changed event for XOffset and DateTime.
        /// </summary>
        /// <param name="d">Dependency object whose property got changed.</param>
        /// <param name="e">Event args containing instance data.</param>
        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualFocusLine vfl = d as VisualFocusLine;
            if (vfl != null && e.OldValue != e.NewValue)
            {
                vfl.SetVFL();
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an element in the template.
        /// </summary>
        /// <typeparam name="T">Type of the element.</typeparam>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="raiseExceptions">Boolean to indicate whether to raise exceptions when not found.</param>
        /// <returns>If an element is found with the specified name and type then returns the element, else null.</returns>
        /// <remarks>If <paramref name="raiseExceptions"/> is true and the element is not found then an exception of type <see cref="System.ArgumentNullException"/> is thrown. 
        /// If an element is found but is not of specified type then an exception of type <see cref="System.ArgumentException"/> is thrown</remarks>        
        private T GetTemplateChild<T>(string elementName, bool raiseExceptions)
        {
            object obj = null;
            obj = this.GetTemplateChild(elementName);
            T typeCastedObj;

            if (raiseExceptions)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementNullMessage, elementName));
                }

                typeCastedObj = (T)obj;
                if (typeCastedObj == null)
                {
                    throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementTypeInvalidMessage, elementName, typeof(T).FullName));
                }
            }

            return (T)obj;
        }
        
        /// <summary>
        /// Handles the size changed event of the visual focus line.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void VisualFocusLine_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SetVFL();
        }            

        /// <summary>
        /// Positions the VFL and other elements.
        /// </summary>
        private void SetVFL()
        {
            if (this.templatePartsLoaded)
            {
                this.vflLine.X1 = this.vflLine.X2 = this.XOffset;
                this.topDateTimeTextBlock.Text = this.bottomDateTimeTextBlock.Text = this.DateTime.ToString(this.DateFormatString, CultureInfo.CurrentCulture);

                this.vflLine.Y1 = 0;
                this.vflLine.Y2 = this.ActualHeight;

                Canvas.SetTop(this.topDateTimePlaceHolder, 0);
                Canvas.SetTop(this.bottomDateTimePlaceHolder, this.ActualHeight - this.bottomDateTimePlaceHolder.ActualHeight);

                if (this.focusVisual != null)
                {
                    Canvas.SetLeft(this.focusVisual, this.XOffset);
                    this.focusVisual.Height = this.ActualHeight;
                }

                if (this.XOffset - this.topDateTimePlaceHolder.ActualWidth / 2 < 0)
                {
                    Canvas.SetLeft(this.topDateTimePlaceHolder, 0);
                    Canvas.SetLeft(this.bottomDateTimePlaceHolder, 0);
                }
                else if (this.XOffset + this.topDateTimePlaceHolder.ActualWidth / 2 > this.ActualWidth)
                {
                    Canvas.SetLeft(this.topDateTimePlaceHolder, this.ActualWidth - this.topDateTimePlaceHolder.ActualWidth);
                    Canvas.SetLeft(this.bottomDateTimePlaceHolder, this.ActualWidth - this.bottomDateTimePlaceHolder.ActualWidth);
                }                
                else
                {
                    Canvas.SetLeft(this.topDateTimePlaceHolder, this.XOffset - this.topDateTimePlaceHolder.ActualWidth / 2);
                    Canvas.SetLeft(this.bottomDateTimePlaceHolder, this.XOffset - this.bottomDateTimePlaceHolder.ActualWidth / 2);
                }
            }
        }

        /// <summary>
        /// Shows or hides the focus visual.
        /// </summary>
        /// <param name="show">Value indicating whether to show or hide.</param>
        private void ShowFocusVisual(bool show)
        {
            if (this.focusVisual != null)
            {
                if (show)
                {
                    this.focusVisual.Visibility = Visibility.Visible;
                }
                else
                {
                    if (!this.focussed)
                    {
                        this.focusVisual.Visibility = Visibility.Collapsed;
                    }
                }
            }            
        }
        #endregion
    }
}
