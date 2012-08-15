//-----------------------------------------------------------------------
// <copyright file="NhsDate.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary></summary>
//-----------------------------------------------------------------------

// Hand ported from C# version on 2006-12-19 & 2007-01-02. 
//
// If the C# version has been updated since then, and this file has not had 
// the same changes made, then it will be out of sync.

Type.registerNamespace("NhsCui.Toolkit.Web");

var DateType = function() {
    /// <summary>
    /// DateType is a public enumeration of date type values
    /// </summary>
};
DateType.prototype = {      

        /// <summary>
        /// Date should be treated as exact
        /// </summary>
        Exact:0,

        /// <summary>
        /// Date is to be treated as a just a year
        /// </summary>
        Year:1,

        /// <summary>
        /// Date is to be treated as a combination of Month and Year
        /// </summary>
        YearMonth:2,
        
        /// <summary>
        /// Date should be treated as a named null value with the NullIndex property
        /// giving the index of the name in the NullStrings property
        /// </summary>
        NullIndex:3,

        /// <summary>
        /// Date should be treated as approximate
        /// </summary>
        Approximate:4,

        /// <summary>
        /// Date should be treated as a true null
        /// </summary>
        Null:5
};
DateType.registerEnum("DateType");


var NhsDate = NhsCui.Toolkit.Web.NhsDate = function() {
    NhsCui.Toolkit.Web.NhsDate.initializeBase(this);


    /// <summary>
    /// The DateType that represents the date (defaults to DateType.Exact)
    /// </summary>
    this._dateType = DateType.Exact;
        
    /// <summary>
    /// The exact date value (defaults to new Date())
    /// </summary>
    this._dateValue = new Date();
        
    /// <summary>
    /// The month of a DateType.YearMonth date
    /// </summary>
    /// <remarks>Defaults to 1</remarks>
    this._month = 1;                   
        
    /// <summary>
    /// The year of a DateType.Year or DateType.YearMonth.
    /// </summary>
    /// <remarks>Defaults to 0</remarks>
    this._year = 0;
        
    /// <summary>
    /// A number identifying a null type. The index has no meaning in and of itself. The meaning is implied by the UI control.
    /// </summary>
    /// <remarks>Defaults to -1</remarks>
    this._nullIndex = -1;
    
    
    // Implement overloaded constructors
    if(arguments.length === 1)
    {
        if(typeof arguments[0] === "object" && arguments[0].constructor==Date)
        {
            this._dateValue = arguments[0];
            this._dateType = DateType.Exact;        
        }
        else if(typeof arguments[0]=="number")
        {
            this._year = arguments[0];
            this._dateType = DateType.Year;        
        }
        else if(typeof arguments[0] === "string")
        {
            var nd = NhsDate.parse(arguments[0]);

            this._dateValue = nd._dateValue;
            this._dateType = nd._dateType;
            this._month = nd._month;
            this._year = nd._year;            
            this._nullIndex = nd._nullIndex;        
        }
    }
    else if(arguments.length === 2)
    {       
        if(typeof arguments[0] === "number" && arguments[0] >= 1000 &&
                typeof arguments[1] === "number")
        {
            this._dateType = DateType.YearMonth;
            this._year = arguments[0];
            this._month = arguments[1];
        }
    }
};
    
NhsDate.AddInstructionValidRegExFormat = "([+-]?\\d{1,2}\[#0##1##2##3#\])+";
//NhsDate.AddInstructionProcessingRegExFormat = "([-+]?\\d+w)/g|([-+]?\\d+m)/g|([-+]?\\d+d)/g|([-+]?\\d+y)/g";
NhsDate.NonDigitCharactersRegEx=/\D/g;

NhsDate.isAddInstruction = function (value)
{
    /// <summary>
    /// Check that value is a valid add instruction that could be passed to the Add method
    /// </summary>
    /// <param name="value">The string to check for an add instruction</param>
    /// <returns>True if the string is a valid value for the Add method</returns>

    var e = Function._validateParams(arguments, [
        {name: "value", type: String, mayBeNull: false, optional: false}
    ]);
    if (e) throw e;    

    var shortcut = new RegExp(NhsDate._resolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);
    
    // Trim out all the spaces and check the possible instruction against a RegEx
    var matches = shortcut.exec( value.replace(" ", ""));

    // If we have matches check that the the ext macthes is the same length as the input text. 
    // If it is not that means that some of the text was invalid
    if (matches != null && matches.length > 0){
        if (matches[0].length == value.replace(" ", "").length){
            return true;
        }
        else{
            return false;
        }
    }
    else{
        return false;
    }
};

