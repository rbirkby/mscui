//-----------------------------------------------------------------------
// <copyright file="MonthCalendar.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NHS MonthCalendar</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web");

NhsCui.Toolkit.Web.MonthCalendar = function(element) 
{
    NhsCui.Toolkit.Web.MonthCalendar.initializeBase(this, [element]);
    this._format = "d";
    this._cssClass = "nhs_monthcalendar";
    this._visibleDate = null;
    this._calendarDate = null;
    this._calendarImage = null;
    this._imageWidth = -1;    
    this._hasFocus = false;    
        
    this._monthCalendarDiv = null;
    this._calendarDiv = null;
    this._monthSection = null;
    this._prevYearArrow = null;
    this._nextYearArrow = null;
    this._yearSection = null;
    this._prevMonthArrow = null;
    this._nextMonthArrow = null;
    
    this._prevYearArrowImage = null;
    this._nextYearArrowImage = null;
    
    this._prevMonthArrowImage = null;
    this._nextMonthArrowImage = null;
    
    this._year = null;    
    this._month = null;
    
    this._today = null;
        
    this._daysRow = null;
    this._daysBody = null;
    
    this._nullStrings = []; 
    this._valueIsNullString = false;    
    
    this.freeformMode = 0;
    this.arithmeticMode = 1;
    this.assistedFreeformMode = 2;
    this.nullEntryMode = 3;

    this._mode = this.freeformMode;
    this._totalWidthFixed = false;       
        
    //Setup events
    this._focusDelegate = Function.createDelegate(this, this._focusHandler);
    this._blurDelegate = Function.createDelegate(this, this._blurHandler);
    this._keyPressDelegate = Function.createDelegate(this, this._keyPressHandler);      
    this._propertyChangedDelegate = Function.createDelegate(this, this._propertyChangedHandler);
        
    // whether in finish editing
    this._inFinishEditing = false;
};

NhsCui.Toolkit.Web.MonthCalendar.MinDateValue = -62135596800000;
NhsCui.Toolkit.Web.MonthCalendar.MaxDateValue = 253402300799000;

