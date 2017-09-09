function loadWebPreviewDefault(path)
{
	//$(document).ready(function () {
    if (document.getElementById('preview_iframe')) document.getElementById('preview_iframe').src = path + "images/cardboard-placeholder.jpg"; 
	if(document.getElementById('preview_iframe2'))
	{
		document.getElementById('preview_iframe2').src = path + "images/cardboard-placeholder.jpg";
		$('#secpage2 aside').css('height', document.getElementById('preview_iframe2').getBoundingClientRect().height + "px"); 
	}
	if(document.getElementById('preview_iframe3'))
	{
		document.getElementById('preview_iframe3').src = path + "images/cardboard-placeholder.jpg";
		$('#secpage3 aside').css('height', document.getElementById('preview_iframe3').getBoundingClientRect().height + "px"); 
	}


}
function loadWebPreview(site, tier, thisObj) {
    var id= 'preview_iframe'+ tier;
    if (document.getElementById(id)) { document.getElementById(id).src = "about:blank"; document.getElementById(id).src = site; document.getElementById(id).name = thisObj.href; }
    else if (document.getElementById('PlaceHolder_Preview')) { document.getElementById('PlaceHolder_Preview').src = site; document.getElementById(id).name = thisObj.href; }
    
}

function setPreviewWidth() { }

