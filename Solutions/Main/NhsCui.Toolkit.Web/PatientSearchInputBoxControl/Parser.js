//-----------------------------------------------------------------------
// <copyright file="Parser.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>Ported JS version of C# PatientSearch.Parser class</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

//=============================================================================
// Enums
//=============================================================================
var Gender = function()
{
    /// <summary>
    /// Gender is a public enumeration of supported genders
    /// </summary>
};

Gender.prototype = 
{
    /// <summary>
    /// Enum to indicate male search criterion.
    /// </summary>
    Male:0,

    /// <summary>
    /// Enum to indicate female search criterion.
    /// </summary>
    Female:1,

    /// <summary>
    /// Enum to indicate no gender search criterion.
    /// </summary>
    None:2
};

Gender.registerEnum("Gender");

var Information = function() 
{
    /// <summary>
    /// A public enumeration of information values for PatientSearch.
    /// </summary>
};

Information.prototype = 
{
    /// <summary>
    /// All flags cleared.
    /// </summary>
    None:0,

    /// <summary>
    /// Enum to indicate address search criterion.
    /// </summary>
    Address:1,

    /// <summary>
    /// Enum to indicate age search criterion.
    /// </summary>
    Age:2,

    /// <summary>
    /// Enum to indicate date of birth search criterion.
    /// </summary>
    DateOfBirth:4,

    /// <summary>
    /// Enum to indicate family name search criterion.
    /// </summary>
    FamilyName:8,

    /// <summary>
    /// Enum to indicate gender search criterion.
    /// </summary>
    Gender:16,

    /// <summary>
    /// Enum to indicate given name search criterion.
    /// </summary>
    GivenName:32,

    /// <summary>
    /// Enum to indicate NHS Number search criterion.
    /// </summary>
    NhsNumber:64,

    /// <summary>
    /// Enum to indicate postcode search criterion.
    /// </summary>
    Postcode:128,

    /// <summary>
    /// Enum to indicate title search criterion.
    /// </summary>
    Title:256
};

Information.registerEnum("Information", true);

var FoundDataType = function() 
{
    /// <summary>
    /// A public enumeration of found data type values for PatientSearch. EDITED BY AON BUT NOT YET REVIEWED
    /// </summary>
};

FoundDataType.prototype = 
{
    /// <summary>
    /// Enumeration to indicate no specific type
    /// </summary>
    NotSpecified:0,

    /// <summary>
    /// Enumeration to indicate a string containing integer day, integer month and integer year
    /// </summary>
    DateDayIntMonthYear:1,

    /// <summary>
    /// Enumeration to indicate a string containing integer day, text month and integer year
    /// </summary>
    DateDayTextMonthYear:2,

    /// <summary>
    /// Enumeration to indicate a string containing integer month and integer year
    /// </summary>
    DateIntMonthYear:3,

    /// <summary>
    /// Enumeration to indicate a string containing text month and integer year
    /// </summary>
    DateTextMonthYear:4,

    /// <summary>
    /// Enumeration to indicate a string containing integer year
    /// </summary>
    DateYear:5
};

FoundDataType.registerEnum("FoundDataType", true);

//=============================================================================
// Title class
//=============================================================================
var Title = NhsCui.Toolkit.Web.Title = function() 
{
    NhsCui.Toolkit.Web.Title.initializeBase(this);
    
    //=============================================================================
    // Member Vars
        
    /// <summary>
    /// Backing member var for Name property
    /// </summary>
    this.name = "";

    /// <summary>
    /// Backing member var for Gender property
    /// </summary>
    this.gender = Gender.None;

    //=============================================================================
    // Constructor args
    if(arguments.length==1 && typeof arguments[0]=="string")
    {
        this.name = arguments[0];
    }
    else if(arguments.length==2 && typeof arguments[0]=="string" && typeof arguments[1]=="number")
    {
        this.name = arguments[0];
        this.gender = arguments[1];
    }
};

//=============================================================================
// Title prototype
//=============================================================================
NhsCui.Toolkit.Web.Title.prototype = 
{
    //=============================================================================
    // Properties

    /// <summary>
    /// The Gender of the Title class instance
    /// </summary>
    get_gender : function() 
    {
        return this.gender;
    },
    set_gender : function(value) 
    {
        this.gender = value;
        this.raisePropertyChanged('gender');
    },

    /// <summary>
    /// The Name of the Title class instance
    /// </summary>
    get_name : function() 
    {
        return this.name;
    },
    set_name : function(value) 
    {
        this.name = value;
        this.raisePropertyChanged('name');
    }
};

NhsCui.Toolkit.Web.Title.registerClass('NhsCui.Toolkit.Web.Title', Sys.Component);

//=============================================================================
// FoundText class
//=============================================================================
var FoundText = NhsCui.Toolkit.Web.FoundText = function() 
{
    NhsCui.Toolkit.Web.FoundText.initializeBase(this);
    
    //=============================================================================
    // Member Vars

    /// <summary>
    /// The index of the start of the found text
    /// </summary>
    this.start = -1;

    /// <summary>
    /// The found text value
    /// </summary>
    this.value = "";
    
    /// <summary>
    /// The found text data type
    /// </summary>
    this.foundDataType = FoundDataType.NotSpecified;

    //=============================================================================
    // Constructor args
    if(arguments.length==2 && typeof arguments[0]=="string" && typeof arguments[1]=="number")
    {
        this.value = arguments[0];
        this.start = arguments[1];
    }
    else if (arguments.length==3 && typeof arguments[0]=="string" && typeof arguments[1]=="number" && typeof arguments[2]=="number")
    {
        this.value = arguments[0];
        this.start = arguments[1];
        this.foundDataType = arguments[2];
    }
};

//=============================================================================
// FoundText prototype
//=============================================================================
NhsCui.Toolkit.Web.FoundText.prototype = 
{
    //=============================================================================
    // Properties
    
    /// <summary>
    /// The length of the found text
    /// </summary>
    get_length : function() 
    {
        return this.value.length;
    },

    /// <summary>
    /// The index of the start of the found text
    /// </summary>
    get_start : function() 
    {
        return this.start;
    },
    set_start : function(value) 
    {
        this.start = value;
        this.raisePropertyChanged('start');
    },

    /// <summary>
    /// The found text value
    /// </summary>
    get_value : function() 
    {
        return this.value;
    },
    set_value : function(value) 
    {
        this.value = value;
        this.raisePropertyChanged('value');
    },
    
    /// <summary>
    /// The found text found data type
    /// </summary>
    get_type : function() 
    {
        return this.foundDataType;
    },
    set_type : function(value) 
    {
        this.foundDataType = value;
        this.raisePropertyChanged('foundDataType');
    }
};

NhsCui.Toolkit.Web.FoundText.registerClass('NhsCui.Toolkit.Web.FoundText', Sys.Component);