NhsDate.parse = function(date)
{
    /// <summary>
    /// Parses a string in the format returned by ToString and returns a new NhsDate object
    /// </summary>
    /// <param name="date">The date to parse</param>
    /// <param name="cultureInfo">The culture that should be used to parse the string.</param>
    /// <returns>NhsDate object</returns>
    /// <exception cref="Sys.ArgumentNullException">date is null</exception>
    /// <exception cref="NhsError.FormatException">date is not in a recognised format</exception>
    
    var e = Function._validateParams(arguments, [
        {name: "date", type: String, mayBeNull: true, optional: false}
    ]);
    if (e) throw e;     
    
    var newNhsDate = NhsDate.tryParse(date);
    
    if(!newNhsDate)
    {
        throw NhsError.formatException(NhsDateResources.ParseCalledWithBadFormat);
    }
    
    return newNhsDate;
};
    
NhsDate.tryParse = function(date)
{
    /// <summary>
    /// Converts the specified string representation of a logical value to its Date equivalent. A return value indicates whether the conversion succeeded or failed. 
    /// </summary>
    /// <param name="date">a string containing the value to convert</param>
    /// <param name="result">a container for a successfully parsed date</param>
    /// <returns>parsed result if value was converted successfully; otherwise, null.</returns>
    
    var e = Function._validateParams(arguments, [
        {name: "date", type: String, mayBeNull: true, optional: false}
    ]);
    if (e) throw e;
      
    var newNhsDate;
    
    // check for true null
    if(date === null || date.length === 0)
    {
        newNhsDate = new NhsDate();
        newNhsDate._dateType = DateType.Null;
        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }
    
    // first check for numeric year month as this can be confused as a date
    var numericYearMonthRegEx = new RegExp("^(0?[1-9]|10|11|12)[-\\s/](\\d{4}|\\d{2})$");

    if (numericYearMonthRegEx.test(date))
    {
        var numericYearMonthRegExResults = numericYearMonthRegEx.exec(date);
        newNhsDate = new NhsDate();
        
        newNhsDate._dateType = DateType.YearMonth;
        newNhsDate._month = Number.parseLocale(numericYearMonthRegExResults[1]);
        newNhsDate._year = NhsDate._parseYear(numericYearMonthRegExResults[2], Sys.CultureInfo.CurrentCulture);
        
        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }
    
    // Check if 'date' is a year
    var yearRegEx = new RegExp("^(\\d{4}|\\d{2})$");

    if (yearRegEx.test(date))
    {
        newNhsDate = new NhsDate();
        
        newNhsDate._year = NhsDate._parseYear(date, Sys.CultureInfo.CurrentCulture);

        newNhsDate._dateType = DateType.Year;
        
        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }
    
    var approxIndicatorPresent = (date.indexOf(NhsDateResources.Approximate) >= 0);

    if (approxIndicatorPresent)
    {
        date = date.replace(NhsDateResources.Approximate, string.Empty).trim();
    }
    
    // try standard datetime parse with our custom formats
    var gs = new GlobalizationService();
   
    var parsedDate = Date.parseLocale(date, gs.shortDatePattern, gs.shortDatePatternWithDayOfWeek);
   
    if (parsedDate !== null)
    {
        newNhsDate = new NhsDate();      
        newNhsDate._dateValue = parsedDate;
        newNhsDate._dateType = (approxIndicatorPresent ? DateType.Approximate : DateType.Exact);
    
        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }
    
    // Check if 'date' is a year and a month
    var yearMonthRegEx = NhsDate._buildMonthYearRegEx();
    
    if (yearMonthRegEx.test(date) === true)
    {
        var yearMonthRegExResults = yearMonthRegEx.exec(date);
        newNhsDate = new NhsDate();
        
        newNhsDate._year = NhsDate._parseYear(yearMonthRegExResults[2], Sys.CultureInfo.CurrentCulture);            
        var monthIndex = NhsDate._findCaseInsensitiveEntry(Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames, yearMonthRegExResults[1]);
        if (monthIndex == -1)
        {                
            monthIndex = NhsDate._findCaseInsensitiveEntry(Sys.CultureInfo.CurrentCulture.dateTimeFormat.AbbreviatedMonthNames, yearMonthRegExResults[1]);
        }
        
        newNhsDate._month = 1 + monthIndex;
        newNhsDate._dateType = DateType.YearMonth;

        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }     
      
     // Check if 'date' is a NullIndex date
    var nullDateRegEx = new RegExp("^Null:(-?\\d+)$", RegexOptions.IgnoreCase);

    if (nullDateRegEx.test(date))
    {
        var nullDateMatch = nullDateRegEx.exec(date);
        newNhsDate = new NhsDate();
        
        newNhsDate._nullIndex = Number.parseLocale(nullDateMatch[1]);
        newNhsDate._dateType = DateType.NullIndex;

        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }
    
    // try standard datetime parse with alternative formats
    var parsedDate = Date.parseLocale(date, 'd-MMM-yyyy', 'd-M-yyyy', 'd-MM-yyyy', 'd-MMMM-yyyy',
                                            'd/MMM/yyyy', 'd/M/yyyy', 'd/MM/yyyy', 'd/MMMM/yyyy',
                                            'd MMM yyyy', 'd M yyyy', 'd MM yyyy', 'd MMMM yyyy',
                                            'ddd d-MMM-yyyy', 'ddd d-M-yyyy', 'ddd d-MM-yyyy', 'ddd d-MMMM-yyyy',
                                            'ddd d/MMM/yyyy', 'ddd d/M/yyyy', 'ddd d/MM/yyyy', 'ddd d/MMMM/yyyy',
                                            'ddd d MMM yyyy', 'ddd d M yyyy', 'ddd d MM yyyy', 'ddd d MMMM yyyy',
                                            'd-MMM-yy', 'd-M-yy', 'd-MM-yy', 'd-MMMM-yy',
                                            'd/MMM/yy', 'd/M/yy', 'd/MM/yy', 'd/MMMM/yy',
                                            'd MMM yy', 'd M yy', 'd MM yy', 'd MMMM yy',
                                            'ddd d-MMM-yy', 'ddd d-M-yy', 'ddd d-MM-yy', 'ddd d-MMMM-yy',
                                            'ddd d/MMM/yy', 'ddd d/M/yy', 'ddd d/MM/yy', 'ddd d/MMMM/yy',
                                            'ddd d MMM yy', 'ddd d M yy', 'ddd d MM yy', 'ddd d MMMM yy');
   
    if (parsedDate !== null)
    {
        newNhsDate = new NhsDate();      
        newNhsDate._dateValue = parsedDate;
        newNhsDate._dateType = (approxIndicatorPresent ? DateType.Approximate : DateType.Exact);
    
        return NhsDate._isValid(newNhsDate) ? newNhsDate : null;
    }
   
    // no match
    return null;
};    
    
