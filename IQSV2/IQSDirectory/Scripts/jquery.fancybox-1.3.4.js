/*(function(a){var n,s,t,e,z,l,A,i,w,x,q=0,d={},o=[],p=0,b={},j=[],C=null,m=new Image,F=/\.(jpg|gif|png|bmp|jpeg)(.*)?$/i,Q=/[^\.]\.(swf)\s*$/i,G,H=1,v=0,u="",r,g,k=!1,y=a.extend(a("<div/>")[0],{prop:0}),I=a.browser.msie&&7>a.browser.version&&!window.XMLHttpRequest,J=function(){s.hide();m.onerror=m.onload=null;C&&C.abort();n.empty()},K=function(){!1===d.onError(o,q,d)?(s.hide(),k=!1):(d.titleShow=!1,d.width="auto",d.height="auto",n.html('<p id="fancybox-error">The requested content cannot be loaded.<br />Please try again later.</p>'),
B())},E=function(){var c=o[q],b,f,e,g,j,i;J();d=a.extend({},a.fn.fancybox.defaults,"undefined"==typeof a(c).data("fancybox")?d:a(c).data("fancybox"));i=d.onStart(o,q,d);if(!1===i)k=!1;else{"object"==typeof i&&(d=a.extend(d,i));e=d.title||(c.nodeName?a(c).attr("title"):c.title)||"";c.nodeName&&!d.orig&&(d.orig=a(c).children("img:first").length?a(c).children("img:first"):a(c));""===e&&(d.orig&&d.titleFromAlt)&&(e=d.orig.attr("alt"));b=d.href||(c.nodeName?a(c).attr("href"):c.href)||null;if(/^(?:javascript)/i.test(b)||
"#"==b)b=null;d.type?(f=d.type,b||(b=d.content)):d.content?f="html":b&&(f=b.match(F)?"image":b.match(Q)?"swf":a(c).hasClass("iframe")?"iframe":0===b.indexOf("#")?"inline":"ajax");if(f)switch("inline"==f&&(c=b.substr(b.indexOf("#")),f=0<a(c).length?"inline":"ajax"),d.type=f,d.href=b,d.title=e,d.autoDimensions&&("html"==d.type||"inline"==d.type||"ajax"==d.type?(d.width="auto",d.height="auto"):d.autoDimensions=!1),d.modal&&(d.overlayShow=!0,d.hideOnOverlayClick=!1,d.hideOnContentClick=!1,d.enableEscapeButton=
!1),d.padding=parseInt(d.padding,10),d.margin=parseInt(d.margin,10),n.css("padding",d.padding+d.margin),a(".fancybox-inline-tmp").unbind("fancybox-cancel").bind("fancybox-change",function(){a(this).replaceWith(l.children())}),f){case "html":n.html(d.content);B();break;case "inline":if(!0===a(c).parent().is("#fancybox-content")){k=!1;break}a('<div class="fancybox-inline-tmp" />').hide().insertBefore(a(c)).bind("fancybox-cleanup",function(){a(this).replaceWith(l.children())}).bind("fancybox-cancel",
function(){a(this).replaceWith(n.children())});a(c).appendTo(n);B();break;case "image":k=!1;a.fancybox.showActivity();m=new Image;m.onerror=function(){K()};m.onload=function(){k=true;m.onerror=m.onload=null;d.width=m.width;d.height=m.height;a("<img />").attr({id:"fancybox-img",src:m.src,alt:d.title}).appendTo(n);L()};m.src=b;break;case "swf":d.scrolling="no";g='<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="'+d.width+'" height="'+d.height+'"><param name="movie" value="'+b+'"></param>';
j="";a.each(d.swf,function(a,c){g=g+('<param name="'+a+'" value="'+c+'"></param>');j=j+(" "+a+'="'+c+'"')});g+='<embed src="'+b+'" type="application/x-shockwave-flash" width="'+d.width+'" height="'+d.height+'"'+j+"></embed></object>";n.html(g);B();break;case "ajax":k=!1;a.fancybox.showActivity();d.ajax.win=d.ajax.success;C=a.ajax(a.extend({},d.ajax,{url:b,data:d.ajax.data||{},error:function(a){a.status>0&&K()},success:function(a,c,f){if((typeof f=="object"?f:C).status==200){if(typeof d.ajax.win==
"function"){i=d.ajax.win(b,a,c,f);if(i===false){s.hide();return}if(typeof i=="string"||typeof i=="object")a=i}n.html(a);B()}}}));break;case "iframe":L()}else K()}},B=function(){var c=d.width,b=d.height,c=-1<c.toString().indexOf("%")?parseInt((a(window).width()-2*d.margin)*parseFloat(c)/100,10)+"px":"auto"==c?"auto":c+"px",b=-1<b.toString().indexOf("%")?parseInt((a(window).height()-2*d.margin)*parseFloat(b)/100,10)+"px":"auto"==b?"auto":b+"px";n.wrapInner('<div style="width:'+c+";height:"+b+";overflow: "+
("auto"==d.scrolling?"auto":"yes"==d.scrolling?"scroll":"hidden")+';position:relative;"></div>');d.width=n.width();d.height=n.height();L()},L=function(){var c,h;s.hide();if(e.is(":visible")&&!1===b.onCleanup(j,p,b))a.event.trigger("fancybox-cancel"),k=!1;else{k=!0;a(l.add(t)).unbind();a(window).unbind("resize.fb scroll.fb");a(document).unbind("keydown.fb");e.is(":visible")&&"outside"!==b.titlePosition&&e.css("height",e.height());j=o;p=q;b=d;if(b.overlayShow){if(t.css({"background-color":b.overlayColor,
opacity:b.overlayOpacity,cursor:b.hideOnOverlayClick?"pointer":"auto",height:a(document).height()}),!t.is(":visible")){if(I)a("select:not(#fancybox-tmp select)").filter(function(){return"hidden"!==this.style.visibility}).css({visibility:"hidden"}).one("fancybox-cleanup",function(){this.style.visibility="inherit"});t.show()}}else t.hide();c=M();var f={},D=b.autoScale,m=2*b.padding;f.width=-1<b.width.toString().indexOf("%")?parseInt(c[0]*parseFloat(b.width)/100,10):b.width+m;f.height=-1<b.height.toString().indexOf("%")?
parseInt(c[1]*parseFloat(b.height)/100,10):b.height+m;if(D&&(f.width>c[0]||f.height>c[1]))if("image"==d.type||"swf"==d.type){if(D=b.width/b.height,f.width>c[0]&&(f.width=c[0],f.height=parseInt((f.width-m)/D+m,10)),f.height>c[1])f.height=c[1],f.width=parseInt((f.height-m)*D+m,10)}else f.width=Math.min(f.width,c[0]),f.height=Math.min(f.height,c[1]);f.top=parseInt(Math.max(c[3]-20,c[3]+0.5*(c[1]-f.height-40)),10);f.left=parseInt(Math.max(c[2]-20,c[2]+0.5*(c[0]-f.width-40)),10);g=f;u=b.title||"";v=0;
i.empty().removeAttr("style").removeClass();if(!1!==b.titleShow&&(u=a.isFunction(b.titleFormat)?b.titleFormat(u,j,p,b):u&&u.length?"float"==b.titlePosition?'<table id="fancybox-title-float-wrap" cellpadding="0" cellspacing="0"><tr><td id="fancybox-title-float-left"></td><td id="fancybox-title-float-main">'+u+'</td><td id="fancybox-title-float-right"></td></tr></table>':'<div id="fancybox-title-'+b.titlePosition+'">'+u+"</div>":!1)&&""!==u)switch(i.addClass("fancybox-title-"+b.titlePosition).html(u).appendTo("body").show(),
b.titlePosition){case "inside":i.css({width:g.width-2*b.padding,marginLeft:b.padding,marginRight:b.padding});v=i.outerHeight(!0);i.appendTo(z);g.height+=v;break;case "over":i.css({marginLeft:b.padding,width:g.width-2*b.padding,bottom:b.padding}).appendTo(z);break;case "float":i.css("left",-1*parseInt((i.width()-g.width-40)/2,10)).appendTo(e);break;default:i.css({width:g.width-2*b.padding,paddingLeft:b.padding,paddingRight:b.padding}).appendTo(e)}i.hide();e.is(":visible")?(a(A.add(w).add(x)).hide(),
c=e.position(),r={top:c.top,left:c.left,width:e.width(),height:e.height()},h=r.width==g.width&&r.height==g.height,l.fadeTo(b.changeFade,0.3,function(){var c=function(){l.html(n.contents()).fadeTo(b.changeFade,1,N)};a.event.trigger("fancybox-change");l.empty().removeAttr("filter").css({"border-width":b.padding,width:g.width-2*b.padding,height:d.autoDimensions?"auto":g.height-v-2*b.padding});h?c():(y.prop=0,a(y).animate({prop:1},{duration:b.changeSpeed,easing:b.easingChange,step:O,complete:c}))})):
(e.removeAttr("style"),l.css("border-width",b.padding),"elastic"==b.transitionIn?(r=P(),l.html(n.contents()),e.show(),b.opacity&&(g.opacity=0),y.prop=0,a(y).animate({prop:1},{duration:b.speedIn,easing:b.easingIn,step:O,complete:N})):("inside"==b.titlePosition&&0<v&&i.show(),l.css({width:g.width-2*b.padding,height:d.autoDimensions?"auto":g.height-v-2*b.padding}).html(n.contents()),e.css(g).fadeIn("none"==b.transitionIn?0:b.speedIn,N)))}},N=function(){a.support.opacity||(l.get(0).style.removeAttribute("filter"),
e.get(0).style.removeAttribute("filter"));d.autoDimensions&&l.css("height","auto");e.css("height","auto");u&&u.length&&i.show();b.showCloseButton&&A.show();(b.enableEscapeButton||b.enableKeyboardNav)&&a(document).bind("keydown.fb",function(c){if(c.keyCode==27&&b.enableEscapeButton){c.preventDefault();a.fancybox.close()}else if((c.keyCode==37||c.keyCode==39)&&b.enableKeyboardNav&&c.target.tagName!=="INPUT"&&c.target.tagName!=="TEXTAREA"&&c.target.tagName!=="SELECT"){c.preventDefault();a.fancybox[c.keyCode==
37?"prev":"next"]()}});b.showNavArrows?((b.cyclic&&1<j.length||0!==p)&&w.show(),(b.cyclic&&1<j.length||p!=j.length-1)&&x.show()):(w.hide(),x.hide());b.hideOnContentClick&&l.bind("click",a.fancybox.close);b.hideOnOverlayClick&&t.bind("click",a.fancybox.close);a(window).bind("resize.fb",a.fancybox.resize);b.centerOnScroll&&a(window).bind("scroll.fb",a.fancybox.center);"iframe"==b.type&&a('<iframe id="fancybox-frame" name="fancybox-frame'+(new Date).getTime()+'" frameborder="0" hspace="0" '+(a.browser.msie?
'allowtransparency="true""':"")+' scrolling="'+d.scrolling+'" src="'+b.href+'"></iframe>').appendTo(l);e.show();k=!1;a.fancybox.center();b.onComplete(j,p,b);var c,h;j.length-1>p&&(c=j[p+1].href,"undefined"!==typeof c&&c.match(F)&&(h=new Image,h.src=c));0<p&&(c=j[p-1].href,"undefined"!==typeof c&&c.match(F)&&(h=new Image,h.src=c))},O=function(a){var d={width:parseInt(r.width+(g.width-r.width)*a,10),height:parseInt(r.height+(g.height-r.height)*a,10),top:parseInt(r.top+(g.top-r.top)*a,10),left:parseInt(r.left+
(g.left-r.left)*a,10)};"undefined"!==typeof g.opacity&&(d.opacity=0.5>a?0.5:a);e.css(d);l.css({width:d.width-2*b.padding,height:d.height-v*a-2*b.padding})},M=function(){return[a(window).width()-2*b.margin,a(window).height()-2*b.margin,a(document).scrollLeft()+b.margin,a(document).scrollTop()+b.margin]},P=function(){var c=d.orig?a(d.orig):!1,h={};c&&c.length?(h=c.offset(),h.top+=parseInt(c.css("paddingTop"),10)||0,h.left+=parseInt(c.css("paddingLeft"),10)||0,h.top+=parseInt(c.css("border-top-width"),
10)||0,h.left+=parseInt(c.css("border-left-width"),10)||0,h.width=c.width(),h.height=c.height(),h={width:h.width+2*b.padding,height:h.height+2*b.padding,top:h.top-b.padding-20,left:h.left-b.padding-20}):(c=M(),h={width:2*b.padding,height:2*b.padding,top:parseInt(c[3]+0.5*c[1],10),left:parseInt(c[2]+0.5*c[0],10)});return h},R=function(){s.is(":visible")?(a("div",s).css("top",-40*H+"px"),H=(H+1)%12):clearInterval(G)};a.fn.fancybox=function(c){if(!a(this).length)return this;a(this).data("fancybox",a.extend({},
c,a.metadata?a(this).metadata():{})).unbind("click.fb").bind("click.fb",function(c){c.preventDefault();k||(k=!0,a(this).blur(),o=[],q=0,c=a(this).attr("rel")||"",!c||""==c||"nofollow"===c?o.push(this):(o=a("a[rel="+c+"], area[rel="+c+"]"),q=o.index(this)),E())});return this};a.fancybox=function(c,b){var d;if(!k){k=!0;d="undefined"!==typeof b?b:{};o=[];q=parseInt(d.index,10)||0;if(a.isArray(c)){for(var e=0,g=c.length;e<g;e++)"object"==typeof c[e]?a(c[e]).data("fancybox",a.extend({},d,c[e])):c[e]=a({}).data("fancybox",
a.extend({content:c[e]},d));o=jQuery.merge(o,c)}else"object"==typeof c?a(c).data("fancybox",a.extend({},d,c)):c=a({}).data("fancybox",a.extend({content:c},d)),o.push(c);if(q>o.length||0>q)q=0;E()}};a.fancybox.showActivity=function(){clearInterval(G);s.show();G=setInterval(R,66)};a.fancybox.hideActivity=function(){s.hide()};a.fancybox.next=function(){return a.fancybox.pos(p+1)};a.fancybox.prev=function(){return a.fancybox.pos(p-1)};a.fancybox.pos=function(a){k||(a=parseInt(a),o=j,-1<a&&a<j.length?
(q=a,E()):b.cyclic&&1<j.length&&(q=a>=j.length?0:j.length-1,E()))};a.fancybox.cancel=function(){k||(k=!0,a.event.trigger("fancybox-cancel"),J(),d.onCancel(o,q,d),k=!1)};a.fancybox.close=function(){function c(){t.fadeOut("fast");i.empty().hide();e.hide();a.event.trigger("fancybox-cleanup");l.empty();b.onClosed(j,p,b);j=d=[];p=q=0;b=d={};k=!1}if(!k&&!e.is(":hidden"))if(k=!0,b&&!1===b.onCleanup(j,p,b))k=!1;else if(J(),a(A.add(w).add(x)).hide(),a(l.add(t)).unbind(),a(window).unbind("resize.fb scroll.fb"),
a(document).unbind("keydown.fb"),l.find("iframe").attr("src",I&&/^https/i.test(window.location.href||"")?"javascript:void(false)":"about:blank"),"inside"!==b.titlePosition&&i.empty(),e.stop(),"elastic"==b.transitionOut){r=P();var h=e.position();g={top:h.top,left:h.left,width:e.width(),height:e.height()};b.opacity&&(g.opacity=1);i.empty().hide();y.prop=1;a(y).animate({prop:0},{duration:b.speedOut,easing:b.easingOut,step:O,complete:c})}else e.fadeOut("none"==b.transitionOut?0:b.speedOut,c)};a.fancybox.resize=
function(){t.is(":visible")&&t.css("height",a(document).height());a.fancybox.center(!0)};a.fancybox.center=function(a){var d,f;if(!k&&(f=!0===a?1:0,d=M(),f||!(e.width()>d[0]||e.height()>d[1])))e.stop().animate({top:parseInt(Math.max(d[3]-20,d[3]+0.5*(d[1]-l.height()-40)-b.padding)),left:parseInt(Math.max(d[2]-20,d[2]+0.5*(d[0]-l.width()-40)-b.padding))},"number"==typeof a?a:200)};a.fancybox.init=function(){a("#fancybox-wrap").length||(a("body").append(n=a('<div id="fancybox-tmp"></div>'),s=a('<div id="fancybox-loading"><div></div></div>'),
t=a('<div id="fancybox-overlay"></div>'),e=a('<div id="fancybox-wrap"></div>')),z=a('<div id="fancybox-outer"></div>').append('<div class="fancybox-bg" id="fancybox-bg-n"></div><div class="fancybox-bg" id="fancybox-bg-ne"></div><div class="fancybox-bg" id="fancybox-bg-e"></div><div class="fancybox-bg" id="fancybox-bg-se"></div><div class="fancybox-bg" id="fancybox-bg-s"></div><div class="fancybox-bg" id="fancybox-bg-sw"></div><div class="fancybox-bg" id="fancybox-bg-w"></div><div class="fancybox-bg" id="fancybox-bg-nw"></div>').appendTo(e),
z.append(l=a('<div id="fancybox-content"></div>'),A=a('<a id="fancybox-close"></a>'),i=a('<div id="fancybox-title"></div>'),w=a('<a href="javascript:;" id="fancybox-left"><span class="fancy-ico" id="fancybox-left-ico"></span></a>'),x=a('<a href="javascript:;" id="fancybox-right"><span class="fancy-ico" id="fancybox-right-ico"></span></a>')),A.click(a.fancybox.close),s.click(a.fancybox.cancel),w.click(function(c){c.preventDefault();a.fancybox.prev()}),x.click(function(c){c.preventDefault();a.fancybox.next()}),
a.fn.mousewheel&&e.bind("mousewheel.fb",function(c,b){if(k)c.preventDefault();else if(0==a(c.target).get(0).clientHeight||a(c.target).get(0).scrollHeight===a(c.target).get(0).clientHeight)c.preventDefault(),a.fancybox[0<b?"prev":"next"]()}),a.support.opacity||e.addClass("fancybox-ie"),I&&(s.addClass("fancybox-ie6"),e.addClass("fancybox-ie6"),a('<iframe id="fancybox-hide-sel-frame" src="'+(/^https/i.test(window.location.href||"")?"javascript:void(false)":"about:blank")+'" scrolling="no" border="0" frameborder="0" tabindex="-1"></iframe>').prependTo(z)))};
a.fn.fancybox.defaults={padding:10,margin:40,opacity:!1,modal:!1,cyclic:!1,scrolling:"auto",width:560,height:340,autoScale:!0,autoDimensions:!0,centerOnScroll:!0,ajax:{},swf:{wmode:"transparent"},hideOnOverlayClick:!0,hideOnContentClick:!1,overlayShow:!0,overlayOpacity:0.7,overlayColor:"#777",titleShow:!0,titlePosition:"float",titleFormat:null,titleFromAlt:!1,transitionIn:"fade",transitionOut:"fade",speedIn:300,speedOut:300,changeSpeed:300,changeFade:"fast",easingIn:"swing",easingOut:"swing",showCloseButton:!0,
showNavArrows:!0,enableEscapeButton:!0,enableKeyboardNav:!0,onStart:function(){},onCancel:function(){},onComplete:function(){},onCleanup:function(){},onClosed:function(){},onError:function(){}};a(document).ready(function(){a.fancybox.init()})})(jQuery);
*/
(function (a) {
    var n, s, t, e, z, l, A, i, w, x, q = 0,
        d = {},
        o = [],
        p = 0,
        b = {},
        j = [],
        C = null,
        m = new Image,
        F = /\.(jpg|gif|png|bmp|jpeg)(.*)?$/i,
        Q = /[^\.]\.(swf)\s*$/i,
        G, H = 1,
        v = 0,
        u = "",
        r, g, k = !1,
        y = a.extend(a("<div/>")[0], {
            prop: 0
        }),
        I = a.browser.msie && 7 > a.browser.version && !window.XMLHttpRequest,
        J = function () {
            s.hide();
            m.onerror = m.onload = null;
            C && C.abort();
            n.empty()
        },
        K = function () {
            !1 === d.onError(o, q, d) ? (s.hide(), k = !1) : (d.titleShow = !1, d.width = "auto", d.height = "auto", n.html('<p id="fancybox-error">The requested content cannot be loaded.<br />Please try again later.</p>'),
                B())
        },
        E = function () {
            var c = o[q],
                b, f, e, g, j, i;
            J();
            d = a.extend({}, a.fn.fancybox.defaults, "undefined" == typeof a(c).data("fancybox") ? d : a(c).data("fancybox"));
            i = d.onStart(o, q, d);
            if (!1 === i) k = !1;
            else {
                "object" == typeof i && (d = a.extend(d, i));
                e = d.title || (c.nodeName ? a(c).attr("title") : c.title) || "";
                c.nodeName && !d.orig && (d.orig = a(c).children("img:first").length ? a(c).children("img:first") : a(c));
                "" === e && (d.orig && d.titleFromAlt) && (e = d.orig.attr("alt"));
                b = d.href || (c.nodeName ? a(c).attr("href") : c.href) || null;
                if (/^(?:javascript)/i.test(b) ||
                    "#" == b) b = null;
                d.type ? (f = d.type, b || (b = d.content)) : d.content ? f = "html" : b && (f = b.match(F) ? "image" : b.match(Q) ? "swf" : a(c).hasClass("iframe") ? "iframe" : 0 === b.indexOf("#") ? "inline" : "ajax");
                if (f) switch ("inline" == f && (c = b.substr(b.indexOf("#")), f = 0 < a(c).length ? "inline" : "ajax"), d.type = f, d.href = b, d.title = e, d.autoDimensions && ("html" == d.type || "inline" == d.type || "ajax" == d.type ? (d.width = "auto", d.height = "auto") : d.autoDimensions = !1), d.modal && (d.overlayShow = !0, d.hideOnOverlayClick = !1, d.hideOnContentClick = !1, d.enableEscapeButton = !1), d.padding = parseInt(d.padding, 10), d.margin = parseInt(d.margin, 10), n.css("padding", d.padding + d.margin), a(".fancybox-inline-tmp").unbind("fancybox-cancel").bind("fancybox-change", function () {
                    a(this).replaceWith(l.children())
                }), f) {
                        case "html":
                            n.html(d.content);
                            B();
                            break;
                        case "inline":
                            if (!0 === a(c).parent().is("#fancybox-content")) {
                                k = !1;
                                break
                            }
                            a('<div class="fancybox-inline-tmp" />').hide().insertBefore(a(c)).bind("fancybox-cleanup", function () {
                                a(this).replaceWith(l.children())
                            }).bind("fancybox-cancel",
                                function () {
                                    a(this).replaceWith(n.children())
                                });
                            a(c).appendTo(n);
                            B();
                            break;
                        case "image":
                            k = !1;
                            a.fancybox.showActivity();
                            m = new Image;
                            m.onerror = function () {
                                K()
                            };
                            m.onload = function () {
                                k = true;
                                m.onerror = m.onload = null;
                                d.width = m.width;
                                d.height = m.height;
                                a("<img />").attr({
                                    id: "fancybox-img",
                                    src: m.src,
                                    alt: d.title
                                }).appendTo(n);
                                L()
                            };
                            m.src = b;
                            break;
                        case "swf":
                            d.scrolling = "no";
                            g = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="' + d.width + '" height="' + d.height + '"><param name="movie" value="' + b + '"></param>';
                            j = "";
                            a.each(d.swf, function (a, c) {
                                g = g + ('<param name="' + a + '" value="' + c + '"></param>');
                                j = j + (" " + a + '="' + c + '"')
                            });
                            g += '<embed src="' + b + '" type="application/x-shockwave-flash" width="' + d.width + '" height="' + d.height + '"' + j + "></embed></object>";
                            n.html(g);
                            B();
                            break;
                        case "ajax":
                            k = !1;
                            a.fancybox.showActivity();
                            d.ajax.win = d.ajax.success;
                            C = a.ajax(a.extend({}, d.ajax, {
                                url: b,
                                data: d.ajax.data || {},
                                error: function (a) {
                                    a.status > 0 && K()
                                },
                                success: function (a, c, f) {
                                    if ((typeof f == "object" ? f : C).status == 200) {
                                        if (typeof d.ajax.win ==
                                            "function") {
                                            i = d.ajax.win(b, a, c, f);
                                            if (i === false) {
                                                s.hide();
                                                return
                                            }
                                            if (typeof i == "string" || typeof i == "object") a = i
                                        }
                                        n.html(a);
                                        B()
                                    }
                                }
                            }));
                            break;
                        case "iframe":
                            L()
                    } else K()
            }
        },
        B = function () {
            var c = d.width,
                b = d.height,
                c = -1 < c.toString().indexOf("%") ? parseInt((a(window).width() - 2 * d.margin) * parseFloat(c) / 100, 10) + "px" : "auto" == c ? "auto" : c + "px",
                b = -1 < b.toString().indexOf("%") ? parseInt((a(window).height() - 2 * d.margin) * parseFloat(b) / 100, 10) + "px" : "auto" == b ? "auto" : b + "px";
            n.wrapInner('<div style="width:' + c + ";height:" + b + ";overflow: " +
                ("auto" == d.scrolling ? "auto" : "yes" == d.scrolling ? "scroll" : "hidden") + ';position:relative;"></div>');
            d.width = n.width();
            d.height = n.height();
            L()
        },
        L = function () {
            var c, h;
            s.hide();
            if (e.is(":visible") && !1 === b.onCleanup(j, p, b)) a.event.trigger("fancybox-cancel"), k = !1;
            else {
                k = !0;
                a(l.add(t)).unbind();
                a(window).unbind("resize.fb scroll.fb");
                a(document).unbind("keydown.fb");
                e.is(":visible") && "outside" !== b.titlePosition && e.css("height", e.height());
                j = o;
                p = q;
                b = d;
                if (b.overlayShow) {
                    if (t.css({
                        "background-color": b.overlayColor,
                        opacity: b.overlayOpacity,
                        cursor: b.hideOnOverlayClick ? "pointer" : "auto",
                        height: a(document).height()
                    }), !t.is(":visible")) {
                        if (I) a("select:not(#fancybox-tmp select)").filter(function () {
                            return "hidden" !== this.style.visibility
                        }).css({
                            visibility: "hidden"
                        }).one("fancybox-cleanup", function () {
                            this.style.visibility = "inherit"
                        });
                        t.show()
                    }
                } else t.hide();
                c = M();
                var f = {},
                    D = b.autoScale,
                    m = 2 * b.padding;
                f.width = -1 < b.width.toString().indexOf("%") ? parseInt(c[0] * parseFloat(b.width) / 100, 10) : b.width + m;
                f.height = -1 < b.height.toString().indexOf("%") ?
                    parseInt(c[1] * parseFloat(b.height) / 100, 10) : b.height + m;
                if (D && (f.width > c[0] || f.height > c[1]))
                    if ("image" == d.type || "swf" == d.type) {
                        if (D = b.width / b.height, f.width > c[0] && (f.width = c[0], f.height = parseInt((f.width - m) / D + m, 10)), f.height > c[1]) f.height = c[1], f.width = parseInt((f.height - m) * D + m, 10)
                    } else f.width = Math.min(f.width, c[0]), f.height = Math.min(f.height, c[1]);
                f.top = parseInt(Math.max(c[3] - 20, c[3] + 0.5 * (c[1] - f.height - 40)), 10);
                f.left = parseInt(Math.max(c[2] - 20, c[2] + 0.5 * (c[0] - f.width - 40)), 10);
                g = f;
                u = b.title || "";
                v = 0;
                i.empty().removeAttr("style").removeClass();
                if (!1 !== b.titleShow && (u = a.isFunction(b.titleFormat) ? b.titleFormat(u, j, p, b) : u && u.length ? "float" == b.titlePosition ? '<table id="fancybox-title-float-wrap" cellpadding="0" cellspacing="0"><tr><td id="fancybox-title-float-left"></td><td id="fancybox-title-float-main">' + u + '</td><td id="fancybox-title-float-right"></td></tr></table>' : '<div id="fancybox-title-' + b.titlePosition + '">' + u + "</div>" : !1) && "" !== u) switch (i.addClass("fancybox-title-" + b.titlePosition).html(u).appendTo("body").show(),
                    b.titlePosition) {
                        case "inside":
                            i.css({
                                width: g.width - 2 * b.padding,
                                marginLeft: b.padding,
                                marginRight: b.padding
                            });
                            v = i.outerHeight(!0);
                            i.appendTo(z);
                            g.height += v;
                            break;
                        case "over":
                            i.css({
                                marginLeft: b.padding,
                                width: g.width - 2 * b.padding,
                                bottom: b.padding
                            }).appendTo(z);
                            break;
                        case "float":
                            i.css("left", -1 * parseInt((i.width() - g.width - 40) / 2, 10)).appendTo(e);
                            break;
                        default:
                            i.css({
                                width: g.width - 2 * b.padding,
                                paddingLeft: b.padding,
                                paddingRight: b.padding
                            }).appendTo(e)
                    }
                i.hide();
                e.is(":visible") ? (a(A.add(w).add(x)).hide(),
                    c = e.position(), r = {
                        top: c.top,
                        left: c.left,
                        width: e.width(),
                        height: e.height()
                    }, h = r.width == g.width && r.height == g.height, l.fadeTo(b.changeFade, 0.3, function () {
                        var c = function () {
                            l.html(n.contents()).fadeTo(b.changeFade, 1, N)
                        };
                        a.event.trigger("fancybox-change");
                        l.empty().removeAttr("filter").css({
                            "border-width": b.padding,
                            width: g.width - 2 * b.padding,
                            height: d.autoDimensions ? "auto" : g.height - v - 2 * b.padding
                        });
                        h ? c() : (y.prop = 0, a(y).animate({
                            prop: 1
                        }, {
                                duration: b.changeSpeed,
                                easing: b.easingChange,
                                step: O,
                                complete: c
                            }))
                    })) :
                    (e.removeAttr("style"), l.css("border-width", b.padding), "elastic" == b.transitionIn ? (r = P(), l.html(n.contents()), e.show(), b.opacity && (g.opacity = 0), y.prop = 0, a(y).animate({
                        prop: 1
                    }, {
                            duration: b.speedIn,
                            easing: b.easingIn,
                            step: O,
                            complete: N
                        })) : ("inside" == b.titlePosition && 0 < v && i.show(), l.css({
                            width: g.width - 2 * b.padding,
                            height: d.autoDimensions ? "auto" : g.height - v - 2 * b.padding
                        }).html(n.contents()), e.css(g).fadeIn("none" == b.transitionIn ? 0 : b.speedIn, N)))
            }
        },
        N = function () {
            /*a.support.opacity || (l.get(0).style.removeAttribute("filter"),
                e.get(0).style.removeAttribute("filter"));*/
            /*a.support.opacity || (l.get(0).attr("style","filter:none"),
                e.get(0).attr("style", "filter:none"));*/
            $('#fancybox-content').css('filter', 0);
            $('#fancybox-wrap').css('filter', 0);
            d.autoDimensions && l.css("height", "auto");
            e.css("height", "auto");
            u && u.length && i.show();
            b.showCloseButton && A.show();
            (b.enableEscapeButton || b.enableKeyboardNav) && a(document).bind("keydown.fb", function (c) {
                if (c.keyCode == 27 && b.enableEscapeButton) {
                    c.preventDefault();
                    a.fancybox.close()
                } else if ((c.keyCode == 37 || c.keyCode == 39) && b.enableKeyboardNav && c.target.tagName !== "INPUT" && c.target.tagName !== "TEXTAREA" && c.target.tagName !== "SELECT") {
                    c.preventDefault();
                    a.fancybox[c.keyCode ==
                        37 ? "prev" : "next"]()
                }
            });
            b.showNavArrows ? ((b.cyclic && 1 < j.length || 0 !== p) && w.show(), (b.cyclic && 1 < j.length || p != j.length - 1) && x.show()) : (w.hide(), x.hide());
            b.hideOnContentClick && l.bind("click", a.fancybox.close);
            b.hideOnOverlayClick && t.bind("click", a.fancybox.close);
            a(window).bind("resize.fb", a.fancybox.resize);
            b.centerOnScroll && a(window).bind("scroll.fb", a.fancybox.center);
            "iframe" == b.type && a('<iframe id="fancybox-frame" name="fancybox-frame' + (new Date).getTime() + '" frameborder="0" hspace="0" ' + (a.browser.msie ?
                'allowtransparency="true""' : "") + ' scrolling="' + d.scrolling + '" src="' + b.href + '"></iframe>').appendTo(l);
            e.show();
            k = !1;
            a.fancybox.center();
            b.onComplete(j, p, b);
            var c, h;
            j.length - 1 > p && (c = j[p + 1].href, "undefined" !== typeof c && c.match(F) && (h = new Image, h.src = c));
            0 < p && (c = j[p - 1].href, "undefined" !== typeof c && c.match(F) && (h = new Image, h.src = c))
        },
        O = function (a) {
            var d = {
                width: parseInt(r.width + (g.width - r.width) * a, 10),
                height: parseInt(r.height + (g.height - r.height) * a, 10),
                top: parseInt(r.top + (g.top - r.top) * a, 10),
                left: parseInt(r.left +
                    (g.left - r.left) * a, 10)
            };
            "undefined" !== typeof g.opacity && (d.opacity = 0.5 > a ? 0.5 : a);
            e.css(d);
            l.css({
                width: d.width - 2 * b.padding,
                height: d.height - v * a - 2 * b.padding
            })
        },
        M = function () {
            return [a(window).width() - 2 * b.margin, a(window).height() - 2 * b.margin, a(document).scrollLeft() + b.margin, a(document).scrollTop() + b.margin]
        },
        P = function () {
            var c = d.orig ? a(d.orig) : !1,
                h = {};
            c && c.length ? (h = c.offset(), h.top += parseInt(c.css("paddingTop"), 10) || 0, h.left += parseInt(c.css("paddingLeft"), 10) || 0, h.top += parseInt(c.css("border-top-width"),
                10) || 0, h.left += parseInt(c.css("border-left-width"), 10) || 0, h.width = c.width(), h.height = c.height(), h = {
                    width: h.width + 2 * b.padding,
                    height: h.height + 2 * b.padding,
                    top: h.top - b.padding - 20,
                    left: h.left - b.padding - 20
                }) : (c = M(), h = {
                    width: 2 * b.padding,
                    height: 2 * b.padding,
                    top: parseInt(c[3] + 0.5 * c[1], 10),
                    left: parseInt(c[2] + 0.5 * c[0], 10)
                });
            return h
        },
        R = function () {
            s.is(":visible") ? (a("div", s).css("top", -40 * H + "px"), H = (H + 1) % 12) : clearInterval(G)
        };
    a.fn.fancybox = function (c) {
        if (!a(this).length) return this;
        a(this).data("fancybox", a.extend({},
            c, a.metadata ? a(this).metadata() : {})).unbind("click.fb").bind("click.fb", function (c) {
                c.preventDefault();
                k || (k = !0, a(this).blur(), o = [], q = 0, c = a(this).attr("rel") || "", !c || "" == c || "nofollow" === c ? o.push(this) : (o = a("a[rel=" + c + "], area[rel=" + c + "]"), q = o.index(this)), E())
            });
        return this
    };
    a.fancybox = function (c, b) {
        var d;
        if (!k) {
            k = !0;
            d = "undefined" !== typeof b ? b : {};
            o = [];
            q = parseInt(d.index, 10) || 0;
            if (a.isArray(c)) {
                for (var e = 0, g = c.length; e < g; e++) "object" == typeof c[e] ? a(c[e]).data("fancybox", a.extend({}, d, c[e])) : c[e] = a({}).data("fancybox",
                    a.extend({
                        content: c[e]
                    }, d));
                o = jQuery.merge(o, c)
            } else "object" == typeof c ? a(c).data("fancybox", a.extend({}, d, c)) : c = a({}).data("fancybox", a.extend({
                content: c
            }, d)), o.push(c);
            if (q > o.length || 0 > q) q = 0;
            E()
        }
    };
    a.fancybox.showActivity = function () {
        clearInterval(G);
        s.show();
        G = setInterval(R, 66)
    };
    a.fancybox.hideActivity = function () {
        s.hide()
    };
    a.fancybox.next = function () {
        return a.fancybox.pos(p + 1)
    };
    a.fancybox.prev = function () {
        return a.fancybox.pos(p - 1)
    };
    a.fancybox.pos = function (a) {
        k || (a = parseInt(a), o = j, -1 < a && a < j.length ?
            (q = a, E()) : b.cyclic && 1 < j.length && (q = a >= j.length ? 0 : j.length - 1, E()))
    };
    a.fancybox.cancel = function () {
        k || (k = !0, a.event.trigger("fancybox-cancel"), J(), d.onCancel(o, q, d), k = !1)
    };
    a.fancybox.close = function () {
        function c() {
            t.fadeOut("fast");
            i.empty().hide();
            e.hide();
            a.event.trigger("fancybox-cleanup");
            l.empty();
            b.onClosed(j, p, b);
            j = d = [];
            p = q = 0;
            b = d = {};
            k = !1
        }
        if (!k && !e.is(":hidden"))
            if (k = !0, b && !1 === b.onCleanup(j, p, b)) k = !1;
            else if (J(), a(A.add(w).add(x)).hide(), a(l.add(t)).unbind(), a(window).unbind("resize.fb scroll.fb"),
                a(document).unbind("keydown.fb"), l.find("iframe").attr("src", I && /^https/i.test(window.location.href || "") ? "javascript:void(false)" : "about:blank"), "inside" !== b.titlePosition && i.empty(), e.stop(), "elastic" == b.transitionOut) {
                r = P();
                var h = e.position();
                g = {
                    top: h.top,
                    left: h.left,
                    width: e.width(),
                    height: e.height()
                };
                b.opacity && (g.opacity = 1);
                i.empty().hide();
                y.prop = 1;
                a(y).animate({
                    prop: 0
                }, {
                        duration: b.speedOut,
                        easing: b.easingOut,
                        step: O,
                        complete: c
                    })
            } else e.fadeOut("none" == b.transitionOut ? 0 : b.speedOut, c)
    };
    a.fancybox.resize =
        function () {
            t.is(":visible") && t.css("height", a(document).height());
            a.fancybox.center(!0)
        };
    a.fancybox.center = function (a) {
        var d, f;
        if (!k && (f = !0 === a ? 1 : 0, d = M(), f || !(e.width() > d[0] || e.height() > d[1]))) e.stop().animate({
            top: parseInt(Math.max(d[3] - 20, d[3] + 0.5 * (d[1] - l.height() - 40) - b.padding)),
            left: parseInt(Math.max(d[2] - 20, d[2] + 0.5 * (d[0] - l.width() - 40) - b.padding))
        }, "number" == typeof a ? a : 200)
    };
    a.fancybox.init = function () {
        a("#fancybox-wrap").length || (a("body").append(n = a('<div id="fancybox-tmp"></div>'), s = a('<div id="fancybox-loading"><div></div></div>'),
            t = a('<div id="fancybox-overlay"></div>'), e = a('<div id="fancybox-wrap"></div>')), z = a('<div id="fancybox-outer"></div>').append('<div class="fancybox-bg" id="fancybox-bg-n"></div><div class="fancybox-bg" id="fancybox-bg-ne"></div><div class="fancybox-bg" id="fancybox-bg-e"></div><div class="fancybox-bg" id="fancybox-bg-se"></div><div class="fancybox-bg" id="fancybox-bg-s"></div><div class="fancybox-bg" id="fancybox-bg-sw"></div><div class="fancybox-bg" id="fancybox-bg-w"></div><div class="fancybox-bg" id="fancybox-bg-nw"></div>').appendTo(e),
            z.append(l = a('<div id="fancybox-content"></div>'), A = a('<a id="fancybox-close"></a>'), i = a('<div id="fancybox-title"></div>'), w = a('<a href="javascript:;" id="fancybox-left"><span class="fancy-ico" id="fancybox-left-ico"></span></a>'), x = a('<a href="javascript:;" id="fancybox-right"><span class="fancy-ico" id="fancybox-right-ico"></span></a>')), A.click(a.fancybox.close), s.click(a.fancybox.cancel), w.click(function (c) {
                c.preventDefault();
                a.fancybox.prev()
            }), x.click(function (c) {
                c.preventDefault();
                a.fancybox.next()
            }),
            a.fn.mousewheel && e.bind("mousewheel.fb", function (c, b) {
                if (k) c.preventDefault();
                else if (0 == a(c.target).get(0).clientHeight || a(c.target).get(0).scrollHeight === a(c.target).get(0).clientHeight) c.preventDefault(), a.fancybox[0 < b ? "prev" : "next"]()
            }), a.support.opacity || e.addClass("fancybox-ie"), I && (s.addClass("fancybox-ie6"), e.addClass("fancybox-ie6"), a('<iframe id="fancybox-hide-sel-frame" src="' + (/^https/i.test(window.location.href || "") ? "javascript:void(false)" : "about:blank") + '" scrolling="no" border="0" frameborder="0" tabindex="-1"></iframe>').prependTo(z)))
    };
    a.fn.fancybox.defaults = {
        padding: 10,
        margin: 40,
        opacity: !1,
        modal: !1,
        cyclic: !1,
        scrolling: "auto",
        width: 560,
        height: 340,
        autoScale: !0,
        autoDimensions: !0,
        centerOnScroll: !0,
        ajax: {},
        swf: {
            wmode: "transparent"
        },
        hideOnOverlayClick: !0,
        hideOnContentClick: !1,
        overlayShow: !0,
        overlayOpacity: 0.7,
        overlayColor: "#777",
        titleShow: !0,
        titlePosition: "float",
        titleFormat: null,
        titleFromAlt: !1,
        transitionIn: "fade",
        transitionOut: "fade",
        speedIn: 300,
        speedOut: 300,
        changeSpeed: 300,
        changeFade: "fast",
        easingIn: "swing",
        easingOut: "swing",
        showCloseButton: !0,
        showNavArrows: !0,
        enableEscapeButton: !0,
        enableKeyboardNav: !0,
        onStart: function () { },
        onCancel: function () { },
        onComplete: function () { },
        onCleanup: function () { },
        onClosed: function () { },
        onError: function () { }
    };
    a(document).ready(function () {
        a.fancybox.init()
    })
})(jQuery);