//=============================================================================
// Parser class
//=============================================================================
var Parser = NhsCui.Toolkit.Web.Parser = function() 
{
    NhsCui.Toolkit.Web.Parser.initializeBase(this);

    //=============================================================================
    // Member Vars

    /// <summary>
    /// Regex constant for Year matching
    /// </summary>
    this.YearRegex = "\\b\\d\\d\\d\\d\\b";

    /// <summary>
    /// Regex constant for Day Month(int) Year digit matching
    /// </summary>
    this.DayIntMonthYearRegex = "(\\d\\d?)[/\\-.]\\d\\d?[/\\-.]\\d\\d\\d\\d";

    /// <summary>
    /// Regex constant for Day Month(name) Year digit matching
    /// </summary>
    this.DayTextMonthYearRegex = "(\\d\\d?)[/\\-.\\s]([A-Z]{3,9})[/\\-.\\s]\\d\\d\\d\\d";

    /// <summary>
    /// Regex constant for Month(int) or Month(name) Year digit matching
    /// </summary>
    // this.IntOrTextMonthYearRegex = "((\\d\\d?)|([A-Z]{3,9}))[/\\-.]\\d\\d\\d\\d";
    this.IntOrTextMonthYearRegex = "((\\b01|\\b02|\\b03|\\b04|\\b05|\\b06|\\b07|\\b08|\\b09|\\b1|\\b2|\\b3|\\b4|\\b5|\\b6|\\b7|\\b8|\\b9|\\b10\\b|\\b11|\\b12)|([A-Z]{3,9}))[/\\-.]\\d\\d\\d\\d";

    /// <summary>
    /// Are we using a safe token to encode delimited text?
    /// </summary>
    // this.safeToken = '�';
    this.safeToken = String.fromCharCode(181);

    /// <summary>
    /// The address parsed from the Text property.
    /// </summary>
    this.address = null;

    /// <summary>
    /// The age parsed from the Text property.
    /// </summary>
    this.age = -1;

    /// <summary>
    /// The upper value in an age range.
    /// </summary>
    this.ageUpper = -1;

    /// <summary>
    /// A list of common family names.
    /// </summary>
    this.commonFamilyNames = null;

    /// <summary>
    /// The date of birth parsed from the Text property.
    /// </summary>
    this.dateOfBirth = new NhsDate();
    this.dateOfBirth.set_dateType(DateType.Null);

    /// <summary>
    /// The upper value in a date of birth range.
    /// </summary>
    this.dateOfBirthUpper = new NhsDate();
    this.dateOfBirth.set_dateType(DateType.Null);

    /// <summary>
    /// The character that is used to delimit the end of a group of words.
    /// </summary>
    this.endGroupDelimiter = '"';

    /// <summary>
    /// The family name parsed from the Text property.
    /// </summary>
    this.familyName = null;  

    /// <summary>
    /// The gender parsed from the Text property.
    /// </summary>
    this.gender = Gender.None;

    /// <summary>
    /// The given name parsed from the Text property.
    /// </summary>
    this.givenName = null;

    /// <summary>
    /// The character that is used to delimit the end of a group of words.
    /// </summary>
    this.informationDelimiter = ',';

    /// <summary>
    /// The list of Information enumeration values that are used to parse the Text property.
    /// </summary>
    this.informationFormat = null;

    /// <summary>
    /// Does the FamilyName appear in the common family names list?
    /// </summary>
    this.familyNameIsCommon;

    /// <summary>
    /// The list of Information enumeration values that are mandatory.
    /// </summary>
    this.mandatoryInformation = null;

    /// <summary>
    /// The maximum age recognised by the parser.
    /// </summary>
    this.maximumAge = 130;

    /// <summary>
    /// The NHS Number parsed from the Text property.
    /// </summary>
    this.nhsNumber = null;

    /// <summary>
    /// The postcode parsed from the Text property.
    /// </summary>
    this.postcode = null;

    /// <summary>
    /// The character that is used to delimit the end of a group of words.
    /// </summary>
    this.startGroupDelimiter = '"';

    /// <summary>
    /// The patient search criteria to parse.
    /// </summary>
    this.text = null;

    /// <summary>
    /// The title parsed from the Text property.
    /// </summary>
    this.title = null;

    /// <summary>
    /// A list of Title objects.
    /// </summary>
    this.titles = null;

    /// <summary>
    /// A list of common thoroughfares.
    /// </summary>
    this.thoroughfares  = null;

    /// <summary>
    /// The remaining text that could not be matched after the parsing process has identified all other values.
    /// </summary>
    this.unmatchedText = null;
    
    /// <summary>
    /// Need this.workingText at class level as we can't pass into parse methods by ref
    /// </summary>
    this.workingText = "";
};

