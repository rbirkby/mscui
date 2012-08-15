//-----------------------------------------------------------------------
// <copyright file="NhsTime.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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

// Hand ported from C# version on 2006-12-15. 
//
// If the C# version has been updated since then, and this file has not had 
// the same changes made, then it will be out of sync.

Type.registerNamespace("NhsCui.Toolkit.Web");


var TimeType = function() {
    /// <summary>
    /// TimeType is a public enumeration of Time type values
    /// </summary>
};
TimeType.prototype = {
        /// <summary>
        /// Time should be treated as exact
        /// </summary>
        Exact:0,

        /// <summary>
        /// Time should be treated as apporximate
        /// </summary>
        Approximate:1,       

        /// <summary>
        /// Time should be treated as a named null value with the NullIndex property
        /// giving the index of the name in the NullStrings property
        /// </summary>
        NullIndex:2,
        
        /// <summary>
        /// Time should be treated as a true null 
        /// </summary>
        Null:3
};
TimeType.registerEnum("TimeType");

var TimeSpanUnit = function() {
    /// <summary>
    /// TimeSpanUnits is a public enumeration of the units of time span measurement that can be used in a TimeSpan
    /// </summary>
};
TimeSpanUnit.prototype = {
         /// <summary>
        /// Seconds
        /// </summary>
        Seconds:0,
         
         /// <summary>
        /// Minutes
        /// </summary>
        Minutes:1,
        
        /// <summary>
        /// Hours
        /// </summary>
        Hours:2,
        
        /// <summary>
        /// Days
        /// </summary>
        Days:3,

        /// <summary>
        /// Weeks
        /// </summary>
        Weeks:4,
        
        /// <summary>
        /// Months
        /// </summary>
        Months:5,
                
        /// <summary>
        /// Years
        /// </summary>
        Years:6
};
TimeSpanUnit.registerEnum("TimeSpanUnit");


var NhsTime = NhsCui.Toolkit.Web.NhsTime = function() {
    NhsCui.Toolkit.Web.NhsTime.initializeBase(this);    
    
    /// <summary>
    /// The TimeType that represents the time
    /// </summary>
    this._timeType = TimeType.Exact;
    
    /// <summary>
    /// The exact or approximate time value
    /// </summary>
    this._timeValue = new Date();

    /// <summary>
    /// A number identifying a null type. The index has no meaning in and of itself. The meaning is implied by the UI control.
    /// </summary>
    /// <remarks>Defaults to -1</remarks>
    this._nullIndex = -1;
};

NhsTime.AddInstructionRegExFormat = "^([+-]?\\d{1,2}\[#0##1#\])+?$";
NhsTime.NonDigitCharactersRegEx=/\D/g;

NhsTime.isAddInstruction = function (value)
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="value">The string to check for an add instruction</param>
    /// <returns>True if the string is an arithmetic update for date</returns>
    /// <exception cref="Sys.ArgumentNullException">time is null</exception>

    var shortcut = new RegExp(NhsTime.resolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);

    var match = value.replace(" ", "").match(shortcut);

    return !!match;
};
    
NhsTime.parse = function(time)
{
    /// <summary>
    /// Parses a string in the format returned by ToString and returns a new Time object.
    /// </summary>
    /// <param name="time">The time to parse</param>
    /// <returns>Time object</returns>
    /// <exception cref="Sys.ArgumentNullException">time is null</exception>
    /// <exception cref="NhsError.FormatException">time is not in a recognised format</exception>

    var e = Function._validateParams(arguments, [
        {name: "time", type: String, mayBeNull: true, optional: false}
    ]);
    if (e) throw e;     
    
    var result = NhsTime.tryParse(time);
    
    if(!result)
    {
        throw NhsError.formatException(NhsTimeResources.ParseCalledWithBadFormat);
    }
    
    return result;
};
    
