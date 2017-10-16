// JScript File

$(document).ready(function () {
    
    $("#frmShare").validate({
        //ignore: ".ignore",
        rules: {

            txtName : { required: true, minlength: 2 },
            txtFrom: { required: true, emailRule: true },
            txtTo: { required: true, multiemails: true },
            
           /*
            hiddenRecaptcha: {
                required: function () {
                    if (grecaptcha.getResponse() == '') {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
            */
            
        },
        messages: {

            txtName: { required: "Required ", minlength: "Must be at least 2 characters" },
            txtFrom: { required: "Required ", emailRule: "Please input valid email address" },
            txtTo: { required: "Required ", multiemails: "Please input valid email addresses" }
          
            //hiddenRecaptcha: { required: "Required "}
        },
        submitHandler: function (form) {
            form.submit();
            return false;
            /*
                if (grecaptcha.getResponse()) {
                    $("#rfqmessage").html("You are not a BOT!");
                    form.submit();
                    return false;
                } else {
                    $("#rfqmessage").html("Please make sure that you are not a BOT!");
                   // return false;
                    
                }
            */
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



function recaptchaCallback() {
   $('#hiddenRecaptcha').valid();
}
   