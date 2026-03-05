Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.Design

Namespace DocumentViewerObjects

    Public Class DocumentViewerDesigner : Inherits System.Web.UI.Design.ControlDesigner

        Public Overrides Function GetDesignTimeHtml() As String
            Dim objControl As DocumentViewer = CType(Me.Component, DocumentViewer)
            Dim objSw As New StringWriter
            Dim objHTMLTw As New HtmlTextWriter(objSw)

            objSw.Write("<table id='tblBaseDocumentViewer' style='width:" & objControl.Width.Value & "; height:" & objControl.Height.Value & "; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid' cellSpacing='0' cellPadding='0' bgColor='#e0dfe3' border='0'>")
            objSw.Write("   <tr>")
            objSw.Write("		<td>")

            objSw.Write("			<table id='tblToolbarDocumentViewer' style='width:" & objControl.Width.Value & "px; border-right: 0px outset; border-top: 2px outset; border-left: 2px outset; border-bottom: 2px outset' cellSpacing='0' cellPadding='0' align='center' bgColor='#e0dfe3' border='0'>")
            objSw.Write("               <tr>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgImprimir' title='Imprimir' alt='' src='" & objControl.MyIconURL & "Imprimir_Cold.png' border='0'/>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgGuardar' title='Guardar' alt='' src='" & objControl.MyIconURL & "Guardar_Cold.png' border='0'/>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td style='width:30px'></td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgAjustarAncho' title='Ajustar al ancho' alt='' src='" & objControl.MyIconURL & "Ajustar_Ancho_Cold.png' border='0'/>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgAjustarAlto' title='Ajustar al alto' alt='' src='" & objControl.MyIconURL & "Ajustar_Alto_Cold.png' border='0'/>")
            objSw.Write("               	</td>")

            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgRotateleft' title='Rotar la imagen a la izquierda' alt='' src='" & objControl.MyIconURL & "Rotate_left_Cold.png' border='0'/>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgRotateright' title='Rotar la imagen a la derecha' alt='' src='" & objControl.MyIconURL & "Rotate_right_Cold.png' border='0'/>")
            objSw.Write("               	</td>")

            objSw.Write("               	<td align='right' style='width:30px'>")
            objSw.Write("                       <img id='imgZoomOut' title='Zoom Out' alt='' src='" & objControl.MyIconURL & "Zoom_Out_Cold.png' border='0'/>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td align='center' style='width:130px'>")
            objSw.Write("               	    <select id='ddlZoom' style='width:120px'>")
            objSw.Write("               	    	<option value='10'>10%</option>")
            objSw.Write("               	    	<option value='25'>25%</option>")
            objSw.Write("               	    	<option value='50'>50%</option>")
            objSw.Write("               	    	<option value='75'>75%</option>")
            objSw.Write("               	    	<option value='100' selected='selected'>100%</option>")
            objSw.Write("               	    	<option value='150'>150%</option>")
            objSw.Write("               	    	<option value='200'>200%</option>")
            objSw.Write("               	    	<option value='300'>300%</option>")
            objSw.Write("               	    	<option value='400'>400%</option>")
            objSw.Write("               	    	<option value='500'>Ajustar al ancho</option>")
            objSw.Write("               	    	<option value='600'>Ajustar al alto</option>")
            objSw.Write("               	    </select>")
            objSw.Write("                   </td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgZoomIn' title='Zoom In' alt='' src='" & objControl.MyIconURL & "Zoom_In_Cold.png' border='0'/>")
            objSw.Write("                   </td>")
            objSw.Write("               	<td width='" & (objControl.Width.Value - 580) & "px'></td>")
            objSw.Write("               	<td align='right' style='width:30px'>")
            objSw.Write("                       <img id='imgEndleft' title='Primera pagina' alt='' src='" & objControl.MyIconURL & "Endleft_Cold.png' border='0'/>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td align='right' style='width:30px'>")
            objSw.Write("                       <img id='imgNextleft' title='Pagina anterior' alt='' src='" & objControl.MyIconURL & "Nextleft_Cold.png' border='0'/>")
            objSw.Write("                   </td>")
            objSw.Write("               	<td align='center' style='width:70px'>")
            objSw.Write("               	    <select id='ddlPagina' style='width:60px'>")
            objSw.Write("               	    	<option value='1' selected>1</option>")
            objSw.Write("               	    	<option value='2'>2</option>")
            objSw.Write("               	    	<option value='3'>3</option>")
            objSw.Write("               	    	<option value='4'>4</option>")
            objSw.Write("               	    </select>")
            objSw.Write("               	</td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgNextright' title='Pagina siguiente' alt='' src='" & objControl.MyIconURL & "Nextright_Cold.png' border='0'/>")
            objSw.Write("                   </td>")
            objSw.Write("               	<td style='width:30px'>")
            objSw.Write("                       <img id='imgEndright' title='Ultima pagina' alt='' src='" & objControl.MyIconURL & "Endright_Cold.png' border='0'/>")
            objSw.Write("                   </td>")
            objSw.Write("               </tr>")
            objSw.Write("           </table>")

            objSw.Write("       </td>")
            objSw.Write("   </tr>")
            objSw.Write("   <tr>")
            objSw.Write("       <td style='height:5'></td>")
            objSw.Write("   </tr>")
            objSw.Write("   <tr>")
            objSw.Write("       <td vAlign='middle' align='center'>")

            If objControl.Scrolling Then
                objSw.Write("           <div style='overflow: auto; width:" & objControl.Width.Value & "px; height:" & (objControl.Height.Value - 35) & "px'>")
            Else
                objSw.Write("           <div style='width:" & objControl.Width.Value & "px; height:" & (objControl.Height.Value - 35) & "px'>")
            End If

            objSw.Write("               <img id = 'imgBase' alt='' src='" & objControl.MyIconURL & "DefaultImage.jpg'/>")

            objSw.Write("           </div>")
            objSw.Write("       </td>")
            objSw.Write("   </tr>")
            objSw.Write("</table>")

            Return objSw.ToString()

        End Function

    End Class

End Namespace