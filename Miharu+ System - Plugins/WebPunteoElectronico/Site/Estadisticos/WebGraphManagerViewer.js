function ShowOptions(id) {
    var idOptionDiv = "#OptionDiv_" + id;
    var idShowDiv = "#ShowDiv_" + id;

    $(idShowDiv).css("display", "none");
    $(idOptionDiv).corner();
    $(idOptionDiv).animate({
        opacity: 0.9,
        height: 'toggle'
    }, 500, function () {
    });
}

function HideOptions(id) {
    var idDiv = "#OptionDiv_" + id;
    var idShowDiv = "#ShowDiv_" + id;

    $(idShowDiv).css("display", "");
    $(idDiv).css("display", "none");
}
