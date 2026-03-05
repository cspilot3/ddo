<%@ Page Title="" Language="C#" MasterPageFile="~/_Main/Sites.Master" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="Miharu.Explorer._Site.Garantias_Settings.Administracion.Proyecto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    
    <script type="text/javascript" language="javascript">

        function buscar() {
            $.ajax({
                dataType: 'JSON'
                , contentType: 'Application/json; charset=utf-8'
                , type: 'POST'
                , url: 'Proyecto.aspx/getData'
                , data: '{}'
                , success: function (resp) { fillGrid(resp.d); }
                , error: function () { jAlert('Error consultando los proyectos', 'Proyectos'); }
            });
        }

        function fillGrid(data) {
            eval('data = ' + data);
            
            $("#tblFilter").jqGrid("clearGridData", true);
            $("#tblFilter").jqGrid({
                datatype: 'local'
                , colNames: ['fk_Entidad', 'id_Proyecto', 'Nombre Entidad', 'Nombre Proyecto', 'Vencimiento Proyecto', 'Responsable Proyecto', 'Telefono Responsable', 'E-Mail Responsable', 'fk_Folder_Tipo', 'fk_Caja_Defecto']
                , colModel: [{ name: 'fk_Entidad', index: 'fk_Entidad', hidden: true }
                    , { name: 'id_Proyecto', index: 'id_Proyecto', hidden: true }
                    , { name: 'Nombre_Proyecto', index: 'Nombre_Proyecto' }
                    , { name: 'Vencimiento_Proyecto', index: 'Vencimiento_Proyecto' }
                    , { name: 'Responsable_Proyecto', index: 'Responsable_Proyecto' }
                    , { name: 'Telefono_Responsable_Proyecto', index: 'Telefono_Responsable_Proyecto' }
                    , { name: 'Email_Responsable_Proyecto', index: 'Email_Responsable_Proyecto' }
                    , { name: 'fk_Folder_Tipo', index: 'fk_Folder_Tipo', hidden: true }
                    , { name: 'fk_Caja_Defecto', index: 'fk_Caja_Defecto', hidden: true }
                ]
                , autowidth: true
                , rowNum: 10
                , rowList: [10, 20, 30]
                , pager: '#divFilter'
                , caption: 'Proyectos'
            });

            for (var i = 0; i < data.length; i++) { $("#tblFilter").jqGrid("addRowData", i + 1, data[i]); }
        }        

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentTitle" runat="server">
    Proyecto
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentFilter" runat="server">
    <table width="100%">
        <tr>
            <td class="contentText">
                Entidad
            </td>
        </tr>
        <tr>
            <td>
                <select id="ddlEntidad" class="control"></select>
            </td>
        </tr>
        <tr>
            <td class="contentText">
                Proyecto
            </td>
        </tr>
        <tr>
            <td>
                <input id="txtProyecto" type="text" class="control" />
            </td>
        </tr>
        <tr>
            <td class="contentText">
                Responsable Proyecto
            </td>
        </tr>
        <tr>
            <td>
                <input id="txtResponsable" type="text" class="control" />
            </td>
        </tr>
        <tr>
            <td>
                <br/><br/><br/>
                <input id="btnBuscar" type="button" value="Buscar" class="button" onclick="buscar()" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentList" runat="server">
    
    <table id="tblFilter"></table>
    <div id="divFilter"></div>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="contentDetail" runat="server">
</asp:Content>
