using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class SobelFilter : MatrixFilter
    {
        public SobelFilter()
        {
            createSobelFilterKernel();
        }

        private void createSobelFilterKernel()
        {
            //if (xAxis)
            //{
            //    kernel = new float[,] 
            //    {
            //        { -1, 0 ,1},
            //        { -2, 0, 2},
            //        { -1, 0, 1}
            //    };
            //}
            //else
            //{
            //    kernel = new float[,]
            //    {
            //        {-1, -2, -1 },
            //        { 0, 0, 0},
            //        { 1, 2, 1}
            //    };
            //}
            kernel = new float[,] {
                { -6, 0, 6},
                { 0, 0, 0},
                { -6, 0, 6}
            };
        }
    }
}
