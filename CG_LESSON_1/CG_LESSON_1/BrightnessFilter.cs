using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace CG_LESSON_1
{
    class BrightnessFilter : Filters
    {
        private int coef = 30; 
        public BrightnessFilter(bool increase)
        {
            if (!increase)
            {
                coef = -coef;
            }
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            Color resultColor = Color.FromArgb(
                Clamp(sourceColor.R + coef, 0, 255)
                , Clamp(sourceColor.G + coef, 0, 255)
                , Clamp(sourceColor.B + coef, 0, 255)
                );

            return resultColor;
        }
    }
}
