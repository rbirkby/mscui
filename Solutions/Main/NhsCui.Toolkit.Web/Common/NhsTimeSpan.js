//-----------------------------------------------------------------------
// <copyright file="NhsTimeSpan.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript implementation of the NhsTimeSpan class</summary>
//-----------------------------------------------------------------------

// Ported from C# version on 03-May-2007 - GMM

Type.registerNamespace("NhsCui.Toolkit.Web");

var TimeSpanUnitLength = function() 
{
    /// <summary>
    /// TimeSpanUnitLength is a public enumeration of supported TimeSpanUnitLengths
    /// </summary>
};

TimeSpanUnitLength.prototype = 
{
        /// <summary>
        /// Use long unit names. 
        /// </summary>
        Long : 0,

        /// <summary>
        /// Use short unit names.
        /// </summary>
        Short : 1,

        /// <summary>
        /// Automatically selects the length of TimeSpanUnit to be used. If an age of less than
        /// two years is being displayed, short forms are used; otherwise long forms are used. 
        /// </summary>
        Automatic : 2
};

TimeSpanUnitLength.registerEnum("TimeSpanUnitLength");

// ======================
// NhsTimeSpan Main class
// ======================
var NhsTimeSpan = NhsCui.Toolkit.Web.NhsTimeSpan = function() 
{
    NhsCui.Toolkit.Web.NhsTimeSpan.initializeBase(this);
    
    // Member vars

    /// <summary>
    /// Flag that specifies whether the time span represents an age. 
    /// </summary>
    this._isAge = false;

    /// <summary>
    /// The granularity of the time span returned by ToString when IsAge is false. 
    /// </summary>
    this._granularity = TimeSpanUnit.Years;    
    
    /// <summary>
    /// The threshold of the time span returned by ToString when IsAge is false. 
    /// </summary>
    this._threshold = TimeSpanUnit.Seconds;

    /// <summary>
    /// The granularity of the time span returned by ToString when IsAge is true.
    /// </summary>
    /// <remarks>This value is not surfaced publically. </remarks>
    this._granularityForAnAge = TimeSpanUnit.Years;
    
    /// <summary>
    /// The threshold of the time span returned by ToString when IsAge is true.
    /// </summary>
    /// <remarks>This value is not surfaced publically.</remarks>
    this._thresholdForAnAge = TimeSpanUnit.Minutes; 
            
    /// <summary>
    /// The number of whole months between the start and the end of the TimeSpan, once years has been taken into consideration. 
    /// </summary>
    this._calculatedWholeMonths = 0;

    /// <summary>
    /// The total number of whole months that make up the time span. 
    /// </summary>
    /// <remarks>Unlike calculatedWholeMonths this will be 13 months for 1 year 1 month, calculatedWholeMonths would have been 1 month.</remarks>
    this._calculatedTotalMonths = 0;

    /// <summary>
    /// The number of whole years between the start and end of the time span.
    /// </summary>
    this._calculatedWholeYears = 0;

    /// <summary>
    /// The number of whole days between the start and the end of the time span, once years and months have been calculated.
    /// </summary>
    /// <remarks>
    /// See TotalDays for all the days. 
    /// </remarks>
    this._calculatedWholeDays = 0;

    /// <summary>
    /// The number of whole hours between the start and end of the time span.
    /// </summary>
    this._calculatedWholeHours = 0;
    
    /// <summary>
    /// The number of whole minutes between the start and end of the time span.
    /// </summary>
    this._calculatedWholeMinutes = 0;
    
    /// <summary>
    /// The number of whole seconds between the start and end of the time span.
    /// </summary>
    this._calculatedWholeSeconds = 0;

    /// <summary>
    /// The number of whole milli seconds between the start and end of the time span.
    /// </summary>
    this._calculatedWholeMilliseconds = 0;

    /// <summary>
    /// The DateTime that marks the beginning of the time span. 
    /// </summary>
    this._from = new Date();

    /// <summary>
    /// DateTime that marks the end of the time span.
    /// </summary>
    this._to = new Date();
    
    /// <summary>
    /// Indicates whether DateFrom is later than DateTo.
    /// </summary>
    this._isNegativeTimeSpan = false;
    
    // Implement overloaded constructors
    if(arguments.length === 1)
    {
        if(typeof arguments[0] === "object" && arguments[0].constructor == Date)
        {
            this._from = arguments[0];
        }
    }
    else if(arguments.length === 2)
    {
        if(typeof arguments[0] === "object" && arguments[0].constructor == Date && typeof arguments[1] === "object" && arguments[1].constructor == Date)
        {
            this._from = arguments[0];
            this._to = arguments[1];
        }
    }
    
    this._synchronizeInternalTimeSpanData();
};

