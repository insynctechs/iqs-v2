<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WriteSubComment.aspx.cs" Inherits="IQSDirectory.WriteSubComment" %>
<!DOCTYPE html>
<html lang="en-US">
<head id="Head1" runat="server">
    <title>For writing Sub Comments</title>
<META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //alert($('#hidCommentId').val());
            //alert($('#hidCommentType').val());
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
            $('#txtTopic').val($('#hidCommentedBy').val());

            $('#lnkSubmitSubComment').click(function () {
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

                var cid = $('#hidCommentId').val();
                if ($.trim($('#txtName').val()) == '') {
                    alert('Please Enter Name');
                    $('#txtName').focus();
                    return false;
                }
                if ($.trim($('#txtReview').val()) == '') {
                    alert('Please Enter Review');
                    $('#txtReview').focus();
                    return false;
                }
                if ($.trim($('#CaptchaReview1_txtCaptcha').val()) == '') {
                    alert('Please Enter Code');
                    $('#CaptchaReview1_txtCaptcha').focus();
                    return false;
                }

                list = [$('#txtUserId').val(), cid, $('#txtReview').val(), $('#hidCommentType').val(), $('#CaptchaReview1_txtCaptcha').val(), $('#txtTopic').val(), $('#hidRootPath').val()];
                jsonText = JSON.stringify({ list: list });
                $.ajax({
                    type: "POST",
                    url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/writesubreview",
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
                            alert("Reply Posted Successfully");
                            $('#fancybox-close').trigger('click');
                            if ($('#hidCommentType').val() == 'SubReply') {
                                $(result).appendTo($('#divSubReply' + cid)).hide().slideDown('slow');
                            }
                            else {
                                $(result).appendTo($('#divReply' + cid)).hide().slideDown('slow');
                            }
                        }
                    },
                    failure: function () {
                        alert('Request Failed. Try Again.');
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
      <h2>Reply to Review</h2>
      <p>
      Display Name:<br />
      <input type="text" id="txtName" class="commenttextbox" readonly="readonly" />
      <input type="hidden" id="txtUserId" class="commenttextbox" />
      </p>
      <p>
      In Reply To<br />
      <input type="text" id="txtTopic" class="commenttextbox" readonly="readonly" />
      </p>
      </div>  		
      <div class="write_comment_captcha_wrapper">

      <p>
      
      </p>
      </div>
      <div class="write_comment_review">
      <p class="p1">Review (Required):<br />
      <textarea id="txtReview" ></textarea></p>
      <p class="p2">
      <a href="#SubmitSubComment" id="lnkSubmitSubComment"><img src="<%=RootPath %>images/submit_button.png" alt="Login" /></a>
      </p>
      </div>
    </div>
    </form>
</div>
</body>
</html>
