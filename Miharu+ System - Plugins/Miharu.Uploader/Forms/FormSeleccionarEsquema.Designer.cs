namespace Miharu.Uploader.Forms
{
    partial class FormSeleccionarEsquema
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.ProyectoComboBox = new Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl();
            this.EsquemaDataGridView = new Miharu.Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl();
            this.Id_Esquema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre_Esquema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.AceptarButton = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EsquemaDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.ProyectoComboBox);
            this.GroupBox1.Controls.Add(this.EsquemaDataGridView);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(346, 285);
            this.GroupBox1.TabIndex = 5;
            this.GroupBox1.TabStop = false;
            // 
            // ProyectoComboBox
            // 
            this.ProyectoComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProyectoComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProyectoComboBox.DisabledEnter = false;
            this.ProyectoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProyectoComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ProyectoComboBox.FormattingEnabled = true;
            this.ProyectoComboBox.Location = new System.Drawing.Point(12, 32);
            this.ProyectoComboBox.Name = "ProyectoComboBox";
            this.ProyectoComboBox.Size = new System.Drawing.Size(317, 21);
            this.ProyectoComboBox.TabIndex = 0;
            this.ProyectoComboBox.SelectedIndexChanged += new System.EventHandler(this.ProyectoComboBox_SelectedIndexChanged);
            // 
            // EsquemaDataGridView
            // 
            this.EsquemaDataGridView.AllowUserToAddRows = false;
            this.EsquemaDataGridView.AllowUserToDeleteRows = false;
            this.EsquemaDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.EsquemaDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EsquemaDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.EsquemaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EsquemaDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_Esquema,
            this.Nombre_Esquema});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.EsquemaDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.EsquemaDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.EsquemaDataGridView.Location = new System.Drawing.Point(12, 88);
            this.EsquemaDataGridView.MultiSelect = false;
            this.EsquemaDataGridView.Name = "EsquemaDataGridView";
            this.EsquemaDataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EsquemaDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.EsquemaDataGridView.RowHeadersWidth = 20;
            this.EsquemaDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.EsquemaDataGridView.Size = new System.Drawing.Size(317, 181);
            this.EsquemaDataGridView.TabIndex = 5;
            this.EsquemaDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EsquemaDataGridView_CellMouseDoubleClick);
            // 
            // Id_Esquema
            // 
            this.Id_Esquema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Id_Esquema.DataPropertyName = "fk_Esquema";
            dataGridViewCellStyle2.NullValue = null;
            this.Id_Esquema.DefaultCellStyle = dataGridViewCellStyle2;
            this.Id_Esquema.HeaderText = "Id";
            this.Id_Esquema.Name = "Id_Esquema";
            this.Id_Esquema.ReadOnly = true;
            this.Id_Esquema.Width = 41;
            // 
            // Nombre_Esquema
            // 
            this.Nombre_Esquema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre_Esquema.DataPropertyName = "Nombre_Esquema";
            this.Nombre_Esquema.HeaderText = "Nombre";
            this.Nombre_Esquema.Name = "Nombre_Esquema";
            this.Nombre_Esquema.ReadOnly = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label2.Location = new System.Drawing.Point(9, 69);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(58, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Esquema";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(9, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(58, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Proyecto";
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelarButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.CancelarButton.Image = global::Miharu.Uploader.Properties.Resources.cancel;
            this.CancelarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelarButton.Location = new System.Drawing.Point(271, 307);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(88, 32);
            this.CancelarButton.TabIndex = 7;
            this.CancelarButton.Text = "&Cancelar";
            this.CancelarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // AceptarButton
            // 
            this.AceptarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AceptarButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.AceptarButton.Image = global::Miharu.Uploader.Properties.Resources.tick;
            this.AceptarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AceptarButton.Location = new System.Drawing.Point(179, 307);
            this.AceptarButton.Name = "AceptarButton";
            this.AceptarButton.Size = new System.Drawing.Size(86, 32);
            this.AceptarButton.TabIndex = 6;
            this.AceptarButton.Text = "&Aceptar";
            this.AceptarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AceptarButton.UseVisualStyleBackColor = true;
            this.AceptarButton.Click += new System.EventHandler(this.AceptarButton_Click);
            // 
            // FormSeleccionarEsquema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 351);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.AceptarButton);
            this.Name = "FormSeleccionarEsquema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de Esquema";
            this.Load += new System.EventHandler(this.FormSeleccionarEsquema_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EsquemaDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal Desktop.Controls.DesktopComboBox.DesktopComboBoxControl ProyectoComboBox;
        internal Desktop.Controls.DesktopDataGridView.DesktopDataGridViewControl EsquemaDataGridView;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button CancelarButton;
        internal System.Windows.Forms.Button AceptarButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Esquema;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre_Esquema;
    }
}