NhsTimeSpan.TimeSpanParseRegExFormat = "(?:\\b(\\d+)\\s*(?:##UNITLIST##)\\b)?";

NhsTimeSpan.parseExact = function(timeSpan, isAge)
{
    var e = Function._validateParams(arguments, [{name: "timeSpan", type: String, mayBeNull: false, optional: false}, {name: "isAge", type: Boolean, mayBeNull: false, optional: true}]);
    if (e) throw e;     
   
    var newNhsTimeSpan = NhsTimeSpan.tryParse(timeSpan, isAge);
    
    if(!newNhsTimeSpan)
    {
        throw NhsError.formatException(NhsTimeSpanResources.ParseCalledWithBadFormat);
    }
    
    return newNhsTimeSpan;
};

NhsTimeSpan.tryParse = function(timeSpan, isAge)
{
    var e = Function._validateParams(arguments, [{name: "timeSpan", type: String, mayBeNull: false, optional: false}, {name: "isAge", type: Boolean, mayBeNull: true, optional: true}]);
    if (e) throw e;
    
     if (!isAge || isAge == "undefined")
        isAge = false;
      
    var newNhsTimeSpan;

    var parseExpression = this._buildParseRegularExpression();
    var regEx = new RegExp(parseExpression, "i");
    var wordMatch = regEx.exec(timeSpan);
    if (wordMatch !== null)
    {
        var from = new Date();
        
        var to = new Date(from.getTime());
        var span = new Date(from.getTime());
        var value;                                 
        
        value = this._parseGroupIntegerValue(wordMatch, 7);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addSeconds(span, value);                       
        }
        
        value = this._parseGroupIntegerValue(wordMatch, 6);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addMinutes(span, value);
        }
        
        value = this._parseGroupIntegerValue(wordMatch, 5);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addHours(span, value);
        }
        
        value = this._parseGroupIntegerValue(wordMatch, 4);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addDays(span, value);
        }
        
        value = this._parseGroupIntegerValue(wordMatch, 3);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addDays(span, value * 7);
        }
        
        value = this._parseGroupIntegerValue(wordMatch, 2);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addMonths(span, value);
        }
        
        value = this._parseGroupIntegerValue(wordMatch, 1);
        if (value !== -1)
        {
            value = isAge ? value * -1 : value;
            span = this._addYears(span, value);
        }                                                          

        if (isNaN(span) === true)
        {
            throw Error.argumentException("to", span, TimeSpanResources.TimeSpanArgumentOutOfRangeExceptionMessage);
        }
        
        if (isAge)
            return new NhsTimeSpan(span, to);
        else
            return new NhsTimeSpan(from, span);
    }

    return null;
};
    
