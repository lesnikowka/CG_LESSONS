using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class ClosingFilter : Filters
    {
        private float[,] kernel = null;
        public ClosingFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            var dFilter = new DilationFilter(kernel);
            var eFilter = new ErosionFilter(kernel);

            Bitmap resultImage = dFilter.processImage(sourceImage, worker, 50);
            resultImage = eFilter.processImage(resultImage, worker, 50);

            return resultImage;
        }
    }
}
