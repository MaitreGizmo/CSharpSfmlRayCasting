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
        public static readonly uint FRAME_DIFF_TIME = 30;
        public static readonly bool SHOW_DEBUG = true;

        // 2D Window config
        public static readonly uint WIN_2D_WIDTH = 800;
        public static readonly uint WIN_2D_HEIGHT = 600;
        public static readonly Color WIN_2D_CLEAR_COLOR = new Color(150, 150, 150);

        // 2D World render config
        public static readonly uint BLOC_SIZE = 20;

        // Map config
        public static readonly string MAP_PATH = @"Ressources\map.bmp";
        public static readonly uint MAP_BORDER_COLOR_G = 255;

        // Player config
        public static readonly int PLAYER_SPAWN_X = 1000;
        public static readonly int PLAYER_SPAWN_Y = 200;
        public static readonly int PLAYER_RADIUS = (int)BLOC_SIZE / 2;
        public static readonly float PLAYER_MOVE_SPEED = 2.5f;
        public static readonly float PLAYER_SPRINT_MULT = 2.0f;
        public static readonly float PLAYER_ROTATE_SPEED = MathExt.DegToRad(5.0f);
    }
}
