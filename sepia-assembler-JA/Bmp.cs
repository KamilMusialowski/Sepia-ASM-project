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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace sepia_assembler_JA
{
    public static class Bmp
    {
        public static float[] ToBmpBGRArray(this BitmapSource bitmapSource)
        {
            int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] bytePixels = new byte[bitmapSource.PixelHeight * stride];

            bitmapSource.CopyPixels(bytePixels, stride, 0);

            float[] floatPixels = bytePixels.ToFloatArray();

            return floatPixels;
        }

        public static Bitmap BitmapFromSource(BitmapSource source)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(source));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap source)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                source.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
