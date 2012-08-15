//-----------------------------------------------------------------------
// <copyright file="LabeledContentControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>17-Aug-2009</date>
// <summary>
//      Content control with a label.
// </summary>
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
    /// Content control with a label.
    /// </summary>
    [TemplatePart(Name = LabeledContentControl.ElementLabelAreaElement, Type = typeof(UIElement))]
    public class LabeledContentControl : ContentControl
    {
        /// <summary>
        /// The Label Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(object), typeof(LabeledContentControl), null);

        /// <summary>
        /// The LabelTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LabelTemplateProperty =
            DependencyProperty.Register("LabelTemplate", typeof(DataTemplate), typeof(LabeledContentControl), null);

        /// <summary>
        /// Stores the name of the label area element.
        /// </summary>
        private const string ElementLabelAreaElement = "LabelAreaElement";

        /// <summary>
        /// LabeledContentControl constructor.
        /// </summary>
        public LabeledContentControl()
        {
            this.DefaultStyleKey = typeof(LabeledContentControl);            
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label value.</value>
        public object Label
        {
            get { return (object)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the label template.
        /// </summary>
        /// <value>The label template value.</value>
        public DataTemplate LabelTemplate
        {
            get { return (DataTemplate)GetValue(LabelTemplateProperty); }
            set { SetValue(LabelTemplateProperty, value); }
        }

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UIElement labelAreaElement = this.GetTemplateChild(LabeledContentControl.ElementLabelAreaElement) as UIElement;
            if (labelAreaElement != null)
            {
                labelAreaElement.MouseLeftButtonDown += new MouseButtonEventHandler(this.LabelAreaElement_MouseLeftButtonDown);
            }
        }

        /// <summary>
        /// Gets the fist control in a visual tree.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <returns>The first control in the tree.</returns>
        private static Control GetFirstControl(UIElement element)
        {
            Control control = element as Control;
            if (control != null)
            {
                return control;
            }

            Panel panel = element as Panel;
            if (panel != null)
            {
                foreach (UIElement child in panel.Children)
                {
                    Control childControl = LabeledContentControl.GetFirstControl(child);
                    if (childControl != null)
                    {
                        return childControl;
                    }
                }
            }

            ContentPresenter contentPresenter = element as ContentPresenter;
            if (contentPresenter != null)
            {
                UIElement contentElement = contentPresenter.Content as UIElement;
                if (contentElement != null)
                {
                    return LabeledContentControl.GetFirstControl(contentElement);
                }
            }

            return null;
        }

        /// <summary>
        /// Gives focus to the first control in the tree.
        /// </summary>
        /// <param name="sender">The label area element.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void LabelAreaElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement content = this.Content as UIElement;
            if (content != null)
            {
                Control control = LabeledContentControl.GetFirstControl(content);
                if (control != null)
                {
                    FocusHelper.FocusControl(control);
                }
            }
        }
    }
}