NhsDate.add = function(sourceDate, instruction)
{
    /// <summary>
    /// Add adds a number of months, weeks, days or years to a date
    /// </summary>
    /// <param name="sourceDate">Date to add to</param>
    /// <param name="instruction">Add instructions such as +2w to add 2 weeks</param>
    /// <exception cref="Sys.ArgumentNullException">instruction is null</exception>
    /// <returns>New NhsDate object</returns>

    var e = Function._validateParams(arguments, [
        {name: "sourceDate", type: NhsCui.Toolkit.Web.NhsDate, mayBeNull: false, optional: false},
        {name: "instruction", type: String, mayBeNull: false, optional: false}
    ]);
    if (e) throw e; 

    if (sourceDate._dateType == DateType.NullIndex)
    {
        throw Error.invalidOperation(NhsDateResources.AddNotAllowedForDateType);
    }
    
    if (NhsDate.isAddInstruction(instruction) === false)
    {
        throw Error.argumentOutOfRange("instruction", instruction, NhsDateResources.AddInstructionInvalidFormat);
    }
    
    var shortcut = new RegExp("([-+]?\\d+w)|([-+]?\\d+m)|([-+]?\\d+d)|([-+]?\\d+y)", "gi"); //"NhsDate.AddInstructionProcessingRegExFormat);
            
    instruction = instruction.replace(" ", "");
            
    var match = instruction.match(shortcut);
 
    if (match !== null)
    {
        for(var i = 0; i < match.length; i++) {
            var c = match[i];
            var increment;
            increment = Number.parseInvariant(c.substring(0, c.length - 1));
            if (c.toLowerCase().endsWith(NhsDateResources.DaysUnit))
            {
                if (sourceDate._dateType === DateType.Exact || sourceDate._dateType === DateType.Approximate)
                {
                    // Add days
                    sourceDate._dateValue.setDate(sourceDate._dateValue.getDate() + increment);
                }
                else
                {
                    throw Error.argumentOutOfRange("instruction",
                                   String.format(NhsDateResources.AddInstructionNotAllowedForDateType, "Day", sourceDate._dateType));
                }                               
                
            }
            else if (c.toLowerCase().endsWith(NhsDateResources.WeeksUnit))
            {
                if (sourceDate._dateType === DateType.Exact || sourceDate._dateType === DateType.Approximate)
                {
                    // Add weeks
                    sourceDate._dateValue.setDate(sourceDate._dateValue.getDate() + (increment * 7));
                }
                else
                {
                    throw Error.argumentOutOfRange("instruction",
                                    String.format(NhsDateResources.AddInstructionNotAllowedForDateType, "Week", sourceDate._dateType));
                }              
            }
            else if (c.toLowerCase().endsWith(NhsDateResources.MonthsUnit))
            {
                if (sourceDate._dateType === DateType.Exact || sourceDate._dateType === DateType.Approximate)
                {
                    // Add months
                    
                    // Rather than trust the Javascript Date type to do the adding of months increment the month part of the date manually
                    // If that increment would take the date into a new year increment the the year seperately and manually
                    
                    var monthPriorToAdd = sourceDate._dateValue.getMonth();                                            
                    sourceDate._dateValue = NhsDate.IncrementDateUnit(sourceDate._dateValue, NhsCui.Toolkit.Web.SpecifierUnit.Month, increment);                                                           
                    
                }
                else if (sourceDate._dateType === DateType.YearMonth)
                {
                    // **Note the Month field of NhsDate is 1-based unlike the Month that is part of a 
                    // JavaScript Date which is 0-based**
                    
                    var monthPriorToAdd = sourceDate._month; 
                    var yearPriorToAdd = sourceDate._year;
                    var month = sourceDate._month;
                    
                    month = (month + increment) % 12;
                    
                    if (month <= 0){
                        month = 12 + month;
                    }
                    
                    sourceDate._month = month;      
                    
                    if (increment > 0){
                    
                        if (12 -  (monthPriorToAdd + increment) <= 0){
                            //Increment would take us into a new year so increment year part
                            var yearIncrement = Math.floor((((12 -  (monthPriorToAdd + increment)) * -1) / 12)) + 1;
                            sourceDate._year = yearPriorToAdd + yearIncrement;
                        }
                    }
                    else if (increment < 0){
                        if ((monthPriorToAdd + increment) <= 0){
                            //Increment would take us into a new year so decrement year part
                            var yearDecrement = Math.floor((((monthPriorToAdd + increment) * -1) / 12)) + 1;
                            sourceDate._year = yearPriorToAdd + (0 - yearDecrement);
                        }
                    }                                                 
                }
                else
                {
                    throw Error.argumentOutOfRange("instruction",
                                    String.format(NhsDateResources.AddInstructionNotAllowedForDateType, "Month", sourceDate._dateType));
                }              
            
            }
            else if (c.toLowerCase().endsWith(NhsDateResources.YearsUnit))
            {
                if (sourceDate._dateType === DateType.Exact || sourceDate._dateType === DateType.Approximate)
                {
                    // Add years
                    //sourceDate._dateValue.setFullYear(sourceDate._dateValue.getFullYear() + increment);
                    sourceDate._dateValue = NhsDate.IncrementDateUnit(sourceDate._dateValue, NhsCui.Toolkit.Web.SpecifierUnit.Year, increment);
                }
                else if (sourceDate._dateType === DateType.Year || sourceDate._dateType === DateType.YearMonth)
                {
                    sourceDate._year += increment;
                }
                else
                {
                    throw Error.argumentOutOfRange("instruction",
                                    String.format(NhsDateResources.AddInstructionNotAllowedForDateType, "Year", sourceDate._dateType));
                }              
            
            }
        }
    }

    return sourceDate;
};
        
