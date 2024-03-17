using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    public enum WaveType
    {
        VERTICAL,
        HORIZONTAL
    }
    class WaveFilter : Filters
    {
        WaveType waveType;
        public WaveFilter(WaveType waveType)
        {
            this.waveType = waveType;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int xi;

            if (waveType == WaveType.VERTICAL) 
            {
                xi = (int)(x + 20 * Math.Sin(2 * Math.PI * y / 60));
            }
            else 
            {
                xi = (int)(x + 20 * Math.Sin(2 * Math.PI * x / 30));
            }

            xi = Clamp(xi, 0, sourceImage.Width - 1);

            return sourceImage.GetPixel(xi, y);
        }
    }
}
