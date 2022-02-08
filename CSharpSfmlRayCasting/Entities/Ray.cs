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
        private Vector2f _origin;
        private Vector2f _destination;

        public Vector2f Origin
        {
            get
            {
                return _origin;
            }
        }

        public Vector2f Destination
        {
            get
            {
                return _destination;
            }
        }

        public float Rotation { get; set; } = 0.0f;

        private VertexArray _vertices = null;

        public VertexArray Vertices
        {
            get
            {
                return _vertices;
            }
        }

        public Ray(Vector2f origin, float rotation)
        {
            _origin = origin;
            _destination = origin;

            Rotation = rotation;

            _vertices = new VertexArray(PrimitiveType.Lines, 2);
            _vertices[0] = new Vertex(Origin, Color.White);
            _vertices[1] = new Vertex(Destination, Color.White);
        }

        public void SetOrigin(Vector2f orig)
        {
            _origin = orig;

            Vertex vertex = _vertices[0];

            vertex.Position = _origin;

            _vertices[0] = vertex;
        }

        public void SetDestination(Vector2f dest)
        {
            _destination = dest;

            Vertex vertex = _vertices[1];

            vertex.Position = _destination;

            _vertices[1] = vertex;
        }
    }
}