/// <summary>
/// Builds the RegEx that allows us to parse a time span string. 
/// </summary>
/// <returns>regular expression string</returns>
NhsTimeSpan._buildParseRegularExpression = function()
{
    var regEx = new Sys.StringBuilder();
    regEx.append("^");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.YearsUnit,
                                                NhsTimeSpanResources.YearUnit,
                                                NhsTimeSpanResources.YearsLongUnit,
                                                NhsTimeSpanResources.YearLongUnit)));
    regEx.append("\\s*");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.MonthsUnit,
                                                NhsTimeSpanResources.MonthUnit,
                                                NhsTimeSpanResources.MonthsLongUnit,
                                                NhsTimeSpanResources.MonthLongUnit)));
    regEx.append("\\s*");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.WeeksUnit,
                                                NhsTimeSpanResources.WeekUnit,
                                                NhsTimeSpanResources.WeeksLongUnit,
                                                NhsTimeSpanResources.WeekLongUnit)));
    regEx.append("\\s*");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.DaysUnit,
                                                NhsTimeSpanResources.DayUnit,
                                                NhsTimeSpanResources.DaysLongUnit,
                                                NhsTimeSpanResources.DayLongUnit)));
    regEx.append("\\s*");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.HoursUnit,
                                                NhsTimeSpanResources.HourUnit,
                                                NhsTimeSpanResources.HoursLongUnit,
                                                NhsTimeSpanResources.HourLongUnit)));
    regEx.append("\\s*");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.MinutesUnit,
                                                NhsTimeSpanResources.MinuteUnit,
                                                NhsTimeSpanResources.MinutesLongUnit,
                                                NhsTimeSpanResources.MinuteLongUnit)));
                                                
     regEx.append("\\s*");
    regEx.append(this._buildUnitRegEx(new Array(NhsTimeSpanResources.SecondsUnit,
                                                NhsTimeSpanResources.SecondUnit,
                                                NhsTimeSpanResources.SecondsLongUnit,
                                                NhsTimeSpanResources.SecondLongUnit)));
                                                                                                
    regEx.append("$");
    return regEx.toString();
};

/// <summary>
/// Parse integer from a regex group
/// </summary>
/// <param name="wordMatch">RegExp match array</param>
/// <param name="index">Subexpression (group) index</param>
/// <returns>result value</returns>
NhsTimeSpan._parseGroupIntegerValue = function(wordMatch, index)
{
    var value = -1;
    
    if (wordMatch[index] !== undefined && wordMatch[index].length > 0)
    {
        // if the group has a value it must be an integer
        value = parseInt(wordMatch[index], 10);
    }

    return value;
};

/// <summary>
/// Build reg ex to match single time span units
/// </summary>
/// <param name="groupName">regex group name</param>
/// <param name="unitNames">names used for unit</param>
/// <returns>reg ex</returns>
NhsTimeSpan._buildUnitRegEx = function(unitNames)
{
    var regEx = this.TimeSpanParseRegExFormat.replace("##UNITLIST##", unitNames.join("|"));
    return regEx;
};

