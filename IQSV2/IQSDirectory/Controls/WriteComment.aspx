<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WriteComment.aspx.cs" Inherits="IQSDirectory.WriteComment" %>
<!DOCTYPE html>
<html lang="en-US">
<head runat="server">
    <title>Form for Writing Review Comments</title>
    <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('input[type=radio].writereviewstar').rating({
                required: true,
                callback: function (value, link) {
                    $('#txtRating').val(value);
                }
            });
            $.ajax({
                type: "POST",
                url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/getloginusername",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    var commenter = msg;
                    commenter = $.parseJSON(commenter);
                    $('#txtName').val(commenter[1]);
                    $('#txtUserId').val(commenter[0]);
                },
                failure: function () {
                    alert('Request Failed. Try Again.');
                }
            });
            $('#lnkSubmitComment').click(function () {
                var list = [$('#txtUserId').val()];
                var jsonText = JSON.stringify({ list: list });
                $.ajax({
                    type: "POST",
                    url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/checkcommenteractive",
                    data: jsonText,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg == "Invalid") {
                            $('#divLogout').hide();
                            alert("Your account is disabled.");
                            return false;
                        }
                    },
                    failure: function () {
                        alert('Request Failed. Try Again.');
                        return false;
                    }
                });
                if ($.trim($('#txtName').val()) == '') {
                    alert('Please Enter Name');
                    $('#txtName').focus();
                    return false;
                }
                if ($.trim($('#txtRating').val()) == '') {
                    alert('Please Rate the Company');
                    $('#txtRating').focus();
                    return false;
                }
                if ($.trim($('#txtTitle').val()) == '') {
                    alert('Please Enter Title');
                    $('#txtTitle').focus();
                    return false;
                }
                if ($.trim($('#txtReview').val()) == '') {
                    alert('Please Enter Review');
                    $('#txtReview').focus();
                    return false;
                }
                if ($.trim($('#CaptchaReview1_txtCaptcha')) == '') {
                    alert('Please Enter Code');
                    $('#CaptchaReview1_txtCaptcha').focus();
                    return false;
                }

                list = [$('#txtUserId').val(), $('#txtRating').val(), $('#txtTitle').val(), $('#txtReview').val(), $('#hdnProfileClientSk').val(), $('#CaptchaReview1_txtCaptcha').val(), $('#hidRootPath').val()];
                jsonText = JSON.stringify({ list: list });
                $.ajax({
                    type: "POST",
                    url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/writereview",
                    data: jsonText,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg == "Invalid") {
                            alert("Unexpected Error Occured. Try Again!!");
                        }
                        else if (msg == "Foul Word") {
                            $('#divLogout').hide();
                            alert("You have used foul word(s).\nYour review/comment has been removed and session terminated.");
                            $('#fancybox-close').trigger('click');
                        }
                        else if (msg == "Code") {
                            alert("The code you entered was not correct.");
                        }
                        else {
                            var result = JSON.parse(msg);
                            alert("Review Posted Successfully");
                            $('#fancybox-close').trigger('click');
                            $(result).prependTo($('#divCommentDisp')).hide().slideDown('slow');
                            LoadCompanyTotalRating();
                        }
                    },
                    failure: function () {
                        $('#divWriteReviewErr').text('Request Failed. Try Again.');
                    }
                });
                return false;
            });
        });
    </script>

</head>
<body>
<div id="divLogin" class="write_comment_wrapper">
<form id="form1" runat="server">
    <div id="divRegForm">
      <div class="write_comment_inner">
      <br />
      <h2>Write a Review</h2>
      <p>
      Display Name:<br />
      <input type="text" id="txtName" class="commenttextbox" readonly="readonly" />
      <input type="hidden" id="txtUserId" class="commenttextbox" class="hide_elem" />
      </p>
      Rating (Required):<br />
      <div class="write_comment_rating" id="divReviewRate">
        <input name="star1" type="radio" class="writereviewstar" value="1" title="1"/>
        <input name="star1" type="radio" class="writereviewstar" value="2" title="2"/>
        <input name="star1" type="radio" class="writereviewstar" value="3" title="3"/>
        <input name="star1" type="radio" class="writereviewstar" value="4" title="4"/>
        <input name="star1" type="radio" class="writereviewstar" value="5" title="5"/>
      </div>
      <input type="text" id="txtRating" class="commenttextbox" class="hide_elem" />
      <p>
      Review Title (Required):<br />
      <input type="text" id="txtTitle" class="commenttextbox" />
      </p>
      </div>  		
      <div class="write_comment_captcha_wrapper">
       
      </div>
      <div class="write_comment_review">
      <p class="p1">Review (Required):<br />
      <textarea id="txtReview" ></textarea></p>
      <p class="p2">
      <a href="#SubmitComment" id="lnkSubmitComment"><img src="<%=RootPath %>images/submit_button.png" alt="Login" /></a>
      </p>
      </div>
    </div>
    <div id="divSuccess" class="hide_elem">
    <p></p>
        <br />
        <br />
          <h2>User Registration Successfull !!!</h2><br />
          <h2>Your Password has been sent to your email.</h2><br /><br />
          <a href="#Close" id="lnkClose"><img src="<%=RootPath %>images/close.png" alt="Close" /></a>
    </div>
    </form>
</div>
</body>
</html>
