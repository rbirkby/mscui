//-----------------------------------------------------------------------
// <copyright file="DecoratorTextBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Dec-2008</date>
// <summary>The control used to provide decoration of characters in a textbox.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// The control used to provide decoration of characters in a textbox.
    /// </summary>
    [StyleTypedProperty(Property = "DecoratorItemContainerStyle", StyleTargetType = typeof(DecoratorItemContainer))]
    [TemplatePart(Name = DecoratorTextBox.ElementTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = DecoratorTextBox.ElementDecoratorItemsControl, Type = typeof(DecoratorItemsControl))]
    [TemplatePart(Name = DecoratorTextBox.ElementScrollViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = DecoratorTextBox.ElementWatermarkContentPresenter, Type = typeof(ContentPresenter))]
    public class DecoratorTextBox : Control
    {
        /// <summary>
        /// The Item Container Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DecoratorItemContainerStyleProperty =
            DependencyProperty.Register("DecoratorItemContainerStyle", typeof(Style), typeof(DecoratorTextBox), null);

        /// <summary>
        /// The Term Items Source Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermItemsSourceProperty =
            DependencyProperty.Register("MatchingTermItemsSource", typeof(IEnumerable), typeof(DecoratorTextBox), new PropertyMetadata(new PropertyChangedCallback(MatchingTermItemsSource_Changed)));

        /// <summary>
        /// The term start index member path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermStartIndexMemberPathProperty =
            DependencyProperty.Register("MatchingTermStartIndexMemberPath", typeof(string), typeof(DecoratorTextBox), new PropertyMetadata(new PropertyChangedCallback(MatchingTermStartIndexMemberPath_Changed)));

        /// <summary>
        /// The term length member path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermLengthMemberPathProperty =
            DependencyProperty.Register("MatchingTermLengthMemberPath", typeof(string), typeof(DecoratorTextBox), new PropertyMetadata(new PropertyChangedCallback(MatchingTermLengthMemberPath_Changed)));

        /// <summary>
        /// The term is selected member path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermIsSelectedMemberPathProperty =
            DependencyProperty.Register("MatchingTermIsSelectedMemberPath", typeof(string), typeof(DecoratorTextBox), new PropertyMetadata(new PropertyChangedCallback(MatchingTermIsSelectedMemberPath_Changed)));

        /// <summary>
        /// The is read only Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DecoratorTextBox), new PropertyMetadata(false));

        /// <summary>
        /// The max text box length Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(DecoratorTextBox), new PropertyMetadata(null));

        /// <summary>
        /// The text box text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DecoratorTextBox), new PropertyMetadata(new PropertyChangedCallback(Text_Changed)));

        /// <summary>
        /// The watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(object), typeof(DecoratorTextBox), null);

        /// <summary>
        /// The watermark template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTemplateProperty =
            DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(DecoratorTextBox), null);

        /// <summary>
        /// The TextBox element name.
        /// </summary>
        private const string ElementTextBox = "TextBox";

        /// <summary>
        /// The DecoratorItemsControl element name.
        /// </summary>
        private const string ElementDecoratorItemsControl = "DecoratorItemsControl";

        /// <summary>
        /// The ScrollViewer name.
        /// </summary>
        private const string ElementScrollViewer = "ScrollViewer";

        /// <summary>
        /// The watermark ContentPresenter name.
        /// </summary>
        private const string ElementWatermarkContentPresenter = "WatermarkContentPresenter";

        /// <summary>
        /// Stores a collection of the displayed matching items.
        /// </summary>
        private ObservableCollection<object> displayedMatchingTerms = new ObservableCollection<object>();

        /// <summary>
        /// Stores the displayed matching terms.
        /// </summary>
        private Collection<TermItem> validMatchingTerms = new Collection<TermItem>();

        /// <summary>
        /// Stores the control's textbox.
        /// </summary>
        private TextBox textBox;

        /// <summary>
        /// Stores the control's items control.
        /// <para>
        /// The DecoratorItemsControl is used to draw a copy of each character behind the
        /// text box, enabling the control to draw decoration around each character in the
        /// text box, using control instances.
        /// </para>
        /// </summary>
        private DecoratorItemsControl decoratorItemsControl;

        /// <summary>
        /// Stores the term items control.
        /// <para>
        /// The TermItemsControl is a non-UI items control used for consuming an IEnumerable
        /// source of a list of terms, and returning a list of TermItem's, with the StartIndex,
        /// Length and IsSelected properties bound. These items are then used to decorate
        /// the individual text items.
        /// </para>
        /// </summary>
        private TermItemsControl termItemsControl;

        /// <summary>
        /// Stores the scrollviewer.
        /// </summary>
        private ScrollViewer scrollViewer;

        /// <summary>
        /// Stores the watermark content presenter.
        /// </summary>
        private ContentPresenter watermarkContentPresenter;

        /// <summary>
        /// Stores the selection start.
        /// </summary>
        private int selectionStart;

        /// <summary>
        /// Stores the selection end.
        /// </summary>
        private int selectionEnd;

        /// <summary>
        /// Stores a collection for the textbox items.
        /// </summary>
        private ObservableCollection<DecoratorTextBoxItem> textBoxItems = new ObservableCollection<DecoratorTextBoxItem>();

        /// <summary>
        /// Stores the initialised text.
        /// </summary>
        private string initialisedText = string.Empty;

        /// <summary>
        /// Stores whether the control has focus.
        /// </summary>
        private bool hasFocus;

        /// <summary>
        /// Stores a count of how many programatic text changes require processing.
        /// </summary>
        private int programaticTextChangeCount;

        /// <summary>
        /// Stores the term objects under the mouse.
        /// </summary>
        private List<object> termObjectsUnderMouse = new List<object>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorTextBox"/> class.
        /// </summary>
        public DecoratorTextBox()
        {
            this.DefaultStyleKey = typeof(DecoratorTextBox);

            this.termItemsControl = new TermItemsControl();
            this.termItemsControl.TermItemsCollectionChanged += new EventHandler(this.TermItemsControl_TermItemsCollectionChanged);
            this.termItemsControl.StartIndexMemberPath = this.MatchingTermStartIndexMemberPath;
            this.termItemsControl.LengthMemberPath = this.MatchingTermLengthMemberPath;
            this.termItemsControl.IsSelectedMemberPath = this.MatchingTermIsSelectedMemberPath;
            this.termItemsControl.ItemsSource = this.MatchingTermItemsSource;
            this.MouseMove += new MouseEventHandler(this.DecoratorTextBox_MouseMove);
            this.MouseLeave += new MouseEventHandler(this.DecoratorTextBox_MouseLeave);
            this.GotFocus += new RoutedEventHandler(this.DecoratorTextBox_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.DecoratorTextBox_LostFocus);
        }

        /// <summary>
        /// The text changed event.
        /// </summary>
        public event TextChangedEventHandler TextChanged;

        /// <summary>
        /// The text selection changed event.
        /// </summary>
        public event RoutedEventHandler SelectionChanged;

        /// <summary>
        /// The item mouse enter event.
        /// </summary>
        public event MouseEventHandler ItemMouseEnter;

        /// <summary>
        /// The item mouse leave event.
        /// </summary>
        public event MouseEventHandler ItemMouseLeave;

        /// <summary>
        /// The enter pressed event.
        /// </summary>
        public event KeyEventHandler EnterPressed;

        /// <summary>
        /// Gets or sets the text contents of the text box.
        /// </summary>
        /// <value>A string value.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is read only.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the textbox's max length.
        /// </summary>
        /// <value>An integer value.</value>
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        /// <value>An object value.</value>
        public object Watermark
        {
            get { return (object)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark template.
        /// </summary>
        /// <value>A DataTemplate value.</value>
        public DataTemplate WatermarkTemplate
        {
            get { return (DataTemplate)GetValue(WatermarkTemplateProperty); }
            set { SetValue(WatermarkTemplateProperty, value); }
        }

        /// <summary>
        /// Gets a list of selected terms.
        /// </summary>
        /// <value>A list of selected terms.</value>
        public IList SelectedTerms
        {
            get
            {
                ObservableCollection<object> selectedTerms = new ObservableCollection<object>();
                foreach (TermItem item in this.validMatchingTerms)
                {
                    if (item.IsSelected)
                    {
                        selectedTerms.Add(this.termItemsControl.GetItemFromContainer(item));
                    }
                }

                return selectedTerms;
            }
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        /// <value>A string value.</value>
        public string SelectedText
        {
            get
            {
                if (this.textBox != null)
                {
                    return this.textBox.SelectedText;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the text selection start index.
        /// </summary>
        /// <value>A string value.</value>
        public int SelectionStart
        {
            get
            {
                if (this.textBox != null)
                {
                    return this.textBox.SelectionStart;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the text selection length.
        /// </summary>
        /// <value>An integer value.</value>
        public int SelectionLength
        {
            get
            {
                if (this.textBox != null)
                {
                    return this.textBox.SelectionLength;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets a collection of the displayed matching items.
        /// </summary>
        /// <value>A collection of term data objects.</value>
        public ObservableCollection<object> DisplayedMatchingTerms
        {
            get { return this.displayedMatchingTerms; }
        }

        /// <summary>
        /// Gets or sets the term items source.
        /// </summary>
        /// <value>An IEnumerable value.</value>
        public IEnumerable MatchingTermItemsSource
        {
            get { return (IEnumerable)GetValue(MatchingTermItemsSourceProperty); }
            set { SetValue(MatchingTermItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the path to the term's start index property.
        /// </summary>
        /// <value>A string value.</value>
        public string MatchingTermStartIndexMemberPath
        {
            get { return (string)GetValue(MatchingTermStartIndexMemberPathProperty); }
            set { SetValue(MatchingTermStartIndexMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the path to the term's length property.
        /// </summary>
        /// <value>A string value.</value>
        public string MatchingTermLengthMemberPath
        {
            get { return (string)GetValue(MatchingTermLengthMemberPathProperty); }
            set { SetValue(MatchingTermLengthMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the path to the term's selected property.
        /// </summary>
        /// <value>A string value.</value>
        public string MatchingTermIsSelectedMemberPath
        {
            get { return (string)GetValue(MatchingTermIsSelectedMemberPathProperty); }
            set { SetValue(MatchingTermIsSelectedMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the decorator item container style.
        /// </summary>
        /// <value>A style for a DecoratorItemContainer.</value>
        public Style DecoratorItemContainerStyle
        {
            get { return (Style)GetValue(DecoratorItemContainerStyleProperty); }
            set { SetValue(DecoratorItemContainerStyleProperty, value); }
        }

        /// <summary>
        /// Gets whether the term is under the mouse.
        /// </summary>
        /// <param name="term">The term to check.</param>
        /// <returns>Whether the term is under the mouse.</returns>
        public bool TermUnderMouse(object term)
        {
            return this.termObjectsUnderMouse.Contains(term);
        }

        /// <summary>
        /// Selects a range of text in the text box.
        /// </summary>
        /// <param name="start">The zero-based index of the first character in the selection.</param>
        /// <param name="length">The length of the selection, in characters.</param>
        public void Select(int start, int length)
        {
            this.textBox.Select(start, length);
        }

        /// <summary>
        /// Selects the entire contents of the text box.
        /// </summary>
        public void SelectAll()
        {
            this.textBox.SelectAll();
        }

        /// <summary>
        /// Mouse highlights a specific term.
        /// </summary>
        /// <param name="term">The term data object.</param>
        /// <param name="highlighted">Whether the term should appear highlighted.</param>
        public void SetTermMouseHighlight(object term, bool highlighted)
        {
            TermItem termItem = this.termItemsControl.GetContainerFromItem(term);
            if (termItem != null && termItem.StartIndex >= 0 && termItem.Length > 0)
            {
                for (int i = termItem.StartIndex; i < termItem.StartIndex + termItem.Length; i++)
                {
                    this.textBoxItems[i].IsMouseHighlighted = highlighted;
                    this.decoratorItemsControl.SetContainerZIndex(this.textBoxItems[i], highlighted ? (i + 2) : 0);
                }
            }
        }

        /// <summary>
        /// Focus highlights a specific term.
        /// </summary>
        /// <param name="term">The term data object.</param>
        /// <param name="highlighted">Whether the term should appear highlighted.</param>
        public void SetTermFocusHighlight(object term, bool highlighted)
        {
            TermItem termItem = this.termItemsControl.GetContainerFromItem(term);
            if (termItem != null && termItem.StartIndex >= 0 && termItem.Length > 0)
            {
                for (int i = termItem.StartIndex; i < termItem.StartIndex + termItem.Length; i++)
                {
                    this.textBoxItems[i].IsFocusHighlighted = highlighted;
                    this.decoratorItemsControl.SetContainerZIndex(this.textBoxItems[i], highlighted ? (i + 2) : 0);
                }
            }
        }

        /// <summary>
        /// Gets the parts from the templates.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.scrollViewer = (ScrollViewer)this.GetTemplateChild(ElementScrollViewer);

            this.textBox = (TextBox)this.GetTemplateChild(ElementTextBox);
            if (this.textBox != null)
            {
                this.textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                this.textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                this.textBox.TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
                this.textBox.GotFocus += new RoutedEventHandler(this.TextBox_GotFocus);
                this.textBox.LostFocus += new RoutedEventHandler(this.TextBox_LostFocus);

#if SILVERLIGHT
                this.textBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);

                // This is used to make the selection background transparent in Silverlight. #715599DE
                this.textBox.SelectionBackground = new SolidColorBrush(Color.FromArgb(0x71, 0x55, 0x99, 0xde));
                this.textBox.SelectionForeground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
#else
                this.textBox.PreviewKeyDown += new KeyEventHandler(this.TextBox_KeyDown);
#endif
            }

            this.decoratorItemsControl = (DecoratorItemsControl)this.GetTemplateChild(ElementDecoratorItemsControl);
            if (this.decoratorItemsControl != null)
            {
                this.decoratorItemsControl.ItemsSource = this.textBoxItems;
            }

            if (this.scrollViewer != null && this.textBox != null && this.decoratorItemsControl != null)
            {
                this.textBox.SelectionChanged += new RoutedEventHandler(this.TextBox_SelectionChanged);
            }

            this.watermarkContentPresenter = (ContentPresenter)this.GetTemplateChild(ElementWatermarkContentPresenter);
            if (this.watermarkContentPresenter != null && !string.IsNullOrEmpty(this.Text))
            {
                this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
            }

            if (this.textBox != null)
            {
                this.textBox.Text = this.initialisedText;
                this.UpdateTextBoxItems();
            }
        }

        /// <summary>
        /// Sets the text in the text box. If uninitialised, stores it temporarily.
        /// </summary>
        /// <param name="text">The text to set.</param>
        internal void SetText(string text)
        {
            if (this.textBox != null)
            {
                this.textBox.Text = text;
            }
            else
            {
                this.initialisedText = text;
            }
        }

        /// <summary>
        /// Changes the text for a specific term.
        /// <para>
        /// This method uses the start index (from term item) to move through the decorator textbox
        /// items, and update the text in the item to show the new text. If the new text is longer, 
        /// more text box items are inserted. If the new text is shorter, text box items are removed.
        /// </para>
        /// <para>
        /// The text box text is then updated with the change, flagging that the change is a programmatic
        /// change. And finally, each term item that comes after the changed item, has the start index
        /// updated with its new start position.
        /// </para>
        /// </summary>
        /// <param name="termContainer">The term container to update.</param>
        internal void ChangeText(MatchingTermItemContainer termContainer)
        {
            if (termContainer != null)
            {
                TermItem termItem = this.termItemsControl.GetContainerFromItem(termContainer.DataContext);
                if (termItem != null)
                {
                    if (this.textBox != null)
                    {
                        this.programaticTextChangeCount++;
                    }

                    int count = 0;
                    bool mouseHighlighted = false;
                    bool focusHighlighted = false;
                    for (int i = termItem.StartIndex; i < Math.Min(termItem.StartIndex + termItem.Length, termItem.StartIndex + termContainer.SelectedItemText.Length); i++)
                    {
                        if (i == termItem.StartIndex)
                        {
                            focusHighlighted = this.textBoxItems[i].IsFocusHighlighted;
                            mouseHighlighted = this.textBoxItems[i].IsMouseHighlighted;
                        }

                        this.textBoxItems[i].Text = termContainer.SelectedItemText[count].ToString(CultureInfo.CurrentCulture);                        
                        count++;
                    }

                    if (termItem.Length < termContainer.SelectedItemText.Length)
                    {
                        if (termItem.Length > 1)
                        {
                            this.textBoxItems[termItem.StartIndex + termItem.Length - 1].ContainerPosition = ContainerPosition.Middle;
                        }
                        else
                        {
                            this.textBoxItems[termItem.StartIndex].ContainerPosition = ContainerPosition.Start;
                        }

                        for (int i = termItem.StartIndex + termItem.Length; i < termItem.StartIndex + termContainer.SelectedItemText.Length; i++)
                        {
                            DecoratorTextBoxItem textBoxItem = new DecoratorTextBoxItem(termContainer.SelectedItemText[i - termItem.StartIndex].ToString(CultureInfo.CurrentCulture));
                            if (i == termItem.StartIndex + termContainer.SelectedItemText.Length - 1)
                            {
                                textBoxItem.ContainerPosition = ContainerPosition.End;
                            }
                            else
                            {
                                textBoxItem.ContainerPosition = ContainerPosition.Middle;
                            }

                            textBoxItem.IsFocusHighlighted = focusHighlighted;
                            textBoxItem.IsMouseHighlighted = mouseHighlighted;
                            textBoxItem.AddTermItem(termItem);
                            this.textBoxItems.Insert(i, textBoxItem);
                        }
                    }
                    else if (termItem.Length > termContainer.SelectedItemText.Length)
                    {
                        for (int i = termItem.StartIndex + termContainer.SelectedItemText.Length; i < termItem.StartIndex + termItem.Length; i++)
                        {
                            this.textBoxItems.RemoveAt(termItem.StartIndex + termContainer.SelectedItemText.Length);
                        }

                        if (termContainer.SelectedItemText.Length > 1)
                        {
                            this.textBoxItems[termItem.StartIndex + termContainer.SelectedItemText.Length - 1].ContainerPosition = ContainerPosition.End;
                        }
                        else
                        {
                            this.textBoxItems[termItem.StartIndex].ContainerPosition = ContainerPosition.Only;
                        }
                    }                    

                    if (this.textBox != null)
                    {
                        string newText = this.textBox.Text.Remove(termItem.StartIndex, termItem.Length);
                        newText = newText.Insert(termItem.StartIndex, termContainer.SelectedItemText);
                        this.textBox.Text = newText;
                    }

                    foreach (TermItem item in this.termItemsControl.TermItems)
                    {
                        if (item.StartIndex > termItem.StartIndex)
                        {
                            item.StartIndex += termContainer.SelectedItemText.Length - termItem.Length;
                        }
                    }

                    termItem.Length = termContainer.SelectedItemText.Length;
                }
            }
        }

        /// <summary>
        /// Sets the text in the text box.
        /// </summary>
        /// <param name="dependencyObject">The Decorator Text Box.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void Text_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            DecoratorTextBox decoratorTextBox = (DecoratorTextBox)dependencyObject;
            decoratorTextBox.SetText(eventArgs.NewValue.ToString());
        }

        /// <summary>
        /// Splits the text.
        /// </summary>
        /// <param name="source">Splits the string on each character.</param>
        /// <returns>A list of string items.</returns>
        private static List<string> SplitText(string source)
        {
            List<string> splitStrings = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                splitStrings.Add(source[i].ToString(CultureInfo.CurrentCulture));
            }

            return splitStrings;
        }

        /// <summary>
        /// Updates the term items source.
        /// </summary>
        /// <param name="sender">The DecoratorTextBox.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void MatchingTermItemsSource_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DecoratorTextBox decoratorTextBox = (DecoratorTextBox)sender;
            if (decoratorTextBox.termItemsControl != null)
            {
                decoratorTextBox.termItemsControl.ItemsSource = (IEnumerable)args.NewValue;
            }
        }

        /// <summary>
        /// Updates the term start index member path.
        /// </summary>
        /// <param name="sender">The DecoratorTextBox.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void MatchingTermStartIndexMemberPath_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DecoratorTextBox decoratorTextBox = (DecoratorTextBox)sender;
            if (decoratorTextBox.termItemsControl != null)
            {
                decoratorTextBox.termItemsControl.StartIndexMemberPath = (string)args.NewValue;
            }
        }

        /// <summary>
        /// Updates the term length member path.
        /// </summary>
        /// <param name="sender">The DecoratorTextBox.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void MatchingTermLengthMemberPath_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DecoratorTextBox decoratorTextBox = (DecoratorTextBox)sender;
            if (decoratorTextBox.termItemsControl != null)
            {
                decoratorTextBox.termItemsControl.LengthMemberPath = (string)args.NewValue;
            }
        }

        /// <summary>
        /// Updates the term selected member path.
        /// </summary>
        /// <param name="sender">The DecoratorTextBox.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void MatchingTermIsSelectedMemberPath_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DecoratorTextBox decoratorTextBox = (DecoratorTextBox)sender;
            if (decoratorTextBox.termItemsControl != null)
            {
                decoratorTextBox.termItemsControl.IsSelectedMemberPath = (string)args.NewValue;
            }
        }

        /// <summary>
        /// Raises the EnterPressed event.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Key Event Args.</param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.EnterPressed != null)
                {
                    this.EnterPressed(this, e);
                }
            }
        }

        /// <summary>
        /// Updates the text property. If the change was non-programatic, the handler updates the text box items collection,
        /// and raises a text changed event.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Text changed event args.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.programaticTextChangeCount > 0)
            {
                this.Text = this.textBox.Text;
                this.programaticTextChangeCount--;
                return;
            }

            this.UpdateTextBoxItems();
            this.Text = this.textBox.Text;

            if (string.IsNullOrEmpty(this.Text) && !this.hasFocus && this.watermarkContentPresenter != null)
            {
                this.watermarkContentPresenter.Visibility = Visibility.Visible;
            }
            else if (!string.IsNullOrEmpty(this.Text))
            {
                this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
            }

            if (this.TextChanged != null)
            {
                this.TextChanged(this, e);
            }
        }

        /// <summary>
        /// Hides the watermark.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = true;
            if (this.watermarkContentPresenter != null)
            {
                this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Shows the watermark, if the text box is empty.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = false;
            if (string.IsNullOrEmpty(this.Text) && this.watermarkContentPresenter != null)
            {
                this.watermarkContentPresenter.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Updates the scrollviewer position.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Routed event args.</param>
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (this.selectionStart != this.textBox.SelectionStart)
            {
                this.selectionStart = this.textBox.SelectionStart;
                this.UpdateScrollViewer((int)Math.Max(0, this.selectionStart - 1));
            }

            if (this.selectionEnd != this.textBox.SelectionStart + this.textBox.SelectionLength)
            {
                this.selectionEnd = this.textBox.SelectionStart + this.textBox.SelectionLength;
                this.UpdateScrollViewer((int)Math.Max(0, this.selectionEnd - 1));
            }

            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, e);
            }
        }

        /// <summary>
        /// Updates the item decorators.
        /// </summary>
        /// <param name="sender">The indexer items control.</param>
        /// <param name="e">Event args.</param>
        private void TermItemsControl_TermItemsCollectionChanged(object sender, EventArgs e)
        {
            this.validMatchingTerms.Clear();

            foreach (TermItem item in this.termItemsControl.TermItems)
            {
                this.validMatchingTerms.Add(item);
            }

            if (this.textBox != null)
            {
                this.UpdateItemDecorators();
            }
        }

        /// <summary>
        /// Highlights an item if the item is under the mouse.
        /// </summary>
        /// <param name="sender">The decorator text box.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void DecoratorTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.decoratorItemsControl != null)
            {                
                List<object> highlightedTerms = new List<object>();
                foreach (DecoratorTextBoxItem item in this.textBoxItems)
                {
                    FrameworkElement element = this.decoratorItemsControl.GetContainerFromItem(item);
                    if (element != null && VisualTreeHelper.GetParent(element) != null)
                    {
                        Point pos = e.GetPosition(element);
                        if (pos.X >= 0 && pos.X <= element.ActualWidth && pos.Y >= 0 && pos.Y <= element.ActualHeight)
                        {                            
                            foreach (TermItem termItem in item.TermItems)
                            {
                                object termObject = this.termItemsControl.GetItemFromContainer(termItem);
                                highlightedTerms.Add(termObject);
                                this.SetTermMouseHighlight(termObject, true);
                            }
                        }
                        else
                        {
                            foreach (TermItem termItem in item.TermItems)
                            {
                                object termObject = this.termItemsControl.GetItemFromContainer(termItem);
                                if (!highlightedTerms.Contains(termObject))
                                {
                                    this.SetTermMouseHighlight(termObject, false);
                                }
                            }
                        }
                    }
                }

                bool matchingCollection = true;
                if (highlightedTerms.Count != this.termObjectsUnderMouse.Count)
                {
                    matchingCollection = false;
                }
                else
                {
                    for (int i = 0; i < highlightedTerms.Count; i++)
                    {
                        if (highlightedTerms[i] != this.termObjectsUnderMouse[i])
                        {
                            matchingCollection = false;
                            break;
                        }
                    }
                }

                if (!matchingCollection)
                {
                    if (this.ItemMouseLeave != null)
                    {
                        this.ItemMouseLeave(this.termObjectsUnderMouse, e);
                    }

                    if (this.ItemMouseEnter != null && highlightedTerms.Count > 0)
                    {
                        this.ItemMouseEnter(highlightedTerms, e);
                    }

                    this.termObjectsUnderMouse = highlightedTerms;
                }
            }
        }

        /// <summary>
        /// Unhighlights any terms that are highlighted.
        /// </summary>
        /// <param name="sender">The decorator text box.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void DecoratorTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (object term in this.termObjectsUnderMouse)
            {
                this.SetTermMouseHighlight(term, false);                
            }

            if (this.ItemMouseLeave != null)
            {
                this.ItemMouseLeave(this.termObjectsUnderMouse, e);
            }

            this.termObjectsUnderMouse.Clear();
        }

        /// <summary>
        /// Moves focus to the main text box.
        /// </summary>
        /// <param name="sender">The decorator text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DecoratorTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.IsTabStop = false;
            if (this.textBox != null)
            {
                this.textBox.Focus();
            }
        }

        /// <summary>
        /// Sets the losing focus flag.
        /// </summary>
        /// <param name="sender">The decorator text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DecoratorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsTabStop = true;
        }

        /// <summary>
        /// Updates the text box items.
        /// <para>
        /// For each character in the text, a TextBoxItem is created. This collection of TextBoxItems
        /// is then bound to the DecoratorItemsControl. Each character in the text can then be individually
        /// highlighted.
        /// </para>
        /// <para>
        /// The method below splits the text box text, and then moves through the existing list of TextBoxItems
        /// to identify where the change took place. At the point where the change has taken place, all new
        /// characters are inserted, and any existing characters afterwards are removed.
        /// </para>
        /// <para>
        /// After the TextBoxItems have been updated, any decorators that appear after the change start are cleared.
        /// If the change took place at the beginning, or a character is deleted from the middle, then the 
        /// decorators are refreshed.
        /// </para>
        /// </summary>
        private void UpdateTextBoxItems()
        {
            List<string> textParts = SplitText(this.textBox.Text);
            int itemCount = 0;
            bool changeFound = false;
            bool updateDecorators = false;

            foreach (string part in textParts)
            {
                if (this.textBoxItems.Count <= itemCount)
                {
                    break;
                }
                else if (this.textBoxItems[itemCount].Text != part)
                {
                    // found a non match.
                    this.textBoxItems.Insert(
                        itemCount,
                        new DecoratorTextBoxItem(part));

                    if (!changeFound)
                    {
                        changeFound = true;
                        this.ClearDecorators(itemCount);

                        List<TermItem> deadTerms = new List<TermItem>();
                        foreach (TermItem item in this.validMatchingTerms)
                        {
                            if (itemCount < item.StartIndex + item.Length)
                            {
                                deadTerms.Add(item);
                            }
                        }

                        foreach (TermItem deadTerm in deadTerms)
                        {
                            this.validMatchingTerms.Remove(deadTerm);
                        }
                    }
                }

                itemCount++;
            }

            // If there are less items now than previously...
            if (itemCount < this.textBoxItems.Count)
            {
                if (!changeFound)
                {
                    changeFound = true;
                    updateDecorators = true;
                }

                int textBoxItemsCount = this.textBoxItems.Count;
                for (int i = itemCount; i < textBoxItemsCount; i++)
                {
                    this.textBoxItems.RemoveAt(itemCount);
                }
            }

            if (itemCount < textParts.Count)
            {
                if (!changeFound)
                {
                    changeFound = true;
                    updateDecorators = true;
                }

                // Adds any remaining items.
                for (int i = itemCount; i < textParts.Count; i++)
                {
                    this.textBoxItems.Add(new DecoratorTextBoxItem(textParts[i]));
                }
            }

            if (updateDecorators)
            {
                this.UpdateItemDecorators();
            }
        }

        /// <summary>
        /// Updates the scroll viewer position.
        /// </summary>
        /// <param name="index">The current item index.</param>
        private void UpdateScrollViewer(int index)
        {
            if (index < this.textBoxItems.Count)
            {
                DecoratorItemContainer container = this.decoratorItemsControl.GetContainerFromItem(this.textBoxItems[index]);

                if (container != null && this.scrollViewer != null)
                {
                    if (this.scrollViewer.VerticalOffset > container.Top)
                    {
                        this.scrollViewer.ScrollToVerticalOffset(container.Top - 5);
                    }
                    else if (this.scrollViewer.VerticalOffset + this.scrollViewer.ViewportHeight < container.Top + container.ActualHeight)
                    {
                        this.scrollViewer.ScrollToVerticalOffset(
                            container.Top + container.ActualHeight - this.scrollViewer.ViewportHeight + 5);
                    }
                }
            }
        }

        /// <summary>
        /// Clears all of the decorators.
        /// </summary>
        /// <param name="startIndex">The index for where to start clearing.</param>
        private void ClearDecorators(int startIndex)
        {
            if (this.textBoxItems.Count > startIndex)
            {
                int termStartIndex = startIndex;

                for (int i = startIndex; i < this.textBoxItems.Count; i++)
                {
                    this.textBoxItems[i].IsFocusHighlighted = false;
                    this.textBoxItems[i].IsMouseHighlighted = false;

                    foreach (TermItem termItem in this.textBoxItems[i].TermItems)
                    {
                        if (termItem.StartIndex < startIndex)
                        {
                            termStartIndex = termItem.StartIndex;
                            break;
                        }

                        if (termStartIndex != startIndex)
                        {
                            break;
                        }
                    }
                }

                foreach (TermItem termItem in this.termItemsControl.TermItems)
                {
                    if (termItem.StartIndex >= termStartIndex)
                    {
                        this.displayedMatchingTerms.Remove(this.termItemsControl.GetItemFromContainer(termItem));
                    }
                }

                for (int i = termStartIndex; i < this.textBoxItems.Count; i++)
                {
                    this.textBoxItems[i].ClearTermItems();
                }
            }
        }

        /// <summary>
        /// Updates the item decorators.
        /// <para>
        /// This moves through the list of current valid terms. If each term's start index + length
        /// is within the count of text box items, then the term is assigned to each of the text items
        /// it covers. 
        /// </para>
        /// <para>
        /// The position of the text item in relation to the term is also set allowing different
        /// visual states for characters that are at the beginning, middle or end of a term.
        /// </para>
        /// <para>
        /// The 'displayed matching terms' collection is also updated, so that UI outside of the
        /// text box can display a list of matching terms.
        /// </para>
        /// <para>
        /// Any term that is outside of the bounds of the text items count is also removed from the valid
        /// terms collection.
        /// </para>
        /// </summary>
        private void UpdateItemDecorators()
        {
            this.displayedMatchingTerms.Clear();
            this.ClearDecorators(0);
            int endIndex = this.textBoxItems.Count;

            List<TermItem> removedItems = new List<TermItem>();

            foreach (TermItem item in this.validMatchingTerms)
            {
                if (item.StartIndex <= endIndex &&
                    item.StartIndex + item.Length <= endIndex &&
                    this.textBoxItems.Count >= item.StartIndex &&
                    this.textBoxItems.Count >= item.StartIndex + item.Length)
                {
                    for (int i = item.StartIndex; i < item.StartIndex + item.Length; i++)
                    {
                        if (i == item.StartIndex && item.Length == 1)
                        {
                            this.textBoxItems[i].ContainerPosition = ContainerPosition.Only;
                        }
                        else if (i == item.StartIndex)
                        {
                            this.textBoxItems[i].ContainerPosition = ContainerPosition.Start;
                        }
                        else if (i == item.StartIndex + item.Length - 1)
                        {
                            this.textBoxItems[i].ContainerPosition = ContainerPosition.End;
                        }
                        else
                        {
                            this.textBoxItems[i].ContainerPosition = ContainerPosition.Middle;
                        }

                        this.textBoxItems[i].AddTermItem(item);
                    }

                    this.displayedMatchingTerms.Add(this.termItemsControl.GetItemFromContainer(item));
                }
                else
                {
                    removedItems.Add(item);
                }
            }

            foreach (TermItem item in removedItems)
            {
                this.validMatchingTerms.Remove(item);
            }
        }
    }
}
