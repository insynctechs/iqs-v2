using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Controls_MailSend : System.Web.UI.Page
{
    public static string rootDirPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rootDirPath = Request.QueryString["p"].ToString();
            Captcha1.UrlPath = rootDirPath;
            txtTitle.Value = Request.QueryString["title"].ToString();
            txtDescription.Value = Request.QueryString["des"].ToString();
            txtUrl.Value = Request.QueryString["url"].ToString();
            this.Page.Header.Controls.Add(new LiteralControl("<link rel='stylesheet' type='text/css' href='" + rootDirPath + "css/styler.css' />"));
            this.Page.Header.Controls.Add(new LiteralControl("<link rel='stylesheet' type='text/css' href='" + rootDirPath + "css/jquery-ui.css' />"));
            this.Page.Header.Controls.Add(new LiteralControl("<link rel='stylesheet' type='text/css' href='" + rootDirPath + "css/jquery.tagsinput.css' />"));
            this.Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='" + rootDirPath + "js/jquery-1.7.2.min.js'></script>"));
            this.Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='" + rootDirPath + "js/jquery.tagsinput.js'></script>"));
            this.Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='" + rootDirPath + "js/jquery-ui.js'></script>"));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine("$(document).ready(function(){");
            sb.AppendLine("$('#txtTo').tagsInput({");
            sb.AppendLine("'width': '360px',");
            sb.AppendLine("'height': '60px',");
            sb.AppendLine("'defaultText': 'add email',");
            sb.AppendLine("});");
            sb.AppendLine("$('#lnkSend').click(function(){");
            sb.AppendLine("if ($.trim($('#txtName').val()) == '') {");
            sb.AppendLine("alert('Please Enter Name');");
            sb.AppendLine("$('#txtName').focus();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("if ($.trim($('#txtFrom').val()) == '') {");
            sb.AppendLine("alert('Please Enter Email');");
            sb.AppendLine("$('#txtFrom').focus();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("if (!isValidEmailAddress($('#txtFrom').val())) {");
            sb.AppendLine("alert('Enter a Valid Email');");
            sb.AppendLine("$('#txtFrom').focus();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("if ($.trim($('#txtTo').val()) == '') {");
            sb.AppendLine("alert('Please Enter Receiver(s) Email');");
            sb.AppendLine("$('#txtTo').focus();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("if ($.trim($('#Captcha1_txtCaptcha').val()) == '') {");
            sb.AppendLine("alert('Please Enter Code');");
            sb.AppendLine("$('#Captcha1_txtCaptcha').focus();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("$('#lnkSend').fadeOut(function(){$('.spanwait').show();});");
            sb.AppendLine("var list = [$('#txtName').val(), $('#txtFrom').val(), $('#txtTo').val(), $('#txtTitle').val(), $('#txtDescription').val(), $('#txtUrl').val(), $('#Captcha1_txtCaptcha').val()];");
            sb.AppendLine("var jsonText = JSON.stringify({ list: list });");
            sb.AppendLine("$.ajax({");
            sb.AppendLine("type: 'POST',");
            sb.AppendLine("url: '" + rootDirPath + "controls/reviewmanager.aspx/sendpagebyemail',");
            sb.AppendLine("data: jsonText,");
            sb.AppendLine("contentType: 'application/json; charset=utf-8',");
            sb.AppendLine("dataType: 'json',");
            sb.AppendLine("async: true,");
            sb.AppendLine("cache: false,");
            sb.AppendLine("success: function (msg) {");
            sb.AppendLine("if (msg == 'Success') {");
            sb.AppendLine("$('#divRegForm').slideUp('slow', function() {");
            sb.AppendLine("$('#divSuccess').slideDown();");
            sb.AppendLine("});return false;");
            sb.AppendLine("}");
            sb.AppendLine("else if (msg == 'Captcha') {");
            sb.AppendLine("$('#lnkSend').fadeIn(function(){$('.spanwait').hide();alert('The code you entered was not correct!!');});");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("else {");
            sb.AppendLine("alert(msg);");
            sb.AppendLine("$('.spanwait').hide();");
            sb.AppendLine("$('#lnkSend').show();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("},");
            sb.AppendLine("failure: function () {");
            sb.AppendLine("alert('Request Failed. Try Again.');");
            sb.AppendLine("$('.spanwait').hide();");
            sb.AppendLine("$('#lnkSend').show();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            sb.AppendLine("});");
            sb.AppendLine("return false;");
            sb.AppendLine("});");
            sb.AppendLine("$('#lnkClose').click(function(){");
            sb.AppendLine("parent.$.fancybox.close();");
            sb.AppendLine("return false;");
            sb.AppendLine("});");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
            this.Page.Header.Controls.Add(new LiteralControl(sb.ToString()));
        }
    }
}