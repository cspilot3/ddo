<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterForm.Master" AutoEventWireup="true"
    CodeBehind="OficinaB.aspx.cs" Inherits="WebPunteoElectronico.Site.OficinasB.OficinaB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <style type="text/css">
        P
        {
            margin-top: 0px;
            margin-bottom: 12px;
            color: #000000;
            font-family: Tahoma;
        }
        PRE
        {
            border-right: #f0f0e0 1px solid;
            padding-right: 5px;
            border-top: #f0f0e0 1px solid;
            margin-top: -5px;
            padding-left: 5px;
            font-size: x-small;
            padding-bottom: 5px;
            border-left: #f0f0e0 1px solid;
            padding-top: 5px;
            border-bottom: #f0f0e0 1px solid;
            font-family: Courier New;
            background-color: #e5e5cc;
        }
        TD
        {
            font-size: 12px;
            color: #000000;
            font-family: Tahoma;
        }
        H2
        {
            border-top: #003366 1px solid;
            margin-top: 25px;
            font-weight: bold;
            font-size: 1.5em;
            margin-bottom: 10px;
            margin-left: -15px;
            color: #003366;
        }
        H3
        {
            margin-top: 10px;
            font-size: 1.1em;
            margin-bottom: 10px;
            margin-left: -15px;
            color: #000000;
        }
        UL
        {
            margin-top: 10px;
            margin-left: 20px;
        }
        OL
        {
            margin-top: 10px;
            margin-left: 20px;
        }
        LI
        {
            margin-top: 10px;
            color: #000000;
        }
        FONT.value
        {
            font-weight: bold;
            color: darkblue;
        }
        FONT.key
        {
            font-weight: bold;
            color: darkgreen;
        }
        .divTag
        {
            border: 1px;
            border-style: solid;
            background-color: #FFFFFF;
            text-decoration: none;
            height: auto;
            width: auto;
            background-color: #cecece;
        }
        .BannerColumn
        {
            background-color: #000000;
        }
        .Banner
        {
            border: 0;
            padding: 0;
            height: 8px;
            margin-top: 0px;
            color: #ffffff;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#1c5280',EndColorStr='#FFFFFF');
        }
        .BannerTextCompany
        {
            font: bold;
            font-size: 18pt;
            color: #cecece;
            font-family: Tahoma;
            height: 0px;
            margin-top: 0;
            margin-left: 8px;
            margin-bottom: 0;
            padding: 0px;
            white-space: nowrap;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .BannerTextApplication
        {
            font: bold;
            font-size: 18pt;
            font-family: Tahoma;
            height: 0px;
            margin-top: 0;
            margin-left: 8px;
            margin-bottom: 0;
            padding: 0px;
            white-space: nowrap;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .BannerText
        {
            font: bold;
            font-size: 18pt;
            font-family: Tahoma;
            height: 0px;
            margin-top: 0;
            margin-left: 8px;
            margin-bottom: 0;
            padding: 0px;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .BannerSubhead
        {
            border: 0;
            padding: 0;
            height: 16px;
            margin-top: 0px;
            margin-left: 10px;
            color: #ffffff;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#4B3E1A',EndColorStr='#FFFFFF');
        }
        .BannerSubheadText
        {
            font: bold;
            height: 11px;
            font-size: 11px;
            font-family: Tahoma;
            margin-top: 1;
            margin-left: 10;
            filter: progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');
        }
        .FooterRule
        {
            border: 0;
            padding: 0;
            height: 1px;
            margin: 0px;
            color: #ffffff;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#4B3E1A',EndColorStr='#FFFFFF');
        }
        .FooterText
        {
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
            font-family: Tahoma;
            margin-top: 10;
            margin-left: 0px;
            margin-bottom: 2;
            padding: 0px;
            color: #999999;
            white-space: nowrap;
        }
        .FooterText A:link
        {
            font-weight: normal;
            color: #999999;
            text-decoration: underline;
        }
        .FooterText A:visited
        {
            font-weight: normal;
            color: #999999;
            text-decoration: underline;
        }
        .FooterText A:active
        {
            font-weight: normal;
            color: #999999;
            text-decoration: underline;
        }
        .FooterText A:hover
        {
            font-weight: normal;
            color: #FF6600;
            text-decoration: underline;
        }
        .ClickOnceInfoText
        {
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
            font-family: Tahoma;
            margin-top: 0;
            margin-right: 2px;
            margin-bottom: 0;
            padding: 0px;
            color: #000000;
        }
        .InstallTextStyle
        {
            font: bold;
            font-size: 14pt;
            font-family: Tahoma;
            a: #FF0000;
            text-decoration: None;
        }
        .DetailsStyle
        {
            margin-left: 30px;
        }
        .ItemStyle
        {
            margin-left: -15px;
            font-weight: bold;
        }
        .StartColorStr
        {
            background-color: #4B3E1A;
        }
        .JustThisApp A:link
        {
            font-weight: normal;
            color: #000066;
            text-decoration: underline;
        }
        .JustThisApp A:visited
        {
            font-weight: normal;
            color: #000066;
            text-decoration: underline;
        }
        .JustThisApp A:active
        {
            font-weight: normal;
            text-decoration: underline;
        }
        .JustThisApp A:hover
        {
            font-weight: normal;
            color: #FF6600;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <table width="900px" cellpadding="0" cellspacing="2" border="0">
        <!-- Begin Banner -->
        <tr>
            <td>
                <table cellpadding="2" cellspacing="0" border="0" style="background-color: #cecece"
                    width="100%">
                    <tr>
                        <td>
                            <table style="background-color: #009900" width="100%" cellpadding="0" cellspacing="0"
                                border="0">
                                <tr>
                                    <td class="Banner" />
                                </tr>
                                <tr>
                                    <td class="Banner">
                                        <span class="BannerTextCompany">Procesos y Canje S.A.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Banner">
                                        <span class="BannerTextApplication">BancoAgrario.OficinasTipoB</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Banner" align="right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!-- End Banner -->
        <!-- Begin Dialog -->
        <tr>
            <td align="left">
                <table cellpadding="2" cellspacing="0" border="0" width="540">
                    <tr>
                        <td style="width: 496px">
                            <!-- Begin AppInfo -->
                            <table>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Name:</b>
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        BancoAgrario.OficinasTipoB
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Publisher:</b>
                                    </td>
                                    <td class="cs10">
                                    </td>
                                    <td>
                                        Procesos & Canje S.A.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!-- End AppInfo -->
                            <!-- Begin Prerequisites -->
                            <table id="BootstrapperSection" border="0">
                                <tr>
                                    <td colspan="2">
                                        Los siguientes prerequisitos son necesarios:
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cs10">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <ul>
                                            <li>Windows Installer 3.1</li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!-- End Prerequisites -->
                            <!-- Begin Buttons -->
                            <table cellpadding="2" cellspacing="0" border="0" width="540" style="cursor: hand"
                                onclick="window.navigate(InstallButton.href)">
                                <tr>
                                    <td align="left">
                                        <table cellpadding="1" style="background-color: #333333" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <table cellpadding="1" style="background-color: #cecece" cellspacing="0" border="0">
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="1" style="background-color: #efefef" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td class="cs20">
                                                                        </td>
                                                                        <td>
                                                                            <a id="InstallButton" href="<%= ResolveClientUrl(this.NavigationUrl) %>">Iniciar</a>
                                                                        </td>
                                                                        <td class="cs20">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <!-- End Buttons -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <!-- End Dialog -->
        <!-- Spacer Row -->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <!-- Begin Footer -->
                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color: #ffffff">
                    <tr>
                        <td class="cs5">
                        </td>
                    </tr>
                    <tr>
                        <td class="FooterText" align="center">
                            <a href="http://go.microsoft.com/fwlink/?LinkId=154571">Recursos de ClickOnce y .NET
                                Framework </a>
                        </td>
                    </tr>
                    <tr>
                        <td class="cs5">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #cecece; height: 1px">
                        </td>
                    </tr>
                </table>
                <!-- End Footer -->
            </td>
        </tr>
    </table>
    <%--<iframe id="FormIFrame" scrolling="yes" marginheight="0" marginwidth="0" frameborder="0"
        width="900px" height="450px" style="background-color: White;" src="<%= ResolveClientUrl(this.NavigationUrl) %>">
    </iframe>--%>
</asp:Content>
