

$(document).ready(function () {
    $("<div></div>")
        .css('width', '150px')
        .css('height', '150px')
        .css('background-color', 'red')
        .css({"margin-left":"5px","margin-right":"5px"})
        .appendTo("#CONTENT").fadeIn(8000);

    $("<div></div>")
        .css('width', '150px')
        .css('height', '150px')
        .css('background-color', 'blue')
        .css({ "margin-left": "5px", "margin-right": "5px" })
        .appendTo("#CONTENT").fadeIn(8000);

    $("button").click(function () {

        $("#CONTENT")
            .children("div")
            .toggle(1000);
    });
});