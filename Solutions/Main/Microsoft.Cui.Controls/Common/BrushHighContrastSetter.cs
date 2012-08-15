//-----------------------------------------------------------------------
// <copyright file="BrushHighContrastSetter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Adds attached properties to a brush for specifying values for high contrast mode.</summary>
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
    /// Adds attached properties to a brush for specifying values for high contrast mode.
    /// </summary>
    public class BrushHighContrastSetter
    {
        /// <summary>
        /// The Transform Attached Property.
        /// </summary>
        public static readonly DependencyProperty TransformProperty =
            DependencyProperty.RegisterAttached("Transform", typeof(Transform), typeof(BrushHighContrastSetter), new PropertyMetadata(null, new PropertyChangedCallback(Transform_Changed)));

        /// <summary>
        /// The RelativeTransform Attached Property.
        /// </summary>
        public static readonly DependencyProperty RelativeTransformProperty =
            DependencyProperty.RegisterAttached("RelativeTransform", typeof(Transform), typeof(BrushHighContrastSetter), new PropertyMetadata(null, new PropertyChangedCallback(RelativeTransform_Changed)));
 
        /// <summary>
        /// The Opacity Attached Property.
        /// </summary>
        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.RegisterAttached("Opacity", typeof(double), typeof(BrushHighContrastSetter), new PropertyMetadata(1.0, new PropertyChangedCallback(Opacity_Changed)));

        /// <summary>
        /// Gets the opacity.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The opacity.</returns>
        public static double GetOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(OpacityProperty);
        }

        /// <summary>
        /// Sets the opacity.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The opacity.</param>
        public static void SetOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(OpacityProperty, value);
        }

        /// <summary>
        /// Gets the relative transform.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The relative transform.</returns>
        public static Transform GetRelativeTransform(DependencyObject obj)
        {
            return (Transform)obj.GetValue(RelativeTransformProperty);
        }

        /// <summary>
        /// Sets the relative transform.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The relative transform.</param>
        public static void SetRelativeTransform(DependencyObject obj, Transform value)
        {
            obj.SetValue(RelativeTransformProperty, value);
        }

        /// <summary>
        /// Gets the transform.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The transform.</returns>
        public static Transform GetTransform(DependencyObject obj)
        {
            return (Transform)obj.GetValue(TransformProperty);
        }

        /// <summary>
        /// Sets the transform.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The transform.</param>
        public static void SetTransform(DependencyObject obj, Transform value)
        {
            obj.SetValue(TransformProperty, value);
        }

        /// <summary>
        /// Updates the opacity if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void Opacity_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                Brush brush = obj as Brush;
                double opacity = brush.Opacity;
                if (double.TryParse(args.NewValue.ToString(), out opacity) && brush != null)
                {
                    brush.Opacity = opacity;
                }
            }
        }

        /// <summary>
        /// Updates the relative transform if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void RelativeTransform_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                Brush brush = obj as Brush;
                Transform transform = args.NewValue as Transform;
                if (brush != null && transform != null)
                {
                    brush.RelativeTransform = transform;
                }
            }
        }

        /// <summary>
        /// Updates the tramsform if in high contrast.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void Transform_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (SystemParameters.HighContrast)
            {
                Brush brush = obj as Brush;
                Transform transform = args.NewValue as Transform;
                if (brush != null && transform != null)
                {
                    brush.Transform = transform;
                }
            }
        }
    }
}
