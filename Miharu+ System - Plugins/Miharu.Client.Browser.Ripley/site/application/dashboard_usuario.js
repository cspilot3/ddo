Frm.Dashboard.Usuario = {
    Name: "Usuario",

    Init: function () {
        this.Show();
    },

    Show: function () {
        AjaxRequest("getNews", "", "", this.AjaxResult);
    },

    AjaxResult: function (html) {
        $(".news-container").html(html);

        $(".itemtitle").corner("5px top");
        $(".itemcontent").corner("5px bottom");

        $(".itemtitle").append("<div class='itemclose'></div>");
        $(".itemclose").click(function (e) {
            var newsItem = $(e.target).parent().parent();
            var param = "id=" + newsItem[0].id;

            newsItem.remove();
            AjaxRequest("RemoveNews", "", param, function (html) { });
        });
    }
};

Frm.DashboardItems[Frm.DashboardItems.length] = Frm.Dashboard.Usuario;