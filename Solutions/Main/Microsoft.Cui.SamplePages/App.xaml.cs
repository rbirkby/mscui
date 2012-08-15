//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>11-Feb-2008</date>
// <summary>The sample Silverlight Application. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Implementation of the App class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.Startup += this.OnStartup;
            this.Exit += this.OnExit;
            InitializeComponent();
        }

        /// <summary>
        /// Called when [startup].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Load the main control
            string patientId = e.InitParams.ContainsKey("PatientId") == true ? e.InitParams["PatientId"] : "1";
            string dataFilePath = e.InitParams.ContainsKey("DataFilePath") == true ? e.InitParams["DataFilePath"] : string.Empty;
            string startPage = e.InitParams.ContainsKey("StartPage") == true ? e.InitParams["StartPage"] : string.Empty;
            string templateSet = e.InitParams.ContainsKey("TemplateSet") == true ? e.InitParams["TemplateSet"] : "Standard";
            bool allowResize = e.InitParams.ContainsKey("AllowResize") == true ? bool.Parse(e.InitParams["AllowResize"]) : true;

            this.ApplyTemplateSet(templateSet);
            
            this.SetRootVisual(startPage);
            this.SetProperty("AllowResize", allowResize);
            this.SetProperty("PatientId", patientId);

            if (!string.IsNullOrEmpty(dataFilePath))
            {
                this.SetProperty("DataFilePath", dataFilePath);
            }
        }

        /// <summary>
        /// Sets the root visual.
        /// </summary>
        /// <param name="type">The type that needs to be set as root page.</param>
        private void SetRootVisual(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                Type typeToCreate = null;
                string rootNamespace = this.GetType().Namespace + ".";

                typeToCreate = Type.GetType(rootNamespace + type + "Page");
                if (typeToCreate == null)
                {
                    typeToCreate = Type.GetType(rootNamespace + type);
                }

                if (typeToCreate != null)
                {
                    this.RootVisual = Activator.CreateInstance(typeToCreate) as UIElement;
                }                
            }
            else
            {
                this.RootVisual = new DefaultPage();   
            }
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value of the property.</param>
        private void SetProperty(string propertyName, object value)
        {
            if (value != null)
            {
                PropertyInfo propInfo = this.RootVisual.GetType().GetProperty(propertyName);
                if (propInfo != null)
                {
                    propInfo.SetValue(this.RootVisual, value, null);
                }
            }
        }

        /// <summary>
        /// Applies the template set.
        /// </summary>
        /// <param name="templateSet">The template set.</param>
        private void ApplyTemplateSet(string templateSet)
        {
            if (string.Compare(templateSet, "Alternative", StringComparison.OrdinalIgnoreCase) == 0)
            {
                DataTemplate foundTemplate;
                foundTemplate = (DataTemplate)this.Resources["ALT_BasicBoldTemplate"];
                this.Resources.Add("BasicBoldTemplate", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["ALT_BasicTemplate"];
                this.Resources.Add("BasicTemplate", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["ALT_NameStrengthFrequency"];
                this.Resources.Add("NameStrengthFrequency", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["ALT_NameFormFrequency"];
                this.Resources.Add("NameFormFrequency", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["ALT_NameStrengthMedication"];
                this.Resources.Add("NameStrengthMedication", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["ALT_DrugDetailsTemplateDefault"];
                this.Resources.Add("DrugDetailsTemplateDefault", foundTemplate);
            }
            else
            {
                DataTemplate foundTemplate;
                foundTemplate = (DataTemplate)this.Resources["STD_BasicBoldTemplate"];
                this.Resources.Add("BasicBoldTemplate", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["STD_BasicTemplate"];
                this.Resources.Add("BasicTemplate", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["STD_NameStrengthFrequency"];
                this.Resources.Add("NameStrengthFrequency", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["STD_NameFormFrequency"];
                this.Resources.Add("NameFormFrequency", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["STD_NameStrengthMedication"];
                this.Resources.Add("NameStrengthMedication", foundTemplate);
                foundTemplate = (DataTemplate)this.Resources["STD_DrugDetailsTemplateDefault"];
                this.Resources.Add("DrugDetailsTemplateDefault", foundTemplate);
            }
        }

        /// <summary>
        /// Called when [exit].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnExit(object sender, EventArgs e)
        {
        }
    }
}
