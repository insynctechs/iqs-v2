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
            var commenter = msg.d;
            var ms = commenter.replace(/\\/g, '\\');
            commenter = JSON.parse(ms);
            $('#txtName').val(commenter[1]);
            $('#txtUserId').val(commenter[0]);
        },
        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });

    $("#frmComment").validate({
        ignore: ".ignore",
        rules: {
            txtName: { required: true },
            txtRating: { required: true },
            txtTitle: { required: true },
            txtReview: { required: true }
        },
        messages: {
            txtName: { required: "Required " },
            txtRating: { required: "Required " },
            txtTitle: { required: "Required " },
            txtReview: { required: "Required " }
        },
        submitHandler: function (form) { }
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
                if (msg.d == "Invalid") {
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
        /*
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
        */
        if ($("#frmComment").valid())
        {
            list = [$('#txtUserId').val(), $('#txtRating').val(), $('#txtTitle').val(), $('#txtReview').val(), $('#hdnProfileClientSk').val(), $('#hidRootPath').val()];
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
                    if (msg.d == "Invalid") {
                        alert("Unexpected Error Occured. Try Again!!");
                    }
                    else if (msg.d == "Foul Word") {
                        $('#divLogout').hide();
                        alert("You have used foul word(s).\nYour review/comment has been removed and session terminated.");
                        $('#fancybox-close').trigger('click');
                    }
                    else if (msg.d == "Code") {
                        alert("The code you entered was not correct.");
                    }
                    else {

                        alert("Review Posted Successfully");
                        $('#fancybox-close').trigger('click');
                        str = GetCommentsHTML(msg.d);
                        $(str).appendTo($('#divCommentDisp')).hide().slideDown('slow');
                        LoadCompanyTotalRating();
                    }
                },
                failure: function () {
                    $('#divWriteReviewErr').text('Request Failed. Try Again.');
                }
            });
        }
        return false;
    });
});