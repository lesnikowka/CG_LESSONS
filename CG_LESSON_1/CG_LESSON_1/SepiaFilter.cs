using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class SepiaFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float k = 35;

            Color sourceColor = sourceImage.GetPixel(x, y);

            int intensity = Utilities.GetIntensity(sourceColor);

            Color resultColor = Color.FromArgb(
                Clamp((int)(intensity + 2 * k), 0, 255)
                , Clamp((int)(intensity + 0.5 * k), 0, 255)
                , Clamp((int)(intensity - 1 * k), 0, 255)
                );

            return resultColor;
        }
    }
}
