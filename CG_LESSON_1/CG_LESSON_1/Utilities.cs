using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_LESSON_1
{
    class Utilities
    {
        public static byte GetIntensity(Color color)
        {
            return (byte)(0.36f * color.R + 0.53f * color.G + 0.11f * color.B);
        }
    }
}
