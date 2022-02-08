using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using CSharpSfmlRayCasting.Entities;
using CSharpSfmlRayCasting.Enums;
using CSharpSfmlRayCasting.Utils;

namespace CSharpSfmlRayCasting.Core
{
    class RayCaster
    {
        private World _world = null;
        private Player _player = null;

        public List<Ray> Rays { get; set; } = new List<Ray>();

        public VertexArray FovRender2D { get; set; } = null;

        public RayCaster(ref World world, ref Player player)
        {
            _world = world;
            _player = player;

            float lowerBound = -1.0f * Config.PLAYER_FOV / 2.0f;
            float upperBound = Config.PLAYER_FOV / 2.0f;

            // create all the rays to cast through the world
            for (float delta = lowerBound; delta < upperBound; delta += Config.RAY_DIFF_ANGLE)
                Rays.Add(new Ray(_player.Position, _player.Rotation + delta));

            // create the 2D render of the FOV
            FovRender2D = new VertexArray(PrimitiveType.TriangleFan, (uint)Rays.Count + 1);
            FovRender2D[0] = new Vertex(_player.Position, Color.White);

            for(int i = 1; i <= Rays.Count; ++i)
                FovRender2D[(uint)i] = new Vertex(Rays[i - 1].Destination, Color.White);
        }

        public void CastRays()
        {
            float lowerBound = -1.0f * Config.PLAYER_FOV / 2.0f;
            float upperBound = Config.PLAYER_FOV / 2.0f;

            int rayIndex = 0;

            Vector2f tempDest = _player.Position;

            for (float i = lowerBound; i < upperBound; i += Config.RAY_DIFF_ANGLE)
            {
                Rays[rayIndex].Rotation = _player.Rotation + MathExt.DegToRad(i);
                Rays[rayIndex].Origin = _player.Position;
                Rays[rayIndex].Destination = _player.Position;

                tempDest = _player.Position;

                for (int j = 0; j < Config.RAY_LENGTH; ++j)
                {
                    tempDest.X = _player.Position.X + j * (float)Math.Cos(Rays[rayIndex].Rotation);
                    tempDest.Y = _player.Position.Y + j * (float)Math.Sin(Rays[rayIndex].Rotation);

                    int grid_x = (int)(tempDest.X / Config.BLOC_SIZE);
                    int grid_y = (int)(tempDest.Y / Config.BLOC_SIZE);

                    if (_world.MappedBlocs[grid_x, grid_y].Type != BlocType.BACKGROUND)
                        break;
                }

                Rays[rayIndex].Destination = tempDest;

                ++rayIndex;
            }

            // update FOV render 2D
            for (int i = 1; i <= Rays.Count; ++i)
                FovRender2D[(uint)i] = new Vertex(Rays[i - 1].Destination, Color.White);

            FovRender2D[0] = new Vertex(_player.Position, Color.White);

            return;
        }
    }
}
