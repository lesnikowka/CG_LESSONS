using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter(int n)
        {
            createMotionBlurKernel(n);
        }

        private void createMotionBlurKernel(int n)
        {
            kernel = new float[n, n];

            for (int i = 0; i < n; i++)
            {
                kernel[i, i] = 1f / n;
            }
        }
    }
}
