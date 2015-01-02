function showalert(i,cstmmsg) {
    switch(i) {
        case false:
            $(body).prepend('<div id="cstmalert" class="alert alert-danger alert-dismissible fade in" role="alert" style="margin: 0 auto; width: 100%;margin-bottom:2px;z-index:10099;position: fixed;"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button><strong>Error !!</strong> ' + cstmmsg + '</div>');
            break;
        case true:
            $(body).prepend('<div id="cstmalertsuccess" class="alert alert-success alert-dismissible fade in" role="alert" style="margin: 0 auto; width: 100%;margin-bottom:2px;z-index:10099;position: fixed;"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button><strong>Success !!</strong> ' + cstmmsg + '</div>');
            break;
    }
    $("#cstmalert").fadeTo("slow").slideUp(2000);
    
}

var loaderSpinnerDiv = '<div class="loading-container-div"><div class="loading-div"><svg class="loading-spinner" width="50px" height="50px" viewBox="0 0 66 66" xmlns="http://www.w3.org/2000/svg"><circle class="path" fill="none" stroke-width="6" stroke-linecap="round" cx="33" cy="33" r="30"></circle></svg></div><div class="loading-opacity-div"></div></div>';
var blockBySpinner = function (key, unload) {
    if (!unload) {
        var $loadObj = $(loaderSpinnerDiv);
        var tempPosition = $(key).css('position');
        var tempMinHeight = $(key).css('min-height');
        $(key).css('position', 'relative')
                .css('min-height', '100px')
                .attr('tempPosition', tempPosition)
                .attr('tempMinHeight', tempMinHeight)
                .append($loadObj);
        $loadObj.height($(key).height());
        $loadObj.fadeOut(0);
        $loadObj.fadeIn(500);
    }
    else {
        var $loadObj = $(key + ' .loading-container-div');
        var tempPosition = $(key).attr('tempPosition');
        var tempMinHeight = $(key).attr('tempMinHeight');
        $(key).css('position', tempPosition);
        $(key).css('min-height', tempMinHeight);
        $loadObj.fadeOut(0);
        $loadObj.remove();
    }
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}