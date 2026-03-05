Frm.Dashboard.Presupuesto = {
    Name: "Presupuesto",
    Plots: [],

    Init: function () {
        $("#presupuesto_a").click(function () {
            Frm.Dashboard.Presupuesto.Reload();
        });

        this.Plots[0] = this.Plot0;
        this.Plots[1] = this.Plot1;

        $(this.Plots).each(function (i, p) {
            p.Show();
        });
    },

    Reload: function () {
        $(this.Plots).each(function (i, p) {
            try { p.Reload(); } catch (e) { }
        });
    },

    //Componentes Mayor Impacto Valor
    Plot0: {
        Plot: null,
        Data: [],
        Series: [],

        Show: function () {
            AjaxRequest("ShowPresupuesto_ComponentesMayorImpactoValor", "", "", this.AjaxResult);
        },

        Reload: function () {
            Frm.Dashboard.Presupuesto.Plot0.Plot.replot();
        },

        AjaxResult: function (html) {
            TryExec(html);

            if (this.Plot)
                this.Plot.destroy();

            var ticks = ['Componente'];

            if (this.Data.length != 0) {
                Frm.Dashboard.Presupuesto.Plot0.Plot = $.jqplot('chart_presupuesto_1', this.Data, {
                    title: 'Componentes Mayor Impacto - Valor',
                    animate: true,
                    animateReplot: true,

                    seriesDefaults: {
                        renderer: $.jqplot.BarRenderer,
                        rendererOptions: { fillToZero: true, animation: { speed: 2500} },
                        pointLabels: { show: true, location: 'n', edgeTolerance: -15 }
                    },
                    series: this.Series,
                    legend: { show: true, placement: 'outsideGrid' },
                    axes: {
                        xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: ticks
                        },
                        yaxis: {
                            pad: 1.05,
                            tickOptions: { formatString: "$%'i" }
                        }
                    }
                });
            }
        }
    },

    //Componentes Mayor Impacto Cantidad
    Plot1: {
        Plot: null,
        Data: [],
        Series: [],

        Show: function () {
            AjaxRequest("ShowPresupuesto_ComponentesMayorImpactoCantidad", "", "", this.AjaxResult);
        },

        Reload: function () {
            Frm.Dashboard.Presupuesto.Plot1.Plot.replot();
        },

        AjaxResult: function (html) {
            TryExec(html);

            if (this.Plot)
                this.Plot.destroy();

            var ticks = ['Componente'];
            if (this.Data.length != 0) {
                Frm.Dashboard.Presupuesto.Plot1.Plot = $.jqplot('chart_presupuesto_2', this.Data, {
                    title: 'Componentes Mayor Impacto - Cantidad',
                    animate: true,
                    animateReplot: true,

                    seriesDefaults: {
                        renderer: $.jqplot.BarRenderer,
                        rendererOptions: { fillToZero: true, animation: { speed: 2500} },
                        pointLabels: { show: true, location: 'n', edgeTolerance: -15 }
                    },
                    series: this.Series,
                    legend: { show: true, placement: 'outsideGrid' },
                    axes: {
                        xaxis: {
                            renderer: $.jqplot.CategoryAxisRenderer,
                            ticks: ticks
                        },
                        yaxis: {
                            pad: 1.05,
                            tickOptions: { formatString: "$%'i" }
                        }
                    }
                });
            }
        }
    }
}

Frm.DashboardItems[Frm.DashboardItems.length] = Frm.Dashboard.Presupuesto;