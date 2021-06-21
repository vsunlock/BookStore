

function WindowResize() {
    var widHt = $(window).height();
    var headerHt = $("#header").height();
    var footerHt = $("#footer").height();

    $("#indexPage").height(widHt - headerHt - footerHt - 30);
}