//-----------------------------------------------------------------------
// <copyright file="CommonStylesResourceDictionary.cs" company="Microsoft Corporation and Crown copyright 2007 - 2009.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2009.
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
// <date>30-Oct-2009</date>
// <summary>
//      A resource dictionary containing common styles.
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
    /// A resource dictionary containing common styles.
    /// </summary>
    public partial class CommonStylesResourceDictionary : ResourceDictionary
    {
#if SILVERLIGHT
        /// <summary>
        /// Stores whether the resource dictionary content has loaded.
        /// </summary>
        private bool contentLoaded;
#endif

        /// <summary>
        /// CommonStylesResourceDictionary constructor.
        /// </summary>
        public CommonStylesResourceDictionary()    
        {        
            this.InitializeComponent();    
        }

#if SILVERLIGHT
        /// <summary>
        /// Initializes the content.
        /// </summary>
        public void InitializeComponent()
        {
            if (this.contentLoaded)
            {
                return;
            }

            this.contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Microsoft.Cui.Controls;component/ResourceDictionaries/CommonStylesResourceDictionary.xaml", System.UriKind.Relative));
        }
#endif
    }
}
