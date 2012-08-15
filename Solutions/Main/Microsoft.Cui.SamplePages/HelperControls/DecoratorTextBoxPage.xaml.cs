//-----------------------------------------------------------------------
// <copyright file="DecoratorTextBoxPage.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>13-Aug-2009</date>
// <summary>Sample page to host the decorator textbox control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Sample page to host the decorator textbox control.
    /// </summary>
    public partial class DecoratorTextBoxPage : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the DecoratorTextBoxPage class.
        /// </summary>
        public DecoratorTextBoxPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Highlights words in the exmaple DecoratortextBox.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The key event arguments</param>
        private void ExampleDecoratorTextBox_EnterPressed(object sender, KeyEventArgs e)
        {
            List<HighlightSection> terms = new List<HighlightSection>();
            string[] words = this.ExampleDecoratorTextBox.Text.Split(' ');
            int wordTicker = 0;
            int charPosition = 0;
            for (int i = 1; i <= words.Length; i++)
            {
                wordTicker = wordTicker >= 4 == true ? 1 : wordTicker + 1;

                HighlightSection t;

                if (wordTicker == 1)
                {
                    t = new HighlightSection(charPosition, true, words[i - 1].Length);
                    terms.Add(t);
                }
                else if (wordTicker == 3)
                {
                    t = new HighlightSection(charPosition, false, words[i - 1].Length);
                    terms.Add(t);
                }

                charPosition = charPosition + words[i - 1].Length + 1;
            }

            this.ExampleDecoratorTextBox.MatchingTermItemsSource = terms;
        }
    }
}
