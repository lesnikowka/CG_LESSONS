using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class GlassFilter : Filters
    {
        private Random rnd = null;
        public GlassFilter()
        {
            rnd = new Random();
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xi = (int)(x + rnd.Next(0, 10) - 5);
            int yi = (int)(y + rnd.Next(0, 10) - 5);

            xi = Clamp(xi, 0, sourceImage.Width - 1);
            yi = Clamp(yi, 0, sourceImage.Height - 1);

            return sourceImage.GetPixel(xi,yi);
        }
    }
}
