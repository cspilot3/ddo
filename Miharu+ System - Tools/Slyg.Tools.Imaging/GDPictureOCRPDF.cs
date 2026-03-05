using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GdPicture12;
using System.Windows.Forms;
using System.Diagnostics;

namespace Slyg.Tools.Imaging
{
    public class GDPictureOCRPDF
    {
        private readonly GdPicturePDF _nativePdf = new GdPicturePDF();
        private bool _cancellationPending;
        string ntxtoutput;
       
        public void IniciarParam(string txtDictsPath, string txtInputFile, string txtoutput)
        {
            LicenseManager oLicenseManager = new LicenseManager();
            oLicenseManager.RegisterKEY("0485383432374696957431456");

           // txtDictsPath = oLicenseManager.GetRedistPath() + @"OCR\";

            _nativePdf.OcrPagesProgress += this.OcrPagesProgress;
            _nativePdf.BeforePageOcr += this.BeforePageOcr;
            _nativePdf.OcrPagesDone += this.OcrPagesDone;

            btStartOCR_Click(txtDictsPath, txtInputFile, txtoutput);
        }


        private void BeforePageOcr(int pageNo, ref bool cancel)
        {
            if (_nativePdf.PageHasText(false))
            {
                if (MessageBox.Show("The page " + pageNo.ToString() + " of the pdf already has text. Do you want to skip it?", "The page has text", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                    cancel = true;
            }
        }

        private void OcrPagesProgress(GdPictureStatus status, int pageNo, int processed, int count, ref bool cancel)
        {

            if (status != GdPictureStatus.OK)
            {
                if (!_cancellationPending)
                {
                    if (MessageBox.Show("An error occured on page " + pageNo.ToString() + ". Do you want to cancel the process? Status: " + status.ToString(), "error", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                    {
                        cancel = true;
                        _cancellationPending = true;
                    }
                }
            }
        }

        private void OcrPagesDone(GdPictureStatus status)
        {
            if (status == GdPictureStatus.OK)
            {

                string outputFilePath = ntxtoutput + "_ocr.pdf";

                status = _nativePdf.SaveToFile(outputFilePath, true);

                if (status != GdPictureStatus.OK)
                    MessageBox.Show("Can't save file " + outputFilePath + " . Error: " + status.ToString());
                else
                    Process.Start(outputFilePath);
                MessageBox.Show("Done! Please check file " + outputFilePath);
            }
            else
                MessageBox.Show("An error occured. Status: " + status.ToString());

            _nativePdf.CloseDocument();

        }

        private void btStartOCR_Click(string txtDictsPath, string txtInputFile, string txtoutput)
        {
            ntxtoutput = txtoutput;
            bool chkMultithread = true;
            float resolution = float.Parse("300");

            if (_nativePdf.LoadFromFile(txtInputFile, false) == GdPictureStatus.OK)
            {
                if (_nativePdf.IsEncrypted())
                {
                    // PDF is encrypted, try to decrypt by using empty password
                    if (!(_nativePdf.SetPassword("") != GdPictureStatus.OK))
                    {
                        MessageBox.Show("This PDF is password protected", "operation cancelled", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        _nativePdf.CloseDocument();
                        return;
                    }
                }

                _cancellationPending = false;

                int threadCount;
                if (chkMultithread)
                    threadCount = 0; // 0 means automatic number of thread computation
                else
                    threadCount = 1;

                _nativePdf.OcrPages("*", threadCount, "spa", txtDictsPath, "", resolution);
            }
            else
                MessageBox.Show("Can't open file: " + txtInputFile);


        }



    }
}
