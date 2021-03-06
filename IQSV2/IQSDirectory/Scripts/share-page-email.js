﻿// JScript File

$(document).ready(function () {
    
    $("#frmShare").validate({
        //ignore: ".ignore",
        rules: {

            txtName : { required: true},
            txtFrom: { required: true, emailRule: true },
            txtTo: { required: true, multiemails: true },
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

            txtName: { required: "Required " },
            txtFrom: { required: "Required ", emailRule: "Invalid" },
            txtTo: { required: "Required ", multiemails: "Invalid" },          
            hiddenRecaptcha: { required: "Required "}
        },
        submitHandler: function (form) {
            form.submit();
            return false;
            
        }
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

function jqClick()
{
return true;

}


function recaptchaCallback() {
    $('#hiddenRecaptcha').valid();
    $("#frmShare").validate();
}
   