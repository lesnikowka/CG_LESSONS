using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    internal class ErosionFilter : Filters
    {
        private float[,] kernel = null;
        public ErosionFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int y = kernel.GetLength(1) / 2; y < sourceImage.Height - kernel.GetLength(1) / 2; y++)
            {
                worker.ReportProgress((int)((float)y / resultImage.Height * 100));

                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int x = kernel.GetLength(0) / 2; x < sourceImage.Width - kernel.GetLength(0) / 2; x++)
                {
                    int min = 255;
                    for (int j = -kernel.GetLength(1) / 2; j < kernel.GetLength(1) / 2; j++)
                    {
                        for (int i = -kernel.GetLength(0) / 2; i < kernel.GetLength(0) / 2; i++)
                        {
                            if (kernel[i + kernel.GetLength(0) / 2, j + kernel.GetLength(1) / 2] > 0)
                            {
                                min = Math.Min(min, sourceImage.GetPixel(x + i, y + j).R);
                            }
                        }
                    }

                    min = Clamp(min, 0, 255);

                    resultImage.SetPixel(x, y, Color.FromArgb(min, min, min));
                }
            }

            return resultImage;
        }
    }
}