NhsTime.tryParse = function(time)
{
    /// <summary>
    /// Converts the specified string representation of a logical value to its Time equivalent. A return value indicates whether the conversion succeeded or failed. 
    /// </summary>
    /// <param name="time">a string containing the value to convert</param>
    /// <returns>parsed result if value was converted successfully; otherwise, null.</returns>

    var e = Function._validateParams(arguments, [
        {name: "time", type: String, mayBeNull: true, optional: false}
    ]);
    if (e) throw e;     
    
    var result = null;

    // check for true null
    if(time === null || time.length === 0)
    {
        result = new NhsDate();
        result._dateType = DateType.Null;
        return result;
    }
    
    // first check to see if approx indicator is present
    var approxIndicatorPresent = (time.toLowerCase().indexOf(NhsTimeResources.Approximate.toLowerCase()) >= 0);

    if (approxIndicatorPresent)
    {
        time = time.replace(NhsTimeResources.Approximate, "").trim();
    }

    // try standard datetime parse with our custom formats
    var gs = new GlobalizationService();
        
    // As pre PSA the paste format shouldn't accept formats like 1:2, 1:23, 12:2.
    // Explicitely checking the same.        
    var timePattern = new RegExp("^\\d{2}:\\d{2}$|^\\d{2}:\\d{2}\\s\\(\\w{2}\\)$|\\d{2}:\\d{2}:\\d{2}", RegexOptions.IgnoreCase);     
    var matches = timePattern.exec(time);
    var parsedDateTime = null;   
    if (matches != null && matches.length > 0)
    {
          parsedDateTime = Date.parseLocale(time,
                           gs.shortTimePattern,
                           gs.shortTimePatternWithSeconds,
                           gs.shortTimePattern12HourAMPM,
                           gs.shortTimePatternAMPM,
                           gs.shortTimePatternWithSeconds12HourAMPM,
                           gs.shortTimePatternWithSecondsAMPM,
                           gs.shortTimePattern12Hour,
                           gs.shortTimePatternWithSeconds12Hour);
                           

        if (parsedDateTime !== null)
        {
            result = new NhsTime();
            result._timeValue = parsedDateTime;
            result._timeType = (approxIndicatorPresent ? TimeType.Approximate : TimeType.Exact);
            return result;
        }
    }

    if(!approxIndicatorPresent) 
    {                
        // Check if 'time' is a NullIndex time
        var nullTimeRegEx = new RegExp("^Null:(-?[0-9]+)$", RegexOptions.IgnoreCase);
        var nullTimeMatch = nullTimeRegEx.exec(time);
        
        if (nullTimeMatch)
        {
            result = new NhsTime();
            result._nullIndex = Number.parseInvariant(nullTimeMatch[1]);
            result._timeType = TimeType.NullIndex;
            return result;
        }
    }       
                
    if (parsedDateTime !== null)
    {
        result = new NhsTime();
        result._timeValue = parsedDateTime;
        return result;
    }    
    
    return null;
};    
    
NhsTime.add = function(sourceTime, instruction)
{
    /// <summary>
    /// Add adds a number of hours or minutes to a time
    /// </summary>
    /// <param name="sourceTime">The time to add to</param>
    /// <param name="instruction">Add instructions such as +12h to add 12 hours</param>
    /// <exception cref="Sys.ArgumentNullException">instruction is null</exception>
    /// <returns>new NhsTime object</returns>

    var e = Function._validateParams(arguments, [
        {name: "sourceTime", type: NhsCui.Toolkit.Web.NhsTime, mayBeNull: false, optional: false},
        {name: "instruction", type: String, mayBeNull: false, optional: false}
    ]);
    if (e) throw e; 


    if (NhsTime.isAddInstruction(instruction) === false)
    {
        throw Error.argumentOutOfRange("instruction", instruction, NhsTimeResources.AddInstructionInvalidFormat);
    }

    var shortcut = new RegExp(NhsTime.resolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);

    var match = instruction.replace(" ", "").match(shortcut);


    if (!!match === true)
    {
        var milliseconds = sourceTime._timeValue.getTime();
        
        for(var i=1; i<match.length; i++) {
            var c=match[i];
            var increment;

            increment = Number.parseLocale(c.substring(0, c.length - 1));

            if (c.toLowerCase().endsWith(NhsTimeResources.HoursUnit) === true)
            {
                // Add hours
                milliseconds += increment * 3600000;
            }
            else if (c.toLowerCase().endsWith(NhsTimeResources.MinutesUnit) === true)
            {
                // Add minutes
                milliseconds += increment * 60000;
            }
        }
        
        sourceTime._timeValue.setTime(milliseconds);
    }

    return sourceTime;
};
    
