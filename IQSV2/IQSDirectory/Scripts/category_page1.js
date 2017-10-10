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
    /*
    var id= 'preview_iframe'+ tier;
    if (document.getElementById(id)) { document.getElementById(id).src = "about:blank"; document.getElementById(id).src = site; document.getElementById(id).name = thisObj.href; }
    else if (document.getElementById('PlaceHolder_Preview')) { document.getElementById('PlaceHolder_Preview').src = site; document.getElementById(id).name = thisObj.href; }
    */
    var id = 'preview' + tier;
    if (document.getElementById(id)) {
        document.getElementById(id).innerHTML = "<a href='"+site+"' target='_blank' ><img src='https://image.thum.io/get/" + site + "' /></a>";
    }
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
    $('#sectier1 h3.cname a').mousemove(function (e) {
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
        var elemheight = $("#sectier1").height();


        if (top + cutheight > elemheight) {
            top = top - (top + cutheight - elemheight);
        }

        //alert(top);			
        $('#sectier1 .forpreview').css('margin-top', top + "px");
        


    });

    

    $('#sectier2 h3.cname a').mousemove(function (e) {
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
        var elemheight = $("#sectier2").height();


        if (top + cutheight > elemheight) {
            top = top - (top + cutheight - elemheight);
        }

        $('#sectier2 .forpreview').css('margin-top', top + "px");


    });


    $('#sectier3 h3.cname a').mousemove(function (e) {
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
        var elemheight = $("#sectier3").height();


        if (top + cutheight > elemheight) {
            top = top - (top + cutheight - elemheight);
        }

        //alert(top);			
        $('#sectier3 .forpreview').css('margin-top', top + "px");


    });


});						