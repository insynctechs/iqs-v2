﻿// JScript File

$(document).ready(function () {
    
    $("#frmRFQ").validate({
        debug:true,
        //ignore: ".ignore",
        rules: {

            txtCompanyName : { required: true, minlength: 2 },
            txtContactName: { required: true, minlength: 2 },
            txtContactEmail: { required: true, emailRule: true },
            txtContactPhone: { required: true },
            txtContactCity: { required: true }
            
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

            txtCompanyName: { required: "Required ", minlength: "Invalid" },
            txtContactName: { required: "Required ", minlength: "Invalid" },
            txtContactEmail: { required: "Required ", emailRule: "Invalid" },
            txtContactPhone: { required: "Required " },
            txtContactCity: { required: "Required " }
            
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
        },
        invalidHandler: function (event, validator) {
            // 'this' refers to the form
            
            var errors = validator.numberOfInvalids();
            if (errors) {
                var message = errors == 1
                    ? 'You missed 1 field. It has been highlighted'
                    : 'You missed ' + errors + ' fields. They have been highlighted';
                alert(message);
               
            } else {
                alert("No error");
            }
        }
    });

    
    jQuery.validator.addMethod("emailRule", function (value, element) {
        Exp = /\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        return this.optional(element) || Exp.test(value);
    }, 'Invalid email.');

    

});



function recaptchaCallback() {
   $('#hiddenRecaptcha').valid();
}

var _varCount;
var _varFlag;
var _varBrowser = navigator.appName;
function GetDetails(CompanyName, EmailId, SequenceNo, TierSK, ClientSK) {
    _varCount = 1;
    if (document.getElementById('hdnCompanyName').value == '') {
        document.getElementById('hdnCompanyName').value = CompanyName;
    }
    else {
        document.getElementById('hdnCompanyName').value = document.getElementById('hdnCompanyName').value + '`' + CompanyName;
    }

    if (_varCount == '1') {
        document.getElementById('hdnEmailId').value = EmailId;
    }
    else {
        document.getElementById('hdnEmailId').value = document.getElementById('hdnEmailId').value + '`' + EmailId;
    }

    if (_varCount == '1') {
        document.getElementById('hdnSequenceNo').value = SequenceNo;
    }
    else {
        document.getElementById('hdnSequenceNo').value = document.getElementById('hdnSequenceNo').value + '`' + SequenceNo;
    }

    if (_varCount == '1') {
        document.getElementById('hdnTierSK').value = TierSK;
    }
    else {
        document.getElementById('hdnTierSK').value = document.getElementById('hdnTierSK').value + '`' + TierSK;
    }

    if (document.getElementById('hdnClientSK').value == '') {
        document.getElementById('hdnClientSK').value = ClientSK;
    }
    else {
        document.getElementById('hdnClientSK').value = document.getElementById('hdnClientSK').value + '`' + ClientSK;
    }
    _varCount = _varCount + 1;
}

/*********** For resetting the values ************/
function ResetValues() {
    var tblID = document.getElementById('tblCategories');
    var tbllength = tblID.rows.length - 1;
    var tblcount = 0;

    document.getElementById('txtCompanyName').value = "";
    document.getElementById('txtContactName').value = "";
    document.getElementById('txtContactEmail').value = "";
    document.getElementById('txtContactPhone').value = "";
    // document.getElementById('txtContactAddress').value = "";
    document.getElementById('txtContactCity').value = "";
    //document.getElementById('txtContactState').value = "";
    //document.getElementById('txtContactCountry').value = "";
    document.getElementById('txtDescription').value = "";
    // document.getElementById('Captcha1_txtCaptcha').value = "";
    //document.getElementById('chkAttachment').checked = false;
    for (tblcount = 0; tblcount <= tbllength; tblcount++) {
        if (tblID.rows[tblcount].cells[0].childNodes[0].checked == true) {
            tblID.rows[tblcount].cells[0].childNodes[0].checked = false;
        }
    }
}

function validatePhone(txtPhone) {
    var a = document.getElementById(txtPhone).value;
    //var filter = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
    var filter = /^(\+)?[0-9\-\.\s\(\)]{10,}$/;
    if (filter.test(a)) {
        return true;
    }
    else {
        return false;
    }
}

/*function validateEmail(txtEmail) {
var a = document.getElementById(txtEmail).value;
var filter = /^([\w\-\.]+@([\w\-]+\.)+[\w-]{2,4})?$/
if (filter.test(a)) {
return true;
}
else {
return false;
}
}​*/

/************** To validate Email for the given Email id ******************/
function ValidateEmailAddress(EmailId) {
    try {
        reg1 = /\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
        matches = "";
        matches = reg1.exec(EmailId);
        if (matches == null) {
            //alert('Please enter valid Email Id');
            //document.getElementById('txtContactEmailAddress').focus();
            //document.getElementById('txtContactEmailAddress').value = '';
            return false;
        }
        else {
            return true;
        }
    }
    catch (error) {
        alert(error)
    }
}


