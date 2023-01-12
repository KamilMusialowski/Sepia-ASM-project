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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace sepia_assembler_JA
{
    public static class BytesArray
    {
        public static BitmapSource BmpBGRArrayToImage(
           this byte[] pixels, int width, int height, PixelFormat pixelFormat)
        {
            const int bitsInByte = 8;
            const int dpi = 96;
            WriteableBitmap bitmap = new WriteableBitmap(width, height, dpi, dpi, pixelFormat, null);

            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * (bitmap.Format.BitsPerPixel / bitsInByte), 0);

            return bitmap;
        }

        public static float[] ToFloatArray(this byte[] byteArray)
        {
            var newFloatArray = new float[byteArray.Length];
            for (int i = 0; i < newFloatArray.Length; i++)
            {
                newFloatArray[i] = (float)byteArray[i];
            }
            return newFloatArray;
        }
    }
}
