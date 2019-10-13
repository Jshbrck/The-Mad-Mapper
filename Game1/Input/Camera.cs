using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Input
{
    public class Camera
    {
        public float Zoom { get; private set; }
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public Vector2 ViewportCenter { get { return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f); } }

        /// <summary>
        /// Initializes the camera to the center of the passed viewport dimensions and sets the Viewport
        /// </summary>
        /// <param name="ViewportWidth"></param>
        /// <param name="ViewportHeight"></param>
        public void SetAndCenterViewport(int ViewportWidth, int ViewportHeight)
        {
            this.ViewportWidth = ViewportWidth;
            this.ViewportHeight = ViewportHeight;
            Vector2 Center = new Vector2(ViewportWidth/2, ViewportHeight/2);
            CenterOn(Center);
        }

        /// <summary>
        /// Matrix to offset all drawings. A cast to int is used to avoid filtering artifacts.
        /// We negate the positions to simulate a moving camera
        /// </summary>
        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                       Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                       Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }
        /// <summary>
        /// Adjust Zoom by passed in value.
        /// To Zoom in pass positive values.
        /// To Zoom out pass negative values
        /// </summary>
        /// <param name="amt"></param>
        public void AdjustZoom(float amt)
        {
            Zoom += amt;
            if (Zoom < 0.25f)
            {
                Zoom = 0.25f;
            }
        }

        /// <summary>
        /// Move camera in X and Y amount based on passed CameraMovement.
        /// If ClampToMap is true, the camera will try to not to pan outside of the maps bounds
        /// </summary>
        /// <param name="CameraMovement"></param>
        /// <param name="ClampToMap"></param>
        public void MoveCamera(Vector2 CameraMovement, bool ClampToMap = false)
        {
            Vector2 NewPos = Position + CameraMovement;

            if (ClampToMap)
            {
                Position = MapClampedPosition(NewPos);
            }
            else
            {
                Position = NewPos;
            }
        }

        public Rectangle ViewportWorldBoundry()
        {
            Vector2 viewPortCorner = ScreenToWorld(new Vector2(0, 0));
            Vector2 viewPortBottomCorner =
               ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));

            return new Rectangle((int)viewPortCorner.X,
               (int)viewPortCorner.Y,
               (int)(viewPortBottomCorner.X - viewPortCorner.X),
               (int)(viewPortBottomCorner.Y - viewPortCorner.Y));
        }

        // Center the camera on specific pixel coordinates
        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        private Vector2 MapClampedPosition(Vector2 Pos)
        {
            Vector2 CameraMax = new Vector2(GlobalVariables.SCREEN_WIDTH - (ViewportWidth / Zoom / 2),
                                            GlobalVariables.SCREEN_HEIGHT - (ViewportHeight / Zoom / 2));
            return Vector2.Clamp(Pos,
                                new Vector2(ViewportWidth / Zoom / 2, ViewportHeight / Zoom / 2),
                                CameraMax);
        }

        public Vector2 WorldToScreen(Vector2 WorldPos)
        {
            return Vector2.Transform(WorldPos, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 ScreenPos)
        {
            return Vector2.Transform(ScreenPos, Matrix.Invert(TranslationMatrix));
        }

        public bool IsMouseInViewport(MouseState CurState)
        {
            Vector2 NormMax = new Vector2(ViewportWidth, ViewportHeight);
            Vector2 NormCur = new Vector2(CurState.X, CurState.Y);
            Vector2 NormMin = ScreenToWorld(Vector2.Zero);
            NormMax = ScreenToWorld(NormMax);
            NormCur = ScreenToWorld(NormCur);

            if (NormCur.X > NormMin.X && NormCur.X <= NormMax.X && NormCur.Y > NormMin.Y && NormCur.Y <= NormMax.Y) return true;
            else return false;
        }

        public void DoInput(InputState InState)
        {
            MouseState mouse;
            Vector2 CameraMovement = Vector2.Zero;
            
            if (InState.IsZoomIn())
            {
                AdjustZoom(-0.25f);
            }
            if (InState.IsZoomOut())
            {
                AdjustZoom(0.25f);
            }
            if (InState.IsNewRightMouseDrag(out mouse) && IsMouseInViewport(InState.CurrentMouseState))
            {
                CameraMovement.X = -(InState.CurrentMouseState.X - InState.LastMouseState.X);
                CameraMovement.Y = -(InState.CurrentMouseState.Y - InState.LastMouseState.Y);
            }

            MoveCamera(CameraMovement);
        }

        public Camera()
        {
            Zoom = 1.0f;
        }

        

    }
}
