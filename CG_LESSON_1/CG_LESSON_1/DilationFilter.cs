using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class DilationFilter : Filters
    {
        private float[,] kernel = null;
        public DilationFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            return processImage(sourceImage, worker, 100);
        }

        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker, int percent)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int y = kernel.GetLength(1) / 2; y < sourceImage.Height - kernel.GetLength(1) / 2; y++)
            {
                worker.ReportProgress((int)((float)y / resultImage.Height * percent));

                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int x = kernel.GetLength(0) / 2; x < sourceImage.Width - kernel.GetLength(0) / 2; x++)
                {
                    int max = 0;
                    for (int j = -kernel.GetLength(1) / 2; j < kernel.GetLength(1) / 2; j++)
                    {
                        for (int i = -kernel.GetLength(0) / 2; i < kernel.GetLength(0) / 2; i++)
                        {
                            if (kernel[i, j] > 0)
                            {
                                max = Math.Max(max, sourceImage.GetPixel(x + i, y + j).R);
                            }
                        }
                    }

                    max = Clamp(max, 0, 255);

                    resultImage.SetPixel(x, y, Color.FromArgb(max, max, max));
                }
            }

            return resultImage;
        }
    }
}
