using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.ComponentModel;

namespace Slyg.Tools.Imaging
{
    public class DropThumbnailHelper
    {
        #region Variables

        private Form MainForm = null;
        private Panel DragContainer = null;
        private DragPreview Preview = null;
        private Button ScrollLeftButton = null;
        private Button ScrollRightButton = null;

        private bool IsDragging = false;
        private bool IsScrolling = false;

        private TargetDirection TargetDirection = TargetDirection.Left;
        private int TargetDropIndex = -1;

        private BackgroundWorker ScrollWorker = null;
        private int ScrollSteps = 0;

        private object LockDrop = new object();

        #endregion

        #region Propiedades

        public PictureBox CurrentDragPicture { get; protected set; }
        public PictureBox TargetDragPicture { get; protected set; }

        #endregion

        #region Definicion de eventos

        public delegate void ChangeImagePositionHandler(ChangeImagenPositionEventArgs e);
        public event ChangeImagePositionHandler OnChangeImagePosition;

        #endregion

        #region Contructores

        public DropThumbnailHelper()
        {
        }

        public void Inicialize(Form nMainForm, Panel nDragContainer)
        {
            this.MainForm = nMainForm;
            this.DragContainer = nDragContainer;

            this.Preview = new DragPreview();

            this.ScrollLeftButton = new Button();
            this.ScrollLeftButton.Width = 10;
            this.ScrollLeftButton.Height = nDragContainer.Height - 0;
            this.MainForm.Controls.Add(this.ScrollLeftButton);
            this.ScrollLeftButton.BringToFront();
            this.ScrollLeftButton.Hide();
            this.ScrollLeftButton.AllowDrop = true;

            this.ScrollRightButton = new Button();
            this.ScrollRightButton.Width = 10;
            this.ScrollRightButton.Height = nDragContainer.Height - 0;
            this.MainForm.Controls.Add(this.ScrollRightButton);
            this.ScrollRightButton.BringToFront();
            this.ScrollRightButton.Hide();
            this.ScrollRightButton.AllowDrop = true;

            this.MainForm.Resize += new EventHandler(MainForm_Resize);

            this.ScrollLeftButton.MouseEnter += new EventHandler(ScrollLeftButton_MouseEnter);
            this.ScrollLeftButton.MouseLeave += new EventHandler(ScrollLeftButton_MouseLeave);
            this.ScrollLeftButton.DragEnter += new DragEventHandler(ScrollLeftButton_DragEnter);
            this.ScrollLeftButton.DragLeave += new EventHandler(ScrollLeftButton_DragLeave);

            this.ScrollRightButton.MouseEnter += new EventHandler(ScrollRightButton_MouseEnter);
            this.ScrollRightButton.MouseLeave += new EventHandler(ScrollRightButton_MouseLeave);
            this.ScrollRightButton.DragEnter += new DragEventHandler(ScrollRightButton_DragEnter);
            this.ScrollRightButton.DragLeave += new EventHandler(ScrollRightButton_DragLeave);

            ScrollWorker = new BackgroundWorker();
            ScrollWorker.WorkerReportsProgress = true;
            ScrollWorker.DoWork += new DoWorkEventHandler(ScrollWorker_DoWork);
            ScrollWorker.ProgressChanged += new ProgressChangedEventHandler(ScrollWorker_ProgressChanged);

            RecalculateScrollButtonsPosition();
        }

        #endregion

        #region Manejadores de Eventos

        private void ScrollLeftButton_DragLeave(object sender, EventArgs e)
        {
            IsScrolling = false;
        }

        private void ScrollLeftButton_DragEnter(object sender, DragEventArgs e)
        {
            ScrollSteps = 20;
            IsScrolling = true;
            if (!ScrollWorker.IsBusy) ScrollWorker.RunWorkerAsync();
        }

        private void ScrollLeftButton_MouseEnter(object sender, EventArgs e)
        {
            ScrollSteps = 20;
            IsScrolling = true;
            if(! ScrollWorker.IsBusy ) ScrollWorker.RunWorkerAsync();
        }

        private void ScrollLeftButton_MouseLeave(object sender, EventArgs e)
        {
            IsScrolling = false;
        }

        private void ScrollRightButton_DragLeave(object sender, EventArgs e)
        {
            IsScrolling = false;
        }

        private void ScrollRightButton_DragEnter(object sender, DragEventArgs e)
        {
            ScrollSteps = -20;
            IsScrolling = true;
            if (!ScrollWorker.IsBusy) ScrollWorker.RunWorkerAsync();
        }

        private void ScrollRightButton_MouseEnter(object sender, EventArgs e)
        {
            ScrollSteps = -20;
            IsScrolling = true;
            if (!ScrollWorker.IsBusy) ScrollWorker.RunWorkerAsync();
        }

        private void ScrollRightButton_MouseLeave(object sender, EventArgs e)
        {
            IsScrolling = false;
        }

        private void ScrollWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int MaxCycles = 20;
            while (IsScrolling || MaxCycles < 20)
            {
                ScrollWorker.ReportProgress(10);
                Thread.Sleep(100);
                MaxCycles++;
            }
        }

