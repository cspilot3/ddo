<%@ Page Title="" Language="C#" MasterPageFile="~/master/master_form.Master" AutoEventWireup="true" CodeBehind="roles.aspx.cs" Inherits="Miharu.Client.Browser.site.administracion.seguridad.roles" %>

<%@ Register TagPrefix="miharu" Namespace="Miharu.Client.Browser.code.Grid" Assembly="Miharu.Client.Browser" %>
<%@ Register Src="~/controls/filter.ascx" TagName="filter" TagPrefix="block" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/flexigrid.custom.js")%>" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/styles/flexigrid-1.1/flexigrid.css")%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .gridclass
        {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            top: 50px;
        }
        .select {
            background-color: #f06770;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="body" runat="Server" ContentPlaceHolderID="body">
    <script type="text/javascript">

//        function SearchEsquema(esquemaSearch,idProyecto) {
        function SearchEsquema(esquemaSearch) {
            if (Frm.Documento.length > 0) {
                for (var x = 0; x < (Frm.Documento.length); x = x + 1) {
                    var regreso = Frm.Documento[x].fk_Esquema;
                    //var regresop = Frm.Documento[x].fk_Proyecto;
                    if (regreso == esquemaSearch) {
                       // if (regresop == idProyecto) {
                            // return Frm.tabla[x].id_inmueble_cuota;
                            return x;
                       // }
                    }
                }
            }

            return -1;
        };
        function objToString(obj) {
            var str = '';
            for (var p in obj) {
                if (obj.hasOwnProperty(p)) {
                    str += p + ':' + obj[p] + ',';
                }
            }
            return str;
        };

        Frm = {
            CurrentPage: "roles.aspx",
            Esquema: [],
            Documento: [],

            Init: function () {
                Global.InitConfigHandlers("tabs", Frm.OptionClick);
                Global.InitFilter(Frm.FilterClick);
            },

            FilterClick: function (filter) {
                var param = "Filtro=" + filter;
                AjaxRequest("CambiarFiltro", "", param, function () { $('#MainGrid_flex').flexReload(); });
                //alert(GetAtt(e.target, 'filter'));
            },

            MainGridClick: function (value, row, cells, evt, grid) {
                $("#Detalle").show();
                Frm.id_rol.val(cells.id_Rol);
                Frm.nombre_rol.val(cells.Nombre_Rol);
                $("#Esquema_Container").html("");
                $("#Documento_Container").html("");

                Global.SelectTab("tabs", 1, "Edit");
                Frm.Documento = [];
                Frm.CargarEsquema();
                //Frm.CargarDocumento();
                //Frm.CargarProyectos();
            },

            CargarProyectos: function () {
                try {
                    $("#Proyectos_Container").html("");
                    var param = Par("id_rol");
                    AjaxRequest("CargarProyectos", "", param, function (html) {
                        TryExec(html);

                        $(Frm.Proyectos).each(function (i, p) {
                            var id = "entidad_" + p.fk_entidad + "_" + p.id_proyecto;
                            var html = "<tr><td><input id='{0}' type='checkbox'/><label>{1}</label> </td><td>{2}</td></tr>";
                            html = ReplaceAll(html, "{0}", id);
                            html = html.replace("{1}", p.nombre_entidad);
                            html = html.replace("{2}", p.nombre_proyecto);
                            $("#Proyectos_Container").append(html);

                            $("#" + id)[0].checked = (p.asignado == 1);
                        });
                    });
                } catch (e) { alert(e); }
            },

            CargarEsquema: function () {
                try {
                    $("#Esquema_Container").html("");
                    var param = Par("id_rol");
                    AjaxRequest("CargarEsquema", "", param, function (html) {
                        TryExec(html);

                        $(Frm.Esquema).each(function (i, p) {
                            var id = "esquema_" + p.fk_Esquema + "_proyecto_" + p.fk_Proyecto;
                            var html = "<tr>" +
                                 "<td id='{2}' onclick='Frm.CargarDocumento(" + p.fk_Esquema + "," + p.fk_Proyecto + ");'><input id='{0}' type='checkbox' style='display:none'  />" +//checked='false'
                            //    "<td id='{2}' onclick='Frm.CargarDocumento(" + p.fk_Esquema + ");'><input id='{0}' type='checkbox'/>" +
                                "<label>{1}</label> </td></tr>";
                            html = ReplaceAll(html, "{0}", id);
                            html = html.replace("{1}", p.Nombre_Esquema);
                            html = html.replace("{2}", id + "_td");
                            $("#Esquema_Container").append(html);

                            $("#" + id)[0].checked = p.asignado;
                        });
                    });
                } catch (e) { alert(e); }
            },

            CargarDocumento: function (nidEsquema, nIdProyecto) {
                //alert(nidEsquema);
                //alert(nIdProyecto);
                try {
                    $("td", "#Esquema_Container").removeClass('select');
                    $("#esquema_" + nidEsquema + "_proyecto_" + nIdProyecto + "_td").addClass('select');
                    //$("#esquema_" + nidEsquema + "_td").addClass('select');

                    //var idEsquemaBuscado = SearchEsquema(nidEsquema, nIdProyecto);
                    var idEsquemaBuscado = SearchEsquema(nidEsquema);
                    //alert(idEsquemaBuscado);
                    if (idEsquemaBuscado >= 1) {
                        //Frm.CrearTablaDocumentos(idEsquemaBuscado);
                    }
                    else {
                        var param = "fk_Esquema=" + nidEsquema;
                        param += "&" + Par("id_rol");
                        param += "&" + "fk_Proyecto=" + nIdProyecto;
                        //alert(param);
                        AjaxRequest("CargarDocumentoServer", "", param, function (html) {
                            TryExec(html);
                            idEsquemaBuscado = Frm.Documento.length - 1;
                            Frm.CrearTablaDocumentos(idEsquemaBuscado);
                        });
                    }



                } catch (e) { alert(e); }
            },
            CrearTablaDocumentos: function (idEsquemaBuscado) {
                //alert(idEsquemaBuscado);
                var j = 0;
                $("#Documento_Container").html("");

                $(Frm.Documento[idEsquemaBuscado].Documento).each(function (i, p) {
                    var id = "documento_" + j + "_" + Frm.Documento[idEsquemaBuscado].fk_Esquema + "_" + p.fk_Documento;
                    j = j + 1;

                    var html = "<tr>" +
                                "<td><label for='{0}'>{1}</label> </td>" +
                                "<td style='text-align:center';><input onchange='Frm.CambioDocumento(this);' id='{0}_registro" + "_" + idEsquemaBuscado + "' type='checkbox'/></td>" +
                                "<td style='text-align:center';><input onchange='Frm.CambioDocumento(this);' id='{0}_data" + "_" + idEsquemaBuscado + "' type='checkbox'/></td>" +
                                "<td style='text-align:center';><input onchange='Frm.CambioDocumento(this);' id='{0}_imagen" + "_" + idEsquemaBuscado + "' type='checkbox'/></td>" +
                                "<td style='text-align:center';><input onchange='Frm.CambioDocumento(this);' id='{0}_descargar" + "_" + idEsquemaBuscado + "' type='checkbox'/></td>" +
                                "</tr>";
                    html = ReplaceAll(html, "{0}", id);
                    html = html.replace("{1}", p.Nombre_Documento);
                    $("#Documento_Container").append(html);

                    $("#" + id + "_registro" + "_" + idEsquemaBuscado)[0].checked = p.registro;
                    $("#" + id + "_data" + "_" + idEsquemaBuscado)[0].checked = p.data;
                    $("#" + id + "_imagen" + "_" + idEsquemaBuscado)[0].checked = p.imagen;
                    $("#" + id + "_descargar" + "_" + idEsquemaBuscado)[0].checked = p.descargar;
                });
            },
            CambioDocumento: function (control) {
                //alert(control);
                var id = $(control).attr('id');
                var fila = id.replace("documento_", "").split("_")[0];
                var idEsquema = id.replace("documento_", "").split("_")[1];
                var tipo = id.replace("documento_", "").split("_")[3];
                var idProyecto = id.replace("documento_", "").split("_")[4];
                var indiceEsquema = SearchEsquema(idEsquema);

                eval("Frm.Documento[" + idProyecto + "].Documento[" + fila + "]." + tipo + " = " + $(control)[0].checked + ";");
            },

            OptionClick: function (option) {
                try {

                    if (option == "add") {
                        //New

                        $("#Detalle").show();
                        $("input[type=text]", "#Detalle").val('');
                        Global.SelectTab("tabs", 1, "New");
                        Frm.Documento = [];
                        $("#Documento_Container").html("");
                        Frm.CargarEsquema();
                        //Frm.CargarProyectos();
                    } else {
                        //Save
                        var param = "option=" + option;
                        //param += "&" + ValidPar("id_rol", false);
                        param += "&id_rol=" + Frm.id_rol.val();
                        var parametros = new Array();

                        for (var x = 0; x < Frm.Documento.length; x++) {
                            //alert(Frm.Documento);
                            for (var y = 0; y < Frm.Documento[x].Documento.length; y++) {
                                //alert(Frm.Documento[x].Documento);
                                var documentosList = objToString(Frm.Documento[x].Documento[y]);
                                parametros[parametros.length] = 'fk_Esquema:' + Frm.Documento[x].fk_Esquema + ',fk_Proyecto: ' + Frm.Documento[x].fk_Proyecto + ',esquemaChecked:' + $("#esquema_" + Frm.Documento[x].fk_Esquema + "_proyecto_" + Frm.Documento[x].fk_Proyecto)[0].checked + ',' + documentosList;
                            }
                        }

                        param += "&parametros=" + parametros.join(';');
                        if (option != "remove") {
                            param += "&" + Frm.nombre_rol.ValidPar();

                            //                            var proyectos = $("input", Frm.Proyectos_Container);
                            //                            var numProyectos = 0;
                            //                            $(proyectos).each(function (i, p) { if (p.checked) numProyectos++; });
                            //                            if (numProyectos == 0) throw "Por favor seleccione por lo menos un proyecto para el rol";

                            //                            param += "&" + ArrayParams(proyectos);
                        } else {
                            //param += "&" + ValidPar("id_rol", "Id Rol", true);
                            if (!confirm('¿Está seguro de eliminar el registro?')) return;
                        }
                        AjaxRequest("OptionClick", "", param, function (html) {
                            $('#MainGrid_flex').flexReload();
                            TryExec(html);
                        });
                    }
                }
                catch (e) { alert(e); }
            },

            OcultarDetalle: function () {
                $("#Detalle").hide();
                Global.SelectTab("tabs", 0, "");
            }
        };
    </script>
    <script type="text/javascript">
        Global.EditOptions = <%= EditOptions.GetJson() %>;
    </script>
    <div class="form">
        <div id="tabs" class="tabcontrol">
            <ul>
                <li><a id="tab_filtro" href="#tabs-1">Búsqueda</a></li>
                <li><a id="tab_detalle" href="#tabs-2">Detalle</a></li>
            </ul>
            <div id="tabs-1">
                <div class="tab-panel">
                    <div class="aleft">
                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Rol]" CssClass="Label" Width="400px" Height="14px"></asp:Label>
                    </div>
                    <div>
                        <block:filter ID="MyFilter" runat="server" ClientIDMode="Static" />
                    </div>
                    <miharu:SlygFlexigrid ID="MainGrid" runat="server" Title="Roles" UrlData="../../../controls/proxy_data.aspx" OnRowDblClick="Frm.MainGridClick" CssClass="gridclass" />
                </div>
            </div>
            <div id="tabs-2">
                <div class="tab-panel auto">
                    <div class="block">
                        <table>
                            <tr>
                                <td>
                                    <div class="fleft corner pad10" style="width: 450px" >
                                        <table id="Detalle" class="aleft hidden"  >
                                        <tr>
                                            <td>
                                                Id Rol
                                            </td>
                                            <td>
                                                <input id="id_rol" type="text" readonly="readonly" class="readonly" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Rol
                                            </td>
                                            <td>
                                                <input id="nombre_rol" type="text" maxlength="100" style="width: 200px;" />
                                            </td>
                                        </tr>
                         
                                    </table>
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; ">
                                    <div class="fleft corner pad10"  style="width: 450px" >
                                        <table width="100%">
                                            <thead class="titulo">
                                                <tr>
                                                    <td>
                                                        Esquema
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="Esquema_Container" class="table">
                                            </tbody>
                                        </table>
                                   </div>
                                </td>
                                <td>
                                    <div class="fleft corner pad10" style="width: 600px; min-height: 255px">
                                        <table width="100%">
                                            <thead class="titulo">
                                                <tr>
                                                    <td>
                                                        Documento
                                                    </td>
                                                    <td>
                                                        Ver Registro
                                                    </td>
                                                    <td>
                                                        Ver Data
                                                    </td>
                                                    <td>
                                                        Ver Imagen
                                                    </td>
                                                    <td>
                                                        Descargar
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody id="Documento_Container" class="table">
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        
                       
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
