// JScript File

$(document).ready(function () {
    $("#divRegForm").show();
    $("#divSuccess").hide();
    $("#frmShare").validate({
        ignore: ".ignore",
        rules: {

            txtName : { required: true},
            txtFrom: { required: true },
            txtTo: { required: true},
            hiddenRecaptcha2: {
                required: function () {
                    if (grecaptcha.getResponse(recaptcha2) == '') {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
            
            
        },
        messages: {

            txtName: { required: "Required " },
            txtFrom: { required: "Required " },
            txtTo: { required: "Required "},          
            hiddenRecaptcha2: { required: "Required "}
        },
        submitHandler: function (form) {
            //form.submit();
            return false;
            
        }
    });

    $('#btnShare').on('click', function () {
        alert("btn clicked");
        if ($("#frmShare").valid()) {
            //$(this).prop('disabled', 'disabled');
            list = [$('#txtName').val(), $('#txtFrom').val(), $('#txtTo').val(), $('#txtTitle').val(), $('#txtUrl').val(), $('#txtDescription').val()];
            jsonText = JSON.stringify({ list: list });
            $.ajax({
                type: "POST",
                url: $('#hdnSrhRootPath').val() + "controls/reviewmanager.aspx/coprosharepage_email",
                data: jsonText,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    //alert(msg);
                    if (msg.d == "Success") {
                        $("#divSuccess").show();
                        $("#divRegForm").hide();
                        //alert("Mail has been sent sucessfully!!!");
                        /*
                        $.fancybox({
                            type: 'iframe',
                            href: $('#hdnSrhRootPath').val() + 'copro-page-email-thankyou.aspx'
                        });*/
                        $('#txtName').val('');
                        $('#txtFrom').val('');
                        $('#txtTo').val('');
                        
                    }
                    else if (msg == "Country") {
                        alert("The Use of this Form is Restricted - Please Contact IQSDirectory with Questions.");
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
                    //$('#btnSend').removeAttr('disabled');

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

    jQuery.validator.addMethod("emailRule", function (value, element) {
        Exp = /\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        return this.optional(element) || Exp.test(value);
    }, 'Please enter a valid email.');

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





   