NhsDate.IncrementDateUnit = function(value, unit, increment)
{
    /// <summary>
    /// Parse string into year adjusting two digit years if necessary
    /// </summary>
    /// <param name="value">value to parse</param>
    /// <param name="culture">A NhsCui.Toolkit.Web.SpecifierUnit specifying what unit of the Date to increment</param>
    /// <returns>year</returns>
    
    var y = value.getFullYear();
    var M = value.getMonth();
    var d = value.getDate();
    var day = value.getDay();
    var h = value.getHours();
    var m = value.getMinutes();
    var s = value.getSeconds();
    var n = value.getMilliseconds();
    
    var am = (h >= 0 && h < 12);
    switch (unit) {
        case NhsCui.Toolkit.Web.SpecifierUnit.Year: 
            y = (y + increment); 
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.Month:
            if (increment > 0)
            {
                y = y + Math.floor((M + increment) / 12); 
            }
            else if (increment < 0 && Math.abs(increment) > M)
            {
                y = y - Math.ceil((Math.abs(increment) - M) / 12); 
            }
            
            M = (M + increment) % 12;                         
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.DayOfWeek:
        case NhsCui.Toolkit.Web.SpecifierUnit.Day:
            d = (d + increment) % (new Date(y, M + 1, 0).getDate()); 
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.Hour:
            h = (h + increment) % 24; 
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.Minute: 
            m = (m + increment) % 60;
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.Second: 
            s = (s + increment) % 60; 
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.Millisecond: 
            n = (n + increment) % 1000; 
            break;
        case NhsCui.Toolkit.Web.SpecifierUnit.AMPMDesignator: 
            if (increment != 0) {
                am = !am;
            }
            if (am && h >= 12) {
                h-= 12;
            } else if (!am && h < 12) {
                h+= 12;
            }
            break;
    }
    
    if (y < 0){
        y = 9999;
    }
    if (M < 0){ 
        M = 12 + M;
    }
    if (d < 1){
        d = (new Date(y, M + 1, 0).getDate());
    }
    if (h < 0){
        h = 23;
    }
    if (m < 0){
        m = 59;
    }
    if (s < 0){
        s = 59;
    }
    if (n < 0){ 
        n = 999;
    }
    if (unit == NhsCui.Toolkit.Web.SpecifierUnit.Month && d > (new Date(y, M + 1, 0).getDate())){
        d = (new Date(y, M + 1, 0).getDate());
    }
    value = new Date(y, M, d, h, m, s, n);
    // constructor doesn't work with years such as '1'
    value.setFullYear(y);
    
    return value;    
    
};
        
