//-----------------------------------------------------------------------
// <copyright file="Prescription.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
    using System.Linq;

    /// <summary>
    /// A class representing a drug.
    /// </summary>
    public class Prescription : INotifyPropertyChanged
    {      
        /// <summary>
        /// Stores if attributes should be validated.
        /// </summary>
        private bool validateAttributes = true;

        /// <summary>
        /// Stores if the template presctription is being updated.
        /// </summary>
        private bool updatingTemplatePrescription;

        /// <summary>
        /// Stores of this prescription in in detailed view.
        /// </summary>
        private bool isDetailedView;

        /// <summary>
        /// Stores if the prescription is authorizable.
        /// </summary>
        private bool isAuthorizable;

        /// <summary>
        /// Stores if the prescription is unlicensed.
        /// </summary>
        private bool isUnlicensed;

        /// <summary>
        /// Stores the drug.
        /// </summary>
        private Drug drug;

        /// <summary>
        /// Stores the route.
        /// </summary>
        private Route route;

        /// <summary>
        /// Stores the brand.
        /// </summary>
        private DrugElement brand;

        /// <summary>
        /// Stores the template prescription.
        /// </summary>
        private TemplatePrescription templatePrescription;

        /// <summary>
        /// Stores the strength.
        /// </summary>
        private Strength strength;

        /// <summary>
        /// Stores the form.
        /// </summary>
        private Form form;

        /// <summary>
        /// Stores the dose.
        /// </summary>
        private Dose dose;

        /// <summary>
        /// Stores the custom dose item.
        /// </summary>
        private Dose customDose = new Dose() { IsCustomValue = true };

        /// <summary>
        /// Stores the method.
        /// </summary>
        private string method;

        /// <summary>
        /// Stores the site.
        /// </summary>
        private DrugElement site;

        /// <summary>
        /// Stores the unlicensed reason.
        /// </summary>
        private string unlicensedReason;

        /// <summary>
        /// Stores the frequency.
        /// </summary>
        private Frequency frequency;

        /// <summary>
        /// Stores the administration times.
        /// </summary>
        private AdministrationTimes administrationTimes;

        /// <summary>
        /// Stores the as required reason.
        /// </summary>
        private DrugElement asRequiredReason;

        /// <summary>
        /// Stores if the prescription is as required.
        /// </summary>
        private bool asRequired;

        /// <summary>
        /// Stores the duration.
        /// </summary>
        private Duration duration;

        /// <summary>
        /// Stores the reason.
        /// </summary>
        private DrugElement reason;

        /// <summary>
        /// Stores the start date.
        /// </summary>
        private DateTime? startDate;

        /// <summary>
        /// Stores the valid durations.
        /// </summary>
        private Duration[] validDurations;

        /// <summary>
        /// Stores if the start date is being updated.
        /// </summary>
        private bool updatingStartDate;

        /// <summary>
        /// Stores the first dose time.
        /// </summary>
        private DateTime? firstDoseTime = new DateTime?(DateTime.Now);

        /// <summary>
        /// Stores the attributes that are currently being validated.
        /// </summary>
        private List<string> attributeQueue = new List<string>();

        /// <summary>
        /// Stores the property changed queue.
        /// </summary>
        private Dictionary<string, int> propertyChangedQueue = new Dictionary<string, int>();

        /// <summary>
        /// Stores if a frequency has been selected.
        /// </summary>
        private bool frequencySelected;

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the attributes should be validated.
        /// </summary>
        public bool ValidateAttributes
        {
            get { return this.validateAttributes; }
            set { this.validateAttributes = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is in detailed view.
        /// </summary>
        public bool IsDetailedView
        {
            get 
            { 
                return this.isDetailedView; 
            }

            set 
            { 
                this.isDetailedView = value;
                this.RaisePropertyChanged("IsDetailedView");

                if (this.isDetailedView && this.AsRequired && this.Frequency != null)
                {
                    this.Frequency = GetElementValueWithDisplayValue(this.ValidFrequencies, this.Frequency.Value) as Frequency;
                }
                else if (!this.isDetailedView && this.asRequired && this.Frequency != null)
                {
                    this.Frequency = GetElementValueWithDisplayValue(this.ValidFrequencies, this.Frequency.Value + " - as required") as Frequency;
                }

                this.RaisePropertyChanged("ValidFrequencies");
                this.RaisePropertyChanged("AllFrequencies");

                if (this.Dose != null && this.Dose.IsCustomValue && this.Dose.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length < 2)
                {
                    this.Dose = null;
                }
                
                if (this.Frequency != null && this.Frequency.IsCustomValue && this.Frequency.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length < 3)
                {
                    this.Frequency = null;
                }

                if (this.Duration != null && string.IsNullOrEmpty(this.Duration.DisplayValue))
                {
                    this.Duration = null;
                }

                this.RaisePropertyChanged("ValidDurations");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is authorizable.
        /// </summary>
        public bool IsAuthorizable
        {
            get 
            { 
                return this.isAuthorizable; 
            }

            set 
            {
                this.isAuthorizable = value;
                this.RaisePropertyChanged("IsAuthorizable");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is unlicensed.
        /// </summary>
        public bool IsUnlicensed
        {
            get 
            { 
                return this.isUnlicensed; 
            }

            set 
            { 
                this.isUnlicensed = value;
                this.RaisePropertyChanged("IsUnlicensed");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Gets or sets the unlicensed reason.
        /// </summary>
        public string UnlicensedReason
        {
            get 
            { 
                return this.unlicensedReason; 
            }
            
            set 
            { 
                this.unlicensedReason = value;
                this.RaisePropertyChanged("UnlicensedReason");
                this.UpdateIsAuthorizable();
            }
        }
      
        /// <summary>
        /// Gets or sets the drug.
        /// </summary>
        public Drug Drug
        {
            get 
            { 
                return this.drug; 
            }

            set 
            {
                bool changed = this.drug != value;
                this.drug = value;

                if (changed)
                {
                    this.frequencySelected = false;                    
                    this.AsRequired = false;
                    this.RaisePropertyChanged("Routes");
                    this.RaisePropertyChanged("BrandedRoutes");
                    this.RaisePropertyChanged("TemplatePrescriptions");
                    this.RaisePropertyChanged("ValidBrands");
                    this.RaisePropertyChanged("ValidStrengths");
                    this.RaisePropertyChanged("OtherStrengths");
                    this.RaisePropertyChanged("RouteForms");
                    this.RaisePropertyChanged("RouteForms");
                    this.RaisePropertyChanged("ValidForms");
                    this.RaisePropertyChanged("OtherForms");
                    this.RaisePropertyChanged("ValidDosages");
                    this.RaisePropertyChanged("AllDosages");
                    this.RaisePropertyChanged("ValidDoseUnit");
                    this.UpdateCustomDoseValidUnit();
                    this.CustomDose.Value = string.Empty;
                    this.RaisePropertyChanged("AsRequiredReasons");
                    this.RaisePropertyChanged("Drug");
                    this.RaisePropertyChanged("BrandedDrug");
                    this.RaisePropertyChanged("PreviewPrescription");
                    this.StartDate = null;
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        public Route Route
        {
            get 
            { 
                return this.route; 
            }

            set 
            {
                bool changed = this.route != value;
                this.route = value;

                if (changed)
                {
                    if (this.route != null && this.Drug.AllOtherRoutes.Contains(this.route))
                    {
                        this.IsUnlicensed = true;
                    }

                    // Clears out previously selected values.
                    this.frequencySelected = false;
                    this.Strength = null;
                    this.Form = null;
                    this.Dose = null;
                    this.Site = null;
                    this.Method = string.Empty;
                    this.Frequency = null;
                    this.AsRequired = false;
                    this.TemplatePrescription = null;
                    this.CustomDose.Value = string.Empty;
                    this.StartDate = null;
                    this.Duration = null;

                    if (this.Drug != null && this.route != null && this.route.IsABrandGeneric && string.IsNullOrEmpty(this.route.BrandName))
                    {
                        this.Drug.IsBrandGeneric = true;
                    }
                    else if (this.Drug != null)
                    {
                        this.Drug.IsBrandGeneric = false;
                    }

                    if (this.Form == null)
                    {
                        this.RaisePropertyChanged("MandatoryForm");
                    }

                    if (this.Brand == null)
                    {
                        this.RaisePropertyChanged("MandatoryBrand");
                    }

                    if (this.Strength == null)
                    {
                        this.RaisePropertyChanged("MandatoryStrength");
                    }

                    if (this.Site == null)
                    {
                        this.RaisePropertyChanged("MandatorySite");
                    }

                    this.RaisePropertyChanged("Route");
                    this.RaisePropertyChanged("TemplatePrescriptions");
                    this.RaisePropertyChanged("ValidBrands");
                    this.RaisePropertyChanged("ValidStrengths");
                    this.RaisePropertyChanged("OtherStrengths");
                    this.RaisePropertyChanged("RouteForms");
                    this.RaisePropertyChanged("ValidForms");
                    this.RaisePropertyChanged("OtherForms");
                    this.RaisePropertyChanged("ValidDosages");
                    this.RaisePropertyChanged("AllDosages");
                    this.RaisePropertyChanged("ValidDoseUnit");
                    this.UpdateCustomDoseValidUnit();
                    this.RaisePropertyChanged("ValidFrequencies");
                    this.RaisePropertyChanged("AllFrequencies");
                    this.RaisePropertyChanged("OtherFrequencies");
                    this.RaisePropertyChanged("ValidDurations");                    
                    this.RaisePropertyChanged("AsRequiredReasons");
                    this.ValidateSelections("Route");
                    this.RaisePropertyChanged("PreviewPrescription");
                    this.StartDate = null;
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets or sets the prescription brand.
        /// </summary>
        public DrugElement Brand
        {
            get
            {
                return this.brand;
            }

            set
            {
                if (this.brand != value)
                {
                    this.brand = value;

                    if (!this.updatingTemplatePrescription && this.TemplatePrescription != null && this.TemplatePrescription.BrandName != null && (this.Brand == null || this.TemplatePrescription.BrandName != this.Brand.Value))
                    {
                        this.TemplatePrescription = null;
                    }                    

                    // Clears out previously selected values.
                    if (!this.updatingTemplatePrescription)
                    {
                        this.frequencySelected = false;
                        this.Strength = null;
                        this.Form = null;
                        this.Dose = null;
                        this.Site = null;
                        this.Method = string.Empty;
                        this.Frequency = null;
                        this.AsRequired = false;
                        this.TemplatePrescription = null;
                        this.CustomDose.Value = string.Empty;
                        this.StartDate = null;
                        this.Duration = null;
                    }

                    this.RaisePropertyChanged("Brand");
                    this.RaisePropertyChanged("ValidStrengths");
                    this.RaisePropertyChanged("OtherStrengths");
                    this.RaisePropertyChanged("RouteForms");
                    this.RaisePropertyChanged("ValidForms");
                    this.RaisePropertyChanged("OtherForms");
                    this.RaisePropertyChanged("ValidDosages");
                    this.RaisePropertyChanged("AllDosages");
                    this.RaisePropertyChanged("ValidFrequencies");
                    this.RaisePropertyChanged("AllFrequencies");
                    this.RaisePropertyChanged("OtherFrequencies");
                    this.RaisePropertyChanged("ValidDurations");

                    if (this.Form == null)
                    {
                        this.RaisePropertyChanged("MandatoryForm");
                    }

                    if (this.Site == null)
                    {
                        this.RaisePropertyChanged("MandatorySite");
                    }

                    if (this.Strength == null)
                    {
                        this.RaisePropertyChanged("MandatoryStrength");
                    }

                    this.RaisePropertyChanged("RouteBrandedDrugs");
                    this.RaisePropertyChanged("RouteStrengths");
                    this.RaisePropertyChanged("RouteForms");
                    this.RaisePropertyChanged("RouteDosages");
                    this.RaisePropertyChanged("RouteFrequencies");
                    this.RaisePropertyChanged("AsRequiredReasons");
                    this.RaisePropertyChanged("PreviewPrescription");
                    this.ValidateSelections("Route");
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is a mandatory form.
        /// </summary>
        public bool MandatoryForm
        {
            get
            {
                // For 'chloramphenicol - cutaneous', a mandatory form has been hard-coded
                // as the sample data model does not support this check for unlicensed routes.
                if (this.Drug != null && this.Route != null && this.Drug.Name == "chloramphenicol" && this.route.Value == "cutaneous")
                {
                    return true;
                }
                else if (this.Route != null)
                {
                    return this.Route.MandatoryForm;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is a mandatory brand.
        /// </summary>
        public bool MandatoryBrand
        {
            get
            {
                if (this.Drug != null && this.Route != null)
                {
                    return this.Route.MandatoryBrandName;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the site is mandatory.
        /// </summary>
        public bool MandatorySite
        {
            get
            {
                // For 'chloramphenicol - cutaneous', a mandatory form has been hard-coded
                // as the sample data model does not support this check for unlicensed routes.
                if (this.Drug != null && this.Route != null && this.Drug.Name == "chloramphenicol" && this.route.Value == "cutaneous")
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the routes for the selected drug.
        /// </summary>
        public Route[] Routes
        {
            get
            {
                if (this.Drug != null && this.Drug.Routes != null)
                {
                    return this.Drug.Routes;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the branded template prescription routes.
        /// </summary>
        public Route[] BrandedRoutes
        {
            get
            {
                if (this.Drug != null)
                {
                    return this.Drug.BrandedRoutes;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the valid brands.
        /// </summary>
        public DrugElement[] ValidBrands
        {
            get
            {
                if (this.Drug != null && this.Route != null && this.Drug.Brands != null)
                {
                    DrugElement[] sourceAtributes = this.GetValidDrugElements(this.Drug.Brands);
                    if (sourceAtributes.Length > 0)
                    {
                        List<DrugElement> brands = new List<DrugElement>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            brands.Add(attribute as DrugElement);
                        }

                        return brands.ToArray();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the template prescriptions.
        /// </summary>
        public TemplatePrescription[] TemplatePrescriptions
        {
            get
            {
                if (this.Drug != null && this.Drug.TemplatePrescriptions != null && this.Route != null && (!this.Route.MandatoryForm || this.Form != null))
                {
                    TemplatePrescription[] templatePrescriptions = (from template in this.Drug.TemplatePrescriptions
                                                                    where template.Route.DisplayRoute == this.Route.DisplayRoute || template.Route.DisplayRoute == this.Route.DisplayValue + (this.Form != null ? " ― " + this.Form.DisplayValue : string.Empty)
                                                                    select template).ToArray();

                    if (templatePrescriptions.Length > 0)
                    {
                        return templatePrescriptions;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the valid strengths.
        /// </summary>
        public Strength[] ValidStrengths
        {
            get
            {
                if (this.Drug != null && this.Route != null && this.Drug.Strengths != null)
                {
                    DrugElement[] sourceAtributes = this.GetValidDrugElements(this.Drug.Strengths);
                    if (sourceAtributes.Length > 0)
                    {
                        List<Strength> strengths = new List<Strength>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            strengths.Add(attribute as Strength);
                        }

                        return strengths.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the other strengths.
        /// </summary>
        public Strength[] OtherStrengths
        {
            get
            {
                if (this.Drug != null && this.Route != null && this.Drug.Strengths != null)
                {
                    DrugElement[] validStrengths = this.GetValidDrugElements(this.Drug.Strengths);
                    List<Strength> strengths = new List<Strength>();
                    foreach (Strength strength in this.Drug.Strengths)
                    {
                        if (!validStrengths.Contains(strength) && strength.ValidRoutes.Contains(this.Route.DisplayValue))
                        {
                            strengths.Add(strength);
                        }
                    }

                    if (strengths.Count > 0)
                    {
                        return strengths.ToArray();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the route forms.
        /// </summary>
        public Form[] RouteForms
        {
            get
            {
                if (this.Drug != null && this.Route != null && this.Drug.Forms != null)
                {
                    DrugElement[] formElements = (from attribute in this.Drug.Forms
                                      where attribute.ValidRoutes == null || attribute.ValidRoutes.Contains(this.Route.DisplayValue.Trim())
                                      select attribute).ToArray();

                    List<Form> forms = new List<Form>();
                    foreach (DrugElement form in formElements)
                    {
                        forms.Add(form as Form);
                    }

                    if (forms.Count > 0)
                    {
                        return forms.ToArray();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the valid forms.
        /// </summary>
        public Form[] ValidForms
        {
            get
            {
                if (this.Drug != null && this.Route != null && this.Drug.Forms != null)
                {
                    DrugElement[] sourceAtributes = this.GetValidDrugElements(this.Drug.Forms);
                    if (sourceAtributes.Length > 0)
                    {
                        List<Form> forms = new List<Form>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            forms.Add(attribute as Form);
                        }

                        return forms.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the other forms.
        /// </summary>
        /// <value>The other forms.</value>
        public Form[] OtherForms
        {
            get
            {
                if (this.Drug != null && this.Route != null && this.Drug.Forms != null)
                {
                    DrugElement[] validForms = this.GetValidDrugElements(this.Drug.Forms);
                    List<Form> forms = new List<Form>();
                    foreach (Form form in this.Drug.Forms)
                    {
                        if (!validForms.Contains(form) && form.ValidRoutes.Contains(this.Route.DisplayValue))
                        {
                            forms.Add(form);
                        }
                    }

                    if (forms.Count > 0)
                    {
                        return forms.ToArray();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the valid dosages.
        /// </summary>
        public Dose[] ValidDosages
        {
            get
            {
                if (this.Drug != null && this.Route != null && (!this.Route.MandatoryForm || this.Form != null) && this.Drug.Dosages != null)
                {
                    DrugElement[] sourceAtributes = this.GetValidDrugElements(this.Drug.Dosages);
                    if (sourceAtributes.Length > 0)
                    {
                        List<Dose> dosages = new List<Dose>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            dosages.Add(attribute as Dose);
                        }

                        return dosages.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets all the dosages for the current route, brand and form.
        /// </summary>
        public Dose[] AllDosages
        {
            get
            {
                if (this.Drug != null && this.Route != null && (!this.Route.MandatoryForm || this.Form != null) && this.Drug.Dosages != null)
                {
                    DrugElement[] sourceAtributes = this.GetValidRouteFormDrugElements(this.Drug.Dosages);
                    if (sourceAtributes.Length > 0)
                    {
                        List<Dose> dosages = new List<Dose>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            dosages.Add(attribute as Dose);
                        }

                        return dosages.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the valid dose units.
        /// </summary>
        public string ValidDoseUnit
        {
            get
            {
                if (this.ValidDosages != null && this.ValidDosages.Length > 0)
                {
                    return this.ValidDosages[0].Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[this.ValidDosages[0].Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length - 1];
                }

                return "mg";
            }
        }

        /// <summary>
        /// Gets the valid frequencies.
        /// </summary>
        public Frequency[] ValidFrequencies
        {
            get
            {
                if (this.IsUnlicensed)
                {
                    return DrugDataHelper.AllFrequencies;
                }
                else if (this.Drug != null && this.Route != null && (!this.MandatoryForm || this.Form != null) && this.Drug.Frequencies != null && (this.IsDetailedView || this.Dose != null || this.frequencySelected))
                {
                    DrugElement[] sourceAtributes = this.GetValidDrugElements(this.Drug.Frequencies);
                    if (sourceAtributes.Length > 0)
                    {
                        List<Frequency> frequencies = new List<Frequency>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            Frequency frequency = attribute as Frequency;
                            if (this.IsDetailedView && !frequency.AsRequired)
                            {
                                frequencies.Add(frequency);
                            }
                            else if (!this.IsDetailedView)
                            {
                                frequencies.Add(frequency);
                            }
                        }

                        return frequencies.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }                

                return null;
            }
        }

        /// <summary>
        /// Gets the valid frequencies for the current route, brand and form selection.
        /// </summary>
        public Frequency[] AllFrequencies
        {
            get
            {
                if (this.IsUnlicensed)
                {
                    return DrugDataHelper.AllFrequencies;
                }
                else if (this.Drug != null && this.Route != null && (!this.MandatoryForm || this.Form != null) && this.Drug.Frequencies != null)
                {
                    DrugElement[] sourceAtributes = this.GetValidRouteFormDrugElements(this.Drug.Frequencies);
                    if (sourceAtributes.Length > 0)
                    {
                        List<Frequency> frequencies = new List<Frequency>();
                        foreach (DrugElement attribute in sourceAtributes)
                        {
                            Frequency frequency = attribute as Frequency;
                            if (this.IsDetailedView && !frequency.AsRequired)
                            {
                                frequencies.Add(frequency);
                            }                            
                        }

                        return frequencies.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }                

                return null;
            }
        }

        /// <summary>
        /// Gets the valid durations.
        /// </summary>
        public Duration[] ValidDurations
        {
            get
            {
                if (this.Drug != null && this.Route != null && (this.IsUnlicensed || (!this.MandatoryForm || this.Form != null)) && !this.OnceOnly &&
                    (this.IsDetailedView || this.Duration != null || (this.Frequency != null && (!this.AsRequired || (this.AsRequiredReason != null && !string.IsNullOrEmpty(this.AsRequiredReason.Value))))))
                {
                    if (this.validDurations == null)
                    {
                        List<Duration> validDurations = new List<Duration>();
                        validDurations.Add(new Duration() { Value = TimeSpan.MaxValue, DisplayValue = "ongoing" });
                        validDurations.Add(new Duration() { Value = new TimeSpan(7, 0, 0, 0), DisplayValue = "7 days" });
                        validDurations.Add(new Duration() { Value = new TimeSpan(14, 0, 0, 0), DisplayValue = "14 days" });
                        this.validDurations = validDurations.ToArray();
                    }

                    return this.validDurations;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the valid administration times.
        /// </summary>
        public AdministrationTimes[] ValidAdministrationTimes
        {
            get
            {
                if (!this.AsRequired && this.Frequency != null && !this.Frequency.IsCustomValue && this.Drug != null && this.Route != null)
                {
                    if (this.OnceOnly)
                    {
                        List<AdministrationTimes> administrationTimes = new List<AdministrationTimes>();
                        administrationTimes.Add(new AdministrationTimes()
                        {
                            Times = new DateTime?[]
                            {
                                DateTime.Now.Date.AddHours(8)
                            }
                        });

                        administrationTimes.Add(new AdministrationTimes()
                        {
                            Times = new DateTime?[]
                            {
                                DateTime.Now.Date.AddHours(12)
                            }
                        });

                        administrationTimes.Add(new AdministrationTimes()
                        {
                            Times = new DateTime?[]
                            {
                                DateTime.Now.Date.AddHours(16)
                            }
                        });

                        administrationTimes.Add(new AdministrationTimes()
                        {
                            Times = new DateTime?[]
                            {
                                DateTime.Now.Date.AddHours(20)
                            }
                        });

                        return administrationTimes.ToArray();
                    }
                    else if (this.Drug.AdministrationTimes != null)
                    {
                        DrugElement[] sourceAtributes = this.GetValidDrugElements(this.Drug.AdministrationTimes);
                        if (sourceAtributes.Length > 0)
                        {
                            List<AdministrationTimes> administrationTimes = new List<AdministrationTimes>();
                            foreach (DrugElement attribute in sourceAtributes)
                            {
                                administrationTimes.Add(attribute as AdministrationTimes);
                            }

                            return administrationTimes.ToArray();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the as required reasons.
        /// </summary>
        public ElementValue[] AsRequiredReasons
        {
            get
            {
                if (this.Drug != null && this.AsRequired)
                {
                    return this.Drug.AsRequiredReasons;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the template prescription.
        /// </summary>
        public TemplatePrescription TemplatePrescription
        {
            get 
            {
                return this.templatePrescription; 
            }

            set
            {
                this.updatingTemplatePrescription = true;
                this.templatePrescription = value;
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(value.BrandName) && this.Drug.Brands != null)
                    {
                        this.Brand = GetElementValueWithDisplayValue(this.Drug.Brands, this.templatePrescription.BrandName) as DrugElement;
                    }

                    if (this.Form == null)
                    {
                        this.Form = (this.templatePrescription.Route != null) ? GetElementValueWithDisplayValue(this.Drug.Forms, this.templatePrescription.Route.Form) as Form : null;
                    }

                    this.Strength = GetElementValueWithDisplayValue(this.Drug.Strengths, this.templatePrescription.Strength) as Strength;

                    if (!string.IsNullOrEmpty(this.templatePrescription.Dose))
                    {
                        this.Dose = GetElementValueWithDisplayValue(this.Drug.Dosages, this.templatePrescription.Dose) as Dose;
                    }

                    if (this.templatePrescription.Frequency != null)
                    {
                        this.Frequency = GetElementValueWithDisplayValue(this.Drug.Frequencies, this.templatePrescription.Frequency.DisplayValue) as Frequency;

                        if (this.Frequency != null)
                        {
                            this.AsRequired = this.Frequency.AsRequired;
                        }

                        this.RaisePropertyChanged("ValidDurations");

                        if (this.Frequency != null && this.Frequency.AdministrationTimes != null)
                        {
                            this.AdministrationTimes = GetElementValueWithDisplayValue(this.Drug.AdministrationTimes, this.Frequency.AdministrationTimes.Value) as AdministrationTimes;
                        }
                        else if (this.OnceOnly && this.ValidAdministrationTimes != null)
                        {
                            foreach (AdministrationTimes administrationTimes in this.ValidAdministrationTimes)
                            {
                                if (!this.ValidAdministrationTimes.Contains(this.AdministrationTimes) && administrationTimes.Times.Length >= 1 && administrationTimes.Times[0].HasValue && administrationTimes.Times[0].Value.Hour > DateTime.Now.Hour)
                                {
                                    this.AdministrationTimes = administrationTimes;
                                }
                            }

                            if (this.AdministrationTimes == null && this.ValidAdministrationTimes.Length > 0)
                            {
                                this.AdministrationTimes = this.ValidAdministrationTimes[0];
                            }
                        }
                        else
                        {
                            this.AdministrationTimes = null;
                        }
                    }

                    if (this.ValidDurations != null && this.Duration == null && !this.OnceOnly && (!this.AsRequired || (this.AsRequiredReason != null && !string.IsNullOrEmpty(this.AsRequiredReason.Value))))
                    {
                        if (this.templatePrescription.Duration.HasValue)
                        {
                            foreach (Duration duration in this.ValidDurations)
                            {
                                if (duration.Value == this.templatePrescription.Duration)
                                {
                                    this.Duration = duration;
                                    break;
                                }
                            }
                        }
                    }
                }

                this.RaisePropertyChanged("TemplatePrescription");                
                this.UpdateIsAuthorizable();
                this.updatingTemplatePrescription = false;
            }
        }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public Strength Strength
        {
            get 
            {
                return this.strength; 
            }

            set 
            {
                bool changed = this.strength != value;

                if (changed && this.strength != null)
                {
                    this.Dose = null;
                    this.Method = string.Empty;
                    this.Site = null;
                    this.Frequency = null;
                    this.AsRequired = false;
                    this.AdministrationTimes = null;
                }

                this.strength = value;

                if (changed)
                {
                    this.RaisePropertyChanged("Strength");
                    this.ValidateSelections("Strength");                    

                    if (!this.updatingTemplatePrescription && this.TemplatePrescription != null && !string.IsNullOrEmpty(this.TemplatePrescription.Strength) && (this.Strength == null || this.TemplatePrescription.Strength != this.Strength.DisplayValue))
                    {
                        this.TemplatePrescription = null;
                    }

                    if (this.Duration == null || this.ValidDurations == null || !this.ValidDurations.Contains(this.Duration))
                    {
                        this.RaisePropertyChanged("ValidDurations");
                    }

                    this.RaisePropertyChanged("ValidFrequencies");
                    this.RaisePropertyChanged("AllFrequencies");
                    this.RaisePropertyChanged("ValidDosages");
                    this.RaisePropertyChanged("AllDosages");

                    if (this.ValidForms != null && this.Form != null && this.ValidForms.Contains(this.Form))
                    {
                        this.RaisePropertyChanged("ValidForms");
                        this.RaisePropertyChanged("OtherForms");
                    }
                    else
                    {
                        this.RaisePropertyChanged("OtherForms");
                        this.RaisePropertyChanged("ValidForms");
                    }

                    this.RaisePropertyChanged("PreviewPrescription");
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        public Form Form
        {
            get 
            { 
                return this.form; 
            }

            set 
            {
                bool changed = this.form != value;

                if (changed && this.MandatoryForm && !(this.IsUnlicensed && this.form == null))
                {
                    // Clears out previously selected values.   
                    this.Dose = null;
                    this.CustomDose.Value = string.Empty;
                    this.Site = null;
                    this.Method = string.Empty;
                    this.Frequency = null;
                    this.AsRequired = false;
                    this.AdministrationTimes = null;
                    this.frequencySelected = false;
                    this.TemplatePrescription = null;
                    this.StartDate = null;
                    this.Duration = null;
                }

                this.form = value;

                if (changed)
                {
                    this.RaisePropertyChanged("Form");
                    this.ValidateSelections("Form");                    

                    if ((this.Route != null && this.Route.MandatoryForm && this.Form == null) || this.Form != null)
                    {
                        this.RaisePropertyChanged("TemplatePrescriptions");
                        this.RaisePropertyChanged("QuickPrescriptionTemplatePrescriptions");
                    }

                    if (this.Duration == null || this.ValidDurations == null || !this.ValidDurations.Contains(this.Duration))
                    {
                        this.RaisePropertyChanged("ValidDurations");
                    }

                    this.RaisePropertyChanged("ValidFrequencies");
                    this.RaisePropertyChanged("AllFrequencies");
                    this.RaisePropertyChanged("ValidDosages");
                    this.RaisePropertyChanged("AllDosages");
                    
                    if (this.ValidStrengths != null && this.Strength != null && this.ValidStrengths.Contains(this.Strength))
                    {
                        this.RaisePropertyChanged("ValidStrengths");
                        this.RaisePropertyChanged("OtherStrengths");
                    }
                    else
                    {
                        this.RaisePropertyChanged("OtherStrengths");
                        this.RaisePropertyChanged("ValidStrengths");
                    }

                    this.RaisePropertyChanged("ValidDoseUnit");
                    this.UpdateCustomDoseValidUnit();

                    if (this.Strength == null)
                    {
                        this.RaisePropertyChanged("MandatoryStrength");
                    }

                    this.RaisePropertyChanged("PreviewPrescription");
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is a mandatory strength.
        /// </summary>
        public bool MandatoryStrength
        {
            get
            {
                if (this.Form != null)
                {
                    return this.Form.MandatoryStrength;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        public Dose Dose
        {
            get 
            {
                return this.dose; 
            }

            set 
            {
                bool changed = this.dose != value;

                if (this.dose != null && this.dose.IsCustomValue && this.dose != value)
                {
                    this.dose.Value = string.Empty;
                }

                if (this.dose != null)
                {
                    this.dose.PropertyChanged -= new PropertyChangedEventHandler(this.Dose_PropertyChanged);
                }

                this.dose = value;

                if (this.dose != null)
                {
                    this.dose.PropertyChanged += new PropertyChangedEventHandler(this.Dose_PropertyChanged);
                }

                if (changed)
                {
                    this.RaisePropertyChanged("Dose");
                    this.ValidateSelections("Dose");

                    if (!this.updatingTemplatePrescription && this.TemplatePrescription != null && !string.IsNullOrEmpty(this.TemplatePrescription.Dose) && (this.Dose == null || this.TemplatePrescription.Dose != this.Dose.DisplayValue))
                    {
                        this.TemplatePrescription = null;
                    }

                    if (this.Duration == null || this.ValidDurations == null || !this.ValidDurations.Contains(this.Duration))
                    {
                        this.RaisePropertyChanged("ValidDurations");
                    }

                    this.RaisePropertyChanged("ValidFrequencies");
                    this.RaisePropertyChanged("AllFrequencies");

                    if (this.ValidStrengths != null && this.Strength != null && this.ValidStrengths.Contains(this.Strength))
                    {
                        this.RaisePropertyChanged("ValidStrengths");
                        this.RaisePropertyChanged("OtherStrengths");
                    }
                    else
                    {
                        this.RaisePropertyChanged("OtherStrengths");
                        this.RaisePropertyChanged("ValidStrengths");
                    }

                    if (this.ValidForms != null && this.Form != null && this.ValidForms.Contains(this.Form))
                    {
                        this.RaisePropertyChanged("ValidForms");
                        this.RaisePropertyChanged("OtherForms");
                    }
                    else
                    {
                        this.RaisePropertyChanged("OtherForms");
                        this.RaisePropertyChanged("ValidForms");
                    }

                    this.RaisePropertyChanged("PreviewPrescription");
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom dose.
        /// </summary>
        public Dose CustomDose
        {
            get 
            { 
                return this.customDose; 
            }

            set 
            { 
                this.customDose = value;
                this.RaisePropertyChanged("CustomDose");
            }
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public string Method
        {
            get 
            { 
                return this.method; 
            }

            set 
            {
                this.method = value;
                this.RaisePropertyChanged("Method");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        public DrugElement Site
        {
            get 
            { 
                return this.site; 
            }

            set 
            {
                if (this.site != null && this.site.IsCustomValue && this.site != value)
                {
                    this.site.Value = string.Empty;
                }

                if (this.site != null)
                {
                    this.site.PropertyChanged -= new PropertyChangedEventHandler(this.Site_PropertyChanged);
                }

                this.site = value;

                if (this.site != null)
                {
                    this.site.PropertyChanged += new PropertyChangedEventHandler(this.Site_PropertyChanged);
                }

                this.RaisePropertyChanged("Site");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        public Frequency Frequency
        {
            get 
            {
                return this.frequency; 
            }

            set 
            {
                bool updateAsRequired = false;
                if (this.frequency != null && this.frequency.IsCustomValue && this.frequency != value)
                {
                    this.frequency.Value = string.Empty;
                }

                if (this.frequency != null && value != null)
                {
                    updateAsRequired = this.frequency.AsRequired != value.AsRequired;
                }
                else if (this.frequency == null && value != null)
                {
                    updateAsRequired = this.AsRequired != value.AsRequired;
                }

                bool changed = this.frequency != value;

                if (this.frequency != null)
                {
                    this.frequency.PropertyChanged -= new PropertyChangedEventHandler(this.Frequency_PropertyChanged);
                }

                this.frequency = value;

                if (this.frequency != null)
                {
                    this.frequency.PropertyChanged += new PropertyChangedEventHandler(this.Frequency_PropertyChanged);
                }

                if (changed)
                {
                    this.RaisePropertyChanged("ValidAdministrationTimes");
                    this.RaisePropertyChanged("AdministrationTimesLabel");

                    if (!this.updatingTemplatePrescription && this.TemplatePrescription != null && this.TemplatePrescription.Frequency != null && (this.Frequency == null || this.TemplatePrescription.Frequency.DisplayValue != this.Frequency.DisplayValue))
                    {
                        this.TemplatePrescription = null;
                    }

                    if (this.frequency != null)
                    {
                        this.frequencySelected = true;

                        if (this.OnceOnly)
                        {
                            this.AsRequired = false;
                        }
                        else if (!this.IsDetailedView && updateAsRequired)
                        {
                            this.AsRequired = this.Frequency.AsRequired;
                        }

                        if (!this.StartDate.HasValue && (!this.AsRequired || (this.AsRequiredReason != null && !string.IsNullOrEmpty(this.AsRequiredReason.Value))))
                        {
                            this.StartDate = DateTime.Now.Date;
                        }

                        if (this.frequency.AdministrationTimes != null)
                        {
                            this.AdministrationTimes = GetElementValueWithDisplayValue(this.Drug.AdministrationTimes, this.frequency.AdministrationTimes.Value) as AdministrationTimes;

                            if (this.AdministrationTimes != null)
                            {
                                this.AdministrationTimes.UpdateDisplayAdministrationTimes(this.FirstDoseTime, true);
                            }
                        }
                        else if (this.OnceOnly && this.ValidAdministrationTimes != null)
                        {
                            foreach (AdministrationTimes administrationTimes in this.ValidAdministrationTimes)
                            {
                                if (!this.ValidAdministrationTimes.Contains(this.AdministrationTimes) && administrationTimes.Times.Length >= 1 && administrationTimes.Times[0].HasValue && administrationTimes.Times[0].Value.Hour > DateTime.Now.Hour)
                                {
                                    this.AdministrationTimes = administrationTimes;
                                }
                            }

                            if (this.AdministrationTimes == null && this.ValidAdministrationTimes.Length > 0)
                            {
                                this.AdministrationTimes = this.ValidAdministrationTimes[0];
                            }                            
                        }
                        else
                        {
                            this.AdministrationTimes = null;
                        }

                        if (this.ValidDurations != null && this.Duration == null && !this.OnceOnly && (!this.AsRequired || (this.AsRequiredReason != null && !string.IsNullOrEmpty(this.AsRequiredReason.Value))))
                        {
                            if (this.frequency.HasDuration)
                            {
                                foreach (Duration duration in this.ValidDurations)
                                {
                                    if (duration.Value == this.frequency.Duration)
                                    {
                                        this.Duration = duration;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    this.RaisePropertyChanged("Frequency");
                    this.ValidateSelections("Frequency");
                    this.RaisePropertyChanged("ValidDosages");
                    this.RaisePropertyChanged("AllDosages");
                    this.RaisePropertyChanged("FirstDose");
                    this.RaisePropertyChanged("FirstDoseTime");
                    this.RaisePropertyChanged("AsRequiredReasons");
                    this.RaisePropertyChanged("ValidAdministrationTimes");
                    this.RaisePropertyChanged("AdministrationTimesLabel");
                    this.RaisePropertyChanged("StartingLabel");
                    this.RaisePropertyChanged("OnceOnly");
                    this.RaisePropertyChanged("PreviewPrescription");
                    this.RaisePropertyChanged("ValidDurations");
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this prescription is once only.
        /// </summary>
        public bool OnceOnly
        {
            get
            {
                if (this.Frequency != null)
                {
                    return this.Frequency.Value == "once only";
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the administration times label.
        /// </summary>
        public string AdministrationTimesLabel
        {
            get
            {
                if (this.ValidAdministrationTimes != null && this.ValidAdministrationTimes.Length > 0)
                {
                    return this.ValidAdministrationTimes[0].Times.Length == 1 ? "at" : "at these times";
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the administration times.
        /// </summary>
        public AdministrationTimes AdministrationTimes
        {
            get 
            {
                if (!this.AsRequired)
                {
                    return this.administrationTimes;
                }

                return null;
            }

            set 
            {
                if (value == null && this.ValidAdministrationTimes != null && this.ValidAdministrationTimes.Contains(this.administrationTimes))
                {
                    this.RaisePropertyChanged("AdministrationTimes");
                }
                else
                {
                    bool changed = this.administrationTimes != value;
                    this.administrationTimes = value;

                    if (changed)
                    {
                        this.RaisePropertyChanged("AdministrationTimes");
                        this.RaisePropertyChanged("ValidFirstDoseTimes");
                        this.RaisePropertyChanged("AdministrationTimesLabel");
                        this.UpdateFirstDoseTime();
                        this.RaisePropertyChanged("FirstDoseTime");
                        this.RaisePropertyChanged("FirstDose");
                        this.ValidateSelections("AdministrationTimes");
                        this.RaisePropertyChanged("PreviewPrescription");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the valid first dose times.
        /// </summary>
        public DateTime?[] ValidFirstDoseTimes
        {
            get
            {
                if (!this.AsRequired && !this.OnceOnly && this.AdministrationTimes != null)
                {
                    if (this.AdministrationTimes.Times.Length > 1)
                    {
                        return this.AdministrationTimes.Times;
                    }
                }               

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the as required reason.
        /// </summary>
        public DrugElement AsRequiredReason
        {
            get 
            { 
                return this.asRequiredReason; 
            }

            set
            {
                if (this.asRequiredReason != null && this.asRequiredReason.IsCustomValue && this.asRequiredReason != value)
                {
                    this.asRequiredReason.Value = string.Empty;
                }

                if (this.asRequiredReason != null)
                {
                    this.asRequiredReason.PropertyChanged -= new PropertyChangedEventHandler(this.AsRequiredReason_PropertyChanged);
                }

                this.asRequiredReason = value;

                if (this.asRequiredReason != null)
                {
                    this.asRequiredReason.PropertyChanged += new PropertyChangedEventHandler(this.AsRequiredReason_PropertyChanged);
                }

                if (!this.StartDate.HasValue && this.AsRequired && this.asRequiredReason != null && !string.IsNullOrEmpty(this.asRequiredReason.Value))
                {
                    this.StartDate = DateTime.Now.Date;
                }

                if (this.Duration == null && this.AsRequired && this.asRequiredReason != null && !string.IsNullOrEmpty(this.asRequiredReason.Value))
                {
                    if (this.frequency.HasDuration && this.ValidDurations != null)
                    {
                        foreach (Duration duration in this.ValidDurations)
                        {
                            if (duration.Value == this.frequency.Duration)
                            {
                                this.Duration = duration;
                                break;
                            }
                        }
                    }
                }

                this.RaisePropertyChanged("AsRequiredReason");
                this.ValidateSelections("AsRequiredReason");
                this.RaisePropertyChanged("PreviewPrescription");
                this.RaisePropertyChanged("ValidDurations");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is as required.
        /// </summary>
        public bool AsRequired
        {
            get 
            { 
                return this.asRequired; 
            }

            set 
            {
                bool changed = this.asRequired != value;
                this.asRequired = value;

                if (changed)
                {
                    if (!this.asRequired)
                    {
                        this.AsRequiredReason = null;
                    }

                    this.RaisePropertyChanged("AsRequired");
                    this.RaisePropertyChanged("AdministrationTimes");
                    this.RaisePropertyChanged("ValidAdministrationTimes");
                    this.RaisePropertyChanged("AdministrationTimesLabel");
                    this.UpdateFirstDoseTime();
                    this.RaisePropertyChanged("ValidFirstDoseTimes");
                    this.RaisePropertyChanged("FirstDose");
                    this.RaisePropertyChanged("FirstDoseTime");
                    this.RaisePropertyChanged("StartingLabel");
                    this.RaisePropertyChanged("AsRequiredReasons");
                    this.ValidateSelections("AsRequired");
                    this.RaisePropertyChanged("PreviewPrescription");
                    this.RaisePropertyChanged("ValidDurations");
                    this.UpdateIsAuthorizable();
                }
            }
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public Duration Duration
        {
            get 
            { 
                return this.duration; 
            }

            set
            {
                if (this.duration != null)
                {
                    this.duration.PropertyChanged -= new PropertyChangedEventHandler(this.Duration_PropertyChanged);

                    if (this.duration.IsCustomValue && this.duration != value)
                    {
                        this.duration.DisplayValue = string.Empty;
                        this.duration.Value = TimeSpan.FromMilliseconds(1);
                    }
                }                

                this.duration = value;

                if (this.duration != null)
                {
                    this.duration.PropertyChanged += new PropertyChangedEventHandler(this.Duration_PropertyChanged);
                }

                this.RaisePropertyChanged("Duration");
                this.RaisePropertyChanged("ValidDurations");
                this.RaisePropertyChanged("StartingLabel");
                this.RaisePropertyChanged("ContinuingLabel");
                this.RaisePropertyChanged("EndDate");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Gets the continuung label.
        /// </summary>
        public string ContinuingLabel
        {
            get
            {
                if (this.Duration != null)
                {
                    return "duration";
                }

                return "duration";
            }
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        public DrugElement Reason
        {
            get 
            { 
                return this.reason; 
            }

            set 
            {
                if (this.reason != null && this.reason.IsCustomValue && this.reason != value)
                {
                    this.reason.Value = string.Empty;
                }

                if (this.reason != null)
                {
                    this.reason.PropertyChanged -= new PropertyChangedEventHandler(this.Reason_PropertyChanged);
                }

                this.reason = value;

                if (this.reason != null)
                {
                    this.reason.PropertyChanged += new PropertyChangedEventHandler(this.Reason_PropertyChanged);
                }

                this.RaisePropertyChanged("Reason");
            }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate
        {
            get 
            {
                return this.startDate; 
            }

            set
            {
                this.updatingStartDate = true;

                if (value.HasValue)
                {
                    this.startDate = value.Value.Date;
                    this.UpdateFirstDoseTime();
                }
                else
                {
                    this.startDate = value;
                }
                
                this.RaisePropertyChanged("StartDate");
                this.RaisePropertyChanged("FirstDoseTime");
                this.RaisePropertyChanged("FirstDose");
                this.RaisePropertyChanged("EndDate");
                this.RaisePropertyChanged("PreviewPrescription");
                this.updatingStartDate = false;
            }
        }

        /// <summary>
        /// Gets or sets the first dose time.
        /// </summary>
        public DateTime? FirstDoseTime
        {
            get
            {
                if (this.Frequency != null && this.Frequency.IsCustomValue)
                {
                    return null;
                }
                else if (this.OnceOnly)
                {
                    return null;
                }
                else if (this.AdministrationTimes != null && this.AdministrationTimes.Times != null && this.AdministrationTimes.Times.Length == 1)
                {
                    return null;
                }
                else if (this.firstDoseTime.HasValue)
                {
                    return this.firstDoseTime;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                this.firstDoseTime = value;

                if (this.firstDoseTime.HasValue)
                {
                    if (!this.updatingStartDate && this.FirstDose < DateTime.Now.AddMinutes(-1) && this.StartDate.HasValue && this.startDate.Value.Date == DateTime.Now.Date)
                    {
                        this.startDate = DateTime.Now.AddDays(1);
                        this.RaisePropertyChanged("StartDate");
                    }

                    if (this.AdministrationTimes != null)
                    {
                        this.AdministrationTimes.UpdateDisplayAdministrationTimes(this.FirstDoseTime, true);
                    }
                }

                this.RaisePropertyChanged("FirstDoseTime");
                this.RaisePropertyChanged("FirstDose");
                this.RaisePropertyChanged("PreviewPrescription");
            }
        }

        /// <summary>
        /// Gets the first dose date.
        /// </summary>
        public DateTime FirstDose
        {
            get
            {
                if (this.firstDoseTime.HasValue && this.StartDate.HasValue)
                {
                    return this.startDate.Value.AddHours(this.firstDoseTime.Value.Hour).AddMinutes(this.firstDoseTime.Value.Minute);
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Gets the end date.
        /// </summary>
        public DateTime? EndDate
        {
            get
            {
                if (this.OnceOnly)
                {
                    return this.FirstDose;
                }
                else if (this.Duration != null && this.Duration.Value.HasValue && this.Duration.Value.Value != TimeSpan.MaxValue)
                {
                    return this.FirstDose + this.Duration.Value.Value;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the starting label.
        /// </summary>
        public string StartingLabel
        {
            get
            {
                if (this.AsRequired)
                {
                    return "starting";
                }
                else if (this.Frequency != null && this.Frequency.Value == "once only")
                {
                    return "only dose";
                }
                else if (this.AdministrationTimes != null || (this.Frequency != null && this.Frequency.IsCustomValue))
                {
                    return "first dose";
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the prescription for preview.
        /// </summary>
        public CompletedPrescription PreviewPrescription
        {
            get
            {
                return this.CopyForAuthorizing();
            }
        }

        /// <summary>
        /// Creates a copy of the prescription.
        /// </summary>
        /// <returns>A copy of the prescription.</returns>
        public CompletedPrescription CopyForAuthorizing()
        {
            return new CompletedPrescription()
            {
                Name = this.Drug != null ? this.Drug.Name : null,
                BrandName = this.Drug != null ? (this.Drug.IsBrandGeneric ? null : this.Brand != null ? this.Brand.DisplayValue : (this.Drug != null ? this.Drug.BrandName : null)) : null,
                Route = this.Route != null ? this.Route.DisplayValue : null,
                Form = this.Form != null ? this.Form.DisplayValue : null,
                Strength = this.Strength != null ? this.Strength.MedsLineDisplayValue : null,
                Dose = this.Dose != null ? this.Dose.DisplayValue : null,
                Method = this.Method,
                Site = this.Site != null ? this.Site.DisplayValue : null,
                Frequency = this.Frequency != null ? this.Frequency.Value : null,
                IsAsRequired = this.AsRequired,
                Reason = this.Reason != null ? this.Reason.DisplayValue : null,
                StartDate = this.StartDate.HasValue ? this.StartDate.Value : DateTime.Now.Date,
                EndDate = this.EndDate,
                Status = "Not started"
            };
        }

        /// <summary>
        /// Gets and ElementValue with a specific value.
        /// </summary>
        /// <param name="elementValues">The element values to search.</param>
        /// <param name="value">The value to check.</param>
        /// <returns>A matching element value.</returns>
        private static ElementValue GetElementValueWithDisplayValue(ElementValue[] elementValues, string value)
        {
            if (elementValues != null && !string.IsNullOrEmpty(value))
            {
                foreach (ElementValue elementValue in elementValues)
                {
                    if (elementValue.DisplayValue == value)
                    {
                        return elementValue;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Validates the prescription selections.
        /// </summary>
        /// <param name="updatedProperty">The updated property.</param>
        private void ValidateSelections(string updatedProperty)
        {
            if (this.ValidateAttributes)
            {
                this.attributeQueue.Add(updatedProperty);

                if (!this.attributeQueue.Contains("AdministrationTimes") && this.AdministrationTimes != null && !this.AdministrationTimes.IsCustomValue && (this.ValidAdministrationTimes == null || !this.ValidAdministrationTimes.Contains(this.AdministrationTimes)))
                {
                    this.AdministrationTimes = null;
                }

                if (!this.attributeQueue.Contains("Form") && this.Form != null && !this.Form.IsCustomValue && (this.ValidForms == null || (!this.ValidForms.Contains(this.Form))))
                {
                    this.Form = null;
                }

                if (!this.attributeQueue.Contains("Strength") && this.Strength != null && !this.Strength.IsCustomValue && (this.ValidStrengths == null || !this.ValidStrengths.Contains(this.Strength)))
                {
                    this.Strength = null;
                }

                if (!this.attributeQueue.Contains("Brand") && this.Brand != null && (this.ValidBrands == null || !this.ValidBrands.Contains(this.Brand)))
                {
                    this.Brand = null;
                }

                this.attributeQueue.Remove(updatedProperty);
            }
        }

        /// <summary>
        /// Returns a list of drug attributes, valid for all current selections.
        /// </summary>
        /// <param name="sourceAttributes">The source list of attributes.</param>
        /// <returns>The attributes valid for the current selections.</returns>
        private DrugElement[] GetValidDrugElements(DrugElement[] sourceAttributes)
        {
            DrugElement[] attributes = sourceAttributes;
            if (this.Route != null)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidRoutes == null || attribute.ValidRoutes.Contains(this.Route.DisplayValue.Trim())
                              select attribute).ToArray();
            }

            if (this.Brand != null)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidBrands == null || attribute.ValidBrands.Contains(this.Brand.DisplayValue.Trim())
                              select attribute).ToArray();
            }

            if (this.Form != null && !this.Form.IsCustomValue)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidForms == null || attribute.ValidForms.Contains(this.Form.Value.Trim())
                              select attribute).ToArray();
            }

            if (this.Strength != null && !this.Strength.IsCustomValue)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidStrengths == null || attribute.ValidStrengths.Contains(this.Strength.Value.Trim())
                              select attribute).ToArray();
            }

            if (this.Dose != null && !this.Dose.IsCustomValue)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidDosages == null || attribute.ValidDosages.Contains(this.Dose.Value.Trim())
                              select attribute).ToArray();
            }

            if (this.Frequency != null && !this.Frequency.IsCustomValue)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidFrequencies == null || attribute.ValidFrequencies.Contains(this.Frequency.Value.Trim())
                              select attribute).ToArray();
            }

            return attributes;
        }

        /// <summary>
        /// Returns a list of drug attributes, valid for route, brand and form selections.
        /// </summary>
        /// <param name="sourceAttributes">The source list of attributes.</param>
        /// <returns>The attributes valid for the current route, brand and form selections.</returns>
        private DrugElement[] GetValidRouteFormDrugElements(DrugElement[] sourceAttributes)
        {
            DrugElement[] attributes = sourceAttributes;
            if (this.Route != null)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidRoutes == null || attribute.ValidRoutes.Contains(this.Route.DisplayValue.Trim())
                              select attribute).ToArray();
            }

            if (this.Brand != null)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidBrands == null || attribute.ValidBrands.Contains(this.Brand.DisplayValue.Trim())
                              select attribute).ToArray();
            }

            if (this.Form != null && !this.Form.IsCustomValue)
            {
                attributes = (from attribute in attributes
                              where attribute.ValidForms == null || attribute.ValidForms.Contains(this.Form.Value.Trim())
                              select attribute).ToArray();
            }

            return attributes;
        }

        /// <summary>
        /// Updates the IsAuthorizable state.
        /// </summary>
        private void UpdateIsAuthorizable()
        {
            if (this.Drug != null && this.Route != null && (this.Frequency != null) && (!this.MandatoryBrand || this.Brand != null))
            {
                if (this.Dose != null || !string.IsNullOrEmpty(this.Method))
                {
                    if (this.Dose != null && this.Dose.IsCustomValue && this.Dose.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length < 2)
                    {
                        this.IsAuthorizable = false;
                    }
                    else if (this.Frequency.IsCustomValue && this.Frequency.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length < 3)
                    {
                        this.IsAuthorizable = false;
                    }
                    else if (!this.OnceOnly && (this.Duration == null || (this.Duration.IsCustomValue && string.IsNullOrEmpty(this.Duration.DisplayValue))))
                    {
                        this.IsAuthorizable = false;
                    }
                    else if (this.MandatoryForm && this.Form == null)
                    {
                        this.IsAuthorizable = false;
                    }
                    else if (this.MandatorySite && this.Site != null && string.IsNullOrEmpty(this.Site.Value))
                    {
                        this.IsAuthorizable = false;
                    }
                    else if (this.MandatoryStrength && this.Strength == null)
                    {
                        this.IsAuthorizable = false;
                    }                    
                    else if (this.AsRequired && (this.AsRequiredReason == null || string.IsNullOrEmpty(this.AsRequiredReason.Value)))
                    {
                        this.IsAuthorizable = false;
                    }
                    else if (this.IsUnlicensed && string.IsNullOrEmpty(this.UnlicensedReason))
                    {
                        this.IsAuthorizable = false;
                    }
                    else
                    {
                        this.IsAuthorizable = true;
                        return;
                    }
                }
            }

            this.IsAuthorizable = false;
        }

        /// <summary>
        /// Updates the first dose time.
        /// </summary>
        private void UpdateFirstDoseTime()
        {
            if (this.StartDate.HasValue && this.AdministrationTimes != null && this.AdministrationTimes.Times != null && this.AdministrationTimes.Times.Length > 0)
            {
                bool firstDoseTimeSet = false;
                for (int i = 0; i < this.AdministrationTimes.Times.Length; i++)
                {
                    if (this.AdministrationTimes.Times[i].HasValue)
                    {
                        if (this.startDate.Value.AddHours(this.AdministrationTimes.Times[i].Value.Hour).AddMinutes(this.AdministrationTimes.Times[i].Value.Minute) > DateTime.Now)
                        {
                            this.FirstDoseTime = this.AdministrationTimes.Times[i];
                            firstDoseTimeSet = true;
                            break;
                        }
                    }
                }

                if (!firstDoseTimeSet)
                {
                    this.FirstDoseTime = this.AdministrationTimes.Times[0];
                }

                this.AdministrationTimes.UpdateDisplayAdministrationTimes(this.FirstDoseTime, true);
            }
            else
            {
                this.FirstDoseTime = DateTime.Now;
            }
        }
        
        #region INotifyPropertyChanged Members
        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The changed property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                if (!this.propertyChangedQueue.ContainsKey(propertyName))
                {
                    this.propertyChangedQueue.Add(propertyName, 0);
                }

                if (this.propertyChangedQueue[propertyName] < 3)
                {
                    this.propertyChangedQueue[propertyName]++;
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }

                this.propertyChangedQueue[propertyName]--;

                if (this.propertyChangedQueue[propertyName] == 0)
                {
                    this.propertyChangedQueue.Remove(propertyName);
                }
            }
        }

        #endregion

        /// <summary>
        /// Updates the custom dose valid unit.
        /// </summary>
        private void UpdateCustomDoseValidUnit()
        {
            if (this.customDose != null)
            {
                this.customDose.ValidDoseUnit = this.ValidDoseUnit;
            }
        }

        /// <summary>
        /// Updates the dose bindings.
        /// </summary>
        /// <param name="sender">The selected dose.</param>
        /// <param name="e">Property Changed Event Args.</param>
        private void Dose_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                this.RaisePropertyChanged("Dose");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Updates the site binding.
        /// </summary>
        /// <param name="sender">The as required reason.</param>
        /// <param name="e">Property Changed Event Args.</param>
        private void Site_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                this.RaisePropertyChanged("Site");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Raises the frequency property changed event.
        /// </summary>
        /// <param name="sender">The selected frequency.</param>
        /// <param name="e">Property Changed Event Args.</param>
        private void Frequency_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                this.RaisePropertyChanged("Frequency");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Updates the as reequired reason binding.
        /// </summary>
        /// <param name="sender">The as required reason.</param>
        /// <param name="e">Property Changed Event Args.</param>
        private void AsRequiredReason_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                if (!this.StartDate.HasValue && this.AsRequired && this.asRequiredReason != null && !string.IsNullOrEmpty(this.asRequiredReason.Value))
                {
                    this.StartDate = DateTime.Now.Date;
                }

                if (this.Duration == null && this.AsRequired && this.asRequiredReason != null && !string.IsNullOrEmpty(this.asRequiredReason.Value))
                {
                    if (this.frequency.HasDuration && this.ValidDurations != null)
                    {
                        foreach (Duration duration in this.ValidDurations)
                        {
                            if (duration.Value == this.frequency.Duration)
                            {
                                this.Duration = duration;
                                break;
                            }
                        }
                    }
                }

                this.RaisePropertyChanged("AsRequiredReason");
                this.RaisePropertyChanged("PreviewPrescription");
                this.RaisePropertyChanged("ValidDurations");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Raises the duration property changed.
        /// </summary>
        /// <param name="sender">The current duration.</param>
        /// <param name="e">Property Changed Event Args.</param>
        private void Duration_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                this.RaisePropertyChanged("Duration");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }

        /// <summary>
        /// Updates the reason binding.
        /// </summary>
        /// <param name="sender">The as required reason.</param>
        /// <param name="e">Property Changed Event Args.</param>
        private void Reason_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                this.RaisePropertyChanged("Reason");
                this.RaisePropertyChanged("PreviewPrescription");
                this.UpdateIsAuthorizable();
            }
        }
    }
}
