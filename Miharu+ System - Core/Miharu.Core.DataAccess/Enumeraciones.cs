namespace DBCore
{
    /// <summary>
    /// Lista de los posibles estado por los que puede pasar un documento (File) en el Core
    /// </summary>
    public enum EstadoEnum : short
    {
        /// <summary>
        /// Ningun estado
        /// </summary>
        None = -1,

        ///<summary>
        ///Registros faltantes
        ///</summary>
        Faltante = 0,

        ///<summary>
        ///
        ///</summary>
        Rechazado = 1,

        ///<summary>
        ///Pendiente devolver al usuario  
        ///</summary>
        Reproceso = 2,


        ///<summary>
        ///Marcado como eliminado
        ///</summary>
        Eliminado = 3,


        ///<summary>
        ///Se estan ingresando los datos pero aun no estan completos, Ej: Loan Factory  
        ///</summary>
        Precargado = 7,

        ///<summary>
        ///Registros cargados a partir de un archivo plano  
        ///</summary>
        Cargado = 10,

        ///<summary>
        ///Creación de la Orden de trabajo  
        ///</summary>
        Creado = 15,

        ///<summary>
        ///Registro a los que se les ha hecho el proceso de destape  
        ///</summary>
        Destapado = 20,

        ///<summary>
        ///
        ///</summary>
        Mesa_de_Control = 30,

        ///<summary>
        ///Indexación
        ///</summary>
        Indexacion = 31,

        ///<summary>
        ///Pre captura
        ///</summary>
        Pre_Captura = 32,

        ///<summary>
        ///Primera captura
        ///</summary>
        Captura = 33,

        ///<summary>
        ///Segunda captura
        ///</summary>
        Segunda_Captura = 34,

        ///<summary>
        ///Trcera captura
        ///</summary>
        Tercera_Captura = 35,

        ///<summary>
        ///Proceso de calidad
        ///</summary>
        Calidad_Captura = 36,

        ///<summary>
        ///Proceso de calidad
        ///</summary>
        Recorte = 37,

        ///<summary>
        ///Pendiente por publicación  
        ///</summary>
        Indexado = 38,

        ///<summary>
        ///Pendiente por publicación  
        ///</summary>
        Calidad_Recorte = 39,

        ///<summary>
        ///
        ///</summary>
        Empaque = 40,

        ///<summary>
        ///
        ///</summary>
        Empacado = 45,

        ///<summary>
        ///Estado para el sistema de OCR Indexación
        ///</summary>
        OCR_Indexacion = 47,

        ///<summary>
        ///Estado para el sistema de OCR Captura
        ///</summary>
        OCR_Captura = 48,

        ///<summary>
        ///Estado de la Orden de Trabajo cuando se ha finalizado el proceso  
        ///</summary>
        Cerrado = 50,

        ///<summary>
        ///Mesa de Control Deceval
        ///</summary>
        Mesa_de_Control_Deceval = 60,

        ///<summary>
        ///
        ///</summary>
        Centro_Distribucion = 500,

        ///<summary>
        ///
        ///</summary>
        Asignado_a_Remision = 510,

        ///<summary>
        ///
        ///</summary>
        Enviado_a_custodia = 520,

        ///<summary>
        ///Bandeja de entrada de bóveda  
        ///</summary>
        Por_Custodiar = 1000,

        ///<summary>
        ///
        ///</summary>
        Custodia = 1100,

        ///<summary>
        ///
        ///</summary>
        Bandeja_salida_boveda = 1500,

        ///<summary>
        ///
        ///</summary>
        Alistamiento_custodia = 1510,

        ///<summary>
        ///
        ///</summary>
        En_despacho = 1520,

        ///<summary>
        ///
        ///</summary>
        Asignado_a_Guia = 1525,

        ///<summary>
        ///
        ///</summary>
        Enviado_a_prestamo = 2000,

        ///<summary>
        ///
        ///</summary>
        En_Prestamo = 2010,

        ///<summary>
        ///
        ///</summary>
        Pendiente_de_devolucion = 2020,

        ///<summary>
        ///
        ///</summary>
        Por_Prestamo_Deceval = 2025,

        ///<summary>
        ///
        ///</summary>
        En_Prestamo_Deceval = 2030,

        ///<summary>
        ///
        ///</summary>
        Devuelto_a_custodia = 2100,

        ///<summary>
        ///Documentos que llegan en el cargue como devoluciones pero no llega el fisico  
        ///</summary>
        Faltante_en_devolcuion = 2300,

        ///<summary>
        ///
        ///</summary>
        Cancelado = 3000,

        ///<summary>
        ///
        ///</summary>
        Enviado_al_cliente = 3100,

        ///<summary>
        ///La imágen se encuentra publicada en el servidor de imágenes  
        ///</summary>
        Publicado = 4000,

        ///<summary>
        ///La imagen está siendo transferida a otro servidor  
        ///</summary>
        En_transferencia = 4010,

        ///<summary>
        ///
        ///</summary>
        Entregado_a_custodia_cliente = 5000,

        ///<summary>
        ///Pendiente por publicación  
        ///</summary>
        Proceso_Adicional = 41,

        ///<summary>
        ///Corrección captura máquina
        ///</summary>
        Correccion_Maquina = 46,

        ///<summary>
        ///Ciald Relevado Local
        ///</summary>
        Relevo_Ciald_Local = 6000,

        ///<summary>
        ///Ciald Relevado Centralizador
        ///</summary>
        Relevo_Ciald_Centralizador = 6001,

        ///<summary>
        ///Ciald Pendiente Relevo (Faltante)
        ///</summary>
        Pendiente_Relevo = 6002

    }
    
    /// <summary>
    /// Tipos de campo que se pueden utilizar en el Core
    /// </summary>
    public enum CampoTipoEnum
    {
        _01_Texto = 1,
        _02_Numerico = 2,
        _03_Fecha = 3,
        _04_Si_No = 4,
        _05_Lista = 5,
        _06_Lista_Enlazada = 6,
        _08_Funcion = 8,
        _09_Tabla_Asociada = 9
    }

    public enum ServidorTipoEnum : short
    {
        Database = 1,
        Fileserver = 2
    }

    public enum TipoImagenLogo : short
    {
        LogoEncabezado = 1,
        Firma = 2,
        LogoPiePagina = 3
    }
}