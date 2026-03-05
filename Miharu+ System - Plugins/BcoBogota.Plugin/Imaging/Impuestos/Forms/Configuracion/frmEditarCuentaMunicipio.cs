using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBIntegration;
using Imaging.Impuestos;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms.Configuracion
{
    public partial class frmEditarCuentaMunicipio : Form
    {

        ImpuestosPlugin _plugin;
        private int _idBuscar;
        private string _accion = "";

        public frmEditarCuentaMunicipio(ImpuestosPlugin Plugin, int idBuscar, string Accion)
        {
            this._plugin = Plugin;
            this._idBuscar = idBuscar;
            this._accion = Accion;
            this.Cursor = Cursors.Default;
            InitializeComponent();
        }

        private void frmEditarCuentaMunicipio_Load(object sender, EventArgs e)
        {
            LlenarCombos();
            
            this.Text = this.Text + " - " + this._accion;
            this.btnActualizar.Text = this._accion.ToString().ToUpperInvariant();

            if (this._accion == "ACTUALIZAR")
            {
                EnabledControls(false);
                BuscarCuenta(); 
            }
            else
                EnabledControls(true);
                
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            

            switch (this._accion)
            {
                case "ACTUALIZAR":
                    ActualizarCuenta();
                    break;
                case "INSERTAR":
                    if (this.txtNoCuentaBcoBogota.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'No Cuenta Bco Bogota' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    else if (this.txtNumeroCuenta.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'Numero Cuenta' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    else if (this.txtNombreCuenta.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'Nombre Cuenta' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    InsertarCuenta();
                    break;
            }   
            
        }

        private void txtNoCuentaBcoBogota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!ValidaNumericos(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void txtNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!ValidaNumericos(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void cbMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBCore.DBCoreDataBaseManager dbmCore = new DBCore.DBCoreDataBaseManager(this._plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            try
            {
                dbmCore.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                string MunicipioSelected = this.cbMunicipios.SelectedValue.ToString();

                var MunEncontrado = dbmCore.SchemaConfig.TBL_Municipios.DBFindByCodigo_Municipio(MunicipioSelected.Trim());

                if (MunEncontrado.Rows.Count > 0)
                {
                    var Departamento = MunEncontrado.Rows[0]["fk_Codigo_Departamento"].ToString();
                    if (Departamento != "")
                    {
                        this.cbDepartamentos.SelectedValue = Departamento;
                    }
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error cbMunicipios_SelectedIndexChanged()", ref ex);
            }
            finally
            {
                dbmCore.Connection_Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        private bool ValidaNumericos(string strNumero)
        {
            bool result = false;
            int OutVariable;

            try
            {
                result = int.TryParse(strNumero, out OutVariable);
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error ValidaNumericos()", ref ex);
            }
            
            return result;
 
        }

        private void EnabledControls(bool Accion)
        {
            this.cbTipoCuenta.Enabled = Accion;
            this.cbTipoCuentaCorriente.Enabled = Accion;
            this.cbCodigoCompensacion.Enabled = Accion;
            this.cbDepartamentos.Enabled = Accion;
            this.cbMunicipios.Enabled = Accion;
            this.txtNoCuentaBcoBogota.Enabled = Accion;
            this.txtNombreCuenta.Enabled = Accion;
            this.txtNumeroCuenta.Enabled = Accion;
        }

        private void LimpiarControles()
        {
            this.txtNoCuentaBcoBogota.Text = "";
            this.txtNombreCuenta.Text = "";
            this.txtNumeroCuenta.Text = "";
            this.cbTipoCuenta.SelectedIndex = 0;
            this.cbTipoCuentaCorriente.SelectedIndex = 0;
            this.cbCodigoCompensacion.SelectedIndex = 0;
            this.cbDepartamentos.SelectedIndex = 0;
            this.cbMunicipios.SelectedIndex = 0;
        }

        private void LlenarCombos()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);
            DBCore.DBCoreDataBaseManager dbmCore = new DBCore.DBCoreDataBaseManager(this._plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);
                dbmCore.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                //Tipo Cuentas Corriente
                DataTable TipoCuentasCorriente = dbmIntegration.SchemaBcoBogota.TBL_Tipo_Cuenta_Corriente.DBGet(null);
                Utilities.LlenarCombo(this.cbTipoCuentaCorriente, TipoCuentasCorriente, TipoCuentasCorriente.Columns["id"], TipoCuentasCorriente.Columns["Nombre_Cuenta"]);
                this.cbTipoCuentaCorriente.SelectedIndex = 0;

                //Tipo Cuentas
                DataTable TipoCuentas = dbmIntegration.SchemaBcoBogota.TBL_Tipo_Cuenta.DBGet(null);
                Utilities.LlenarCombo(this.cbTipoCuenta, TipoCuentas, TipoCuentas.Columns["id_Tipo_Cuenta"], TipoCuentas.Columns["Valor"]);
                this.cbTipoCuenta.SelectedIndex = 0;

                //Codigo Compensación
                DataTable CodigoCompensa = dbmIntegration.SchemaBcoBogota.TBL_Codigos_Compensacion.DBGet(null);
                Utilities.LlenarCombo(this.cbCodigoCompensacion, CodigoCompensa, CodigoCompensa.Columns["id"], CodigoCompensa.Columns["Codigo_Compensacion"]);
                this.cbCodigoCompensacion.SelectedIndex = 0;

                //Estado
                DataTable Estados = dbmIntegration.SchemaBcoBogota.TBL_Estado_Cuenta.DBGet(null);
                Utilities.LlenarCombo(this.cbEstados, Estados, Estados.Columns["id"], Estados.Columns["Nombre_Estado"]);
                this.cbEstados.SelectedIndex = 0;
                
                //Municipios
                DataTable Municipios = dbmCore.SchemaConfig.TBL_Municipios.DBGet(null);
                Utilities.LlenarCombo(this.cbMunicipios, Municipios, Municipios.Columns["Codigo_Municipio"], Municipios.Columns["Nombre_Municipio"]);
                this.cbMunicipios.SelectedIndex = 0;

                //Departamentos
                DataTable Departamentos = dbmCore.SchemaConfig.TBL_Departamentos.DBGet(null);
                Utilities.LlenarCombo(this.cbDepartamentos, Departamentos, Departamentos.Columns["Codigo_Departamento"], Departamentos.Columns["Nombre_Departamento"]);
                this.cbDepartamentos.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error LlenarCombos", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void BuscarCuenta()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);
                
                DataTable CuentaEncontrada = dbmIntegration.SchemaBcoBogota.PA_SelectStatement_RelacionCuentas_Municipios.DBExecute(this._idBuscar, null, null, null, null, null);

                if (CuentaEncontrada.Rows.Count > 0)
                {
                    //Llenar Campos
                    this.cbTipoCuentaCorriente.SelectedValue = CuentaEncontrada.Rows[0]["id_Tipo_Cuenta_Corriente"].ToString();
                    this.txtNoCuentaBcoBogota.Text = CuentaEncontrada.Rows[0]["No_Cuenta_BcoBogota"].ToString();
                    this.txtNombreCuenta.Text = CuentaEncontrada.Rows[0]["Nombre_Cuenta"].ToString();
                    this.txtNumeroCuenta.Text = CuentaEncontrada.Rows[0]["Numero_Cuenta"].ToString();
                    this.cbCodigoCompensacion.SelectedValue = (CuentaEncontrada.Rows[0]["id_Codigo_Compensacion"].ToString());
                    this.cbMunicipios.SelectedValue = CuentaEncontrada.Rows[0]["Codigo_Municipio"].ToString();
                    this.cbDepartamentos.SelectedValue = CuentaEncontrada.Rows[0]["Codigo_Departamento"].ToString();
                    this.cbEstados.SelectedValue = (CuentaEncontrada.Rows[0]["id_Estado"].ToString());
                    this.cbTipoCuenta.SelectedValue = (CuentaEncontrada.Rows[0]["id_Tipo_Cuenta"].ToString());
                }
                else
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error Buscar, Cuenta no encontada", "Error Buscar Cuenta", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                }
                
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error LlenarCombos", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void ActualizarCuenta()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                var CuentaBuscar = this.txtNumeroCuenta.Text;
                //TODO validar que no exista la cuenta

                if (MessageBox.Show("Desea realmente Guardar los Cambios?", "Guardar Cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DBIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_MunicipioType regUpdate = new DBIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_MunicipioType();
                    regUpdate.No_Cuenta_BcoBogota = this.txtNoCuentaBcoBogota.Text;
                    regUpdate.Nombre_Cuenta = this.txtNombreCuenta.Text;
                    //regUpdate.Nombre_Municipio = this.txtNombreMunicipio.Text;
                    regUpdate.Numero_Cuenta = this.txtNumeroCuenta.Text;
                    regUpdate.fk_Codigo_Compensacion = Convert.ToInt32(this.cbCodigoCompensacion.SelectedValue.ToString());
                    regUpdate.fk_Estado_Cuenta = Convert.ToInt32(this.cbEstados.SelectedValue.ToString());
                    regUpdate.fk_Tipo_Cuenta = Convert.ToInt32(this.cbTipoCuentaCorriente.SelectedValue.ToString());
                    regUpdate.Fecha_Ultima_Modificacion = DateTime.Now;
                    regUpdate.fk_Tipo_Cuenta_dos = this.cbTipoCuenta.SelectedValue.ToString();
                    regUpdate.Codigo_Municipio = this.cbMunicipios.SelectedValue.ToString();
                    regUpdate.Codigo_Departamento = this.cbDepartamentos.SelectedValue.ToString();
                    dbmIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_Municipio.DBUpdate(regUpdate, this._idBuscar);

                    DesktopMessageBoxControl.DesktopMessageShow("Registro actualizado con exito!!!", "Actualizado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error ActualizarCuenta()", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private void InsertarCuenta()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                var CuentaBuscar = this.txtNumeroCuenta.Text;
                //TODO validar que no exista la cuenta
                int idVigente = Convert.ToInt32(dbmIntegration.SchemaBcoBogota.TBL_Estado_Cuenta.DBFindByNombre_Estado("Vigente").ToList().First().id);
                var EncontradaCuenta = dbmIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_Municipio.DBFindByNumero_Cuentafk_Estado_Cuenta(CuentaBuscar, idVigente);

                if (EncontradaCuenta.Rows.Count > 0)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Ya Existe un Registro Vigente con este Numero de Cuenta!!!", "Advertencia", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                }
                else
                {
                    if (MessageBox.Show("Desea realmente Guardar los Cambios?", "Guardar Cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        DBIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_MunicipioType regInsertar = new DBIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_MunicipioType();
                        regInsertar.No_Cuenta_BcoBogota = this.txtNoCuentaBcoBogota.Text;
                        regInsertar.Nombre_Cuenta = this.txtNombreCuenta.Text;
                        regInsertar.Numero_Cuenta = this.txtNumeroCuenta.Text;
                        regInsertar.fk_Codigo_Compensacion = Convert.ToInt32(this.cbCodigoCompensacion.SelectedValue.ToString());
                        regInsertar.fk_Estado_Cuenta = Convert.ToInt32(this.cbEstados.SelectedValue.ToString());
                        regInsertar.fk_Tipo_Cuenta = Convert.ToInt32(this.cbTipoCuentaCorriente.SelectedValue.ToString());
                        regInsertar.fk_Usuario_Creacion = this._plugin.Manager.Sesion.Usuario.id;
                        regInsertar.fk_Tipo_Cuenta_dos = this.cbTipoCuenta.SelectedValue.ToString();
                        regInsertar.Codigo_Municipio = this.cbMunicipios.SelectedValue.ToString();
                        regInsertar.Codigo_Departamento = this.cbDepartamentos.SelectedValue.ToString();

                        dbmIntegration.SchemaBcoBogota.TBL_Relacion_Cuentas_Municipio.DBInsert(regInsertar);
                        DesktopMessageBoxControl.DesktopMessageShow("Registro insertado con exito!!!", "Insertado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);

                        if (DesktopMessageBoxControl.DesktopMessageShow("¿Desea insertar otro registro?", "Insertar", DesktopMessageBoxControl.IconEnum.WarningIcon, false) == System.Windows.Forms.DialogResult.OK)
                        {
                            LimpiarControles();
                            return;
                        }
                        else
                            this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error InsetarCuenta()", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        

        

        

        

        

        

        

    }
}
