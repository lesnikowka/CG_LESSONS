using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class LinearContrastCorrectionFilter : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            byte minIntensity = 255;
            byte maxIntensity = 0;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / sourceImage.Width * 50));

                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    minIntensity = Math.Min(minIntensity
                        , Utilities.GetIntensity(sourceImage.GetPixel(i, j)));
                    maxIntensity = Math.Max(maxIntensity
                        , Utilities.GetIntensity(sourceImage.GetPixel(i, j)));
                }
            }

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress(50 + (int)((float)i / sourceImage.Width * 50));

                if (worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color color = sourceImage.GetPixel(i, j);

                    resultImage.SetPixel(i, j
                        , Color.FromArgb(
                            Clamp((int)(Clamp(color.R - minIntensity, 0, 255) * 255f / (maxIntensity - minIntensity + 1)) ,0, 255),
                            Clamp((int)(Clamp(color.G - minIntensity, 0, 255) * 255f / (maxIntensity - minIntensity + 1)), 0, 255),
                            Clamp((int)(Clamp(color.B - minIntensity, 0, 255) * 255f / (maxIntensity - minIntensity + 1)), 0, 255)
                    ));
                }
            }

            return resultImage;
        }
    }
}