NhsCui.Toolkit.Web.MonthCalendar.prototype = 
{    
    initialize : function() 
    {
        NhsCui.Toolkit.Web.MonthCalendar.callBaseMethod(this, "initialize");
        var elt = this.get_element();

        this._monthCalendarDiv = elt;

        $addHandlers(elt, 
            {
                focus : this._focusDelegate,
                blur : this._blurDelegate,
                keypress : this._keyPressDelegate
            });

        this.add_propertyChanged(this._propertyChangedDelegate);
        this._show();
        // Do this to fix a strange glitch with the styles of the next/prev arrows
        // PS#6367
        this._clearStyleGlitch(this._nextYearArrow);
    },

    get_format : function() 
    { 
        var gs = new GlobalizationService();        
        return (gs.shortDatePattern);
    },

    get_dateType : function() 
    {
        return this.get_value().get_dateType();   
    },

    get_month : function() 
    {
        return this.get_value().get_month();   
    },
    set_month : function(value) 
    {
        this.get_value().set_month(value); 
        
        this.raisePropertyChanged("month"); 
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }        
    },    

    get_year : function() 
    {
        return this.get_value().get_year();   
    },
    set_year : function(value) 
    {
        this.get_value().set_year(value); 
    
        this.raisePropertyChanged("year");
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }        
    },

    get_dateValue : function() 
    {
        return this.get_value().get_dateValue();   
    },
    set_dateValue : function(value) 
    {
        this.get_value().set_dateValue(value); 
        
        this.raisePropertyChanged("dateValue");
        
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }        
    },

    get_value : function() 
    {
        return this._getState().Value;        
    },
    set_value : function(value) 
    {
        var e = Function._validateParams(arguments, [{name: "value", type: NhsDate, mayBeNull: false}]);
        if (e) throw e;        
    
        if(value.get_dateType() !== DateType.Exact)
        {
            throw Error.argumentOutOfRange("value.dateType", value.get_dateType(), "Only DateType.Exact mode is allowed when Functionality is set to DateFunctionality.Simple");
        }
        
        this._getState().Value = value;
                
        this.raisePropertyChanged('value');
            
        if (this.get_isInitialized())
        {
            this._updateHtmlElementsToDateValue();
        }          
    },

    get_cssClass : function() 
    {
        return this._cssClass;
    },
    set_cssClass : function(value) 
    {
        if (this._cssClass != value) 
        {
            if (this._cssClass && this._monthCalendarDiv)
            {
                Sys.UI.DomElement.removeCssClass(this._monthCalendarDiv, this._cssClass);
            }
            this._cssClass = value;
            if (this._cssClass && this._monthCalendarDiv)
            {
                Sys.UI.DomElement.addCssClass(this._monthCalendarDiv, this._cssClass);
            }
            this.raisePropertyChanged("cssClass");
        }
    },            
    
   _getCurrentStyle : function(element) 
   {
        if(element.currentStyle)
        {
            return element.currentStyle;
        }
        var w = (element.ownerDocument ? element.ownerDocument : element.documentElement).defaultView;
        return ((w && (element !== w) && w.getComputedStyle) ? w.getComputedStyle(element, null) : element.style);
    },

    _updateHtmlElementsToDateValue : function() 
    {
        var textBox = this.get_element();
        
        var formattedDate = this._formattedDate();
        
        this._setText(formattedDate);
      
        this._updateCalendar();
        
    },

    _getText : function() 
    {
        return this.get_element().value;
    },
    _setText : function(value) 
    {
        this.get_element().value = value;
    },

    _formattedDate : function() 
    {
        var value = this.get_value();
        return formattedDate = value.toString(false, false, false);
    },

    _getForm : function() 
    {
        var elem = this.get_element();
        
        while(elem && elem.nodeName !== "FORM")
        {
            elem = elem.parentNode;
        }
        return elem;
    },      
    
    dispose : function() 
    {
        // Leave a lot of this in for now - but as we aren't creating and
        // destroying the calendar as a popup div, should be able to simplify
        var e = this.get_element();

        if (this._monthCalendarDiv)
        {
            if (this._monthCalendarDiv.parentNode)
            {
                this._monthCalendarDiv.parentNode.removeChild(this._monthCalendarDiv);
            }
            this._monthCalendarDiv = null;
        }        
        if (this._prevYearArrow)
        {
            this._unWatchCell(this._prevYearArrow);
            this._prevYearArrow = null;
        }
        if (this._prevYearArrowImage)
        {
            this._unWatchCell(this._prevYearArrowImage);
            this._prevYearArrowImage = null;
        }
        if (this._nextYearArrow)
        {        
            this._unWatchCell(this._nextYearArrow);
            this._nextYearArrow = null;
        }
        if (this._nextYearArrowImage) 
        {        
            this._unWatchCell(this._nextYearArrowImage);
            this._nextYearArrowImage = null;
        }
        if (this._prevMonthArrow)
        {
            this._unWatchCell(this._prevMonthArrow);
            this._prevMonthArrow = null;
        }
        if (this._prevMonthArrowImage)
        {
            this._unWatchCell(this._prevMonthArrowImage);
            this._prevMonthArrowImage = null;
        }
        if (this._nextMonthArrow)
        {        
            this._unWatchCell(this._nextMonthArrow);
            this._nextMonthArrow = null;
        }
        if (this._nextMonthArrowImage)
        {        
            this._unWatchCell(this._nextMonthArrowImage);
            this._nextMonthArrowImage = null;
        }
        if (this._today)
        {            
            this._unWatchCell(this._today);
            this._today = null;
        }        
                
        if (this._calendarDiv)
        {
            $removeHandler(this._calendarDiv, "focus", this._calendarFocusDelegate);
            $removeHandler(this._calendarDiv, "blur", this._calendarBlurDelegate);                        
            $removeHandler(this._calendarDiv, "keydown", this._calendarKeyDownDelegate);
        }
        if (this._daysRow)
        {
            for (var i = 0; i < this._daysBody.rows.length; i++)
            {
                var row = this._daysBody.rows[i];
                for (var j = 0; j < row.cells.length; j++)
                {
                    var cell = row.cells[j];                    
                    this._unWatchCell(cell);
                }
            }
            this._daysRow = null;
        }

        var elt = this.get_element();       
                               
        $common.removeHandlers(elt, 
            {
                focus : this._focusDelegate,
                blur : this._blurDelegate,
                keypress : this._keyPressDelegate
           });

        this.remove_propertyChanged(this._propertyChangedDelegate);
        
        NhsCui.Toolkit.Web.MonthCalendar.callBaseMethod(this, "dispose");
    },

    _getState : function() 
    {
        if(!this._state)
        {
            var serializedState = NhsCui.Toolkit.Web.MonthCalendar.callBaseMethod(this, 'get_ClientState');
            
            if (serializedState!==null && serializedState.length > 0)
            {
                this._state = Sys.Serialization.JavaScriptSerializer.deserialize(serializedState);
                this._state.Value = $create(NhsCui.Toolkit.Web.NhsDate, this._state.Value, null, null);
            }
            else
            {
                this._state = {Value : new NhsDate()};
            }
        }
        
        return this._state;
    }, 
    _saveState : function() 
    {
        if(this._state)
        {
            var serializedState = Sys.Serialization.JavaScriptSerializer.serialize(this._state);
            NhsCui.Toolkit.Web.MonthCalendar.callBaseMethod(this, 'set_ClientState', [serializedState]);
        }
    },

    _show : function()
    {
        var value = this.get_value();
        if(value.get_dateType() === DateType.Exact)
        {
            this._calendarDate = value.get_dateValue();
        }
        if(this.get_cssClass())
        {
            Sys.UI.DomElement.addCssClass(this._monthCalendarDiv, this.get_cssClass());
        }
        this._buildCalendar();
        this._buildHeader();
        this._buildDays();
        this._buildFooter();
        this._visibleDate = null;
        this._updateCalendar();
    },

    _buildCalendar : function() 
    {
        // create delegates
        this._cellMouseOverDelegate = Function.createDelegate(this, this._cellMouseOverHandler);
        this._cellMouseOutDelegate = Function.createDelegate(this, this._cellMouseOutHandler);
        this._calendarFocusDelegate = Function.createDelegate(this, this._calendarFocusHandler);
        this._calendarBlurDelegate = Function.createDelegate(this, this._calendarBlurHandler);
        this._calendarClickDelegate = Function.createDelegate(this, this._calendarClickHandler);
        this._calendarKeyDownDelegate = Function.createDelegate(this, this._calendarKeyDownHandler);
        this._cancelDelegate = Function.createDelegate(this, this._cancelHandler);

        $common.addCssClasses(this._monthCalendarDiv, [
            "nhs_monthcalendar_container",
            this._cssClass
        ]);
        this._calendarDiv = document.createElement("div");
        this._calendarDiv.tabIndex = 0;
        $addHandler(this._calendarDiv, "focus", this._calendarFocusDelegate);
        $addHandler(this._calendarDiv, "blur", this._calendarBlurDelegate);
        $addHandler(this._calendarDiv, "keydown", this._calendarKeyDownDelegate);
        this._monthCalendarDiv.appendChild(this._calendarDiv);
    },

    _buildHeader : function() 
    {
        this._header = this._buildHeaderElement(this._calendarDiv, "nhs_monthcalendar_header", null);
        this._monthSection = this._buildHeaderElement(this._header, "nhs_monthcalendar_monthheader", null);
        this._prevMonthArrow = this._buildHeaderElement(this._monthSection, "nhs_monthcalendar_prev", "prevMonth");
        this._month = this._buildHeaderElement(this._monthSection, "nhs_monthcalendar_month_title", null);              
        this._nextMonthArrow = this._buildHeaderElement(this._monthSection, "nhs_monthcalendar_next", "nextMonth");
        this._yearSection = this._buildHeaderElement(this._header, "nhs_monthcalendar_yearheader", null);
        this._prevYearArrow = this._buildHeaderElement(this._yearSection, "nhs_monthcalendar_prev", "prevYear");
        this._year = this._buildHeaderElement(this._yearSection, "nhs_monthcalendar_year_title", null);
        this._nextYearArrow = this._buildHeaderElement(this._yearSection, "nhs_monthcalendar_next", "nextYear");

    },

    _buildHeaderElement : function(parentNode, cssClass, mode) 
    {
    
        var element = document.createElement("div");
        Sys.UI.DomElement.addCssClass(element, cssClass);
        if(mode)
        {
            element.mode = mode;
            element.id = this.get_element().id + "_calendar_" + mode;
            this._watchCell(element);
        }
        
        parentNode.appendChild(element);
        return element;
    },

    _buildDays : function() 
    {
        var days = this._days = document.createElement("div");
        Sys.UI.DomElement.addCssClass(days, "nhs_monthcalendar_days");
        this._calendarDiv.appendChild(days);
        
        var dayIdBase = this.get_element().id + "_calendar_day_";        
        
        // Header Table
        var headerTable = this._headerTable = document.createElement("table");
        headerTable.id = dayIdBase + "_headerTable";
        headerTable.cellPadding = 0;
        headerTable.cellSpacing = 0;                
        headerTable.width = "100%";
        Sys.UI.DomElement.addCssClass(headerTable, "nhs_monthcalendar_table_day_header");
        days.appendChild(headerTable);        
        
        var headerBody = this._headerBody = document.createElement("tbody");
        headerBody.id = dayIdBase + "_headerBody";
        headerTable.appendChild(headerBody);
        
        var daysHeaderRow = document.createElement("tr");
        daysHeaderRow.id = dayIdBase + "_daysHeaderRow";
        headerBody.appendChild(daysHeaderRow);      
                
        var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;        
        
        // Calendar starting from monday.
        for (var i = 1; i < 8; i++)
        {
            var dayCell = document.createElement("td");
            dayCell.id = dayIdBase + "_dayHeader" + i;
            this._setInnerText(dayCell, dtf.ShortestDayNames[i % 7]);
            Sys.UI.DomElement.addCssClass(dayCell, "nhs_monthcalendar_dayname");
            daysHeaderRow.appendChild(dayCell);
        }                                   
        
        // Days Table
        var daysTable = this._daysTable = document.createElement("table");
        daysTable.id = dayIdBase + "_daysTable";
        daysTable.cellPadding = 0;
        daysTable.cellSpacing = 2;
        daysTable.border = 0;
        daysTable.width = "100%";
        days.appendChild(daysTable);            
        
        var daysBody = this._daysBody = document.createElement("tbody");
        daysBody.id = dayIdBase + "_daysBody";
        daysTable.appendChild(daysBody);        

        var idCount = 0;
        for (i = 0; i < 6; i++) 
        {
            var daysRow = document.createElement("tr");
            daysRow.id = dayIdBase + "_daysRow" + i;            
            daysBody.appendChild(daysRow);
            for(var j = 0; j < 7; j++) 
            {
                dayCell = document.createElement("td");
                this._setInnerText(dayCell, "&nbsp");
                dayCell.mode = "day";
                dayCell.id = dayIdBase + (idCount++);
                Sys.UI.DomElement.addCssClass(dayCell, "nhs_monthcalendar_day");
                this._watchCell(dayCell);
                daysRow.appendChild(dayCell);
            }
        }
    },

    _setInnerText : function(element, text) 
    {
        var child = element.lastChild;
        while(child)
        {
            if(child.nodeType === 3)
            {
                element.removeChild(child);
            }
            child = child.prevousSibling;
        }
        
        if(text)
        {
            element.appendChild(document.createTextNode(text));
        }
    },

    _buildFooter : function() 
    {
        // table for layout
        var footerTable = this._footerTable = document.createElement("table");
        footerTable.width = "100%";
        footerTable.cellSpacing = 3;
        var footerBody = this._footerBody = document.createElement("tbody");
        this._footerTable.appendChild(this._footerBody); 
        
        var footerRow = document.createElement("tr");
        var todayCell = this._todayCell = document.createElement("td");
        
        todayCell.id = this.get_element().id + "_calendar_footer" + "_today_cell";
        todayCell.align ="left";
        
        //Add Today Links
        var today = this._today = document.createElement("input");
        today.mode = "today";
        today.type = "button"
        today.id = this.get_element().id  + "_calendar_footer" + "_today_button";
        Sys.UI.DomElement.addCssClass(today, "nhs_monthcalendar_today");   
        today.title =  NhsCui.Toolkit.Web.MonthCalendarResources.todayTitle;     
        this._watchCell(today); 
        todayCell.appendChild(today);
        
        footerRow.appendChild(todayCell);
        
        this._footerBody.appendChild(footerRow);
        this._monthCalendarDiv.appendChild(this._footerTable);
    },
    
    _watchCell : function(cell) 
    {
        $addHandlers(cell,
            {
                mouseover:this._cellMouseOverDelegate,
                mouseout:this._cellMouseOutDelegate,
                click:this._calendarClickDelegate
            });
    },
    _unWatchCell: function(cell) 
    {
        $common.removeHandlers(cell,
            {
                mouseover:this._cellMouseOverDelegate,
                mouseout:this._cellMouseOutDelegate,
                click:this._calendarClickDelegate
            });
    },

    _updateCalendar : function() 
    {
        var today = new Date().getDateOnly();
        var value = this.get_value().get_dateValue();
        if (this._visibleDate === null)
        {
            this._visibleDate = value;
            if (this._visibleDate === null)
            {
                this._visibleDate = today;
            }
        }
        var date = this._visibleDate;
        
        if (this._calendarDate === null)
        {
            this._calendarDate = date;
        }
        
        var startOfMonth = new Date(this._calendarDate.getFullYear(), this._calendarDate.getMonth(), 1);

        var startOfMonthDayOfWeek = startOfMonth.getDay();
        var daysInMonth = new Date(this._calendarDate.getFullYear(), this._calendarDate.getMonth() + 1, 0).getDate();
        var daysInLastMonth = new Date(this._calendarDate.getFullYear(), this._calendarDate.getMonth(), 0).getDate();
        
        //weeksInMonth will always be at least 4 because dayInWeek is always 7, the shortest month is 
        //28, 28 / 7 = 4 so initialise weeksInMonth to 4
        
        var weeksInMonth = 4;
        
        //If daysInMonth is anything over 28 we will need at least another week row so add one
        
        if (daysInMonth > 28)
        {
            weeksInMonth++;
        }
        
        //Depending on where in the week the month start we may need 6 week rows
        //1 is added because starting day is monday in our calendar and not
        //Sunday.
        if ((startOfMonthDayOfWeek) > (7 - (daysInMonth - 28) + 1))
        {
            weeksInMonth++;
        }
        
        //This may look like an odd assignment but it works because startOfMonthDayOfWeek is zero based
        //and our calendar is starting from monday.
        var daysToBacktrack = (startOfMonthDayOfWeek === 1 ? 7 : startOfMonthDayOfWeek === 0 ? 6 : startOfMonthDayOfWeek - 1);
        
        var spanToGoBackFromStartOfMonth = AjaxControlToolkit.TimeSpan.fromDays(daysToBacktrack).subtract(AjaxControlToolkit.TimeSpan.fromHours(1));
        
        var startDate = startOfMonth.subtract(spanToGoBackFromStartOfMonth);
        
        var currentDate = startDate;
        
        for (var week = 0; week < this._daysBody.rows.length; week++) 
        {
            var weekRow = this._daysBody.rows[week];
            for (var dayOfWeek = 0; dayOfWeek < weekRow.cells.length; dayOfWeek++)
            {
                var dayCell = weekRow.cells[dayOfWeek];
                this._setInnerText(dayCell, currentDate.getDate());
                dayCell.title = currentDate.format("D");
                dayCell.date = currentDate;
                $common.removeCssClasses(dayCell, [
                    "nhs_monthcalendar_other",
                    "nhs_monthcalendar_active",
                    "nhs_monthcalendar_selected"
                ]);
                Sys.UI.DomElement.addCssClass(dayCell, this._getCssClass(dayCell.date, 'd'));
                currentDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1);
            }
        }
        this._prevMonthArrow.date = new Date(startOfMonth.getFullYear(), startOfMonth.getMonth() - 1, startOfMonth.getDate());
        this._nextMonthArrow.date = new Date(startOfMonth.getFullYear(), startOfMonth.getMonth() + 1, startOfMonth.getDate());
        
        this._prevYearArrow.date = new Date(startOfMonth.getFullYear() - 1, startOfMonth.getMonth(), startOfMonth.getDate());
        this._nextYearArrow.date = new Date(startOfMonth.getFullYear() + 1, startOfMonth.getMonth(), startOfMonth.getDate());
                            
        this._setInnerText(this._month, this._calendarDate.format("MMMM"));
        this._setInnerText(this._year,  this._calendarDate.format("yyyy"));
        this._setInnerText(this._prevMonthArrow, "<");
        this._setInnerText(this._nextMonthArrow, ">");
        this._setInnerText(this._prevYearArrow, "<");
        this._setInnerText(this._nextYearArrow, ">");
        
        if(NhsCui.Toolkit.Web.MonthCalendarResources)
        {
            this._prevMonthArrow.title = NhsCui.Toolkit.Web.MonthCalendarResources.prevMonth;
            this._nextMonthArrow.title = NhsCui.Toolkit.Web.MonthCalendarResources.nextMonth;
            this._prevYearArrow.title = NhsCui.Toolkit.Web.MonthCalendarResources.prevYear;
            this._nextYearArrow.title = NhsCui.Toolkit.Web.MonthCalendarResources.nextYear;
        }
        
        this._month.date = this._calendarDate;
        this._year.date = this._calendarDate;
        
        //Set Today link in bottom LH corner
        this._today.value = "Today";
        this._today.date = today;                     
        
        Sys.UI.DomElement.removeCssClass(this._monthCalendarDiv, "nhs_monthcalendar_complex");
    },

    _fireChanged : function() 
    {
        var elt = this.get_element();
        if (document.createEventObject) 
        {
            elt.fireEvent("onchange");
        }
        else if (document.createEvent) 
        {
            var e = document.createEvent("HTMLEvents");
            e.initEvent("change", true, true);
            elt.dispatchEvent(e);
        }
    },

    _isSelected : function(date, part) 
    {
        var value = this.get_value();
        if (!value) return false;
        switch (part)
        {
            case 'd':
                if (date.getDate() != value.get_dateValue().getDate()) return false;
                // goto case 'M';
            case 'M':
                if (date.getMonth() != value.get_dateValue().getMonth()) return false;
                // goto case 'y';
            case 'y':
                if (date.getFullYear() != value.get_dateValue().getFullYear()) return false;
                break;
            default:
                break;
        }
        return true;
    },
    
    // Is the date passed is a weekend or a normal day.
    _isWeekend : function(date, part) 
    {
        switch (part)
        {
            case 'd': return (date.getDay() == 0 || date.getDay() == 6);
            case 'M': return false;
            case 'y': return false;              
            default:
                break;
        }
        return false;
    },
    
    //Is the date passed in part of the current calendar month or is it part of the "other"
    _isOther : function(date, part) 
    {
        var value = this._calendarDate;
        if (!value) return false;
        switch (part)
        {
            case 'd': return (date.getFullYear() != value.getFullYear() || date.getMonth() != value.getMonth());
            case 'M': return false;
            case 'y': 
                var minYear = (Math.floor(this._visibleDate.getFullYear() / 10) * 10);
                return date.getFullYear() < minYear || (minYear + 10) <= date.getFullYear();
            default:
                break;
        }
        return false;
    },

    _getCssClass : function(date, part) 
    {
        if (this._isSelected(date, part))
        {
            return "nhs_monthcalendar_selected";
        }
        else if (this._isOther(date, part))
        {
            return "nhs_monthcalendar_other";
        }
        else if (this._isWeekend(date, part))
        {
            return "nhs_monthcalendar_weekend";
        }
        else
        {
            return "";
        }
    },    

    _focusHandler : function(e) 
    {
        this._hasFocus = true;
        this._updateCalendar();
    },

    _blurHandler : function(e) 
    {
        this._hasFocus = false;
    },

    _cellMouseOverHandler : function(e) 
    {
        Sys.UI.DomElement.addCssClass(e.target, "nhs_monthcalendar_active");
    },

    _cellMouseOutHandler : function(e) 
    {
        Sys.UI.DomElement.removeCssClass(e.target, "nhs_monthcalendar_active");
    },

    _calendarFocusHandler : function(e) 
    {
        var activeElement = this._findElementByCssClass(this._daysBody, "nhs_monthcalendar_active");
        if (activeElement === null)
        {
            activeElement = this._findElementByCssClass(this._daysBody, "nhs_monthcalendar_selected");
        }
         
        if(!activeElement)
        {
            activeElement = this._daysBody.firstChild.firstChild;
        }

        this._setCalendarFocusElement(activeElement);
         
    },
    
    _calendarBlurHandler : function(e) 
    {                    
        this._setCalendarFocusElement(null);          
    },

    _calendarKeyDownHandler : function(e) 
    {
        var focusElement = this._getCalendarFocusElement();
        
        if(!focusElement)
        {
            focusElement = this._findElementByCssClass(this._daysBody, "nhs_monthcalendar_active");
        }
        
        var parentNode = (focusElement ? focusElement.parentNode : this._daysBody.firstChild);
        var childIndex = 0, monthYearSwitch = 4;
        var headerNodes = ([this._prevMonthArrow, this._nextMonthArrow, this._prevYearArrow,  this._nextYearArrow]);
        
        if(focusElement)
        {
            // Array.indexOf doesn't work with childNodes
            var childNodes = (parentNode.tagName === "DIV" ? headerNodes : parentNode.childNodes);
            
            for(var i = 0; i < childNodes.length; i++)
            {
                if(childNodes[i] === focusElement)
                {
                    childIndex = i;
                    break;
                }
            }
        }
        
        var childIndexIncrement = 0;
        
        switch(e.keyCode)
        {
           case(Sys.UI.Key["esc"]) :
                e.preventDefault();
                break;
                
           case(Sys.UI.Key["enter"]) :
                if(focusElement)
                {
                    this._calendarClickHandler({ target: focusElement });
                    e.preventDefault();
                }
                break;
             
           case(Sys.UI.Key["down"]) :
           case(Sys.UI.Key["up"]) :
                var down = (e.keyCode === Sys.UI.Key["down"]);
                if(parentNode.tagName === "DIV")
                {
                    parentNode = (down ? this._daysBody.firstChild : this._daysBody.lastChild);
                }
                else
                {
                    parentNode = (down ? parentNode.nextSibling : parentNode.previousSibling);
                }
                
                e.preventDefault();
                break;
               
            case(Sys.UI.Key["left"]) :
            case(Sys.UI.Key["right"]) :
               childIndex += (e.keyCode === Sys.UI.Key["right"] ? 1 : -1);
               e.preventDefault();
               break;
        }
        
        var childNodes = (parentNode && parentNode.tagName === "TR" ? parentNode.childNodes : headerNodes);

        childIndex = (childNodes.length + childIndex) % childNodes.length;
        this._setCalendarFocusElement(childNodes[childIndex]);

    },

    _findElementByCssClass : function(parentNode, cssClass) 
    {
        var node = parentNode.firstChild;
        var matchElement = null;
        while(node && !matchElement)
        {
            if(node.nodeType === 1)
            {
                if(Sys.UI.DomElement.containsCssClass(node, cssClass))
                {
                    matchElement = node;
                }
                else
                {
                    matchElement = this._findElementByCssClass(node, cssClass);
                }
            }
            
            node = node.nextSibling;
        }
        return matchElement;
    },

    _setCalendarFocusElement : function(value) 
    {
        var prevHoverCell = this._getCalendarFocusElement();
        
        if(prevHoverCell != null)
        {
            Sys.UI.DomElement.removeCssClass(prevHoverCell, "nhs_monthcalendar_focus");
        }
        
        if(value)
        {
            Sys.UI.DomElement.addCssClass(value, "nhs_monthcalendar_focus");
        }
    },

    _getCalendarFocusElement : function() 
    {
        return this._findElementByCssClass(this._calendarDiv, "nhs_monthcalendar_focus");
    },              

    _calendarClickHandler : function(e) 
    {        
        var cell = e.target;
        this._setCalendarFocusElement(null);
        Sys.UI.DomElement.removeCssClass(cell, "nhs_monthcalendar_active");
        
        switch(cell.mode)
        {
            case "prevMonth":
            case "nextMonth":
            case "prevYear":
            case "nextYear":
                this._calendarDate = cell.date;
                this._updateCalendar(); 
                break;          
            case "month":
                break;
            case "year":
                break;                
            case "day":
                this._visibleDate = cell.date;
                this._calendarDate = cell.date;
                
                this.set_value(new NhsDate(cell.date));
                
                break;
            case "today":
                this._visibleDate = cell.date;
                this._calendarDate = cell.date;
                
                this.set_value(new NhsDate(cell.date));
                
                break;
            default:
                break;
        }
        if(e.stopPropagation)
        {
            e.stopPropagation();
            e.preventDefault();
        }
    },
       
    _propertyChangedHandler: function(sender, args) 
    {
        this._saveState();
    },
    
    _cancelHandler : function(e) 
    {
        e.stopPropagation();
        e.preventDefault();
    },

    _getPickerBehavior : function() 
    {                
        return Sys.UI.Behavior.getBehaviorByName(this.get_element(), 'PickerBehavior');
    },

    _enablePicker : function(enable) 
    {
        var picker = this._getPickerBehavior();
        if(!picker && enable && NhsCui.Toolkit.Web.PickerBehavior)
        {
            var element = this.get_element();
            picker = $create(NhsCui.Toolkit.Web.PickerBehavior, 
                        {   "acceptKeyDigitInput" : false,
                            "format" : this.get_format(),
                            "holdOwnValueState" : false,
                            "id": element.id + "_PickerExtender"
                         }, null, null, element);
        }
        
        this._pickerExtender = picker;
        
        if (picker && picker.get_enabled() != enable)
        {
            if(enable)
            {
                picker.set_value(this.get_value().get_dateValue());
            }
            picker.set_enabled(enable);
        }
    },
    
    // keypress is the _only_ event to get the proper characters entered by the user. Other events
    // get the key pressed, not the character. 
    _keyPressHandler : function(e) 
    {
        // Don't process tab
        if(e.charCode === Sys.UI.Key["tab"])
        {
            return;
        }
    },
    
    _checkFreeFormAssistedEntry : function(inputBox, character) 
    {
    return false;        
    },
      
    _selectRange : function(inputBox, start, end) 
    {
    },
    _getSelectionPos : function(inputBox) 
    {
    },    
    _checkFreeFormAssistedEntryTrigger : function(inputBox, character) 
    {
        return false;
    },
    _appendStartsWithMatches : function(values, item, matches) 
    {
        var lowerItem = item.toLowerCase();
		for(var i = 0; i < values.length; i++)
		{
			if(values[i].toLowerCase().indexOf(lowerItem) == 0)
			{
			    matches.push(values[i]);
			}
		}
        return matches;
    },

    _containsMonth : function(culture, value) 
    {
        var lowerValue = value.toLowerCase();
        var monthNames = [];
        Array.addRange(monthNames, culture.dateTimeFormat.MonthNames);
        Array.addRange(monthNames, culture.dateTimeFormat.AbbreviatedMonthNames);
        
        for(var i = 0; i < monthNames.length; i++)
        {
            if(monthNames[i].length > 0 && lowerValue.indexOf(monthNames[i].toLowerCase()) >= 0)
            {
                return true;
            }
        }
        return false;
    },
    
    _clearStyleGlitch : function(elt)
    {
        if (document.createEventObject) 
        {
            elt.fireEvent("onmouseover");
            elt.fireEvent("onmouseout");
        }
        else if (document.createEvent) 
        {
            var e = document.createEvent("HTMLEvents");
            e.initEvent("onmouseover", false, false);
            elt.dispatchEvent(e);
            e.initEvent("onmouseout", false, false);
            elt.dispatchEvent(e);
        }
    }
};

NhsCui.Toolkit.Web.MonthCalendar.registerClass("NhsCui.Toolkit.Web.MonthCalendar", AjaxControlToolkit.BehaviorBase);

//This replacement of the Atlas 1.0 RTM function _getAbbrMonthIndex is due to a bug in Atlas
//This bug has been logged in their ProductStudio DB #61157

Sys.CultureInfo.prototype._getAbbrMonthIndex = function(value)
{
    if (!this._upperAbbrMonths)
    {
        this._upperAbbrMonths = this._toUpperArray(this.dateTimeFormat.AbbreviatedMonthNames);
    }
        
    return Array.indexOf(this._upperAbbrMonths, this._toUpper(value));    
};
