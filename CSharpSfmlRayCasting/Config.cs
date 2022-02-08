using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using CSharpSfmlRayCasting.Utils;

namespace CSharpSfmlRayCasting
{
    static class Config
    {
        // Simulation config
        public const uint FRAME_DIFF_TIME = 30; // [ms]

        public const bool SHOW_2D_WINDOW = true;
        public const bool SHOW_3D_WINDOW = false;

        // 2D Window config
        public static readonly Color WIN_2D_CLEAR_COLOR = new Color(150, 150, 150);

        // 3D Window config
        public const int WIN3D_WIDTH = 800; // [px]
        public const int WIN3D_HEIGHT = 400; // [px]
        public static readonly Color WIN_3D_CLEAR_COLOR = new Color(150, 150, 150);

        // 2D World render config
        public const uint BLOC_SIZE = 10; // [px]

        // Map config
        public const string MAP_PATH = @"Ressources\map.bmp";
        public const uint MAP_BORDER_COLOR_G = 255;

        // Player config
        public const int PLAYER_SPAWN_X = 500; // [px]
        public const int PLAYER_SPAWN_Y = 100; // [px]
        public const int PLAYER_RADIUS = (int)BLOC_SIZE / 2; // [px]
        public const float PLAYER_MOVE_SPEED = 2.5f; // [px / frame]
        public const float PLAYER_SPRINT_MULT = 2.0f;
        public static readonly float PLAYER_ROTATE_SPEED = MathExt.DegToRad(5.0f); // [deg] -> [rad]
        public const float PLAYER_FOV = 60.0f; // [deg]

        // RayCaster config
        public static readonly float RAY_DIFF_ANGLE = PLAYER_FOV / WIN3D_WIDTH;
        public const float RAY_LENGTH = 300.0f;
    }
}
