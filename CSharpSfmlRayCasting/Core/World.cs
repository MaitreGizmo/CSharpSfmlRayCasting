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
        }
    }
}
