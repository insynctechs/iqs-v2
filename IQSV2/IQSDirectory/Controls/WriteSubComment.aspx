<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WriteSubComment.aspx.cs" Inherits="IQSDirectory.WriteSubComment" %>
<!DOCTYPE html>
<html lang="en-US">
<head id="Head1" runat="server">
    <title>IQSDirectory: Post Review Reply</title>
<META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <script type="text/javascript" defer async src="<%:RootPath %>Scripts/write_subcomment.js"></script>
</head>
<body>
<div id="divLogin" class="write_comment_wrapper">
    <h2 class="pophead">Post Reply To a Review</h2>
<form id="form1" runat="server">
    <div id="divRegForm">
      <div class="write_comment_inner">
      
      
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

      </div>
      <div class="write_comment_review">
      <p class="p1">Review (Required):<br />
      <textarea id="txtReview" ></textarea></p>
      <p class="p2">
      <a href="#SubmitSubComment" id="lnkSubmitSubComment" class="large submit">Submit</a>
      </p>
      </div>
    </div>
    </form>
</div>
</body>
</html>
