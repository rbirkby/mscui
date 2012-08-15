//-----------------------------------------------------------------------
// <copyright file="RoadmapHost.cs" company="Microsoft Corporation copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation copyright 2007 - 2010.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>23-Jan-2009</date>
// <summary>RoapMap class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Roadmap
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
    /// RoadMap class.
    /// </summary>
    public class RoadmapHost : Control
    {
        /// <summary>
        /// Roadmap class constructor.
        /// </summary>
        public RoadmapHost()
        {
            this.DefaultStyleKey = typeof(RoadmapHost);
        }

        /// <summary>
        /// Gets or sets the items control.
        /// </summary>
        /// <value>The items control.</value>
        internal ItemsControl ItemsControl
        {
            get;
            set;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ItemsControl = this.GetTemplateChild<ItemsControl>("ItemsControl", true);      
        }

        /// <summary>
        /// Gets the template child.
        /// </summary>
        /// <typeparam name="T">The type that the template item returns.</typeparam>
        /// <param name="name">The name of the item.</param>
        /// <param name="mustHave">If set to <c>true</c> [must have].</param>
        /// <returns>The template.</returns>
        private T GetTemplateChild<T>(string name, bool mustHave) where T : class 
        {
            DependencyObject obj = this.GetTemplateChild(name);
            T returnValue;
            returnValue = obj as T;
            if (null == obj && true == mustHave)
            {
                throw new ArgumentNullException(name, "Invalid Type");
            }

            return returnValue;
        }
    }
}
