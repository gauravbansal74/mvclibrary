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