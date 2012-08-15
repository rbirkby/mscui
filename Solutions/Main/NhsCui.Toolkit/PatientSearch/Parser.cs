//-----------------------------------------------------------------------
// <copyright file="Parser.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Jan-2007</date>
// <summary>Parser class - non-UI implementation of parsing for search criteria</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.PatientSearch
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NhsCui.Toolkit.DateAndTime;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Resources;
    using System.Collections;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The class which implements PatientSearch parsing.
    /// </summary>
    [Serializable]
    public class Parser
    {
        #region Member Vars

        #region Module-level Regular Expressions

        /// <summary>
        /// Regex constant for Year matching
        /// </summary>
        private const string YearRegexPattern = @"\b\d\d\d\d\b";

        /// <summary>
        /// Regex constant for Day Month(int) Year digit matching
        /// </summary>
        private const string DayIntMonthYearRegexPattern = @"(\d\d?)[/\-.]\d\d?[/\-.]\d\d\d\d";

        /// <summary>
        /// Regex constant for Day Month(name) Year digit matching
        /// </summary>
        private const string DayTextMonthYearRegexPattern = @"(\d\d?)[/\-.\s]([A-Z]{3,9})[/\-.\s]\d\d\d\d";

        /// <summary>
        /// Regex constant for Month(int) or Month(name) Year digit matching
        /// </summary>
        private const string IntOrTextMonthYearRegexPattern = @"((\b01|\b02|\b03|\b04|\b05|\b06|\b07|\b08|\b09|\b1|\b2|\b3|\b4|\b5|\b6|\b7|\b8|\b9|\b10\b|\b11|\b12)|([A-Z]{3,9}))[/\-.]\d\d\d\d";
        #endregion

        /// <summary>
        /// Safe token to encode delimited text
        /// </summary>
        private const char SafeToken = '�';

        /// <summary>
        /// Day as number then Month and Year RegEx
        /// </summary>
        private static readonly Regex dayIntMonthYearRegex = new Regex(DayIntMonthYearRegexPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        
        /// <summary>
        /// Day as text then Month and Year RegEx
        /// </summary>
        private static readonly Regex dayTextMonthYearRegex = new Regex(DayTextMonthYearRegexPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        
        /// <summary>
        /// Year RegEx
        /// </summary>
        private static readonly Regex yearRegex = new Regex(YearRegexPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        
        /// <summary>
        /// Month and Year RegEx
        /// </summary>
        private static readonly Regex intOrTextMonthYearRegex = new Regex(IntOrTextMonthYearRegexPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        /// <summary>
        /// Age RegEx
        /// </summary>
        private static readonly Regex ageRegex = new Regex(@"\b\d\d?\d?\b", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        
        /// <summary>
        /// Address RegEx
        /// </summary>
        private static readonly Regex addressRegex = new Regex(@"\d\d?\d?\d?(\s\D+)+", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        /// <summary>
        /// Embedded Address RegEx
        /// </summary>
        private static readonly Regex EmbeddedAddressRegex = new Regex(@"\d\d?\d?\d?(" + Parser.SafeToken + @"[A-Z]+)+", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        /// <summary>
        /// Age Range RegEx
        /// </summary>
        private static readonly Regex ageRangeRegex = new Regex(@"(\b\d\d?\b)(\-(\b\d\d?\d?)\b)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// NHS Number RegEx
        /// </summary>
        private static readonly Regex NhsNumberRegex = new Regex(@"(\b\d\d\d[\s\-]?\d\d\d[\s\-]?\d\d\d\d)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// Postcode RegEx
        /// </summary>
        private static readonly Regex PostcodeRegex = new Regex(@"(([A-Z][A-Z]?\d\d?)|([A-Z][A-Z]?\d[A-Z]?))(\s?\d[A-Z][A-Z])", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        /// <summary>
        /// Family name RegEx
        /// </summary>
        private static readonly Regex FamilyNameRegex = new Regex(@"([A-Z\-\'" + Parser.SafeToken + @"]+)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        /// <summary>
        /// Given Name and Familiy Name RegEx
        /// </summary>
        private static readonly Regex GivenNameFamilyNameRegex = new Regex(@"([A-Z\-\'" + Parser.SafeToken + @"]+)\s([A-Z\-\'" + Parser.SafeToken + "]+)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        
        /// <summary>
        /// The address parsed from the Text property.
        /// </summary>
        private string address;

        /// <summary>
        /// The age parsed from the Text property.
        /// </summary>
        private int age = -1;

        /// <summary>
        /// The upper value in an age range.
        /// </summary>
        private int ageUpper = -1;

        /// <summary>
        /// A list of common family names.
        /// </summary>
        private List<string> commonFamilyNames;

        /// <summary>
        /// The date of birth parsed from the Text property.
        /// </summary>
        private NhsDate dateOfBirth;

        /// <summary>
        /// The upper value in a date of birth range.
        /// </summary>
        private NhsDate dateOfBirthUpper;

        /// <summary>
        /// The character that is used to delimit the end of a group of words.
        /// </summary>
        private char endGroupDelimiter = '"';

        /// <summary>
        /// The family name parsed from the Text property.
        /// </summary>
        private string familyName;

        /// <summary>
        /// The gender parsed from the Text property.
        /// </summary>
        private Gender gender = Gender.None;

        /// <summary>
        /// The given name parsed from the Text property.
        /// </summary>
        private string givenName;

        /// <summary>
        /// The character that is used to delimit the end of a group of words.
        /// </summary>
        private char informationDelimiter = ',';

        /// <summary>
        /// The list of Information enumeration values that are used to parse the Text property.
        /// </summary>
        private List<Information> informationFormat;

        /// <summary>
        /// Does the FamilyName appear in the common family names list?
        /// </summary>
        private bool familyNameIsCommon;

        /// <summary>
        /// The list of Information enumeration values that are mandatory.
        /// </summary>
        private List<Information> mandatoryInformation;

        /// <summary>
        /// The maximum age recognised by the parser.
        /// </summary>
        private int maximumAge = 130;

        /// <summary>
        /// The NHS number parsed from the Text property.
        /// </summary>
        private string nhsNumber;

        /// <summary>
        /// The postcode parsed from the Text property.
        /// </summary>
        private string postcode;

        /// <summary>
        /// The character that is used to delimit the start of a group of words.
        /// </summary>
        private char startGroupDelimiter = '"';

        /// <summary>
        /// The patient search criteria to parse.
        /// </summary>
        private string text;

        /// <summary>
        /// The title parsed from the Text property.
        /// </summary>
        private string title;

        /// <summary>
        /// A list of Title objects.
        /// </summary>
        private List<Title> titles;

        /// <summary>
        /// A list of common thoroughfares.
        /// </summary>
        private List<string> thoroughfares;

        /// <summary>
        /// The remaining text that could not be matched after the parsing process has identified all other values.
        /// </summary>
        private string unmatchedText;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a Parser object.
        /// </summary>
        public Parser()
        {
            this.ResetTitles();
            this.ResetCommonFamilyNames();
            this.ResetAllProperties();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The address.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null. 
        /// </remarks>
        public string Address
        {
            get
            {
                return this.address;
            }

            private set
            {
                this.address = Parser.TokenDecode(value);
            }
        }

        /// <summary>
        /// The age.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to -1. Used for both an exact number of years and as the lower value in an age range. 
        /// </remarks>
        public int Age
        {
            get
            {
                return this.age;
            }

            private set
            {
                this.age = value;
            }
        }

        /// <summary>
        /// The upper value in an age range. 
        /// </summary>
        /// <remarks>
        ///  Read-only and defaults to -1. 
        /// </remarks>
        public int AgeUpper
        {
            get
            {
                return this.ageUpper;
            }

            private set
            {
                this.ageUpper = value;
            }
        }

        /// <summary>
        /// A list of common family names. 
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        public List<string> CommonFamilyNames
        {
            get
            {
                if (this.commonFamilyNames == null)
                {
                    this.ResetCommonFamilyNames();
                }

                return this.commonFamilyNames;
            }

            set
            {
                this.commonFamilyNames = value;
            }
        }

        /// <summary>
        /// The date of birth.
        /// </summary>
        /// <remarks>
        ///  Read-only and defaults to an NhsDate of DateType.Null. Used for both an exact date of birth and as the lower value in a date of birth range. 
        /// </remarks>
        public NhsDate DateOfBirth
        {
            get
            {
                return this.dateOfBirth;
            }

            private set
            {
                this.dateOfBirth = value;
            }
        }

        /// <summary>
        /// The upper value in a date of birth range. 
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to an NhsDate of DateType.Null. Used as the upper value in a date of birth range.
        /// </remarks>
        public NhsDate DateOfBirthUpper
        {
            get
            {
                return this.dateOfBirthUpper;
            }

            private set
            {
                this.dateOfBirthUpper = value;
            }
        }

        /// <summary>
        /// The character that is used to delimit the end of a group of words. 
        /// </summary>
        /// <remarks>
        /// Defaults to a double quote mark ("). Throws an ArgumentException if the value is a space, a dash (-), a forward slash (/) or is the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.InformationDelimiter">InformationDelimiter</see>. 
        /// </remarks>
        public char EndGroupDelimiter
        {
            get
            {
                return this.endGroupDelimiter;
            }

            set
            {
                if (value == ' ' | value == '/' | value == '-' | value == this.InformationDelimiter)
                {
                    throw new ArgumentException(PatientSearch.Resources.ParserResources.EndGroupDelimiterExceptionMessage);
                }
                else
                {
                    this.endGroupDelimiter = value;
                }
            }
        }

        /// <summary>
        /// The family name.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null. 
        /// </remarks>
        public string FamilyName
        {
            get
            {
                return this.familyName;
            }

            private set
            {
                this.familyName = Parser.TokenDecode(value);
                this.UpdateIsCommonFamilyName();
            }
        }

        /// <summary>
        /// The gender.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to PatientSearch.Gender.None. 
        /// </remarks>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            private set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// The given name.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null. 
        /// </remarks>
        public string GivenName
        {
            get
            {
                return this.givenName;
            }

            private set
            {
                this.givenName = Parser.TokenDecode(value);
            }
        }

        /// <summary>
        /// The character that is used to delimit a structured list of words. 
        /// </summary>
        /// <remarks>
        /// Defaults to a comma (,). Throws an ArgumentException if the value is a space, a dash (-), a forward slash (/), the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.EndGroupDelimiter">EndGroupDelimiter</see> or the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.StartGroupDelimiter">StartGroupDelimiter</see>. 
        /// </remarks>
        public char InformationDelimiter
        {
            get
            {
                return this.informationDelimiter;
            }

            set
            {
                if (value == ' ' | value == '/' | value == '-' | value == this.StartGroupDelimiter | value == this.EndGroupDelimiter)
                {
                    throw new ArgumentException(PatientSearch.Resources.ParserResources.InformationDelimiterExceptionMessage);
                }
                else
                {
                    this.informationDelimiter = value;
                }
            }
        }

        /// <summary>
        /// A list containing the PatientSearch.Information enumeration values.
        /// </summary>
        /// <remarks>
        /// Defaults to null. 
        /// </remarks>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        public List<Information> InformationFormat
        {
            get
            {
                return this.informationFormat;
            }

            set
            {
                this.informationFormat = value;
            }
        }

        /// <summary>
        /// Determines whether a <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.FamilyName">FamilyName</see> is included in the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.CommonFamilyNames">CommonFamilyNames</see>.
        /// </summary>
        /// <returns>
        /// True if found in the <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.CommonFamilyNames">CommonFamilyNames</see>; otherwise false. 
        /// </returns>
        /// <remarks>
        /// Read-only and defaults to false. 
        /// </remarks>
        public bool IsCommonFamilyName
        {
            get
            {
                return this.familyNameIsCommon;
            }
        }

        /// <summary>
        /// Determines whether the entered values for <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.DateOfBirth">DateOfBirth</see> 
        /// and <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Age">Age</see> are consistent. 
        /// </summary>
        /// <returns>
        /// True if both <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.DateOfBirth">DateOfBirth</see> 
        /// and <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Age">Age</see> have been entered and 
        /// the <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Age">Age</see> does not match the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.DateOfBirth">DateOfBirth</see> based on the current date; otherwise false. 
        /// </returns>
        ///<remarks>
        /// Read-only.
        ///</remarks>
        public bool IsDateOfBirthAgeMismatch
        {
            get
            {
                if (this.dateOfBirth.DateValue != null && this.age > -1 && this.age != (DateTime.Now.Year - this.dateOfBirth.Year))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Determines whether the entered <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Gender">Gender</see> 
        /// and <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Title">Title</see> are consistent. 
        /// </summary>
        /// <returns>
        /// True if both <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Gender">Gender</see> 
        /// and <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Title">Title</see> have been entered and 
        /// the <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Gender">Gender</see> does not match the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Title">Title</see>; otherwise false. 
        /// </returns>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        public bool IsGenderTitleMismatch
        {
            get
            {
                if (this.Gender != Gender.None && this.Title != null)
                {
                    Title checkTitle = this.CheckForTitle(new StringBuilder(this.Title));
                    if (checkTitle != null && this.Gender != checkTitle.Gender)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Determines whether the information entered is consistent with the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.MandatoryInformation">MandatoryInformation</see> property. 
        /// </summary>
        /// <returns>
        /// True if all the values in <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.MandatoryInformation">MandatoryInformation</see>
        /// are not their default values; false if <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.MandatoryInformation">MandatoryInformation</see> 
        /// is null. 
        /// </returns>
        public bool IsMandatoryInformationEntered
        {
            get
            {
                if (this.MandatoryInformation != null && this.MandatoryInformation.Count > 0)
                {
                    return this.AllMandatoryInformationEntered();
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// The list of mandatory PatientSearch.Information enumeration values. 
        /// </summary>
        /// <remarks>
        /// Defaults to null. 
        /// </remarks>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        public List<Information> MandatoryInformation
        {
            get
            {
                return this.mandatoryInformation;
            }

            set
            {
                this.mandatoryInformation = value;
            }
        }

        /// <summary>
        /// The maximum age recognised by the parser. 
        /// </summary>
        /// <remarks>
        /// Defaults to 130. 
        /// </remarks>
        public int MaximumAge
        {
            get
            {
                return this.maximumAge;
            }

            set
            {
                this.maximumAge = value;
            }
        }

        /// <summary>
        /// The NHS number.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null. 
        /// </remarks>
        public string NhsNumber
        {
            get
            {
                return this.nhsNumber;
            }

            private set
            {
                this.nhsNumber = Parser.TokenDecode(value);
            }
        }

        /// <summary>
        /// The postcode.
        /// </summary>
        /// /// <remarks>
        /// Read-only and defaults to null. 
        /// </remarks>
        public string Postcode
        {
            get
            {
                return this.postcode;
            }

            private set
            {
                this.postcode = Parser.TokenDecode(value);
            }
        }

        /// <summary>
        /// The character that is used to delimit the start of a group of words
        /// </summary>
        /// <remarks>
        /// Defaults to a double quote mark ("). Throws an ArgumentException if the value is a space, a dash (-), a forward slash (/) or is the 
        /// <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.InformationDelimiter">InformationDelimiter</see>. 
        /// </remarks>
        public char StartGroupDelimiter
        {
            get
            {
                return this.startGroupDelimiter;
            }

            set
            {
                if (value == ' ' | value == '/' | value == '-' | value == this.InformationDelimiter)
                {
                    throw new ArgumentException(PatientSearch.Resources.ParserResources.StartGroupDelimiterExceptionMessage);
                }
                else
                {
                    this.startGroupDelimiter = value;
                }
            }
        }

        /// <summary>
        /// The patient search criteria to parse. 
        /// </summary>
        /// <remarks>
        /// Defaults to null. 
        /// </remarks>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        /// <summary>
        /// The title.
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null. Identified by a match in the <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Titles">Titles
        /// </see> property. 
        /// </remarks>
        public string Title
        {
            get
            {
                return this.title;
            }

            private set
            {
                this.title = Parser.TokenDecode(value);
            }
        }

        /// <summary>
        /// A list of Title objects. 
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        public List<Title> Titles
        {
            get
            {
                if (this.titles == null)
                {
                    this.ResetTitles();
                }

                return this.titles;
            }

            set
            {
                this.titles = value;
            }
        }

        /// <summary>
        /// Any remaining text that could not be matched after the parsing process has identified all other values. 
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null. 
        /// </remarks>
        public string UnmatchedText
        {
            get
            {
                return this.unmatchedText;
            }

            private set
            {
                this.unmatchedText = Parser.TokenDecode(value).TrimStart();
            }
        }

        /// <summary>
        /// The internal list of recognised thoroughfares - create on demand/first use from resources
        /// </summary>
        private List<string> Thoroughfares
        {
            get
            {
                if (this.thoroughfares == null)
                {
                    this.thoroughfares = (List<string>)Parser.GetCommonThoroughfares();
                }

                return this.thoroughfares;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the string in the <see cref="P:NhsCui.Toolkit.PatientSearch.Parser.Text">Text</see> property and populates the 
        /// equivalent properties with the results. 
        /// </summary>
        public void Parse()
        {
            if (this.Text == null)
            {
                return;
            }

            if (this.InformationFormat != null && this.InformationFormat.Count > 0)
            {
                this.ParseSplit();
            }
            else
            {
                this.ParseRegex();
            }
        }

        /// <summary>
        /// Retrieve a list of common family names from resources
        /// </summary>
        /// <returns>Enumerable list of Common Family Names</returns>
        private static IEnumerable<string> GetCommonFamilyNames()
        {
            List<string> familyNames = new List<string>();
            ResourceManager resourceManager = new ResourceManager("NhsCui.Toolkit.PatientSearch.Resources.CommonFamilyNamesResources", typeof(Parser).Assembly);

            // seem to need to prime the pump before enumerating so explicitly access the first one...
            resourceManager.GetString("String1");

            IDictionaryEnumerator resourceEnumerator = resourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, false, true).GetEnumerator();
            resourceEnumerator.Reset();

            while (resourceEnumerator.MoveNext())
            {
                familyNames.Add(resourceEnumerator.Value.ToString());
            }

            return familyNames;
        }

        /// <summary>
        /// Retrieve a list of common thoroughfares from resources
        /// </summary>
        /// <returns>Enumerable list of Common Thoroughfares</returns>
        private static IEnumerable<string> GetCommonThoroughfares()
        {
            List<string> thoroughfares = new List<string>();
            ResourceManager resourceManager = new ResourceManager("NhsCui.Toolkit.PatientSearch.Resources.CommonThoroughfaresResources", typeof(Parser).Assembly);

            // seem to need to prime the pump before enumerating so explicitly access the first one...
            resourceManager.GetString("String1");

            IDictionaryEnumerator resourceEnumerator = resourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, false, true).GetEnumerator();
            resourceEnumerator.Reset();

            while (resourceEnumerator.MoveNext())
            {
                thoroughfares.Add(resourceEnumerator.Value.ToString());
            }

            return thoroughfares;
        }

        /// <summary>
        /// Retrieve a list of titles from resources
        /// </summary>
        /// <returns>Enumerable list of Titles</returns>
        private static IEnumerable<Title> GetTitles()
        {
            List<Title> titles = new List<Title>();
            ResourceManager resourceManager = new ResourceManager("NhsCui.Toolkit.PatientSearch.Resources.TitlesResources", typeof(Parser).Assembly);

            // seem to need to prime the pump before enumerating so explicitly access the first one...
            resourceManager.GetString("Title01");

            IDictionaryEnumerator resourceEnumerator = resourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, false, true).GetEnumerator();
            resourceEnumerator.Reset();

            while (resourceEnumerator.MoveNext())
            {
                string[] titleInfo = resourceEnumerator.Value.ToString().Split(',');
                titles.Add(new Title(titleInfo[0], (Gender)(Enum.Parse(typeof(Gender), titleInfo[1]))));
            }

            return titles;
        }

        /// <summary>
        /// Gets the month number from the month name.
        /// </summary>
        /// <param name="monthName">The name of the month. For example, if the month number is 3, the monthName is "March".</param>
        /// <param name="cultureInfo">The culture that should be used to parse the string.</param>
        /// <returns>The Month number</returns>
        private static int GetMonthNumberFromMonthName(string monthName, CultureInfo cultureInfo)
        {
            int index = Parser.FindCaseInsensitiveEntry(cultureInfo.DateTimeFormat.MonthNames, monthName, cultureInfo);
            return (index >= 0 ? index + 1 : -1);
        }

        /// <summary>
        /// Find entry an entry in supplied string array by case insensitive match
        /// </summary>
        /// <param name="values">values to search</param>
        /// <param name="item">item to search for</param>
        /// <param name="cultureInfo">culture to use for comparisons</param>
        /// <returns>index in the array of the item ,or -1 if not found</returns>
        private static int FindCaseInsensitiveEntry(string[] values, string item, CultureInfo cultureInfo)
        {
            int compareLength;

            for (int index = 0; index < values.Length; index++)
            {
                compareLength = item.Length <= values[index].Length ? item.Length : values[index].Length;
                if (string.Compare(values[index].Substring(0, compareLength), item, true, cultureInfo) == 0)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// Strip out allowable date formatters that are incompatible with NhsDate
        /// </summary>
        /// <param name="dateString">String containing date</param>
        /// <returns>Clean date string</returns>
        private static string CleanDateString(string dateString)
        {
            return dateString.Replace('-', ' ').Replace('/', ' ');
        }

        /// <summary>
        /// Look through string for one of four variants of a date:
        /// 1) Day Month(numeric) Year
        /// 2) Day Month(text) Year
        /// 3) Day Month(numeric) OR Month(text) Year
        /// 4) Year only
        /// </summary>
        /// <param name="searchText">string to search</param>
        /// <returns>FoundText instance if located, otherwise null</returns>
        private static FoundText CheckForDate(string searchText)
        {
            FoundText foundDate = Parser.CheckForDayIntMonthYear(searchText);

            // Keep checking if necessary...
            if (foundDate == null)
            {
                foundDate = Parser.CheckForDayTextMonthYear(searchText);
            }

            // Keep checking if necessary...
            if (foundDate == null)
            {
                foundDate = Parser.CheckForIntOrTextMonthYear(searchText);
            }

            // Keep checking if necessary...
            if (foundDate == null)
            {
                foundDate = Parser.CheckForYear(searchText);
            }

            return foundDate;
        }

        /// <summary>
        /// Look through string for Day Month(numeric) Year variant of a date:
        /// </summary>
        /// <param name="searchText">string to search</param>
        /// <returns>FoundText instance if located, otherwise null</returns>
        private static FoundText CheckForDayIntMonthYear(string searchText)
        {
            FoundText foundDate = Parser.ParseForDate(searchText, dayIntMonthYearRegex);
            if (foundDate != null)
            {
                foundDate.Type = FoundDataType.DateDayIntMonthYear;
            }

            return foundDate;
        }

        /// <summary>
        /// Look through string for Day Month(text) Year variant of a date:
        /// </summary>
        /// <param name="searchText">string to search</param>
        /// <returns>FoundText instance if located, otherwise null</returns>
        private static FoundText CheckForDayTextMonthYear(string searchText)
        {
            FoundText foundDate = Parser.ParseForDate(searchText, dayTextMonthYearRegex);
            if (foundDate != null)
            {
                foundDate.Type = FoundDataType.DateDayTextMonthYear;
            }

            return foundDate;
        }

        /// <summary>
        /// Look through string for Day Month(numeric) OR Month(text) Year variant of a date:
        /// </summary>
        /// <param name="searchText">string to search</param>
        /// <returns>FoundText instance if located, otherwise null</returns>
        private static FoundText CheckForIntOrTextMonthYear(string searchText)
        {
            FoundText foundDate = Parser.ParseForDate(searchText, intOrTextMonthYearRegex);
            if (foundDate != null)
            {
                // Simple way - but is it the best??
                if (foundDate.Value.Trim().Length == 7)
                {
                    foundDate.Type = FoundDataType.DateIntMonthYear;
                }
                else
                {
                    foundDate.Type = FoundDataType.DateTextMonthYear;
                }
            }

            return foundDate;
        }

        /// <summary>
        /// Look through string for Year only variant of a date:
        /// </summary>
        /// <param name="searchText">string to search</param>
        /// <returns>FoundText instance if located, otherwise null</returns>
        private static FoundText CheckForYear(string searchText)
        {
            FoundText foundDate = Parser.ParseForDate(searchText, yearRegex);
            if (foundDate != null)
            {
                foundDate.Type = FoundDataType.DateYear;
            }

            return foundDate;
        }

        /// <summary>
        /// Apply a Regex on a word boundary, start or end of line and single word input.
        /// Remove match from workingText if found
        /// </summary>
        /// <param name="workingText">Text to search</param>
        /// <param name="wordRegex">Regex to use</param>
        /// <returns>If found - text of match, otherwise string.Empty</returns>
        private static string ParseRemoveWord(ref StringBuilder workingText, Regex wordRegex)
        {
            Regex nonWordRegex = new Regex(@"[,;!?:\s\-]");
            Match wordMatch = wordRegex.Match(workingText.ToString());
            string textValue = workingText.ToString();

            if (wordMatch.Success == true)
            {
                int leftBound = wordMatch.Index - 1 >= 0 ? wordMatch.Index - 1 : wordMatch.Index;
                int rightBound = wordMatch.Index + wordMatch.Length <= textValue.Length ? wordMatch.Index + wordMatch.Length : textValue.Length;

                // Whole string...
                if (textValue.Length == wordMatch.Length)
                {
                    workingText.Remove(wordMatch.Index, wordMatch.Length);
                    return wordMatch.Value;
                }

                // At beginning...
                else if (wordMatch.Index == 0 && nonWordRegex.IsMatch(textValue[wordMatch.Length].ToString()) == true)
                {
                    workingText.Remove(wordMatch.Index, wordMatch.Length + 1);
                    return wordMatch.Value;
                }

                // At end...
                else if (wordMatch.Index == textValue.Length - wordMatch.Value.Length && nonWordRegex.IsMatch(textValue[wordMatch.Index - 1].ToString()) == true)
                {
                    workingText.Remove(wordMatch.Index - 1, wordMatch.Length + 1);
                    return wordMatch.Value;
                }

                // Embedded...
                else if (nonWordRegex.IsMatch(textValue[leftBound].ToString()) == true && nonWordRegex.IsMatch(textValue[rightBound].ToString()) == true)
                {
                    workingText.Remove(wordMatch.Index - 1, wordMatch.Length + 1);
                    return wordMatch.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Apply a Regex on a word boundary, start or end of line and single word input
        /// If found return FoundText struct with match details
        /// </summary>
        /// <param name="searchText">Text to search</param>
        /// <param name="wordRegex">Regex to use</param>
        /// <returns>FoundText struct if word found, otherwise null</returns>
        private static FoundText ParseForDate(string searchText, Regex wordRegex)
        {
            Match wordMatch = wordRegex.Match(searchText);
            Regex whiteSpace = new Regex(@"\W");

            if (wordMatch.Success == true)
            {
                if ((searchText.Length == wordMatch.Length)
                    || (wordMatch.Index == 0 && whiteSpace.IsMatch(searchText[wordMatch.Length].ToString()) == true)
                    || (wordMatch.Index == searchText.Length - wordMatch.Value.Length && wordMatch.Index != 0 && whiteSpace.IsMatch(searchText[wordMatch.Index - 1].ToString()) == true)
                    || (wordMatch.Index != 0 && whiteSpace.IsMatch(searchText[wordMatch.Index - 1].ToString()) == true && whiteSpace.IsMatch(searchText[wordMatch.Index + wordMatch.Length].ToString()) == true))
                {
                    return new FoundText(wordMatch.Value, wordMatch.Index);
                }
            }

            return null;
        }

        /// <summary>
        /// Utility method to find a value in an array
        /// </summary>
        /// <typeparam name="T">Type for comparison</typeparam>
        /// <param name="arrayToSearch">The array to search</param>
        /// <param name="valueToFind">The value to find</param>
        /// <returns>true if found, otherwise false</returns>
        private static bool ArrayContains<T>(T[] arrayToSearch, T valueToFind)
        {
            for (int arrayIndex = 0; arrayIndex < arrayToSearch.Length; arrayIndex++)
            {
                if (arrayToSearch[arrayIndex].Equals(valueToFind) == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Safe parser for a potential date - NhsDate isn't good at parsing with embedded spaces...
        /// </summary>
        /// <param name="potentialDateString">String with candidate date</param>
        /// <returns>NhsDate if parsed successfully, otherwise null</returns>
        private static NhsDate ValidDateParse(string potentialDateString)
        {
            DateTime potentialDateTime;
            NhsDate nhsDateReturn = null;

            DateTime.TryParse(Parser.CleanDateString(potentialDateString), out potentialDateTime);

            if (potentialDateTime != DateTime.MinValue)
            {
                nhsDateReturn = new NhsDate(potentialDateTime);
            }

            return nhsDateReturn;
        }

        /// <summary>
        /// Safe parser for a potential date - catch NhsDate exception if any param out of range
        /// </summary>
        /// <param name="yearValue">YearRegex</param>
        /// <param name="monthValue">Month</param>
        /// <param name="dayValue">Day</param>
        /// <returns>NhsDate if valid date params supplied, otherwise null</returns>
        private static NhsDate ValidDateParse(int yearValue, int monthValue, int dayValue)
        {
            NhsDate nhsDateReturn = null;
            try
            {
                nhsDateReturn = new NhsDate(new DateTime(yearValue, monthValue, dayValue));
            }
            catch (ArgumentOutOfRangeException)
            {
                nhsDateReturn = null;
            }

            return nhsDateReturn;
        }

        /// <summary>
        /// Removes any safe tokens and replaces with spaces
        /// </summary>
        /// <param name="workingText">Text to decode</param>
        /// <returns>Decoded string</returns>
        private static string TokenDecode(string workingText)
        {
            return workingText.Replace(Parser.SafeToken, ' ');
        }

        /// <summary>
        /// Language-neutral gender string lookup
        /// </summary>
        /// <param name="value">Gender string to look up</param>
        /// <returns>Gender match</returns>
        private static Gender LookUpGender(string value)
        {
            if (string.Compare(value, PatientSearch.Resources.ParserResources.Male, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return Gender.Male;
            }
            else if (string.Compare(value, PatientSearch.Resources.ParserResources.Female, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return Gender.Female;
            }

            return Gender.None;
        }

        /// <summary>
        /// Search for delimiters and if present, replace all enclosed space chars with a safe token
        /// </summary>
        /// <param name="workingText">Text to encode</param>
        /// <returns>Encoded string</returns>
        private string TokenEncode(string workingText)
        {
            StringBuilder encodedText = new StringBuilder(workingText.Length);
            int firstStartDelimiter = workingText.IndexOf(this.StartGroupDelimiter);
            int lastEndDelimiter = workingText.LastIndexOf(this.EndGroupDelimiter);
            bool insideDelimitedArea = false;

            if (firstStartDelimiter != -1 && lastEndDelimiter != -1 && firstStartDelimiter != lastEndDelimiter)
            {
                for (int characterIndex = 0; characterIndex < workingText.Length; characterIndex++)
                {
                    if (insideDelimitedArea == true && workingText[characterIndex] == this.EndGroupDelimiter)
                    {
                        insideDelimitedArea = false;
                    }
                    else if (workingText[characterIndex] == this.StartGroupDelimiter)
                    {
                        insideDelimitedArea = true;
                    }

                    if (insideDelimitedArea == true && workingText[characterIndex] == ' ')
                    {
                        encodedText.Append(Parser.SafeToken);
                    }
                    else if (workingText[characterIndex] != this.StartGroupDelimiter && workingText[characterIndex] != this.EndGroupDelimiter)
                    {
                        encodedText.Append(workingText[characterIndex]);
                    }
                }
            }

            return encodedText.ToString();
        }

        /// <summary>
        /// Make sure that a potential Age value is less than or equal to max age
        /// </summary>
        /// <param name="potentialAge">Potential age value</param>
        /// <returns>true if valid age, otherwise false</returns>
        private bool AgeCheck(int potentialAge)
        {
            if (potentialAge <= this.MaximumAge && potentialAge > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Make sure that a potential DOB value less than or equal to current year 
        /// and greater than current year less max age
        /// </summary>
        /// <param name="potentialDateOfBirth">Potential date of birth</param>
        /// <returns>true if valid date of birth, otherwise false</returns>
        private bool DOBCheck(NhsDate potentialDateOfBirth)
        {
            int currentYear = DateTime.Now.Year;

            int potentialYear = -1;

            if (potentialDateOfBirth.DateType == DateType.Exact)
            {
                potentialYear = potentialDateOfBirth.DateValue.Year;
            }
            else
            {
                potentialYear = potentialDateOfBirth.Year;
            }

            if (potentialYear <= currentYear && potentialYear > (currentYear - this.MaximumAge))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Parse workingText for Family Name and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveFamilyName(ref StringBuilder workingText)
        {
            string familyNameMatch = Parser.ParseRemoveWord(ref workingText, FamilyNameRegex);

            if (familyNameMatch.Length > 0)
            {
                this.FamilyName = familyNameMatch;
            }
        }

        /// <summary>
        /// Parse workingText for GivenName followed by FamilyName and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveGivenNameFamilyName(ref StringBuilder workingText)
        {
            string givenNameFamilyNameMatch = Parser.ParseRemoveWord(ref workingText, GivenNameFamilyNameRegex);

            if (givenNameFamilyNameMatch.Length > 0)
            {
                string[] names = givenNameFamilyNameMatch.Split(' ');
                this.GivenName = names[0];
                this.FamilyName = names[1];
            }
        }

        /// <summary>
        /// Parse workingText for Age and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveAge(ref StringBuilder workingText)
        {
            Match ageMatch = ageRegex.Match(workingText.ToString());

            if (ageMatch.Success == true)
            {
                if (((ageMatch.Index + ageMatch.Length) < workingText.Length && workingText[ageMatch.Index + ageMatch.Length] != '-') || (ageMatch.Index + ageMatch.Length) == workingText.Length)
                {
                    int potentialAge = int.Parse(ageMatch.Value, CultureInfo.InvariantCulture);
                    if (this.AgeCheck(potentialAge) == true)
                    {
                        Parser.ParseRemoveWord(ref workingText, ageRegex);
                        this.Age = potentialAge;
                    }
                }
            }
        }

        /// <summary>
        /// Parse workingText for Address - House Number followed by Street Name and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveAddressHouseNumberStreetName(ref StringBuilder workingText)
        {
            string thoroughfare = string.Empty;

            // First check to see if we have a recognised thoroughfare...
            thoroughfare = this.CheckForThoroughfare(workingText);

            string addressPart = addressRegex.Match(workingText.ToString()).Value;
            string addressMatch = string.Empty;

            if (addressPart.Length > 0)
            {
                if (thoroughfare.Length > 0)
                {
                    addressPart = addressPart.Substring(0, addressPart.IndexOf(thoroughfare, StringComparison.OrdinalIgnoreCase) + thoroughfare.Length);

                    Regex thoroughfareRegex = new Regex(addressPart, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                    addressMatch = Parser.ParseRemoveWord(ref workingText, thoroughfareRegex);

                    if (addressMatch.Length > 0)
                    {
                        this.Address = addressMatch;
                    }
                }
                else
                {
                    addressMatch = Parser.ParseRemoveWord(ref workingText, addressRegex);
                    this.Address = addressMatch;
                }
            }
        }

        /// <summary>
        /// Parse workingText for a delimiter-embedded Address - House Number followed by Street Name and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveEmbeddedAddressHouseNumberStreetName(ref StringBuilder workingText)
        {
            string addressPart = EmbeddedAddressRegex.Match(workingText.ToString()).Value;
            string addressMatch = string.Empty;

            if (addressPart.Length > 0)
            {
                addressMatch = Parser.ParseRemoveWord(ref workingText, EmbeddedAddressRegex);
                this.Address = addressMatch;
            }
        }

        /// <summary>
        /// Parse workingText for Date of Birth and remove - Year only
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveDOBYear(ref StringBuilder workingText)
        {
            string textValue = workingText.ToString();
            FoundText foundDate = Parser.CheckForYear(textValue);

            if (foundDate != null)
            {
                NhsDate potentialDateOfBirth = new NhsDate(int.Parse(foundDate.Value, CultureInfo.InvariantCulture));
                if (this.DOBCheck(potentialDateOfBirth) == true)
                {
                    Parser.ParseRemoveWord(ref workingText, new Regex(foundDate.Value, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));
                    this.DateOfBirth = potentialDateOfBirth;
                }
            }
        }

        /// <summary>
        /// Parse workingText for Date of Birth and remove - numeric or text Month followed by Year 
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveDOBIntOrTextMonthYear(ref StringBuilder workingText)
        {
            string textValue = workingText.ToString();
            FoundText foundDate = Parser.CheckForIntOrTextMonthYear(textValue);

            if (foundDate != null)
            {
                string[] dateParts = Parser.CleanDateString(foundDate.Value).Split(' ');
                int monthValue = -1;
                int yearValue = int.Parse(dateParts[1], CultureInfo.InvariantCulture);
                if (yearValue <= 0)
                {
                    return;
                }

                NhsDate potentialDateOfBirth;
                if (int.TryParse(dateParts[0], out monthValue) == true)
                {
                    if (monthValue <= 0 || monthValue > 12)
                    {
                        return;
                    }

                    potentialDateOfBirth = new NhsDate(yearValue, monthValue);
                }
                else
                {
                    monthValue = Parser.GetMonthNumberFromMonthName(dateParts[0], CultureInfo.InvariantCulture);

                    if (monthValue == -1)
                    {
                        potentialDateOfBirth = new NhsDate(yearValue);
                    }
                    else
                    {
                        potentialDateOfBirth = new NhsDate(yearValue, monthValue);
                    }
                }

                if (this.DOBCheck(potentialDateOfBirth) == true)
                {
                    Parser.ParseRemoveWord(ref workingText, new Regex(foundDate.Value, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));
                    this.DateOfBirth = potentialDateOfBirth;
                }
            }
        }

        /// <summary>
        /// Parse workingText for Date of Birth and remove - numeric Day followed by text Month followed by Year 
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveDOBDayTextMonthYear(ref StringBuilder workingText)
        {
            string textValue = workingText.ToString();
            FoundText foundDate = Parser.CheckForDayTextMonthYear(textValue);

            if (foundDate != null)
            {
                // Remove date pattern even if it turns out to be invalid date e.g. 31-Nov-YYYY, 29-Feb-YYYY etc...
                Parser.ParseRemoveWord(ref workingText, new Regex(foundDate.Value, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));

                string[] dateParts = Parser.CleanDateString(foundDate.Value).Split(' ');
                int yearValue = int.Parse(dateParts[2], CultureInfo.InvariantCulture);
                int monthValue = Parser.GetMonthNumberFromMonthName(dateParts[1], CultureInfo.InvariantCulture);
                int dayValue = int.Parse(dateParts[0], CultureInfo.InvariantCulture);
                if (yearValue <= 0 || monthValue <= 0 || monthValue > 12 || dayValue <= 0 || yearValue < (DateTime.Now.Year - this.maximumAge))
                {
                    this.UnmatchedText += " " + foundDate.Value;
                    return;
                }

                NhsDate potentialDateOfBirth = Parser.ValidDateParse(yearValue, monthValue, dayValue);
                if (potentialDateOfBirth != null && this.DOBCheck(potentialDateOfBirth) == true)
                {
                    this.DateOfBirth = potentialDateOfBirth;
                }
                else
                {
                    this.UnmatchedText += " " + foundDate.Value;
                }
            }
        }

        /// <summary>
        /// Parse workingText for Date of Birth and remove - numeric Day followed by numeric Month followed by Year 
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveDOBDayIntMonthYear(ref StringBuilder workingText)
        {
            string textValue = workingText.ToString();
            FoundText foundDate = Parser.CheckForDayIntMonthYear(textValue);

            if (foundDate != null)
            {
                // Remove date pattern even if it turns out to be invalid date e.g. 31-11-YYYY, 29-02-YYYY etc...
                Parser.ParseRemoveWord(ref workingText, new Regex(foundDate.Value, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));

                string[] dateParts = Parser.CleanDateString(foundDate.Value).Split(' ');
                int yearValue = int.Parse(dateParts[2], CultureInfo.InvariantCulture);
                int monthValue = int.Parse(dateParts[1], CultureInfo.InvariantCulture);
                int dayValue = int.Parse(dateParts[0], CultureInfo.InvariantCulture);
                if (yearValue <= 0 || monthValue <= 0 || monthValue > 12 || dayValue <= 0 || yearValue < (DateTime.Now.Year - this.maximumAge))
                {
                    this.UnmatchedText += " " + foundDate.Value;
                    return;
                }

                NhsDate potentialDateOfBirth = Parser.ValidDateParse(foundDate.Value);
                if (potentialDateOfBirth != null && this.DOBCheck(potentialDateOfBirth) == true)
                {
                    this.DateOfBirth = potentialDateOfBirth;
                }
                else
                {
                    this.UnmatchedText += " " + foundDate.Value;
                }
            }
        }

        /// <summary>
        /// Parse workingText for Date of Birth range and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveDOBRange(ref StringBuilder workingText)
        {
            string textValue = workingText.ToString();
            FoundText foundFromDate = Parser.CheckForDate(textValue);

            if (foundFromDate == null)
            {
                return;
            }

            string remainingText = textValue.Substring(foundFromDate.Start + foundFromDate.Length);
            if (remainingText.Length < 4)
            {
                return;
            }

            FoundText foundToDate = Parser.CheckForDate(remainingText);

            if (foundToDate != null)
            {
                string separatorText = textValue.Substring(foundFromDate.Start + foundFromDate.Length, textValue.Length - (foundFromDate.Start + foundFromDate.Length) - (remainingText.Length - foundToDate.Start));

                if (separatorText.Trim() == "-")
                {
                    NhsDate potentialLowerDateOfBirth;
                    NhsDate potentialUpperDateOfBirth;

                    if (foundFromDate.Type == FoundDataType.DateDayIntMonthYear || foundFromDate.Type == FoundDataType.DateDayTextMonthYear)
                    {
                        potentialLowerDateOfBirth = Parser.ValidDateParse(Parser.CleanDateString(foundFromDate.Value));
                    }
                    else
                    {
                        potentialLowerDateOfBirth = new NhsDate(Parser.CleanDateString(foundFromDate.Value));
                    }

                    if (foundToDate.Type == FoundDataType.DateDayIntMonthYear || foundToDate.Type == FoundDataType.DateDayTextMonthYear)
                    {
                        potentialUpperDateOfBirth = Parser.ValidDateParse(Parser.CleanDateString(foundToDate.Value));
                    }
                    else
                    {
                        potentialUpperDateOfBirth = new NhsDate(Parser.CleanDateString(foundToDate.Value));
                    }

                    if (potentialLowerDateOfBirth != null && potentialUpperDateOfBirth != null)
                    {
                        if (this.DOBCheck(potentialLowerDateOfBirth) == true && this.DOBCheck(potentialUpperDateOfBirth) == true)
                        {
                            Parser.ParseRemoveWord(ref workingText, new Regex(foundFromDate.Value + separatorText + foundToDate.Value, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));
                            this.DateOfBirth = potentialLowerDateOfBirth;
                            this.DateOfBirthUpper = potentialUpperDateOfBirth;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parse workingText for Address House Name followed by Street Name and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveAddressHouseNameStreetName(ref StringBuilder workingText)
        {
            string thoroughfare = string.Empty;

            // First check to see if we have a recognised thoroughfare...
            thoroughfare = this.CheckForThoroughfare(workingText);

            if (thoroughfare.Length > 0)
            {
                string thoroughfareRegexPattern = @"([A-Z\'" + Parser.SafeToken + @"]+\s){2,}" + thoroughfare;
                Regex thoroughfareRegex = new Regex(@thoroughfareRegexPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                string thoroughfareMatch = Parser.ParseRemoveWord(ref workingText, thoroughfareRegex);

                if (thoroughfareMatch.Length > 0)
                {
                    this.Address = thoroughfareMatch;
                }
            }
        }

        /// <summary>
        /// Parse workingText for Gender and remove - Male
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveGenderMale(ref StringBuilder workingText)
        {
            Regex genderRegex = new Regex(NhsCui.Toolkit.PatientSearch.Resources.ParserResources.Male, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            if (Parser.ParseRemoveWord(ref workingText, genderRegex).IndexOf(NhsCui.Toolkit.PatientSearch.Resources.ParserResources.Male, StringComparison.OrdinalIgnoreCase) != -1)
            {
                this.Gender = Gender.Male;
            }
        }

        /// <summary>
        /// Check if workingText contains one of the standard titles
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        /// <returns>Title if found, otherwise null</returns>
        private Title CheckForTitle(StringBuilder workingText)
        {
            foreach (Title title in this.Titles)
            {
                string titleRegexString = @"\b" + title.Name + @"\b";
                Regex titleRegex = new Regex(titleRegexString, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                if (titleRegex.IsMatch(workingText.ToString()) == true)
                {
                    return title;
                }
            }

            return null;
        }

        /// <summary>
        /// Put together a regex string to match any of the current titles
        /// </summary>
        /// <returns>Regex pattern for any of the current titles</returns>
        private string BuildTitleRegexString()
        {
            StringBuilder titleRegex = new StringBuilder();
            Title currentTitle;
            string delimiter = "|";
            for (int titleIndex = 0; titleIndex < this.Titles.Count; titleIndex++)
            {
                currentTitle = this.Titles[titleIndex];
                if (titleIndex == this.Titles.Count - 1)
                {
                    titleRegex.Append(currentTitle.Name);
                }
                else
                {
                    titleRegex.Append(currentTitle.Name + delimiter);
                }
            }

            return titleRegex.ToString();
        }

        /// <summary>
        /// Check if workingText contains one of the standard thoroughfares
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        /// <returns>Thoroughfare if found, otherwise null</returns>
        private string CheckForThoroughfare(StringBuilder workingText)
        {
            foreach (string thoroughfare in this.Thoroughfares)
            {
                string thoroughfareRegexString = @"\b" + thoroughfare + @"\b";
                Regex thoroughfareRegex = new Regex(thoroughfareRegexString, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                if (thoroughfareRegex.IsMatch(workingText.ToString()) == true)
                {
                    return thoroughfare;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Check if workingText contains one of the standard thoroughfares within embedded text
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        /// <returns>Thoroughfare if found, otherwise null</returns>
        private string CheckForEmbeddedThoroughfare(StringBuilder workingText)
        {
            foreach (string thoroughfare in this.Thoroughfares)
            {
                string thoroughfareRegexString = Parser.SafeToken + thoroughfare;
                Regex thoroughfareRegex = new Regex(thoroughfareRegexString, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                if (thoroughfareRegex.IsMatch(workingText.ToString()) == true)
                {
                    return thoroughfare;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Parse workingText for Title followed by FamilyName and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveTitleFamilyName(ref StringBuilder workingText)
        {
            Title title = null;

            string titleRegexString = this.BuildTitleRegexString();
            
            // if (title != null)
            if (titleRegexString.Length > 0)
            {
                // string fullNameRegexPattern = "(" + title.Name + @"+\.?)\s([A-Z\-\'" + Parser.SafeToken + @"]+)";
                string fullNameRegexPattern = "(" + titleRegexString + @"+\.?)\s([A-Z\-\'" + Parser.SafeToken + @"]+)";
                Regex fullNameRegex = new Regex(@fullNameRegexPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                string fullNameMatch = Parser.ParseRemoveWord(ref workingText, fullNameRegex);

                if (fullNameMatch.Length > 0)
                {
                    title = this.CheckForTitle(new StringBuilder(fullNameMatch));
                    string[] nameParts = fullNameMatch.Split(' ');
                    this.Title = nameParts[0];
                    this.FamilyName = nameParts[1];
                    if (this.Gender == Gender.None && title.Gender != Gender.None)
                    {
                        this.Gender = title.Gender;
                    }
                }
            }
        }

        /// <summary>
        /// Parse workingText for Title followed by Given Name followed by Family Name and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveFullName(ref StringBuilder workingText)
        {
            Title title = null;

            string titleRegexString = this.BuildTitleRegexString();

            // if (title != null)
            if (titleRegexString.Length > 0)
            {
                // string fullNameRegexPattern = "(" + title.Name + @"+\.?)\s([A-Z\-\'" + Parser.SafeToken + @"]+)\s([A-Z\-\'" + Parser.SafeToken + @"]+)";
                string fullNameRegexPattern = "(" + titleRegexString + @"+\.?)\s([A-Z\-\'" + Parser.SafeToken + @"]+)\s([A-Z\-\'" + Parser.SafeToken + @"]+)";
                Regex fullNameRegex = new Regex(@fullNameRegexPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                string fullNameMatch = Parser.ParseRemoveWord(ref workingText, fullNameRegex);

                if (fullNameMatch.Length > 0)
                {
                    title = this.CheckForTitle(new StringBuilder(fullNameMatch));
                    string[] nameParts = fullNameMatch.Split(' ');
                    this.Title = nameParts[0];
                    this.GivenName = nameParts[1];
                    this.FamilyName = nameParts[2];
                    if (this.Gender == Gender.None && title.Gender != Gender.None)
                    {
                        this.Gender = title.Gender;
                    }
                }
            }
        }

        /// <summary>
        /// Parse workingText for Age Range and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveAgeRange(ref StringBuilder workingText)
        {
            Match ageRangeMatch = ageRangeRegex.Match(workingText.ToString());

            if (ageRangeMatch.Success == true)
            {
                if (((ageRangeMatch.Index + ageRangeMatch.Length) < workingText.Length && workingText[ageRangeMatch.Index + ageRangeMatch.Length] != '-') || (ageRangeMatch.Index + ageRangeMatch.Length) == workingText.Length)
                {
                    string[] ages = ageRangeMatch.Value.Split('-');
                    int lowerAge = int.Parse(ages[0], CultureInfo.InvariantCulture);
                    int upperAge = int.Parse(ages[1], CultureInfo.InvariantCulture);
                    if (this.AgeCheck(lowerAge) == true && this.AgeCheck(upperAge) == true)
                    {
                        Parser.ParseRemoveWord(ref workingText, ageRangeRegex);
                        this.Age = lowerAge;
                        this.AgeUpper = upperAge;
                    }
                }
            }
        }

        /// <summary>
        /// Parse workingText for Postcode and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemovePostcode(ref StringBuilder workingText)
        {
            string postcodeMatch = Parser.ParseRemoveWord(ref workingText, PostcodeRegex);

            if (postcodeMatch.Length > 0)
            {
                this.Postcode = postcodeMatch;
            }
        }

        /// <summary>
        /// Parse workingText for NHS Number and remove
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveNhsNumber(ref StringBuilder workingText)
        {
            string nhsNumberMatch = Parser.ParseRemoveWord(ref workingText, NhsNumberRegex);

            if (nhsNumberMatch.Length > 0)
            {
                this.NhsNumber = nhsNumberMatch;
            }
        }

        /// <summary>
        /// Parse workingText for Gender and remove - Female
        /// </summary>
        /// <param name="workingText">Working text buffer</param>
        private void ParseRemoveGenderFemale(ref StringBuilder workingText)
        {
            Regex genderRegex = new Regex(NhsCui.Toolkit.PatientSearch.Resources.ParserResources.Female, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            if (Parser.ParseRemoveWord(ref workingText, genderRegex).IndexOf(NhsCui.Toolkit.PatientSearch.Resources.ParserResources.Female, StringComparison.OrdinalIgnoreCase) != -1)
            {
                this.Gender = Gender.Female;
            }
        }

        /// <summary>
        /// Refresh the isCommonFamilyName flag
        /// </summary>
        private void UpdateIsCommonFamilyName()
        {
            if (this.FamilyName == null)
            {
                return;
            }

            this.familyNameIsCommon = false;

            foreach (string familyName in this.CommonFamilyNames)
            {
                if (familyName.ToUpperInvariant() == this.FamilyName.ToUpperInvariant())
                {
                    this.familyNameIsCommon = true;
                }
            }
        }

        /// <summary>
        /// Parses the string in the Parser.Text property by splitting on the InformationDelimiter char and
        /// using the InformationFormat array to populates the corresponding properties with the results.
        /// </summary>
        private void ParseSplit()
        {
            if (this.InformationFormat == null)
            {
                return;
            }

            string[] fields = this.Text.Split(this.InformationDelimiter);
            int currentItemIndex = 0;

            while (currentItemIndex < fields.Length && currentItemIndex < this.InformationFormat.Count)
            {
                string unmatchedResult = this.SetProperty(this.InformationFormat[currentItemIndex], fields[currentItemIndex].Trim());
                if (unmatchedResult.Length > 0)
                {
                    this.UnmatchedText += " " + unmatchedResult;
                }

                currentItemIndex++;
            }
        }

        /// <summary>
        /// Set a property using a PatientSearch.Information value
        /// </summary>
        /// <param name="patientSearchInfo">PatientSearch.Information value</param>
        /// <param name="valueToSet">Value to set</param>
        /// <returns>If assignment fails, the valueToSet is returned for inclusion in unmatched text</returns>
        private string SetProperty(Information patientSearchInfo, string valueToSet)
        {
            try
            {
                switch (patientSearchInfo)
                {
                    case Information.Address:
                        this.Address = valueToSet;
                        break;
                    case Information.Age:
                        this.Age = int.Parse(valueToSet, CultureInfo.InvariantCulture);
                        break;
                    case Information.DateOfBirth:
                        this.DateOfBirth = new NhsDate(valueToSet);
                        break;
                    case Information.FamilyName:
                        this.FamilyName = valueToSet;
                        break;
                    case Information.Gender:
                        this.Gender = Parser.LookUpGender(valueToSet.Trim());
                        break;
                    case Information.GivenName:
                        this.GivenName = valueToSet;
                        break;
                    case Information.NhsNumber:
                        this.NhsNumber = valueToSet;
                        break;
                    case Information.Postcode:
                        this.Postcode = valueToSet;
                        break;
                    case Information.Title:
                        this.Title = valueToSet;
                        break;
                }
            }
            catch (FormatException)
            {
                return valueToSet;
            }
            catch (ArgumentException)
            {
                return valueToSet;
            }

            return string.Empty;
        }

        /// <summary>
        /// Parses the string in the Parser.Text property using Regexs and populates the equivalent properties with the results.
        /// </summary>
        private void ParseRegex()
        {
            int startDelimiterPosition = this.Text.IndexOf(this.StartGroupDelimiter);
            int endDelimiterPosition = this.Text.LastIndexOf(this.EndGroupDelimiter);
            StringBuilder workingText;

            if (startDelimiterPosition != -1 && endDelimiterPosition != -1 && startDelimiterPosition != endDelimiterPosition)
            {
                workingText = new StringBuilder(this.TokenEncode(this.Text));
            }
            else
            {
                workingText = new StringBuilder(this.Text);
            }

            this.ResetAllProperties();

            this.ParseRemoveGenderFemale(ref workingText);
            this.ParseRemoveNhsNumber(ref workingText);
            this.ParseRemovePostcode(ref workingText);
            this.ParseRemoveAgeRange(ref workingText);
            this.ParseRemoveFullName(ref workingText);
            this.ParseRemoveTitleFamilyName(ref workingText);
            this.ParseRemoveGenderMale(ref workingText);
            if (this.FamilyName != null)
            {
                this.ParseRemoveAddressHouseNameStreetName(ref workingText);
            }

            this.ParseRemoveDOBRange(ref workingText);
            this.ParseRemoveDOBDayIntMonthYear(ref workingText);
            this.ParseRemoveDOBDayTextMonthYear(ref workingText);
            this.ParseRemoveDOBIntOrTextMonthYear(ref workingText);
            this.ParseRemoveDOBYear(ref workingText);
            this.ParseRemoveEmbeddedAddressHouseNumberStreetName(ref workingText);
            if (this.Address == null)
            {
                this.ParseRemoveAddressHouseNumberStreetName(ref workingText);
            }

            this.ParseRemoveAge(ref workingText);
            if (this.FamilyName == null)
            {
                this.ParseRemoveGivenNameFamilyName(ref workingText);
            }

            // If we still don't have a family name...
            if (this.FamilyName == null)
            {
                this.ParseRemoveFamilyName(ref workingText);
            }

            if (workingText.ToString().Length > 0)
            {
                this.UnmatchedText += " " + workingText.ToString().Trim();
            }
        }

        /// <summary>
        /// Checks that all properties corresponding to Information 
        /// enums marked as mandatory in MandatoryInformation have been set.
        /// </summary>
        /// <returns>True if all mandatory properties have been set; otherwise false.</returns>
        private bool AllMandatoryInformationEntered()
        {
            int setFlags = 0;
            int allFlags = 0;

            foreach (Information patientSearchInfo in this.MandatoryInformation)
            {
                allFlags = allFlags | (Int32)patientSearchInfo;
                if (this.PropertyHasBeenSet(patientSearchInfo) == true)
                {
                    setFlags = setFlags | (Int32)patientSearchInfo;
                }
            }

            if (allFlags == setFlags)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Initialise or reset the CommonFamilyNames list.
        /// </summary>
        private void ResetCommonFamilyNames()
        {
            if (this.commonFamilyNames == null)
            {
                this.commonFamilyNames = new List<string>();
            }
            else
            {
                this.commonFamilyNames.Clear();
            }

            this.commonFamilyNames.AddRange(Parser.GetCommonFamilyNames());
        }

        /// <summary>
        /// Checks whether a property corresponding to a
        /// Information enumeration value has been set
        /// </summary>
        /// <param name="patientSearchInfo">Information corresponding to a property</param>
        /// <returns>true if property has been set, otherwise false</returns>
        private bool PropertyHasBeenSet(Information patientSearchInfo)
        {
            switch (patientSearchInfo)
            {
                case Information.Address:
                    if (this.Address != null)
                    {
                        return true;
                    }

                    break;
                case Information.Age:
                    if (this.Age > -1)
                    {
                        return true;
                    }

                    break;
                case Information.DateOfBirth:
                    if (this.DateOfBirth.IsNull == false)
                    {
                        return true;
                    }

                    break;
                case Information.FamilyName:
                    if (this.FamilyName != null)
                    {
                        return true;
                    }

                    break;
                case Information.Gender:
                    if (this.Gender != Gender.None)
                    {
                        return true;
                    }

                    break;
                case Information.GivenName:
                    if (this.GivenName != null)
                    {
                        return true;
                    }

                    break;
                case Information.NhsNumber:
                    if (this.NhsNumber != null)
                    {
                        return true;
                    }

                    break;
                case Information.Postcode:
                    if (this.Postcode != null)
                    {
                        return true;
                    }

                    break;
                case Information.Title:
                    if (this.Title != null)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        /// <summary>
        /// (Re)Initialise all the properties.
        /// </summary>
        private void ResetAllProperties()
        {
            this.address = null;
            this.age = -1;
            this.ageUpper = -1;
            this.dateOfBirth = new NhsDate();
            this.dateOfBirth.DateType = DateType.Null;
            this.dateOfBirthUpper = new NhsDate();
            this.dateOfBirthUpper.DateType = DateType.Null;
            this.familyName = null;
            this.gender = Gender.None;
            this.givenName = null;
            this.nhsNumber = null;
            this.postcode = null;
            this.title = null;
            this.unmatchedText = null;

            this.UpdateIsCommonFamilyName();
        }

        /// <summary>
        /// Initialise or reset the titles list.
        /// </summary>
        private void ResetTitles()
        {
            if (this.titles == null)
            {
                this.titles = new List<Title>();
            }
            else
            {
                this.titles.Clear();
            }

            // this.titles.AddRange(Parser.GetTitles());
            foreach (Title title in Parser.GetTitles())
            {
                this.titles.Add(title);
            }
        }

        #endregion
    }
}
