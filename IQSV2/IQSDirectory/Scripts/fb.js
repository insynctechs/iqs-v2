// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {

    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    //alert(response.stataus);
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        performFBActions();
    } else {
        // The person is not logged into your app or we are unable to tell.

    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    //alert('Test');
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '504326666290316',
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.8' // use graph api version 2.8
    });

    FB.getLoginStatus(function (response) {
        //statusChangeCallback(response);
    });

};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function postToFeed(cname, title, review) {
    var lnkurl = $(location).attr('href');
    var obj = {
        method: 'feed',
        redirect_uri: 'http://www.iqsdirectory.com/',
        link: lnkurl,
        picture: 'http://www.iqsdirectory.com/images/iqsdirectory_home_logo.png',
        name: cname,
        caption: title,
        description: review
    };
    function callback(response) {
    }
    FB.ui(obj, callback);
}

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function performFBActions() {
    //alert('Welcome!  Fetching your information.... ');
    /*FB.api('/me', function (response) {
        alert('Successful login for: ' + response.name);

    });*/
    
    FB.api('/me?fields=id,name,email', function (me) {
        //alert(me.name + ',' + me.id + ',' + me.email);
        var list = [me.email, me.name, me.id];
        var jsonText = JSON.stringify({ list: list });
        $.ajax({
            type: "POST",
            url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/fbuserlogin",
            data: jsonText,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                //alert(msg.d);
                if (msg.d == "Success") {
                    $('#fancybox-close').trigger('click');
                    window.setTimeout('openreviewbox()', 600);
                    $('#divLogout').show();
                    return false;
                }
                else if (msg.d == "InActive") {
                    alert("Your IQS account is disabled!!");
                }
                else {
                    alert("Unexpected Error Occured. Try Again!!");
                }
            },
            failure: function () {
                alert('Request Failed. Try Again.');
            }
        });
    });
}

/* FB.init({
    appId: '504326666290316', // App ID
    channelUrl: 'http://216.250.147.171/IQSBeta2017/channel.html', // Channel File
    scope: 'id,name,email',
    status: true,
    cookie: true,
    xfbml: true
});

FB.init({
    appId: '221653014637602',
    channelUrl: 'http://www.iqsdirectory.com/channel.html',
    scope: 'id,name,email',
    status: true,
    cookie: true,
    xfbml: true
});*/