//=============================================================================
// Parser prototype
//=============================================================================
NhsCui.Toolkit.Web.Parser.prototype = 
{
    //=============================================================================
    // Properties
    
    /// <summary>
    /// The address parsed from the Text property.
    /// </summary>
    get_address : function()
    {
        return this.address;
    },
    set_address : function(value)
    {
        this.address = this._tokenDecode(value);
    },

    /// <summary>
    /// The age parsed from the Text property.
    /// </summary>
    get_age : function()
    {
        return this.age;
    },
    set_age : function(value)
    {
        this.age = value;
    },

    /// <summary>
    /// The upper value in an age range.
    /// </summary>
    get_ageUpper : function()
    {
        return this.ageUpper;
    },
    set_ageUpper : function(value)
    {
        this.ageUpper = value;
    },

    /// <summary>
    /// A list of common family names.
    /// </summary>
    get_commonFamilyNames : function()
    {
        if (this.commonFamilyNames === null)
        {
            this._resetCommonFamilyNames();
        }

        return this.commonFamilyNames;
    },
    set_commonFamilyNames : function(value)
    {
        this.commonFamilyNames = value;
    },

    /// <summary>
    /// The date of birth parsed from the Text property.
    /// </summary>
    get_dateOfBirth : function()
    {
        return this.dateOfBirth;
    },
    set_dateOfBirth : function(value)
    {
        this.dateOfBirth = value;
    },

    /// <summary>
    /// The upper value in a date of birth range.
    /// </summary>
    get_dateOfBirthUpper : function()
    {
        return this.dateOfBirthUpper;
    },
    set_dateOfBirthUpper : function(value)
    {
        this.dateOfBirthUpper = value;
    },

    /// <summary>
    /// The character that is used to delimit the end of a group of words.
    /// </summary>
    get_endGroupDelimiter : function()
    {
        return this.endGroupDelimiter;
    },
    set_endGroupDelimiter : function(value)
    {
        if (value == ' ' | value == '/' | value == '-' | value == this.informationDelimiter)
        {
            throw Error.argumentException("endGroupDelimiterValue", value, ParserResources.EndGroupDelimiterExceptionMessage);
        }
        else
        {
            this.endGroupDelimiter = value;
        }
    },

    /// <summary>
    /// The family name parsed from the Text property.
    /// </summary>
    get_familyName : function()
    {
        return this.familyName;
    },
    set_familyName : function(value)
    {
        this.familyName = this._tokenDecode(value);
        this._updateIsCommonFamilyName();
    },

    /// <summary>
    /// The gender parsed from the Text property.
    /// </summary>
    get_gender : function()
    {
        return this.gender;
    },
    set_gender : function(value)
    {
        this.gender = value;
    },

    /// <summary>
    /// The given name parsed from the Text property.
    /// </summary>
    get_givenName : function()
    {
        return this.givenName;
    },
    set_givenName : function(value)
    {
        this.givenName = this._tokenDecode(value);
    },

    /// <summary>
    /// The character that is used to delimit the end of a group of words.
    /// </summary>
    get_informationDelimiter : function()
    {
        return this.informationDelimiter;
    },
    set_informationDelimiter : function(value)
    {
        if (value == ' ' | value == '/' | value == '-' | value == this.startGroupDelimiter | value == this.endGroupDelimiter)
        {
            throw Error.argumentException("informationDelimiter", value, ParserResources.InformationDelimiterExceptionMessage);
        }
        else
        {
            this.informationDelimiter = value;
        }
    },
        
    /// <summary>
    /// The list of Information enumeration values that are used to parse the Text property.
    /// </summary>
    get_informationFormat : function()
    {
        return this.informationFormat;
    },
    set_informationFormat : function(value)
    {
        this.informationFormat = value;
    },

    /// <summary>
    /// true if FamilyName is found in CommonFamilyNames
    /// </summary>
    get_isCommonFamilyName : function()
    {
        return this.familyNameIsCommon;
    },

    /// <summary>
    /// IsDateOfBirthAgeMismatch is true if both DateOfBirth and Age have been entered, 
    /// and the number of years of the Age is not the same number of years for the DateOfBirth 
    /// based on the current date. Otherwise, it is false
    /// </summary>
    get_isDateOfBirthAgeMismatch : function()
    {
        if (this.dateOfBirth.get_isNull() != DateType.Null && this.age > -1 && this.age != (new Date().getFullYear() - this.dateOfBirth.get_year()))
        {
            return true;
        }
        else
        {
            return false;
        }
    },

    /// <summary>
    /// IsGenderTitleMismatch is true if both Gender and Title have been entered, 
    /// and the Title has a Gender of Male or Female and the Gender�s 
    /// Gender is not the same. Otherwise, it is false
    /// </summary>
    get_isGenderTitleMismatch : function()
    {
        if (this.get_gender() != Gender.None && this.get_title() != null)
        {
            var checkTitle = this._checkForTitle(this.get_title());
            if (checkTitle != null && this.get_gender() != checkTitle.get_gender())
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
    },

    /// <summary>
    /// Returns true if all of the values in MandatoryInformation are not their default values. 
    /// If MandatoryInformation is null, IsMandatoryInformationEntered is false
    /// </summary>
    get_isMandatoryInformationEntered : function()
    {
        // TODO
        if (this.get_mandatoryInformation() != null)
        {
            return this._allMandatoryInformationEntered();
        }
        else
        {
            return false;
        }
    },
        
    /// <summary>
    /// The list of Information enumeration values that are mandatory.
    /// </summary>
    get_mandatoryInformation : function()
    {
        return this.mandatoryInformation;
    },
    set_mandatoryInformation : function(value)
    {
        this.mandatoryInformation = value;
    },

    /// <summary>
    /// The maximum age recognised by the parser.
    /// </summary>
    get_maximumAge : function()
    {
        return this.maximumAge;
    },
    set_maximumAge : function(value)
    {
        this.maximumAge = value;
    },

    /// <summary>
    /// The NHS Number parsed from the Text property.
    /// </summary>
    get_nhsNumber : function()
    {
        return this.nhsNumber;
    },
    set_nhsNumber : function(value)
    {
        this.nhsNumber = this._tokenDecode(value);
    },

    /// <summary>
    /// The postcode parsed from the Text property.
    /// </summary>
    get_postcode : function()
    {
        return this.postcode;
    },
    set_postcode : function(value)
    {
        this.postcode = this._tokenDecode(value);
    },

    /// <summary>
    /// The character that is used to delimit the end of a group of words.
    /// </summary>
    get_startGroupDelimiter : function()
    {
        return this.startGroupDelimiter;
    },
    set_startGroupDelimiter : function(value)
    {
        if (value == ' ' | value == '/' | value == '-' | value == this.get_informationDelimiter)
        {
            throw Error.argumentException("startGroupDelimiter", value, ParserResources.StartGroupDelimiterExceptionMessage);
        }
        else
        {
            this.startGroupDelimiter = value;
        }
    },

    /// <summary>
    /// The patient search criteria to parse.
    /// </summary>
    get_text : function()
    {
        return this.text;
    },
    set_text : function(value)
    {
        this.text = value;
    },

    /// <summary>
    /// The title parsed from the Text property.
    /// </summary>
    get_title : function()
    {
        return this.title;
    },
    set_title : function(value)
    {
        this.title = this._tokenDecode(value);
    },

    /// <summary>
    /// The internal list of recognised thoroughfares - create on demand/first use from resources
    /// </summary>
    get_thoroughfares : function()
    {
        if (this.thoroughfares == null)
        {
            this.thoroughfares = this._getCommonThoroughfares();
        }

        return this.thoroughfares;
    },

    /// <summary>
    /// A list of Title objects.
    /// </summary>
    get_titles : function()
    {
        if (this.titles == null)
        {
            this._resetTitles();
        }

        return this.titles;
    },
    set_titles : function(value)
    {
        this.titles = value;
    },

    /// <summary>
    /// The remaining text that could not be matched after the 
    /// parsing process has identified all other values.
    /// </summary>
    get_unmatchedText : function()
    {
        return this.unmatchedText;
    },
    set_unmatchedText : function(value)
    {
        this.unmatchedText = this._tokenDecode(value).trimStart();
    },
    
    //=============================================================================
    // Methods

    /// <summary>
    /// Emulate Remove method from grown-ups' version of StringBuilder...
    /// </summary>
    /// <param name="startIndex">The position in this instance where removal begins.</param>
    /// <param name="length">The number of characters to remove.</param>
    _removeText : function(workingText, startIndex, length)
    {
        var e = Function._validateParams(arguments, [{name: "workingText", mayBeNull: false, optional: false}, {name: "startIndex", mayBeNull: false, optional: false}, {name: "length", mayBeNull: true, optional: true}]);
        if (e) throw e;

        if (typeof length != 'number' || length == null)
        {
            length = workingText.length;
        }
        
        return workingText.substring(0, startIndex) + workingText.substring(startIndex+length);
    },

    /// <summary>
    /// Retrieve a list of titles from resources
    /// </summary>
    /// <returns>Enumerable list of Titles</returns>
    _getCommonFamilyNames : function()
    {
        var commonFamilyNamePrefix = "String";
        var commonFamilyNameIndex = 1;
        var commonFamilyNames = new Array();
        var commonFamilyName = CommonFamilyNamesResources[commonFamilyNamePrefix + commonFamilyNameIndex];
        
        while (commonFamilyName != null)
        {
            commonFamilyNames.push(commonFamilyName);
            commonFamilyNameIndex++;
            commonFamilyName = CommonFamilyNamesResources[commonFamilyNamePrefix + commonFamilyNameIndex];
        }
        
        return commonFamilyNames;
    },

    /// <summary>
    /// Retrieve a list of titles from resources
    /// </summary>
    /// <returns>Enumerable list of Titles</returns>
    _getCommonThoroughfares: function()
    {
        var commonThoroughfarePrefix = "String";
        var commonThoroughfareIndex = 1;
        var commonThoroughfares = new Array();
        var commonThoroughfare = CommonThoroughfaresResources[commonThoroughfarePrefix + commonThoroughfareIndex];
        
        while (commonThoroughfare != null)
        {
            commonThoroughfares.push(commonThoroughfare);
            commonThoroughfareIndex++;
            commonThoroughfare = CommonThoroughfaresResources[commonThoroughfarePrefix + commonThoroughfareIndex];
        }
        
        return commonThoroughfares;
    },

    /// <summary>
    /// Retrieve a list of titles from resources
    /// </summary>
    /// <returns>Enumerable list of Titles</returns>
    _getTitles : function()
    {
        var titlePrefix = "String";
        var titleIndex = 1;
        var titles = new Array();
        var title = TitlesResources[titlePrefix + titleIndex];
        
        while (title != null)
        {
            var titleParts = title.split(',');
            var newTitle = new Title(titleParts[0], eval("Gender." + titleParts[1]));
            titles.push(newTitle);
            titleIndex++;
            title = TitlesResources[titlePrefix + titleIndex];
        }
        
        return titles;
    },

    /// <summary>
    /// Gets the day value from the date
    /// </summary>
    /// <param name="dayValue">The day of the date.</param>    
    /// <param name="monthValue">The month of the date.</param>    
    /// <param name="yearValue">The year of the date.</param>   
    /// <returns>The day of the month</returns>    
    _getDayValueFromDate : function(dayValue, monthValue, yearValue)
    {
        // Fix bug # 5265. Javascript when a date is supplied with day greater than the days in the month        
        var dt = new Date(yearValue, monthValue, dayValue);
        
        if(dt.getMonth() == monthValue)
        {
            return dayValue;
        }
        else
        {
            return -1;
        }
    },
    
    /// <summary>
    /// Gets the month number from the month name.
    /// </summary>
    /// <param name="monthName">The name of the month. For example, if the month number is 3, the monthName is "March".</param>
    /// <param name="cultureInfo">The culture that should be used to parse the string.</param>
    /// <returns>The Month number</returns>
    _getMonthNumberFromMonthName : function(monthName, cultureInfo)
    {
        var index = this._findCaseInsensitiveEntry(Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames, monthName);
        return (index >= 0 ? index : -1);
    },

    /// <summary>
    /// Find entry an entry in supplied string array by case insensitive match
    /// </summary>
    /// <param name="values">values to search</param>
    /// <param name="item">item to search for</param>
    /// <returns>index in the array of the item ,or -1 if not found</returns>
    _findCaseInsensitiveEntry : function(values, item)
    {
        var compareLength;
        var lowerItem = item.toLowerCase();

        for (var index = 0; index < values.length; index++)
        {
            compareLength = item.length <= values[index].length ? item.length : values[index].length;
            if (values[index].toLowerCase().substring(0, compareLength) == lowerItem)
            {
                return index;
            }
        }

        return -1;
    },

    /// <summary>
    /// Strip out allowable date formatters that are incompatible with NhsDate
    /// </summary>
    /// <param name="dateString">String containing date</param>
    /// <returns>Clean date string</returns>
    _cleanDateString : function(dateString)
    {
        return dateString.replace(new RegExp('\\-', 'gi'), ' ').replace(new RegExp('\/', 'gi'), ' ');
    },

    /// <summary>
    /// Look through string for one of four variants of a date:
    /// 1) Day Month(numeric) Year
    /// 2) Day Month(text) Year
    /// 3) Day Month(numeric) OR Month(text) Year
    /// 4) Year only
    /// </summary>
    /// <param name="searchText">string to search</param>
    /// <returns>FoundText instance if located, otherwise null</returns>
    _checkForDate : function(searchText)
    {
        foundDate = this._checkForDayIntMonthYear(searchText);

        // Keep checking if necessary...
        if (foundDate == null)
        {
            foundDate = this._checkForDayTextMonthYear(searchText);
        }

        // Keep checking if necessary...
        if (foundDate == null)
        {
            foundDate = this._checkForIntOrTextMonthYear(searchText);
        }

        // Keep checking if necessary...
        if (foundDate == null)
        {
            foundDate = this._checkForYear(searchText);
        }

        return foundDate;
    },

    /// <summary>
    /// Look through string for Day Month(numeric) Year variant of a date:
    /// </summary>
    /// <param name="searchText">string to search</param>
    /// <returns>FoundText instance if located, otherwise null</returns>
    _checkForDayIntMonthYear : function(searchText)
    {
        var foundDate = this._parseForDate(searchText, new RegExp(this.DayIntMonthYearRegex, "i"));
        
        if (foundDate != null)
        {
            foundDate.set_type(FoundDataType.DateDayIntMonthYear);
        }

        return foundDate;
    },

    /// <summary>
    /// Look through string for Day Month(text) Year variant of a date:
    /// </summary>
    /// <param name="searchText">string to search</param>
    /// <returns>FoundText instance if located, otherwise null</returns>
    _checkForDayTextMonthYear : function(searchText)
    {
        var foundDate = this._parseForDate(searchText, new RegExp(this.DayTextMonthYearRegex, "i"));
        if (foundDate != null)
        {
            foundDate.set_type(FoundDataType.DateDayTextMonthYear);
        }

        return foundDate;
    },

    /// <summary>
    /// Look through string for Day Month(numeric) OR Month(text) Year variant of a date:
    /// </summary>
    /// <param name="searchText">string to search</param>
    /// <returns>FoundText instance if located, otherwise null</returns>
    _checkForIntOrTextMonthYear : function(searchText)
    {
        var foundDate = this._parseForDate(searchText, new RegExp(this.IntOrTextMonthYearRegex, "i"));
        if (foundDate != null)
        {
            // Simple way - but is it the best??
            if (foundDate.get_value().trim().length == 7)
            {
                foundDate.set_type(FoundDataType.DateIntMonthYear);
            }
            else
            {
                foundDate.set_type(FoundDataType.DateTextMonthYear);
            }
        }
        
        return foundDate;
    },

    /// <summary>
    /// Look through string for Year only variant of a date:
    /// </summary>
    /// <param name="searchText">string to search</param>
    /// <returns>FoundText instance if located, otherwise null</returns>
    _checkForYear : function(searchText)
    {
        var foundDate = this._parseForDate(searchText, new RegExp(this.YearRegex, "i"));
        if (foundDate != null)
        {
            foundDate.set_type(FoundDataType.DateYear);
        }

        return foundDate;
    },

    /// <summary>
    /// Apply a Regex on a word boundary, start or end of line and single word input.
    /// Remove match from this.workingText if found
    /// </summary>
    /// <param name="wordRegex">Regex to use</param>
    /// <returns>If found - text of match, otherwise string.Empty</returns>
    _parseRemoveWord : function(wordRegex)
    {
        var nonWordRegex = new RegExp("[,;!?:\\s\\-]", "gi");
        // var nonWordRegex = new RegExp("\\W", "gi");
        var textValue = this.workingText;
        var wordMatch = wordRegex.exec(textValue);

        if (wordMatch != null)
        {
            var wordMatchString = wordMatch[0].toString();
            var leftBound = wordMatch.index - 1 >= 0 ? wordMatch.index - 1 : wordMatch.index;
            var rightBound = wordMatch.index + wordMatchString.length <= textValue.length ? wordMatch.index + wordMatchString.length : textValue.length;

            // Whole string...
            if (textValue.length == wordMatchString.length)
            {
                this.workingText = this._removeText(this.workingText, wordMatch.index, wordMatchString.length);
                return wordMatchString;
            }

            // At beginning...
            else if (wordMatch.index == 0 && (nonWordRegex.test(textValue.charAt(wordMatchString.length)) == true || textValue.charAt(wordMatchString.length) == ' '))
            {
                this.workingText = this._removeText(this.workingText, wordMatch.index, wordMatchString.length + 1);
                return wordMatchString;
            }

            // At end...
            else if (wordMatch.index == textValue.length - wordMatchString.length && (nonWordRegex.test(textValue.charAt(wordMatch.index - 1)) == true || textValue.charAt(wordMatch.index - 1) == ' '))
            {
                this.workingText = this._removeText(this.workingText, wordMatch.index - 1, wordMatchString.length + 1);
                return wordMatchString;
            }

            // Embedded...
            else if ((nonWordRegex.test(textValue.charAt(leftBound)) == true || textValue.charAt(leftBound) == ' ') && (nonWordRegex.test(textValue.charAt(rightBound)) == true || textValue.charAt(rightBound) == ' '))
            {
                this.workingText = this._removeText(this.workingText, wordMatch.index - 1, wordMatchString.length + 1);
                return wordMatchString;
            }
        }

        return "";
    },

    /// <summary>
    /// Apply a Regex on a word boundary, start or end of line and single word input
    /// If found return FoundText struct with match details
    /// </summary>
    /// <param name="searchText">Text to search</param>
    /// <param name="wordRegex">Regex to use</param>
    /// <returns>FoundText struct if word found, otherwise null</returns>
    _parseForDate : function(searchText, wordRegex)
    {
        var wordMatch = wordRegex.exec(searchText);
        var whiteSpace = new RegExp("\\W");

        if (wordMatch != null)
        {
            var wordMatchString = wordMatch[0].toString();
            if ((searchText.length == wordMatchString.length) || (wordMatch.index == 0 && whiteSpace.test(searchText.charAt(wordMatchString.length)) == true) || (wordMatch.index == searchText.length - wordMatchString.length && whiteSpace.test(searchText.charAt(wordMatch.index - 1)) == true) || (whiteSpace.test(searchText.charAt(wordMatch.index - 1)) == true && whiteSpace.test(searchText.charAt(wordMatch.index + wordMatchString.length)) == true))
            {
                return new FoundText(wordMatchString, wordMatch.index);
            }
        }

        return null;
    },

    /// <summary>
    /// Removes any safe tokens and replaces with spaces
    /// </summary>
    /// <param name="safeToken">Safe token to use</param>
    /// <returns>Decoded string</returns>
    _tokenDecode : function(workingText)
    {
        return workingText.replace(new RegExp(this.safeToken, 'gi'), ' ');
    },

    /// <summary>
    /// Parses the string in the Parser.Text property and populates the equivalent properties with the results.
    /// </summary>
    parse : function()
    {
        if (this.text == null)
        {
            return;
        }

        if (this.informationFormat != null)
        {
            this._parseSplit();
        }
        else
        {
            this._parseRegex();
        }
    },

    /// <summary>
    /// Search for delimiters and if present, replace all enclosed space chars with a safe token
    /// </summary>
    /// <returns>Encoded string</returns>
    _tokenEncode : function(workingText)
    {
        var encodedText = new Sys.StringBuilder();
        var firstStartDelimiter = workingText.indexOf(this.get_startGroupDelimiter());
        var lastEndDelimiter = workingText.lastIndexOf(this.get_endGroupDelimiter());
        var insideDelimitedArea = false;

        if (firstStartDelimiter != -1 && lastEndDelimiter != -1 && firstStartDelimiter != lastEndDelimiter)
        {
            for (var characterIndex = 0; characterIndex < workingText.length; characterIndex++)
            {
                if (insideDelimitedArea == true && workingText.charAt(characterIndex) == this.get_endGroupDelimiter())
                {
                    insideDelimitedArea = false;
                }
                else if (workingText.charAt(characterIndex) == this.get_startGroupDelimiter())
                {
                    insideDelimitedArea = true;
                }

                if (insideDelimitedArea == true && workingText.charAt(characterIndex) == ' ')
                {
                    encodedText.append(this.safeToken);
                }
                else if (workingText.charAt(characterIndex) != this.get_startGroupDelimiter() && workingText.charAt(characterIndex) != this.get_endGroupDelimiter())
                {
                    encodedText.append(workingText.charAt(characterIndex));
                }
            }
        }

        return encodedText.toString();
    },

    /// <summary>
    /// Make sure that a potential Age value is less than or equal to max age
    /// </summary>
    /// <param name="potentialAge">Potential age value</param>
    /// <returns>true if valid age, otherwise false</returns>
    _ageCheck : function(potentialAge)
    {
        if (potentialAge <= this.get_maximumAge() && potentialAge > 0)
        {
            return true;
        }

        return false;
    },

    /// <summary>
    /// Make sure that a potential DOB value less than or equal to current year 
    /// and greater than current year less max age
    /// </summary>
    /// <param name="potentialDateOfBirth">Potential date of birth</param>
    /// <returns>true if valid date of birth, otherwise false</returns>
    _DOBCheck : function(potentialDateOfBirth)
    {
        var currentYear = new Date().getFullYear();
        var potentialYear = -1;
        
        if (potentialDateOfBirth.get_dateType() == DateType.Exact)
        {
            potentialYear = potentialDateOfBirth.get_dateValue().getFullYear();
        }
        else
        {
            potentialYear = potentialDateOfBirth.get_year();
        }

        if (potentialYear <= currentYear && potentialYear > (currentYear - this.get_maximumAge()))
        {
            return true;
        }

        return false;
    },

    /// <summary>
    /// Parse this.workingText for Family Name and remove
    /// </summary>
    _parseRemoveFamilyName : function()
    {
        var familyNameRegex = new RegExp("([A-Z\\-\\'" + this.safeToken + "]+)", "i");
        var familyNameMatch = this._parseRemoveWord(familyNameRegex);

        if (familyNameMatch.length > 0)
        {
            this.set_familyName(familyNameMatch);
        }
    },

    /// <summary>
    /// Parse this.workingText for GivenName followed by FamilyName and remove
    /// </summary>
    _parseRemoveGivenNameFamilyName : function()
    {
        var givenNameFamilyNameRegex = new RegExp("([A-Z\\-\\'" + this.safeToken + "]+)\\s([A-Z\\-\\'" + this.safeToken + "]+)", "i");
        var givenNameFamilyNameMatch = this._parseRemoveWord(givenNameFamilyNameRegex);

        if (givenNameFamilyNameMatch.length > 0)
        {
            names = givenNameFamilyNameMatch.split(' ');
            this.set_givenName(names[0]);
            this.set_familyName(names[1]);
        }
    },

    /// <summary>
    /// Parse this.workingText for Age and remove
    /// </summary>
    _parseRemoveAge : function()
    {
        var ageRegex = new RegExp("\\b\\d\\d?\\d?\\b");
        var ageMatch = ageRegex.exec(this.workingText);

        if (ageMatch != null)
        {
            if (((ageMatch.index + ageMatch[0].length) < this.workingText.length && this.workingText.charAt(ageMatch.index + ageMatch[0].length) != '-') || (ageMatch.index + ageMatch[0].length) == this.workingText.length)
            {
                var potentialAge = parseInt(ageMatch[0], 10);
                if (this._ageCheck(potentialAge) == true)
                {
                    this._parseRemoveWord(ageRegex);
                    this.set_age(potentialAge);
                }
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Address - House Number followed by Street Name and remove
    /// </summary>
    _parseRemoveAddressHouseNumberStreetName : function()
    {
        var thoroughfare = "";

        // First check to see if we have a recognised thoroughfare...
        thoroughfare = this._checkForThoroughfare();

        var addressRegex = new RegExp("\\d\\d?\\d?\\d?(\\s\\D+)+", "i");

        var addressPart = addressRegex.exec(this.workingText);
        if (addressPart == null)
        {
            return;
        }
        
        addressPart = addressPart[0];
        
        var addressMatch = "";

        if (addressPart.length > 0)
        {
            if (thoroughfare.length > 0)
            {
                addressPart = addressPart.substring(0, addressPart.toLowerCase().indexOf(thoroughfare.toLowerCase()) + thoroughfare.length);

                var thoroughfareRegex = new RegExp(addressPart, "i");

                addressMatch = this._parseRemoveWord(thoroughfareRegex);

                if (addressMatch.length > 0)
                {
                    this.set_address(addressMatch);
                }
            }
            else
            {
                addressMatch = this._parseRemoveWord(addressRegex);
                this.set_address(addressMatch);
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for  delimiter-embedded Address - House Number followed by Street Name and remove
    /// </summary>
    _parseRemoveEmbeddedAddressHouseNumberStreetName : function()
    {
        var addressRegex = new RegExp("\\d\\d?\\d?\\d?(" + this.safeToken + "[A-Z]+)+", "gi");

        var addressPart = addressRegex.exec(this.workingText);
        if (addressPart == null)
        {
            return;
        }
        
        addressPart = addressPart[0];
        
        var addressMatch = "";

        if (addressPart.length > 0)
        {
            addressMatch = this._parseRemoveWord(new RegExp(addressPart, "gi"));
            this.set_address(addressMatch);
        }
    },

    /// <summary>
    /// Perform additional checks on a crude date match string
    /// </summary>
    _validDateParseString : function(potentialDateString)
    {
        var nhsDateReturn = null;
        var potentialDateTime = Date.parseInvariant(potentialDateString, "dd-MMM-yyyy", "dd MMM yyyy", "dd/MMM/yyyy", "dd-MM-yyyy", "dd MM yyyy", "dd/MM/yyyy");

        if (potentialDateTime != null)
        {
            nhsDateReturn = new NhsDate(new Date(potentialDateTime));
        }

        return nhsDateReturn;
    },

    /// <summary>
    /// Perform additional checks on crude date match numeric values
    /// </summary>
    _validDateParseInt : function(yearValue, monthValue, dayValue)
    {
        var nhsDateReturn = null;
        try
        {
            nhsDateReturn = new NhsDate(new Date(yearValue, monthValue, dayValue));
        }
        catch (e)
        {
            nhsDateReturn = null;
        }

        return nhsDateReturn;
    },

    /// <summary>
    /// Parse this.workingText for Date of Birth and remove - Year only
    /// </summary>
    _parseRemoveDOBYear : function()
    {
        var textValue = this.workingText;
        var foundDate = this._checkForYear(textValue);

        if (foundDate != null)
        {
            var potentialDateOfBirth = new NhsDate(parseInt(foundDate.value, 10));
            if (this._DOBCheck(potentialDateOfBirth) == true)
            {
                this._parseRemoveWord(new RegExp(foundDate.value, "i"));
                this.set_dateOfBirth(potentialDateOfBirth);
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Date of Birth and remove - numeric or text Month followed by Year 
    /// </summary>
    _parseRemoveDOBIntOrTextMonthYear : function()
    {
        var textValue = this.workingText;
        var foundDate = this._checkForIntOrTextMonthYear(textValue);

        if (foundDate != null)
        {
            var dateParts = this._cleanDateString(foundDate.value).split(' ');
            var monthValue = parseInt(dateParts[0], 10);
            var yearValue = parseInt(dateParts[1], 10);
            if (yearValue <= 0)
            {
                return;
            }
            var potentialDateOfBirth;
            if (!isNaN(monthValue))
            {
                if (monthValue <= 0 || monthValue > 12)
                {
                    return;
                }
                potentialDateOfBirth = new NhsDate(yearValue, monthValue);
            }
            else
            {
                potentialDateOfBirth = new NhsDate(parseInt(dateParts[1], 10), this._getMonthNumberFromMonthName(dateParts[0]) + 1);
            }

            if (this._DOBCheck(potentialDateOfBirth) == true)
            {
                this._parseRemoveWord(new RegExp(foundDate.value, "i"));
                this.set_dateOfBirth(potentialDateOfBirth);
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Date of Birth and remove - numeric Day followed by text Month followed by Year 
    /// </summary>
    _parseRemoveDOBDayTextMonthYear : function()
    {
        var textValue = this.workingText;
        var foundDate = this._checkForDayTextMonthYear(textValue);

        if (foundDate != null)
        {
            // Remove date pattern even if it turns out to be invalid date e.g. 31-Nov-YYYY, 29-Feb-YYYY etc...
            this._parseRemoveWord(new RegExp(foundDate.value, "i"));

            var dateParts = this._cleanDateString(foundDate.value).split(' ');
            
            var yearValue = parseInt(dateParts[2], 10);
            var monthValue = this._getMonthNumberFromMonthName(dateParts[1]);
            var dayValue = this._getDayValueFromDate(parseInt(dateParts[0], 10), monthValue, yearValue);
            if (yearValue <= 0 || monthValue < 0 || monthValue > 12 || dayValue <= 0 || yearValue < (new Date().getFullYear() - this.maximumAge))
            {
                this.set_unmatchedText(this.get_unmatchedText() + " " + foundDate.value);
                return;
            }

            var potentialDateOfBirth = this._validDateParseInt(yearValue, monthValue, dayValue);
            if (potentialDateOfBirth != null && this._DOBCheck(potentialDateOfBirth) == true)
            {
                this.set_dateOfBirth(potentialDateOfBirth);
            }
            else
            {
                this.set_unmatchedText(this.get_unmatchedText() + " " + foundDate.value);
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Date of Birth and remove - numeric Day followed by numeric Month followed by Year 
    /// </summary>
    _parseRemoveDOBDayIntMonthYear : function()
    {
        var textValue = this.workingText;
        var foundDate = this._checkForDayIntMonthYear(textValue);

        if (foundDate != null)
        {
            // Remove date pattern even if it turns out to be invalid date e.g. 31-11-YYYY, 29-02-YYYY etc...
            this._parseRemoveWord(new RegExp(foundDate.value, "i"));
            
            var dateParts = this._cleanDateString(foundDate.value).split(' ');
            var yearValue = parseInt(dateParts[2], 10);
            var monthValue = parseInt(dateParts[1], 10);
            var dayValue = this._getDayValueFromDate(parseInt(dateParts[0], 10), monthValue, yearValue);
            if (yearValue <= 0 || monthValue < 0 || monthValue > 12 || dayValue <= 0 || yearValue < (new Date().getFullYear() - this.maximumAge))
            {
                this.set_unmatchedText(this.get_unmatchedText() + " " + foundDate.value);
                return;
            }

            var potentialDateOfBirth = this._validDateParseString(foundDate.value);
            if (potentialDateOfBirth != null && this._DOBCheck(potentialDateOfBirth) == true)
            {
                this.set_dateOfBirth(potentialDateOfBirth);
            }
            else
            {
                this.set_unmatchedText(this.get_unmatchedText() + " " + foundDate.value);
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Date of Birth range and remove
    /// </summary>
    _parseRemoveDOBRange : function()
    {
        var textValue = this.workingText;
        var foundFromDate = this._checkForDate(textValue);

        if (foundFromDate == null)
        {
            return;
        }

        var remainingText = textValue.substring(foundFromDate.get_start() + foundFromDate.get_length());
        if (remainingText.length < 4)
        {
            return;
        }

        var foundToDate = this._checkForDate(remainingText);

        if (foundToDate != null)
        {
            var separatorText = textValue.substr(foundFromDate.get_start() + foundFromDate.get_length(), textValue.length - (foundFromDate.get_start() + foundFromDate.get_length()) - (remainingText.length - foundToDate.get_start()));

            if (separatorText.trim() == "-")
            {
                var potentialLowerDateOfBirth; 
                var potentialUpperDateOfBirth;
                
                if (foundFromDate.get_type() == FoundDataType.DateDayIntMonthYear || foundFromDate.get_type() == FoundDataType.DateDayTextMonthYear)
                {
                    potentialLowerDateOfBirth = this._validDateParseString(this._cleanDateString(foundToDate.get_value()));
                }
                else
                {
                    potentialLowerDateOfBirth = new NhsDate(this._cleanDateString(foundFromDate.get_value()));
                }

                if (foundToDate.get_type() == FoundDataType.DateDayIntMonthYear || foundToDate.get_type() == FoundDataType.DateDayTextMonthYear)
                {
                    potentialUpperDateOfBirth = this._validDateParseString(this._cleanDateString(foundFromDate.get_value()));
                }
                else
                {
                    potentialUpperDateOfBirth = new NhsDate(this._cleanDateString(foundToDate.get_value()));
                }

                if (potentialLowerDateOfBirth != null && potentialUpperDateOfBirth != null)
                {
                    if (this._DOBCheck(potentialLowerDateOfBirth) == true && this._DOBCheck(potentialUpperDateOfBirth) == true)
                    {
                        this._parseRemoveWord(new RegExp(foundFromDate.get_value() + separatorText + foundToDate.get_value(), "i"));
                        this.set_dateOfBirth(potentialLowerDateOfBirth);
                        this.set_dateOfBirthUpper(potentialUpperDateOfBirth);
                    }
                }
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Address House Name followed by Street Name and remove
    /// </summary>
    _parseRemoveAddressHouseNameStreetName : function()
    {
        var thoroughfare = "";

        // First check to see if we have a recognised thoroughfare...
        thoroughfare = this._checkForThoroughfare();

        if (thoroughfare.length > 0)
        {
            var thoroughfareRegexPattern = "([A-Z\\'" + this.safeToken + "]+\\s){2,}" + thoroughfare;
            thoroughfareRegex = new RegExp(thoroughfareRegexPattern, "i");

            var thoroughfareMatch = this._parseRemoveWord(thoroughfareRegex);

            if (thoroughfareMatch.length > 0)
            {
                this.set_address(thoroughfareMatch);
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Gender and remove - Male
    /// </summary>
    _parseRemoveGenderMale : function()
    {
        var genderRegex = new RegExp(ParserResources.Male, "i");
        if (this._parseRemoveWord(genderRegex).indexOf(ParserResources.Male) != -1)
        {
            this.set_gender(Gender.Male);
        }
    },

    /// <summary>
    /// Check if this.workingText contains one of the standard titles
    /// </summary>
    /// <returns>Title if found, otherwise null</returns>
    _checkForTitle : function(titleValue)
    {
        var titles = this.get_titles();
        for (var titleIndex = 0; titleIndex < titles.length; titleIndex++)
        {
            var title = titles[titleIndex];
            var titleRegexString = "\\b" + title.get_name() + "\\b";
            var titleRegex = new RegExp(titleRegexString, "i");
            if (titleRegex.test(titleValue) == true)
            {
                return title;
            }
        }

        return null;
    },

    /// <summary>
    /// Put together a regex string to match any of the current titles
    /// </summary>
    /// <returns>Regex pattern for any of the current titles</returns>
    _buildTitleRegexString : function()
    {
        var titleRegex = new Sys.StringBuilder();
        var currentTitle;
        var delimiter = "|";
        for (var titleIndex = 0; titleIndex < this.titles.length; titleIndex++)
        {
            currentTitle = this.titles[titleIndex];
            if (titleIndex == this.titles.length - 1)
            {
                titleRegex.append(currentTitle.get_name());
            }
            else
            {
                titleRegex.append(currentTitle.get_name() + delimiter);
            }
        }

        return titleRegex.toString();
    },


    /// <summary>
    /// Check if this.workingText contains one of the standard thoroughfares
    /// </summary>
    /// <returns>Thoroughfare if found, otherwise null</returns>
    _checkForThoroughfare : function()
    {
        var thoroughfares = this.get_thoroughfares();
        for (var thoroughfareIndex = 0; thoroughfareIndex < thoroughfares.length; thoroughfareIndex++)
        {
            var thoroughfare = thoroughfares[thoroughfareIndex];
            var thoroughfareRegexString = "\\b" + thoroughfare + "\\b";
            var thoroughfareRegex = new RegExp(thoroughfareRegexString, "i");
            if (thoroughfareRegex.test(this.workingText) == true)
            {
                return thoroughfare;
            }
        }

        return "";
    },

    /// <summary>
    /// Check if this.workingText contains one of the standard thoroughfares within embedded text
    /// </summary>
    /// <returns>Thoroughfare if found, otherwise null</returns>
    _checkForEmbeddedThoroughfare : function()
    {
        var thoroughfares = this.get_thoroughfares();
        for (var thoroughfareIndex = 0; thoroughfareIndex < thoroughfares.length; thoroughfareIndex++)
        {
            var thoroughfare = thoroughfares[thoroughfareIndex];
            var thoroughfareRegexString = this.safeToken + thoroughfare;
            var thoroughfareRegex = new RegExp(thoroughfareRegexString, "i");
            if (thoroughfareRegex.test(this.workingText) == true)
            {
                return thoroughfare;
            }
        }

        return "";
    },

    /// <summary>
    /// Parse this.workingText for Title followed by FamilyName and remove
    /// </summary>
    _parseRemoveTitleFamilyName : function()
    {
        var title;

        var titleRegexString = this._buildTitleRegexString();

        if (titleRegexString.length > 0)
        {
            var fullNameRegexPattern = "(" + titleRegexString + "+\\.?)\\s([A-Z\\-\\'" + this.safeToken + "]+)";
            var fullNameRegex = new RegExp(fullNameRegexPattern, "i");

            var fullNameMatch = this._parseRemoveWord(fullNameRegex);

            if (fullNameMatch.length > 0)
            {
                title = this._checkForTitle(fullNameMatch);
                var nameParts = fullNameMatch.split(' ');
                this.set_title(nameParts[0]);
                this.set_familyName(nameParts[1]);
                if (this.get_gender() == Gender.None && title.get_gender() != Gender.None)
                {
                    this.set_gender(title.get_gender());
                }
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Title followed by Given Name followed by Family Name and remove
    /// </summary>
    _parseRemoveFullName : function()
    {
        var title;

        var titleRegexString = this._buildTitleRegexString();

        if (titleRegexString.length > 0)
        {
            var fullNameRegexPattern = "(" + titleRegexString + "+\\.?)\\s([A-Z\\-\\'" + this.safeToken + "]+)\\s([A-Z\\-\\'" + this.safeToken + "]+)";
            var fullNameRegex = new RegExp(fullNameRegexPattern, "i");

            var fullNameMatch = this._parseRemoveWord(fullNameRegex);

            if (fullNameMatch.length > 0)
            {
                title = this._checkForTitle(fullNameMatch);
                nameParts = fullNameMatch.split(' ');
                this.set_title(nameParts[0]);
                this.set_givenName(nameParts[1]);
                this.set_familyName(nameParts[2]);
                if (this.get_gender() == Gender.None && title.get_gender() != Gender.None)
                {
                    this.set_gender(title.get_gender());
                }
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Age Range and remove
    /// </summary>
    _parseRemoveAgeRange : function()
    {
        var ageRangeRegex = new RegExp("(\\b\\d\\d?\\b)(\\-(\\b\\d\\d?\\d?\\b))");
        var ageRangeMatch = ageRangeRegex.exec(this.workingText);

        if (ageRangeMatch != null)
        {
            if (((ageRangeMatch.index + ageRangeMatch[0].length) < this.workingText.length && this.workingText.charAt(ageRangeMatch.index + ageRangeMatch[0].length) != '-') || (ageRangeMatch.index + ageRangeMatch[0].length) == this.workingText.length)
            {
                var ages = ageRangeMatch[0].split('-');
                var lowerAge = parseInt(ages[0], 10);
                var upperAge = parseInt(ages[1], 10);
                if (this._ageCheck(lowerAge) == true && this._ageCheck(upperAge) == true)
                {
                    this._parseRemoveWord(ageRangeRegex);
                    this.set_age(lowerAge);
                    this.set_ageUpper(upperAge);
                }
            }
        }
    },

    /// <summary>
    /// Parse this.workingText for Postcode and remove
    /// </summary>
    _parseRemovePostcode : function()
    {
        var postcodeRegex = new RegExp("(([A-Z][A-Z]?\\d\\d?)|([A-Z][A-Z]?\\d[A-Z]?))(\\s?\\d[A-Z][A-Z])", "i");
        var postcodeMatch = this._parseRemoveWord(postcodeRegex);

        if (postcodeMatch.length > 0)
        {
            this.set_postcode(postcodeMatch);
        }
    },

    /// <summary>
    /// Parse this.workingText for NHS Number and remove
    /// </summary>
    _parseRemoveNhsNumber : function()
    {
        var nhsNumberRegex = new RegExp("(\\b\\d\\d\\d[\\s\\-]?\\d\\d\\d[\\s\\-]?\\d\\d\\d\\d)");
        var nhsNumberMatch = this._parseRemoveWord(nhsNumberRegex);

        if (nhsNumberMatch.length > 0)
        {
            this.set_nhsNumber(nhsNumberMatch);
        }
    },

    /// <summary>
    /// Parse this.workingText for Gender and remove - Female
    /// </summary>
    _parseRemoveGenderFemale : function()
    {
        var genderRegex = new RegExp(ParserResources.Female, "i");
        if (this._parseRemoveWord(genderRegex).toUpperCase().indexOf(ParserResources.Female.toUpperCase()) != -1)
        {
            this.set_gender(Gender.Female);
        }
    },

    /// <summary>
    /// Refresh the isCommonFamilyName flag
    /// </summary>
    _updateIsCommonFamilyName : function()
    {
        if (this.get_familyName() == null)
        {
            return;
        }

        var familyName;
        
        this.familyNameIsCommon = false;
        
        for (var familyNameIndex = 0; familyNameIndex < this.get_commonFamilyNames().length; familyNameIndex++)
        {
            familyName = this.get_commonFamilyNames()[familyNameIndex];
            if (familyName.toLowerCase() == this.get_familyName().toLowerCase())
            {
                this.familyNameIsCommon = true;
            }
        }
    },

    /// <summary>
    /// Parses the string in the Parser.Text property by splitting on the InformationDelimiter char and
    /// using the InformationFormat array to populates the corresponding properties with the results.
    /// </summary>
    _parseSplit : function()
    {
        var fields = this.get_text().split(this.get_informationDelimiter());
        var currentItemIndex = 0;

        while (currentItemIndex < fields.length && currentItemIndex < this.get_informationFormat().length)
        {
            var unmatchedResult = this._setProperty(this.get_informationFormat()[currentItemIndex], fields[currentItemIndex]).trim();
            if (unmatchedResult.length > 0)
            {
                this.set_unmatchedText(this.get_unmatchedText() + " " + unmatchedResult);
            }
            currentItemIndex++;
        }
    },

    /// <summary>
    /// Set a property using a PatientSearch.Information value
    /// </summary>
    /// <param name="patientSearchInfo">PatientSearch.Information value</param>
    /// <param name="valueToSet">Value to set</param>
    _setProperty : function(patientSearchInfo, valueToSet)
    {
        try
        {
            switch (patientSearchInfo)
            {
                case Information.Address:
                    this.set_address(valueToSet);
                    break;
                case Information.Age:
                    this.set_age(parseInt(valueToSet, 10));
                    break;
                case Information.DateOfBirth:
                    this.set_dateOfBirth(new NhsDate(valueToSet));
                    break;
                case Information.FamilyName:
                    this.set_familyName(valueToSet);
                    break;
                case Information.Gender:
                    this.set_gender(this._lookUpGender(valueToSet.trim().toUpperCase()));
                    break;
                case Information.GivenName:
                    this.set_givenName(valueToSet);
                    break;
                case Information.NhsNumber:
                    this.set_nhsNumber(valueToSet);
                    break;
                case Information.Postcode:
                    this.set_postcode(valueToSet);
                    break;
                case Information.Title:
                    this.set_title(valueToSet);
                    break;
                default:
                    return valueToSet;
                    break;
            }
        }
        catch (e)
        {
            return valueToSet;
        }
        
        return "";
    },

    _lookUpGender : function(value)
    {
        if (value == ParserResources.Male.toUpperCase())
        {
            return Gender.Male;
        }
        else if (value == ParserResources.Female.toUpperCase())
        {
            return Gender.Female;
        }

        return Gender.None;
    },

    /// <summary>
    /// Parses the string in the Parser.Text property using Regexs and populates the equivalent properties with the results.
    /// </summary>
    _parseRegex : function()
    {
        var startDelimiterPosition = this.get_text().indexOf(this.get_startGroupDelimiter());
        var endDelimiterPosition = this.get_text().lastIndexOf(this.get_endGroupDelimiter());

        if (startDelimiterPosition != -1 && endDelimiterPosition != -1 && startDelimiterPosition != endDelimiterPosition)
        {
            this.workingText = this._tokenEncode(this.get_text());
        }
        else
        {
            this.workingText = this.get_text();
        }

        this._resetAllProperties();

        this._parseRemoveGenderFemale();
        this._parseRemoveNhsNumber();
        this._parseRemovePostcode();
        this._parseRemoveAgeRange();
        this._parseRemoveFullName();
        this._parseRemoveTitleFamilyName();
        this._parseRemoveGenderMale();
        if (this.get_familyName() != null)
        {
            this._parseRemoveAddressHouseNameStreetName();
        }

        this._parseRemoveDOBRange();
        this._parseRemoveDOBDayIntMonthYear();
        this._parseRemoveDOBDayTextMonthYear();
        this._parseRemoveDOBIntOrTextMonthYear();
        this._parseRemoveDOBYear();
        this._parseRemoveEmbeddedAddressHouseNumberStreetName();
        if (this.get_address() == null)
        {
            this._parseRemoveAddressHouseNumberStreetName();
        }
        this._parseRemoveAge();
        if (this.get_familyName() == null)
        {
            this._parseRemoveGivenNameFamilyName();
        }
        
        // If we still don't have a family name...
        if (this.get_familyName() == null)
        {
            this._parseRemoveFamilyName();
        }

        if (this.workingText.length > 0)
        {
            this.set_unmatchedText(this.get_unmatchedText() + " " + this.workingText.trim());
        }
    },

    /// <summary>
    /// Checks that all properties corresponding to Information 
    /// enums marked as mandatory in MandatoryInformation have been set
    /// </summary>
    /// <returns>true if all mandatory properties have been set, otherwise false</returns>
    _allMandatoryInformationEntered : function()
    {
        var setFlags = 0;
        var allFlags = 0;
        var patientSearchInfo;
        
        for (var informationIndex = 0; informationIndex < this.get_mandatoryInformation().length; informationIndex++)
        {
            patientSearchInfo = this.get_mandatoryInformation()[informationIndex];
            allFlags = allFlags | patientSearchInfo;
            if (this._propertyHasBeenSet(patientSearchInfo) == true)
            {
                setFlags = setFlags | patientSearchInfo;
            }
        }

        if (allFlags == setFlags)
        {
            return true;
        }

        return false;
    },

    /// <summary>
    /// Initialise or reset the commonFamilyNames list
    /// </summary>
    _resetCommonFamilyNames : function()
    {
        if (this.commonFamilyNames == null)
        {
            this.commonFamilyNames = new Array();
        }
        else
        {
            this.commonFamilyNames.Clear();
        }

        this.commonFamilyNames.AddRange(this._getCommonFamilyNames());
    },

    /// <summary>
    /// Checks whether a property corresponding to a
    /// Information enumeration value has been set
    /// </summary>
    /// <param name="patientSearchInfo">Information corresponding to a property</param>
    /// <returns>true if property has been set, otherwise false</returns>
    _propertyHasBeenSet : function(patientSearchInfo)
    {
        switch (patientSearchInfo)
        {
            case Information.Address:
                if (this.get_address() != null)
                {
                    return true;
                }
                
                break;
            case Information.Age:
                if (this.get_age() > -1)
                {
                    return true;
                }

                break;
            case Information.DateOfBirth:
                if (this.get_dateOfBirth().get_isNull() != DateType.Null)
                {
                    return true;
                }

                break;
            case Information.FamilyName:
                if (this.get_familyName() != null)
                {
                    return true;
                }

                break;
            case Information.Gender:
                if (this.get_gender() != Gender.None)
                {
                    return true;
                }

                break;
            case Information.GivenName:
                if (this.get_givenName() != null)
                {
                    return true;
                }

                break;
            case Information.NhsNumber:
                if (this.get_nhsNumber() != null)
                {
                    return true;
                }

                break;
            case Information.Postcode:
                if (this.get_postcode() != null)
                {
                    return true;
                }

                break;
            case Information.Title:
                if (this.get_title() != null)
                {
                    return true;
                }

                break;
            default:
                return false;
                break;
        }

        return false;
    },

    /// <summary>
    /// (Re)Initialise all the properties
    /// </summary>
    _resetAllProperties : function()
    {
        this.address = null;
        this.age = -1;
        this.ageUpper = -1;
        this.dateOfBirth = new NhsDate();
        this.dateOfBirth.set_dateType(DateType.Null);
        this.dateOfBirthUpper = new NhsDate();
        this.dateOfBirthUpper.set_dateType(DateType.Null);
        this.familyName = null;
        this.gender = Gender.None;
        this.givenName = null;
        this.nhsNumber = null;
        this.postcode = null;
        this.title = null;
        this.unmatchedText = "";
        
        this._updateIsCommonFamilyName();
    },

    /// <summary>
    /// Initialise or reset the titles list
    /// </summary>
    _resetTitles : function()
    {
        if (this.titles == null)
        {
            this.titles = new Array();
        }
        else
        {
            this.titles.Clear();
        }

        this.titles = this._getTitles();
    }
};

NhsCui.Toolkit.Web.Parser.registerClass('NhsCui.Toolkit.Web.Parser', Sys.Component);


// TAKEN FROM DATEINPUTBOX.JS AS PARSER USES THE AJAX DATE EXTENSIONS TOO...GMM

// This replacement of the Atlas 1.0 RTM function _getAbbrMonthIndex is due to a bug in Atlas
// This bug has been logged in their ProductStudio DB #61157
Sys.CultureInfo.prototype._getAbbrMonthIndex = function(value)
{
    if (!this._upperAbbrMonths)
    {
        this._upperAbbrMonths = this._toUpperArray(this.dateTimeFormat.AbbreviatedMonthNames);
    }
        
    return Array.indexOf(this._upperAbbrMonths, this._toUpper(value));    
};

