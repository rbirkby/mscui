//-----------------------------------------------------------------------
// <copyright file="CfhRuleOutput.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>10-Feb-2008</date>
// <summary> Class representing output data for Cfh rules </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Data
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
    /// Rule output class.
    /// </summary>
    public struct CfhRuleOutput
    {
        /// <summary>
        /// Level name for the template.
        /// </summary>
        private string levelName;

        /// <summary>
        /// Template name to be added.
        /// </summary>
        private string templateName;

        /// <summary>
        /// Gets or sets the level name for the template.
        /// </summary>
        /// <value>The name of the level.</value>
        public string LevelName
        {
            get
            {
                return this.levelName;
            }

            set
            {
                this.levelName = value;
            }
        }

        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }
    }
}