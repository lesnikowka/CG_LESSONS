using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class SobelFilter : MatrixFilter
    {
        public SobelFilter(bool xAxis)
        {
            createSobelFilterKernel(xAxis);
        }

        private void createSobelFilterKernel(bool xAxis)
        {
            if (xAxis)
            {
                kernel = new float[,] 
                {
                    { -1, 0 ,1},
                    { -2, 0, 2},
                    { -1, 0, 1}
                };
            }
            else
            {
                kernel = new float[,]
                {
                    {-1, -2, -1 },
                    { 0, 0, 0},
                    { 1, 2, 1}
                };
            }
        }
    }
}
