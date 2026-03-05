<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="filter.ascx.cs" Inherits="Websantander.controls.filter" %>
<table>
    <tr>
        <td>
            <span id="NoneButton" class="button filter-button" filter="">Ninguno</span>
        </td>
        <td>
            <span id="AllButton" class="button filter-button" filter="*">Todos</span>
        </td>
        <td>
            <span id="ADButton" class="button filter-button" filter="[A-D]*">A-D</span>
        </td>
        <td>
            <span id="EHButton" class="button filter-button" filter="[E-H]*">E-H</span>
        </td>
        <td>
            <span id="ILButton" class="button filter-button" filter="[I-L]*">I-L</span>
        </td>
        <td>
            <span id="MPButton" class="button filter-button" filter="[M-P]*">M-P</span>
        </td>
        <td>
            <span id="QTButton" class="button filter-button" filter="[Q-T]*">R-T</span>
        </td>
        <td>
            <span id="UZButton" class="button filter-button" filter="[U-Z]*">U-Z</span>
        </td>
    </tr>
</table>
