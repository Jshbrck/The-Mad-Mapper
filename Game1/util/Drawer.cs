using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Forms.Services;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;




namespace MapGenerator.Util
{
    /// <summary>
    /// Methods should only be invoked in the Draw loop after base.Draw()
    /// Must begin drawing with BeginDrawing() end drawing with drawer.EndDrawing()
    /// </summary>
    class Drawer
    {
        Texture2D TileSet;
        Dictionary<String, Rectangle> Tiles;
        UpdateService Editor;
        DrawService Draw;
        float fTileScale;

        public Drawer(UpdateService editor, Dictionary<String, Rectangle> tiles, Texture2D tileset)
        {
            Tiles = tiles;
            Editor = editor;
            Draw = null;
            TileSet = tileset;
        }

        public Drawer(DrawService d, Dictionary<String, Rectangle> tiles, Texture2D tileset)
        {
            Tiles = tiles;
            Draw = d;
            Editor = null;
            TileSet = tileset;
        }

        void BeginDrawing()
        {
            if (Editor == null) Draw.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, GlobalVariables.CAMERA.TranslationMatrix);
            else Editor.spriteBatch.Begin(SpriteSortMode.BackToFront,null,null,null,null,null,GlobalVariables.CAMERA.TranslationMatrix);
        }

        void EndDrawing()
        {
            if (Editor == null) Draw.spriteBatch.End();
            else Editor.spriteBatch.End();
        }

        void DrawTile(String TileKey,Vector2 Pos, int Layer)
        {
            Vector2 Origin = new Vector2(0, 0);
            Rectangle TileRec;
            //if (TileKey != GlobalConstants.NULL)
            //{
                Tiles.TryGetValue(TileKey, out TileRec);
                if (Editor == null) Draw.spriteBatch.Draw(TileSet, Pos, TileRec, Color.White, 0f, Origin,this.fTileScale, SpriteEffects.None, (1f / (Layer + 1)));
                else Editor.spriteBatch.Draw(TileSet, Pos, TileRec, Color.White,0f,Origin,this.fTileScale,SpriteEffects.None,(1f/(Layer+1)));
            //}
        }

        /// <summary>
        /// Draws a Map.Layer object starting from the top left corner then filling out each row
        /// Uses Tile_Height and Tile_Width to determine the size of the drawn Tile
        /// </summary>
        public void DrawLayer(int Height, int Width, Map.Layer Layer)
        {
            Vector2 v = new Vector2(0);
            for (int h = 0; h < Height; h++, v.Y+=GlobalVariables.TILE_HEIGHT)
            {
                v.X = 0;
                for (int w = 0; w < Width; w++, v.X+= GlobalVariables.TILE_WIDTH)
                {
                    DrawTile(Layer.Tiles[w,h].Texture, v, Layer.Index);
                }
            }
        }
        /// <summary>
        /// Draws a Map.Layer object starting from the top left corner then filling out each row
        /// Uses Tile_Height and Tile_Width to determine the size of the drawn Tile
        /// </summary>
        public void DrawLayer(int Height, int Width, int TILE_HEIGHT, int TILE_WIDTH, Map.Layer Layer)
        {
            Vector2 v = new Vector2(0);
            for (int h = 0; h < Height; h++, v.Y += TILE_HEIGHT)
            {
                v.X = 0;
                for (int w = 0; w < Width; w++, v.X += TILE_WIDTH)
                {
                    DrawTile(Layer.Tiles[w, h].Texture, v, Layer.Index);
                }
            }
        }


        /// <summary>
        /// Draws a map object by starting with the 0th layer and drawing all layers on top of it
        /// </summary>
        public void DrawMap(Map M)
        {
            for(int i = 0; i < M.Layers.Count; i++)
            {
                this.fTileScale = GlobalVariables.TILE_SCALE;
                BeginDrawing();
                DrawLayer(M.Height, M.Width, M.Layers[i]);
                EndDrawing();
            }
        }

        /// <summary>
        /// Draws a map object by starting with the 0th layer and drawing all layers on top of it
        /// </summary>
        public void DrawMap(Map M, float fTileScale)
        {
            for (int i = 0; i < M.Layers.Count; i++)
            {
                this.fTileScale = fTileScale;
                BeginDrawing();
                DrawLayer(M.Height, M.Width, M.Layers[i]);
                EndDrawing();
            }
        }

        /// <summary>
        /// Draws a map object by starting with the 0th layer and drawing all layers on top of it.
        /// Allows control over each tiles Height and Width.
        /// </summary>
        /// <param name="M"></param>
        /// <param name="Height"></param>
        /// <param name="Width"></param>
        public void DrawMap(Map M, int Tile_Height, int Tile_Width)
        {
            for (int i = 0; i < M.Layers.Count; i++)
            {
                BeginDrawing();
                DrawLayer(M.Height, M.Width, Tile_Height, Tile_Width, M.Layers[i]);
                EndDrawing();
            }
        }
    }
}
