<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="Miharu.Client.Browser.site.administracion.seguridad.usuarios"
    MasterPageFile="~/master/master_form.Master" %>

<%@ Register Src="~/controls/filter.ascx" TagName="filter" TagPrefix="block" %>
<%@ Register TagPrefix="miharu" Namespace="Miharu.Client.Browser.code.Grid" Assembly="Miharu.Client.Browser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=ResolveClientUrl("~/scripts/jquery/js/flexigrid.custom.js")%>" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/styles/flexigrid-1.1/flexigrid.css")%>" rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        .gridclass
        {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            top: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" runat="Server" ContentPlaceHolderID="body">
    <script type="text/javascript">
        Frm = {
            CurrentPage: "usuarios.aspx",
            Entidad: [],
            Esquema: [],

            Perfiles: [],
            Roles: [],

            Init: function () {
                Global.InitConfigHandlers("tabs", Frm.OptionClick);
                Global.InitFilter(Frm.FilterClick);

                Frm.nombre_entidad.autoList(Frm.Entidad, function () { Frm.CargarEsquemas(true); });
                Frm.nombre_esquema_seguridad.autoList(Frm.Esquema);
                Frm.cambiar_password.click(Frm.HabilitarCambioPassword);
                Frm.id_usuario[0].disabled = true;
                Frm.ConfirmarPassword_Container.hide();

                Frm.cambiar_password[0].checked = true;
                Frm.password_usuario.prop("disabled", false);
                Frm.cambiar_password.prop("disabled", true);
                Frm.nombre_entidad.prop("disabled", true);
                
            },

            HabilitarCambioPassword: function () {
                if (Frm.cambiar_password[0].checked) {
                    Frm.password_usuario.val("");
                    Frm.password_usuario_confirm.val("");
                    Frm.password_usuario.prop("disabled", false);
                    Frm.ConfirmarPassword_Container.show();
                } else {
                    Frm.password_usuario.val(new Date());
                    Frm.password_usuario.prop("disabled", true);
                    Frm.ConfirmarPassword_Container.hide();
                }
            },

            FilterClick: function (filter) {
                var param = "Filtro=" + filter;
                AjaxRequest("CambiarFiltro", "", param, function () {
                    $('#MainGrid_flex').flexReload();
                });
                //alert(GetAtt(e.target, 'filter'));
            },

            MainGridClick: function (value, row, cells, evt, grid) {
                $("#Detalle").show();
                Frm.id_usuario.val(GetRowValue(cells, 'id_Usuario'));
                Frm.login_usuario.val(GetRowValue(cells, 'Login_Usuario'));
                Frm.password_usuario.val(new Date());
                Frm.password_usuario.prop("disabled", true);
                Frm.cambiar_password.prop("disabled", false);
                Frm.cambiar_password[0].checked = false;
                Frm.ConfirmarPassword_Container.hide();
                Frm.nombres_usuario.val(GetRowValue(cells, 'Nombres_Usuario'));
                Frm.apellidos_usuario.val(GetRowValue(cells, 'Apellidos_Usuario'));
                Frm.identificacion_usuario.val(GetRowValue(cells, 'Identificacion_Usuario'));
                Frm.email_usuario.val(GetRowValue(cells, 'Email_Usuario').replace(" ", ""));
                Frm.usuario_activo[0].checked = (GetRowValue(cells, 'Usuario_Activo') == "true");
                SetAutoListValue("nombre_entidad", GetRowValue(cells, 'Nombre_Entidad'));
                Frm.nombre_esquema_seguridad.val(GetRowValue(cells, 'Nombre_Esquema_Seguridad'));
                Frm.solicitar_cambio_password[0].checked = (cells.solicitar_cambio_password == "1");

                Global.SelectTab("tabs", 1, "Edit");
                Frm.CargarEsquemas(true);

                Frm.CargarPerfiles();
                Frm.CargarRoles();
            },

            CargarEsquemas: function (p) {
                try {
                    SetAutoListDataSource("nombre_esquema_seguridad", [], p);
                    var param = Frm.nombre_entidad.ValidPar();
                    AjaxRequest("CargarEsquemas", "", param, function (html) {
                        TryExec(html);
                        SetAutoListDataSource("nombre_esquema_seguridad", Frm.Esquema, p);
                    });
                } catch (e) { alert(e); }
            },

            CargarPerfiles: function () {
                try {
                    $("#Perfiles_Container").html("");
                    var param = Par("id_usuario");
                    AjaxRequest("CargarPerfiles", "", param, function (html) {
                        TryExec(html);

                        $(Frm.Perfiles).each(function (i, p) {
                            var id = "perfil_" + p.id_Perfil;
                            var html = "<tr><td><input id='{0}' type='checkbox'/><label for='{0}'>{1}</label> </td></tr>";
                            html = ReplaceAll(html, "{0}", id);
                            html = html.replace("{1}", p.Nombre_Perfil);
                            $("#Perfiles_Container").append(html);

                            $("#" + id)[0].checked = p.asignado;
                        });
                    });
                } catch (e) { alert(e); }
            },

            CargarRoles: function () {
                try {
                    $("#Roles_Container").html("");
                    var param = Par("id_usuario");
                    AjaxRequest("CargarRoles", "", param, function (html) {
                        TryExec(html);

                        $(Frm.Roles).each(function (i, p) {
                            var id = "rol_" + p.id_Rol;
                            var html = "<tr><td><input id='{0}' type='checkbox'/><label for='{0}'>{1}</label> </td></tr>";
                            html = ReplaceAll(html, "{0}", id);
                            html = html.replace("{1}", p.Nombre_Rol);
                            $("#Roles_Container").append(html);

                            $("#" + id)[0].checked = (p.asignado == 1);
                        });
                    });
                } catch (e) { alert(e); }
            },

            ValidarLogin: function () {
                if (Frm.Entidad.val() != "") {
                    var param = Frm.Entidad.ValidPar();
                    param += "&" + Par("Login_Usuario");
                    AjaxRequest("Login_Usuario", "", param, function (html) {
                        TryExec(html);
                    });
                } else { alert("Ya existe el usuario."); }
            },

            OptionClick: function (option) {
                try {
                    if (option == "add") {
                        //New

                        $("#Detalle").show();
                        $("input[type=text]", "#Detalle").val('');
                        Global.SelectTab("tabs", 1, "New");
                        Frm.password_usuario.prop("disabled", false);
                        Frm.cambiar_password[0].checked = true;
                        Frm.cambiar_password.prop("disabled", true);
                        Frm.password_usuario.val("");
                        Frm.password_usuario_confirm.val("");
                        Frm.ConfirmarPassword_Container.show();
                        SetAutoListValue("nombre_entidad", "Ripley");
                        SetAutoListDataSource("nombre_esquema_seguridad", [], false);
                        Frm.CargarPerfiles();
                        Frm.CargarRoles();
                        Frm.CargarEsquemas(false);
                    } else {
                        //Save
                        var param = "option=" + option;
                        param += "&" + Par("id_usuario");
                        if (option != "remove") {
                            param += "&" + ValidPar("login_usuario", "Login Usuario");
                            param += "&" + ValidPar("cambiar_password");
                            if (Frm.cambiar_password[0].checked) {
                                param += "&" + ValidPar("password_usuario", "Password");
                                if (Frm.password_usuario.val() != Frm.password_usuario_confirm.val())
                                    throw "Las contraseñas no coincides, vuelva a escribirlas";
                            } else
                                param += "&password_usuario=";

                            param += "&" + ValidPar("nombres_usuario", "Nombres Usuario");
                            param += "&" + ValidPar("apellidos_usuario", "Apellidos Usuario");
                            param += "&" + ValidPar("identificacion_usuario", "Identificación Usuario");
                            param += "&" + ValidPar("usuario_activo", "Activo");
                            param += "&" + ValidPar("nombre_entidad", "Constructora");
                            param += "&" + ValidPar("nombre_esquema_seguridad", "Esquema de Seguridad");
                            param += "&" + ValidPar("solicitar_cambio_password", "Solicitar Cambio de Contraseña");
                            param += "&" + ValidPar("email_usuario", "E-Mail");
                            IsEmail("email_usuario");

                            var cant = 0;
                            var perfiles = $("input", Frm.Perfiles_Container);
                            $(perfiles).each(function (i, p) { if (p.checked) cant++; });
                            if (cant == 0) throw "Por favor seleccione por lo menos un perfil para el usuario";

                            cant = 0;
                            var roles = $("input", Frm.Roles_Container);
                            $(roles).each(function (i, p) { if (p.checked) cant++; });
                            if (cant == 0) throw "Por favor seleccione por lo menos un rol para el usuario";

                            param += "&" + ArrayParams(perfiles);
                            param += "&" + ArrayParams(roles);
                        }
                        else {
                            //                            param += "&" + ValidPar("id_usuario", "Id usuario", true);
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
        }
    </script>
    <script type="text/javascript">
        Global.EditOptions = <%= EditOptions.GetJson() %>;
        Frm.Entidad = <%= Entidad.GetJson() %>;
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
                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro: [Login Usuario]" CssClass="Label"
                            Width="400px" Height="14px"></asp:Label>
                    </div>
                    <div>
                        <block:filter ID="MyFilter" runat="server" ClientIDMode="Static" />
                    </div>
                    <miharu:SlygFlexigrid ID="MainGrid" runat="server" Title="Usuarios" UrlData="../../../controls/proxy_data.aspx"
                        OnRowDblClick="Frm.MainGridClick" CssClass="gridclass" />
                </div>
            </div>
            <div id="tabs-2">
                <div class="tab-panel auto">
                    <div class="block">
                        <div class="fleft corner pad10" style="width: 450px">
                            <table id="Detalle" class="aleft hidden">
                                <tr>
                                    <td>
                                        Id Usuario
                                    </td>
                                    <td>
                                        <input id="id_usuario" type="text" readonly="readonly" class="readonly" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Login Usuario
                                    </td>
                                    <td>
                                        <input id="login_usuario" type="text" maxlength="20" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr title="Cambiar contraseña">
                                                <td>
                                                    <input id="password_usuario" type="password" maxlength="200" style="width: 200px;" />
                                                </td>
                                                <td>
                                                    <input type="checkbox" id="cambiar_password" />
                                                    <label for="cambiar_password">
                                                        Cambiar</label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="ConfirmarPassword_Container">
                                    <td>
                                        Confirmar Password
                                    </td>
                                    <td>
                                        <input id="password_usuario_confirm" type="password" maxlength="200" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nombres Usuario
                                    </td>
                                    <td>
                                        <input id="nombres_usuario" type="text" maxlength="50" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Apellidos Usuario
                                    </td>
                                    <td>
                                        <input id="apellidos_usuario" type="text" maxlength="50" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Identificación Usuario
                                    </td>
                                    <td>
                                        <input id="identificacion_usuario" type="text" maxlength="30" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        E-Mail
                                    </td>
                                    <td>
                                        <input id="email_usuario" type="text" maxlength="100" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="usuario_activo">
                                            Usuario Activo</label>
                                    </td>
                                    <td>
                                        <input type="checkbox" id="usuario_activo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Entidad
                                    </td>
                                    <td>
                                        <input id="nombre_entidad" type="text"  style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Esquema de Seguridad
                                    </td>
                                    <td>
                                        <input id="nombre_esquema_seguridad" type="text" style="width: 200px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="solicitar_cambio_password">
                                            Solicitar Cambio de Password</label>
                                    </td>
                                    <td>
                                        <input id="solicitar_cambio_password" type="checkbox" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="fleft corner pad10" style="width: 300px; min-height: 255px">
                            <table width="100%">
                                <thead class="titulo">
                                    <tr>
                                        <td>
                                            Perfiles
                                        </td>
                                    </tr>
                                </thead>
                                <tbody id="Perfiles_Container" class="table">
                                </tbody>
                            </table>
                        </div>
                        <div class="fleft corner pad10" style="width: 300px; min-height: 255px">
                            <table width="100%">
                                <thead class="titulo">
                                    <tr>
                                        <td>
                                            Roles
                                        </td>
                                    </tr>
                                </thead>
                                <tbody id="Roles_Container" class="table">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
