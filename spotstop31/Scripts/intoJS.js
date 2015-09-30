$(document).ready(function () {
    $("#howItWorks").click(function () {
        $('html, body').animate({
            scrollTop: $("#about").offset().top
        }, 1000);
    });
});