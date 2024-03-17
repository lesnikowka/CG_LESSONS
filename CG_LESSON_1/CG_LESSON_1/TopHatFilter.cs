using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class TopHatFilter : Filters
    {
        float[,] kernel = null;
        public TopHatFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            var cFilter = new ClosingFilter(kernel);
            var eFilter = new ErosionFilter(kernel);

            Bitmap resultImage = cFilter.processImage(sourceImage, worker);
            resultImage = eFilter.processImage(sourceImage, worker);

            return resultImage;
        }
    }
}
