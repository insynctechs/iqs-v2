<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterCommenter.aspx.cs" Inherits="IQSDirectory.RegisterCommenter" %>
<!DOCTYPE html>
<html lang="en-US">
<head>
    <title>IQSDirectory::User Registration Form</title>
    <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    
    <style type="text/css">        
        
    </style>
    <script type="text/javascript" defer async src="<%:RootPath %>Scripts/register_commenter.js"></script>
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
            <input type="text" id="txtRegDName" class="commenttextbox" /></p>
          <p>
        Name:<span class="requireD">* </span><br />
            <input type="text" id="txtRegName" class="commenttextbox" /></p>
        <p>E-mail Address:<span class="requireD">* </span><br />
            <input type="text" id="txtRegEmail" class="commenttextbox" /></p>
        <p>Password (Min 5 characters):<span class="requireD">* </span><br />
            <input type="password" id="txtRegPass" class="commenttextbox" maxlength="15" /></p>
        <p>Verify Password:<span class="requireD">* </span><br />
            <input type="password" id="txtRegVerify" class="commenttextbox" maxlength="15" />
            <input type="hidden" id="hidIp" value="<%=UserIP %>" />
        </p>
          <p >
              <a href="#Register" id="lnkRegister" class="register_btn large" >Register</a>

          </p>
          
          <!-- <div id="divErr" style="clear:both;text-align: right;color:Red; display:none;"></div>-->
      </div>
      <div id="divRegChild" class="register_popup_right" >
      <div>
      <h2>You can Log-in with Facebook</h2>
      <div id="fb-root"></div>
        <div class="fb-login-button" autologoutlink="false" onlogin="fbLogin();" scope="user_birthday,email" >
            Login
        </div>
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
   
    
</body>
</html>
