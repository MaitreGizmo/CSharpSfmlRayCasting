using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSfmlRayCasting.Utils
{
    static class MathExt
    {
        public static float DegToRad(float alpha)
        {
            return alpha * (float)Math.PI / 180.0f;
        }

        public static float RadToDeg(float alpha)
        {
            return alpha * 180.0f / (float)Math.PI;
        }

        public static int RoundCoordinates(float a)
        {
            if ((int)a % 10 == 0)
                return (int)a;

            if ((int)(a + 0.5f) % 10 == 0)
                return (int)(a + 0.5f);

            if ((int)(a - 0.5f) % 10 == 0)
                return (int)(a - 0.5f);

            return (int)Math.Round(a);
        }
    }
}
