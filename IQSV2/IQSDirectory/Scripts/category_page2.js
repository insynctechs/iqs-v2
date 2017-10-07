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
    /*if (document.getElementById('preview_iframe')) {
        document.getElementById('preview_iframe').src = "about:blank";
        document.getElementById('preview_iframe').src = site;
        document.getElementById('preview_iframe').name = thisobj.href; 
    }*/
    var id = 'preview1' ;
    if (document.getElementById(id)) {
        document.getElementById(id).innerHTML = "<a href='" + site + "' target='_blank' ><img src='https://image.thum.io/get/" + site + "' /></a>";
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
    $('h3.cname a').mousemove(function (e) {
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
       
        top = Math.max(0, $(window).scrollTop() - 250 + (mod / 4));
        var docheight = $(document).height();
        

        if (top + 900 > docheight) {
            top = top - (top + 1000 - docheight);
        }

        			
        $('.forpreview').css('margin-top', top + "px");

        
        

    });
});
							