/// <summary>
/// Gets the TimeSpanUnit's text
/// </summary>
/// <param name="value">The value of the TimeSpanUnit. </param>
/// <param name="timeSpanUnit">The TimeSpanUnit, TimeSpanUnits.Minutes</param>
/// <returns>The text for the TimeSpanUnit. This is  e.g. 'd' for TimeSpanUnits.Days</returns>
/// <param name="longUnits">if true use long units rather than short ones</param>
NhsTimeSpan._getTimeSpanUnitsString = function(value, timeSpanUnit, longUnits)
{
    if (longUnits === true)
    {
        switch (timeSpanUnit)
        {
            case TimeSpanUnit.Years:
                return (value > 1 ? NhsTimeSpanResources.YearsLongUnit : NhsTimeSpanResources.YearLongUnit);
            case TimeSpanUnit.Months:
                return (value > 1 ? NhsTimeSpanResources.MonthsLongUnit : NhsTimeSpanResources.MonthLongUnit);
            case TimeSpanUnit.Weeks:
                return (value > 1 ? NhsTimeSpanResources.WeeksLongUnit : NhsTimeSpanResources.WeekLongUnit);
            case TimeSpanUnit.Days:
                return (value > 1 ? NhsTimeSpanResources.DaysLongUnit : NhsTimeSpanResources.DayLongUnit);
            case TimeSpanUnit.Hours:
                return (value > 1 ? NhsTimeSpanResources.HoursLongUnit : NhsTimeSpanResources.HourLongUnit);
            case TimeSpanUnit.Minutes:
                return (value > 1 ? NhsTimeSpanResources.MinutesLongUnit : NhsTimeSpanResources.MinuteLongUnit);
            case TimeSpanUnit.Seconds:
                return (value > 1 ? NhsTimeSpanResources.SecondsLongUnit : NhsTimeSpanResources.SecondLongUnit);                
        }
    }
    else
    {
        switch (timeSpanUnit)
        {
            case TimeSpanUnit.Years:
                return (value > 1 ? NhsTimeSpanResources.YearsUnit : NhsTimeSpanResources.YearUnit);
            case TimeSpanUnit.Months:
                return (value > 1 ? NhsTimeSpanResources.MonthsUnit : NhsTimeSpanResources.MonthUnit);
            case TimeSpanUnit.Weeks:
                return (value > 1 ? NhsTimeSpanResources.WeeksUnit : NhsTimeSpanResources.WeekUnit);
            case TimeSpanUnit.Days:
                return (value > 1 ? NhsTimeSpanResources.DaysUnit : NhsTimeSpanResources.DayUnit);
            case TimeSpanUnit.Hours:
                return (value > 1 ? NhsTimeSpanResources.HoursUnit : NhsTimeSpanResources.HourUnit);
            case TimeSpanUnit.Minutes:
                return (value > 1 ? NhsTimeSpanResources.MinutesUnit : NhsTimeSpanResources.MinuteUnit);
            case TimeSpanUnit.Seconds:
                return (value > 1 ? NhsTimeSpanResources.SecondsUnit : NhsTimeSpanResources.SecondUnit);
        }
    }

    throw new Sys.ArgumentOutOfRangeException("timeSpanUnit");
};

NhsTimeSpan._addYears = function(date, increment)
{
    date.setFullYear(date.getFullYear() + increment);
    return date;
};

NhsTimeSpan._addMonths = function(date, increment)
{
    date.setMonth(date.getMonth() + increment);
    return date;
};

NhsTimeSpan._addDays = function(date, increment)
{
    date.setDate(date.getDate() + increment);
    return date;
};

NhsTimeSpan._addHours = function(date, increment)
{
    date.setHours(date.getHours() + increment);
    return date;
};

NhsTimeSpan._addMinutes = function(date, increment)
{
    date.setMinutes(date.getMinutes() + increment);
    return date;
};

NhsTimeSpan._addSeconds = function(date, increment)
{
    date.setSeconds(date.getSeconds() + increment);
    return date;
};

