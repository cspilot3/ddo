<%@ Page Title="Punteo Electrónico" Language="C#" MasterPageFile="~/Master/MasterPage.master"
    AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebPunteoElectronico.Main" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContentPlaceHolder">
    <script type="text/javascript">
        function ResizeChild() {
            ResizeFrame();
        }

        $(function () {
            ResizeFrame();
        });

        $(window).resize(function () { ResizeFrame(); });

        function SubformLoaded() {
            try { document.getElementById('LoadingDiv').style.display = 'none'; } catch (ex) { }
            try { document.getElementById('FormIFrame').style.display = 'inline'; } catch (ex) { }
        }

        function ResizeFrame() {
            var Alto = ($(window).height() - 20);
            var Ancho = ($(window).width() - 20);

            $("#FormIFrame").css("height", (Alto - 122) + "px");
            $("#FormIFrame").css("width", (Ancho - 17) + "px");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="BodyContentPlaceHolder">
    <div id='LoadingDiv' class="Loading_div">
        <img class="ProcessImg" src="<%= ResolveClientUrl("~/Images/basic/ajax-loader.gif") %>"
            alt="" ondblclick="SubformLoaded();" />
    </div>
    <iframe id="FormIFrame" scrolling="yes" marginheight="0" marginwidth="0" frameborder="0"
        style="background-color:White; display: none;" src="<%= ResolveClientUrl(MiharuSession.Pagina.PageDir) %>">
    </iframe>
</asp:Content>
