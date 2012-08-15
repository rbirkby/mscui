//-----------------------------------------------------------------------
// <copyright file="FocusHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>04-Aug-2009</date>
// <summary>
//      Handles moving control focus on next application frame.
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
    using System.Collections.Generic;

    /// <summary>
    /// Handles moving control focus on next application frame.
    /// </summary>
    public static class FocusHelper
    {
        /// <summary>
        /// Stores the next control to focus.
        /// </summary>
        private static Control focusControl;

        /// <summary>
        /// Stores if the rendering event has been hooked up.
        /// </summary>
        private static bool renderingRegistered;

        /// <summary>
        /// Focus a control.
        /// </summary>
        /// <param name="control">The control to focus.</param>
        public static void FocusControl(Control control)
        {
            if (control != null)
            {
                focusControl = control;

                if (!renderingRegistered)
                {
                    renderingRegistered = true;
                    CompositionTarget.Rendering -= CompositionTarget_Rendering;
                    CompositionTarget.Rendering += CompositionTarget_Rendering;
                }
            }
        }

        /// <summary>
        /// Focuses the next control.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private static void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (focusControl != null)
            {
                focusControl.Focus();
                focusControl = null;
            }

            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            renderingRegistered = false;
        }
    }
}
