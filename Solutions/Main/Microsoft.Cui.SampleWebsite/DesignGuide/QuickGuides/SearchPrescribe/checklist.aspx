<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CheckList" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The following table lists all of the guidance points in order. 
            Click on a guidance ID to find it in the guide.
            </p>
            <table class="checklist">
            	<tr>
                	<th>ID</th>
                    <th>Guideline</th>
                    <th>Compliance</th>
                </tr>
            	<tr>
                	<td class="number"><a href="context/appwindow.aspx">MSP-0010</a></td>
                    <td>
                    Do not allow the prescribing area to occlude the Patient Banner
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="context/simviews.aspx">MSP-0020</a></td>
                    <td>
                    Allow a patient&#39;s current medications to be accessed whilst prescribing, 
                    preferably by allowing current medications to be displayed simultaneously
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="context/simviews.aspx">MSP-0030</a></td>
                    <td>
                    Support switching to, or simultaneous presentation of, 
                    other views without losing prescription information entered so far
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="context/appwindow.aspx">MSP-0040</a></td>
                    <td>
                    If it is possible to navigate away from the prescribing area 
                    before completing a prescription, ensure that a clear indication 
                    that there is an incomplete prescription remains displayed on screen
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="context/simviews.aspx">MSP-0050</a></td>
                    <td>
                    Do not allow the prescribing area to be positioned such that it 
                    separates the controls (such as those on a toolbar) from the view 
                    to which they relate (see Design Guidance &#8211; 
                    <a href="../../MedicationsList.aspx" title="Links to Guidance - Medications List page">
                    Medications List</a>)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/contents.aspx">MSP-0060</a></td>
                    <td>
                    Support the display of a Quick List containing preselected drug names
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0070</a></td>
                    <td>
                    Minimise the frequency with which the contents of the Quick List change
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/multiplelists.aspx">MSP-0080</a></td>
                    <td>
                    When one or more Quick Lists are provided, display one by 
                    default when the prescribing process is started
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/contents.aspx">MSP-0090</a></td>
                    <td>
                    Include a description of the contents of the Quick List at the 
                    top or bottom of the list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/layout.aspx">MSP-0100</a></td>
                    <td>
                    Display the Quick List below (or as a <span class="nowrap">drop-down</span> list extension of) 
                    the search text input box
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/contents.aspx">MSP-0110</a></td>
                    <td>
                    Do not support navigation (such as expanding and collapsing 
                    or drilling down) within a Quick List
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/contents.aspx">MSP-0120</a></td>
                    <td>
                    Limit the number of drugs in the Quick List such that they can be 
                    displayed in full without a scroll bar
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/contents.aspx">MSP-0130</a></td>
                    <td>
                    Allow only items that can be displayed in a search results 
                    list to be included in a Quick List
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/shortcutkeys.aspx">MSP-0140</a></td>
                    <td>
                    Supplement Quick List entries with shortcut keys
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/shortcutkeys.aspx">MSP-0150</a></td>
                    <td>
                    Display shortcut keys to the right of each entry in the Quick List
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/layout.aspx">MSP-0160</a></td>
                    <td>
                    Use alternate row shading in the Quick List, as in the search results list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0170</a></td>
                    <td>
                    Display a notification when the contents of a Quick List have 
                    changed since it was last presented to the current user
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0180</a></td>
                    <td>
                    Provide a control for closing the Quick List notification
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0190</a></td>
                    <td>
                    Provide a control for disabling the notification so that it is 
                    not displayed again (until the Quick List is changed again)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0200</a></td>
                    <td>
                    Display the Quick List notification every time the Quick List is 
                    displayed (until the user selects an option that disables it)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0210</a></td>
                    <td>
                    Do not display the notification such that it obscures the contents 
                    of the Quick List
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0220</a></td>
                    <td>
                    Close the notification automatically when either a drug is selected 
                    from the Quick List or text is entered into the search text input box
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="mandatory">
                	<td class="number"><a href="quicklist/notifications.aspx">MSP-0230</a></td>
                    <td>
                    Do not allow a drug to be selected from the Quick List by using 
                    the keyboard until the notification has been closed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/multiplelists.aspx">MSP-0240</a></td>
                    <td>
                    Limit the number of Quick Lists that are available to an 
                    individual user to the minimum that are contextually appropriate
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/multiplelists.aspx">MSP-0250</a></td>
                    <td>
                    When multiple Quick Lists are available to a single user, 
                    provide a means of navigating between them
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="quicklist/multiplelists.aspx">MSP-0260</a></td>
                    <td>
                    When multiple Quick Lists are necessary, display the currently 
                    selected Quick List in the control that is used to select a Quick List
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="quicklist/multiplelists.aspx">MSP-0270</a></td>
                    <td>
                    When multiple Quick Lists are necessary, use the words &#39;Quick List&#39; 
                    in a label for the Quick List control or within the control
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/layout.aspx">MSP-0280</a></td>
                    <td>
                    Do not allow the search results list to be positioned such that it is 
                    separated from the search text input box by other controls or by a 
                    significant space
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/layout.aspx">MSP-0290</a></td>
                    <td>
                    Clearly describe the scope of the search
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/layout.aspx">MSP-0300</a></td>
                    <td>
                    Use an in-field prompt in the search text input box to describe 
                    the scope of the search
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/textentry.aspx">MSP-0310</a></td>
                    <td>
                    Do not support entry of codes in the search text input box. (This does 
                    not preclude the use of spelling matching or the provision of an 
                    alternative box for entering codes)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/textentry.aspx">MSP-0320</a></td>
                    <td>
                    Do not provide <span class="nowrap">auto-complete</span> in the search text input box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/textentry.aspx">MSP-0330</a></td>
                    <td>
                    Retain focus in the search text input box until a selection 
                    is made in the search results list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/textentry.aspx">MSP-0340</a></td>
                    <td>
                    When focus is first switched to the results list, set focus to 
                    the first entry in the list by default
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/compactdesign.aspx">MSP-0350</a></td>
                    <td>
                    When space is limited (such that the search results lists may obscure 
                    other information), support the replacement of the search text input box 
                    with an input control in which the selected drug name is displayed
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/compactdesign.aspx">MSP-0360</a></td>
                    <td>
                    When a search results list has been replaced with a control in which the 
                    selected drug name is displayed, <span class="nowrap">re-display</span> the search text input box, 
                    the search criteria and the search results list when this control is selected
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/progressive.aspx">MSP-0370</a></td>
                    <td>
                    Display results using progressive matching where possible
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/progressive.aspx">MSP-0380</a></td>
                    <td>
                    In the absence of progressive matching, provide a static search that 
                    submits text in the search text input box by pressing the ENTER key 
                    and/or activating a control (such as a button) to submit the search
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/progressive/characters.aspx">MSP-0390</a></td>
                    <td>
                    Require a minimum of two characters before displaying search results
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/progressive/characters.aspx">MSP-0400</a></td>
                    <td>
                    When only one character has been entered, display a message that 
                    explains why there are no results and reports the <span class="nowrap">two-character</span> minimum
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/progressive/characters.aspx">MSP-0410</a></td>
                    <td>
                    When two or more characters have been entered and no matches were found, 
                    display a message that clearly indicates a search has been performed and 
                    no matches were found
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/progressive/messages.aspx">MSP-0420</a></td>
                    <td>
                    When a message is displayed, maintain a minimum height equivalent to at 
                    least three rows of results and a width that is at least as wide as the 
                    search text input box
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight.aspx">MSP-0430</a></td>
                    <td>
                    Display search results in a list that is only as high as needed to show 
                    the successful matches or up to a defined maximum height
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight.aspx">MSP-0440</a></td>
                    <td>
                    When the number of matches is too large to be displayed in the maximum 
                    list height, display a message at the end of the search results list that 
                    contains counts of the displayed results and total matches
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight.aspx">MSP-0450</a></td>
                    <td>
                    When the number of matches is too large to be displayed in the maximum 
                    list height, place a control for displaying the full list at the end of 
                    the search results list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight.aspx">MSP-0460</a></td>
                    <td>
                    Allow the height of the search results list to grow to an upper 
                    limit to accommodate the number of results matched
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight/showall.aspx">MSP-0470</a></td>
                    <td>
                    When a limited list has been extended by selecting the control to 
                    display a full list, extend the list by providing a scroll bar
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight/showall.aspx">MSP-0480</a></td>
                    <td>
                    Keep search results &#39;flat&#39;. Do not provide expand or collapse 
                    mechanisms or tree-style browsing within the search results
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/listheight/showall.aspx">MSP-0490</a></td>
                    <td>
                    When a selection has been made in a search results list that has a 
                    scroll bar, allow the scroll bar to be used such that the selection 
                    can be scrolled out of view
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/matching.aspx">MSP-0500</a></td>
                    <td>
                    Match the text in the search text input box to generic drug 
                    names and brand names respectively
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/matching.aspx">MSP-0510</a></td>
                    <td>
                    Match text entered into the search text input box to beginning 
                    of any word (and not to substrings or endings of words)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/matching.aspx">MSP-0520</a></td>
                    <td>
                    Support multiple word searching by allowing the entry of letters 
                    separated with a space and matching those against multiple words
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/matching/listorder.aspx">MSP-0530</a></td>
                    <td>
                    List search results in matched order, such that matches are prioritised by 
                    proximity to the beginning of the drug name and matches in generic drug names 
                    are prioritised above matches in brand names
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/matching/listorder.aspx">MSP-0540</a></td>
                    <td>
                    Where relevancy ranking is not implemented, list search results alphabetically 
                    within each set that have the same text matched
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/matching/groups.aspx">MSP-0550</a></td>
                    <td>
                    For specific searches that return significantly more results (most 
                    of these will be three or four character searches), support the 
                    display of groups in the search results list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/matching/groups.aspx">MSP-0560</a></td>
                    <td>
                    When a group is displayed in a search results list, provide a control 
                    (such as a button) that, when selected, replaces the original text in 
                    the search text input box with the full title of the group and replaces 
                    the original search results with the results within the group
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/matching/spellsynonyms.aspx">MSP-0570</a></td>
                    <td>
                    Support spelling matches
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/matching/spellsynonyms.aspx">MSP-0580</a></td>
                    <td>
                    Support synonyms such that a drug name for which a synonym has been 
                    defined is displayed when the synonym is matched
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/matching/spellsynonyms.aspx">MSP-0590</a></td>
                    <td>
                    When there are spelling matches or synonyms to display, list them 
                    along with other results in the search results list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/codrugs.aspx">MSP-0600</a></td>
                    <td>
                    Supplement co-drugs with text that lists the ingredients of the co-drug
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/codrugs.aspx">MSP-0610</a></td>
                    <td>
                    Format text that lists the ingredients of <span class="nowrap">co-drugs</span> such that it 
                    is differentiated from the drug name
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised.aspx">MSP-0620</a></td>
                    <td>
                    Display prioritised results in a separate section that appears 
                    above other results in the list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised.aspx">MSP-0630</a></td>
                    <td>
                    Separate the prioritised results from standard matches with a horizontal line
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised/labels.aspx">MSP-0640</a></td>
                    <td>
                    Provide a label for the prioritised results that 
                    gives a brief indication of how they are prioritised
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised/labels.aspx">MSP-0650</a></td>
                    <td>
                    Ensure that the labels are sufficiently different from list 
                    items in the search results
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised/labels.aspx">MSP-0660</a></td>
                    <td>
                    Label results that are not prioritised with 
                    &#39;Standard Matches&#39;
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised/labels.aspx">MSP-0670</a></td>
                    <td>
                    When there are no prioritised matches, omit the 
                    prioritised section, horizontal line and label
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised/shortcutkeys.aspx">MSP-0680</a></td>
                    <td>
                    Display keyboard shortcuts for prioritised matches only
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/prioritised/shortcutkeys.aspx">MSP-0690</a></td>
                    <td>
                    Display keyboard shortcuts on the same line as each match. 
                    Display them <span class="nowrap">right-aligned</span> after each match
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/misselection.aspx">MSP-0700</a></td>
                    <td>
                    Where drug names associated with <span class="nowrap">mis-selection</span> errors are listed 
                    in the search results, use formatting to draw attention to them
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/formatting/misselection.aspx">MSP-0710</a></td>
                    <td>
                    Where drug names associated with <span class="nowrap">mis-selection</span> errors are listed 
                    in the search results, highlight the row with a pale background colour
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/misselection.aspx">MSP-0720</a></td>
                    <td>
                    Where drug names associated with <span class="nowrap">mis-selection</span> errors are listed in 
                    the search results, supplement the drug name with a brief warning message
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/formatting/misselection.aspx">MSP-0730</a></td>
                    <td>
                    Display <span class="nowrap">mis-selection</span> warning messages in grey italics and in a 
                    smaller font size than the drug name
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/formatting/spellmatches.aspx">MSP-0740</a></td>
                    <td>
                    Use background colour to highlight differences in characters 
                    between text that has been entered and spelling matches
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/spellmatches.aspx">MSP-0750</a></td>
                    <td>
                    When spelling matches are displayed, ensure that there is sufficient 
                    colour and contrast differences between text and both background 
                    highlighting and spelling matching highlighting
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/spellmatches.aspx">MSP-0760</a></td>
                    <td>
                    When spelling matches are displayed, ensure that there is sufficient 
                    colour and contrast differences between background highlighting and 
                    spelling matching highlighting
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/matching/spellsynonyms.aspx">MSP-0770</a></td>
                    <td>
                    For drug names that are displayed when matched on a synonym, supplement 
                    the drug name with a message that includes the synonym
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting.aspx">MSP-0780</a></td>
                    <td>
                    Use subtle alternate shading of matches in the search results list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting.aspx">MSP-0790</a></td>
                    <td>
                    Avoid the use of strong horizontal lines to separate individual list results
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting.aspx">MSP-0800</a></td>
                    <td>
                    Re-start alternate shading at the beginning of a new 
                    section in a search results list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/brandnames.aspx">MSP-0810</a></td>
                    <td>
                    When brand names that have a generic name are matched, display the 
                    generic drug name and supplement it with the brand name
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/brandnames.aspx">MSP-0820</a></td>
                    <td>
                    Separate generic drug names and brand names with a hyphen that has a 
                    space either side
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="drugsearch/formatting/brandnames.aspx">MSP-0830</a></td>
                    <td>
                    Do not display brand names unless they have been matched with 
                    text entered in the search text input box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="drugsearch/formatting/brandnames.aspx">MSP-0840</a></td>
                    <td>
                    Display generic and brand names in the same order as described in Design Guidance &#8211;  
                    <a href="../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                    Medication Line</a>
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/display.aspx">MSP-0850</a></td>
                    <td>
                    Display a cascading list on the selection of drug name and up to 
                    two further cascading lists for basic prescription attributes
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/display.aspx">MSP-0860</a></td>
                    <td>
                    Present a second list when a selection is made in the search results list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/listwidth.aspx">MSP-0870</a></td>
                    <td>
                    Allow the width of the search results list to extend into available 
                    space to accommodate the longest entry when first presented
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/display.aspx">MSP-0880</a></td>
                    <td>
                    Allow different cascading lists (such as route and form or route and 
                    strength) to be presented depending on the drug selected
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/display.aspx">MSP-0890</a></td>
                    <td>
                    Limit the options presented within cascading lists to those that are 
                    relevant to the previous selection
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/listwidth.aspx">MSP-0900</a></td>
                    <td>
                    When a cascaded list is displayed and the search results list remains open, reduce
                    the width of the search results list as necessary (following Design Guidance &#8211; 
                    <a href="../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                    Medication Line</a> for wrapping)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/layout.aspx">MSP-0910</a></td>
                    <td>
                    Do not allow any of the results or cascaded lists to obscure one another
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/layout.aspx">MSP-0920</a></td>
                    <td>
                    Maintain visibility of selections, and the list from which they were 
                    selected (including the search results), throughout the cascade select, 
                    as long as there is enough space to do so without obscuring other information
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/listwidth.aspx">MSP-0930</a></td>
                    <td>
                    When the width of the search results list is reduced and a scroll 
                    bar is displayed, expand the list to show all results
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/contents.aspx">MSP-0940</a></td>
                    <td>
                    Include a list item in each cascading list that provides access 
                    to values that are not in the list (where they exist)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/contents.aspx">MSP-0950</a></td>
                    <td>
                    Place the list item that provides access to values that are not 
                    in the list last in the list and separate it from the rest of the 
                    list items with a horizontal line
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/contents.aspx">MSP-0960</a></td>
                    <td>
                    Do not provide keyboard shortcuts for the item that provides 
                    access to values that are not in the list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/contents.aspx">MSP-0970</a></td>
                    <td>
                    Where relevant, allow a selection to be made from a cascading list 
                    that differentiates preparations with different <span class="nowrap">bio-availability</span> 
                    properties (such as modified release)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/selectbrand.aspx">MSP-0980</a></td>
                    <td>
                    When a brand name is selected for which generic equivalents are 
                    available, present a cascading list that includes options for the 
                    selected brand and for generic equivalents
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="cascadinglists/selectbrand.aspx">MSP-0990</a></td>
                    <td>
                    When a brand name is selected for which there are no generic 
                    equivalents displayed, present template prescriptions for the 
                    brand (or proceed to a step-by-step approach)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/selectbrand.aspx">MSP-1000</a></td>
                    <td>
                    When a cascading list is presented that includes options for the 
                    selected brand and for generic equivalents, include the drug names 
                    (generic and brand respectively) in the cascading list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/genericequiv.aspx">MSP-1010</a></td>
                    <td>
                    When a cascading list includes options for the selected brand and for 
                    generic equivalents, divide the list into two parts
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="cascadinglists/genericequiv.aspx">MSP-1020</a></td>
                    <td>
                    Display generic equivalent options above specific brand 
                    options in cascading lists
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/display.aspx">MSP-1030</a></td>
                    <td>
                    Require at least drug name and route (or attributes from which the type 
                    of medication can be derived) to be selected before template prescriptions 
                    are displayed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/display.aspx">MSP-1040</a></td>
                    <td>
                    Display template prescriptions only after selections have been made 
                    (manually or automatically) in all other cascading lists
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/display.aspx">MSP-1050</a></td>
                    <td>
                    Keep the number of template prescriptions displayed to a 
                    practical and useful minimum
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/selectiontrail.aspx">MSP-1060</a></td>
                    <td>
                    When a selection has been made in the last cascading list, display a selection trail
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/selectiontrail.aspx">MSP-1070</a></td>
                    <td>
                    Where space is limited such that text within the list of template 
                    prescriptions may wrap onto a new line, display the whole list of 
                    template prescriptions on a new line (below the other input controls)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/selectiontrail/reopen.aspx">MSP-1080</a></td>
                    <td>
                    When an item in a selection trail is selected, reopen the 
                    lists for all the items in the selection trail
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/selectiontrail/reopen.aspx">MSP-1090</a></td>
                    <td>
                    When lists are reopened before a template prescription has been 
                    selected, remove the list of template prescriptions from view
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/selectiontrail/reopen.aspx">MSP-1100</a></td>
                    <td>
                    When lists are reopened, display them all as they were before 
                    the template prescriptions were displayed
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/layout.aspx">MSP-1110</a></td>
                    <td>
                    Present template prescriptions in a list without column headings
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/layout.aspx">MSP-1120</a></td>
                    <td>
                    Where necessary, combine attributes into a 
                    single column to reduce the number of columns
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/layout.aspx">MSP-1130</a></td>
                    <td>
                    Display dose or a dose equivalent at the beginning of each template prescription
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/layout.aspx">MSP-1140</a></td>
                    <td>
                    When space is limited, display template prescriptions in 
                    the style described in Design Guidance &#8211; 
                    <a href="../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                    Medication Line</a>
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/layout.aspx">MSP-1150</a></td>
                    <td>
                    Do not allow horizontal scrolling of a list of template prescriptions
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/contents.aspx">MSP-1160</a></td>
                    <td>
                    Display only template prescriptions relevant to 
                    the drug and selections from cascading lists
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/contents.aspx">MSP-1170</a></td>
                    <td>
                    Minimise (where possible, avoid) the number of template prescriptions 
                    that have only one attribute that is different from other template 
                    prescriptions in the same list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/attributes.aspx">MSP-1180</a></td>
                    <td>
                    Where possible include dose (or equivalent) and frequency 
                    in template prescriptions
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/attributes.aspx">MSP-1190</a></td>
                    <td>
                    Include strength in template prescriptions when it is required for this drug
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/attributes.aspx">MSP-1200</a></td>
                    <td>
                    Include brand in template prescriptions when it is required for this drug
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/attributes.aspx">MSP-1210</a></td>
                    <td>
                    When a template prescription includes supplementary information, 
                    display this information in grey italics
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/contents.aspx">MSP-1220</a></td>
                    <td>
                    Keep the number of attributes defined by a template prescription to a minimum
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/contents.aspx">MSP-1230</a></td>
                    <td>
                    Include an option to proceed directly to the prescription 
                    form without selecting a template prescription
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/contents.aspx">MSP-1240</a></td>
                    <td>
                    Display the option for proceeding directly to the prescription form 
                    at the end of the list and separate it from the template prescriptions 
                    with a horizontal line
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/selecting.aspx">MSP-1250</a></td>
                    <td>
                    After a template prescription has been selected, 
                    display editable pre-filled input controls for each 
                    of the data values defined by the template prescription
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/reopenlist.aspx">MSP-1260</a></td>
                    <td>
                    After a template prescription has been selected (and 
                    one or more fields are displayed as a result) provide 
                    a control that allows the list of template prescriptions 
                    to be reopened
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="templateprescriptions/reopenlist.aspx">MSP-1270</a></td>
                    <td>
                    After a template prescription has been selected, allow 
                    the selection of an alternative template prescription
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/reopenlist.aspx">MSP-1280</a></td>
                    <td>
                    When the control for <span class="nowrap">re-opening</span> template prescriptions 
                    has focus or is activated, draw attention to the fields 
                    that are defined by the template prescriptions
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="templateprescriptions/reopenlist.aspx">MSP-1290</a></td>
                    <td>
                    When the template prescription control is selected, 
                    provide visual cues to draw attention to the fields 
                    that are defined by the template
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/interaction.aspx">MSP-1300</a></td>
                    <td>
                    When there are no template prescriptions to display and 
                    a known set of safe values are available (for example, for 
                    dose and frequency), present selection lists for those fields 
                    sequentially (step by step)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="stepbystep/interaction.aspx">MSP-1310</a></td>
                    <td>
                    Require the selection of at least drug name and route (or 
                    attributes from which the type of medication can be derived) 
                    before presenting input controls for any other values
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="stepbystep/interaction.aspx">MSP-1320</a></td>
                    <td>
                    After selections have been made in all cascading lists, 
                    if there are no template prescriptions, display any required 
                    fields that will not be <span class="nowrap">pre-filled</span> 
                    in sequence such that a 
                    field is displayed when the previous one has been completed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/interaction.aspx">MSP-1330</a></td>
                    <td>
                    If a selection from a cascading list (such as frequency of 
                    &#39;as required&#39;) requires a further field to be completed, 
                    display that field before the remaining required fields
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1340</a></td>
                    <td>
                    Provide a control (such as a button) for switching to a detailed 
                    view from which input controls for all valid fields for this 
                    prescription can be accessed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1350</a></td>
                    <td>
                    Disable the control for displaying all valid input controls until 
                    at least a drug name and route (or attributes from which the type 
                    of medication can be derived) have been selected
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1360</a></td>
                    <td>
                    Provide a control that allows the switch to a more detailed 
                    prescription form to be undone, thus returning to the previous 
                    view containing only the required fields
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/inputcontrols.aspx">MSP-1370</a></td>
                    <td>
                    Restrict the number of input controls to the minimum 
                    required to enter information needed to complete the 
                    prescription
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/inputcontrols.aspx">MSP-1380</a></td>
                    <td>
                    When presenting fields step by step, support pre-filling 
                    of one or more of the fields that are already displayed 
                    when a selection is made in a related field
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/inputcontrols.aspx">MSP-1390</a></td>
                    <td>
                    When presenting fields step by step, allow the contents of 
                    all fields to be reselected such that a <span class="nowrap">pre-filled</span> value, 
                    previous choice or text entry can be changed (even if the 
                    associated selection list has only one option)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1400</a></td>
                    <td>
                    Do not display optional fields, or controls for accessing 
                    optional fields (apart from the button for accessing a more 
                    detailed prescription form)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1410</a></td>
                    <td>
                    Ensure that no data is lost whilst switching from one form to another
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1420</a></td>
                    <td>
                    Minimise the number of controls that are needed for navigation
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1430</a></td>
                    <td>
                    Ensure that no data is lost whilst switching from one form to another
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="stepbystep/detailscontrol.aspx">MSP-1440</a></td>
                    <td>
                    Minimise the number of controls that 
                    are needed to navigate between forms
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sentencelayout/purpose.aspx">MSP-1450</a></td>
                    <td>
                    Use sentence layout when fields are displayed 
                    in an area with much greater width than height 
                    (a thin horizontal strip)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="sentencelayout/purpose.aspx">MSP-1460</a></td>
                    <td>
                    Use sentence layout for cascading lists and 
                    whenever selection lists are presented step by step
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="sentencelayout/format.aspx">MSP-1470</a></td>
                    <td>
                    When using sentence layout, for fields that 
                    have labels, incorporate labels into the fields
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sentencelayout/format.aspx">MSP-1480</a></td>
                    <td>
                    When using sentence layout, wrap 
                    fields onto a new line as necessary
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="sentencelayout/format.aspx">MSP-1490</a></td>
                    <td>
                    When grouping fields in sentence layout, 
                    start a new line after each group
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sentencelayout/dimensions.aspx">MSP-1500</a></td>
                    <td>
                    When using sentence layout, allow fields to grow 
                    in width to fit the text entered or value selected from a list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="sentencelayout/dimensions.aspx">MSP-1510</a></td>
                    <td>
                    When using sentence layout, allow fields 
                    to shrink in width when a value is changed
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="sentencelayout/format.aspx">MSP-1520</a></td>
                    <td>
                    Provide labels for controls whose contents could 
                    be interpreted as belonging to a different control
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="prescriptionforms/structure.aspx">MSP-1530</a></td>
                    <td>
                    Display fields (and controls for accessing individual 
                    optional fields) in a consistent order for all 
                    prescriptions
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="prescriptionforms/structure.aspx">MSP-1540</a></td>
                    <td>
                    Minimise the number of different types of 
                    input controls displayed in any one view
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="prescriptionforms/drugname.aspx">MSP-1550</a></td>
                    <td>
                    Do not allow the drug name to be scrolled out 
                    of view. Keep the drug name visible at the top 
                    of the prescription form, even when the form has 
                    a scroll bar
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="prescriptionforms/layout.aspx">MSP-1560</a></td>
                    <td>
                    When displaying a prescription form with fields 
                    arranged in a column, display field labels <span class="nowrap">right-aligned</span> 
                    and on the left with the fields <span class="nowrap">left-aligned</span> and on the right
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="prescriptionforms/layout.aspx">MSP-1570</a></td>
                    <td>
                    When placing fields labels above input controls, display 
                    the labels <span class="nowrap">left-aligned</span> and in a 
                    smaller font than the text displayed in the control
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/interaction.aspx">MSP-1580</a></td>
                    <td>
                    In a detailed prescription form, require the selection of drug 
                    name and route (or drug name and attributes that allow the type 
                    of medication to be determined) before fields are displayed in 
                    the rest of the detailed prescription form
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/defaultfields.aspx">MSP-1590</a></td>
                    <td>
                    Present the required fields by default when 
                    a detailed prescription form is opened
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/defaultfields.aspx">MSP-1600</a></td>
                    <td>
                    Provide access to a detailed prescription form that 
                    presents the most important attributes by default and 
                    from which all fields can be accessed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/controls.aspx">MSP-1610</a></td>
                    <td>
                    Provide controls such as tabs or buttons for navigating 
                    between areas of the detailed prescription form
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/controls.aspx">MSP-1620</a></td>
                    <td>
                    Provide controls for accessing all areas of the detailed 
                    prescription such that there is no area that can only be 
                    accessed by selecting an item (such as edit administration 
                    times) from a selection list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="detailedforms/controls.aspx">MSP-1630</a></td>
                    <td>
                    When displaying the input controls in a detailed 
                    prescription form, include an appropriate set of 
                    controls for accessing optional fields
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="detailedforms/drugname.aspx">MSP-1640</a></td>
                    <td>
                    When displaying a detailed prescription form, 
                    combine the drug name and route (or drug name 
                    and attributes that allow the type of medication 
                    to be determined) into a single control
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="detailedforms/drugname.aspx">MSP-1650</a></td>
                    <td>
                    When the combined drug name and route field is 
                    selected, provide an option to change the drug 
                    name and route
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/interaction.aspx">MSP-1660</a></td>
                    <td>
                    Do not rely on disabling fields (or controls for accessing 
                    optional fields) to impose an order
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/interaction.aspx">MSP-1670</a></td>
                    <td>
                    After selections from cascading lists have been completed, 
                    do not automatically open a selection list for a control in 
                    the detailed prescription form unless a change to a field has 
                    triggered the need to confirm or <span class="nowrap">re-enter</span> 
                    values in related fields
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/structure.aspx">MSP-1680</a></td>
                    <td>
                    Display the drug name and route (or drug name and 
                    attributes that allow the type of medication to be 
                    determined) in a section at the top of the detailed 
                    prescription view
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/structure.aspx">MSP-1690</a></td>
                    <td>
                    Display the first field in each section on a new line
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="detailedforms/structure.aspx">MSP-1700</a></td>
                    <td>
                    When section labels are provided, display them 
                    at the top of the section
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="detailedforms/structure.aspx">MSP-1710</a></td>
                    <td>
                    Label at least each input control, 
                    group of input controls or section
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/promotedoptional.aspx">MSP-1720</a></td>
                    <td>
                    Only when it is important to encourage the completion 
                    of an optional field, promote it by displaying an 
                    input control for it
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/promotedoptional.aspx">MSP-1730</a></td>
                    <td>
                    When an optional input control is promoted, support 
                    the entry or selection of a null value and require 
                    it to be completed
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="fields/prompts.aspx">MSP-1740</a></td>
                    <td>
                    Display in-field prompts for fields that have 
                    to be completed by the user and would otherwise 
                    be blank. (A field does not have to have an 
                    <span class="nowrap">in-field</span> prompt if 
                    it contains a label)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/prompts.aspx">MSP-1750</a></td>
                    <td>
                    Use a phrase that begins with a verb for 
                    <span class="nowrap">in-field</span> prompts in
                    fields that have to be completed by the user
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="fields/accessoptional.aspx">MSP-1760</a></td>
                    <td>
                    Provide access to individual optional fields by placing 
                    a control in the place where the field will appear when 
                    the control is selected
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/layout.aspx">MSP-1770</a></td>
                    <td>
                    If the value selected for an optional field causes 
                    it to increase in width, allow it to wrap onto a 
                    new line if necessary
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/multipleoptional.aspx">MSP-1780</a></td>
                    <td>
                    When necessary, display more than one optional field on 
                    activation of a control for displaying an optional field
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/formatting.aspx">MSP-1790</a></td>
                    <td>
                    Use formatting to reduce the relative 
                    emphasis on optional controls
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/layout.aspx">MSP-1800</a></td>
                    <td>
                    Allow values entered in optional fields to be removed such 
                    that the optional field or control is returned to the state 
                    it was in when the  prescription form was opened
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="fields/formatting.aspx">MSP-1810</a></td>
                    <td>
                    Use other formatting to mark required fields 
                    (or their labels) in a detailed prescription 
                    form where necessary to ensure consistency 
                    with other areas of the application
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/dynamiclayout.aspx">MSP-1820</a></td>
                    <td>
                    Allow some input controls to be defined that are only displayed 
                    when certain values are selected in another input control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/order.aspx">MSP-1830</a></td>
                    <td>
                    When determining the order in which to display input 
                    controls, prioritise the placement of fields whose values 
                    determine which other fields may be displayed in the form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/order.aspx">MSP-1840</a></td>
                    <td>
                    When determining the order in which to display input 
                    controls, prioritise the grouping together of controls 
                    whose values affect the options available in other controls
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/dynamiclayout.aspx">MSP-1850</a></td>
                    <td>
                    When an input causes new input control(s) to appear, 
                    allow other input controls to move so that the new one 
                    can be placed correctly and consistently
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/dynamiclayout.aspx">MSP-1860</a></td>
                    <td>
                    When an input causes new input control(s) to appear, 
                    place the new input controls next (at least in sequence 
                    if not in layout) to the control that caused it to appear
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/selectionlists/interactions.aspx">MSP-1870</a></td>
                    <td>
                    Allow the ESC key to be used to close a selection list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists/combinedcontrols.aspx">MSP-1880</a></td>
                    <td>
                    Combine controls (such as check boxes and 
                    text entry boxes) into a single list control 
                    where this achieves a usability benefit
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/selectionlists/interactions.aspx">MSP-1890</a></td>
                    <td>
                    Do not empty other fields when a selection list is reopened
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists.aspx">MSP-1900</a></td>
                    <td>
                    Where possible, encourage the selection of an item 
                    from a selection list before allowing free text to be entered
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists.aspx">MSP-1910</a></td>
                    <td>
                    When there is supplementary information to display for 
                    an entry in a selection list, display the information 
                    in grey italics
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists/commonvalues.aspx">MSP-1920</a></td>
                    <td>
                    Prioritise the items displayed in a selection 
                    list by separating them into sections
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists/commonvalues.aspx">MSP-1930</a></td>
                    <td>
                    Limit the options available in the first section 
                    of a selection list (and in automatically presented 
                    cascading lists) to relevant values
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists/commonvalues.aspx">MSP-1940</a></td>
                    <td>
                    Where there are further choices than are displayed 
                    by default in a prioritised list, provide access to 
                    further options with an additional section of the list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/selectionlists/detailedvalues.aspx">MSP-1950</a></td>
                    <td>
                    When a more detailed view is available for 
                    defining the values in a prioritised list 
                    (such as one for editing individual 
                    administration times), include a single 
                    list item for accessing that view at the 
                    end of the selection list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/relatedfields.aspx">MSP-1960</a></td>
                    <td>
                    Where relevant, use supplementary text in a drop-down 
                    list of options if the selection will affect other 
                    options in the form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/relatedfields.aspx">MSP-1970</a></td>
                    <td>
                    Where data is available, update the contents of a 
                    selection list based on selections made in related fields
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/relatedfields/layout.aspx">MSP-1980</a></td>
                    <td>
                    When displaying list items that are not valid in 
                    relation to values selected in other fields, list 
                    them in a separate section in the selection list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/relatedfields.aspx">MSP-1990</a></td>
                    <td>
                    When a list item is selected that is not valid in 
                    relation to values selected in other fields (and 
                    data is available to support this) clear the other fields
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/relatedfields/layout.aspx">MSP-2000</a></td>
                    <td>
                    As far as possible, present input controls for 
                    fields that are <span class="nowrap">inter-dependent</span> close to one another
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/relatedfields.aspx">MSP-2010</a></td>
                    <td>
                    In a system that cannot validate entered values (because 
                    decision support checking is not available), when a 
                    selection list is reopened and a different value selected, 
                    clear entries in all input controls that are interdependent
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/prefilling.aspx">MSP-2020</a></td>
                    <td>
                    Support <span class="nowrap">pre-filling</span> of fields (or sets of fields) when they 
                    are first displayed and ensure that the <span class="nowrap">pre-filled</span> values are 
                    based on at least the drug name and route (or attributes from 
                    which the type of medication can be derived)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/prefilling.aspx">MSP-2030</a></td>
                    <td>
                    Allow the contents of all fields to be reselected such that a 
                    <span class="nowrap">pre-filled</span> value, previous choice or text entry can be changed 
                    (even if the associated selection list has only one option)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/prefilling.aspx">MSP-2040</a></td>
                    <td>
                    When a value is selected in a field, <span class="nowrap">pre-fill</span> appropriate 
                    fields that have defaults (or only one possible value) based 
                    on the selected value (for example, <span class="nowrap">pre-fill</span> administration 
                    times when a frequency is selected)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/prefilling.aspx">MSP-2050</a></td>
                    <td>
                    Use formatting (such as highlighting) to draw attention to a 
                    field whose contents have changed automatically rather than 
                    directly by the user
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/prefilling.aspx">MSP-2060</a></td>
                    <td>
                    Pre-fill administration times and time of first dose (or 
                    equivalent for once only and as required medications) when 
                    frequency has been selected
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/dosestrength.aspx">MSP-2070</a></td>
                    <td>
                    When a dose field (or equivalent) is displayed, also display 
                    a label for the dose (either within or outside of the input control)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/dosestrength.aspx">MSP-2080</a></td>
                    <td>
                    If possible, do not allow the selection of a unit of measurement 
                    for a dose that would result in an invalid value when combined 
                    with the number entered for the dose amount
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/dosestrength.aspx">MSP-2090</a></td>
                    <td>
                    When a strength field is displayed, also display a label for 
                    the strength field or a group label including the word 
                    &#39;strength&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/dosestrength.aspx">MSP-2100</a></td>
                    <td>
                    Do not present strength and dose input controls next to each 
                    other (side by side) in a detailed prescription form
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/administrationtimes.aspx">MSP-2110</a></td>
                    <td>
                    When displaying a list of administration times, 
                    display the dose for the first scheduled 
                    administration in bold
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/administrationtimes.aspx">MSP-2120</a></td>
                    <td>
                    Do not display a horizontal <span class="nowrap">(text-only)</span> list of 
                    administration times for schedules containing more 
                    than six administration events in 24 hours
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/administrationtimes.aspx">MSP-2130</a></td>
                    <td>
                    Provide a selection list containing predefined 
                    sets of administration times
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="inputcontrols/administrationtimes.aspx">MSP-2140</a></td>
                    <td>
                    Do not display input controls for entering or editing 
                    individual administration times within the view that 
                    shows all the required fields for a prescription
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/datetime.aspx">MSP-2150</a></td>
                    <td>
                    For all prescriptions, require a date and time to be defined (or <span class="nowrap">pre-filled</span>) 
                    for: the first dose (for regular medications), the starting date and time (for 
                    as required medications), the only dose (for once only medications)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/datetime.aspx">MSP-2160</a></td>
                    <td>
                    Use unique labels for the following fields: the first dose (for regular 
                    medications), the starting date and time (for as required medications), 
                    the only dose (for once only medications)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="inputcontrols/controltype.aspx">MSP-2170</a></td>
                    <td>
                    Do not provide a check box for fields with two opposite states when one of
                    those states causes a related field to be presented. (For example, do not
                    provide a check box to set &#39;as required&#39; to on or off if a setting 
                    of &#39;on&#39; requires another field to be presented to qualify the 
                    conditions for administration)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/preview.aspx">MSP-2180</a></td>
                    <td>
                    Provide a control for displaying a preview
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/preview/contents.aspx">MSP-2190</a></td>
                    <td>
                    Include all values defined as part of the prescription in a preview
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/preview/format.aspx">MSP-2200</a></td>
                    <td>
                    Adhere to guidance in 
                    
                    Design Guidance &#8211; <a href="../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">Medication Line</a> 
                    for the display of drug details in a preview
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="previewauthorise/preview.aspx">MSP-2210</a></td>
                    <td>
                    Do not introduce a preview as a compulsory step before a 
                    detailed prescription form has been opened
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/preview/interaction.aspx">MSP-2220</a></td>
                    <td>
                    Require a preview to be presented before a prescription 
                    can be authorised when the prescription details are 
                    distributed over more than one screen such that a 
                    navigation control (such as a button or tab) is needed 
                    to move between screens
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="previewauthorise/preview/format.aspx">MSP-2230</a></td>
                    <td>
                    Do not display the medication line within a preview as 
                    a long line of text extending for longer than 120 
                    characters without wrapping onto a new line
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="previewauthorise/preview/format.aspx">MSP-2240</a></td>
                    <td>
                    Where relevant, display some prescription attributes 
                    in a preview using a format similar to that used in 
                    other medications views (though different to the format 
                    used for the input control)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/preview/control.aspx">MSP-2250</a></td>
                    <td>
                    Provide a control for closing the preview and 
                    returning to the prescription form (such that 
                    the prescription can be amended)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/preview/control.aspx">MSP-2260</a></td>
                    <td>
                    Set default focus to the control that closes the preview
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/authorising.aspx">MSP-2270</a></td>
                    <td>
                    Place the preview button before the authorise button 
                    and reflect this in the tabbing order
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/authorising.aspx">MSP-2280</a></td>
                    <td>
                    Provide a button for authorising the prescription 
                    and label it &#39;Authorise&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/authorising.aspx">MSP-2290</a></td>
                    <td>
                    Place the Authorise button at the bottom right of the 
                    prescription form such that it may be out of view if 
                    the form is long enough to need a scroll bar
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/authorising.aspx">MSP-2300</a></td>
                    <td>
                    Do not set the focus to the Authorise button by default
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="previewauthorise/authorising.aspx">MSP-2310</a></td>
                    <td>
                    Disable the Authorise button until all required 
                    fields have been completed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            </table>
      </div>
    </div>
</asp:Content>
