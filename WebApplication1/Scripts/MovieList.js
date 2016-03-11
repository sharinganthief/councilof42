function setMovieHeaderCheckBoxToSeen(filmId) {

    var checkBox = $('h3[data-filmID=' + filmId + '] .headerChk');
    checkBox.prop('checked', true);
}

function BuildMovieList(movieAccordionContainer, accordionId, movies) {
    var movieAccordion = $("#" + accordionId);
    if (movieAccordion != null) {
        movieAccordion.remove();
    }

    movieAccordion = $(document.createElement("div"));
    movieAccordion.attr("id", accordionId);

    for (var i = 0; i < movies.length; i++) {
        var movie = movies[i];
        console.log(movie);
        var titleNoSpaces = (movie.Title).replace(/[^a-z0-9]+ /gi, " ").replace(/ /g,'').replace('\'', '');
        console.log(titleNoSpaces);
        var h3 = $(document.createElement("h3"));
        h3.attr("id", titleNoSpaces + '-h3');
        h3.text(movie.Title);
        h3.attr('data-filmID', movie.FilmID);
        var checkBox = $('<input />', { type: 'checkbox', id: titleNoSpaces + 'chk' });
        checkBox.addClass('headerChk');
        checkBox.appendTo(h3);

        var div = $(document.createElement("div"));
        div.attr("id", titleNoSpaces + '-div');
        var ul = $(document.createElement("ul"));

        var magnetLi = $(document.createElement("li"));

        for (var prop in movie) {
            if (prop == "Title") {
                var magnetA = $(document.createElement("a"));
                magnetA.attr("href", "/Movie/Search/Magnet?title=" + movie[prop]);
                magnetA.attr("target", "_blank");
                magnetA.text('Torrent');
                magnetLi.append(magnetA);

                continue;
            }
            if (prop == "FilmID") continue;

            if (movie.hasOwnProperty(prop)) {
                var li = $(document.createElement("li"));
                var text = null;
                if (prop == "ImdbURL") {
                    var a = $(document.createElement("a"));
                    a.attr("href", movie[prop]);
                    a.attr("target", "_blank");
                    a.text('IMDB');
                    li.append(a);
                } else {
                    text = prop + ': ' + movie[prop];
                }

                if (text != null) {
                    li.text(text);
                }
                ul.append(li);
            }
        }

        ul.append(magnetLi);

        div.append(ul);
        movieAccordion.append(h3);
        movieAccordion.append(div);
    }
    movieAccordionContainer.append(movieAccordion);
    movieAccordion.accordion({
        collapsible: true
    });

    $('#' + accordionId + ' input[type="checkbox"]').click(function(e) {
        e.stopPropagation();
    });
}