function effectiveDeviceWidth() {
    var deviceWidth = window.orientation == 0 ? window.screen.height : window.screen.width;
    // iOS returns available pixels, Android returns pixels / pixel ratio
    // http://www.quirksmode.org/blog/archives/2012/07/more_about_devi.html
    if (navigator.userAgent.indexOf('Android') >= 0 && window.devicePixelRatio) {
        deviceWidth = deviceWidth / window.devicePixelRatio;
    }
    return deviceWidth;
}
function effectiveDeviceHeight() {
    var deviceHeight = window.orientation == 0 ? window.screen.width : window.screen.height;
    // iOS returns available pixels, Android returns pixels / pixel ratio
    // http://www.quirksmode.org/blog/archives/2012/07/more_about_devi.html
    if (navigator.userAgent.indexOf('Android') >= 0 && window.devicePixelRatio) {
        deviceHeight = deviceHeight / window.devicePixelRatio;
    }
    return deviceHeight;
}
function getPosition(element) {
    var xPosition = 0;
    var yPosition = 0;

    while (element) {
        xPosition += (element.offsetLeft - element.scrollLeft + element.clientLeft);
        yPosition += (element.offsetTop - element.scrollTop + element.clientTop);
        element = element.offsetParent;
    }
    return { x: xPosition, y: yPosition };
}
$(document).ready(function () {

    var boxheight = 900;
    var subfact = Math.floor(boxheight / 4);
    var fact = 0.48;
    if ($(window).width() < 700)
        fact = 0.35;
    else if ($(window).width() < 1300)
        fact = 0.48;
    else if ($(window).width() < 1500)
        fact = 0.54;
    else if ($(window).width() < 1700)
        fact = 0.62;
    else if ($(window).width() < 1900)
        fact = 0.68;
    else if ($(window).width() < 2200)
        fact = 0.7;
    else if ($(window).width() < 2500)
        fact = 0.72;
    else
        fact = 0.75;
    var cutheight = parseInt(boxheight * fact);
    $("#iframe_mask1").click(function () {
        //outboundTracker(); msnTracker();
        var framesource = document.getElementById('preview_iframe').name;
        if (!framesource.includes("cardboard-placeholder.jpg"))
            window.open(framesource, '_blank')
        // alert(document.getElementById('preview_iframe').src);
    });
    $("#iframe_mask2").click(function () {
        //outboundTracker(); msnTracker();
        var framesource = document.getElementById('preview_iframe2').name;
        if (!framesource.includes("cardboard-placeholder.jpg"))
            window.open(framesource, '_blank')
        // alert(document.getElementById('preview_iframe').src);
    });
    $("#iframe_mask3").click(function () {
        //outboundTracker(); msnTracker();
        var framesource = document.getElementById('preview_iframe').name;
        if (!framesource.includes("cardboard-placeholder.jpg"))
            window.open(framesource, '_blank')
        // alert(document.getElementById('preview_iframe').src);
    });
    $('#secpage1art li.licname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var mod = (y - subfact) % h; //+ 250;	
        var f = parseInt(y / h);
        var top = y; //(f * h);
        var offset = parseInt(h / 2);
        //alert( mod + '---' + offset);
        if (mod > offset)
            offset = mod; // - offset ;
        else
            offset = parseInt(h); // - (mod - offset)) ;
        top = Math.max(0, $(window).scrollTop() - subfact + (mod / 4));
        var elemheight = $("#secpage1art").height();


        if (top + cutheight > elemheight) {
            top = top - (top + cutheight - elemheight);
        }

        //alert(top);			
        $('#secpage1 iframe.foriframe').css('margin-top', top + "px");
        $('#secpage1 div#iframe_mask1').css('margin-top', top + "px");


    });

    /*  $('#secpage1art li.licname a').mousemove(function (e) {
    var y = e.pageY;
    var x = e.pageX;
    var h = $(window).height();
    var h1 = effectiveDeviceHeight();
    var el = document.querySelector("#preview_iframe");
    var tel = document.querySelector("#secpage1art");
    var position = getPosition(el);

    var ifheight = el.getBoundingClientRect().height;
    var txtheight = tel.getBoundingClientRect().height;
    var mod = (y - position.y) % h; //+ 250;	
    var f = parseInt(y / h);
    var top = y; //(f * h);
    var top1 = $(window).scrollTop();

    //(elem
    top = Math.max(0, top1 - position.y + (mod / 4));

    var elemheight = $("#secpage1art").height();


    if (top + ifheight > elemheight) {
    top = top - (top + ifheight - elemheight);
    }
    //alert(txtheight);
	      
    //alert("elemheight=" + elemheight + "\r\nelementtop=" + position.y + "\r\nwindowheight=" + h + "\r\n" + "deviceheight=" + h1 + "\r\ntop=" + top + "\r\nscrolltop=" + top1 + "\r\nmousey=" + y + "\r\npreview iframeheight=" + ifheight);
    //alert(top);			
    $('#secpage1 iframe.foriframe').css('margin-top', top + "px");



    });*/

    $('#secpage2art li.licname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var mod = (y - subfact) % h; //+ 250;	
        var f = parseInt(y / h);
        var top = y; //(f * h);
        var offset = parseInt(h / 2);
        //alert( mod + '---' + offset);
        if (mod > offset)
            offset = mod; // - offset ;
        else
            offset = parseInt(h); // - (mod - offset)) ;
        top = Math.max(0, $(window).scrollTop() - subfact + (mod / 4));
        var elemheight = $("#secpage2art").height();


        if (top + cutheight > elemheight) {
            top = top - (top + cutheight - elemheight);
        }

        //alert(top);			
        $('#secpage2 iframe.foriframe').css('margin-top', top + "px");
        $('#secpage2 div#iframe_mask2').css('margin-top', top + "px");


    });


    $('#secpage3art li.licname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var mod = (y - subfact) % h; //+ 250;	
        var f = parseInt(y / h);
        var top = y; //(f * h);
        var offset = parseInt(h / 2);
        //alert( mod + '---' + offset);
        if (mod > offset)
            offset = mod; // - offset ;
        else
            offset = parseInt(h); // - (mod - offset)) ;
        top = Math.max(0, $(window).scrollTop() - subfact + (mod / 4));
        var elemheight = $("#secpage3art").height();


        if (top + cutheight > elemheight) {
            top = top - (top + cutheight - elemheight);
        }

        //alert(top);			
        $('#secpage3 iframe.foriframe').css('margin-top', top + "px");
        $('#secpage3 div#iframe_mask3').css('margin-top', top + "px");


    });


});						