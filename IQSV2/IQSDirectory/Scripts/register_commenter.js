if (are_cookies_enabled()) {
    var cookieName = 'profilelogin';
    var cookie = $.cookie(cookieName);
    if (cookie !== null) {
        var list = cookie.split("|| ||");
        var acomp = new Array();
        $.each(list, function (key, val) {
            var vl = val.split("| |");
            acomp.push(vl[0]);
        });
        if (list.length > 0) {
            var lastval = list[list.length - 2];
            if (lastval !== null) {
                var lval = lastval.toString().split("| |");
                $('#txtEmail').val(lval[0]);
                $('#txtPassword').val(lval[1]);
                $('#chkRemember').attr('checked', 'checked');
            }
        }
        $("#txtEmail").autocomplete({
            source: acomp,
            select: function (event, ui) {
                var pass = "";
                $.each(list, function (key, val) {
                    var vl = val.split("| |");
                    if (vl[0] == ui.item.value) {
                        pass = vl[1];
                        return false;
                    }
                });
                if (pass !== "") {
                    $('#txtPassword').val(pass);
                    $('#chkRemember').attr('checked', 'checked');
                }
            }
        });
    }
}
$('#txtRegPass').bind('cut copy paste', function (event) {
    event.preventDefault();
});

$('#txtRegVerify').bind('cut copy paste', function (event) {
    event.preventDefault();
});

$('#txtRegPass, #txtRegVerify').keypress(function (e) {
    if (e.which == 32) {
        e.preventDefault();
    }
});

$("#txtPassword").keyup(function (event) {
    if (event.keyCode == 13) {
        $("#lnkLogin").click();
    }
});

$("#txtForgotEmail").keyup(function (event) {
    if (event.keyCode == 13) {
        $("#lnkForgotSubmit").click();
    }
});

/*$("#Captcha1_txtCaptcha").keyup(function (event) {
    if (event.keyCode == 13) {
        $("#lnkRegister").click();
    }
});*/

$('#lnkClose').click(function () {
    $('#fancybox-close').trigger('click');
    return false;
});

$('#lnkForgotPassword').click(function () {
    if ($('#divForgot').is(':hidden')) {
        $('#divRegChild').animate({ 'padding-top': '-=30' });
        $('#divForgot').slideDown();
        $('#txtForgotEmail').focus();
    }
    else {
        $('#divRegChild').animate({ 'padding-top': '+=30' });
        $('#divForgot').slideUp();
    }
    return false;
});

$('#lnkRegLogin').click(function () {
    $('#txtRegDName').val('');
    $('#txtRegName').val('');
    $('#txtRegEmail').val('');
    $('#txtRegPass').val('');
    $('#txtRegVerify').val('');
    $('#Captcha1_txtCaptcha').val('');
    $('#divSuccess').slideUp('slow', function () {
        $('#divRegForm').slideDown();
    });
    return false;
});

$("#lnkForgotSubmit").click(function () {

    if ($.trim($('#txtForgotEmail').val()) == '') {
        alert('Please Enter Email');
        $('#txtForgotEmail').focus();
        return false;
    }
    if (!isValidEmailAddress($('#txtForgotEmail').val())) {
        alert('Enter a Valid Email');
        $('#txtForgotEmail').focus();
        return false;
    }

    var list = [$('#txtForgotEmail').val()];
    var jsonText = JSON.stringify({ list: list });
    $.ajax({
        type: "POST",
        url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/userforgotpassword",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg == "Success") {
                $('#divRegForm').slideUp('slow', function () {
                    $('#divSuccessForgot').slideDown();
                });
                return false;
            }
            else if (msg === "Invalid") {
                alert("Email Address You Entered Does Not Exists !!");
                $('#txtForgotEmail').focus();
            }
            else {
                alert("Unexpected Error Occured. Try Again!!");
            }
        },
        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });
    return false;
});

