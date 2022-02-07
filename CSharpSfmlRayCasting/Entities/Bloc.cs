using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSfmlRayCasting.Enums;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CSharpSfmlRayCasting.Entities
{
    class Bloc
    {
        public Vector2i GridPos { get; }
        public Vector2f WindowPos { get; }
        public BlocType Type { get; }

        public VertexArray Vertices { get; } = null;

        public FloatRect Bounds
        {
            get
            {
                return Vertices.Bounds;
            }
        }

        public Bloc(int x, int y, BlocType type)
        {
            GridPos = new Vector2i(x, y);
            WindowPos = new Vector2f(x * Config.BLOC_SIZE, y * Config.BLOC_SIZE);
            Type = type;

            if(Type == BlocType.BORDER)
            {
                Vertices = new VertexArray(PrimitiveType.Quads, 4);
                Vertices[0] = new Vertex(new Vector2f(x * Config.BLOC_SIZE, y * Config.BLOC_SIZE), Color.Green);
                Vertices[1] = new Vertex(new Vector2f((x + 1) * Config.BLOC_SIZE, y * Config.BLOC_SIZE), Color.Green);
                Vertices[2] = new Vertex(new Vector2f((x + 1) * Config.BLOC_SIZE, (y + 1) * Config.BLOC_SIZE), Color.Green);
                Vertices[3] = new Vertex(new Vector2f(x * Config.BLOC_SIZE, (y + 1) * Config.BLOC_SIZE), Color.Green);
            }
        }

    }
}
