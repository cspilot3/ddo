using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Slyg.Tools.Imaging
{
    public class DragPreview
    {
        #region Variables

        private PictureBox TargetPicture = null;
        private PictureBox DragPicture = null;
        private Point Location = new Point(0,0);
        private Size Size = new Size();

        #endregion

        #region Propiedades

        public Form Parent { get; protected set; }

        #endregion

        #region Contructores

        public DragPreview()
        {
            TargetPicture = new PictureBox();
            DragPicture = new PictureBox();

            this.Size = new Size(300, 300);

            TargetPicture.Size = new Size(180, 260);
            TargetPicture.BorderStyle = BorderStyle.FixedSingle;

            DragPicture.Size = new Size(160, 200);
            DragPicture.BorderStyle = BorderStyle.FixedSingle;

            SetLocation(this.Location);
        }

        #endregion

        #region Funciones publicas

        public void SetParent(Form nParent)
        {
            Parent = nParent;
            Parent.Controls.AddRange(GetControls());
        }

        public void ShowPreview(PictureBox nTarget, PictureBox nDrag, TargetDirection nTargetDirection)
        {
            var pos = Parent.PointToClient(Cursor.Position);
            pos.X = pos.X - (this.Size.Width / 2);
            pos.Y = pos.Y - this.Size.Height - 20;

            if (pos.X > Parent.Width - (this.Size.Width) - 20)
                pos.X = Parent.Width - (this.Size.Width) - 20;

            if (pos.X < 10) 
                pos.X = 10;

            Location = pos;

            DragPicture.Image = nDrag.Image.GetThumbnailImage(DragPicture.Size.Width, DragPicture.Size.Height, new Image.GetThumbnailImageAbort(AbortThumbNail), IntPtr.Zero);
            TargetPicture.Image = nTarget.Image.GetThumbnailImage(TargetPicture.Size.Width, TargetPicture.Size.Height, new Image.GetThumbnailImageAbort(AbortThumbNail), IntPtr.Zero);

            TargetPicture.Location = new Point(Location.X + 50, Location.Y + 20);

            if (nTargetDirection == TargetDirection.Left)
            {
                DragPicture.Location = new Point(Location.X + 5, Location.Y + 50);
            }
            else
            {
                DragPicture.Location = new Point(Location.X + 135, Location.Y + 50);
            }

            TargetPicture.Show();
            DragPicture.Show();

            if (nTargetDirection == TargetDirection.Left)
            {
                DragPicture.BringToFront();
                TargetPicture.BringToFront();
            }
            else
            {
                TargetPicture.BringToFront();
                DragPicture.BringToFront();
            }
        }

        public void HidePreview()
        {
            TargetPicture.Hide();
            DragPicture.Hide();
        }

        #endregion

        #region Funciones privadas

        private Control[] GetControls()
        {
            return new Control[] { TargetPicture, DragPicture };
        }

        private void SetLocation(Point nLocation)
        {
            Location = nLocation;
            TargetPicture.Location = new Point(Location.X + 50, Location.Y + 20);
            DragPicture.Location = new Point(Location.X + 5, Location.Y + 100);
        }

        private bool AbortThumbNail()
        {
            return false;
        }

        #endregion
    }
}