NhsDate._isValid = function(date)
{
    /// <summary>
    /// Checks to see if the nhs date supplied is valid
    /// </summary>
    /// <param name="date">date to check</param>
    /// <returns>true if valid</returns>
    
    var isValid = false;
    
    if(date)
    {
        switch(date._dateType)
        {                        
            case DateType.Approximate :
            case DateType.Exact :
                isValid = (date._dateValue && Date.isInstanceOfType(date._dateValue));
                break;
            
            case DateType.Year :
                isValid = (date._year >= 0);
                break;
            
            case DateType.YearMonth :
                isValid = (date._year >= 0 && date._month >= 1 && date._month <= 12);
                break;
            
            case DateType.NullIndex :
                isValid = (date._nullIndex >= -1 && date._nullIndex <= 99);
                break;
            
            case DateType.Null :
                isValid = true;
                break;
        }
    }
    
    return isValid;
};
    
NhsDate._datesAreTheSameDay = function(date, date2)
{
    /// <summary>
    /// Checks to see if two days are the same calendar day
    /// </summary>
    /// <param name="date">first date</param>
    /// <param name="date2">date to compare to</param>
    /// <returns>True if the two dates are on the same day</returns>

    return date.getDate() == date2.getDate() && date.getMonth() == date2.getMonth() && date.getFullYear() == date2.getFullYear();
};    
    
