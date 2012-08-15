//-----------------------------------------------------------------------
// <copyright file="LinearGradientBrushHighContrastSetter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Adds attached properties to a linear gradient brush for specifying values for high contrast mode.</summary>
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
    using System.Globalization;

    /// <summary>
    /// Adds attached properties to a linear gradient brush for specifying values for high contrast mode.
    /// </summary>
    public class LinearGradientBrushHighContrastSetter : BrushHighContrastSetter
    {
        /// <summary>
        /// The GradientStops Attached Property.
        /// </summary>
        public static readonly DependencyProperty GradientStopsProperty =
            DependencyProperty.RegisterAttached("GradientStops", typeof(string), typeof(LinearGradientBrushHighContrastSetter), new PropertyMetadata(null, new PropertyChangedCallback(GradientStops_Changed)));

        /// <summary>
        /// The SpreadMethod Attached Property.
        /// </summary>
        public static readonly DependencyProperty SpreadMethodProperty =
            DependencyProperty.RegisterAttached("SpreadMethod", typeof(GradientSpreadMethod), typeof(LinearGradientBrushHighContrastSetter), new PropertyMetadata(GradientSpreadMethod.Pad, new PropertyChangedCallback(SpreadMethod_Changed)));
        
        /// <summary>
        /// The EndPoint Attached Property.
        /// </summary>
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.RegisterAttached("EndPoint", typeof(Point), typeof(LinearGradientBrushHighContrastSetter), new PropertyMetadata(new Point(0.5, 1.0), new PropertyChangedCallback(EndPoint_Changed)));

        /// <summary>
        /// The StartPoint Attached Property.
        /// </summary>
        public static readonly DependencyProperty StartPointProperty =
            DependencyProperty.RegisterAttached("StartPoint", typeof(Point), typeof(LinearGradientBrushHighContrastSetter), new PropertyMetadata(new Point(0.5, 0.0), new PropertyChangedCallback(StartPoint_Changed)));

        /// <summary>
        /// The BrushMappingMode Attached Property.
        /// </summary>
        public static readonly DependencyProperty BrushMappingModeProperty =
            DependencyProperty.RegisterAttached("BrushMappingMode", typeof(BrushMappingMode), typeof(LinearGradientBrushHighContrastSetter), new PropertyMetadata(BrushMappingMode.RelativeToBoundingBox, new PropertyChangedCallback(BrushMappingMode_Changed)));

        /// <summary>
        /// Gets the brush mapping mode.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <returns>The brush mapping mode.</returns>
        public static BrushMappingMode GetBrushMappingMode(DependencyObject obj)
        {
            return (BrushMappingMode)obj.GetValue(BrushMappingModeProperty);
        }

        /// <summary>
        /// Sets the brush mapping mode.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <param name="value">The brush mapping mode.</param>
        public static void SetBrushMappingMode(DependencyObject obj, BrushMappingMode value)
        {
            obj.SetValue(BrushMappingModeProperty, value);
        }

        /// <summary>
        /// Gets the start point.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <returns>The start point.</returns>
        public static Point GetStartPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(StartPointProperty);
        }

        /// <summary>
        /// Sets the start point.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <param name="value">The start point.</param>
        public static void SetStartPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(StartPointProperty, value);
        }

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <returns>The end point.</returns>
        public static Point GetEndPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(EndPointProperty);
        }

        /// <summary>
        /// Sets the end point.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <param name="value">The end point.</param>
        public static void SetEndPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(EndPointProperty, value);
        }

        /// <summary>
        /// Gets the spread method.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <returns>The spread method.</returns>
        public static GradientSpreadMethod GetSpreadMethod(DependencyObject obj)
        {
            return (GradientSpreadMethod)obj.GetValue(SpreadMethodProperty);
        }

        /// <summary>
        /// Sets the spread method.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <param name="value">The spread method.</param>
        public static void SetSpreadMethod(DependencyObject obj, GradientSpreadMethod value)
        {
            obj.SetValue(SpreadMethodProperty, value);
        }

        /// <summary>
        /// Gets the gradient stops.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <returns>The gradient stops.</returns>
        public static string GetGradientStops(DependencyObject obj)
        {
            return (string)obj.GetValue(GradientStopsProperty);
        }

        /// <summary>
        /// Sets the gradient stops.
        /// </summary>
        /// <param name="obj">The depedency object.</param>
        /// <param name="value">The gradient stops.</param>
        public static void SetGradientStops(DependencyObject obj, string value)
        {
            obj.SetValue(GradientStopsProperty, value);
        }

        /// <summary>
        /// Updates the brush mapping mode if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void BrushMappingMode_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                LinearGradientBrush linearGradientBrush = obj as LinearGradientBrush;
                BrushMappingMode brushMappingMode = (BrushMappingMode)args.NewValue;
                if (linearGradientBrush != null)
                {
                    linearGradientBrush.MappingMode = brushMappingMode;
                }
            }
        }

        /// <summary>
        /// Updates the start point if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void StartPoint_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                LinearGradientBrush linearGradientBrush = obj as LinearGradientBrush;
                Point point = (Point)args.NewValue;
                if (linearGradientBrush != null)
                {
                    linearGradientBrush.StartPoint = point;
                }
            }
        }

        /// <summary>
        /// Updates the end point if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void EndPoint_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                LinearGradientBrush linearGradientBrush = obj as LinearGradientBrush;
                Point point = (Point)args.NewValue;
                if (linearGradientBrush != null)
                {
                    linearGradientBrush.StartPoint = point;
                }
            }
        }

        /// <summary>
        /// Updates the spread method if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SpreadMethod_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                LinearGradientBrush linearGradientBrush = obj as LinearGradientBrush;
                GradientSpreadMethod spreadMethod = (GradientSpreadMethod)args.NewValue;
                if (linearGradientBrush != null)
                {
                    linearGradientBrush.SpreadMethod = spreadMethod;
                }
            }
        }

        /// <summary>
        /// Updates the gradient stops if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void GradientStops_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                LinearGradientBrush linearGradientBrush = obj as LinearGradientBrush;
                if (linearGradientBrush != null && args.NewValue != null && !string.IsNullOrEmpty(args.NewValue.ToString()))
                {
                    string[] gradientStops = args.NewValue.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    GradientStopCollection gradientStopCollection = new GradientStopCollection();

                    foreach (string gradientStop in gradientStops)
                    {
                        string[] parts = gradientStop.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            if (parts[0].StartsWith("#", StringComparison.OrdinalIgnoreCase) && (parts[0].Length == 7 || parts[0].Length == 9))
                            {
                                Color color = new Color();
                                int current = 1;
                                if (parts[0].Length == 9)
                                {
                                    byte a = 0x00;
                                    if (byte.TryParse(parts[0].Substring(current, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out a))
                                    {
                                        color.A = a;
                                        current += 2;

                                        byte r = 0x00;
                                        if (byte.TryParse(parts[0].Substring(current, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out r))
                                        {
                                            color.R = r;
                                            current += 2;

                                            byte g = 0x00;
                                            if (byte.TryParse(parts[0].Substring(current, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out g))
                                            {
                                                color.G = g;
                                                current += 2;

                                                byte b = 0x00;
                                                if (byte.TryParse(parts[0].Substring(current, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b))
                                                {
                                                    color.B = b;

                                                    double offset = 0;
                                                    if (double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out offset))
                                                    {
                                                        gradientStopCollection.Add(new GradientStop()
                                                        {
                                                            Color = color,
                                                            Offset = offset
                                                        });
                                                    }
                                                } 
                                            }
                                        }
                                    }
                                }   
                            }
                        }
                    }

                    linearGradientBrush.GradientStops = gradientStopCollection;
                }
            }
        }
    }
}
