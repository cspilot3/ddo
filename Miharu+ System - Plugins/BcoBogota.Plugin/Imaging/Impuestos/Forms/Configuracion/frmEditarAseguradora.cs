using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Imaging.Impuestos;
using DBIntegration;
using Miharu.Desktop.Library.Config;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms.Configuracion
{
    public partial class frmEditarAseguradora : Form
    {
        ImpuestosPlugin _plugin;
        private int _idBuscar;
        private string _accion = "";

        public frmEditarAseguradora(ImpuestosPlugin Plugin, int idBuscar, string Accion)
        {
            this._plugin = Plugin;
            this._idBuscar = idBuscar;
            this._accion = Accion;
            this.Cursor = Cursors.Default;
            InitializeComponent();
        }

        private void frmEditarAseguradora_Load(object sender, EventArgs e)
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


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            switch (this._accion)
            {
                case "ACTUALIZAR":
                    ActualizarCuenta();
                    break;
                case "INSERTAR":
                    if (this.txtTipo.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'Tipo' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    else if (this.txtNombreAseguradora.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'Nombre Aseguradora' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    else if (this.txtCodigo.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'Codigo' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    else if (this.txtNit.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'NIT' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }
                    else if (this.txtDv.Text == "")
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Error el Campo 'DV' no puede estar vacio", "Campo Vacio", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                        return;
                    }

                    InsertarCuenta();
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LlenarCombos()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);
            DBCore.DBCoreDataBaseManager dbmCore = new DBCore.DBCoreDataBaseManager(this._plugin.Manager.DesktopGlobal.ConnectionStrings.Core);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);
                dbmCore.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                //Estados
                DataTable Estados = new DataTable();
                Estados.Columns.Add("Value");
                Estados.Columns.Add("Text");

                Estados.Rows.Add(1, "Activo");
                Estados.Rows.Add(0, "Inactivo");

                this.cbEstados.DisplayMember = Estados.Columns["Text"].ToString();
                this.cbEstados.ValueMember = Estados.Columns["Value"].ToString();
                this.cbEstados.DataSource = Estados;
                this.cbEstados.SelectedIndex = 0;


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

        private void EnabledControls(bool Accion)
        {
            this.txtNit.Enabled = Accion;
            this.txtNombreAseguradora.Enabled = Accion;
            this.txtDv.Enabled = Accion;
            this.txtTipo.Enabled = Accion;
            this.txtNit.Enabled = Accion;
            this.txtCodigo.Enabled = Accion;
        }

        private void BuscarCuenta()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                var AseguradoraEncontrada = dbmIntegration.SchemaBcoBogota.PA_SelectStatement_Aseguradoras.DBExecute(this._idBuscar, null, null, null, null, null, true);

                if (AseguradoraEncontrada.Rows.Count > 0)
                {
                    //Llenar Campos
                    this.txtCodigo.Text = AseguradoraEncontrada.Rows[0]["CODIGO"].ToString();
                    this.txtDv.Text = AseguradoraEncontrada.Rows[0]["DV"].ToString();
                    this.txtNit.Text = AseguradoraEncontrada.Rows[0]["NIT"].ToString();
                    this.txtNombreAseguradora.Text = AseguradoraEncontrada.Rows[0]["DENOMINACION"].ToString();
                    this.txtTipo.Text = (AseguradoraEncontrada.Rows[0]["Tipo"].ToString());
                    this.cbEstados.SelectedValue = AseguradoraEncontrada.Rows[0]["Activo"].ToString() == "True" ? 1 : 0;
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

        private void InsertarCuenta()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                var NitBuscar = this.txtNit.Text;
                //TODO validar que no exista la cuenta
                int idVigente = Convert.ToInt32(dbmIntegration.SchemaBcoBogota.TBL_Estado_Cuenta.DBFindByNombre_Estado("Vigente").ToList().First().id);
                var EncontradaAseguradora = dbmIntegration.SchemaBcoBogota.TBL_Aseguradoras.DBFindByNitActivo(NitBuscar, true);

                if (EncontradaAseguradora.Rows.Count > 0)
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Ya Existe un Registro Vigente con este Numero de NIT!!!", "Advertencia", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                }
                else
                {
                    if (MessageBox.Show("Desea realmente Guardar los Cambios?", "Guardar Cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        DBIntegration.SchemaBcoBogota.TBL_AseguradorasType regInsertar = new DBIntegration.SchemaBcoBogota.TBL_AseguradorasType();
                        regInsertar.Codigo = this.txtCodigo.Text;
                        regInsertar.DV = this.txtDv.Text;
                        regInsertar.Nit = this.txtNit.Text;
                        regInsertar.Nombre_Aseguradora = this.txtNombreAseguradora.Text;
                        regInsertar.Tipo = this.txtTipo.Text;
                        regInsertar.Activo = this.cbEstados.SelectedValue.ToString() == "1" ? true : false;

                        dbmIntegration.SchemaBcoBogota.TBL_Aseguradoras.DBInsert(regInsertar);
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

        private void LimpiarControles()
        {
            this.txtCodigo.Text = "";
            this.txtDv.Text = "";
            this.txtNit.Text = "";
            this.txtNombreAseguradora.Text = "";
            this.txtTipo.Text = "";
            this.cbEstados.SelectedIndex = 0;
        }

        private void ActualizarCuenta()
        {
            DBIntegrationDataBaseManager dbmIntegration = new DBIntegrationDataBaseManager(this._plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._plugin.Manager.Sesion.Usuario.id);

                //var CuentaBuscar = this.txtNumeroCuenta.Text;
                //TODO validar que no exista la cuenta

                if (MessageBox.Show("Desea realmente Guardar los Cambios?", "Guardar Cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DBIntegration.SchemaBcoBogota.TBL_AseguradorasType regUpdate = new DBIntegration.SchemaBcoBogota.TBL_AseguradorasType();
                    regUpdate.Activo = this.cbEstados.SelectedValue.ToString() == "1" ? true : false;
                    dbmIntegration.SchemaBcoBogota.TBL_Aseguradoras.DBUpdate(regUpdate, this._idBuscar);

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

        

        
    }
}
