using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() 
        {
        }

        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radX = kernel.GetLength(0) / 2;
            int radY = kernel.GetLength(1) / 2;
            float resR = 0;
            float resG = 0; 
            float resB = 0;

            for (int l = - radY; l <= radY; l++)
            {
                for (int k = -radX; k <= radX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);

                    Color neighbourColor = sourceImage.GetPixel(idX, idY);

                    resR += neighbourColor.R * kernel[k + radX, l + radY];
                    resG += neighbourColor.G * kernel[k + radX, l + radY];
                    resB += neighbourColor.B * kernel[k + radX, l + radY];
                    
                }
            }

            return Color.FromArgb(
                Clamp((int)resR, 0, 255)
                , Clamp((int)resG, 0, 255)
                , Clamp((int)resG, 0, 255)
                );
        }
    }
}
