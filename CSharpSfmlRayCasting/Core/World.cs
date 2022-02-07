using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpSfmlRayCasting.Entities;
using CSharpSfmlRayCasting.Enums;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace CSharpSfmlRayCasting.Core
{
    class World
    {
        public List<Bloc> WorldBlocs { get; set; } = new List<Bloc>();
        public Bloc[,] MappedBlocs { get; set; } = null;

        public List<VertexArray> WorldMatrix { get; set; } = new List<VertexArray>();

        public int Width { get; } = 0;
        public int Height { get; } = 0;

        public World(string path)
        {
            Image imgMap = new Image(path);

            Width = (int)imgMap.Size.X;
            Height = (int)imgMap.Size.Y;

            MappedBlocs = new Bloc[Width, Height];

            for (uint i = 0; i < Width; ++i)
            {
                for (uint j = 0; j < Height; ++j)
                {
                    if (
                        imgMap.GetPixel(i, j).R == 0 &&
                        imgMap.GetPixel(i, j).G == 255 &&
                        imgMap.GetPixel(i, j).B == 0)
                    {
                        MappedBlocs[i, j] = new Bloc((int)i, (int)j, BlocType.BORDER);
                        WorldBlocs.Add(MappedBlocs[i, j]);
                    }
                    else
                    {
                        MappedBlocs[i, j] = new Bloc((int)i, (int)j, BlocType.BACKGROUND);
                    }
                }
            }

            for(int i = 1; i < Width; ++i)
            {
                VertexArray col = new VertexArray(PrimitiveType.Lines, 2);
                col[0] = new Vertex(new Vector2f(i * Config.BLOC_SIZE, 0), Color.Black);
                col[1] = new Vertex(new Vector2f(i * Config.BLOC_SIZE, Height * Config.BLOC_SIZE), Color.Black);

                WorldMatrix.Add(col);
            }

            for(int i = 1; i < Height; ++i)
            {
                VertexArray col = new VertexArray(PrimitiveType.Lines, 2);
                col[0] = new Vertex(new Vector2f(0, i * Config.BLOC_SIZE), Color.Black);
                col[1] = new Vertex(new Vector2f(Width * Config.BLOC_SIZE, i * Config.BLOC_SIZE), Color.Black);

                WorldMatrix.Add(col);
            }
        }
    }
}
