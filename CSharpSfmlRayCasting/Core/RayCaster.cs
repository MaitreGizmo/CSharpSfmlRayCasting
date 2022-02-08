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

        public RayCaster(ref World world, ref Player player)
        {
            _world = world;
            _player = player;

            float lowerBound = -1.0f * Config.PLAYER_FOV / 2.0f;
            float upperBound = Config.PLAYER_FOV / 2.0f;

            for (float delta = lowerBound; delta < upperBound; delta += Config.RAY_DIFF_ANGLE)
                Rays.Add(new Ray(_player.Position, _player.Rotation + delta));
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
                Rays[rayIndex].SetOrigin(_player.Position);
                Rays[rayIndex].SetDestination(_player.Position);

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

                Rays[rayIndex].SetDestination(tempDest);

                ++rayIndex;
            }

            return;
        }
    }
}
