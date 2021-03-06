﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="copro-page-email_v1.ascx.cs" Inherits="IQSDirectory.Controls.copro_page_email_v1" %>
<script type="text/javascript">
    $(document).ready(function () {         
    $("#frmMaster").validate({        
        ignore: ".ignore",
        rules: {
            txtFirstName: { required: true },
            txtLastName: { required: true },
            txtEmailAddress: { required: true, emailRule: true },
            txtCompanyName: { required: true },
            txtZip: { required: true }            ,
            hiddenRecaptcha1: { required: function () {
                    if (grecaptcha.getResponse(recaptcha1) == '') {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
        },
        messages: {
            txtFirstName: { required: "Required " },
            txtLastName: { required: "Required " },
            txtEmailAddress: { required: "Required ", emailRule: "Invalid" },
            txtCompanyName: { required: "Required " },
            txtZip: {required: "Required"},
            hiddenRecaptcha1: { required: "Required "}
        },
        submitHandler: function (form) {
          
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
    $('#btnSend').on('click', function () {
        
        if ($("#frmMaster").valid())
        {           
            //$(this).prop('disabled', 'disabled');
            list = [$('#txtFirstName').val(), $('#txtLastName').val(), $('#txtEmailAddress').val(), $('#txtCompanyName').val(), $('#txtZip').val(), $('#txtSubject').val(), $('#txtMessage').val(), $('#hdnProfileClientSk').val()];
            jsonText = JSON.stringify({ list: list });
            $.ajax({
                type: "POST",
                url: $('#hdnSrhRootPath').val() + "controls/reviewmanager.aspx/sendcoproemail",
                data: jsonText,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    //alert(msg);
                    if (msg.d == "Success") {
                        //alert("Mail has been sent sucessfully!!!");
                        $.fancybox({
                            type: 'iframe',
                            href: $('#hdnSrhRootPath').val() + 'copro-page-email-thankyou.aspx'                            
                        });
                        $('#txtFirstName').val('');
                        $('#txtLastName').val('');
                        $('#txtEmailAddress').val('');
                        $('#txtCompanyName').val('');
                        $('#txtZip').val('');                       
                        $('#txtSubject').val('');
                        $('#txtMessage').val('');
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

});

function verify_callback1(response) {        
   $('#hiddenRecaptcha1').val(response);
   $("#frmMaster").valid();
}
</script>
<div id="profileForm">
              
    <h5><i class="far fa-envelope"></i><span id="divEmailCName" runat="server"></span></h5>
          <div id="profContInfo">
                <ul>
                <li>First Name:<span class="require">* </span></li>
                <li><input type="text" id="txtFirstName" name="txtFirstName" class="commenttextbox"  /></li>
                <li>Last Name:<span class="require">* </span></li>
                <li><input type="text" id="txtLastName" name="txtLastName" class="commenttextbox"  /></li>
                <li>Email Address :<span class="require">* </span></li>
                <li><input type="text" id="txtEmailAddress" name="txtEmailAddress" class="commenttextbox"  /></li>
                <li>Company Name :<span class="require">* </span></li>
                <li><input type="text" id="txtCompanyName"  name="txtCompanyName" class="commenttextbox"  /></li>                
                <li>Zip/Postal Code :<span class="require">* </span></li>
                <li><input type="text" id="txtZip" name="txtZip" class="commenttextbox"  /></li>                
                <li>Subject :</li>
                <li><input type="text" id="txtSubject" name="txtSubject" class="rfqtextboxsub width90" maxlength="200"  /></li>
                <li>Message :</li>
                <li><textarea id="txtMessage" name="txtMessage" class="TextCtrlArea width90" style="height:64px;"  ></textarea></li>
                <li><!--<div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"  ></div>
                    <input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha"  data-callback="recaptchaCallback"  /> 
                -->
                    <div id="recaptcha1"></div>
                    <input type="hidden" class="hiddenRecaptcha1 required" name="hiddenRecaptcha1" id="hiddenRecaptcha1"  data-callback="verify_callback1"  /> 
  
                </li>
              
                </ul>
<div id="ip_error" class="error" runat="server" ></div>
              <div id="divStatus" runat="server" ></div>
<div ><input type="hidden" name="val_ipblock" id="val_ipblock" value="-1" runat="server" />
    
    <input type="button" id="btnSend" EnableViewState="false" class="hoverable btn waves-effect waves-light orange white-text" value="Send"  />
</div>
</div>
<div class="clearfix" ></div>        
<div class="reqText">
<div class="ProfilewarnTextRFQ">
    <p><span class="requireD">WARNING: This form is not to be used for solicitation. </span> Solicitation is a violation of the <a href="/DirectoryTermsConditions.htm"> Terms and Conditions </a>  of this site. Solicitors will have their IP banned and reported to the FCC. <br /> <br /> We will keep your personal information private and secure.We will not share your email address with any third parties.</p><br /> 
</div>   
</div>
</div>