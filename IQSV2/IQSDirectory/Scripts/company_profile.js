﻿$(document).ready(function () {
    $('#hidRootPath').val($('#hdnSrhRootPath').val());
    $.ajax({
        type: "POST",
        url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/checkloginstate",
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
        url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/getcompanytotalrating",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg.d !== "Invalid") {
                var result = [];
                var ms = msg.d;
                ms = ms.replace(/\\/g, '\\');
                result = JSON.parse(ms);
                var rate = parseInt(result[0]);
                $('input[type=radio].topcommentstar').rating('enable');
                $('input[type=radio].topcommentstar').rating('select',rate , false);
                $('input[type=radio].topcommentstar').rating('disable');
                if (rate > 0) {
                    $('#spanTopRate').show();
                }
                var starval = rate + 1;
                $('#spanRateNum').html(starval + '/5');
                if (rate > 0) {
                    $('#spanRateNum').show();
                }
                $('input[type=radio].totalreviewstar').rating('enable');
                $('input[type=radio].totalreviewstar').rating('select', rate, false);
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
    var list = [clientsk, id];
    var jsonText = JSON.stringify({ list: list });
    var result;
    $.ajax({
        type: "POST",
        url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/fetchcomments",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg.d === "Invalid") {
                $('#divCommentDisp').text('Problem Fetching Data. Please Refresh(F5).');
            }
            else if (msg.d == "LastRecord") {
                $('#hidLastRecord').val('1');
                $('.loadingdiv').remove();
            }
            else {
                var result = [];
                var ms = msg.d;
                ms = ms.replace(/\\/g, '\\');
                result = JSON.parse(ms);
                var str = "";
                for (i in result)
                {
                    
                    var rate = parseInt(result[i].Rating) - 1;
                   
                    str += "<div class='divComments' id='divCommentid'><input type='hidden' id='hdCommentId' value='" + result[i].CommentId + "' />";
                    str += "<input type='hidden' id='hdCommenter' value='" + result[i].CName + "' />";
                    str += "<div class='review_title_wrapper'>";
                    str += "<h2>" + result[i].Title + "</h2>";
                    str += "<div class='review_meta_wrapper'><h3>By <span>" + result[i].CName + "</span>- <span>" + result[i].CDate + "</span></h3></div>";
                    str += "</div>";
                    str += "<span class='review_rating_wrapper'>";
                    str += "<input name='star1' type='radio' class='commentstar" + result[i].CommentId + "' value='1' title='1'/>";
                    str += "<input name='star1' type='radio' class='commentstar" + result[i].CommentId + "' value='2' title='2'/>";
                    str += "<input name='star1' type='radio' class='commentstar" + result[i].CommentId + "' value='3' title='3'/>";
                    str += "<input name='star1' type='radio' class='commentstar" + result[i].CommentId + "' value='4' title='4'/>";
                    str += "<input name='star1' type='radio' class='commentstar" + result[i].CommentId + "' value='5' title='5'/>";
                    str += "</span>";
                    str += "<div style='clear:both;'></div>";
                    str += "<div class='review_content_wrapper'>" + result[i].Review + "</div>";

                    str += "<div id='divCom" + result[i].CommentId + "' class='review_action_wrapper'>";
                    str += "<span class='spnHelpful'>Was this helpful? <a class='lnkHelpful small' href='#Helpful'>Yes</a></span>";
                    str += "<span class='spnHelpCount' >";
                    str += result[i].Helpful + "</span><span class='spnHelpCountDesc'>&nbsp;people found this review useful";
                    str += "</span>";
                    str += "<span><a class='lnkReply small' href='#Reply'>Reply</a></span>";
                    str += "</div>";
                    str += "<script type='text/javascript'>";
                    str += "$('input[type=radio].commentstar" + result[i].CommentId + "').rating({required: true});";
                    str += "$('input[type=radio].commentstar" + result[i].CommentId + "').rating('select',"+rate+", false);";
                    str += "$('input[type=radio].commentstar" + result[i].CommentId + "').rating('disable');";
                    if (i == result.length-1)
                        str += "$('#hidLastFetchId').val(" + result[i].CommentId + ");";
                    str += "</script>";
                    //if (RatingReceived > 0)
                    str += "</div>";
                }
                
                $(str).appendTo($('#divCommentDisp')).hide().slideDown('slow');
                /*$("#divCommentDisp").find("script").each(function (i) {
                    eval($(this).text());
                });*/
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
    var list = [commentid];
    var jsonText = JSON.stringify({ list: list });
    $.ajax({
        type: "POST",
        url: $('#hidRootPath').val() + "controls/reviewmanager.aspx/fetchsubcomments",
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg.d !== "") {
                var result = JSON.parse(msg.d);
                //$(result).appendTo($('#divReply' + commentid)).hide().slideDown('slow');
            }
        },
        failure: function () {
            $(divToAppend).append('Problem Fetching Data. Please Refresh(F5).');
        }
    });
}