$(document).ready(function () {
    $('#hidRootPath').val($('#hdnRootPath').val());
    $.ajax({
        type: "POST",
        url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/checkloginstate",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg === "true") {
                $('#divLogout').show();
            }
        },
        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });
    $('input[type=radio].totalreviewstar').rating({
        required: true
    });
    $('input[type=radio].topcommentstar').rating({
        required: true
    });
    //$('input[type=radio].totalreviewstar').rating('select', comprating, false);
    LoadCompanyTotalRating();
    $('#lnkRegBox').fancybox({
        'padding': 0,
        'showCloseButton': true,
        'modal': true,
        'titleShow': false
    });
    $('#lnkReviewBox').fancybox({
        'padding': 0,
        'showCloseButton': true,
        'modal': true,
        'titleShow': false
    });
    $('#lnkReplyBox').fancybox({
        'padding': 0,
        'showCloseButton': true,
        'modal': true,
        'titleShow': false
    });

    LoadComments($('#hdnProfileClientSk').val(), -1);

    $('#lnkWriteReview').live('click', function () {
        $('#hidCommentType').val('Review');
        $('#lnkRegBox').trigger('click');

        /*$.ajax({
            type: "POST",
            url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/checkloginstate",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg == "false") {
                    $('#divLogout').hide();
                    $('#lnkRegBox').trigger('click');
                    return false;
                }
                else {
                    $('#divLogout').show();
                    $('#lnkReviewBox').trigger('click');
                    return false;
                }
            },
            failure: function () {
                $('#divWriteReviewErr').text('Request Failed. Try Again.');
            }
        });
        return false;*/
    });

    $('.lnkReply').live('click', function () {
        $('#hidCommentType').val('Reply');
        var cid = $(this).closest('.divComments').children('#hdCommentId').attr('value');
        var cname = $(this).closest('.divComments').children('#hdCommenter').attr('value');
        $('#hidCommentId').val(cid);
        $('#hidCommentedBy').val(cname);
        var divToAppend = $(this).closest('#divCom' + cid);
        if (divToAppend.children('#divReply' + cid).length === 0) {
            $(divToAppend).append('<div id="divReply' + cid + '" style="padding-left:30px;"></div>');
        }
        $.ajax({
            type: "POST",
            url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/checkloginstate",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg === "false") {
                    $('#lnkRegBox').trigger('click');
                    return false;
                }
                else {
                    $('#lnkReplyBox').trigger('click');
                    return false;
                }
            },
            failure: function () {
                $('#divWriteReviewErr').text('Request Failed. Try Again.');
            }
        });
        return false;
    });

    $('.lnkSubReply').live('click', function () {
        $('#hidCommentType').val('SubReply');
        var cid = $(this).closest('.divSubComments').children('#hdSubCommentId').attr('value');
        var cname = $(this).closest('.divSubComments').children('#hdCommenter').attr('value');
        $('#hidCommentId').val(cid);
        $('#hidCommentedBy').val(cname);
        var divToAppend = $(this).closest('#divSubCom' + cid);
        if (divToAppend.children('#divSubReply' + cid).length === 0) {
            $(divToAppend).append('<div id="divSubReply' + cid + '" style="padding-left:30px;"></div>');
        }
        $.ajax({
            type: "POST",
            url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/checkloginstate",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg === "false") {
                    $('#lnkRegBox').trigger('click');
                    return false;
                }
                else {
                    $('#lnkReplyBox').trigger('click');
                    return false;
                }
            },
            failure: function () {
                $('#divWriteReviewErr').text('Request Failed. Try Again.');
            }
        });
        return false;
    });

    $('#lnkLogout').live('click', function () {
        $.ajax({
            type: "POST",
            url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/logoutsession",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg === "success") {
                    alert("You have successfully logged out");
                    $('#divLogout').hide();
                    return false;
                }
                else {
                    alert(msg);
                    return false;
                }
            },
            failure: function () {
                $('#divWriteReviewErr').text('Request Failed. Try Again.');
            }
        });
        return false;
    });

    $('.lnkHelpful').live('click', function () {
        var cid = $(this).closest('.divComments').children('#hdCommentId').attr('value');
        var spnHelp = $(this).closest('.spnHelpful');
        spnHelp.text('Sending feedback...');
        var list = [cid];
        var jsonText = JSON.stringify({ list: list });
        $.ajax({
            type: "POST",
            url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/submithelpful",
            data: jsonText,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                if (msg === "Invalid") {
                    spnHelp.html("Was this helpful? <a class='lnkHelpful' href='#Helpful'><img alt='Yes' src='" + $('#hdnRootPath').val() + "images/helpful_button.png' style='vertical-align:middle;'></a>");
                    alert('Request Failed. Try Again.');
                }
                else {
                    spnHelp.next().text(msg);
                    spnHelp.text('Thank you for your feedback.');
                }
            },
            failure: function () {
                alert('Request Failed. Try Again.');
            }
        });
        return false;
    });
    var scrollFunction = function () {
        if ($('#hidLastRecord').val() === '0') {
            var docHeightChk = $(document).height() - $(window).height() - 70;
            var divHeightChk = $('#divCommentDisp').height() - $('#divCommentDisp').position().top;
            if (($(window).scrollTop() >= docHeightChk) || ($(window).scrollTop() >= divHeightChk)) {
                $(window).unbind("scroll");
                $('#divCommentDisp').append('<div class="loadingdiv" style="text-align:center;"><img src="' + $('#hdnRootPath').val() + 'images/small-spinner.gif" alt="Wait" /></div>');
                setTimeout(function () {
                    if ($('#hidLastFetchId').val() > 0) {
                        LoadComments($('#hdnProfileClientSk').val(), $('#hidLastFetchId').val());
                        $(window).scroll(scrollFunction);
                        //                            setTimeout(function () {
                        //                                if ($('#divCommentDisp').height() < $('#googlemap').position().top - $('.divComments:last').height()) {
                        //                                    scrollFunction();
                        //                                }
                        //                                else {
                        //                                    $(window).scroll(scrollFunction);
                        //                                }
                        //                            }, 1000);
                    }
                }, 1000);
            }
        }
    }
    $(window).scroll(scrollFunction);
});

