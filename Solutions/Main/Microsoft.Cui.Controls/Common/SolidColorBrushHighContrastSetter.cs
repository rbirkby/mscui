//-----------------------------------------------------------------------
// <copyright file="SolidColorBrushHighContrastSetter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Nov-2009</date>
// <summary>Adds attached properties to a solid color brush for specifying values for high contrast mode.</summary>
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
    /// Adds attached properties to a solid color brush for specifying values for high contrast mode.
    /// </summary>
    public class SolidColorBrushHighContrastSetter : BrushHighContrastSetter
    {
        /// <summary>
        /// The Color Attached Property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.RegisterAttached("Color", typeof(Color), typeof(SolidColorBrushHighContrastSetter), new PropertyMetadata(Color.FromArgb(0x01, 0x00, 0x00, 0x00), new PropertyChangedCallback(Color_Changed)));

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The color.</returns>
        public static Color GetColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(ColorProperty);
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The color.</param>
        public static void SetColor(DependencyObject obj, Color value)
        {
            obj.SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Updates the color if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void Color_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                SolidColorBrush solidColorBrush = obj as SolidColorBrush;                
                Color color = (Color)args.NewValue;
                if (solidColorBrush != null && color != null)
                {
                    solidColorBrush.Color = color;
                }
            }
        }
    }
}
