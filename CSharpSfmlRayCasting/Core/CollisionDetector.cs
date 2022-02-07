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

namespace CSharpSfmlRayCasting.Core
{
    class CollisionDetector
    {
        private World _world;
        private Player _player;

        public CollisionDetector(ref World world, ref Player player)
        {
            _world = world;
            _player = player;
        }

        public void DetectAndCorrectCollisions()
        {
            FloatRect playerGlobalBounds = _player.GlobalBounds;

            Vector2i currentGridPos = new Vector2i((int)(_player.Position.X / Config.BLOC_SIZE), (int)(_player.Position.Y / Config.BLOC_SIZE));

            for (int d_x = -2; d_x <= 2; ++d_x)
            {
                for (int d_y = -2; d_y <= 2; ++d_y)
                {
                    if (currentGridPos.X + d_x >= 0 && currentGridPos.X + d_x < _world.Width && currentGridPos.Y + d_y >= 0 && currentGridPos.Y + d_y < _world.Height)
                    {
                        Bloc bloc = _world.MappedBlocs[currentGridPos.X + d_x, currentGridPos.Y + d_y];

                        if (bloc.Type != Enums.BlocType.BACKGROUND)
                        {
                            if (playerGlobalBounds.Intersects(bloc.Bounds))
                            {
                                Border border = new Border();

                                if (PlayerIntersects(bloc, ref border))
                                {
                                    float delta = Config.PLAYER_RADIUS - border.Delta;

                                    switch (border.Direction)
                                    {
                                        case Direction.TOP:
                                            _player.MoveY((-1.0f) * delta);
                                            break;
                                        case Direction.BOTTOM:
                                            _player.MoveY(delta);
                                            break;
                                        case Direction.LEFT:
                                            _player.MoveX((-1.0f) * delta);
                                            break;
                                        case Direction.RIGHT:
                                            _player.MoveX(delta);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool PlayerIntersects(Bloc bloc, ref Border returnBorder)
        {
            Vector2f bpos = bloc.WindowPos;
            Vector2f ppos = _player.Position;

            List<Border> borders = new List<Border>
            {
                new Border(new Vector2f(bpos.X + Config.BLOC_SIZE, bpos.Y), Direction.TOP),
                new Border(new Vector2f(bpos.X, bpos.Y + Config.BLOC_SIZE), Direction.BOTTOM),
                new Border(new Vector2f(bpos.X, bpos.Y), Direction.LEFT),
                new Border(new Vector2f(bpos.X + Config.BLOC_SIZE, bpos.Y + Config.BLOC_SIZE), Direction.RIGHT)
            };

            for (int i = 0; i < Config.BLOC_SIZE; ++i)
            {
                foreach (Border border in borders)
                {
                    border.TempDelta = GetDelta(ppos, border.Point);

                    if (border.TempDelta < Config.PLAYER_RADIUS)
                    {
                        border.Collision = true;
                        if (border.TempDelta < border.Delta)
                        {
                            border.Delta = border.TempDelta;
                        }
                    }

                    border.NextPoint();
                }
            }

            List<Border> collisions = new List<Border>();

            foreach (Border border in borders)
            {
                if (border.Collision)
                    collisions.Add(border);
            }

            if (collisions.Count > 0)
            {
                //returnBorder = *min_element(collisions.begin(), collisions.end(), [](Border & a, Border & b) { return a._delta < b._delta; });

                returnBorder = collisions.OrderBy(x => x.Delta).First();

                return true;
            }

            return false;
        }

        private float GetDelta(Vector2f point_a, Vector2f point_b)
        {
            float delta_x = Math.Abs(point_a.X - point_b.X);
            float delta_y = Math.Abs(point_a.Y - point_b.Y);

            return (float)Math.Sqrt(Math.Pow(delta_x, 2.0f) + Math.Pow(delta_y, 2.0f));
        }
    }

    class Border
    {
        public Direction Direction { get; set; } = Direction.NONE;

        public Vector2f Point { get; set; }

        public float Delta { get; set; } = Config.PLAYER_RADIUS;
        public float TempDelta { get; set; } = 0.0f;

        public bool Collision { get; set; } = false;

        public Border()
        {

        }

        public Border(Vector2f point, Direction direction)
        {
            Direction = direction;
            Point = point;
        }

        public void NextPoint()
        {
            float x = Point.X;
            float y = Point.Y;

            switch (Direction)
            {
                case Direction.TOP:
                    x -= 1;
                    break;
                case Direction.BOTTOM:
                    x += 1;
                    break;
                case Direction.LEFT:
                    y += 1;
                    break;
                case Direction.RIGHT:
                    y -= 1;
                    break;
            }

            Point = new Vector2f(x, y);
        }
    }
}
