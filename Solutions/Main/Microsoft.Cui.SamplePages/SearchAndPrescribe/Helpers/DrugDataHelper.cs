//-----------------------------------------------------------------------
// <copyright file="DrugDataHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class containing helper functions for working with drug data.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// A class containing helper functions for working with drug data.
    /// </summary>
    public class DrugDataHelper
    {
        /// <summary>
        /// Field for the static instance of this class.
        /// </summary>
        private static DrugDataHelper instance;

        /// <summary>
        /// Stores a list of the commonly prescribed drugs.
        /// </summary>
        private List<Drug> commonDrugs;

        /// <summary>
        /// Stores all of the drugs.
        /// </summary>
        private List<Drug> allDrugs;

        /// <summary>
        /// Stores all of the routes.
        /// </summary>
        private List<Route> allRoutes;

        /// <summary>
        /// Stores all of the frequencies.
        /// </summary>
        private List<Frequency> allFrequencies;

        /// <summary>
        /// Stores all of the administration times.
        /// </summary>
        private List<AdministrationTimes> allAdministrationTimes;

        /// <summary>
        /// Stores the drugs by generic name.
        /// </summary>
        private Dictionary<string, List<Drug>> drugsByGeneric = new Dictionary<string, List<Drug>>();

        /// <summary>
        /// Gets or sets the public accessor for the field instance.
        /// </summary>
        public static DrugDataHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DrugDataHelper();
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Gets all of the administration times.
        /// </summary>
        public static AdministrationTimes[] AllAdministrationTimes
        {
            get
            {
                if (Instance.allAdministrationTimes == null)
                {
                    XDocument prototypeXml = XDocument.Load("SampleData/DrugData.xml");
                    Instance.allAdministrationTimes = (from administrationTimes in prototypeXml.Element("SearchAndPrescribeData").Element("AllAdministrationTimes").Descendants("AdministrationTimes") 
                                                             select new AdministrationTimes(CreateDrugElement(administrationTimes)) { Times = administrationTimes.Attribute("Value") != null ? (from administrationTime in administrationTimes.Attribute("Value").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select new StringToTimeConverter().Convert(administrationTime, typeof(DateTime?), null, CultureInfo.InvariantCulture) as DateTime?).ToArray() : null }).ToList();
                }

                return Instance.allAdministrationTimes.ToArray();
            }
        }

        /// <summary>
        /// Gets all of the routes.
        /// </summary>
        public static Route[] AllRoutes
        {
            get
            {
                if (Instance.allRoutes == null)
                {
                    XDocument prototypeXml = XDocument.Load("SampleData/DrugData.xml");
                    Instance.allRoutes = (from route in prototypeXml.Element("SearchAndPrescribeData").Element("AllRoutes").Descendants("Route") 
                                                select new Route(CreateDrugElement(route)) { MandatorySite = (route.Attribute("MandatorySite") != null) ? bool.Parse(route.Attribute("MandatorySite").Value) : false, MandatoryForm = (route.Attribute("MandatoryForm") != null) ? bool.Parse(route.Attribute("MandatoryForm").Value) : false }).ToList();
                }

                return Instance.allRoutes.ToArray();
            }
        }

        /// <summary>
        /// Gets all of the frequencies.
        /// </summary>
        public static Frequency[] AllFrequencies
        {
            get
            {
                if (Instance.allFrequencies == null)
                {
                    XDocument prototypeXml = XDocument.Load("SampleData/DrugData.xml");
                    Instance.allFrequencies = (from frequency in prototypeXml.Element("SearchAndPrescribeData").Element("AllFrequencies").Descendants("Frequency")
                                                   select new Frequency(CreateDrugElement(frequency)) { AdministrationTimes = (frequency.Attribute("AdministrationTimes") != null) ? new AdministrationTimes() { Value = frequency.Attribute("AdministrationTimes").Value, Times = (from administrationTime in frequency.Attribute("AdministrationTimes").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select(new StringToTimeConverter().Convert(administrationTime, typeof(DateTime?), null, CultureInfo.InvariantCulture) as DateTime?)).ToArray() } : null, HasDuration = frequency.Attribute("Duration") != null, Duration = (frequency.Attribute("Duration") != null) ? (TimeSpan?)(new StringToTimeSpanConverter().Convert(frequency.Attribute("Duration").Value, typeof(TimeSpan?), null, CultureInfo.InvariantCulture)) : TimeSpan.Zero }).ToList();
                }

                return Instance.allFrequencies.ToArray();
            }
        }

        /// <summary>
        /// Gets the commonly prescribed drugs.
        /// </summary>
        public static Drug[] CommonDrugs
        {
            get
            {
                if (Instance.commonDrugs == null)
                {
                    Instance.commonDrugs = (from drug in Instance.AllDrugs
                                   where drug.CommonlyPrescribed
                                   orderby drug.Name
                                   select drug).ToList();

                    if (Instance.commonDrugs.Count > 10)
                    {
                        Instance.commonDrugs.RemoveRange(10, Instance.commonDrugs.Count - 10);
                    }
                }

                return Instance.commonDrugs.ToArray();
            }
        }

        /// <summary>
        /// Gets all of the drugs.
        /// </summary>
        public Drug[] AllDrugs
        {
            get
            {
                if (Instance.allDrugs == null)
                {
                    XDocument prototypeXml = XDocument.Load("SampleData/DrugData.xml");

                    Instance.allDrugs = (from drug in prototypeXml.Element("SearchAndPrescribeData").Element("Drugs").Descendants("Drug")
                                             select new Drug()
                                             {
                                                 Name = drug.Attribute("Name") != null ? PrepareDrugName(drug.Attribute("Name").Value) : string.Empty,
                                                 Description = drug.Attribute("Description") != null ? PrepareDrugName(drug.Attribute("Description").Value) : string.Empty,
                                                 CommonlyPrescribed = drug.Attribute("CommonlyPrescribed") != null ? bool.Parse(drug.Attribute("CommonlyPrescribed").Value) : false,
                                                 BrandName = drug.Attribute("BrandName") != null ? drug.Attribute("BrandName").Value : string.Empty,
                                                 Ingredients = drug.Element("Ingredients") != null ? (from ingredient in drug.Element("Ingredients").Descendants("Ingredient") select new ElementValue() { Value = (ingredient.Attribute("Value") != null) ? ingredient.Attribute("Value").Value : string.Empty }).ToArray() : null,
                                                 Routes = drug.Element("Routes") != null ? ((from route in drug.Element("Routes").Descendants("Route")
                                                                                               select new Route(CreateDrugElement(route))
                                                                                               {
                                                                                                   DrugName = (drug.Attribute("Name") != null) ? drug.Attribute("Name").Value : string.Empty,
                                                                                                   BrandName = (route.Attribute("BrandName") != null) ? route.Attribute("BrandName").Value : string.Empty,
                                                                                                   DrugBrandName = drug.Attribute("BrandName") != null ? drug.Attribute("BrandName").Value : string.Empty,
                                                                                                   Form = (route.Attribute("Form") != null) ? route.Attribute("Form").Value : null,
                                                                                                   Modifier = (route.Attribute("Modifier") != null) ? route.Attribute("Modifier").Value : null,
                                                                                                   IsABrandGeneric = IsABrandGeneric(drug.Element("Routes").Descendants("Route")),
                                                                                                   MandatoryBrandName = (route.Attribute("MandatoryBrandName") != null) ? bool.Parse(route.Attribute("MandatoryBrandName").Value) : false,
                                                                                                   MandatoryForm = (route.Attribute("MandatoryForm") != null) ? bool.Parse(route.Attribute("MandatoryForm").Value) : false,
                                                                                                   MandatorySite = (route.Attribute("MandatorySite") != null) ? bool.Parse(route.Attribute("MandatorySite").Value) : false,
                                                                                                   AllowQuickPrescription = (route.Attribute("AllowQuickPrescription") != null) ? bool.Parse(route.Attribute("AllowQuickPrescription").Value) : false,
                                                                                               }).ToArray()) : null,
                                                 Brands = (drug.Element("Brands") != null) ? ((from brand in drug.Element("Brands").Descendants("Brand")
                                                                                               select CreateDrugElement(brand)).ToArray()) : null,
                                                 Strengths = (drug.Element("Strengths") != null) ? ((from strength in drug.Element("Strengths").Descendants("Strength")
                                                                                                     select new Strength(CreateDrugElement(strength))).ToArray()) : null,
                                                 Forms = (drug.Element("Forms") != null) ? ((from form in drug.Element("Forms").Descendants("Form")
                                                                                             select new Form(CreateDrugElement(form))
                                                                                             {
                                                                                                 MandatoryStrength = (form.Attribute("MandatoryStrength") != null) ? bool.Parse(form.Attribute("MandatoryStrength").Value) : false,
                                                                                             }).ToArray()) : null,
                                                 Dosages = (drug.Element("Dosages") != null) ? ((from dose in drug.Element("Dosages").Descendants("Dose")
                                                                                                 select new Dose(CreateDrugElement(dose))
                                                                                                 {
                                                                                                     AllowAsRequired = (dose.Attribute("AllowAsRequired") != null) ? bool.Parse(dose.Attribute("AllowAsRequired").Value) : false
                                                                                                 }).ToArray()) : null,
                                                 Frequencies = (drug.Element("Frequencies") != null) ? ((from frequency in drug.Element("Frequencies").Descendants("Frequency")
                                                                                                         select new Frequency(CreateDrugElement(frequency))
                                                                                                         {
                                                                                                             AdministrationTimes = (frequency.Attribute("AdministrationTimes") != null) ? new AdministrationTimes()
                                                                                                             {
                                                                                                                 Value = frequency.Attribute("AdministrationTimes").Value,
                                                                                                                 Times = (from administrationTime in frequency.Attribute("AdministrationTimes").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select(new StringToTimeConverter().Convert(administrationTime, typeof(DateTime?), null, CultureInfo.InvariantCulture) as DateTime?)).ToArray()
                                                                                                             } : null,
                                                                                                             HasDuration = frequency.Attribute("Duration") != null,
                                                                                                             Duration = (frequency.Attribute("Duration") != null) ? (TimeSpan?)(new StringToTimeSpanConverter().Convert(frequency.Attribute("Duration").Value, typeof(TimeSpan?), null, CultureInfo.InvariantCulture)) : TimeSpan.Zero,
                                                                                                             AsRequired = (frequency.Attribute("AsRequired") != null) ? bool.Parse(frequency.Attribute("AsRequired").Value) : false,
                                                                                                         }).ToArray()) : null,
                                                                                                         AllFrequencies = AllFrequencies,
                                                                                                         AdministrationTimes = AllAdministrationTimes,
                                                 AsRequiredReasons = (drug.Element("AsRequiredReasons") != null) ? ((from asRequiredReason in drug.Element("AsRequiredReasons").Descendants("AsRequiredReason")
                                                                                                                     select new DrugElement()
                                                                                                                     {
                                                                                                                         Value = (asRequiredReason.Attribute("Value") != null) ? asRequiredReason.Attribute("Value").Value : string.Empty,
                                                                                                                     }).ToArray()) : null,
                                                 TemplatePrescriptions = (drug.Element("TemplatePrescriptions") != null) ? ((from prescriptionDetail in drug.Element("TemplatePrescriptions").Descendants("TemplatePrescription")
                                                                                                                             select new TemplatePrescription()
                                                                                                                             {
                                                                                                                                 Route = (prescriptionDetail.Attribute("Route") != null) ? new Route()
                                                                                                                                 {
                                                                                                                                     DrugName = (drug.Attribute("Name") != null) ? drug.Attribute("Name").Value : string.Empty,
                                                                                                                                     Value = (prescriptionDetail.Attribute("Route") != null) ? prescriptionDetail.Attribute("Route").Value : null,
                                                                                                                                     Form = (prescriptionDetail.Attribute("Form") != null) ? prescriptionDetail.Attribute("Form").Value : null,
                                                                                                                                 } : null,
                                                                                                                                 BrandName = (prescriptionDetail.Attribute("BrandName") != null) ? prescriptionDetail.Attribute("BrandName").Value : string.Empty,
                                                                                                                                 Strength = (prescriptionDetail.Attribute("Strength") != null) ? prescriptionDetail.Attribute("Strength").Value : string.Empty,
                                                                                                                                 Dose = (prescriptionDetail.Attribute("Dose") != null) ? prescriptionDetail.Attribute("Dose").Value : string.Empty,
                                                                                                                                 AsRequired = (prescriptionDetail.Attribute("AsRequired") != null) ? bool.Parse(prescriptionDetail.Attribute("AsRequired").Value) : false,
                                                                                                                                 Frequency = (prescriptionDetail.Attribute("Frequency") != null) ? new Frequency()
                                                                                                                                 {
                                                                                                                                     Value = prescriptionDetail.Attribute("Frequency").Value,
                                                                                                                                     AsRequired = (prescriptionDetail.Attribute("AsRequired") != null) ? bool.Parse(prescriptionDetail.Attribute("AsRequired").Value) : false,
                                                                                                                                 } : null,
                                                                                                                                 Duration = (prescriptionDetail.Attribute("Duration") != null) ? (TimeSpan?)(new StringToTimeSpanConverter().Convert(prescriptionDetail.Attribute("Duration").Value, typeof(TimeSpan?), null, CultureInfo.InvariantCulture)) : TimeSpan.Zero,
                                                                                                                                 ShowDuration = (prescriptionDetail.Attribute("ShowDuration") != null) ? bool.Parse(prescriptionDetail.Attribute("ShowDuration").Value) : false,
                                                                                                                                 AdministrationTimes = (prescriptionDetail.Attribute("AdministrationTimes") != null) ? new AdministrationTimes()
                                                                                                                                 {
                                                                                                                                     Value = prescriptionDetail.Attribute("AdministrationTimes").Value,
                                                                                                                                     Times = (from administrationTime in prescriptionDetail.Attribute("AdministrationTimes").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select(new StringToTimeConverter().Convert(administrationTime, typeof(DateTime?), null, CultureInfo.InvariantCulture) as DateTime?)).ToArray(),
                                                                                                                                 } : null,
                                                                                                                                 Description = (prescriptionDetail.Attribute("Description") != null) ? prescriptionDetail.Attribute("Description").Value : string.Empty,
                                                                                                                             }).ToArray()) : null
                                             }).ToList();

                    foreach (Drug drug in allDrugs)
                    {
                        if (!string.IsNullOrEmpty(drug.Name))
                        {
                            if (!drugsByGeneric.ContainsKey(drug.Name))
                            {
                                drugsByGeneric.Add(drug.Name, new List<Drug>() { drug });
                            }
                            else
                            {
                                drugsByGeneric[drug.Name].Add(drug);
                            }
                        }
                    }

                    foreach (Drug drug in allDrugs)
                    {
                        if (!string.IsNullOrEmpty(drug.Name) && drugsByGeneric.ContainsKey(drug.Name))
                        {
                            drug.BrandedDrugs = drugsByGeneric[drug.Name].ToArray();
                        }
                    }
                }

                return Instance.allDrugs.ToArray();
            }
        }

        /// <summary>
        /// Prepares the drug name.
        /// </summary>
        /// <param name="name">The name to prepare.</param>
        /// <returns>A prepared drug name.</returns>
        private static string PrepareDrugName(string name)
        {
            string[] drugNameParts = name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (drugNameParts.Length > 0)
            {
                string drugName = string.Empty;
                foreach (string namePart in drugNameParts)
                {
                    drugName += namePart.Trim() + " + ";
                }

                return drugName.Remove(drugName.Length - 3, 3);
            }

            return name.Trim();
        }

        /// <summary>
        /// Checks if a drug is a brand generic.
        /// </summary>
        /// <param name="elements">The drug elements.</param>
        /// <returns>Whether the drug is a brand generic.</returns>
        private static bool IsABrandGeneric(IEnumerable<XElement> elements)
        {
            XElement[] brandItems = (from element in elements
                                     where element.Attribute("BrandName") != null
                                     select element).ToArray();

            return brandItems.Length > 0;
        }

        /// <summary>
        /// Creates a drug attribute from an XElement.
        /// </summary>
        /// <param name="element">The element to create the DrugElement from.</param>
        /// <returns>A drug attribute.</returns>
        private static DrugElement CreateDrugElement(XElement element)
        {
            if (element != null)
            {
                return new DrugElement()
                {
                    Value = element.Attribute("Value") != null ? element.Attribute("Value").Value : string.Empty,
                    ValidBrands = element.Attribute("ValidBrands") != null ? (from validBrand in element.Attribute("ValidBrands").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validBrand).ToArray() : null,
                    ValidRoutes = element.Attribute("ValidRoutes") != null ? (from validRoute in element.Attribute("ValidRoutes").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validRoute).ToArray() : null,
                    ValidForms = element.Attribute("ValidForms") != null ? (from validForm in element.Attribute("ValidForms").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validForm).ToArray() : null,
                    ValidStrengths = element.Attribute("ValidStrengths") != null ? (from validStrength in element.Attribute("ValidStrengths").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validStrength).ToArray() : null,
                    ValidDosages = element.Attribute("ValidDosages") != null ? (from validDose in element.Attribute("ValidDosages").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validDose).ToArray() : null,
                    ValidVolumes = element.Attribute("ValidVolumes") != null ? (from validVolume in element.Attribute("ValidVolumes").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validVolume).ToArray() : null,
                    ValidDoseDurations = element.Attribute("ValidDoseDurations") != null ? (from validDoseDuration in element.Attribute("ValidDoseDurations").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validDoseDuration).ToArray() : null,
                    ValidFrequencies = element.Attribute("ValidFrequencies") != null ? (from validFrequency in element.Attribute("ValidFrequencies").Value.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries) select validFrequency).ToArray() : null,
                    Description = element.Attribute("Description") != null ? element.Attribute("Description").Value : string.Empty,
                };
            }

            return null;
        }
    }
}
