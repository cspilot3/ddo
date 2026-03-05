using System.Web;
using System.Web.SessionState;
using DBAgrario.SchemaConfig;
using DBAgrario.SchemaCore;

public class TransaccionDataCache
{
    private static HttpSessionState Session
    {
        get { return HttpContext.Current.Session; }
    }

    public static CTA_Regional_COB_Oficina_ConcatenacionDataTable OficinaData
    {
        get
        {
            if (Session["_OficinaData"] == null)
                Session["_OficinaData"] = new CTA_Regional_COB_Oficina_ConcatenacionDataTable();

            return (CTA_Regional_COB_Oficina_ConcatenacionDataTable)Session["_OficinaData"];
        }
        set
        {
            Session["_OficinaData"] = value;
        }
    }

    public static CTA_Documento_ConcatenacionDataTable DocumentoData
    {
        get
        {
            if (Session["_DocumentoData"] == null)
                Session["_DocumentoData"] = new CTA_Documento_ConcatenacionDataTable();

            return (CTA_Documento_ConcatenacionDataTable)Session["_DocumentoData"];
        }
        set
        {
            Session["_DocumentoData"] = value;
        }
    }

    public static CTA_Consulta_Config_CampoDataTable CampoData
    {
        get
        {
            if (Session["_CampoData"] == null)
                Session["_CampoData"] = new CTA_Consulta_Config_CampoDataTable();

            return (CTA_Consulta_Config_CampoDataTable)Session["_CampoData"];
        }
        set
        {
            Session["_CampoData"] = value;
        }
    }

    public static CTA_Config_Campo_ListaDataTable ListaItemsData
    {
        get
        {
            if (Session["_ListaItemsData"] == null)
                Session["_ListaItemsData"] = new CTA_Config_Campo_ListaDataTable();

            return (CTA_Config_Campo_ListaDataTable)Session["_ListaItemsData"];
        }
        set
        {
            Session["_ListaItemsData"] = value;
        }
    }

    public static TBL_RegionalDataTable RegionalData
    {
        get
        {
            if (Session["_RegionalData"] == null)
                Session["_RegionalData"] = new TBL_RegionalDataTable();

            return (TBL_RegionalDataTable)Session["_RegionalData"];
        }
        set
        {
            Session["_RegionalData"] = value;
        }
    }

    public static CTA_Regional_COB_ConcatenacionDataTable COBData
    {
        get
        {
            if (Session["_COBData"] == null)
                Session["_COBData"] = new CTA_Regional_COB_ConcatenacionDataTable();

            return (CTA_Regional_COB_ConcatenacionDataTable)Session["_COBData"];
        }
        set
        {
            Session["_COBData"] = value;
        }
    }

    public static void UpdateCacheRegional(TBL_RegionalDataTable nRegionalDataTable)
    {
        //SyncLock RegionalData
        RegionalData.Rows.Clear();
        RegionalData.Merge(nRegionalDataTable);
        //End SyncLock
    }

    public static void UpdateCacheCOB(CTA_Regional_COB_ConcatenacionDataTable nCOBDataTable)
    {
        //SyncLock COBData
        COBData.Rows.Clear();
        COBData.Merge(nCOBDataTable);
        //End SyncLock
    }

    public static void UpdateCacheOficina(CTA_Regional_COB_Oficina_ConcatenacionDataTable nOficinaDataTable)
    {
        //SyncLock _OficinaData
        OficinaData.Rows.Clear();
        OficinaData.Merge(nOficinaDataTable);
        //End SyncLock
    }

    public static void UpdateCacheDocumento(CTA_Documento_ConcatenacionDataTable nDocumentoDataTable)
    {
        //SyncLock _DocumentoData
        DocumentoData.Rows.Clear();
        DocumentoData.Merge(nDocumentoDataTable);
        //End SyncLock
    }

    public static void UpdateCacheCampos(CTA_Consulta_Config_CampoDataTable nCampoDataTable)
    {
        //SyncLock _CampoData
        CampoData.Rows.Clear();
        CampoData.Merge(nCampoDataTable);
        //End SyncLock
    }

    public static void UpdateCacheListaItems(CTA_Config_Campo_ListaDataTable nListaDataTable)
    {
        //SyncLock _ListaItemsData
        ListaItemsData.Rows.Clear();
        ListaItemsData.Merge(nListaDataTable);
        //End SyncLock
    }

    public static CTA_Regional_COB_Oficina_ConcatenacionType FindOficina(string nNombreOficina)
    {
        var rows = OficinaData.Select(TBL_OficinaEnum.Nombre_Oficina.ColumnName + " = \'" + nNombreOficina + "\'");

        if (rows != null && rows.Length > 0)
            return ((CTA_Regional_COB_Oficina_ConcatenacionRow)rows[0]).ToCTA_Regional_COB_Oficina_ConcatenacionType();

        return null;
    }

    public static CTA_Documento_ConcatenacionType FindDocumento(string nNombreDocumento)
    {
        var rows = DocumentoData.Select(CTA_Documento_ConcatenacionEnum.Nombre_Documento.ColumnName + " = '" + nNombreDocumento + "'");

        if (rows != null && rows.Length > 0)
            return ((CTA_Documento_ConcatenacionRow)rows[0]).ToCTA_Documento_ConcatenacionType();

        return null;
    }

    public static CTA_Consulta_Config_CampoRow[] FindCampos(CTA_Documento_ConcatenacionType nDocumento)
    {
        return (CTA_Consulta_Config_CampoRow[])CampoData.Select(CTA_Consulta_Config_CampoEnum.fk_Documento.ColumnName + " = " + nDocumento.id_Documento.ToString());
    }

    public static CTA_Config_Campo_ListaRow[] FindListaItems(int nListaId)
    {
        return (CTA_Config_Campo_ListaRow[])CampoData.Select(CTA_Config_Campo_ListaEnum.fk_Campo_Lista.ColumnName + " = " + nListaId.ToString());
    }

    public static object FindCampo(int nDocumentoId, string nColumnName)
    {
        var campo = (CTA_Consulta_Config_CampoRow[])CampoData.Select(CTA_Consulta_Config_CampoEnum.fk_Documento.ColumnName + "=" + nDocumentoId.ToString() + " AND " + CTA_Consulta_Config_CampoEnum.Nombre_Columna.ColumnName + " = '" + nColumnName + "'");

        if (campo.Length > 0)
            return campo[0].Nombre_Campo;
        else
            return nColumnName;
    }

    public static TBL_RegionalType FindRegional(string nNombreRegional)
    {
        var rows = RegionalData.Select(TBL_RegionalEnum.Nombre_Regional.ColumnName + " = '" + nNombreRegional + "'");

        if (rows != null && rows.Length > 0)
            return ((TBL_RegionalRow)rows[0]).ToTBL_RegionalType();

        return null;
    }

    public static CTA_Regional_COB_ConcatenacionType FindCOB(string nNombreCOB)
    {
        var rows = COBData.Select(TBL_COBEnum.Nombre_COB.ColumnName + " = '" + nNombreCOB + "'");

        if (rows != null && rows.Length > 0)
            return ((CTA_Regional_COB_ConcatenacionRow)rows[0]).ToCTA_Regional_COB_ConcatenacionType();

        return null;
    }
}