NhsDate._buildMonthYearRegEx = function()
{
    /// <summary>
    /// Builds a RegEx object that will help parse a text date looking for a Month - Year string
    /// </summary>
    /// <returns>A RegEx object</returns>

    var monthYearRegExFormat = "^(##Months##)[-\\s/](\\d{4}|\\d{2})$";
    
    // WARNING: Non-hebrew calendar assumption. ie assume <=12 months. Atlas is missing the GetMonthsInYear() method
	var monthsNames=Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames.slice(0,12).join("|");
	monthsNames += "|";
	monthsNames += Sys.CultureInfo.CurrentCulture.dateTimeFormat.AbbreviatedMonthNames.slice(0,12).join("|");
  
    return new RegExp(monthYearRegExFormat.replace("##Months##", monthsNames), RegexOptions.IgnoreCase);
};
    
NhsDate._resolveAddInstructionRegExTokens = function()
{
    /// <summary>
    /// Resolve the tokens on the add instruction regex for the values in the resource file
    /// </summary>
    /// <returns>The add instruction regular expression with its tokens resolved</returns>

    var resolvedText;

    // I wanted to use string.Format but the {1,2} in the actual regEx was being mistaked for a formatting token
    
    // Day token
    resolvedText = NhsDate.AddInstructionValidRegExFormat.replace("#0#", NhsDateResources.DaysUnit);

    // Months token
    resolvedText = resolvedText.replace("#1#", NhsDateResources.MonthsUnit);

    // Weeks token
    resolvedText = resolvedText.replace("#2#", NhsDateResources.WeeksUnit);

    // Years token
    resolvedText = resolvedText.replace("#3#", NhsDateResources.YearsUnit);

    return resolvedText;
};
        
NhsDate._findCaseInsensitiveEntry = function(values, item)
{
    /// <summary>
    /// Find entry an entry in supplied string array by case insensitive match
    /// </summary>
    /// <param name="values">values to search</param>
    /// <param name="item">item to search for</param>
    /// <param name="cultureInfo">culture to use for comparisons</param>
    /// <returns>index in the array of the item ,or -1 if not found</returns>

    var lowerItem = item.toLowerCase();
    for (var i = 0; i < values.length; i++)
    {
        if (values[i].toLowerCase() == lowerItem)
        {
            return i;
        }
    }
    return -1;
}; 
    
NhsDate._parseYear = function(value, culture)
{
    /// <summary>
    /// Parse string into year adjusting two digit years if necessary
    /// </summary>
    /// <param name="value">value to parse</param>
    /// <param name="culture">culture</param>
    /// <returns>year</returns>
    
    var year = parseInt(value, 10);

    // expands 2-digit year into 4 digits.
    if (value.length <= 2)
    {
        var currentYear = new Date().getFullYear();
        year += currentYear - (currentYear % 100);
        if (year > culture.dateTimeFormat.Calendar.TwoDigitYearMax)
        {
            year -= 100;
        }
    }

    return year;
};

