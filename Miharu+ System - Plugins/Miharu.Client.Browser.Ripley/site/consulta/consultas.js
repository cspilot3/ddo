Frm = {
    CurrentPage: "consultas.aspx",
    DocumentosList: [],
    PuntosList: [],
    ProyectosList: [],
    Campos: [],
    PermisosDocumento: [],
    Registros: [],
    Validaciones: [],
    UrlVisorImagen: "",


    Init: function () {
        var InitMainGrid = function () {
            if (Grids.MainGrid_flex) Frm.MainGrid = Grids.MainGrid_flex[0].grid;
            else setTimeout(InitMainGrid, 100);
        };

        setTimeout(InitMainGrid, 100);

        Frm.fechaInicialInput.datepicker().mask("9999/99/99");
        //        Frm.fechaInicialInput.val($.datepicker.formatDate('yy/mm/dd', new Date()));
        Frm.fechaFinalInput.datepicker().mask("9999/99/99");
        //        Frm.fechaFinalInput.val($.datepicker.formatDate('yy/mm/dd', new Date()));
        Frm.proyectoInput.autoList(Frm.ProyectosList, Frm.CargarDocumentosPorProyecto);
        Frm.puntoInput.autoList(Frm.PuntosList);
        Frm.documentoInput.autoList(Frm.DocumentosList, Frm.CargarCamposPorTipo);
      
        Global.ShowOptions('E', 'D');
        Options.find.click(Frm.Buscar);
    },


    CargarCamposPorTipo: function () {
        var count = 0;
        try {
            Frm.CamposContainer.html("");
            //Frm.PermisosDocumento.html("");
            var param = Frm.documentoInput.ValidPar("Tipo de documento");
            param += "&" + Frm.proyectoInput.ValidPar("Proyecto");
            AjaxRequest("CargarCamposPorTipo", "", param, function (html) {
                TryExec(html);

                $(Frm.Campos).each(function (i, campo) {
                    if (campo.Activo && campo.Es_Busqueda) {
                        var id = "campo_" + campo.id_Campo;
                        html = "<tr><td style='width:150px'>{nombre}</td><td><input id='{id}' type='text' style='width:300px'/></td><tr>";

                        html = html.replace("{id}", id);
                        html = html.replace("{nombre}", campo.Nombre_Campo);

                        Frm.CamposContainer.append(html);
                        count = count + 1;
                    }
                });

                if (count > 0) {
                    html = "<tr><td style='width:150px'>VALOR ABSOLUTO</td><td><input type='checkbox' checked id='cbAbsoluto' name='cbAbsoluto'/> </td><tr>";
                    Frm.CamposContainer.append(html);
                }

            });
        } catch (e) {
            alert(e);
        }
    },

    CargarDocumentosPorProyecto: function () {
        try {
            var param = Frm.proyectoInput.ValidPar("Proyecto");
            AjaxRequest("CargarDocumentosPorProyecto", "", param, function (html) {
                TryExec(html);
                SetAutoListDataSource(Frm.documentoInput, Frm.DocumentosList);
            });

        } catch (e) {
            alert(e);
        }
    },

    MostrarColumnasDocumento: function () {
        $(".col_header", "#MainGrid").each(function (i, col) {
            var colName = $(col).attr('abbr'), campo = null, selector = null, cellIndex = null;
            if (colName.indexOf('Campo_') == 0) {
                $(Frm.Campos).each(function (j, c) { if (colName == "Campo_" + c.id_Campo) campo = c; });
                $("input", Frm.MainGrid.nDiv).each(function (j, c) {
                    if (c.value == col.cellIndex) {
                        cellIndex = col.cellIndex;
                        selector = $('.ndcol2', $(c).parent().parent());
                    }
                });
                if (campo != null) {
                    $("div", $(col)).html(campo.Nombre_Campo);
                    if (selector) selector.html(campo.Nombre_Campo);
                    if (cellIndex) Frm.MainGrid.toggleCol(cellIndex, true);
                } else {
                    $("div", $(col)).html("N/A");
                    if (selector) selector.html("N/A");
                    if (cellIndex) Frm.MainGrid.toggleCol(cellIndex, false);
                }
            }
          
            if (Frm.PermisosDocumento[0].Ver_Imagen == "False" && colName == "UrlImagen") {
                Frm.MainGrid.toggleCol(0, false);
            }
            if (Frm.PermisosDocumento[0].Ver_Imagen == "True" && colName == "UrlImagen") {
                Frm.MainGrid.toggleCol(0, true);
            }
        });
    },

    MainGridFormat: function flexigridCellFormat(colName, idx, content, row) {
        if (colName == "UrlImagen") {
            var Token = row.cell[0];
            return "<img class='click' alt='imagen' src='../../images/basic/visualizar.png' onclick='window.open(\"" + Frm.UrlVisorImagen + "?Token=" + Token + "\");' />";
        } else {
            return content;
        }
    },

    Buscar: function () {
        try {
            Frm.ValidacionesContainer.html("");
            Frm.MostrarColumnasDocumento();

            var param = Frm.fechaInicialInput.ValidPar("Fecha inicial", false);
            param += "&" + Frm.fechaFinalInput.ValidPar("Fecha final", false);
            param += "&" + Frm.proyectoInput.ValidPar("Proyecto");
            param += "&" + Frm.puntoInput.ValidPar("Punto");
            param += "&" + Frm.documentoInput.ValidPar("Tipo de documento");
            param += "&" + ArrayParams($("input", "#CamposContainer"));
            AjaxRequest("Buscar", "", param, function (html) {
                TryExec(html);
                $('#MainGrid_flex').flexReload();
            });
        } catch (e) {
            alert(e);
        }
    },

    MainGridClick: function (value, row, cells, evt, grid) {
        try {
            Frm.ValidacionesContainer.html("");

            var param = "fk_Expediente=" + cells.fk_Expediente;
            param += "&fk_Folder=" + cells.fk_Folder;
            param += "&fk_File=" + cells.fk_File;

            AjaxRequestUrl("CargarValidaciones", "", param, function (html) {
                TryExec(html);

                $(Frm.Validaciones).each(function (i, vali) {
                    html = "<tr><td >{nombre}</td><td>{res}</td><tr>";

                    html = html.replace("{nombre}", vali.Nombre_Validacion);
                    html = html.replace("{res}", (vali.Respuesta) ? "Si" : "No");

                    Frm.ValidacionesContainer.append(html);
                });


            });
        } catch (e) {
            alert(e);
        }
    }
};

function flexigridCellFormat(colName, idx, content, row, grid) {
    return Frm.MainGridFormat(colName, idx, content, row);
}