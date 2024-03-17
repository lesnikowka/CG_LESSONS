using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class ClarityFilter : MatrixFilter
    {
        public ClarityFilter()
        {
            CreateClarityKernel();
        }
        private void CreateClarityKernel()
        {
            kernel = new float[,]{
                {0, -1, 0 },
                {-1, 5, -1 },
                {0, -1, 0 }
            };
        }
    }
}
