if (are_cookies_enabled()) {
    var cookieName = 'profilelogin';
    var cookie = $.cookie(cookieName);
    //alert(cookie)
    if (cookie !== null && cookie!="") {
        //alert('inside cookie null');
        var list = cookie.split("|| ||");
        var acomp = new Array();
        $.each(list, function (key, val) {
            var vl = val.split("| |");
            acomp.push(vl[0]);
        });
        if (list.length > 0) {
            var lastval = list[list.length - 2];
            if (lastval !== null && lastval!="") {
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
    
    $('#divSuccess').slideUp('slow', function () {
        $('#divRegForm').slideDown();
    });
    return false;
});

$("#frmRegForgot").validate({
    rules: {
        txtForgotEmail: { required: true, emailRule: true }
            },
    messages: {
        txtForgotEmail: { required: "Required ", emailRule: "Invalid" }
        
    },
    submitHandler: function (form) { }
});
$("#lnkForgotSubmit").click(function () {
    if ($("#frmRegForgot").valid()) {
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
                if (msg.d == "Success") {
                    $('#divRegForm').slideUp('slow', function () {
                        $('#divSuccessForgot').slideDown();
                    });
                    return false;
                }
                else if (msg.d === "Invalid") {
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
    }
    return false;
});

$("#frmRegLogin").validate({
    rules: {
        txtEmail: { required: true, emailRule: true },
        txtPassword: { required: true }
    },
    messages: {
        txtEmail: { required: "Required ", emailRule: "Invalid" },
        txtPassword: { required: "Required " }
    },
    submitHandler: function (form) { }
});



$('#lnkLogin').click(function () {
    if ($("#frmRegLogin").valid()) {
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

                if (msg == "Success" || msg.d == "Success") {
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
    }
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

$("#frmRegCommenter").validate({
    ignore: ".ignore",
    rules: {
        txtRegName: { required: true },
        txtRegEmail: { required: true, emailRule: true },
        txtRegPass: { required: true },
        txtRegVerify: { required: true, equalTo: "#txtRegPass" },
        hiddenRecaptcha3: {
            required: function () {
                if (grecaptcha.getResponse(recaptcha3) == '') {
                    return true;
                } else {
                    return false;
                }
            }
        }        
    },
    messages: {
        txtRegName: { required: "Required " },
        txtRegEmail: { required: "Required ", emailRule: "Invalid" },
        txtRegPass: { required: "Required " },
        txtRegVerify: { required: "Required ", equalTo: "Verify Password Same as Password" },       
        hiddenRecaptcha3: { required: "Required " }
    },
    submitHandler: function (form) { }
});

$('#lnkRegister').click(function () {
    
    if ($("#frmRegCommenter").valid()) {
        var list = [$('#txtRegDName').val(), $('#txtRegName').val(), $('#txtRegEmail').val(), $('#txtRegPass').val(), $('#hidIp').val()];

        var jsonText = JSON.stringify({ list: list, doaction: "yes", returntype: '' });

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
                if (msg.d == "Success") {
                    $('#divRegForm').slideUp('slow', function () {
                        $('#divSuccess').slideDown();
                    });
                }
                else if (msg.d == "Exists") {
                    alert("Email Address You Entered Already Exists!!");
                }
                else if (msg.d == "IP") {
                    alert("You have exceeded maximum (5) registration from this IP");
                }
                else if (msg.d == "") {
                    alert("Unexpected Error Occured. Try Again!!");
                }
                else {
                    alert(msg.d);
                }
            },

            failure: function () {
                alert('Request Failed. Try Again.');
            }
        });
        return false;
    }
    
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

