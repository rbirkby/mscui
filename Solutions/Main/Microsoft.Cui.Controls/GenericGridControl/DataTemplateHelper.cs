//-----------------------------------------------------------------------
// <copyright file="DataTemplateHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Helper class to load data template.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Helper class to load data template content.
    /// </summary>
    public static class DataTemplateHelper
    {
        /// <summary>
        /// Loads content in the specified template.
        /// </summary>
        /// <param name="template">Template to load.</param>
        /// <returns>Contents loaded from the specified template.</returns>
        public static DependencyObject LoadContent(DataTemplate template)
        {
            // This is a work around for a Beta1 Bug
            // Where under load the LoadContent can fail.
            // This stops us seeing this issue as frequently.
            // The idea is to try upto 5 times to load a template.
            // If it still does not load then rethrow the exception.
            int retryCount = 5;
            DependencyObject dependencyObject = null;
            if (template != null)
            {
                while (null == dependencyObject && 0 != retryCount)
                {
                    try
                    {
                        dependencyObject = template.LoadContent();
                    }
                    catch (System.Windows.Markup.XamlParseException e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                        System.Diagnostics.Debug.WriteLine("\n Template Load Error");
                        retryCount--;
                        System.Threading.Thread.SpinWait(2000);
                        if (retryCount == 0)
                        {
                            throw;
                        }
                    }
                }
            }

            return dependencyObject;
        }
    }
}
