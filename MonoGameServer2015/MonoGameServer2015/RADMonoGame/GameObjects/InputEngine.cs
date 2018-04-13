using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Engines
{
    public enum Direction { None, Up, Down, Left, Right }

    class InputEngine : GameComponent
    {
        const float Deadzone = 0.8f;
        const float DiagonalAvoidance = 0.2f;

        public static Color clearColor;

        private static GamePadState previousPadState;
        private static GamePadState currentPadState;
        private static KeyboardState previousKeyState;
        private static KeyboardState currentKeyState;
        private static Vector2 previousMousePos;
        private static Vector2 currentMousePos;
        private static MouseState previousMouseState;
        private static MouseState currentMouseState;

        public InputEngine(Game _game)
            : base(_game)
        {
            currentPadState = GamePad.GetState(PlayerIndex.One);
            currentKeyState = Keyboard.GetState();
            _game.Components.Add(this);
        }

        public static Direction GetDirection(Vector2 gamepadThumbStick)
        {
            var length = gamepadThumbStick.Length();
            if (length < Deadzone)
                return Direction.None;

            var absX = Math.Abs(gamepadThumbStick.X);
            var absY = Math.Abs(gamepadThumbStick.Y);
            var absDiff = Math.Abs(absX - absY);

            if (absDiff < length * DiagonalAvoidance)
                return Direction.None;

            if (absX > absY)
            {
                if (gamepadThumbStick.X > 0)
                    return Direction.Right;
                else
                    return Direction.Left;
            }
            else
            {
                if (gamepadThumbStick.Y > 0)
                    return Direction.Up;
                else
                    return Direction.Down;
            }
        }

        public static void ClearState()
        {
            previousMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();
            previousKeyState = Keyboard.GetState();
            currentKeyState = Keyboard.GetState();
        }

        public override void Update(GameTime gametime)
        {
            PreviousPadState = currentPadState;
            previousKeyState = currentKeyState;

            currentPadState = GamePad.GetState(PlayerIndex.One);
            currentKeyState = Keyboard.GetState();


            previousMouseState = currentMouseState;
            currentMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            currentMouseState = Mouse.GetState();


            KeysPressedInLastFrame.Clear();
            CheckForTextInput();

            base.Update(gametime);
        }

        public List<string> KeysPressedInLastFrame = new List<string>();

        private void CheckForTextInput()
        {
            foreach (var key in Enum.GetValues(typeof(Keys)) as Keys[])
            {
                if (IsKeyPressed(key))
                {
                    KeysPressedInLastFrame.Add(key.ToString());
                    break;
                }
            }
        }

        public static bool IsButtonPressed(Buttons buttonToCheck)
        {
            if (currentPadState.IsButtonUp(buttonToCheck) && PreviousPadState.IsButtonDown(buttonToCheck))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsButtonHeld(Buttons buttonToCheck)
        {
            if (currentPadState.IsButtonDown(buttonToCheck))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsKeyHeld(Keys buttonToCheck)
        {
            if (currentKeyState.IsKeyDown(buttonToCheck))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsKeyPressed(Keys keyToCheck)
        {
            if (currentKeyState.IsKeyUp(keyToCheck) && previousKeyState.IsKeyDown(keyToCheck))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static GamePadState CurrentPadState
        {
            get { return currentPadState; }
            set { currentPadState = value; }
        }
        public static KeyboardState CurrentKeyState
        {
            get { return currentKeyState; }
        }

        public static MouseState CurrentMouseState
        {
            get { return currentMouseState; }
        }

        public static MouseState PreviousMouseState
        {
            get { return previousMouseState; }
        }



        public static bool IsMouseLeftClick()
        {
            if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public static bool IsMouseRightClick()
        {
            if (currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public static bool IsMouseRightHeld()
        {
            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public static bool IsMouseLeftHeld()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public static Vector2 MousePosition
        {
            get { return currentMousePos; }
        }

        public static GamePadState PreviousPadState
        {
            get
            {
                return previousPadState;
            }

            set
            {
                previousPadState = value;
            }
        }
    }
}
