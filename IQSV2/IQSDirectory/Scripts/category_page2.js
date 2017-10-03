function loadWebPreviewDefault(path)
{
	//$(document).ready(function () {
    if (document.getElementById('preview_iframe')) document.getElementById('preview_iframe').src = path + "images/cardboard-placeholder.jpg";
	if(document.getElementById('preview_iframe2'))
		document.getElementById('preview_iframe2').src = path + "images/cardboard-placeholder.jpg";
	if(document.getElementById('preview_iframe3'))
		document.getElementById('preview_iframe3').src = path + "images/cardboard-placeholder.jpg";

}
function loadWebPreview(site, thisobj)
{
    if (document.getElementById('preview_iframe')) {
        document.getElementById('preview_iframe').src = "about:blank";
        document.getElementById('preview_iframe').src = site;
        document.getElementById('preview_iframe').name = thisobj.href; 
    }
}
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

    $("#iframe_mask").click(function () {
        /*outboundTracker(); msnTracker();*/
        var framesource = document.getElementById('preview_iframe').name;
        if (!framesource.includes("cardboard-placeholder.jpg"))
            window.open(framesource, '_blank')

    });
    $('logo').html("IQS Directory");
    $('li.licname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var mod = (y - 250) % h; //+ 250;	
        var f = parseInt(y / h);
        var top = y; //(f * h);
        var offset = parseInt(h / 2);
        //alert( mod + '---' + offset);
        if (mod > offset)
            offset = mod; // - offset ;
        else
            offset = parseInt(h); // - (mod - offset)) ;
        //top = Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) + $(window).scrollTop()) ;
        //top = Math.max(0, y - ( y - $(window).scrollTop()) - (h/2) + h/4) ; 
        // (($(window).height() - $(this).outerHeight()) / 2) + 
        //top = Math.max(0, $(window).scrollTop()-mod) ; 
        // (($(window).height() - $(this).outerHeight()) / 2) + 
        top = Math.max(0, $(window).scrollTop() - 250 + (mod / 4));
        var docheight = $(document).height();
        //alert(top);
        //alert(parseInt(top+h) + '---' + docheight);

        

        if (top + 900 > docheight) {
            top = top - (top + 1000 - docheight);
        }

        //alert(top);			
        $('.foriframe').css('margin-top', top + "px");

        
        /*var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var h1 = effectiveDeviceHeight();
        var el = document.querySelector("#preview_iframe");
        var tel = document.querySelector("#secadpageart");
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
        $('.foriframe').css('margin-top', top + "px");*/

    });
});
							