using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class GrayScaleFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            byte intensity = Utilities.GetIntensity(sourceColor);

            Color resultColor = Color.FromArgb(
                intensity
                , intensity
                , intensity
                );

            return resultColor;
        }
    }
}
