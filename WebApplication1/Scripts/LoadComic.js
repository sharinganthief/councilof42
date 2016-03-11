   
window.loading = false;

function LoadComic(tag, comicDivId) {

    //NProgress.configure({ minimum: 0.1 });
    //NProgress.start();
    var url = tag.data("href");
    if (url && url.length > 0 && comicDivId && comicDivId.length > 0) {
        
        $('html, body').animate({ scrollTop: $('#' + comicDivId).position().top - 20 }, 'slow');
        window.loading = true;
        $("#" + comicDivId).load(url, function() {
            window.loading = false;
        });
    }

}


    