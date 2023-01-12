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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace sepia_assembler_JA
{
    class SepiaManager
    {
        private BitmapSource _oldBitmap;
        private List<SepiaInterface> _sepiaInterfaces = new List<SepiaInterface>();
        private List<Task> _tasks = new List<Task>();
        private int _numberOfThreads;
        private float[] _allPixels;

        const int _bitsInByte = 8;

        private SepiaManager() { }

        public SepiaManager(BitmapSource bitmapImage,
            SepiaMechanismType mechanismType,
            float sepiaRate, int numberOfThreads)
        {
            _oldBitmap = bitmapImage;
            _allPixels = RetrievePixels(bitmapImage);
            _numberOfThreads = numberOfThreads;
            int pieceLenght = AdjustPieceLenght();
            float[] sepiaRates = { 0, sepiaRate, 2 * sepiaRate, 0 };
            for (int partNumber = 0; partNumber < _numberOfThreads; partNumber++)   
            {
                int tempPartNumber = partNumber;
                int pieceEnd;
                if (partNumber + 1 == _numberOfThreads)
                    pieceEnd = _allPixels.Length;
                else
                    pieceEnd = pieceLenght * (tempPartNumber + 1) - 1;
                _sepiaInterfaces.Add(SepiaMechanismFactory.Create(
                        mechanismType, sepiaRates,
                        bitmapImage.Format.BitsPerPixel / _bitsInByte,
                        pieceLenght * tempPartNumber,
                        pieceEnd));
                _tasks.Add(new Task(() =>
                    _sepiaInterfaces[tempPartNumber].ExecuteEffect(_allPixels)));
            }
        }

        private int AdjustPieceLenght()
        {
            int pieceLenght = _allPixels.Length / _numberOfThreads;
            while (pieceLenght % (_oldBitmap.Format.BitsPerPixel / _bitsInByte) != 0)
                pieceLenght++;
            return pieceLenght;
        }

        private float[] RetrievePixels(BitmapSource bitmapImage)
        {
            return bitmapImage.ToBmpBGRArray();
        }

        public BitmapSource ExecuteEffect(out System.TimeSpan elapsedTime)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.ForEach(_tasks, (task) => task.Start());
            Task.WaitAll(_tasks.ToArray());

            stopwatch.Stop();
            elapsedTime = stopwatch.Elapsed;

            return _allPixels.BmpBGRArrayToImage(_oldBitmap.PixelWidth,
                _oldBitmap.PixelHeight, _oldBitmap.Format);
        }
    }
}