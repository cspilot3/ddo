<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="WebSantander.site.application.about" MasterPageFile="~/master/master_form.Master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="form">
        <img src="<%=ResolveClientUrl("~/images/basic/sol_soluciones.png")%>" alt="" class="sol_soluciones" />
        <div class="corner" style="margin-left: auto; margin-right: auto; margin-top: 30px; width: 800px; height: 400px; text-align: center;">
            <br />
            <br />
            <img src="<%=ResolveClientUrl("~/images/basic/logoPyC.png")%>" alt="imagen no disponible" />
            <br />
            <br />
            <div style="padding: 20px; margin-left: 100px; margin-right: 100px;">
                <h1>
                    MIHARU Client Browser</h1>
                <asp:Label ID="VersionLabel" runat="server" Text=""></asp:Label>
                <h3>
                    Visualización de informes y consulta de data del Cliente
                </h3>
                <br />
                <br />
                <br />
                <br />
                <h4>
                    Procesos &amp; Canje S.A.</h4>
            </div>
        </div>
    </div>
</asp:Content>
