AppInfo = {
    CurrentPage: "main.aspx",
    currentDate: null,

    DataPagosbyMes: [],
    DataProveedoresByObra: [],

    Init: function() {
        AppInfo.clock();
    },

    //
    // Initialize the clock.
    //
    clock: function() {
        AjaxRequestUrl("GetDate", "", "", function(html) {
            TryExec(html);
            AppInfo.showClock(AppInfo.currentDate);
        }, undefined, AppInfo.CurrentPage, undefined, true);

        // Update every 60 seconds.
        setTimeout(AppInfo.clock, 60000);
    },

    showClock: function(date_obj) {
        var clock = $('#clock');

        if (!clock.length) return;

        var hour = date_obj.getHours();
        var minute = date_obj.getMinutes();
        var day = date_obj.getDate();
        var year = date_obj.getFullYear();
        var suffix = 'AM';

        var weekday = $.datepicker.regional['es'].dayNames[date_obj.getDay()];
        var month = $.datepicker.regional['es'].monthNames[date_obj.getMonth()];

        // AM or PM?
        if (hour >= 12) suffix = 'PM';

        if (hour > 12) hour = hour - 12;
        else if (hour === 0) hour = 12;

        if (minute < 10) minute = '0' + minute;

        // Build two HTML strings.
        var clock_time = hour + ':' + minute + ' ' + suffix;
        var clock_date = weekday + ', ' + day + ' de ' + month + ' de ' + year;

        // Shove in the HTML.
        clock.html(clock_date + " - " + clock_time); //.attr('title', clock_date);
    },

    ChangePassword: function() {
        $('#OptionTooltip').hide();

        ShowDivDialog("ChangePassword_Ventana", "Cambiar contraseña", 430, 200, "AceptarPassword", "CancelarPassword", Site.ChangePasswordClick);
        $("#OldPassword").focus();
    }
};