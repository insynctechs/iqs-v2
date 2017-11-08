// JScript File
function GetCategoryname(objControl)
{
if (document.getElementById('rbtnSelect' + objControl).checked)
    document.getElementById('hdnCategoryName').value =  document.getElementById('hdnCategoryName').value + objControl + ',';
else
{
var replacedvalue =objControl+',';
    document.getElementById('hdnCategoryName').value= document.getElementById('hdnCategoryName').value.replace(replacedvalue,',');  
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

function isUrlValid(url) {
    return /^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(url);
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

function Validate()
{
    if(document.getElementById('txtCompanyName').value == '')
    {
        error(15,'Company Name','');
        document.getElementById('txtCompanyName').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtCompanyName').value).length==0)
    {
        error(15,'Company Name','');
        document.getElementById('txtCompanyName').value = '';
        document.getElementById('txtCompanyName').focus();
        return false;
    }
    else if(document.getElementById('txtCompanyPhone').value == '')
    {
        error(15,'Company Phone','');
        document.getElementById('txtCompanyPhone').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtCompanyPhone').value).length==0)
    {
        error(15,'Company Phone','');
        document.getElementById('txtCompanyPhone').value = '';
        document.getElementById('txtCompanyPhone').focus();
        return false;
    }
    else if (!validatePhone('txtCompanyPhone')) {
        error(16,'Company Phone','');
        //document.getElementById('txtCompanyPhone').value = '';
        document.getElementById('txtCompanyPhone').focus();
        return false;

    }
    else if(document.getElementById('txtCompanyWebsite').value == ''){
        error(15,'Company Website','');
        document.getElementById('txtCompanyWebsite').value = '';
        document.getElementById('txtCompanyWebsite').focus();
        return false;

    }
    else if(lTrim(document.getElementById('txtCompanyPhone').value).length==0){
        error(15,'Company Website','');
        document.getElementById('txtCompanyWebsite').value = '';
        document.getElementById('txtCompanyWebsite').focus();
        return false;

    }

    else if (!isUrlValid(document.getElementById('txtCompanyWebsite').value)) {
        error(16,'Company Website','');
       //document.getElementById('txtCompanyWebsite').value = '';
        document.getElementById('txtCompanyWebsite').focus();
        return false;

    }
    
    else if (document.getElementById('txtContactName').value == '') {
        error(15, 'Contact Name', '');
        document.getElementById('txtContactName').focus();
        return false;
    }
    else if (lTrim(document.getElementById('txtContactName').value).length == 0) {
        error(15, 'Contact Name', '');
        document.getElementById('txtContactName').value = '';
        document.getElementById('txtContactName').focus();
        return false;
    }
    else if (document.getElementById('txtContactEmailAddress').value == '') {
        error(16, 'Contact Email Address', '');
        document.getElementById('txtContactEmailAddress').focus();
        return false;
    }
    else if (lTrim(document.getElementById('txtContactEmailAddress').value).length == 0) {
        error(15, 'Contact Email Address', '');
        document.getElementById('txtContactEmailAddress').value = '';
        document.getElementById('txtContactEmailAddress').focus();
        return false;
    }
    else if (ValidateEmailAddress(document.getElementById('txtContactEmailAddress').value) == false) {
        error(16, 'Contact Email Address', '');
        //document.getElementById('txtContactEmailAddress').value='';
        document.getElementById('txtContactEmailAddress').focus();
        return false;
    }
    else if (document.getElementById('Captcha1_txtCaptcha').value == '') {
        error(15, 'Captcha', '');
        document.getElementById('Captcha1_txtCaptcha').focus();
        return false;
    }
    else if (lTrim(document.getElementById('Captcha1_txtCaptcha').value).length == 0) {
        error(15, 'Captcha', '');
        document.getElementById('Captcha1_txtCaptcha').value = '';
        document.getElementById('Captcha1_txtCaptcha').focus();
        return false;
    }
    else {
        return true;
    }   
    
}

/************** To validate Email for the given Email id ******************/
function ValidateEmailAddress(EmailId)
{
	try
	{	
		reg1=/\w+([-.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
		matches="";
		matches=reg1.exec(EmailId);
		if(matches == null)
		{
		    //alert('Please enter valid Email Id');
		    //document.getElementById('txtContactEmailAddress').focus();
		    //document.getElementById('txtContactEmailAddress').value = '';
		    return false;
		}
		else
        {
            return true;
        }   
	}
	catch(error)
	{
		alert(error)
	}
}

function SetAlphabet(obj)
{
     document.getElementById('hdnCategoryAlphabet').value = obj;
}

/* To validate required field in standard listing */
function ValidateStandardListing()
{
    var elementRef = document.getElementById('rbtnlstAmount');
    var inputWebsiteElementArray = elementRef.getElementsByTagName('INPUT');
    
    if((inputWebsiteElementArray[0].checked == false) && (inputWebsiteElementArray[1].checked == false))
    {
        error(19,'Amount details','');
        return false;
    }
    else if(document.getElementById('txtCompanyName').value == '')
    {
        error(15,'Company Name','');
        document.getElementById('txtCompanyName').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtCompanyName').value).length==0)
    {
        error(15,'Company Name','');
        document.getElementById('txtCompanyName').value = '';
        document.getElementById('txtCompanyName').focus();
        return false;
    }
    else if(document.getElementById('txtCompanyPhone').value == '')
    {
        error(15,'Company Phone','');
        document.getElementById('txtCompanyPhone').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtCompanyPhone').value).length==0)
    {
        error(15,'Company Phone','');
        document.getElementById('txtCompanyPhone').value = '';
        document.getElementById('txtCompanyPhone').focus();
        return false;
    }
    else if (!validatePhone('txtCompanyPhone')) {
        error(16, 'Company Phone', '');
        //document.getElementById('txtCompanyPhone').value = '';
        document.getElementById('txtCompanyPhone').focus();
        return false;

    }
    else if (document.getElementById('txtCompanyWebsite').value == '') {
        error(15, 'Company Website', '');
        document.getElementById('txtCompanyWebsite').value = '';
        document.getElementById('txtCompanyWebsite').focus();
        return false;

    }
    else if (lTrim(document.getElementById('txtCompanyPhone').value).length == 0) {
        error(15, 'Company Website', '');
        document.getElementById('txtCompanyWebsite').value = '';
        document.getElementById('txtCompanyWebsite').focus();
        return false;

    }

    else if (!isUrlValid(document.getElementById('txtCompanyWebsite').value)) {
        error(16, 'Company Website', '');
        //document.getElementById('txtCompanyWebsite').value = '';
        document.getElementById('txtCompanyWebsite').focus();
        return false;

    }
    
    else if(document.getElementById('txtContactName').value == '')
    {
        error(15,'Contact Name','');
        document.getElementById('txtContactName').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtContactName').value).length==0)
    {
        error(15,'Contact Name','');
        document.getElementById('txtContactName').value = '';
        document.getElementById('txtContactName').focus();
        return false;
    }
    else if(document.getElementById('txtContactEmailAddress').value == '')
    {
        error(15,'Contact Email Address','');
        document.getElementById('txtContactEmailAddress').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtContactEmailAddress').value).length==0)
    {
        error(15,'Contact Email Address','');
        document.getElementById('txtContactEmailAddress').value = '';
        document.getElementById('txtContactEmailAddress').focus();
        return false;
    }
    else if (ValidateEmailAddress(document.getElementById('txtContactEmailAddress').value) == false) {
            error(16, 'Contact Email Address', '');
            //document.getElementById('txtContactEmailAddress').value='';
            document.getElementById('txtContactEmailAddress').focus();
            return false; 
    } 
    else if(document.getElementById('Captcha1_txtCaptcha').value == '')
    {
        error(15,'Captcha','');
        document.getElementById('Captcha1_txtCaptcha').focus();
        return false;
    }
    else if(lTrim(document.getElementById('Captcha1_txtCaptcha').value).length==0)
    {
        error(15,'Captcha','');
        document.getElementById('Captcha1_txtCaptcha').value = '';
        document.getElementById('Captcha1_txtCaptcha').focus();
        return false;
    }
    else if(lTrim(document.getElementById('txtareaDescription').value).length > 400)
    {
        alert('Enter the description upto 400 Characters');
        return false;
    }
    else {
        return true;
    }
}

function Reset()
{
    document.getElementById('txtCompanyName').value = '';
    document.getElementById('txtCompanyPhone').value = '';
    document.getElementById('txtCompanyWebsite').value = '';
    document.getElementById('txtContactName').value = '';
    document.getElementById('txtContactTitle').value = '';
    document.getElementById('txtContactEmailAddress').value = '';
    document.getElementById('Captcha1_txtCaptcha').value = '';
    return false;
}

function ResetStandardListing()
{
    document.getElementById('txtCompanyName').value = '';
    document.getElementById('txtCompanyPhone').value = '';
    document.getElementById('txtCompanyWebsite').value = '';
    document.getElementById('txtContactName').value = '';
    document.getElementById('txtContactTitle').value = '';
    document.getElementById('txtContactEmailAddress').value = '';
    document.getElementById('Captcha1_txtCaptcha').value = '';
    document.getElementById('txtareaDescription').value = '';
    //document.getElementById('rbtnlstCategory').selectedValue = '';
}

//function btnG_onclick()
//{
//window.open("http://gmini.iqsdirectory.com/search?q="+ document.getElementById("txtSearch").value + "&btnG=&site=IQSdirectory&client=IQS&proxystylesheet=IQS&output=xml_no_dtd");
//}

/********** To check the max length for Meta Description,Title tag, Tracking script and Keyword **********/
function fnMaxLength(val,Id)
{
    var _varId = Id.id;
    var exp=val.which;
    if(lTrim(document.getElementById(_varId).value).length > 399)
    {
        if(navigator.appName=="Microsoft Internet Explorer")
        {
            event.keyCode = 0;
        }
        else
        {
            if(exp == 8 || exp == 0)
            {
                return true;
            }
            else
            {
                val.preventDefault();
                return;
            }
        }
    }
}

function fnEnterKeyCommon(e)
{ 
    var exp;
    if(navigator.appName == 'Microsoft Internet Explorer')
    {
        exp=e.keyCode;
    }
    else
    {
        exp=e.which; 
    } 
    if(exp==13)
    {
        return false;
    }
    else
    {
        return true;
    }

}

