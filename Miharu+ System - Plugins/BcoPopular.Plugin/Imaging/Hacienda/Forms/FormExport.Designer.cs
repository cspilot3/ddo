using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace BcoPopular.Plugin.Imaging.Hacienda.Forms
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    partial class FormExport : Miharu.Desktop.Library.FormBase
    {

        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.TXTRadioButton = new System.Windows.Forms.RadioButton();
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.VisorRadioButton = new System.Windows.Forms.RadioButton();
            this.XMLRadioButton = new System.Windows.Forms.RadioButton();
            this.CarpetaSalidaTextBox = new System.Windows.Forms.TextBox();
            this.BuscarCarpetaButton = new System.Windows.Forms.Button();
            this.GroupBoxExportar = new System.Windows.Forms.GroupBox();
            this.CheckBoxExpedientesValidos = new System.Windows.Forms.CheckBox();
            this.CheckBoxExpedientes = new System.Windows.Forms.CheckBox();
            this.FechaProcesoFinalLabel = new System.Windows.Forms.Label();
            this.FechaProcesoFinalDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.BuscarFechaButton = new System.Windows.Forms.Button();
            this.FechaProcesoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.OTLabel = new System.Windows.Forms.Label();
            this.FechaProcesoLabel = new System.Windows.Forms.Label();
            this.ExpedientesDataGridView = new System.Windows.Forms.DataGridView();
            this.Expediente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha_Proceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fk_OT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Exportar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Ruta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OTDataGridView = new System.Windows.Forms.DataGridView();
            this.id_OT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_OT_Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCerrado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnExportado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnTipoExportacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.ExportarButton = new System.Windows.Forms.Button();
            this.MainGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GroupBoxExportar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExpedientesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OTDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TXTRadioButton
            // 
            this.TXTRadioButton.AutoSize = true;
            this.TXTRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTRadioButton.Location = new System.Drawing.Point(153, 51);
            this.TXTRadioButton.Name = "TXTRadioButton";
            this.TXTRadioButton.Size = new System.Drawing.Size(50, 19);
            this.TXTRadioButton.TabIndex = 10;
            this.TXTRadioButton.Text = "TXT";
            this.TXTRadioButton.UseVisualStyleBackColor = true;
            this.TXTRadioButton.Visible = false;
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainGroupBox.Controls.Add(this.panel1);
            this.MainGroupBox.Controls.Add(this.GroupBoxExportar);
            this.MainGroupBox.Controls.Add(this.FechaProcesoFinalLabel);
            this.MainGroupBox.Controls.Add(this.FechaProcesoFinalDateTimePicker);
            this.MainGroupBox.Controls.Add(this.BuscarFechaButton);
            this.MainGroupBox.Controls.Add(this.FechaProcesoDateTimePicker);
            this.MainGroupBox.Controls.Add(this.OTLabel);
            this.MainGroupBox.Controls.Add(this.FechaProcesoLabel);
            this.MainGroupBox.Controls.Add(this.ExpedientesDataGridView);
            this.MainGroupBox.Controls.Add(this.OTDataGridView);
            this.MainGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainGroupBox.Location = new System.Drawing.Point(10, 6);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(593, 152);
            this.MainGroupBox.TabIndex = 0;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Parametros de exportación";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCarpeta);
            this.panel1.Controls.Add(this.VisorRadioButton);
            this.panel1.Controls.Add(this.XMLRadioButton);
            this.panel1.Controls.Add(this.CarpetaSalidaTextBox);
            this.panel1.Controls.Add(this.BuscarCarpetaButton);
            this.panel1.Controls.Add(this.TXTRadioButton);
            this.panel1.Location = new System.Drawing.Point(13, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 79);
            this.panel1.TabIndex = 18;
            // 
            // lblCarpeta
            // 
            this.lblCarpeta.AutoSize = true;
            this.lblCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarpeta.Location = new System.Drawing.Point(3, 9);
            this.lblCarpeta.Name = "lblCarpeta";
            this.lblCarpeta.Size = new System.Drawing.Size(122, 15);
            this.lblCarpeta.TabIndex = 5;
            this.lblCarpeta.Text = "Carpeta de Salida";
            // 
            // VisorRadioButton
            // 
            this.VisorRadioButton.AutoSize = true;
            this.VisorRadioButton.Checked = true;
            this.VisorRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisorRadioButton.Location = new System.Drawing.Point(7, 51);
            this.VisorRadioButton.Name = "VisorRadioButton";
            this.VisorRadioButton.Size = new System.Drawing.Size(57, 19);
            this.VisorRadioButton.TabIndex = 8;
            this.VisorRadioButton.TabStop = true;
            this.VisorRadioButton.Text = "Visor";
            this.VisorRadioButton.UseVisualStyleBackColor = true;
            this.VisorRadioButton.Visible = false;
            // 
            // XMLRadioButton
            // 
            this.XMLRadioButton.AutoSize = true;
            this.XMLRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XMLRadioButton.Location = new System.Drawing.Point(83, 51);
            this.XMLRadioButton.Name = "XMLRadioButton";
            this.XMLRadioButton.Size = new System.Drawing.Size(54, 19);
            this.XMLRadioButton.TabIndex = 9;
            this.XMLRadioButton.Text = "XML";
            this.XMLRadioButton.UseVisualStyleBackColor = true;
            this.XMLRadioButton.Visible = false;
            // 
            // CarpetaSalidaTextBox
            // 
            this.CarpetaSalidaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CarpetaSalidaTextBox.Location = new System.Drawing.Point(7, 28);
            this.CarpetaSalidaTextBox.Name = "CarpetaSalidaTextBox";
            this.CarpetaSalidaTextBox.Size = new System.Drawing.Size(498, 20);
            this.CarpetaSalidaTextBox.TabIndex = 6;
            // 
            // BuscarCarpetaButton
            // 
            this.BuscarCarpetaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BuscarCarpetaButton.Image = global::BcoPopular.Plugin.Properties.Resources.MainFolder;
            this.BuscarCarpetaButton.Location = new System.Drawing.Point(511, 22);
            this.BuscarCarpetaButton.Name = "BuscarCarpetaButton";
            this.BuscarCarpetaButton.Size = new System.Drawing.Size(43, 30);
            this.BuscarCarpetaButton.TabIndex = 7;
            this.BuscarCarpetaButton.UseVisualStyleBackColor = true;
            this.BuscarCarpetaButton.Click += new System.EventHandler(this.BuscarCarpetaButton_Click);
            // 
            // GroupBoxExportar
            // 
            this.GroupBoxExportar.Controls.Add(this.CheckBoxExpedientesValidos);
            this.GroupBoxExportar.Controls.Add(this.CheckBoxExpedientes);
            this.GroupBoxExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxExportar.Location = new System.Drawing.Point(15, 66);
            this.GroupBoxExportar.Name = "GroupBoxExportar";
            this.GroupBoxExportar.Size = new System.Drawing.Size(623, 44);
            this.GroupBoxExportar.TabIndex = 17;
            this.GroupBoxExportar.TabStop = false;
            this.GroupBoxExportar.Text = "Exportar por:";
            this.GroupBoxExportar.Visible = false;
            // 
            // CheckBoxExpedientesValidos
            // 
            this.CheckBoxExpedientesValidos.AutoSize = true;
            this.CheckBoxExpedientesValidos.Location = new System.Drawing.Point(288, 21);
            this.CheckBoxExpedientesValidos.Name = "CheckBoxExpedientesValidos";
            this.CheckBoxExpedientesValidos.Size = new System.Drawing.Size(170, 17);
            this.CheckBoxExpedientesValidos.TabIndex = 1;
            this.CheckBoxExpedientesValidos.Text = "Expedientes validos x OT";
            this.CheckBoxExpedientesValidos.UseVisualStyleBackColor = true;
            this.CheckBoxExpedientesValidos.CheckedChanged += new System.EventHandler(this.CheckBoxExpedientesValidos_CheckedChanged);
            // 
            // CheckBoxExpedientes
            // 
            this.CheckBoxExpedientes.AutoSize = true;
            this.CheckBoxExpedientes.Checked = true;
            this.CheckBoxExpedientes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxExpedientes.Location = new System.Drawing.Point(15, 21);
            this.CheckBoxExpedientes.Name = "CheckBoxExpedientes";
            this.CheckBoxExpedientes.Size = new System.Drawing.Size(95, 17);
            this.CheckBoxExpedientes.TabIndex = 0;
            this.CheckBoxExpedientes.Text = "Expedientes";
            this.CheckBoxExpedientes.UseVisualStyleBackColor = true;
            this.CheckBoxExpedientes.Visible = false;
            this.CheckBoxExpedientes.CheckedChanged += new System.EventHandler(this.CheckBoxExpedientes_CheckedChanged);
            // 
            // FechaProcesoFinalLabel
            // 
            this.FechaProcesoFinalLabel.AutoSize = true;
            this.FechaProcesoFinalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoFinalLabel.Location = new System.Drawing.Point(300, 19);
            this.FechaProcesoFinalLabel.Name = "FechaProcesoFinalLabel";
            this.FechaProcesoFinalLabel.Size = new System.Drawing.Size(138, 15);
            this.FechaProcesoFinalLabel.TabIndex = 12;
            this.FechaProcesoFinalLabel.Text = "Fecha Proceso Final";
            // 
            // FechaProcesoFinalDateTimePicker
            // 
            this.FechaProcesoFinalDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoFinalDateTimePicker.Location = new System.Drawing.Point(303, 38);
            this.FechaProcesoFinalDateTimePicker.Name = "FechaProcesoFinalDateTimePicker";
            this.FechaProcesoFinalDateTimePicker.Size = new System.Drawing.Size(279, 22);
            this.FechaProcesoFinalDateTimePicker.TabIndex = 11;
            // 
            // BuscarFechaButton
            // 
            this.BuscarFechaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BuscarFechaButton.Image = global::BcoPopular.Plugin.Properties.Resources.btnBuscar;
            this.BuscarFechaButton.Location = new System.Drawing.Point(538, 30);
            this.BuscarFechaButton.Name = "BuscarFechaButton";
            this.BuscarFechaButton.Size = new System.Drawing.Size(43, 30);
            this.BuscarFechaButton.TabIndex = 2;
            this.BuscarFechaButton.UseVisualStyleBackColor = true;
            this.BuscarFechaButton.Visible = false;
            this.BuscarFechaButton.Click += new System.EventHandler(this.BuscarFechaButton_Click);
            // 
            // FechaProcesoDateTimePicker
            // 
            this.FechaProcesoDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoDateTimePicker.Location = new System.Drawing.Point(15, 38);
            this.FechaProcesoDateTimePicker.Name = "FechaProcesoDateTimePicker";
            this.FechaProcesoDateTimePicker.Size = new System.Drawing.Size(279, 22);
            this.FechaProcesoDateTimePicker.TabIndex = 1;
            // 
            // OTLabel
            // 
            this.OTLabel.AutoSize = true;
            this.OTLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OTLabel.Location = new System.Drawing.Point(12, 113);
            this.OTLabel.Name = "OTLabel";
            this.OTLabel.Size = new System.Drawing.Size(32, 15);
            this.OTLabel.TabIndex = 3;
            this.OTLabel.Text = "OTs";
            this.OTLabel.Visible = false;
            // 
            // FechaProcesoLabel
            // 
            this.FechaProcesoLabel.AutoSize = true;
            this.FechaProcesoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaProcesoLabel.Location = new System.Drawing.Point(12, 19);
            this.FechaProcesoLabel.Name = "FechaProcesoLabel";
            this.FechaProcesoLabel.Size = new System.Drawing.Size(145, 15);
            this.FechaProcesoLabel.TabIndex = 0;
            this.FechaProcesoLabel.Text = "Fecha Proceso Inicial";
            // 
            // ExpedientesDataGridView
            // 
            this.ExpedientesDataGridView.AllowUserToAddRows = false;
            this.ExpedientesDataGridView.AllowUserToDeleteRows = false;
            this.ExpedientesDataGridView.AllowUserToResizeColumns = false;
            this.ExpedientesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExpedientesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExpedientesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Expediente,
            this.Fecha_Proceso,
            this.fk_OT,
            this.Exportar,
            this.Ruta});
            this.ExpedientesDataGridView.Location = new System.Drawing.Point(13, 131);
            this.ExpedientesDataGridView.MultiSelect = false;
            this.ExpedientesDataGridView.Name = "ExpedientesDataGridView";
            this.ExpedientesDataGridView.RowHeadersWidth = 10;
            this.ExpedientesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ExpedientesDataGridView.Size = new System.Drawing.Size(566, 0);
            this.ExpedientesDataGridView.TabIndex = 16;
            this.ExpedientesDataGridView.Visible = false;
            // 
            // Expediente
            // 
            this.Expediente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Expediente.DataPropertyName = "fk_Expediente";
            this.Expediente.HeaderText = "Expediente";
            this.Expediente.Name = "Expediente";
            this.Expediente.ReadOnly = true;
            // 
            // Fecha_Proceso
            // 
            this.Fecha_Proceso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Fecha_Proceso.DataPropertyName = "Fecha_Proceso";
            this.Fecha_Proceso.HeaderText = "Fecha de Proceso";
            this.Fecha_Proceso.Name = "Fecha_Proceso";
            this.Fecha_Proceso.ReadOnly = true;
            this.Fecha_Proceso.Width = 150;
            // 
            // fk_OT
            // 
            this.fk_OT.DataPropertyName = "fk_OT";
            this.fk_OT.HeaderText = "OT";
            this.fk_OT.Name = "fk_OT";
            this.fk_OT.ReadOnly = true;
            this.fk_OT.Width = 40;
            // 
            // Exportar
            // 
            this.Exportar.DataPropertyName = "Exportar";
            this.Exportar.HeaderText = "Exportar";
            this.Exportar.Name = "Exportar";
            this.Exportar.Width = 60;
            // 
            // Ruta
            // 
            this.Ruta.DataPropertyName = "Ruta";
            this.Ruta.HeaderText = "Ruta";
            this.Ruta.Name = "Ruta";
            this.Ruta.ReadOnly = true;
            this.Ruta.Width = 300;
            // 
            // OTDataGridView
            // 
            this.OTDataGridView.AllowUserToAddRows = false;
            this.OTDataGridView.AllowUserToDeleteRows = false;
            this.OTDataGridView.AllowUserToOrderColumns = true;
            this.OTDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OTDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OTDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_OT,
            this.Nombre_OT_Tipo,
            this.ColumnCerrado,
            this.ColumnExportado,
            this.ColumnTipoExportacion,
            this.ColumnRuta});
            this.OTDataGridView.Location = new System.Drawing.Point(14, 131);
            this.OTDataGridView.MultiSelect = false;
            this.OTDataGridView.Name = "OTDataGridView";
            this.OTDataGridView.ReadOnly = true;
            this.OTDataGridView.RowHeadersWidth = 10;
            this.OTDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OTDataGridView.Size = new System.Drawing.Size(565, 0);
            this.OTDataGridView.TabIndex = 4;
            this.OTDataGridView.Visible = false;
            // 
            // id_OT
            // 
            this.id_OT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.id_OT.DataPropertyName = "id_OT";
            this.id_OT.HeaderText = "OT";
            this.id_OT.Name = "id_OT";
            this.id_OT.ReadOnly = true;
            // 
            // Nombre_OT_Tipo
            // 
            this.Nombre_OT_Tipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Nombre_OT_Tipo.DataPropertyName = "Nombre_OT_Tipo";
            this.Nombre_OT_Tipo.HeaderText = "Tipo OT";
            this.Nombre_OT_Tipo.Name = "Nombre_OT_Tipo";
            this.Nombre_OT_Tipo.ReadOnly = true;
            this.Nombre_OT_Tipo.Width = 200;
            // 
            // ColumnCerrado
            // 
            this.ColumnCerrado.DataPropertyName = "Cerrado";
            this.ColumnCerrado.HeaderText = "Cerrado";
            this.ColumnCerrado.Name = "ColumnCerrado";
            this.ColumnCerrado.ReadOnly = true;
            this.ColumnCerrado.Width = 60;
            // 
            // ColumnExportado
            // 
            this.ColumnExportado.DataPropertyName = "Exportado";
            this.ColumnExportado.HeaderText = "Exportado";
            this.ColumnExportado.Name = "ColumnExportado";
            this.ColumnExportado.ReadOnly = true;
            this.ColumnExportado.Width = 60;
            // 
            // ColumnTipoExportacion
            // 
            this.ColumnTipoExportacion.DataPropertyName = "Nombre_Exportacion_Tipo";
            this.ColumnTipoExportacion.HeaderText = "Modo";
            this.ColumnTipoExportacion.Name = "ColumnTipoExportacion";
            this.ColumnTipoExportacion.ReadOnly = true;
            this.ColumnTipoExportacion.Width = 40;
            // 
            // ColumnRuta
            // 
            this.ColumnRuta.DataPropertyName = "Ruta";
            this.ColumnRuta.HeaderText = "Ruta";
            this.ColumnRuta.Name = "ColumnRuta";
            this.ColumnRuta.ReadOnly = true;
            this.ColumnRuta.Width = 300;
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelarButton.Image = global::BcoPopular.Plugin.Properties.Resources.btnSalir;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(488, 164);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(104, 37);
            this.CancelarButton.TabIndex = 2;
            this.CancelarButton.Text = "Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // ExportarButton
            // 
            this.ExportarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportarButton.Image = global::BcoPopular.Plugin.Properties.Resources.Aceptar;
            this.ExportarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportarButton.Location = new System.Drawing.Point(373, 164);
            this.ExportarButton.Name = "ExportarButton";
            this.ExportarButton.Size = new System.Drawing.Size(104, 37);
            this.ExportarButton.TabIndex = 1;
            this.ExportarButton.Text = "Exportar";
            this.ExportarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportarButton.UseVisualStyleBackColor = true;
            this.ExportarButton.Click += new System.EventHandler(this.ExportarButton_Click);
            // 
            // FormExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 213);
            this.Controls.Add(this.MainGroupBox);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.ExportarButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar";
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GroupBoxExportar.ResumeLayout(false);
            this.GroupBoxExportar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExpedientesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OTDataGridView)).EndInit();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.RadioButton TXTRadioButton;
        internal System.Windows.Forms.GroupBox MainGroupBox;
        internal System.Windows.Forms.Button BuscarCarpetaButton;
        internal System.Windows.Forms.Label lblCarpeta;
        internal System.Windows.Forms.TextBox CarpetaSalidaTextBox;
        internal System.Windows.Forms.RadioButton XMLRadioButton;
        internal System.Windows.Forms.RadioButton VisorRadioButton;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button ExportarButton;
        internal System.Windows.Forms.Label FechaProcesoLabel;
        internal System.Windows.Forms.Label OTLabel;
        internal System.Windows.Forms.DateTimePicker FechaProcesoDateTimePicker;
        internal System.Windows.Forms.DataGridView OTDataGridView;
        internal System.Windows.Forms.Button BuscarFechaButton;
        internal System.Windows.Forms.DataGridViewTextBoxColumn id_OT;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Nombre_OT_Tipo;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCerrado;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn ColumnExportado;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ColumnTipoExportacion;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ColumnRuta;
        internal System.Windows.Forms.Label FechaProcesoFinalLabel;
        internal System.Windows.Forms.DateTimePicker FechaProcesoFinalDateTimePicker;
        internal System.Windows.Forms.DataGridView ExpedientesDataGridView;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Expediente;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Fecha_Proceso;
        internal System.Windows.Forms.DataGridViewTextBoxColumn fk_OT;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn Exportar;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Ruta;
        internal System.Windows.Forms.GroupBox GroupBoxExportar;
        internal System.Windows.Forms.CheckBox CheckBoxExpedientesValidos;
        internal System.Windows.Forms.CheckBox CheckBoxExpedientes;
        private System.Windows.Forms.Panel panel1;
    }
}