// =====================
// NhsTimeSpan Prototype
// =====================
NhsCui.Toolkit.Web.NhsTimeSpan.prototype = 
{

    // =====================
    // Public Properties
    
    /// <summary>
    /// The DateTime that marks the beginning of the time span. 
    /// </summary>
    get_from : function()
    {
        return this._from;
    },
    set_from : function(value)
    {
        this._from = value;
        this._synchronizeInternalTimeSpanData();
    },

    /// <summary>
    /// The DateTime that marks the end of the time span. 
    /// </summary>
    get_to : function()
    {
        return this._to;
    },
    set_to : function(value)
    {
        this._to = value;
        this._synchronizeInternalTimeSpanData();
    },

    /// <summary>
    /// The number of whole years. 
    /// </summary>
    get_years : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeYears * -1;
        }
        
        return this._calculatedWholeYears;
    },

    /// <summary>
    /// The number of whole months. 
    /// </summary>
    get_months : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeMonths * -1;
        }
        
        return this._calculatedWholeMonths;
    },

    /// <summary>
    /// The current span of time expressed as total whole months. 
    /// </summary>
    /// <remarks>A span of 1 year and 1 month would have a TotalMonths value of 13, whereas 
    get_totalMonths : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedTotalMonths * -1;
        }
        
        return this._calculatedTotalMonths;
    },

    /// <summary>
    /// The current span of time expressed as total whole weeks.
    /// </summary>
    /// <remarks>A span of 1 year and 1 week would have a TotalWeeks value of 53, whereas 
    get_totalWeeks : function()
    {
        if (this._get_weeksAreRelevant() === true)
        {
            // Only under one of the previous conditions is totalWeeks valid. Return it;
            var totalWeeks = Math.floor(this._get_totalDays(this._calcTimeSpan()) / 7);
            
            // Return a negative value if DateFrom is later then DateTo
            if(this._isNegativeTimeSpan)
            {
                return totalWeeks * -1;
            }
            
            return totalWeeks;
        }
        else
        {
            // Weeks is irrelevant so return 0
            return 0;
        }
    },

    /// <summary>
    /// The number of whole weeks.
    /// </summary>
    get_weeks : function()
    {
        if (this._get_weeksAreRelevant() === true)
        {
            // Only under one of the previous conditions is Weeks valid. Return it;
            var weeks = Math.floor(this.get_days() / 7);
            
            // Return a negative value if DateFrom is later then DateTo
            if(this._isNegativeTimeSpan)
            {
                return weeks * -1;
            }
            
            return weeks;
        }
        else
        {
            // Weeks is irrelevant so return 0
            return 0;
        }
    },

    /// <summary>
    /// The number of whole days.   
    /// </summary>
    /// <remarks> 
    /// This value is read-only.   
    /// </remarks>
    get_days : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeDays * -1;
        }
            
        return this._calculatedWholeDays;
    },

    /// <summary>
    /// The number of whole hours.
    /// </summary>
    ///<remarks> 
    /// This value is read-only. 
    ///   </remarks>
    get_hours : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeHours * -1;
        }
           
        return this._calculatedWholeHours;
        // return this._calcTimeSpan().getHours();
    },

    /// <summary>
    /// The number of whole minutes. 
    /// </summary>
    /// <remarks>
    /// This value is read-only.
    /// </remarks>
    get_minutes : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeMinutes * -1;
        }
        
        return this._calculatedWholeMinutes;
        // return this._calcTimeSpan().getMinutes();
    },

    /// <summary>
    /// The number of whole seconds. 
    /// </summary>
    /// <remarks>
    /// This value is read-only. 
    /// </remarks>
    get_seconds : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeSeconds * -1;
        }
        
        return this._calculatedWholeSeconds;
    },

    /// <summary>
    /// The number of whole milliseconds. 
    /// </summary>
    ///  <remarks>
    /// This value is read-only. 
    /// </remarks>
    get_milliseconds : function()
    {
        // Return a negative value if DateFrom is later then DateTo
        if(this._isNegativeTimeSpan)
        {
            return this._calculatedWholeMilliseconds * -1;
        }
        
        return this._calculatedWholeMilliseconds;
    },

    /// <summary>
    /// The granularity of the time span returned by toString when _isAge is false.
    /// </summary>
    /// <remarks>Defaults to TimeSpanUnit.Years. </remarks>
    get_granularity : function()
    {
        return this._granularity;
    },
    set_granularity : function(value)
    {
        this._granularity = value;
    },

    /// <summary>
    /// Specifies whether a time span represents an age. 
    /// </summary>
    get_isAge : function()
    {
        return this._isAge;
    },

    set_isAge : function(value)
    {
        this._isAge = value;

        if (value === true)
        {
            this._fixGranularityAndThreshold();
        }
    },

    /// <summary>
    /// The threshold of the time span returned by toString when _isAge is false. 
    /// </summary>  
    /// <remarks>Defaults to TimeSpanUnit.Seconds. </remarks>
    get_threshold : function()
    {
        return this._threshold;
    },

    set_threshold : function(value)
    {
        this._threshold = value;
    },

    // ==================
    // Private Properties

    _get_weeksAreRelevant : function()
    {
        return (this.get_isAge() === true || this.get_granularity() === TimeSpanUnit.Weeks || this.get_threshold() === TimeSpanUnit.Weeks);
    },

    _get_totalSeconds : function(date)
    {
        date = this._checkDate(date);
        return parseInt(date.getTime() / 1000, 10);
    },

    _get_totalMinutes : function(date)
    {
        date = this._checkDate(date);
        return parseInt(date.getTime() / (1000 * 60), 10);
    },

    _get_totalHours : function(date)
    {
        date = this._checkDate(date);
        return parseInt(date.getTime() / (1000 * 60 * 60), 10);
    },

    _get_totalDays : function(date)
    {
        date = this._checkDate(date);
        return parseInt(date.getTime() / (1000 * 60 * 60 * 24), 10);
    },
    
    _get_totalWeeks : function(date)
    {
        date = this._checkDate(date);
        return parseInt(date.getTime() / (1000 * 60 * 60 * 24 * 7), 10);
    },
    
    // ==============
    // Public Methods

    toString : function()
    {
        return this.toString(TimeSpanUnitLength.Short);
    },
    
    toString : function(unitLength)
    {
        var workingToStringValue = new Sys.StringBuilder();
        var longUnitsBool;
            
        // decide whether to use long or short units
        if (unitLength === TimeSpanUnitLength.Automatic)
        {
            longUnits = this.get_isAge() && this.get_years() >= 2;
        }
        else
        {
            longUnits = (unitLength === TimeSpanUnitLength.Long);
        }

        // relevantGranularity holds the Granularity that is relevant for the current state of NhsTimeSpan (see if block below)
        var relevantGranularity;

        // relevantThreshold holds the Threshold that is relevant for the current state of NhsTimeSpan (see if block below)
        var relevantThreshold;
            
        if (this.get_isAge() === true)
        {
            relevantGranularity = this._relevantGranularity = this._granularityForAnAge;
            relevantThreshold = this._relevantThreshold = this._thresholdForAnAge;
        }
        else
        {
            relevantGranularity = this._relevantGranularity = this.get_granularity();
            relevantThreshold = this._relevantThreshold = this.get_threshold();
        }

        for (var timeSpanUnit = relevantGranularity; timeSpanUnit >= TimeSpanUnit.Seconds; timeSpanUnit--)
        {
            // Get the relevant part of the TimeSpan, flag up if the is the beginning part of the the ToString()
            var unitValue = this._getToStringPartValue(timeSpanUnit === relevantGranularity, timeSpanUnit);
            
            if (unitValue < 0)
            {
                unitValue = unitValue * -1;
            }
                
            if (unitValue > 0)
            {
                // if necessary append a leading space
                if (workingToStringValue.isEmpty() === false)
                {
                    workingToStringValue.append(" ");
                }

                workingToStringValue.append(unitValue.toString());

                // Get the necessary unit text such as 'd' for days
                workingToStringValue.append(NhsTimeSpan._getTimeSpanUnitsString(unitValue, timeSpanUnit, longUnits));
            }

            if (timeSpanUnit === relevantThreshold)
            {
                break;
            }
        }
        
        // Negate the unit values if the dateFrom is later than dateTo
        if (this._isNegativeTimeSpan && workingToStringValue.toString() !== "")
        {
            return "-(" + workingToStringValue.toString() + ")";
        }
            
        return workingToStringValue.toString();
    },
    
    
    // ===============
    // Private Methods

    /// <summary>
    /// Gets part of the TimeSpan data used to make up a string representation of the current TimeSpan.
    /// </summary>
    /// <param name="beginningOfToString">Is this part the first part in the string representation</param>
    /// <param name="timeSpanUnit">The part of the TimeSpan required</param>
    /// <returns>A numeric value for the part</returns>
    _getToStringPartValue : function(beginningOfToString, timeSpanUnit)
    {
        switch (timeSpanUnit)
        {
            case TimeSpanUnit.Years:
                return this.get_years();
            case TimeSpanUnit.Months:
                if (beginningOfToString === true)
                {
                    return this.get_totalMonths();
                }
                else
                {
                    return this.get_months();
                }

            case TimeSpanUnit.Weeks:
                if (beginningOfToString === true)
                {
                    return this.get_totalWeeks();
                }
                else
                {
                    return this.get_weeks();
                }

            case TimeSpanUnit.Days:
                if (beginningOfToString === true)
                {
                    return this._get_totalDays();
                }
                else if (this._get_weeksAreRelevant() === false)
                {
                    // If weeks are not relevant then they will have not been 
                    // used as part of the ToString so use Days rather than calculating the remaining
                    return this.get_days();
                }
                else
                {
                    if (this._relevantGranularity === TimeSpanUnit.Weeks)
                    {
                        return this._get_totalDays() % 7;
                    }
                    else
                    {
                        // The remaining days once weeks have been taken into consideration.

                        // Of course we know that we only want remaining days because this is 
                        // not beginningOfToString so Weeks have already been read off
                        return this.get_days() % 7;
                    }
                }

            case TimeSpanUnit.Hours:
                if (beginningOfToString === true)
                {
                    return this._get_totalHours();
                }
                else
                {
                    return this.get_hours();
                }

            case TimeSpanUnit.Minutes:
                if (beginningOfToString === true)
                {
                    return this._get_totalMinutes();
                }
                else
                {
                    return this.get_minutes();
                }
                
           case TimeSpanUnit.Seconds:
                if (beginningOfToString === true)
                {
                    return this._get_totalSeconds();
                }
                else
                {
                    return this.get_seconds();
                }
        }

        throw new Sys.ArgumentOutOfRangeException("timeSpanUnit");
    },

    /// <summary>
    /// The backing store for the actual span of time is a System.TimeSpan.
    /// </summary>
    _synchronizeInternalTimeSpanData : function()
    {
        this._calculateYearsMonthsAndDays();
        this._calculateHMSM();

        if (this.get_isAge() === true)
        {
            // Set the Granularity and Threshold rules for the new time span
            this._fixGranularityAndThreshold();
        }
    },

    /// <summary>
    /// If the TimeSpan represents an Age, the Granularity and Threshold values must be set to an agreed combination of values. 
    /// </summary>
    _fixGranularityAndThreshold : function()
    {
        if (this._get_totalHours() < 2)
        {
            this._granularityForAnAge = TimeSpanUnit.Minutes;
            this._thresholdForAnAge = TimeSpanUnit.Minutes;
        }
        else if (this._get_totalDays() < 2)
        {
            this._granularityForAnAge = TimeSpanUnit.Hours;
            this._thresholdForAnAge = TimeSpanUnit.Hours;
        }
        else if (this.get_totalWeeks() < 4)
        {
            this._granularityForAnAge = TimeSpanUnit.Days;
            this._thresholdForAnAge = TimeSpanUnit.Days;
        }
        else if (this.get_years() < 1)
        {
            this._granularityForAnAge = TimeSpanUnit.Weeks;
            this._thresholdForAnAge = TimeSpanUnit.Days;
        }
        else if (this.get_years() < 2)
        {
            this._granularityForAnAge = TimeSpanUnit.Months;
            this._thresholdForAnAge = TimeSpanUnit.Days;
        }
        else if (this.get_years() < 18)
        {
            this._granularityForAnAge = TimeSpanUnit.Years;
            this._thresholdForAnAge = TimeSpanUnit.Months;
        }
        else if (this.get_years() >= 18)
        {
            this._granularityForAnAge = TimeSpanUnit.Years;
            this._thresholdForAnAge = TimeSpanUnit.Years;
        }
    },

    /// <summary>
    /// Based on the fromDate and toDate calulate the whole years, whole months and whole days
    /// </summary>
    _calculateYearsMonthsAndDays : function()
    {
        var dateFrom = this._from;
        var dateTo = this._to;
        if(this._from > this._to)
        {
            this._isNegativeTimeSpan = true;
            dateFrom = this._to;
            dateTo = this._from;
        }
        else
        {
            this._isNegativeTimeSpan = false;
            dateFrom = this._from;
            dateTo = this._to;
        }
        
        this._calculatedWholeYears = dateTo.getFullYear() - dateFrom.getFullYear();

        if (dateTo.getMonth() > dateFrom.getMonth() || (dateTo.getMonth() == dateFrom.getMonth() && dateTo.getDate() >= dateFrom.getDate()))
        {
            this._calculatedWholeMonths = dateTo.getMonth() - dateFrom.getMonth();
        }
        else
        {
            this._calculatedWholeYears--;
            this._calculatedWholeMonths = (12 - dateFrom.getMonth()) + dateTo.getMonth();
        }

        if (dateTo.getDate() >= dateFrom.getDate())
        {
            this._calculatedWholeDays = dateTo.getDate() - dateFrom.getDate();
        }
        else
        {
            this._calculatedWholeMonths--;
            var daysInMonth = new Date(dateFrom.getFullYear(), dateFrom.getMonth() + 1, 0).getDate();
            this._calculatedWholeDays = (daysInMonth - dateFrom.getDate()) + dateTo.getDate();
        }

        if (dateTo.getHours() < dateFrom.getHours())
        {
            // Not a full day so subtract fom the calculated days
            this._calculatedWholeDays--;
        }

        this._calculatedTotalMonths = (this._calculatedWholeYears * 12) + this._calculatedWholeMonths;
    },

    /// <summary>
    /// Based on the fromDate and toDate calulate the whole hours, 
    //  whole minutes, whole seconds and milliseconds
    /// </summary>
    _calculateHMSM : function()
    {
        var fromDate = new Date();
        var toDate = new Date();
        
        fromDate.setHours(this._from.getHours());
        fromDate.setMinutes(this._from.getMinutes());
        fromDate.setSeconds(this._from.getSeconds());
        fromDate.setMilliseconds(0);
        
        toDate.setHours(this._to.getHours());
        toDate.setMinutes(this._to.getMinutes());
        toDate.setSeconds(this._to.getSeconds());
        toDate.setMilliseconds(0);

        this._calculatedWholeHours = toDate.getHours() - fromDate.getHours();
        this._calculatedWholeMinutes = toDate.getMinutes() - fromDate.getMinutes();
        this._calculatedWholeSeconds = toDate.getSeconds() - fromDate.getSeconds();
        this._calculatedWholeMilliseconds = toDate.getMilliseconds() - fromDate.getMilliseconds();

        if (toDate.getSeconds() < fromDate.getSeconds())
        {
            if (this._calculatedWholeMinutes > 0)
            {
                this._calculatedWholeMinutes--;
            }
            this._calculatedWholeSeconds = 60 + this._calculatedWholeSeconds;
        }

        if (toDate.getMinutes() < fromDate.getMinutes())
        {
            if (this._calculatedWholeHours > 0)
            {
                this._calculatedWholeHours--;
            }
            this._calculatedWholeMinutes = 60 + this._calculatedWholeMinutes;
        }
        
        if (toDate.getHours() < fromDate.getHours())
        {
            this._calculatedWholeHours = 24 + this._calculatedWholeHours;
        }
    },

    /// <summary>
    /// Calculate the actual time-span between From and To values
    /// </summary>
    /// <returns>regular expression string</returns>
    _calcTimeSpan : function()
    {
        return new Date(this._to.getTime() - this._from.getTime());
    },

    _checkDate : function(date)
    {
        if (date === null || date === undefined)
        {
            return this._calcTimeSpan();
        }
        
        return date;
    }
};

NhsCui.Toolkit.Web.NhsTimeSpan.registerClass('NhsCui.Toolkit.Web.NhsTimeSpan', Sys.Component);
