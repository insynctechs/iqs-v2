﻿$(document).ready(function () {

    $("#frmMaster").validate({
        ignore: ".ignore",
        rules: {
            txtCompanyName: { required: true },
            txtCompanyPhone: { required: true, phoneRule: true },
            txtCompanyWebsite: { required: true, webRule: true },            
            txtContactName: { required: true },
            txtContactEmailAddress: { required: true, emailRule: true },
            hiddenRecaptcha: {
                required: function () {
                    if (grecaptcha.getResponse() == '') {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
        },
        messages: {
            txtCompanyName: { required: "Required " },
            txtCompanyPhone: { required: "Required ", phoneRule: "Invalid" },
            txtCompanyWebsite: { required: "Required ", webRule: "Invalid" },
            txtContactName: { required: "Required" },            
            txtContactEmailAddress: { required: "Required", emailRule: "Invalid" }
            ,hiddenRecaptcha: { required: "Required" }
        },
        submitHandler: function (form) {
            form.submit();
            return false;

        }
    });


    $('#btnReset').on('click', function () {
        $('#txtCompanyName').val('');
        $('#txtCompanyPhone').val('');
        $('#txtCompanyWebsite').val('');
        $('#txtContactName').val('');
        $('#txtContactTitle').val('');
        $('#txtContactEmailAddress').val('');
        $('#txtProductArea').val('');

    });

    $('#btnSubmit').on('click', function () {

        if ($("#frmMaster").valid()) {
            //$(this).prop('disabled', 'disabled');
            $(this).text("Sending Email");
            list = [$('#txtCompanyName').val(), $('#txtCompanyPhone').val(), $('#txtCompanyWebsite').val(), $('#txtProductArea').val(), $('#txtContactName').val(), $('#txtContactTitle').val(), $('#txtContactEmailAddress').val(), $("#hdnCategoryName").val()];
            jsonText = JSON.stringify({ list: list });
            $.ajax({
                type: "POST",
                url:  "controls/reviewmanager.aspx/premiumlistingemail",
                data: jsonText,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {                  
                    if (msg.d.indexOf("Success!") != -1)
                    {
                        $("#contentHomePremium").hide();
                        $("#successBlock").show();
                        var str = msg.d.replace("Success!", "");
                        $("#returnBlock").html(str);

                    }
                    else if (msg.d == "Error1") {
                        alert("Unexpected Error Occured. Please contact IQSDirectory");
                    }
                    else if (msg.d == "MailError") {
                        alert("Error sending in email");
                    }
                    else { 
                        alert("Unexpected Error Occured. Try Again!!");
                    }
                    

                },

                failure: function () {
                    alert('Request Failed. Try Again.');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert('error' + textStatus + "--" + errorThrown);
                }
            });

        }
        return false;
    });

      
    function recaptchaCallback()
    {  alert("thx Lord");
        $("#hiddenRecaptcha").val(grecaptcha.getResponse());
        alert($("#hiddenRecaptcha").val());
        $("#frmMaster").valid();
    }

    jQuery.validator.addMethod("emailRule", function (value, element) {
        Exp = /\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        return this.optional(element) || Exp.test(value);
    }, 'Please enter a valid email.');

    jQuery.validator.addMethod("phoneRule", function (value, element) {
        Exp = /^(\+)?[0-9\-\.\s\(\)]{10,}$/;
        return this.optional(element) || Exp.test(value);
    }, 'Invalid Ph#.');

    jQuery.validator.addMethod("webRule", function (value, element) {
        Exp = /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/;
        return this.optional(element) || Exp.test(value);
    }, 'Invalid Web URL.');
    
    jQuery.validator.addMethod(
        "multiemails",
        function (value, element) {
            if (this.optional(element)) // return true on optional element
                return true;
            var emails = value.split(/[;,]+/); // split element by , and ;
            valid = true;
            for (var i in emails) {
                value = emails[i];
                valid = valid &&
                    jQuery.validator.methods.email.call(this, $.trim(value), element);
            }
            return valid;
        },

        jQuery.validator.messages.multiemails
    );

  
});

function SetAlphabet(obj)
{
     document.getElementById('hdnCategoryAlphabet').value = obj;
}



/********** To check the max length for Meta Description,Title tag, Tracking script and Keyword **********/
function fnMaxLength(val,Id)
{
    var _varId = Id.id;
    var exp=val.which;
    if(lTrim(document.getElementById(_varId).value).length > 399)
    {
        if(navigator.appName=="Microsoft Internet Explorer")
        {
            event.keyCode = 0;
        }
        else
        {
            if(exp == 8 || exp == 0)
            {
                return true;
            }
            else
            {
                val.preventDefault();
                return;
            }
        }
    }
}

function fnEnterKeyCommon(e)
{ 
    var exp;
    if(navigator.appName == 'Microsoft Internet Explorer')
    {
        exp=e.keyCode;
    }
    else
    {
        exp=e.which; 
    } 
    if(exp==13)
    {
        return false;
    }
    else
    {
        return true;
    }

}

