// JScript File
$(document).ready(function () {    
    $("#frmShare").validate({        
        rules: {

            txtFirstName: { required: true },
            txtLastName: { required: true },
            txtEmailAddress: { required: true, emailRule: true },
            txtCompanyName: { required: true },
            txtZip: { required: true }
            
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

            txtFirstName: { required: "Required " },
            txtLastName: { required: "Required " },
            txtEmailAddress: { required: "Required ", emailRule: "Invalid" },
            txtCompanyName: { required: "Required " },
            txtZip: {required: "Required"}
          
            //hiddenRecaptcha: { required: "Required "}
        },
        submitHandler: function (form) {
            alert("Thanks Lord");
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

function jqClick()
{
    /*
    var response = grecaptcha.getResponse();

    if (response.length == 0) {
        //reCaptcha not verified
        _varFlag = false;
        alert("Please verify that you are not a BOT!");
        //document.getElementById('rfqmessage').innerHTML = "Please verify that you are not a BOT!";
        return false;
    }
    */

    alert("js executed");
    return true;

}


function recaptchaCallback() {
   $('#hiddenRecaptcha').valid();
}
   