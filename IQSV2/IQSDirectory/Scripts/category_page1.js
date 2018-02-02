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
function loadWebPreview(site, tier, customimage) {
   
   // var md5 = require('MD5');

    var secret = '!qskey2017$';
    var url = site;

    // Add 300 seconds to the current time for a 5 minute expiry
    var expires = new Date().getTime() + (1000 * 300);

    var hash = MD5(secret + expires + url);
    var auth = '925-' + expires + '-' + hash;

    var width = parseFloat($("#sectier1 aside").width());
    var imgUrl = '//image.thum.io/get/width/'+width+'/auth/' + auth + '/' + url;
    var custompath = document.getElementById('hdnSrhRootPath').value + 'preview_images//';
    //alert(imgUrl);
    //var imgUrl = '//image.thum.io/get/' + site;
    var id = 'preview' + tier;
    if (document.getElementById(id)) {
        if ((/\.(gif|jpg|jpeg|tiff|png|bmp)$/i).test(customimage) )
            document.getElementById(id).innerHTML = "<a href='" + site + "' target='_blank' ><img src='" + custompath + customimage + "' /></a>";
        else
        document.getElementById(id).innerHTML = "<a href='" + site + "' target='_blank' ><img src='" + imgUrl + "' /></a>";
    }
    //else if (document.getElementById('PlaceHolder_Preview')) { document.getElementById('PlaceHolder_Preview').src = site; document.getElementById(id).name = thisObj.href; }

    
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

    var width = parseFloat($("#sectier1 aside").width());
    var height = parseFloat($("#sectier1 ul").height());
    var newwidth = width;
    if (width > height) {
        var top = (width - height) / 2;
        //alert(width + '---' + height + '----' + top * 2);
        $('#sectier1 ul').css('padding-top', top + "px");
        /*if (top <= 25)
            $('#sectier1 ul').css('padding-top', top + "px");
        else if (top < 50) {
            newwidth = width * 90 / 100;

        }
        else if (top < 100) {
            newwidth = width * 80 / 100;

        }
        else if (top < 150) {
            newwidth = width * 75 / 100;

        }
        else if (top < 200) {
            newwidth = width * 70 / 100;

        }
        else if (top < 300) {
            newwidth = width * 60 / 100;

        }
        else if (top < 500) {
            newwidth = width * 50 / 100;

        }
        else {
            newwidth = width * 49 / 100;

        }
        if (newwidth != width) {
            var m = (parseFloat($("#sectier1").width()) - parseFloat($("#sectier1 ul").width()) - newwidth) / 2 + parseFloat($("#sectier1").css('padding-right')) / 2;
            //alert(m);
            $("#sectier1 aside").width(newwidth);
            $("#sectier1 aside").css('margin-left', m + "px");
            if (newwidth > height) {
                var top = (newwidth - height) / 2;
                $('#sectier1 ul').css('padding-top', top + "px");
            }
        }*/
    }

    var width1 = parseFloat($("#sectier2 aside").width());
    var height1 = parseFloat($("#sectier2 ul").height());
    var newwidth1 = width1;
    if (width1 > height1) {
        var top1 = (width1 - height1) / 2;
        $('#sectier2 ul').css('padding-top', top1 + "px");
        /*if (top1 <= 25) {
            $('#sectier2 ul').css('padding-top', top1 + "px");
        }
        else if (top1 < 50) {
            newwidth1 = width1 * 90 / 100;
        }
        else if (top1 < 100) {
            newwidth1 = width1 * 80 / 100;

        }
        else if (top1 < 150) {
            newwidth1 = width1 * 75 / 100;

        }
        else if (top1 < 200) {
            newwidth1 = width1 * 70 / 100;

        }
        else if (top1 < 300) {
            newwidth1 = width1 * 60 / 100;

        }
        else if (top1 < 500) {
            newwidth1 = width1 * 50 / 100;

        }
        else {
            newwidth1 = width1 * 49 / 100;

        }
        if (newwidth1 != width1) {
            var m1 = (parseFloat($("#sectier2").width()) - parseFloat($("#sectier2 ul").width()) - newwidth) / 2 + parseFloat($("#sectier2").css('padding-right')) / 2;
            //alert(m);
            $("#sectier2 aside").width(newwidth1);
            $("#sectier2 aside").css('margin-left', m1 + "px");
            if (newwidth1 > height1) {
                var top1 = (newwidth1 - height1) / 2;
                $('#sectier2 ul').css('padding-top', top1 + "px");
            }
        }*/

    }

    $('#sectier1 h3.cname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var elemheight = $("#sectier1 .adlist_ul").height();
        var elemtop = $("#sectier1 .adlist_ul").position().top;
        var sectionfull = parseFloat(elemheight) + parseFloat(elemtop);
        var h1 = parseFloat($("#sectier1 aside").width());
        var elemFull = parseFloat(y) + h1;
        var mod = (y - elemtop) % h;
        //elemFull = elemFull + widthoff;
        //alert('width=' + divW + 'height=' + divH + "imgheight=" + $('#sectier1 .forpreview').height());

        //alert(elemFull + "--" + sectionfull);
        var top1;
        if (elemFull > sectionfull) {
            top1 = y - (elemFull - sectionfull) - elemtop - (mod / 4);

        }
        else {
            top1 = y - elemtop - (h1 / 2);
        }
        top1 = Math.max(0, top1);
        $('#sectier1 .forpreview').css('margin-top', top1 + "px");


    });



    $('#sectier2 h3.cname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();
        var elemheight = $("#sectier2 .adlist_ul").height();
        var elemtop = $("#sectier2 .adlist_ul").position().top;
        var sectionfull = parseFloat(elemheight) + parseFloat(elemtop);
        var h1 = parseFloat($("#sectier2 aside").width());
        var elemFull = parseFloat(y) + h1;
        var mod = (y - elemtop) % h;

        //alert('width=' + divW + 'height=' + divH + "imgheight=" + $('#sectier1 .forpreview').height());

        //alert(elemFull + "--" + sectionfull);
        var top1;
        if (elemFull > sectionfull) {
            top1 = y - (elemFull - sectionfull) - elemtop - (mod / 4);

        }
        else {
            top1 = y - elemtop - (h1 / 2);
        }
        top1 = Math.max(0, top1);
        $('#sectier2 .forpreview').css('margin-top', top1 + "px");


    });


    $('#sectier3 h3.cname a').mousemove(function (e) {
        var y = e.pageY;
        var x = e.pageX;
        var h = $(window).height();

        var elemheight = $("#sectier3 .adlist_ul").height();
        var elemtop = $("#sectier3 .adlist_ul").position().top;
        var sectionfull = parseFloat(elemheight) + parseFloat(elemtop);
        var h1 = parseFloat($("#sectier3 aside").width());
        var elemFull = parseFloat(y) + h1;
        var mod = (y - elemtop) % h;

        //alert('width=' + divW + 'height=' + divH + "imgheight=" + $('#sectier1 .forpreview').height());

        //alert(elemFull + "--" + sectionfull);
        var top1;
        if (elemFull > sectionfull) {
            top1 = y - (elemFull - sectionfull) - elemtop - (mod / 4);

        }
        else {
            top1 = y - elemtop - (h1 / 2);
        }
        top1 = Math.max(0, top1);
        $('#sectier3 .forpreview').css('margin-top', top1 + "px");


    });


});		