        private void ScrollWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DragContainer.AutoScrollPosition = new Point(DragContainer.AutoScrollPosition.X * -1 - this.ScrollSteps, DragContainer.AutoScrollPosition.Y);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (IsDragging)
            {
                RecalculateScrollButtonsPosition();
            }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsDragging) return;

            if (e.Button == MouseButtons.Right)
            {
                CurrentDragPicture = sender as PictureBox;
                TargetDragPicture = null;

                MainForm.DoDragDrop(CurrentDragPicture, DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        private void Picture_DragEnter(object sender, DragEventArgs e)
        {
            if (!IsDragging) return;

            var data = e.Data.GetData(typeof(PictureBox)) as PictureBox;
            if (data != null)
            {
                if (sender is PictureBox && (PictureBox)sender != data)
                {
                    e.Effect = DragDropEffects.Move;

                    TargetDragPicture = sender as PictureBox;
                    //TargetDragPicture.BorderStyle = BorderStyle.FixedSingle;

                    return;
                }
                else
                {
                    TargetDragPicture = null;
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void Picture_DragOver(object sender, DragEventArgs e)
        {
            if (!IsDragging) return;

            if (TargetDragPicture == null) { Preview.HidePreview(); return; }

            var pos = TargetDragPicture.PointToClient(System.Windows.Forms.Control.MousePosition);

            FlowLayoutPanel TargetParent = TargetDragPicture.Parent as FlowLayoutPanel;

            int newIndex = 0;
            newIndex = TargetParent.Controls.GetChildIndex(TargetDragPicture);
            TargetDirection = (pos.X < TargetDragPicture.Width / 2) ? TargetDirection.Left : TargetDirection.Right;

            if (TargetDirection == TargetDirection.Right) newIndex++;

            if (TargetDropIndex != newIndex)
            {
                TargetDropIndex = newIndex;

                if (Preview.Parent == null) Preview.SetParent(MainForm);
                Preview.ShowPreview(TargetDragPicture, CurrentDragPicture, TargetDirection);
            }
        }

        private void Picture_DragLeave(object sender, EventArgs e)
        {
            if (!IsDragging) return;

            if (sender is PictureBox)
            {
                TargetDragPicture = sender as PictureBox;
                //TargetDragPicture.BorderStyle = BorderStyle.None;
                TargetDragPicture = null;
            }

            TargetDropIndex = -1;
            Preview.HidePreview();
        }

        private void Picture_DragDrop(object sender, DragEventArgs e)
        {
            if (!IsDragging) return;

            lock (LockDrop)
            {

                if (TargetDragPicture == null) return;
                Preview.HidePreview();

                if (OnChangeImagePosition != null)
                {
                    int index = TargetDropIndex;
                    if (CurrentDragPicture.Parent == TargetDragPicture.Parent)
                    {
                        if (CurrentDragPicture.Parent.Controls.GetChildIndex(CurrentDragPicture) < TargetDragPicture.Parent.Controls.GetChildIndex(TargetDragPicture))
                            index--;
                    }

                    if (index > TargetDragPicture.Parent.Controls.Count + 1) index = TargetDragPicture.Parent.Controls.Count;
                    if (index < 0) index = 0;

                    OnChangeImagePosition(new ChangeImagenPositionEventArgs()
                    {
                        SourcePicture = CurrentDragPicture,
                        TargetPicture = TargetDragPicture,
                        TargetDirection = TargetDirection,
                        TargetParentIndex = index
                    });
                }
            }
        }

        #endregion

        #region Funciones publicas

        public void AddPictureHandlers(PictureBox nControl)
        {
            if (this.MainForm == null) throw new Exception("No se ha inicializado el ayudante de organizacion de imagenes, utilice Inicialize(...)");

            nControl.AllowDrop = true;

            nControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseDown);
            nControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.Picture_DragEnter);
            nControl.DragOver += new System.Windows.Forms.DragEventHandler(this.Picture_DragOver);
            nControl.DragLeave += new System.EventHandler(this.Picture_DragLeave);
            nControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.Picture_DragDrop);
        }

        public void BeginDrag()
        {
            this.IsDragging = true;
            RecalculateScrollButtonsPosition();
            this.ScrollLeftButton.Show();
            this.ScrollRightButton.Show();
        }

        public void EndDrag()
        {
            this.IsDragging = false;
            this.ScrollLeftButton.Hide();
            this.ScrollRightButton.Hide();
        }

        #endregion

        #region Funciones privadas

        private void RecalculateScrollButtonsPosition()
        {
            this.ScrollLeftButton.Location = new Point(DragContainer.Location.X + 0, DragContainer.Location.Y + 0);
            this.ScrollRightButton.Location = new Point(DragContainer.Location.X + DragContainer.Width - ScrollRightButton.Width, DragContainer.Location.Y + 0);
        }

        private bool AbortThumbNail()
        {
            return false;
        }

        #endregion
    }
}