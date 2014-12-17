function showalert(i,cstmmsg) {
    switch(i) {
        case false:
            $("#maindiv").prepend('<div id="cstmalert" class="alert alert-danger alert-dismissible fade in" role="alert" style="margin: 0 auto; width: 70%;margin-bottom:2px;"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button><strong>Error !!</strong> ' + cstmmsg + '</div>');
            break;
        case true:
            $("#maindiv").prepend('<div id="cstmalert" class="alert alert-success alert-dismissible fade in" role="alert" style="margin: 0 auto; width: 70%;margin-bottom:2px;"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button><strong>Success !!</strong> ' + cstmmsg + '</div>');
            break;
    }
    $("#cstmalert").fadeTo(1000, 500).slideUp(500, function () {
        $("#cstmalert").alert('close');
    });
    
}