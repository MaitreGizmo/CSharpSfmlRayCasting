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
    }
}
