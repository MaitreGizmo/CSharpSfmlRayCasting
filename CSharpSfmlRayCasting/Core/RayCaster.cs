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

        public VertexArray Render3D { get; set; } = null;

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

            for (int i = 1; i <= Rays.Count; ++i)
                FovRender2D[(uint)i] = new Vertex(Rays[i - 1].Destination, Color.White);

            // create the VertexArray for the 3D render
            Render3D = new VertexArray(PrimitiveType.Lines, (uint)Rays.Count * 2);
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

                for (float j = 0.0f; j < Config.RAY_LENGTH; j += 1.0f)
                {
                    tempDest.X = _player.Position.X + j * (float)Math.Cos(Rays[rayIndex].Rotation);
                    tempDest.Y = _player.Position.Y + j * (float)Math.Sin(Rays[rayIndex].Rotation);

                    int grid_x = (int)(MathExt.RoundCoordinates(tempDest.X) / Config.BLOC_SIZE);
                    int grid_y = (int)(MathExt.RoundCoordinates(tempDest.Y) / Config.BLOC_SIZE);

                    if (_world.MappedBlocs[grid_x, grid_y].Type != BlocType.BACKGROUND)
                        break;
                }

                Rays[rayIndex].Destination = tempDest;

                ++rayIndex;
            }

            // update FOV render 2D
            for (int i = 1; i <= Rays.Count; ++i)
                FovRender2D[(uint)i] = new Vertex(Rays[i - 1].Destination, Config.FOV_RAY_COLOR);

            FovRender2D[0] = new Vertex(_player.Position, Config.FOV_RAY_COLOR);

            return;
        }

        public void Generate3DRender()
        {
            List<Vertex> vertices = new List<Vertex>();

            for (int i = 0; i < Rays.Count; ++i)
            {
                int grid_x = (int)(MathExt.RoundCoordinates(Rays[i].Destination.X) / Config.BLOC_SIZE);
                int grid_y = (int)(MathExt.RoundCoordinates(Rays[i].Destination.Y) / Config.BLOC_SIZE);

                if (_world.MappedBlocs[grid_x, grid_y].Type == BlocType.BORDER)
                {
                    float angle = (Rays[i].Rotation - _player.Rotation);

                    float correctLength = Rays[i].DistortedLength * (float)Math.Cos(angle);

                    float ratio = 1.0f - correctLength / Config.RAY_LENGTH;

                    float height = Config.WIN3D_HEIGHT * ratio;

                    float space_y = ((float)Config.WIN3D_HEIGHT - height) / 2.0f;

                    height = (float)Math.Round(height);
                    space_y = (float)Math.Round(space_y);

                    //Color color;
                    byte alpha_variation = (byte)(255.0f * ratio);

                    //switch (_world.MappedBlocs[grid_x, grid_y].GetBorderHitByRay(Rays[i].Destination)){
                    //    case Direction.CORNER:
                    //        color = new Color(0, 0, 0, alpha_variation);
                    //        break;
                    //    case Direction.TOP_BOTTOM:
                    //        color = new Color(0, 255, 0, alpha_variation);
                    //        break;
                    //    case Direction.LEFT_RIGHT:
                    //        color = new Color(255, 0, 0, alpha_variation);
                    //        break;
                    //    default:
                    //        color = new Color(255, 0, 132, alpha_variation);
                    //        break;
                    //}

                    Color color = new Color(0, 255, 0, alpha_variation);

                    vertices.Add(new Vertex(new Vector2f(i, space_y), color));
                    vertices.Add(new Vertex(new Vector2f(i, space_y + height), color));
                }
            }

            Render3D.Dispose();

            Render3D = new VertexArray(PrimitiveType.Lines, (uint)vertices.Count);

            for (int i = 0; i < vertices.Count; ++i)
                Render3D[(uint)i] = vertices[i];
        }
    }
}
