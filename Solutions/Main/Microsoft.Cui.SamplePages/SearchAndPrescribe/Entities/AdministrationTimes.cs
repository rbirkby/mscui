//-----------------------------------------------------------------------
// <copyright file="AdministrationTimes.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Jul-2009</date>
// <summary>
//      A class representing a set of prescription administration times.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// A class representing a set of prescription administration times.
    /// </summary>
    public class AdministrationTimes : DrugElement
    {
        /// <summary>
        /// Stores the first dose time.
        /// </summary>
        private DateTime? firstDose;

        /// <summary>
        /// Stores the display administration times.
        /// </summary>
        private TextBlock[] displayAdministrationTimes;

        /// <summary>
        /// Initializes a new instance of the AdministrationTimes class.
        /// </summary>
        public AdministrationTimes()
        {
        }

        /// <summary>
        /// Initializes a new instance of the AdministrationTimes class with a drug attribute to copy.
        /// </summary>
        /// <param name="drugAttribute">The drug attribute to copy.</param>
        public AdministrationTimes(DrugElement drugAttribute)
        {
            this.Copy(drugAttribute);
        }

        /// <summary>
        /// Gets or sets the times.
        /// </summary>
        public DateTime?[] Times 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets the display administration times.
        /// </summary>
        public TextBlock[] DisplayAdministrationTimes
        {
            get
            {
                if (this.Times == null)
                {
                    return null;
                }

                this.UpdateDisplayAdministrationTimes(this.firstDose, false);
                return this.displayAdministrationTimes;
            }
        }

        /// <summary>
        /// Updates the display administration times.
        /// </summary>
        /// <param name="raisePropertyChanged">Whether the property changed event should be raised.</param>
        public void UpdateDisplayAdministrationTimes(bool raisePropertyChanged)
        {
            this.UpdateDisplayAdministrationTimes(this.firstDose, raisePropertyChanged);
        }

        /// <summary>
        /// Updates the display administration times with a new first dose.
        /// </summary>
        /// <param name="firstDose">The first dose.</param>
        /// <param name="raisePropertyChanged">Whether the property changed event should be raised.</param>
        public void UpdateDisplayAdministrationTimes(DateTime? firstDose, bool raisePropertyChanged)
        {
            this.firstDose = firstDose;
            List<TextBlock> textBlocks = new List<TextBlock>();

            if (this.Times == null)
            {
                return;
            }

            for (int i = 0; i < this.Times.Length; i++)
            {
                if (this.Times[i].HasValue)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = this.Times[i].Value.ToString("HH:mm", CultureInfo.CurrentCulture);

                    if (i < this.Times.Length - 1)
                    {
                        textBlock.Text += "; ";
                    }

                    if (this.firstDose.HasValue && this.firstDose.Value.Hour == this.Times[i].Value.Hour && this.firstDose.Value.Minute == this.Times[i].Value.Minute)
                    {                      
                        textBlock.FontWeight = FontWeights.Bold;
                    }
                    else if (!this.firstDose.HasValue && this.Times.Length == 1)
                    {
                        textBlock.FontWeight = FontWeights.Bold;
                    }

                    textBlocks.Add(textBlock);
                }
            }

            this.displayAdministrationTimes = textBlocks.ToArray();

            if (raisePropertyChanged)
            {
                this.RaisePropertyChanged("DisplayAdministrationTimes");
            }
        }

        /// <summary>
        /// Override for get hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override for equals.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>Whether the items are equal.</returns>
        public override bool Equals(object obj)
        {
            AdministrationTimes administrationTimes = obj as AdministrationTimes;
            if (administrationTimes != null)
            {
                if (administrationTimes.Times != null && this.Times != null && administrationTimes.Times.Length == this.Times.Length)
                {
                    for (int i = 0; i < this.Times.Length; i++)
                    {
                        if (this.Times[i] != administrationTimes.Times[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return base.Equals(obj);
        }
    }
}
