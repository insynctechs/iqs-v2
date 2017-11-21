(function () {

    var scriptName = "embed.js"; //name of this script, used to get reference to own tag
    var jQuery; //noconflict reference to jquery
    var jqueryPath = "http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"; 
    var jqueryVersion = "1.8.3";
    var scriptTag; //reference to the html script tag

    /******** Get reference to self (scriptTag) *********/
    var allScripts = document.getElementsByTagName('script');
    var targetScripts = [];
    for (var i in allScripts) {
        var name = allScripts[i].src
        if(name && name.indexOf(scriptName) > 0)
            targetScripts.push(allScripts[i]);
    }

    scriptTag = targetScripts[targetScripts.length - 1];

    /******** helper function to load external scripts *********/
    function loadScript(src, onLoad) {
        var script_tag = document.createElement('script');
        script_tag.setAttribute("type", "text/javascript");
        script_tag.setAttribute("src", src);

        if (script_tag.readyState) {
            script_tag.onreadystatechange = function () {
                if (this.readyState == 'complete' || this.readyState == 'loaded') {
                    onLoad();
                }
            };
        } else {
            script_tag.onload = onLoad;
        }
        (document.getElementsByTagName("head")[0] || document.documentElement).appendChild(script_tag);
    }

    /******** helper function to load external css  *********/
    function loadCss(href) {
        var link_tag = document.createElement('link');
        link_tag.setAttribute("type", "text/css");
        link_tag.setAttribute("rel", "stylesheet");
        link_tag.setAttribute("href", href);
        (document.getElementsByTagName("head")[0] || document.documentElement).appendChild(link_tag);
    }

    /******** load jquery into 'jQuery' variable then call main ********/
    if (window.jQuery === undefined || window.jQuery.fn.jquery !== jqueryVersion) {
        loadScript(jqueryPath, initjQuery);
    } else {
        initjQuery();
    }

    function initjQuery() {
        jQuery = window.jQuery.noConflict(true);
        main();
    }

    /******** starting point for your widget ********/
    function main() {
	document.getElementById("myCarousel").innerHTML ="<div id='title'><img src='http://www.iqsdirectory.com/news-events/images/info_icon.png' class='title-icon'>Helpful Resources</div><div class='sliders'><a href='https://www.cxenergy.com/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/cxenergy.jpg' alt='CxEnergy Conference & Expo' title='CxEnergy Conference & Expo'></a><p></p><p class='link'><a href='https://www.cxenergy.com/' target='_blank' rel='nofollow'>CxEnergy Conference & Expo</a></p><div class='button'><a href='https://www.cxenergy.com/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='https://www.aabc.com/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/aabc.jpg' alt='Associated Air Balance Council' title='Associated Air Balance Council'></a><p></p><p class='link'><a href='https://www.aabc.com/' target='_blank' rel='nofollow'>Associated Air Balance Council</a></p><div class='button'><a href='https://www.aabc.com/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='http://energymgmt.org/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/ema.jpg' alt='Energy Management Association' title='Energy Management Association'></a><p></p><p class='link'><a href='http://energymgmt.org/' target='_blank' rel='nofollow'>Energy Management Association</a></p><div class='button'><a href='http://energymgmt.org/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='https://www.commissioning.org/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/acg.jpg' alt='ACG AABC Commissioning Group' title='ACG AABC Commissioning Group'></a><p></p><p class='link'><a href='https://www.commissioning.org/' target='_blank' rel='nofollow'>AABC Commissioning Group</a></p><div class='button'><a href='https://www.commissioning.org/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='https://www.american-purchasing.com/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/aps.jpg' alt='American Purchasing Society' title='American Purchasing Society'></a><p></p><p class='link'><a href='https://www.american-purchasing.com/' target='_blank' rel='nofollow'>American Purchasing Society</a></p><div class='button'><a href='https://www.american-purchasing.com/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='http://www.nasampe.org/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/sampe.jpg' alt='Society for the Advancement of Material and Process Engineering' title='Society for the Advancement of Material and Process Engineering'></a><p></p><p class='link'><a href='http://www.nasampe.org/' target='_blank' rel='nofollow'>SAMPE</a></p><div class='button'><a href='http://www.nasampe.org/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='http://www.myprocessexpo.com/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/process-expo.jpg' alt='Process Expo' title='Process Expo'></a><p></p><p class='link'><a href='http://www.myprocessexpo.com/' target='_blank' rel='nofollow'>Process Expo</a></p><div class='button'><a href='http://www.myprocessexpo.com/' target='_blank' rel='nofollow'>Learn More</a></div><p></p></div><div class='sliders'><a href='http://www.metalcon.com/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/metalcon.jpg' alt='METALCON' title='METALCON'></a><p></p><p class='link'><a href='http://www.metalcon.com/' target='_blank' rel='nofollow'>METALCON</a></p><div class='button'><a href='http://www.metalcon.com/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='sliders'><a href='http://www.aws.org/' target='_blank' rel='nofollow'><img src='http://www.iqsdirectory.com/news-events/images/aws.jpg' alt='American Welding Society' title='American Welding Society'></a><p></p><p class='link'><a href='http://www.aws.org/' target='_blank' rel='nofollow'>American Welding Society</a></p><div class='button'><a href='http://www.aws.org/' target='_blank' rel='nofollow'>Learn More</a></div></div><div class='clear'></div>";
        jQuery(document).ready(function ($) {
          //or you could wait until the page is ready

loadCss("http://www.iqsdirectory.com/news-events/css/widget-micro.css");
          //example jsonp call
          //var jsonp_url = "www.example.com/jsonpscript.js?callback=?";
          //jQuery.getJSON(jsonp_url, function(result) {
          //	alert("win");
          //});
          
          //example load css
         //  loadCss("http://www.deburringmachinery.com/wp-content/themes/domain%20directories/style.css?ver=4.8");
          
          //example script load
          //loadScript("http://example.com/anotherscript.js", function() { /* loaded */ });
        });
		
    }
})();