//<![CDATA[ 
function hitsLinkTrack(hitslink) {

    if (hitslink != '') {

        var re = /\s*;\s*/;
        var arrHits = hitslink.split(re);
        //alert(hitslink);
        for (var i = 0; i < arrHits.length; i++) {
            var s = arrHits[i];
            //alert(s);
            if (s != '') {
                if (s.indexOf("=") != -1) {
                    var val = s.substr(s.indexOf("=") + 1);
                    //alert(val);
                    var index = s.substr(0, s.indexOf("="));
                    //alert(index);
                    if (index.indexOf("wa_account") != -1)
                        acc_name = val.replace(/(\")|(&quot;)|(\s)/g, '');
                    else if (index.indexOf("wa_location") != -1)
                        loc_name = val;
                }
            }
        }

        //alert("wa_account = " + acc_name + "wa_location =" + loc_name)
        var wa_pageName = location.pathname;
        // customize the page name here; 

        wa_account = acc_name;
        wa_location = loc_name;

        wa_MultivariateKey = '';
        // Set this variable to perform multivariate testing 

        var wa_c = new RegExp('__wa_v=([^;]+)').exec(document.cookie),
            wa_tz = new Date();
        /*if (document.referrer != '') {
            wa_rf = document.referrer;
            //alert(wa_rf);
        }
        else*/
            wa_rf = location.href;
            var wa_sr = location.search,
            wa_hp = 'http' + (location.protocol == 'https:' ? 's' : '');

        if (wa_c != null) {
            wa_c = wa_c[1]
        }
        else {
            wa_c = wa_tz.getTime();

            /*document.cookie = '__wa_v=' + wa_c + ';
            path = /;
            expires = 1 / 1 / '+(wa_tz.getUTCFullYear()+2);*/
        }
        wa_img = new Image();

        wa_img.src = wa_hp + '://counter.hitslink.com/statistics.asp?v=1&s=' + wa_location + '&eacct=' + wa_account + '&an=' +
            escape(navigator.appName) + '&sr=' + escape(wa_sr) + '&rf=' + escape(wa_rf) + '&mvk=' + escape(wa_MultivariateKey) +
            '&sl=' + escape(navigator.systemLanguage) + '&l=' + escape(navigator.language) +
            '&pf=' + escape(navigator.platform) + '&pg=' + escape(wa_pageName) + '&cd=' + screen.colorDepth + '&rs=' + escape(screen.width +
                ' x ' + screen.height) + '&je=' + navigator.javaEnabled() + '&c=' + wa_c + '&tks=' + wa_tz.getTime();
        //alert(wa_img.src);

    }
}//]]> 

var MD5 = function (string) {

    function RotateLeft(lValue, iShiftBits) {
        return (lValue << iShiftBits) | (lValue >>> (32 - iShiftBits));
    }

    function AddUnsigned(lX, lY) {
        var lX4, lY4, lX8, lY8, lResult;
        lX8 = (lX & 0x80000000);
        lY8 = (lY & 0x80000000);
        lX4 = (lX & 0x40000000);
        lY4 = (lY & 0x40000000);
        lResult = (lX & 0x3FFFFFFF) + (lY & 0x3FFFFFFF);
        if (lX4 & lY4) {
            return (lResult ^ 0x80000000 ^ lX8 ^ lY8);
        }
        if (lX4 | lY4) {
            if (lResult & 0x40000000) {
                return (lResult ^ 0xC0000000 ^ lX8 ^ lY8);
            } else {
                return (lResult ^ 0x40000000 ^ lX8 ^ lY8);
            }
        } else {
            return (lResult ^ lX8 ^ lY8);
        }
    }

    function F(x, y, z) { return (x & y) | ((~x) & z); }
    function G(x, y, z) { return (x & z) | (y & (~z)); }
    function H(x, y, z) { return (x ^ y ^ z); }
    function I(x, y, z) { return (y ^ (x | (~z))); }

    function FF(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(F(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    };

    function GG(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(G(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    };

    function HH(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(H(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    };

    function II(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(I(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    };

    function ConvertToWordArray(string) {
        var lWordCount;
        var lMessageLength = string.length;
        var lNumberOfWords_temp1 = lMessageLength + 8;
        var lNumberOfWords_temp2 = (lNumberOfWords_temp1 - (lNumberOfWords_temp1 % 64)) / 64;
        var lNumberOfWords = (lNumberOfWords_temp2 + 1) * 16;
        var lWordArray = Array(lNumberOfWords - 1);
        var lBytePosition = 0;
        var lByteCount = 0;
        while (lByteCount < lMessageLength) {
            lWordCount = (lByteCount - (lByteCount % 4)) / 4;
            lBytePosition = (lByteCount % 4) * 8;
            lWordArray[lWordCount] = (lWordArray[lWordCount] | (string.charCodeAt(lByteCount) << lBytePosition));
            lByteCount++;
        }
        lWordCount = (lByteCount - (lByteCount % 4)) / 4;
        lBytePosition = (lByteCount % 4) * 8;
        lWordArray[lWordCount] = lWordArray[lWordCount] | (0x80 << lBytePosition);
        lWordArray[lNumberOfWords - 2] = lMessageLength << 3;
        lWordArray[lNumberOfWords - 1] = lMessageLength >>> 29;
        return lWordArray;
    };

    function WordToHex(lValue) {
        var WordToHexValue = "", WordToHexValue_temp = "", lByte, lCount;
        for (lCount = 0; lCount <= 3; lCount++) {
            lByte = (lValue >>> (lCount * 8)) & 255;
            WordToHexValue_temp = "0" + lByte.toString(16);
            WordToHexValue = WordToHexValue + WordToHexValue_temp.substr(WordToHexValue_temp.length - 2, 2);
        }
        return WordToHexValue;
    };

    function Utf8Encode(string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    };

    var x = Array();
    var k, AA, BB, CC, DD, a, b, c, d;
    var S11 = 7, S12 = 12, S13 = 17, S14 = 22;
    var S21 = 5, S22 = 9, S23 = 14, S24 = 20;
    var S31 = 4, S32 = 11, S33 = 16, S34 = 23;
    var S41 = 6, S42 = 10, S43 = 15, S44 = 21;

    string = Utf8Encode(string);

    x = ConvertToWordArray(string);

    a = 0x67452301; b = 0xEFCDAB89; c = 0x98BADCFE; d = 0x10325476;

    for (k = 0; k < x.length; k += 16) {
        AA = a; BB = b; CC = c; DD = d;
        a = FF(a, b, c, d, x[k + 0], S11, 0xD76AA478);
        d = FF(d, a, b, c, x[k + 1], S12, 0xE8C7B756);
        c = FF(c, d, a, b, x[k + 2], S13, 0x242070DB);
        b = FF(b, c, d, a, x[k + 3], S14, 0xC1BDCEEE);
        a = FF(a, b, c, d, x[k + 4], S11, 0xF57C0FAF);
        d = FF(d, a, b, c, x[k + 5], S12, 0x4787C62A);
        c = FF(c, d, a, b, x[k + 6], S13, 0xA8304613);
        b = FF(b, c, d, a, x[k + 7], S14, 0xFD469501);
        a = FF(a, b, c, d, x[k + 8], S11, 0x698098D8);
        d = FF(d, a, b, c, x[k + 9], S12, 0x8B44F7AF);
        c = FF(c, d, a, b, x[k + 10], S13, 0xFFFF5BB1);
        b = FF(b, c, d, a, x[k + 11], S14, 0x895CD7BE);
        a = FF(a, b, c, d, x[k + 12], S11, 0x6B901122);
        d = FF(d, a, b, c, x[k + 13], S12, 0xFD987193);
        c = FF(c, d, a, b, x[k + 14], S13, 0xA679438E);
        b = FF(b, c, d, a, x[k + 15], S14, 0x49B40821);
        a = GG(a, b, c, d, x[k + 1], S21, 0xF61E2562);
        d = GG(d, a, b, c, x[k + 6], S22, 0xC040B340);
        c = GG(c, d, a, b, x[k + 11], S23, 0x265E5A51);
        b = GG(b, c, d, a, x[k + 0], S24, 0xE9B6C7AA);
        a = GG(a, b, c, d, x[k + 5], S21, 0xD62F105D);
        d = GG(d, a, b, c, x[k + 10], S22, 0x2441453);
        c = GG(c, d, a, b, x[k + 15], S23, 0xD8A1E681);
        b = GG(b, c, d, a, x[k + 4], S24, 0xE7D3FBC8);
        a = GG(a, b, c, d, x[k + 9], S21, 0x21E1CDE6);
        d = GG(d, a, b, c, x[k + 14], S22, 0xC33707D6);
        c = GG(c, d, a, b, x[k + 3], S23, 0xF4D50D87);
        b = GG(b, c, d, a, x[k + 8], S24, 0x455A14ED);
        a = GG(a, b, c, d, x[k + 13], S21, 0xA9E3E905);
        d = GG(d, a, b, c, x[k + 2], S22, 0xFCEFA3F8);
        c = GG(c, d, a, b, x[k + 7], S23, 0x676F02D9);
        b = GG(b, c, d, a, x[k + 12], S24, 0x8D2A4C8A);
        a = HH(a, b, c, d, x[k + 5], S31, 0xFFFA3942);
        d = HH(d, a, b, c, x[k + 8], S32, 0x8771F681);
        c = HH(c, d, a, b, x[k + 11], S33, 0x6D9D6122);
        b = HH(b, c, d, a, x[k + 14], S34, 0xFDE5380C);
        a = HH(a, b, c, d, x[k + 1], S31, 0xA4BEEA44);
        d = HH(d, a, b, c, x[k + 4], S32, 0x4BDECFA9);
        c = HH(c, d, a, b, x[k + 7], S33, 0xF6BB4B60);
        b = HH(b, c, d, a, x[k + 10], S34, 0xBEBFBC70);
        a = HH(a, b, c, d, x[k + 13], S31, 0x289B7EC6);
        d = HH(d, a, b, c, x[k + 0], S32, 0xEAA127FA);
        c = HH(c, d, a, b, x[k + 3], S33, 0xD4EF3085);
        b = HH(b, c, d, a, x[k + 6], S34, 0x4881D05);
        a = HH(a, b, c, d, x[k + 9], S31, 0xD9D4D039);
        d = HH(d, a, b, c, x[k + 12], S32, 0xE6DB99E5);
        c = HH(c, d, a, b, x[k + 15], S33, 0x1FA27CF8);
        b = HH(b, c, d, a, x[k + 2], S34, 0xC4AC5665);
        a = II(a, b, c, d, x[k + 0], S41, 0xF4292244);
        d = II(d, a, b, c, x[k + 7], S42, 0x432AFF97);
        c = II(c, d, a, b, x[k + 14], S43, 0xAB9423A7);
        b = II(b, c, d, a, x[k + 5], S44, 0xFC93A039);
        a = II(a, b, c, d, x[k + 12], S41, 0x655B59C3);
        d = II(d, a, b, c, x[k + 3], S42, 0x8F0CCC92);
        c = II(c, d, a, b, x[k + 10], S43, 0xFFEFF47D);
        b = II(b, c, d, a, x[k + 1], S44, 0x85845DD1);
        a = II(a, b, c, d, x[k + 8], S41, 0x6FA87E4F);
        d = II(d, a, b, c, x[k + 15], S42, 0xFE2CE6E0);
        c = II(c, d, a, b, x[k + 6], S43, 0xA3014314);
        b = II(b, c, d, a, x[k + 13], S44, 0x4E0811A1);
        a = II(a, b, c, d, x[k + 4], S41, 0xF7537E82);
        d = II(d, a, b, c, x[k + 11], S42, 0xBD3AF235);
        c = II(c, d, a, b, x[k + 2], S43, 0x2AD7D2BB);
        b = II(b, c, d, a, x[k + 9], S44, 0xEB86D391);
        a = AddUnsigned(a, AA);
        b = AddUnsigned(b, BB);
        c = AddUnsigned(c, CC);
        d = AddUnsigned(d, DD);
    }

    var temp = WordToHex(a) + WordToHex(b) + WordToHex(c) + WordToHex(d);

    return temp.toLowerCase();
}