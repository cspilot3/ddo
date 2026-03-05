using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Imaging.Impuestos;
using Miharu.Desktop.Controls.DesktopMessageBox;

namespace BcoBogota.Plugin.Imaging.Impuestos.Forms
{
    public partial class frmDevoluciones : Form
    {
        #region Devoluciones
        private ImpuestosPlugin _Plugin;

        #endregion

        public frmDevoluciones(ImpuestosPlugin plugin)
        {
            this._Plugin = plugin;
            InitializeComponent();
        }

        private void frmDevoluciones_Load(object sender, EventArgs e)
        {
            this.dgvDevueltos.AutoGenerateColumns = false;
            CargarCombos();
            Cargargrilla(); 
        }

        private void txtFormulario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //var value = Char.GetNumericValue(e.KeyChar);

            if (e.KeyChar != (char)Keys.Back)
            {
                if (!ValidaNumericos(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void btlSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtPlaca.Text != "")
            {
                this.txtPlaca.Text = this.txtPlaca.Text.ToUpper();
                this.txtPlaca.SelectionStart = this.txtPlaca.TextLength;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.txtFiltro.Text == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error, Valor a Buscar no puede estar vacio.!!", "Campo Vacio", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                return;
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                switch (this.cbFiltros.SelectedItem.ToString())
                {
                    case "Formulario":
                        Cargargrilla(null, null, null, this.txtFiltro.Text);
                        break;
                    case "Oficina":
                        Cargargrilla(null, null, this.txtFiltro.Text);
                        break;
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void mtxtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar.ToString() != Keys.Back.ToString())
            //{
            //    if (!ValidaNumericos(e.KeyChar.ToString()))
            //    {
            //        e.Handled = true;
            //    }
            //    else
            //    {
            //        var ValorNumeric = this.mtxtValor.Text;

            //        if (ValorNumeric.Length > 3)
            //        {
            //            string auxValorNumeric = "";

            //            for (int i = ValorNumeric.Length-1; i > 0; i--)
            //            {
            //                auxValorNumeric = auxValorNumeric + ValorNumeric[i];
            //            }
            //        }
            //    }
            //}
        }

        private void mtxtValor_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue.ToString() != Keys.Back.ToString() && e.KeyValue.ToString() != Keys.Delete.ToString())
            //{
            //    if (!ValidaNumericos(e.KeyValue.ToString()))
            //    {
            //        e.Handled = true;
            //    }
            //    else
            //    {
            //        var ValorNumeric = this.mtxtValor.Text.Replace(",", "");

            //        if (ValorNumeric.Length > 3)
            //        {
            //            string auxValorNumeric = "";
            //            int ContadorChar = 1;

            //            for (int i = ValorNumeric.Length - 1; i >= 0; i--)
            //            {
            //                auxValorNumeric = ValorNumeric[i] + auxValorNumeric;

            //                if (ContadorChar == 3)
            //                {
            //                    ContadorChar = 0;
            //                    auxValorNumeric = "," + auxValorNumeric;

            //                }
            //                ContadorChar++;
            //            }

            //            this.mtxtValor.Text = auxValorNumeric;
            //        } 
            //    }
            //}
        }

        private void btnInsertarRegistro_Click(object sender, EventArgs e)
        {

            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
                if (ValidarCampos())
                {
                    dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoBogotaConnectionString);

                    dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                    var encontradoFormulario = dbmIntegration.SchemaBcoBogota.TBL_Formularios_Devueltos.DBFindByNumero_Formulario(this.txtFormulario.Text);

                    if (encontradoFormulario.Rows.Count > 0)
                    {
                        DesktopMessageBoxControl.DesktopMessageShow("Ya Existe Este Numero de Formulario en el Sistema.!!!", "Formulario Existente", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                        return;
                    }


                    DBIntegration.SchemaBcoBogota.TBL_Formularios_DevueltosType rowInsertar = new DBIntegration.SchemaBcoBogota.TBL_Formularios_DevueltosType();

                    rowInsertar.Codigo_Oficina = Convert.ToInt64(this.cbOficinas.SelectedValue);
                    rowInsertar.Fecha_Recaudo = this.dtpFechaRecaudo.Value.ToString("yyyy/MM/dd");
                    rowInsertar.fk_causal_devolucion = Convert.ToInt32(this.cbCausalDevolucion.SelectedValue.ToString());
                    rowInsertar.Numero_Formulario = this.txtFormulario.Text;
                    rowInsertar.Oficina_Str = this.cbOficinas.Text;
                    rowInsertar.Codigo_Oficina = Convert.ToInt64(this.cbOficinas.SelectedValue.ToString());
                    rowInsertar.Placa = this.txtPlaca.Text;
                    rowInsertar.Valor = this.mtxtValor.Text;

                    this.Cursor = Cursors.WaitCursor;
                    dbmIntegration.SchemaBcoBogota.TBL_Formularios_Devueltos.DBInsert(rowInsertar);

                    DesktopMessageBoxControl.DesktopMessageShow("Registro insertado con exito", "Registro insertado", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, true);

                    Cargargrilla();
                    this.Cursor = Cursors.Default;
                }

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en btnInsertarRegistro_Click()", ref ex);
            }
            finally
            {
                if (dbmIntegration != null)
                    dbmIntegration.Connection_Close();
            }
        }

