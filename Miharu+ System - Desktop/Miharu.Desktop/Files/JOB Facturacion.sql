--Registro de Log Inicio
DECLARE @NEXTID INT
DECLARE @SYSDATE DATETIME
DECLARE @FECHAINICIO DATETIME
DECLARE @FECHAFIN DATETIME
DECLARE @USER VARCHAR(30)
DECLARE @IDTRANSMISION INT
DECLARE @IDGARANTIA INT
DECLARE @COUNTREG INT
DECLARE @SUMREG INT
DECLARE @COUNTREGTRANS INT
DECLARE @SUMREGTRANS INT
DECLARE @MENSAJEFIN VARCHAR(60)
DECLARE @FECHAFACTURACION DATETIME
DECLARE @DWH_FECO_FECHA_INI DATETIME
DECLARE @DWH_FECO_FECHA_FIN DATETIME
DECLARE @SCI_LOIN_FC_DT_INI DATETIME
DECLARE @SCI_LOIN_FC_DT_FIN DATETIME

SELECT @NEXTID = ID, @SYSDATE=DT, @USER=US FROM OPENQUERY(PPRDFACT, 'SELECT S_SCI_LOIN_PK.NEXTVAL ID, TO_CHAR(SYSDATE,''YYYY-MM-DD HH24:mi:ss'') DT, USER US FROM DUAL')
SET @FECHAINICIO=@SYSDATE

SELECT @COUNTREG=COUNT(CANTIDAD_MOVIMIENTO), @SUMREG= ISNULL(SUM(CANTIDAD_MOVIMIENTO),0)
FROM         Bill.CTA_Transmision_Facturacion

