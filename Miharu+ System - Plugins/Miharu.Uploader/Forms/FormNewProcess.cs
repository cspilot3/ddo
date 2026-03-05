using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Miharu.Uploader.Forms
{
    public partial class FormNewProcess : Form
    {
        public bool IsImaging;
        public bool IsData; 

        public FormNewProcess()
        {
            InitializeComponent();
        }

        private void newImageProcessButton_Click(object sender, EventArgs e)
        {
            IsImaging = true;
            Program.IsImage = IsImaging;
            this.DialogResult = DialogResult.OK;
        }
        
        private void newDataProcessButton_Click(object sender, EventArgs e)
        {
            IsData = true;
            Program.IsData = IsData;
            this.DialogResult = DialogResult.OK;
        }

    }
}
