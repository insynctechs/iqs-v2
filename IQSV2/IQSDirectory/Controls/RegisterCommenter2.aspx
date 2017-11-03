<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterCommenter2.aspx.cs" Inherits="IQSDirectory.RegisterCommenter2" %>
<!DOCTYPE html>
<html lang="en-US">
<head>
    <title>IQSDirectory::User Registration Form</title>
    <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">    
    <style type="text/css">                
    </style>
     <script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  type="text/javascript"></script> 
    <script type="text/javascript" defer async src="<%:RootPath %>Scripts/register_commenter.js"></script>
        <script>
             

            
            // This is called with the results from from FB.getLoginStatus().
            function statusChangeCallback(response) {
                
                // The response object is returned with a status field that lets the
                // app know the current login status of the person.
                // Full docs on the response object can be found in the documentation
                // for FB.getLoginStatus().
                if (response.status === 'connected') {
                    // Logged into your app and Facebook.
                    performFBActions();
                } else {
                    // The person is not logged into your app or we are unable to tell.
                    
                }
            }

            // This function is called when someone finishes with the Login
            // Button.  See the onlogin handler attached to it in the sample
            // code below.
            function checkLoginState() {
                FB.getLoginStatus(function (response) {
                    statusChangeCallback(response);
                });
            }

            window.fbAsyncInit = function () {
                FB.init({
                    appId: '504326666290316',
                    cookie: true,  // enable cookies to allow the server to access 
                    // the session
                    xfbml: true,  // parse social plugins on this page
                    version: 'v2.8' // use graph api version 2.8
                });

                // Now that we've initialized the JavaScript SDK, we call 
                // FB.getLoginStatus().  This function gets the state of the
                // person visiting this page and can return one of three states to
                // the callback you provide.  They can be:
                //
                // 1. Logged into your app ('connected')
                // 2. Logged into Facebook, but not your app ('not_authorized')
                // 3. Not logged into Facebook and can't tell if they are logged into
                //    your app or not.
                //
                // These three cases are handled in the callback function.

                FB.getLoginStatus(function (response) {
                    //statusChangeCallback(response);
                });

            };

            // Load the SDK asynchronously
            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s); js.id = id;
                js.src = "//connect.facebook.net/en_US/sdk.js";
                fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));

            // Here we run a very simple test of the Graph API after login is
            // successful.  See statusChangeCallback() for when this call is made.
            function performFBActions() {
                alert('Welcome!  Fetching your information.... ');
                /*FB.api('/me', function (response) {
                    alert('Successful login for: ' + response.name);

                });*/
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
                            if (msg.d == "Success") {
                                $('#fancybox-close').trigger('click');
                                window.setTimeout('openreviewbox()', 600);
                                $('#divLogout').show();
                                return false;
                            }
                            else if (msg.d == "InActive") {
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

</script>
</head>
<body>
<div id="divLogin" class="register_popup">
<form id="frmRegCommeter" runat="server">
     <h2 class="pophead">USER REGISTRATION / LOGIN</h2>
<div id="divRegForm" class="register_form_wrapper" >
      <div class="register_popup_left">
      <p></p>
      <h2>Fill in the info below to register.</h2><br />
      <p>
      Display Name (If left empty Name will be used):<br />
            <input type="text" id="txtRegDName" name="txtRegDName" class="commenttextbox" /></p>
          <p>
        Name:<span class="requireD">* </span><br />
            <input type="text" id="txtRegName" name="txtRegName" class="commenttextbox" /></p>
        <p>E-mail Address:<span class="requireD">* </span><br />
            <input type="text" id="txtRegEmail" name="txtRegEmail" class="commenttextbox" /></p>
        <p>Password (Min 5 characters):<span class="requireD">* </span><br />
            <input type="password" id="txtRegPass" name="txtRegPass"  class="commenttextbox" maxlength="15" /></p>
        <p>Verify Password:<span class="requireD">* </span><br />
            <input type="password" id="txtRegVerify" name="txtRegVerify" class="commenttextbox" maxlength="15" />
            <input type="hidden" id="hidIp" value="<%=UserIP %>" />
        </p>
         <!--
              <div class="g-recaptcha" data-sitekey="6Lc72zMUAAAAABk1ajqMH-ThUswu6BIps5JS10s_"  ></div>
          <p>
              <input type="hidden" class="hiddenRecaptcha required" name="hiddenRecaptcha" id="hiddenRecaptcha"  data-callback="recaptchaCallback"  /> 
          </p>-->
          <div id="recaptcha3"></div>
          <input type="hidden" class="hiddenRecaptcha3 required" name="hiddenRecaptcha3" id="hiddenRecaptcha3"  data-callback="verify_callback3"  /> 
         
        <p>
              <a href="#Register" id="lnkRegister" class="register_btn large" >Register</a>

          </p>
          <p>test</p>
          <!-- <div id="divErr" style="clear:both;text-align: right;color:Red; display:none;"></div>-->
      </div>
    
      <div id="divRegChild" class="register_popup_right" >
      <div>
      <h2>You can Log-in with Facebook</h2>
      <fb:login-button scope="public_profile,email" onlogin="checkLoginState();">
</fb:login-button>
          
      </div>
      <div>
      <h2>Or If Already registered?<br />Log-in below.</h2>
      <p>
      E-mail Address:<br />
      <input type="text" id="txtEmail" class="commenttextbox" />
      </p>
      <p>
      Password:<br />
      <input type="password" id="txtPassword" class="commenttextbox" />
      </p>
      <p class="register_login_bot_wrapper">
      <span class="register_checkbox_wrapper"><input type="checkbox" id="chkRemember" /> Remember me?</span>
      <a href="#Login" id="lnkLogin" class="large">Login</a><br />
      <a href="#ForgotPassword" id="lnkForgotPassword">Forgot Password?</a>
          
      </p>
      </div>
      <div id="divForgot" class="register_forgot_wrapper">
        <h2>Forgot Password</h2>
        <p>
          Enter Registered E-mail Address:<br />
          <input type="text" id="txtForgotEmail" class="commenttextbox" />
          </p>
          <p class="register_forgot_btn_wrapper">
        <a href="#ForgotSubmit" id="lnkForgotSubmit" class="large">Get Password</a>
      </p>
      </div>
      <div class="cleardiv"></div>
      </div>
     
        		
</div>
<div id="divSuccess" class="register_success">
<p></p>
    <br />
    <br />
      <h2>User Registration Successfull !!!</h2><br />
      <p>Your login details have been sent to your email address.</p>
      <p>Please login using your email address and password to post your reviews and comments.</p>
    <p>&nbsp;</p>
      <a href="#Login" id="lnkRegLogin" class="large">Login</a>
</div>

<div id="divSuccessForgot" class="register_success">
<p></p>
    <br />
    <br />
      <h2>Your request has been processed successfully !</h2><br />
      <h2>The password has been emailed to your registered email address.</h2><br /><br />
      <a href="#Close" id="lnkClose">Close</a>
</div>

 </form>
	</div>
   <script type='text/javascript'>
           //this will insert recaptcha in the form
            //captchaCallBack();  
        //fbLogin();
    </script>
    
</body>
</html>