NhsTime.resolveAddInstructionRegExTokens = function()
{
    /// <summary>
    /// Resolve the tokens on the add instruction regex for the values in the resource file
    /// </summary>
    /// <returns>The add instruction regular expression with its tokens resolved</returns>

    var resolvedText;

    // I wanted to use string.Format but the {1,2} in the actual regEx was being mistaked for a formatting token

    // Hour token
    resolvedText = NhsTime.AddInstructionRegExFormat.replace("#0#", NhsTimeResources.HoursUnit);

    // Minutes token
    resolvedText = resolvedText.replace("#1#", NhsTimeResources.MinutesUnit);

    return resolvedText;
};    

NhsCui.Toolkit.Web.NhsTime.prototype = {   
    
    /// <summary>
    /// The TimeType that represents the time
    /// </summary>
    /// <remarks>Default is TimeType.Exact</remarks>    
    get_timeType : function() {
        return this._timeType;
    },
    set_timeType : function(value) {
        this._timeType=value;
        this.raisePropertyChanged('timeType');

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
    /// The exact or approximate time value
    /// </summary>
    /// <remarks>Default is DateTime.Now</remarks>
    get_timeValue : function() {
        return this._timeValue;
    },
    set_timeValue : function(value) {
        this._timeValue = value;
        this.raisePropertyChanged('timeValue');
    },
    
    /// <summary>
    /// True if TimeType is Null
    /// </summary>
    get_isNull : function() {
        return (this._timeType === TimeType.Null);
    },
    
    /// <summary>
    /// Add adds a number of hours or minutes to the time
    /// </summary>
    /// <param name="instruction">Add instructions such as +12h to add 12 hours</param>
    /// <exception cref="System.ArgumentNullException">instruction is null</exception>
    add : function(instruction)
    {
        this._timeValue = NhsTime.add(this, instruction)._timeValue;
    },
    
    /// <summary>
    /// Returns a string representing the time.
    /// </summary>
    /// <param name="showApproxIndicatorWhenApproximate">When the DateType is DateType.Approximate, the Approx text indicator is displayed.</param>
    /// <param name="cultureInfo">culture info</param>
    /// <param name="displaySeconds">whether to display seconds</param>
    /// <param name="display12Hour">whether to use 12 hour or 24 hour clock</param>
    /// <param name="displayAMPM">whether to display am / pm indicator</param>
    /// <returns>The time as a string.</returns>
    toString : function(showApproxIndicatorWhenApproximate, cultureInfo, displaySeconds, display12Hour, displayAMPM)
    {
        var e = Function._validateParams(arguments, [
            {name: "showApproxIndicatorWhenApproximate", type: Boolean, mayBeNull: false, optional: true},
            {name: "cultureInfo", type: Object, mayBeNull: false, optional: true},
            {name: "displaySeconds", type: Boolean, mayBeNull: false, optional: true},
            {name: "display12Hour", type: Boolean, mayBeNull: false, optional: true},
            {name: "displayAMPM", type: Boolean, mayBeNull: false, optional: true}
        ]);
        if (e) throw e;
        
        // Supports toString() - ie zero parameters
        showApproxIndicatorWhenApproximate = this._defaultParameter(showApproxIndicatorWhenApproximate, this._timeType == TimeType.Approximate);
        cultureInfo = this._defaultParameter(cultureInfo, Sys.CultureInfo.CurrentCulture);
        displaySeconds = this._defaultParameter(displaySeconds, false);
        display12Hour = this._defaultParameter(display12Hour, false);
        displayAMPM = this._defaultParameter(displayAMPM, false);
        
        if (showApproxIndicatorWhenApproximate && this._timeType !== TimeType.Approximate)
        {
            // This is invalid. You cannot make use of the indicateWhenApproximate flag when the 
            // TimeType is not Approximate
            throw Error.argumentOutOfRange("showApproxIndicatorWhenApproximate", showApproxIndicatorWhenApproximate, NhsTimeResources.ShowApproxIndicatorInvalidForTimeType);
        }

        var formattedTime;

        switch (this._timeType)
        {
            case TimeType.Exact:
            case TimeType.Approximate:
                var gs = new GlobalizationService();

                // we never return a string such as "14:04 (pm)"
                var reallyDisplayAMPM = displayAMPM && (display12Hour || this._timeValue.getHours() < 12);

                var format = this._getTimeFormat(gs, displaySeconds, display12Hour, reallyDisplayAMPM);

                formattedTime = this._timeValue.format(format /*, cultureInfo*/);

                if(reallyDisplayAMPM)
                {
                    formattedTime = formattedTime.toLowerCase();
                }
                
                if (showApproxIndicatorWhenApproximate)
                {
                    // prepend "Approx" indicator
                    formattedTime = String.format(
                                        /*cultureInfo, */
                                        NhsTimeResources.ApproximateTimeFormat,
                                        NhsTimeResources.Approximate,
                                        formattedTime);
                }

                break;

            case TimeType.NullIndex:
                formattedTime = String.format(/*cultureInfo, */ "Null:{0}", this._nullIndex);
                break;            

            case TimeType.Null:
                formattedTime = "";
                break;
                
            default:
                throw Error.invalidOperation("");
        }

        return formattedTime;

    },
    
    /// <summary>
    /// Returns value or default value if undefined
    /// </summary>
    /// <param name="value">value to test</param>
    /// <param name="defaultValue">default value to return if value is undefined</param>
    /// <returns>value or its default</returns>
    _defaultParameter : function(value, defaultValue)
    {
        if(typeof(value) === "undefined")
        {
            return defaultValue;
        }
        return value;
    },
    
    /// <summary>
    /// Get the time format to use from the supplied globalization service
    /// </summary>
    /// <param name="gs">globalization service to get format from</param>
    /// <param name="displaySeconds">whether to display seconds</param>
    /// <param name="display12Hour">whether to use 12 hour or 24 hour clock</param>
    /// <param name="displayAMPM">whether to display am / pm indicator</param>
    /// <returns>time format</returns>
    _getTimeFormat : function(gs, displaySeconds, display12Hour, displayAMPM)
    {
        var format;
        
        if (displaySeconds)
        {
            if (display12Hour)
            {
                format = displayAMPM ? gs.shortTimePatternWithSeconds12HourAMPM :
                                        gs.shortTimePatternWithSeconds12Hour;
            }
            else
            {
                format = displayAMPM ? gs.shortTimePatternWithSecondsAMPM :
                                        gs.shortTimePatternWithSeconds;
            }
        }
        else
        {
            if (display12Hour)
            {
                format = displayAMPM ? gs.shortTimePattern12HourAMPM :
                                        gs.shortTimePattern12Hour;
            }
            else
            {
                format = displayAMPM ? gs.shortTimePatternAMPM :
                                        gs.shortTimePattern;
            }
        }
        
        return format;
    }
};
NhsCui.Toolkit.Web.NhsTime.registerClass('NhsCui.Toolkit.Web.NhsTime', Sys.Component);