$('#lnkLogin').click(function () {
    if ($.trim($('#txtEmail').val()) == '') {
        alert("Please Enter Email");
        $('#txtEmail').focus();
        return false;
    }
    if ($.trim($('#txtPassword').val()) == '') {
        alert('Please Enter Password');
        $('#txtPassword').focus();
        return false;
    }
    var list = [$('#txtEmail').val(), $('#txtPassword').val()];
    var jsonText = JSON.stringify({ list: list });
    $.ajax({
        type: "POST",
        url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/userlogin",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {

            if (msg == "Success" || msg.d=="Success") {
                if (are_cookies_enabled()) {
                    var cookieOptions = { expires: 30, path: '/' };
                    var newval = '';
                    if (cookie !== null) {
                        var nlist = cookie.split("|| ||");
                        $.each(nlist, function (key, val) {
                            var nvl = val.split("| |");
                            if (nvl[0] !== $('#txtEmail').val()) {
                                newval += val + '|| ||';
                            }
                        });
                    }
                    if ($('#chkRemember').is(':checked')) {
                        var val = $('#txtEmail').val() + '| |' + $('#txtPassword').val();
                        newval += val + '|| ||';
                        $.cookie(cookieName, newval, cookieOptions);
                    }
                    else {
                        $.cookie(cookieName, newval, cookieOptions);
                    }
                }
                else {
                    if ($('#chkRemember').is(':checked')) {
                        alert("Your browser cookies are disabled. Please enable it.");
                    }
                }
                $('#fancybox-close').trigger('click');
                window.setTimeout('openreviewbox()', 600);
                $('#divLogout').show();
                return false;
            }
            else if (msg == "Invalid" || msg.d == "Invalid") {
                alert("Invalid Username / Password !!");
            }
            else {
                alert("Unexpected Error Occured. Try Again!!");
            }
        },
        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });
    return false;
});
function openreviewbox() {
    if ($('#hidCommentType').val() == 'Review') {
        $('#lnkReviewBox').trigger('click');
    }
    else {
        $('#lnkReplyBox').trigger('click');
    }
}
$('#lnkRegister').click(function () {

    if ($.trim($('#txtRegName').val()) == '') {
        alert('Please Enter Name');
        $('#txtRegName').focus();
        return false;
    }
    if ($.trim($('#txtRegEmail').val()) == '') {
        alert('Please Enter Email');
        $('#txtRegEmail').focus();
        return false;
    }
    if (!isValidEmailAddress($('#txtRegEmail').val())) {
        alert('Enter a Valid Email');
        $('#txtRegEmail').focus();
        return false;
    }

    if ($('#txtRegPass').val().length < 5) {
        alert('Password Should be Atleast 5 Characters');
        $('#txtRegPass').focus();
        return false;
    }

    if ($('#txtRegPass').val() != $('#txtRegVerify').val()) {
        alert('Confirm Password did not Match');
        $('#txtRegVerify').focus();
        return false;
    }


    var list = [$('#txtRegDName').val(), $('#txtRegName').val(), $('#txtRegEmail').val(), $('#txtRegPass').val(), $('#hidIp').val()];
    
    var jsonText = JSON.stringify({ list: list, doaction: "yes", returntype : '' });
    
    $.ajax({
        type: "POST",
        url: $('#hdnApiPath').val() + "api/Reviews/AddCommenter",
        //url: $('#hdnApiPath').val() + "controls/reviewmanager/registercommenter",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg == "Success") {
                $('#divRegForm').slideUp('slow', function () {
                    $('#divSuccess').slideDown();
                });
            }
            else if (msg == "Exists") {
                alert("Email Address You Entered Already Exists!!");
            }
            else if (msg == "IP") {
                alert("You have exceeded maximum (5) registration from this IP");
            }
            else if (msg == "") {
                alert("Unexpected Error Occured. Try Again!!");
            }
            else {
                alert(msg);
            }
        },

        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });
    return false;
});
function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][\d]\.|1[\d]{2}\.|[\d]{1,2}\.))((25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\.){2}(25[0-5]|2[0-4][\d]|1[\d]{2}|[\d]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
};
function are_cookies_enabled() {
    var cookieEnabled = (navigator.cookieEnabled) ? true : false;
    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
        document.cookie = "testcookie";
        cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
    }
    return (cookieEnabled);
};

FB.init({
    appId: '221653014637602', // App ID
    channelUrl: 'http://216.250.147.171/directorytestarea/channel.html', // Channel File
    scope: 'id,name,email',
    status: true, // check login status
    cookie: true, // enable cookies to allow the server to access the session
    xfbml: true  // parse XFBML
});

function fbLogin() {
    //FB.Event.subscribe('auth.statusChange', OnLogin);
    FB.getLoginStatus(function (response) {
        if (response.status === 'connected') {
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
                        if (msg == "Success") {
                            $('#fancybox-close').trigger('click');
                            window.setTimeout('openreviewbox()', 600);
                            $('#divLogout').show();
                            return false;
                        }
                        else if (msg == "InActive") {
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
    });
}