using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using CSharpSfmlRayCasting.Utils;
using CSharpSfmlRayCasting.Enums;

namespace CSharpSfmlRayCasting.Entities
{
    class Player
    {
        public float Rotation { get; set; }
        public Vector2f Position { get; set; }

        public CircleShape Body { get; set; } = null;
        public RectangleShape Eye { get; set; } = null;

        public FloatRect GlobalBounds
        {
            get
            {
                return Body.GetGlobalBounds();
            }
        }

        public Player()
        {
            Rotation = 0.0f;
            Position = new Vector2f(Config.PLAYER_SPAWN_X, Config.PLAYER_SPAWN_Y);

            Body = new CircleShape(Config.PLAYER_RADIUS);
            Body.Position = Position;
            Body.Origin = new Vector2f(Config.PLAYER_RADIUS, Config.PLAYER_RADIUS);
            Body.FillColor = Color.Red;
            Body.Rotation = Rotation;

            Eye = new RectangleShape(new Vector2f(Config.PLAYER_RADIUS, 2.0f));
            Eye.Position = Position;
            Eye.Origin = new Vector2f(0.0f, 1.0f);
            Eye.FillColor = Color.White;
            Eye.Rotation = Rotation;
        }

        public void Rotate(Direction dir)
        {
            if (dir == Direction.LEFT)
                Rotation -= Config.PLAYER_ROTATE_SPEED;
            else
                Rotation += Config.PLAYER_ROTATE_SPEED;

            CorrectRotationMultiples();

            Body.Rotation = MathExt.RadToDeg(Rotation);
            Eye.Rotation = MathExt.RadToDeg(Rotation);
        }

        private void CorrectRotationMultiples()
        {
            float rad360deg = MathExt.DegToRad(360.0f);

            if (Rotation > rad360deg)
            {
                int mult = (int)Math.Abs(Rotation / rad360deg);

                Rotation -= mult * rad360deg;
            }
            else if (Rotation < (-1.0f) * rad360deg)
            {
                int mult = (int)Math.Abs(Rotation / rad360deg);

                Rotation += mult * rad360deg;
            }
        }

        public void Move(Direction dir, bool sprint = false)
        {
            float moveX;
            float moveY;

            if (sprint)
            {
                moveX = Config.PLAYER_MOVE_SPEED * Config.PLAYER_SPRINT_MULT * (float)Math.Cos(Rotation);
                moveY = Config.PLAYER_MOVE_SPEED * Config.PLAYER_SPRINT_MULT * (float)Math.Sin(Rotation);
            }
            else
            {
                moveX = Config.PLAYER_MOVE_SPEED * (float)Math.Cos(Rotation);
                moveY = Config.PLAYER_MOVE_SPEED * (float)Math.Sin(Rotation);
            }

            Vector2f move = new Vector2f(moveX, moveY);

            if (dir == Direction.BACK)
            {
                move *= (-1.0f);
                Position -= new Vector2f(moveX, moveY);
            }
            else
            {
                Position += new Vector2f(moveX, moveY);
            }


            Body.Position = Position;
            Eye.Position = Position;
        }

        public void MoveX(float delta)
        {
            float x = Position.X + delta;
            float y = Position.Y;

            Position = new Vector2f(x, y);
        }

        public void MoveY(float delta)
        {
            float x = Position.X;
            float y = Position.Y + delta;

            Position = new Vector2f(x, y);
        }
    }
}
