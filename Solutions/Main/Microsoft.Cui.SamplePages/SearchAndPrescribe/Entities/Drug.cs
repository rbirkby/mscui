//-----------------------------------------------------------------------
// <copyright file="Drug.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class representing a drug.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// A class representing a drug.
    /// </summary>
    public class Drug : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores if the drug is commonly prescribed.
        /// </summary>
        private bool commonlyPrescribed;

        /// <summary>
        /// Stores the branded drugs.
        /// </summary>
        private Drug[] brandedDrugs;

        /// <summary>
        /// Stores the drug name.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// Stores the drug brand name.
        /// </summary>
        private string brandName;

        /// <summary>
        /// Stores the description.
        /// </summary>
        private string description;

        /// <summary>
        /// Stores the ingredients.
        /// </summary>
        private ElementValue[] ingredients;

        /// <summary>
        /// Stores the brands.
        /// </summary>
        private DrugElement[] brands;

        /// <summary>
        /// Stores the routes.
        /// </summary>
        private Route[] routes;

        /// <summary>
        /// Stores the strengths.
        /// </summary>
        private Strength[] strengths;

        /// <summary>
        /// Stores the forms.
        /// </summary>
        private Form[] forms;
        
        /// <summary>
        /// Stores the dosages.
        /// </summary>
        private Dose[] dosages;

        /// <summary>
        /// Stores the frequencies.
        /// </summary>
        private Frequency[] frequencies;

        /// <summary>
        /// Stores the as required reasons.
        /// </summary>
        private DrugElement[] asRequiredReasons;

        /// <summary>
        /// Stores if this drug is being used to precribe generically.
        /// </summary>
        private bool isBrandGeneric;

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the drug is commonly prescribed.
        /// </summary>
        public bool CommonlyPrescribed
        {
            get 
            { 
                return this.commonlyPrescribed; 
            }

            set 
            {
                this.commonlyPrescribed = value;
                this.RaisePropertyChanged("CommonlyPrescribed");
            }
        }

        /// <summary>
        /// Gets or sets the branded drugs.
        /// </summary>
        public Drug[] BrandedDrugs
        {
            get { return this.brandedDrugs; }
            set { this.brandedDrugs = value; }
        }

        /// <summary>
        /// Gets or sets the drug brand name.
        /// </summary>
        public string BrandName
        {
            get 
            { 
                return this.brandName; 
            }

            set 
            {
                this.brandName = value;
                this.RaisePropertyChanged("BrandName");
                this.RaisePropertyChanged("DisplayBrandName");
            }
        }

        /// <summary>
        /// Gets or sets the drug name.
        /// </summary>
        public string Name 
        {
            get
            {
                if (!string.IsNullOrEmpty(this.name))
                {
                    return this.name;
                }
                else if (this.Ingredients != null)
                {
                    StringBuilder ingredientsString = new StringBuilder();

                    if (this.Ingredients != null && this.Ingredients.Length > 0)
                    {
                        for (int i = 0; i < this.Ingredients.Length - 1; i++)
                        {
                            ingredientsString.Append(this.Ingredients[i].Value + " + ");
                        }

                        ingredientsString.Append(this.Ingredients[this.Ingredients.Length - 1].Value);
                    }

                    return ingredientsString.ToString();
                }

                return null;
            }

            set
            {
                this.name = value;
                this.RaisePropertyChanged("Name");
                this.RaisePropertyChanged("NameWithDash");
            }
        }

        /// <summary>
        /// Gets the name with a dash, if there is a brand name.
        /// </summary>
        public string NameWithDash
        {
            get
            {
                return this.Name + ((!string.IsNullOrEmpty(this.BrandName) && !string.IsNullOrEmpty(this.Name)) ? " ― " : string.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get 
            {
                return this.description; 
            }
            
            set 
            {
                this.description = value;
                this.RaisePropertyChanged("Description");
            }
        }

        /// <summary>
        /// Gets or sets the ingredients.
        /// </summary>
        public ElementValue[] Ingredients
        {
            get 
            { 
                return this.ingredients; 
            }

            set 
            {
                this.ingredients = value;
                this.RaisePropertyChanged("Name");
                this.RaisePropertyChanged("Ingredients");
            }
        }

        /// <summary>
        /// Gets or sets the template prescriptions.
        /// </summary>
        public TemplatePrescription[] TemplatePrescriptions 
        {
            get; set; 
        }

        /// <summary>
        /// Gets the display ingredients as a single string.
        /// </summary>
        public string DisplayIngredientsString
        {
            get
            {
                StringBuilder ingredientsString = new StringBuilder();

                if (this.Ingredients != null && this.Ingredients.Length > 0)
                {
                    for (int i = 0; i < this.Ingredients.Length - 1; i++)
                    {
                        ingredientsString.Append(this.Ingredients[i].Value + " + ");
                    }

                    ingredientsString.Append(this.Ingredients[this.Ingredients.Length - 1].Value);
                }

                if (ingredientsString.ToString() != this.Name)
                {
                    return ingredientsString.ToString();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the brands.
        /// </summary>
        public DrugElement[] Brands
        {
            get
            {
                return this.brands;
            }

            set
            {
                this.brands = value;
            }
        }

        /// <summary>
        /// Gets or sets the routes.
        /// </summary>
        public Route[] Routes
        {
            get 
            {
                List<Route> genericRoutes = new List<Route>();
                if (this.routes != null)
                {
                    foreach (Route route in this.routes)
                    {
                        if (string.IsNullOrEmpty(route.BrandName))
                        {
                            genericRoutes.Add(route);
                        }
                    }
                }

                return (genericRoutes.Count > 0) ? genericRoutes.ToArray() : null;
            }

            set
            {
                this.routes = value;
            }
        }

        /// <summary>
        /// Gets the branded routes.
        /// </summary>
        public Route[] BrandedRoutes
        {
            get
            {
                List<Route> brandedRoutes = new List<Route>();
                if (this.routes != null)
                {
                    foreach (Route route in this.routes)
                    {
                        if (!string.IsNullOrEmpty(route.BrandName))
                        {
                            brandedRoutes.Add(route);
                        }
                    }
                }

                return (brandedRoutes.Count > 0) ? brandedRoutes.ToArray() : null;
            }
        }

        /// <summary>
        /// Gets all the other routes.
        /// </summary>
        public Route[] AllOtherRoutes
        {
            get
            {
                List<Route> allOtherRoutes = new List<Route>();
                foreach (Route otherRoute in DrugDataHelper.AllRoutes)
                {
                    if (!this.ContainsRoute(otherRoute))
                    {
                        allOtherRoutes.Add(otherRoute);
                    }
                }

                return allOtherRoutes.ToArray();
            }
        }

        /// <summary>
        /// Gets or sets all the frequencies.
        /// </summary>
        public Frequency[] AllFrequencies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the strengths.
        /// </summary>
        public Strength[] Strengths
        {
            get
            {
                return this.strengths;
            }

            set
            {
                this.strengths = value;
                this.RaisePropertyChanged("Strengths");
            }
        }

        /// <summary>
        /// Gets or sets the forms.
        /// </summary>
        public Form[] Forms
        {
            get
            { 
                return this.forms; 
            }

            set
            {
                this.forms = value;
                this.RaisePropertyChanged("Forms");
            }
        }

        /// <summary>
        /// Gets or sets the dosages.
        /// </summary>
        public Dose[] Dosages
        {
            get 
            {
                return this.dosages; 
            }

            set
            {
                this.dosages = value;
                this.RaisePropertyChanged("Dosages");
            }
        }

        /// <summary>
        /// Gets or sets the frequencies.
        /// </summary>
        public Frequency[] Frequencies
        {
            get 
            { 
                return this.frequencies; 
            }

            set
            {
                this.frequencies = value;
                this.RaisePropertyChanged("Frequencies");
            }
        }

        /// <summary>
        /// Gets or sets all the administration times.
        /// </summary>
        public AdministrationTimes[] AdministrationTimes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the as required reasons.
        /// </summary>
        public DrugElement[] AsRequiredReasons
        {
            get 
            { 
                return this.asRequiredReasons; 
            }

            set
            {
                this.asRequiredReasons = value;
                this.RaisePropertyChanged("AsRequiredReasons");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this drug is being used to prescribe generically.
        /// </summary>
        public bool IsBrandGeneric
        {
            get
            {
                return this.isBrandGeneric;
            }

            set
            {
                this.isBrandGeneric = value;
                this.RaisePropertyChanged("IsBrandGeneric");
            }
        }

        /// <summary>
        /// Gets the display ingredients, internal.
        /// </summary>
        private string[] DisplayIngredientsInternal
        {
            get
            {
                if (this.Ingredients != null && this.Ingredients.Length > 0)
                {
                    List<string> ingredients = new List<string>();
                    for (int i = 0; i < this.Ingredients.Length - 1; i++)
                    {
                        ingredients.Add(this.Ingredients[i].Value.ToLower(CultureInfo.CurrentCulture).Replace("(product)", string.Empty) + " + ");
                    }

                    ingredients.Add(this.Ingredients[this.Ingredients.Length - 1].Value.ToLower(CultureInfo.CurrentCulture).Replace("(product)", string.Empty));
                    return ingredients.ToArray();
                }

                return null;
            }
        }

        /// <summary>
        /// Checks if the drug contains a route.
        /// </summary>
        /// <param name="route">The route to check.</param>
        /// <returns>Whether the drug contains the route.</returns>
        public bool ContainsRoute(ElementValue route)
        {
            if (!string.IsNullOrEmpty(route.Value) && this.Routes != null)
            {
                string routeValue = route.Value.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                foreach (ElementValue validRoute in this.Routes)
                {
                    if (!string.IsNullOrEmpty(validRoute.Value))
                    {
                        string validRouteValue = validRoute.Value.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        if (routeValue == validRouteValue)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Override for ToString().
        /// </summary>
        /// <returns>The drug name.</returns>
        public override string ToString()
        {
            string stringValue = string.Empty;

            if (!string.IsNullOrEmpty(this.Name))
            {
                stringValue += this.Name;
            }

            if (!string.IsNullOrEmpty(this.BrandName))
            {
                stringValue += " ― " + this.BrandName;
            }

            if (!string.IsNullOrEmpty(this.Description))
            {
                stringValue += " ― " + this.Description;
            }

            return stringValue;
        }
       
        #region INotifyPropertyChanged Members
        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
