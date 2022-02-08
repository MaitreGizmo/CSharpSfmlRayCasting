using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using CSharpSfmlRayCasting.Entities;
using CSharpSfmlRayCasting.Enums;

namespace CSharpSfmlRayCasting.Core
{
    class Game
    {
        private World _world = null;
        private Player _player = null;

        private RenderWindow _window2D = null;
        private RenderWindow _window3D = null;

        private CollisionDetector _collisionDetector = null;
        private RayCaster _rayCaster = null;

        Stopwatch _clock = null;

        public Game()
        {
            _world = new World(Config.MAP_PATH);

            if (Config.SHOW_2D_WINDOW)
            {
                _window2D = new RenderWindow(new VideoMode((uint)(_world.Width * Config.BLOC_SIZE), (uint)(_world.Height * Config.BLOC_SIZE)), "C# - SFML - RayCasting - 2D RenderWindow", Styles.Close);
                _window2D.Closed += new EventHandler(Window2D_Closed);
            }

            if (Config.SHOW_3D_WINDOW)
            {
                _window3D = new RenderWindow(new VideoMode(Config.WIN3D_WIDTH, Config.WIN3D_HEIGHT), "C# - SFML - RayCasting - 3D RenderWindow", Styles.Close);
                _window3D.Closed += new EventHandler(Window3D_Closed);
            }

            _player = new Player();

            _collisionDetector = new CollisionDetector(ref _world, ref _player);
            _rayCaster = new RayCaster(ref _world, ref _player);
        }

        public void Run()
        {
            _clock = new Stopwatch();
            _clock.Start();

            while ((_window2D != null && _window2D.IsOpen) || (_window3D != null && _window3D.IsOpen))
            {
                DispatchWindowsEvents();

                if (_clock.ElapsedMilliseconds >= Config.FRAME_DIFF_TIME)
                {
                    ManageKeyboardInputs();

                    _collisionDetector.DetectAndCorrectCollisions();

                    _rayCaster.CastRays();

                    _clock.Restart();
                }

                Window2D_DisplayFrame();

                Window3D_DisplayFrame();
            }
        }

        private void ManageKeyboardInputs()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                _player.Rotate(Direction.LEFT);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                _player.Rotate(Direction.RIGHT);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                _player.Move(Direction.FRONT, Keyboard.IsKeyPressed(Keyboard.Key.LShift));

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                _player.Move(Direction.BACK, Keyboard.IsKeyPressed(Keyboard.Key.LShift));
        }

        #region DISPLAY_FRAME

        private void Window2D_DisplayFrame()
        {
            if (_window2D != null && _window2D.IsOpen)
            {
                _window2D.Clear(Config.WIN_2D_CLEAR_COLOR);

                _world.WorldBlocs.ForEach(x => _window2D.Draw(x.Vertices));

                _window2D.Draw(_rayCaster.FovRender2D);

                _window2D.Draw(_player.Body);
                _window2D.Draw(_player.Eye);

                _window2D.Display();
            }
        }

        private void Window3D_DisplayFrame()
        {
            if (_window3D != null && _window3D.IsOpen)
            {
                _window3D.Clear(Config.WIN_3D_CLEAR_COLOR);



                _window3D.Display();
            }
        }

        #endregion

        #region WINDOWS_EVENTS

        private void DispatchWindowsEvents()
        {
            if (_window2D != null && _window2D.IsOpen)
            {
                _window2D.DispatchEvents();
            }

            if (_window3D != null && _window3D.IsOpen)
            {
                _window3D.DispatchEvents();
            }
        }

        private void Window2D_Closed(object sender, EventArgs e)
        {
            _window2D.Close();
        }

        private void Window3D_Closed(object sender, EventArgs e)
        {
            _window3D.Close();
        }

        #endregion

    }
}