--Obtiene el periodo de facturación
SELECT @FECHAFACTURACION = MESPROCESO, @DWH_FECO_FECHA_INI=DWH_FECO_FECHA_INI, @DWH_FECO_FECHA_FIN=DWH_FECO_FECHA_FIN FROM OPENQUERY(PPRDFACT, 'SELECT TO_CHAR(DWH_FECO_MES_PROCESO,''YYYY-MM-DD HH24:mi:ss'') MESPROCESO, DWH_FECO_NOMBRE_MES, TO_CHAR(DWH_FECO_FECHA_INI,''YYYY-MM-DD HH24:mi:ss'') DWH_FECO_FECHA_INI, TO_CHAR(DWH_FECO_FECHA_FIN,''YYYY-MM-DD HH24:mi:ss'') DWH_FECO_FECHA_FIN 
FROM DWH_FECO_FECHAS_CORTE
WHERE SYSDATE BETWEEN DWH_FECO_FECHA_INI AND DWH_FECO_FECHA_FIN')

--Obtiene rango de facturación
SELECT     @SCI_LOIN_FC_DT_INI=MIN(CONVERT(VARCHAR(30),Fecha_Movimiento,20)), @SCI_LOIN_FC_DT_FIN=MAX(CONVERT(VARCHAR(30),Fecha_Movimiento,20))
FROM         Bill.CTA_Transmision_Facturacion
WHERE		Fecha_Movimiento BETWEEN @DWH_FECO_FECHA_INI AND @DWH_FECO_FECHA_FIN


INSERT OPENQUERY(PPRDFACT, 'SELECT SCI_LOIN_SEC, CC1_CODIGO, SCI_INTE_CODIGO, SCI_TIPO_CODIGO, SCI_LOIN_FCH_GEN,
SCI_LOIN_HR_INI,SCI_LOIN_HRA_FIN,SCI_LOIN_HR_INI_C,SCI_LOIN_HR_FIN_C,
SCI_LOIN_USER_G, SCI_LOIN_USU_CAR,SCI_LOIN_FC_DT_INI, SCI_LOIN_FC_DT_FIN, 
SCI_LOIN_NR_REG_GEN, SCI_LOIN_CONTROL_GEN, SCI_LOIN_NR_REG_PR, SCI_LOIN_VOLUMEN,
SCI_LOIN_FCH_CAR,SCI_LOIN_DET_REPOR FROM SCI_LOIN_LOG_DIA_INT WHERE 1=0')
VALUES(@NEXTID, 1, 99, 1, @FECHAFACTURACION,
@SYSDATE, @SYSDATE, @SYSDATE, @SYSDATE,
@USER, @USER, @SCI_LOIN_FC_DT_INI, @SCI_LOIN_FC_DT_FIN,
@COUNTREG,@SUMREG,0,0,
@SYSDATE,NULL)

--Inserta registro inicio transacción
INSERT INTO Bill.TBL_Transmision (FK_LOG_INICIO, FK_LOG_FIN)
VALUES (@NEXTID, NULL)

--Obtiene el id de la transmisión
SELECT @IDTRANSMISION = MAX(ID_TRANSMISION) FROM Bill.TBL_Transmision

BEGIN TRY
	DECLARE @fk_Entidad INT, 
		@fk_Proyecto INT, 
		@fk_Esquema INT, 
		@fk_Esquema_Facturacion INT, 
		@fk_Servicio INT, 
		@Clasificacion INT, 
		@Grupo INT, 
		@Genero INT, 
		@Producto INT, 
		@Producto_Especifico INT, 
		@Producto_Detalle INT, 
		@Cod_Cuenta INT, 
		@Fecha_Movimiento DATETIME, 
		@Cantidad_Movimiento INT, 
		@fk_Usuario_Log INT, 
		@Transmitido BIT, 
		@Entidad_Detalle INT, 
		@Facturacion_Detalle INT, 
		@Id_Detalle INT,
		@NIT_Entidad VARCHAR(15),
		@Codigo_Interno_Facturacion INT, 
		@Codigo_Centro_Beneficio VARCHAR(4),
		@Nombre_Detalle VARCHAR(100),
		@Nombre_Servicio VARCHAR(100),
		@Tipo_Registro VARCHAR(2)

	--Cursor para los elementos a transmitir
	DECLARE curTransmision CURSOR FOR

	SELECT     fk_Entidad, fk_Proyecto, fk_Esquema, fk_Esquema_Facturacion, fk_Servicio, Clasificacion, Grupo, Genero, Producto, Producto_Especifico, 
                      Producto_Detalle, Cod_Cuenta, CONVERT(VARCHAR(30),Fecha_Movimiento,20) Fecha_Movimiento, Cantidad_Movimiento, fk_Usuario_Log, Transmitido, Entidad_Detalle, Facturacion_Detalle, 
                      Id_Detalle, NIT_Entidad, Codigo_Interno_Facturacion, Codigo_Centro_Beneficio, Nombre_Servicio, UPPER(Tipo_Registro) Tipo_Registro
	FROM         Bill.CTA_Transmision_Facturacion
	WHERE		Fecha_Movimiento BETWEEN @DWH_FECO_FECHA_INI AND @DWH_FECO_FECHA_FIN

	OPEN curTransmision

	FETCH curTransmision INTO @fk_Entidad, @fk_Proyecto, @fk_Esquema, @fk_Esquema_Facturacion, @fk_Servicio, 
		@Clasificacion, @Grupo, @Genero, @Producto, @Producto_Especifico, @Producto_Detalle, 
		@Cod_Cuenta, @Fecha_Movimiento, @Cantidad_Movimiento, @fk_Usuario_Log, @Transmitido, 
		@Entidad_Detalle, @Facturacion_Detalle, @Id_Detalle, @NIT_Entidad, @Codigo_Interno_Facturacion, 
		@Codigo_Centro_Beneficio, @Nombre_Servicio, @Tipo_Registro
	
	WHILE (@@FETCH_STATUS = 0 )
	BEGIN
		SELECT @IDGARANTIA = ID FROM OPENQUERY(PPRDFACT, 'SELECT S_FAC_DEGA_PK.NEXTVAL ID FROM DUAL')

		SELECT @SYSDATE=DT, @Nombre_Detalle=N FROM OPENQUERY(PPRDFACT, 'SELECT DETA_NOMBRE N, TO_CHAR(SYSDATE,''YYYY-MM-DD HH24:mi:ss'') DT, 
		CLAP_PRODUCTO, GRUP_PRODUCTO, GENE_PRODUCTO, PROD_CODIGO, ESPE_CODIGO, DETA_CODIGO
		FROM PRODUCTO_DETALLE')
		WHERE CLAP_PRODUCTO=@Clasificacion
		AND GRUP_PRODUCTO=@Grupo
		AND GENE_PRODUCTO=@Genero
		AND PROD_CODIGO=@Producto
		AND ESPE_CODIGO=@Producto_Especifico
		AND DETA_CODIGO=@Producto_Detalle

		--Crea el registro en al tabla de facturación	
		INSERT OPENQUERY(PPRDFACT, 'select fac_dega_secuencia, fac_dega_codigo_ciudad, fac_dega_nit_cliente,
		fac_dega_cod_cuenta, fac_dega_clap_producto, fac_dega_grup_producto, fac_dega_gene_producto, fac_dega_prod_codigo,
		fac_dega_espe_codigo, fac_dega_deta_codigo, fac_dega_deta_nombre, fac_dega_fecha_gestion, fac_dega_fecha_facturacion,
		fac_dega_tipo_accion, fac_dega_cantidad, fac_dega_adicionales, fac_dega_estado, fac_dega_fecha_proceso,
		fac_dega_apli_codigo, fac_dega_tipo_registro, sci_loin_sec, aud_usr_ins, aud_fch_ins, aud_usr_mod, aud_fch_mod
		from fac_dega_detalle_garantia WHERE 1=0')
		VALUES(@IDGARANTIA, @Codigo_Centro_Beneficio, @NIT_Entidad, 
		@Cod_Cuenta, @Clasificacion, @Grupo, @Genero, @Producto, 
		@Producto_Especifico, @Producto_Detalle, @Nombre_Detalle, @Fecha_Movimiento, @Fecha_Movimiento,
		@Nombre_Servicio, @Cantidad_Movimiento, '', 'G', @SYSDATE,
		49,@Tipo_Registro,@NEXTID,@USER, @SYSDATE, @USER, @SYSDATE)


		--Marca el registro como trasmitido
		UPDATE Bill.TBL_Facturacion_Detalle
		SET TRANSMITIDO=1
		WHERE fk_Entidad=@Entidad_Detalle AND
		fk_Facturacion=@Facturacion_Detalle AND
		id_Facturacion_Detalle=@Id_Detalle

		FETCH curTransmision INTO @fk_Entidad, @fk_Proyecto, @fk_Esquema, @fk_Esquema_Facturacion, @fk_Servicio, 
		@Clasificacion, @Grupo, @Genero, @Producto, @Producto_Especifico, @Producto_Detalle, 
		@Cod_Cuenta, @Fecha_Movimiento, @Cantidad_Movimiento, @fk_Usuario_Log, @Transmitido, 
		@Entidad_Detalle, @Facturacion_Detalle, @Id_Detalle, @NIT_Entidad, @Codigo_Interno_Facturacion, 
		@Codigo_Centro_Beneficio, @Nombre_Servicio, @Tipo_Registro
	END

	CLOSE curTransmision
	DEALLOCATE curTransmision
END TRY
BEGIN CATCH
	INSERT INTO Bill.TBL_Transmision_Detalle (FK_TRANSMISION, FK_ENTIDAD, FK_FACTURACION, FK_FACTURACION_DETALLE)
	VALUES(@IDTRANSMISION,0,0,ERROR_MESSAGE())

	UPDATE OPENQUERY(PPRDFACT, 'SELECT SCI_LOIN_SEC, CC1_CODIGO, SCI_INTE_CODIGO, SCI_TIPO_CODIGO, SCI_LOIN_FCH_GEN,
	SCI_LOIN_HR_INI,SCI_LOIN_HRA_FIN,SCI_LOIN_HR_INI_C,SCI_LOIN_HR_FIN_C,
	SCI_LOIN_USER_G, SCI_LOIN_USU_CAR,SCI_LOIN_FC_DT_INI, SCI_LOIN_FC_DT_FIN, 
	SCI_LOIN_NR_REG_GEN, SCI_LOIN_CONTROL_GEN, SCI_LOIN_NR_REG_PR, SCI_LOIN_VOLUMEN,
	SCI_LOIN_FCH_CAR,SCI_LOIN_DET_REPOR FROM SCI_LOIN_LOG_DIA_INT')
	SET SCI_LOIN_DET_REPOR = 'El proceso finalizó con inconsistencias: ' + ERROR_MESSAGE()
	WHERE SCI_LOIN_SEC= @NEXTID
END CATCH 


--Registro de Log Fin
SELECT @SYSDATE=DT FROM OPENQUERY(PPRDFACT, 'SELECT TO_CHAR(SYSDATE,''YYYY-MM-DD HH24:mi:ss'') DT FROM DUAL')
SET @FECHAFIN=@SYSDATE

--Obtiene conteo de registros transmitidos
SELECT @COUNTREGTRANS=COUNT(*), @SUMREGTRANS=SUM(FAC_DEGA_CANTIDAD) 
FROM PPRDFACT..U_ADMFACD.FAC_DEGA_DETALLE_GARANTIA 
WHERE FAC_DEGA_FECHA_PROCESO BETWEEN CONVERT(VARCHAR(20),@FECHAINICIO,20) AND CONVERT(VARCHAR(20),@FECHAFIN,20)

--Se valida que los registros transmitidos, sean los que se enviaron
IF (@COUNTREG <> @COUNTREGTRANS) OR (@SUMREG <> @SUMREGTRANS)
	BEGIN
		SET @MENSAJEFIN ='El proceso finalizó con diferencia de registros.'
	END
ELSE
	BEGIN
		SET @MENSAJEFIN ='El proceso finalizó correctamente. Secuencia: ' + CONVERT(VARCHAR(30),@IDGARANTIA)
	END

UPDATE OPENQUERY(PPRDFACT, 'SELECT SCI_LOIN_SEC, CC1_CODIGO, SCI_INTE_CODIGO, SCI_TIPO_CODIGO, SCI_LOIN_FCH_GEN,
SCI_LOIN_HR_INI,SCI_LOIN_HRA_FIN,SCI_LOIN_HR_INI_C,SCI_LOIN_HR_FIN_C,
SCI_LOIN_USER_G, SCI_LOIN_USU_CAR,SCI_LOIN_FC_DT_INI, SCI_LOIN_FC_DT_FIN, 
SCI_LOIN_NR_REG_GEN, SCI_LOIN_CONTROL_GEN, SCI_LOIN_NR_REG_PR, SCI_LOIN_VOLUMEN,
SCI_LOIN_FCH_CAR,SCI_LOIN_DET_REPOR FROM SCI_LOIN_LOG_DIA_INT')
SET SCI_LOIN_DET_REPOR = @MENSAJEFIN,
SCI_LOIN_HRA_FIN = @FECHAFIN,
SCI_LOIN_NR_REG_PR = @COUNTREGTRANS,
SCI_LOIN_VOLUMEN = @SUMREGTRANS
WHERE SCI_LOIN_SEC= @NEXTID


--Actualiza registro fin transacción
UPDATE Bill.TBL_Transmision 
SET FK_LOG_FIN=@NEXTID
WHERE ID_TRANSMISION=@IDTRANSMISION