function UpdateReviewRating(ratectrl, rateval) {
    var commentid = $(ratectrl).closest('.divComments').children('#hdCommentId').attr('value');
    var list = [commentid, rateval];
    var jsonText = JSON.stringify({ list: list });
    $.ajax({
        type: "POST",
        url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/updatereviewrating",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg === "Success") {
                $(ratectrl).rating('disable');
            }
            else {
                alert('Request Failed. Try Again.');
            }
        },
        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });
}

function LoadCompanyTotalRating() {
    var comprating = -1;
    var list = [$('#hdnProfileClientSk').val()];
    var jsonText = JSON.stringify({ list: list });

    $.ajax({
        type: "POST",
        url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/getcompanytotalrating",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg !== "Invalid") {
                var result = JSON.parse(msg);
                $('input[type=radio].topcommentstar').rating('enable');
                $('input[type=radio].topcommentstar').rating('select', parseInt(result[0]), false);
                $('input[type=radio].topcommentstar').rating('disable');
                if (parseInt(result[0]) > 0) {
                    $('#spanTopRate').show();
                }
                var starval = parseInt(result[0]) + 1;
                $('#spanRateNum').html(starval + '/5');
                if (parseInt(result[0]) > 0) {
                    $('#spanRateNum').show();
                }
                $('input[type=radio].totalreviewstar').rating('enable');
                $('input[type=radio].totalreviewstar').rating('select', parseInt(result[0]), false);
                $('input[type=radio].totalreviewstar').rating('disable');
                $('#divTotalReviewCount').text(result[1]);
            }
            else {
                alert('Request Failed. Try Again.');
            }
        },
        failure: function () {
            alert('Request Failed. Try Again.');
        }
    });
}

function LoadComments(clientsk, id) {
    var list = [clientsk, id, $('#hdnRootPath').val()];
    var jsonText = JSON.stringify({ list: list });
    var result;
    $.ajax({
        type: "POST",
        url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/fetchcomments",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg === "Invalid") {
                $('#divCommentDisp').text('Problem Fetching Data. Please Refresh(F5).');
            }
            else if (msg == "LastRecord") {
                $('#hidLastRecord').val('1');
                $('.loadingdiv').remove();
            }
            else {
                result = JSON.parse(msg);
                $(result).appendTo($('#divCommentDisp')).hide().slideDown('slow');
            }
            $('.loadingdiv').remove();
            $(result).siblings('.divComments').each(function () {
                var commentid = $(this).children('#hdCommentId').val();
                var divcom = '#divCom' + commentid;
                LoadSubComments(commentid, divcom);
            });
        },
        failure: function () {
            $('#divCommentDisp').text('Problem Fetching Data. Please Refresh(F5).');
        }
    });
}

function LoadSubComments(commentid, divToAppend) {
    if ($(divToAppend).children('#divReply' + commentid).length == 0) {
        $(divToAppend).append('<div id="divReply' + commentid + '" style="padding-left:30px;"></div>');
    }
    var list = [commentid, $('#hdnRootPath').val()];
    var jsonText = JSON.stringify({ list: list });
    $.ajax({
        type: "POST",
        url: $('#hdnRootPath').val() + "controls/reviewmanager.aspx/fetchsubcomments",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg !== "") {
                var result = JSON.parse(msg);
                $(result).appendTo($('#divReply' + commentid)).hide().slideDown('slow');
            }
        },
        failure: function () {
            $(divToAppend).append('Problem Fetching Data. Please Refresh(F5).');
        }
    });
}