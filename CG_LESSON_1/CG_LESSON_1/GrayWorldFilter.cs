using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class GrayWorldFilter : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker) 
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            int N = sourceImage.Width * sourceImage.Height;

            float avR = 0;
            float avG = 0;
            float avB = 0;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));

                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color color = sourceImage.GetPixel(i, j);

                    avR += color.R;
                    avG += color.G;
                    avB += color.B;
                }
            }

            avR /= N; avG /= N; avB /= N;

            float av = (avR + avG + avB) / 3;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress(50 + (int)((float)i / resultImage.Width * 50));

                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color color = sourceImage.GetPixel(i, j);

                    resultImage.SetPixel(i, j
                        , Color.FromArgb(
                            Clamp((int)(color.R * av / avR), 0, 255),
                            Clamp((int)(color.G * av / avG), 0, 255),
                            Clamp((int)(color.B * av / avB), 0, 255)
                    ));
                }
            }

            return resultImage;
        }
    }
}
