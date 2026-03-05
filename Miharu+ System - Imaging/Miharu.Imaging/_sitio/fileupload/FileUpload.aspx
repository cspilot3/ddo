<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FileUpload.aspx.vb" Inherits="Miharu.Imaging.FileUpload" Async="True" EnableViewState="True" ViewStateMode="Enabled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="height:100px; width:300px;" >
    <div>
    
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <br />
        <asp:Button ID="BtnSubir" runat="server" Text="Subir Archivo" />
        <br />
        <br />
        <asp:Label ID="LblInfo" runat="server" Text="." Font-Bold="True" 
            Font-Names="Calibri"></asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
