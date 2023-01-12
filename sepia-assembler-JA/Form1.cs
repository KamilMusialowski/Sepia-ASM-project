/*
 Temat projektu: Filtr Sepia
Algorytm sumuje wartości RGB kolejnych pikseli przemnożone przez odpowiedni współczynnik.
Następnie oblicza nowe wartości RGB na podstawie powyższej sumy i współczynnika siły filtru.
Tabela zapisanych, uzyskanych w ten sposób wartości stanowi podstawę do wygenerowania grafiki
z nałożonym filtrem.
Data wykonania projektu: semestr V/rok akademicki 2022/2023, Kamil Musiałowski, Informatyka Katowice AEiI
Wersja: 1.0
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Windows.Media.Imaging;

namespace sepia_assembler_JA
{
    public partial class SepiaForm : Form
    {
        private Stopwatch stopwatch;
        private SepiaManager sm;
        private Bitmap pictureOriginal;
        private Bitmap pictureSepia;
        private float[] pixels;
        public SepiaForm()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            threadsNumber.Value = System.Environment.ProcessorCount;
            
        }

        

        public void openImage()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Plik graficzny (*.bmp)|*.BMP; *.bmp",
                FileName = "*.bmp"
            };
            openDialog.ShowDialog();

            if (openDialog.FileName != "*.bmp")
            {
                Image image = Image.FromFile(openDialog.FileName);
                openDialog.Dispose();
                pictureOriginal = (Bitmap)image;
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private Bitmap ScaleBitmap(Bitmap bmp, PictureBox picBox)
        {

            float ratio = 1.0f;
            int thumbHeight = 0;
            int thumbWidth = 0;

            if (bmp.Height > picBox.Height || bmp.Width > picBox.Width)
            {
                Image.GetThumbnailImageAbort myCallback =
                    new Image.GetThumbnailImageAbort(ThumbnailCallback);

                if (bmp.Height >= bmp.Width)
                {
                    ratio = (((float)bmp.Width) / ((float)bmp.Height));
                    thumbHeight = picBox.Height;
                    thumbWidth = (int)((thumbHeight) * (ratio));
                }
                else
                {
                    ratio = (((float)bmp.Height) / ((float)bmp.Width));
                    thumbWidth = picBox.Width;
                    thumbHeight = (int)((thumbWidth) * (ratio));
                }

                Image myThumbnail = bmp.GetThumbnailImage(thumbWidth, thumbHeight, myCallback, IntPtr.Zero);
                return new Bitmap(myThumbnail);
            }
            return bmp;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            openImage();
            pictureBefore.Image = ScaleBitmap(pictureOriginal, pictureBefore);
        }

        private void threads_Scroll(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if ((ASM.Checked || CS.Checked) && !pictureBefore.Image.Equals(null))
            {
                String sepiaMechanism = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;
                BitmapSource bs = Bmp.BitmapToBitmapSource(pictureOriginal);
                pixels = Bmp.ToBmpBGRArray(bs);
                double[] wyniki = new double[320];
                SepiaManager manager = null;
                if (ASM.Checked)
                {
                    manager = new SepiaManager(bs, SepiaMechanismType.Assembly, 20, threadsNumber.Value);
                }
                if (CS.Checked)
                {
                    manager = new SepiaManager(bs, SepiaMechanismType.Cpp, 20, threadsNumber.Value);
                }
                
                TimeSpan t;
                BitmapSource bsa = manager.ExecuteEffect(out t);
                pictureAfter.Image = ScaleBitmap(Bmp.BitmapFromSource(bsa), pictureAfter);
                pictureSepia = Bmp.BitmapFromSource(bsa);
                time.Text = t.ToString();
            }
            
        }

        private void CS_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pictureSepia != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = "moj_plik";
                saveDialog.Filter = "Plik graficzny (*.bmp)|*.BMP; *.bmp";
                saveDialog.ShowDialog();
                pictureSepia.Save(saveDialog.FileName);
                saveDialog.Dispose();
            }
        }
    }
 }
