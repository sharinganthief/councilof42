$(function () {

    var keyChecked = false;
    var swipeChecked = false;

    function onKeyUp(evt) {
        if (keyChecked == false && (evt = evt ? evt : window.event ? event : null)) {
            switch (evt.keyCode) {
                //case 90: // z
                //case 188: // <
                //case 89: // y
                case 37: // (left)
                    console.log("left...");
                    keyChecked = true;
                    left();
                    return;
                    case 88: // x
                    case 191: // ?
                    case 82: // r
                        console.log("random...");
                        keyChecked = true;
                        random();
                        return;
                    //case 190: // >
                    //case 67: // c
                case 39: // (right)
                    console.log("right...");
                    keyChecked = true;
                    right();
                    return;
                    //case 86: // v
                    //    toggleBlock("aftercomic"); break;
            }
        }
    }

    function left() {
        change(".comic-prev");
    }
    function right() {
        change(".comic-next");
    }
    function random() {
        change(".comic-random");
    }

    function change(selector) {
        if (window.loading != undefined && window.loading != null && !window.loading) {
            $(selector).click();
        }
    }
    if (document.addEventListener) {
        document.addEventListener("keyup", onKeyUp, false);

    } else {
        document.onkeyup = onKeyUp;
    }
    
    $(".main-comic").hammer().bind('swiperight', function (ev) {
        if (swipeChecked == false) {
            left();
            swipeChecked = true;
        }
    });

    $(".main-comic").hammer().bind('swipeleft', function (ev) {
        if (swipeChecked == false) {
            right();
            swipeChecked = true;
        }
    });

});