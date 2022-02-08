using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CSharpSfmlRayCasting.Entities
{
    class Ray
    {
        public Vector2f Origin { get; set; }
        public Vector2f Destination { get; set; }

        public float Rotation { get; set; } = 0.0f;

        public Ray(Vector2f origin, float rotation)
        {
            Origin = origin;
            Destination = origin;

            Rotation = rotation;
        }

        public float DistortedLength
        {
            get
            {
                float delta_x = Math.Abs(Origin.X - Destination.X);
                float delta_y = Math.Abs(Origin.Y - Destination.Y);

                return (float)Math.Sqrt(Math.Pow(delta_x, 2) + Math.Pow(delta_y, 2));
            }
        }
    }
}