        private void CargarCombos()
        {
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = null;

            try
            {
                dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoBogotaConnectionString);

                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                var Oficinas = dbmIntegration.SchemaBcoBogota.PA_Obtiene_Oficinas.DBExecute(this._Plugin.Manager.ImagingGlobal.Entidad);

                if (Oficinas.Rows.Count > 0)
                {
                    this.cbOficinas.DisplayMember = Oficinas.Columns["Etiqueta_Campo_Lista_Item"].ToString();
                    this.cbOficinas.ValueMember = Oficinas.Columns["Valor_Campo_Lista_Item"].ToString();
                    this.cbOficinas.DataSource = Oficinas;
                }
                else
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Combo de Oficinas, no hay oficinas registradas en el Modulo!!", "Error al cargar", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                }

                var CausalesDevolucion = dbmIntegration.SchemaBcoBogota.TBL_Causales_Devolucion.DBGet(null);

                if (CausalesDevolucion.Rows.Count > 0)
                {
                    this.cbCausalDevolucion.ValueMember = CausalesDevolucion.Columns["id_Causal"].ToString();
                    this.cbCausalDevolucion.DisplayMember = CausalesDevolucion.Columns["Nombre_Causal"].ToString();
                    this.cbCausalDevolucion.DataSource = CausalesDevolucion;
                }
                else
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error al cargar Combo de Causales, no hay causales devolución registradas en el Modulo!!", "Error al cargar", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                }

                this.cbFiltros.SelectedIndex = 0;
            }
            catch (Exception ex) 
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en CargarCombos()", ref ex);
            }
            finally
            {
                if (dbmIntegration != null)
                    dbmIntegration.Connection_Close();
            }
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



        private void Cargargrilla(int? id_form_devuelto = null, string Fecha_Recaudo = null, string Oficina_str = null, string Num_formulario = null, string placa = null, int? causal = null)
        {                                                                                                                                                          
            DBIntegration.DBIntegrationDataBaseManager dbmIntegration = new DBIntegration.DBIntegrationDataBaseManager(this._Plugin.BcoBogotaConnectionString);

            try
            {
                dbmIntegration.Connection_Open(this._Plugin.Manager.Sesion.Usuario.id);

                var Devueltos = dbmIntegration.SchemaBcoBogota.PA_SelectStatement_FormulariosDevueltos.DBExecute(id_form_devuelto, Fecha_Recaudo, Oficina_str, Num_formulario, placa, causal);

                if (Devueltos.Rows.Count > 0)
                {
                    this.dgvDevueltos.DataSource = Devueltos;
                }
                else
                {
                    DesktopMessageBoxControl.DesktopMessageShow("No hay Datos para esta Consulta.!!", "Consulta Vacia", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, true);
                    this.dgvDevueltos.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error en Cargargrilla()", ref ex);
            }
            finally
            {
                dbmIntegration.Connection_Close();
            }
        }

        private bool ValidarCampos()
        {
            bool result = true;

            if (this.txtFormulario.Text == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error, el campo Formulario no puede estar vacio!!", "Error Campo", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                result = false;
                goto Devuelve;
            }

            if (this.txtPlaca.Text == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error, el campo Placa no puede estar vacio!!", "Error Campo", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                result = false;
                goto Devuelve;
            }

            if (this.mtxtValor.Text == "")
            {
                DesktopMessageBoxControl.DesktopMessageShow("Error, el campo Valor no puede estar vacio!!", "Error Campo", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                result = false;
                goto Devuelve;
            }
            else
            {
                Double varOut = 0.0;
                if (!Double.TryParse(this.mtxtValor.Text, out varOut))
                {
                    DesktopMessageBoxControl.DesktopMessageShow("Error, el campo Valor no es un numero valido!!", "Error Campo", DesktopMessageBoxControl.IconEnum.ErrorIcon, true);
                    result = false;
                    goto Devuelve;
                }
            }



Devuelve:
            return result;

        }

        

        

        

        

        

        

        
    }
}
