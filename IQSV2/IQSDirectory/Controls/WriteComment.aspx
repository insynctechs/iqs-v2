﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WriteComment.aspx.cs" Inherits="IQSDirectory.WriteComment" %>
<!DOCTYPE html>
<html lang="en-US">
<head runat="server">
    <title>IQSDirectory: Post Reviews and Ratings</title>
    <META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
    <script type="text/javascript" defer async src="<%:RootPath %>Scripts/write_comment.js"></script>       
</head>
<body>
<div id="divLogin" class="write_comment_wrapper">
    <h2 class="pophead">Write a Review</h2>
<form id="frmComment" runat="server">
    <div id="divRegForm"  >
        
      <div class="write_comment_inner">
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
      <input type="text" id="txtRating" name="txtRating" class="commenttextbox hide_elem"  /> <!-- -->
      <p>
      Review Title (Required):<br />
      <input type="text" id="txtTitle" name="txtTitle" class="commenttextbox" />
      </p>
        		
      <div class="write_comment_captcha_wrapper">
       
      </div>
      <p class="p1">Review (Required):<br />
      <textarea id="txtReview" name="txtReview" class="commentTextArea" ></textarea></p>
      <p class="p2">
      <a href="#SubmitComment" id="lnkSubmitComment" class="large">Submit</a>
      </p>
          </div>
      
    </div>
    
    </form>
</div>
</body>
</html>
