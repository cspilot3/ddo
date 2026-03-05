ToolTipItem = { ToolTipItem : null, Message: null};

function ShowToolTip (c, msg) {
    ToolTipItem.Control = c;
    ToolTipItem.Message = msg;

    setTimeout(FadeInToolTip, 100);
}

function FadeInToolTip () {
    HideToolTip();

    var jq = $(ToolTipItem.Control);
    var offSet = jq.offset();
    offSet.left = offSet.left + jq.width() + 30;

    var qHtml = $("<div id='slyg_tooltip' class='slyg-tooltip'><div id='slyg-tooltip-content' class='slyg-tooltip-content'>" + ToolTipItem.Message + "</div></div>");
    qHtml.offset(offSet);
    $('body').append(qHtml)
    qHtml.fadeIn("slow");
}

function HideToolTip  () {
    $('#slyg_tooltip').remove();
}

function Currency(id) {
    $("#"+id).focus(function (a, b) {
        var c = $("#"+id)[0];
        ShowToolTip(c, c.value);
    });
    $("#"+id).keydown(function (a, b) {
        $("#slyg-tooltip-content").html($("#"+id).val());
    });
    $("#"+id).blur(function (a, b) {
        var c = $("#"+id)[0];
        HideToolTip();
    });
}