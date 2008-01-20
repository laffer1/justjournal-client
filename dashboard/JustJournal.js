// This file was generated by Dashcode from Apple Inc.
// You may edit this file to customize your Dashboard widget.

//
// Function: load()
// Called by HTML body element's onload event when the widget is ready to start
//
function load()
{
    setupParts();
}

//
// Function: remove()
// Called when the widget has been removed from the Dashboard
//
function remove()
{
    // Stop any timers to prevent CPU usage
    // Remove any preferences as needed
    // widget.setPreferenceForKey(null, createInstancePreferenceKey("your-key"));
}

//
// Function: hide()
// Called when the widget has been hidden
//
function hide()
{
    // Stop any timers to prevent CPU usage
}

//
// Function: show()
// Called when the widget has been shown
//
function show()
{
    // Restart any timers that were stopped on hide
}

//
// Function: sync()
// Called when the widget has been synchronized with .Mac
//
function sync()
{
    // Retrieve any preference values that you need to be synchronized here
    // Use this for an instance key's value:
    // instancePreferenceValue = widget.preferenceForKey(null, createInstancePreferenceKey("your-key"));
    //
    // Or this for global key's value:
    // globalPreferenceValue = widget.preferenceForKey(null, "your-key");
}

//
// Function: showBack(event)
// Called when the info button is clicked to show the back of the widget
//
// event: onClick event from the info button
//
function showBack(event)
{
    var front = document.getElementById("front");
    var back = document.getElementById("back");

    if (window.widget) {
        widget.prepareForTransition("ToBack");
    }

    front.style.display = "none";
    back.style.display = "block";

    if (window.widget) {
        setTimeout('widget.performTransition();', 0);
    }
    
    // display the username and password if they're saved
    var user = widget.preferenceForKey(widget.identifier + "-" + "user");
	var pass = widget.preferenceForKey(widget.identifier + "-" + "pass");
	var sec = widget.preferenceForKey(widget.identifier + "-" + "security");

	if (user != null)
		txtUsername.value = user;
		
	if (pass != null)
		txtPassword.value = pass;
		
	
	if (sec != null) {
	    if (sec == 1)
	        index = 1;
	    else if (sec == 2)
	        index = 0;
	    else
	        index = 2;
	        
		document.getElementById("lstsecurity").object.setSelectedIndex(index);
	}
}



//
// Function: showFront(event)
// Called when the done button is clicked from the back of the widget
//
// event: onClick event from the done button
//
function showFront(event)
{
	var preferenceKey = "user";		// replace with the key for a preference
	var preferenceValue = txtUsername.value;	// replace with a preference to save
	widget.setPreferenceForKey(preferenceValue, widget.identifier + "-" + preferenceKey);

	preferenceKey = "pass";
	preferenceValue = txtPassword.value;
	widget.setPreferenceForKey(preferenceValue, widget.identifier + "-" + preferenceKey);
	
	var sec = document.getElementById("lstsecurity");
	
	preferenceKey = "security";
	preferenceValue = sec.object.getValue();
	widget.setPreferenceForKey(preferenceValue, widget.identifier + "-" + preferenceKey);

    var front = document.getElementById("front");
    var back = document.getElementById("back");

    if (window.widget) {
        widget.prepareForTransition("ToFront");
    }

    front.style.display="block";
    back.style.display="none";

    if (window.widget) {
        setTimeout('widget.performTransition();', 0);
    }
}

if (window.widget) {
    widget.onremove = remove;
    widget.onhide = hide;
    widget.onshow = show;
    widget.onsync = sync;
}


function myPost(event)
{
	var user = widget.preferenceForKey(widget.identifier + "-" + "user");
	var pass = widget.preferenceForKey(widget.identifier + "-" + "pass");
	var security =  widget.preferenceForKey(widget.identifier + "-" + "security");
    // "2005-09-30 16:18:26"
	var d = new Date();
	var curr_date = d.getDate();
	var curr_month = d.getMonth();
	var curr_year = d.getFullYear();
	var curr_hour = d.getHours();
	var curr_min = d.getMinutes();
	var curr_sec = d.getSeconds();
	curr_month++; // zero based
	
	if (user == null || pass == null) {
	    document.getElementById('status').innerHTML = "Please set your username and password.";
	    return;
	}
	
	var websiteURL = "https://www.justjournal.com/updateJournal"
	 + "?user=" + encodeURIComponent(user) + "&pass=" + encodeURIComponent(pass) +
	 "&security=" + security +"&mood=12&location=0&client=dash" +
	 "&date=" + curr_year + "-";
	
	 if ( curr_month < 10 )
	     websiteURL = websiteURL + "0";
		  
	websiteURL = websiteURL + curr_month + "-" + curr_date +
	 " " + curr_hour + ":" + curr_min + ":" + curr_sec;
	 
	websiteURL = websiteURL + "&subject=" + encodeURIComponent(txtSubject.value) + 
	"&body=" + encodeURIComponent(txtBody.value);
	
	var onloadHandler = function() { xmlLoaded(xmlRequest); };	// The function to call when the feed is loaded; currently calls the XMLHttpRequest load snippet

	// XMLHttpRequest setup code
	var xmlRequest = new XMLHttpRequest();
	xmlRequest.onload = onloadHandler;
	xmlRequest.open("GET", websiteURL);
	xmlRequest.setRequestHeader("Cache-Control", "no-cache");
	xmlRequest.send(null);
}

// Called when an XMLHttpRequest loads a feed; works with the XMLHttpRequest setup snippet
function xmlLoaded(xmlRequest) 
{
    var response = xmlRequest.reponseText;
    
	if (xmlRequest.status == 200) {
		// Parse and interpret results
		// XML results found in xmlRequest.responseXML
		// Text results found in xmlRequest.reponseText
		
		if (response != null) {		
		    if (response.indexOf("JJ.LOGIN.FAIL") > -1) {
		        // We had an error logging in
		        document.getElementById('status').innerHTML = "Invalid user or password";
		        return;
		    }
		    document.getElementById('status').innerHTML = response;
		} else {
		     document.getElementById('status').innerHTML = "";
		}
		
		//clear submitted values
		txtSubject.value = "";
		txtBody.value = "";
	}
	else {
	    response = "Error " + xmlRequest.status + " : " + response;
	    document.getElementById('status').innerHTML = response;
	}
}