NhsCui.Toolkit.Web.NhsDate.prototype = {    
        
    /// <summary>
    /// The DateType that represents the Date
    /// </summary>
    /// <remarks>Default is DateType.Exact</remarks>
    get_dateType : function() {
        return this._dateType;
    },
    set_dateType : function(value) {
        this._dateType=value;
        this.raisePropertyChanged('dateType');
    },
    
    /// <summary>
    /// Gets and sets a number identifying a null type. The index has no meaning in and of itself. The meaning is implied by the UI control.
    /// </summary>
    /// <remarks>Defaults to -1</remarks>
    get_nullIndex : function() {
        return this._nullIndex;
    },

    set_nullIndex : function(value) {
        this._nullIndex = value;
        this.raisePropertyChanged('nullIndex');
    },
    
    /// <summary>
    /// The exact or approximate date value
    /// </summary>
    /// <remarks>Defaults new Date()</remarks>
    get_dateValue : function() {
        return this._dateValue;
    },
    set_dateValue : function(value) {
        this._dateValue = value;
        this.raisePropertyChanged('dateValue');
    },
    
    /// <summary>
    /// The year of a DateType.Year or DateType.YearMonth.
    /// </summary>
    /// <remarks>Defaults to 0</remarks>
    get_year : function() {
        return this._year;
    },
    set_year : function(value) {
        this._year = value;
        this.raisePropertyChanged('year');
    },
    
    /// <summary>
    /// The month of a DateType.YearMonth date
    /// </summary>
    /// <remarks>Defaults to 1</remarks>
    get_month : function() {
        return this._month;
    },
    set_month : function(value) {
        this._month = value;
        this.raisePropertyChanged('month');
    },
    
    /// <summary>
    /// True if DateType is Null
    /// </summary>
    get_isNull : function() {
        return (this._dateType === DateType.Null);
    },
    
    /// <summary>
    /// Add adds a number of months, weeks, days or years to the date
    /// </summary>
    /// <param name="instruction">Add instructions such as +2w to add 2 weeks</param>
    /// <exception cref="Sys.ArgumentNullException">instruction is null</exception>
    add : function(instruction)
    {
        var e = Function._validateParams(arguments, [
            {name: "instruction", type: String, mayBeNull: false, optional: false}
        ]);
        if (e) throw e;

        this._dateValue = NhsDate.add(this, instruction)._dateValue;
    },
    
    /// <summary>
    /// Displays a string of the NhsDate object. 
    /// </summary>
    /// <param name="includeDayOfWeek">Include the day of the week in the string</param>
    /// <param name="showApproxIndicatorWhenApproximate">When the DateType is DateType.Approximate show the Approx text indicator</param>
    /// <param name="showRelativeDayText">If the date is Today, Tomorrow or Yesterday return a string for that rather than the date</param>
    /// <returns>The NhsDate as a string</returns>
    toString : function(includeDayOfWeek, showApproxIndicatorWhenApproximate, showRelativeDayText)
    {
        // Cast "undefined" parameters (ie missing from other toString overloads) to bool (defaulting to false)
        includeDayOfWeek = !!includeDayOfWeek;
        showApproxIndicatorWhenApproximate = !!showApproxIndicatorWhenApproximate;
        showRelativeDayText = !!showRelativeDayText;
    

        function formatYear(year) {
            if (year < 10) {
                return '000' + year;
            }
            if (year < 100) {
                return '00' + year;
            }
            if (year < 1000) {
                return '0' + year;
            }
            return year.toString();
        }
        
        if (this._dateType !== DateType.Approximate && showApproxIndicatorWhenApproximate)
        {
            // This is invalid. You cannot make use of the indicateWhenApproximate flag when the 
            // Date is not Approximate
            throw "Error.argumentOutOfRange(showApproxIndicatorWhenApproximate, showApproxIndicatorWhenApproximate, NhsDateResources.ShowApproxIndicatorInvalidForDateType)";
        }
  
        var formattedDate = null;

        switch(this._dateType)
        {
            case(DateType.Exact):
            case(DateType.Approximate):

                if (showRelativeDayText)
                {
                    var today = new Date();
                    
                    var yesterday = new Date();
                    yesterday.setDate(yesterday.getDate() - 1);
                    
                    var tomorrow = new Date();
                    tomorrow.setDate(tomorrow.getDate() + 1);
                    
                    if (NhsDate._datesAreTheSameDay(today, this._dateValue))
                    {
                        formattedDate = NhsDateResources.Today;
                    }
                    else if (NhsDate._datesAreTheSameDay(yesterday, this._dateValue))
                    {
                        formattedDate = NhsDateResources.Yesterday;
                    }
                    else if (NhsDate._datesAreTheSameDay(tomorrow, this._dateValue))
                    {
                        formattedDate = NhsDateResources.Tomorrow;
                    }
                }
                
                if(!formattedDate)
                {
                    var gs = new GlobalizationService();
                    formattedDate = this._dateValue.format(includeDayOfWeek ? gs.shortDatePatternWithDayOfWeek : gs.shortDatePattern);
                }
                
                if (this._dateType == DateType.Approximate && showApproxIndicatorWhenApproximate)
                {
                    formattedDate = String.format(
                                        /*Sys.CultureInfo.CurrentCulture, */
                                        NhsDateResources.ApproximateDateFormat,
                                        NhsDateResources.Approximate, 
                                        formattedDate);
                }
                break;           

            case (DateType.Year):
                formattedDate = formatYear(this._year);
                break;

            case (DateType.YearMonth):
                formattedDate = String.format("{0}-{1}", 
                    Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames[this._month - 1], 
                    formatYear(this._year));
                break;

            case (DateType.NullIndex):
                formattedDate = String.format("Null:{0}", this._nullIndex);
                break;

            case (DateType.Null):
                formattedDate = "";
                break;

            default:
                throw Error.invalidOperation("");
        }

        return formattedDate;
 
    }
};
NhsCui.Toolkit.Web.NhsDate.registerClass('NhsCui.Toolkit.Web.NhsDate', Sys.Component);

