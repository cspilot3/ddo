using DBAgrario.SchemaProcess;
using System.Collections.Generic;

public class ConsultaCamposOcultos : List<string>
{
    public ConsultaCamposOcultos()
    {
        AddRange(new string[]  {
                                CTA_Consulta_Proceso_DetalleEnum.fk_Proceso.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.fk_Documento.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Codigo_Causal.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Nombre_Causal.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Uno.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Dos.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Tres.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Cuatro.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Cinco.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Seis.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Siete.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Ocho.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Nueve.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Campo_Diez.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Cheque_Local.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Cheque_Propio.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Cheque_Gerencia.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Remesa_Negociada.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Remesa_al_Cobro.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Nota_Debito.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.MP_Nota_Credito.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Efectivo.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.TotalReg.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.RowId.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Numero_Cheque_Gerencia.ColumnName,
                                CTA_Consulta_Proceso_DetalleEnum.Usuario.ColumnName, 
                                CTA_Consulta_Proceso_DetalleEnum.Comision.ColumnName
                                });
    }
}