/********* To clear controls ********/
function ClearControls() {
    //alert(document.getElementById('hdnSelectedtext').value);
    document.getElementById('hdnSelectedtext').value = '';
    document.getElementById('Captcha1_txtCaptcha').value = '';
    //document.getElementById('txtContactDetails').value = '';
    //document.getElementById('txtContactAddress').value = '';
    document.getElementById('txtContactCity').value = '';
    // document.getElementById('txtContactState').value = '';
    //document.getElementById('txtContactCountry').value = '';   
    document.getElementById('txtContactEmail').value = '';
    document.getElementById('txtContactName').value = '';
    document.getElementById('txtCompanyName').value = '';
    document.getElementById('txtContactPhone').value = '';
    document.getElementById('txtDescription').innerText = '';
    //document.getElementById('chkAttachment').checked = false;
}



/************* For checking all the checkbox on click of CheckAll button ***********/
function CheckAll(ButtonIds) {
    var _varId = ButtonIds.split('`');
    for (var i = 0; i < _varId.length; i++) {
        document.getElementById(_varId[i]).checked = true;
    }
}


function SetSelectedValues() {
    document.getElementById('rfqmessage').innerHTML = "";
    var tblID = document.getElementById('tblCategories');
    var tbllength = tblID.rows.length - 1;
    var tblcount = 0;
    var Selectedtext = '';
    for (tblcount = 0; tblcount <= tbllength; tblcount++) {
        if (tblID.rows[tblcount].cells[0].childNodes[0].checked == true) {
            _varFlag = true;
            break;
        }
        else {
            _varFlag = false;
        }
    }
    if (_varFlag == true) {
        for (tblcount = 0; tblcount <= tbllength; tblcount++) {
            if (tblID.rows[tblcount].cells[0].childNodes[0].checked == true) {
                if (Selectedtext == '') {
                    Selectedtext = tblID.rows[tblcount].cells[1].childNodes[0].innerHTML + "|" + tblID.rows[tblcount].cells[1].childNodes[1].value;
                }
                else {
                    Selectedtext = Selectedtext + "^" + tblID.rows[tblcount].cells[1].childNodes[0].innerHTML + "|" + tblID.rows[tblcount].cells[1].childNodes[1].value;
                }
            }
        }
        document.getElementById('hdnSelectedtext').value = Selectedtext;
        _varFlag = '';
        
        //document.getElementById('btnSubmit').click();
        
        var response = grecaptcha.getResponse();

        if (response.length == 0) {
            //reCaptcha not verified
            _varFlag = false;
           // alert("Please verify that you are not a BOT!");
            document.getElementById('rfqmessage').innerHTML = "Please verify that you are not a BOT!";
            return false;
        }
       
        return true;

    }
    else {
        //error(19,'Company Name','');
        //tblID.rows[tblcount].cells[0].childNodes[0].focus();
        //alert("Please select Company names");

        document.getElementById('rfqmessage').innerHTML = "Please select Company names";
        _varFlag = '';
        return false;
    }
}

/********** To check the max length for Meta Description,Title tag, Tracking script and Keyword **********/
function fnMaxLength(val, Id) {
    var _varId = Id.id;
    var exp = val.which;
    if (trim(document.getElementById(_varId).value).length > 4999) {
        if (navigator.appName == "Microsoft Internet Explorer") {
            event.keyCode = 0;
        }
        else {
            if (exp == 8 || exp == 0) {
                return true;
            }
            else {
                val.preventDefault();
                return;
            }
        }
    }
}

function fnKeyCode(Code) {
    var exp;
    var _vartextboxId = document.getElementById('txtDescription');
    if (_varBrowser == 'Microsoft Internet Explorer') {
        if (event.keyCode == 94 || event.keyCode == 124) {
            event.keyCode = 0;
        }
        else {
            //return true;
            fnMaxLength(event, _vartextboxId);
        }
    }
    else {
        exp = code.which;
        if (exp == 94 || exp == 124) {
            code.preventDefault();
            return;
        }
        else {
            return true;
            fnMaxLength(event, _vartextboxId);
        }
    }
} 

/*	Purpose : Left trimming the  space */
function lTrim(LtrimValue) {
    var length = LtrimValue.length;
    var counter = 0;
    for (counter = 0; counter < length; counter++) {
        if (LtrimValue.charCodeAt(counter) != 32) break;
    }
    return LtrimValue.substring(counter, LtrimValue.length);
}

/*	Purpose : Right trimming the  space */
function rTrim(RtrimValue) {
    var count = 0;
    for (counter = RtrimValue.length - 1; counter >= 0; counter--) {
        if (RtrimValue.charCodeAt(counter) == 32)
            count++;
        else
            break;
    }
    return RtrimValue.substring(0, RtrimValue.length - count)
}

/*	Purpose : Left and Right trimming the  space */
function trim(val) {
    return lTrim(rTrim(val));
}
