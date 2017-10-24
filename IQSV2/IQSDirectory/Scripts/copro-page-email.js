// JScript File
$(document).ready(function () {    
    $("#frmShare").validate({        
        ignore: ".ignore",
        rules: {
            txtFirstName: { required: true },
            txtLastName: { required: true },
            txtEmailAddress: { required: true, emailRule: true },
            txtCompanyName: { required: true },
            txtZip: { required: true, zipRule: true }
            /*,      
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

            txtFirstName: { required: "Required " },
            txtLastName: { required: "Required " },
            txtEmailAddress: { required: "Required ", emailRule: "Invalid" },
            txtCompanyName: { required: "Required " },
            txtZip: {required: "Required", zipRule:"Invalid"}
             //hiddenRecaptcha: { required: "Required "}
        },
        submitHandler: function (form) {
            alert("Thanks Lord");
            form.submit();
            return false;
          
        }
    });

    jQuery.validator.addMethod("captchaRule", function (value, element) {
        var _varFlag = false;

        var response = grecaptcha.getResponse();
        $("#hiddenRecaptcha").value(response);
        if (response.length == 0)
            _varFlag = false;
        else
            _varFlag = true;

        alert(_varFlag);
        return this.optional(element) || _varFlag;
    }, 'Please make sure that you are not a BOT!');

    
    jQuery.validator.addMethod("emailRule", function (value, element) {
        Exp = /\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        return this.optional(element) || Exp.test(value);
    }, 'Please enter a valid email.');

    jQuery.validator.addMethod("zipRule", function (value, element) {
        Exp = /^\d{5}$/;
        return this.optional(element) || Exp.test(value);
    }, 'Please enter a valid zip.');
    
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
    
    //var response = grecaptcha.getResponse();
   // document.getElementById('rfqmessage').innerHTML
    /*
    if (response.length == 0) {
        //reCaptcha not verified
        _varFlag = false;
        alert("Please verify that you are not a BOT!");
        //document.getElementById('rfqmessage').innerHTML = "Please verify that you are not a BOT!";
        return false;
    }
    */
    
    return true;

}


function recaptchaCallback() {
    //$('#hiddenRecaptcha').valid();
    $("#frmShare").validate();
}
   