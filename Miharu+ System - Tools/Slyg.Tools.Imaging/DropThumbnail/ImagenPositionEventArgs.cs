using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Slyg.Tools.Imaging
{
    public class ChangeImagenPositionEventArgs : EventArgs
    {
        public PictureBox SourcePicture { set; get; }
        public PictureBox TargetPicture { set; get; }
        public TargetDirection TargetDirection { set; get; }
        public int TargetParentIndex{ set; get; }
    }
}
