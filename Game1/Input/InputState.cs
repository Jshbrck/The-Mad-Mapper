using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MapGenerator.Input
{
    public class InputState
    {
        public MouseState CurrentMouseState { get; private set; }
        public MouseState LastMouseState    { get; private set; }
        public KeyboardState CurrentKeyState { get; private set; }
        public KeyboardState LastKeyState { get; private set; }

        /// <summary>
        /// Constructs a new Input State
        /// </summary>
        public InputState()
        {
            CurrentMouseState = new MouseState();
            LastMouseState = new MouseState();

            CurrentKeyState = new KeyboardState();
            LastKeyState = new KeyboardState();
        }

        /// <summary>
        /// Reads latest keyboard and Mouse state.
        /// </summary>
        public void Update()
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            LastKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();
        }
        /// <summary>
        /// Helper for checking if the Up Key is Pressed
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsKeyUpPress(out KeyboardState state)
        {
            state = CurrentKeyState;
            return (CurrentKeyState.IsKeyDown(Keys.Up));
        }
        /// <summary>
        /// Helper for checking if the Down Key is Pressed
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsKeyDownPress(out KeyboardState state)
        {
            state = CurrentKeyState;
            return (CurrentKeyState.IsKeyDown(Keys.Down));
        }
        /// <summary>
        /// Helper for checking if the Left Key is Pressed
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsKeyLeftPress(out KeyboardState state)
        {
            state = CurrentKeyState;
            return (CurrentKeyState.IsKeyDown(Keys.Left));
        }
        /// <summary>
        /// Helper for checking if the Right Key is Pressed
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsKeyRightPress(out KeyboardState state)
        {
            state = CurrentKeyState;
            return (CurrentKeyState.IsKeyDown(Keys.Right));
        }
        /// <summary>
        /// Helper for checking if a new Left Mouse click occured
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsNewLeftMouseClick(out MouseState state)
        {
            state = CurrentMouseState;
            return (CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed);
        }
        /// <summary>
        /// Helper for checking if a new Right Mouse click occured
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsNewRightMouseClick(out MouseState state)
        {
            state = CurrentMouseState;
            return (CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed);
        }
        /// <summary>
        /// Helper for checking if a new Middle Mouse click occured
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsNewThirdMouseClick(out MouseState state)
        {
            state = CurrentMouseState;
            return (CurrentMouseState.MiddleButton == ButtonState.Released && LastMouseState.MiddleButton == ButtonState.Pressed);
        }
        /// <summary>
        /// Helper for checking if mouse wheel was scrolled up.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsNewMouseScrollUp(out MouseState state)
        {
            state = CurrentMouseState;
            return (CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue);
        }
        /// <summary>
        /// Helper for checking if mouse wheel was scrolled down.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsNewMouseScrollDown(out MouseState state)
        {
            state = CurrentMouseState;
            return (CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue);
        }
        /// <summary>
        /// Helper for checking if mouse has been right pressed and dragged.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool IsNewRightMouseDrag(out MouseState state)
        {
            state = CurrentMouseState;
            return (LastMouseState.RightButton == ButtonState.Pressed && CurrentMouseState.Position != LastMouseState.Position);
        }

        public bool IsZoomOut()
        {
            MouseState MouseState;
            return IsNewMouseScrollUp(out MouseState);
        }

        public bool IsZoomIn()
        {
            MouseState MouseState;
            return IsNewMouseScrollDown(out MouseState);
        }
        
    }
}
